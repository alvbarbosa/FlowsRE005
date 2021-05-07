using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RE005.DB;
using RE005.Models;
using RE005.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RE005.Services
{
    internal class FlowExec : IFlowExec
    {
        public int sequence { get; }
        private readonly IFlowsParams _flowsParams;
        private readonly RE005Context _context;
        private readonly IHttpClientFactory _clientFactory;
        public FlowExec(IFlowsParams flowsParams, RE005Context context, IHttpClientFactory clientFactory)
        {
            _flowsParams = flowsParams;
            _context = context;
            _clientFactory = clientFactory;
        }

        public async Task<ResponseFlow> ExecuteFlowAsync(RequestFlow data)
        {
            FlowsExec flowExec;
            ResponseFlow responseFlow = new ResponseFlow();
            Flow paramFlow = _flowsParams.GetFlow(data.Flow);

            if (data.Id > 0)
            {
                flowExec = await _context.FlowsExecs
                    .Where(a => a.Id == data.Id)
                    .Include(a => a.FieldsByFlowExecs)
                    .ThenInclude(a => a.IdFieldNavigation)
                    .FirstOrDefaultAsync();

                if (flowExec.Sequence == paramFlow.Sequences)
                {
                    throw new Exception("Ya no tiene más pasos para ejecutar");
                }

                flowExec.FieldsByFlowExecs.ToList().ForEach(a =>
                {
                    if (!data.Fields.ContainsKey(a.IdFieldNavigation.Code))
                    {
                        data.Fields.Add(a.IdFieldNavigation.Code, a.Value);
                    }
                });
            }
            else
            {
                flowExec = _context.FlowsExecs.Add(new FlowsExec
                {
                    IdFlow = paramFlow.Id,
                    Sequence = 0
                }).Entity;
                await _context.SaveChangesAsync();
            }

            flowExec.Sequence += 1;


            List<StepsByFlow> steps = paramFlow.StepsByFlows.Where(a => a.Sequence == flowExec.Sequence).ToList();
            List<Task<Dictionary<int, (string, string)>>> tasks = new List<Task<Dictionary<int, (string, string)>>>();
            Dictionary<int, (string, string)> keyValues = new Dictionary<int, (string, string)>();
            steps.ForEach(a => tasks.Add(ExecuteStepAsync(a.IdStepNavigation, data.Fields)));
            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            tasks.ForEach(a => keyValues = keyValues.Concat(a.Result).GroupBy(a => a.Key).ToDictionary(a => a.Key, a=> a.First().Value));
            responseFlow.Fields = new Dictionary<string, string>();

            foreach (var item in keyValues)
            {
                FieldsByFlowExec fieldByFlow = _context.FieldsByFlowExecs.Where(a => a.IdFlowExec == flowExec.Id && a.IdField == item.Key).FirstOrDefault();

                if (fieldByFlow == null)
                {
                    fieldByFlow = new FieldsByFlowExec
                    {
                        IdField = item.Key,
                        IdFlowExec = flowExec.Id,
                        Value = item.Value.Item2
                    };
                    _context.FieldsByFlowExecs.Add(fieldByFlow);
                }
                else
                {
                    fieldByFlow.Value = item.Value.Item2;
                }
                responseFlow.Fields.Add(item.Value.Item1, item.Value.Item2);
            }
            await _context.SaveChangesAsync();

            responseFlow.Id = flowExec.Id;

            return responseFlow;
        }

        private async Task<Dictionary<int, (string, string)>> ExecuteStepAsync(Step step, Dictionary<string, string> data)
        {

            HttpClient client = _clientFactory.CreateClient("steps");
            Dictionary<int, (string, string)> res = new Dictionary<int, (string, string)>();

            Dictionary<string, string> fields = step.FieldsBySteps
                .Where(a => a.Type == "in")
                .ToDictionary(a => a.IdFieldNavigation.Code, a => data[a.IdFieldNavigation.Code]);

            string jsonInString = JsonConvert.SerializeObject(fields);
            HttpResponseMessage response = await client.PostAsync(step.Service, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var json = await response.Content.ReadAsStringAsync();
            Dictionary<string, string> pairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            res = step.FieldsBySteps
                .ToDictionary(a => a.IdField, a => (a.IdFieldNavigation.Code, pairs[a.IdFieldNavigation.Code]));
            

            return res;

        }



    }
}

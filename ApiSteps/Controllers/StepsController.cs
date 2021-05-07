using ApiSteps.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSteps.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StepsController : ControllerBase
    {
        [HttpPost("step1")]
        public Dictionary<string, string> Step1([FromBody]Dictionary<string, string> data)
        {
            data.Add("F-0002", "Andres");
            return data;
        }
        [HttpPost("step2")]
        public Dictionary<string, string> Step2([FromBody]Dictionary<string, string> data)
        {
            data.Add("F-0003", "Barbosa");
            return data;
        }
        [HttpPost("step3")]
        public Dictionary<string, string> Step3([FromBody]Dictionary<string, string> data)
        {
            data.Add("F-0004", "30");
            return data;
        }
        [HttpPost("step4")]
        public Dictionary<string, string> Step4([FromBody]Dictionary<string, string> data)
        {
            data.Add("F-0005", "A+");
            return data;
        }
    }
}

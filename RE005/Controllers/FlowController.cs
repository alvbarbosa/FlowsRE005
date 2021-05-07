using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RE005.Models;
using RE005.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RE005.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowController : ControllerBase
    {

        private readonly ILogger<FlowController> _logger;
        private readonly IFlowExec _flowExec;
        public FlowController(ILogger<FlowController> logger, IFlowExec flowExec)
        {
            _logger = logger;
            _flowExec = flowExec;
        }

        [HttpPost]
        public async Task<ResponseFlow> PostAsync([FromBody] RequestFlow data)
        {
            return await _flowExec.ExecuteFlowAsync(data);
        }

    }
}

using MetricsManager.Models;
using MetricsManager.Services.Repositorys;
using MetricsManager.Services.Repositorys.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentMetricsManagerRepository _agentMetricsRepository;

        public AgentsController(IAgentMetricsManagerRepository agentMetricsRepository)
        {
            _agentMetricsRepository = agentMetricsRepository;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] string uri)
        {
            _agentMetricsRepository.Create(uri);
            return Ok();
        }

        [HttpGet("get")]
        public ActionResult<List<AgentInfo>> GetAllAgents()
        {
            return Ok(_agentMetricsRepository.GetAll());
        }
    }
}

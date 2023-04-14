using MetricsManager.Models;
using MetricsManager.Models.Requests.Impl.Request;
using MetricsManager.Models.Requests.Impl.Response;
using MetricsManager.Services.Client;
using MetricsManager.Services.Repositorys;
using MetricsManager.Services.Repositorys.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private IAgentMetricsManagerRepository _agentMetricsManagerRepository;
        private IMetricsAgentClient _metricsAgentClient;

        public NetworkMetricsController(
            IMetricsAgentClient metricsAgentClient,
            IHttpClientFactory httpClientFactory,
            IAgentMetricsManagerRepository agentMetricsManagerRepository)
        {
            _httpClientFactory = httpClientFactory;
            _metricsAgentClient = metricsAgentClient;
            _agentMetricsManagerRepository = agentMetricsManagerRepository;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<CpuMetricsResponse> GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok(_metricsAgentClient.GetNetworkMetrics(new NetworkMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            }));
        }
    }
}

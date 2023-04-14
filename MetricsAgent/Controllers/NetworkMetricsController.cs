using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Services.Impl;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(
            INetworkMetricsRepository networkMetricsRepository,
            ILogger<NetworkMetricsController> logger,
            IMapper mapper)
        {
            _networkMetricsRepository = networkMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }



        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<GetNetworkMetricsResponse> GetNetworkMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok(new GetNetworkMetricsResponse
            {
                Metrics = _networkMetricsRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<NetworkMetricDto>(metric)).ToList()
            });
        }

        [HttpGet("all")]
        public ActionResult<GetNetworkMetricsResponse> GetAllCpuMetrics()
        {
            return Ok(_networkMetricsRepository.GetAll()
                .Select(metric => _mapper.Map<NetworkMetricDto>(metric)).ToList());
        }

    }
}

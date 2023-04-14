using AutoMapper;
using MetricsAgent.Converters;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMapper _mapper;


        public CpuMetricsController(
                    ICpuMetricsRepository cpuMetricsRepository,
                    ILogger<CpuMetricsController> logger,
                    IMapper mapper)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<GetCpuMetricsResponse> GetCpuMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok(new GetCpuMetricsResponse
            {
                Metrics = _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList()
            });
        }

        [HttpGet("all")]
        public ActionResult<GetCpuMetricsResponse> GetAllCpuMetrics()
        {
            return Ok(_cpuMetricsRepository.GetAll()
                .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList());
        }

    }
}

using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Models.Dto;
using MetricsAgent.Services.Impl;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMerticsController : ControllerBase
    {
        private readonly ILogger<RamMerticsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly IMapper _mapper;

        public RamMerticsController(
            IRamMetricsRepository ramMetricsRepository,
            ILogger<RamMerticsController> logger,
            IMapper mapper)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<GetRamMetricsResponse> GetRamMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok(new GetRamMetricsResponse
            {
                Metrics = _ramMetricsRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<RamMetricDto>(metric)).ToList()
            });
        }

        [HttpGet("all")]
        public ActionResult<GetRamMetricsResponse> GetAllCpuMetrics()
        {
            return Ok(_ramMetricsRepository.GetAll()
                .Select(metric => _mapper.Map<RamMetricDto>(metric)).ToList());
        }
    }
}

using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Models.Dto;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _hddMetricsRepository;
        private readonly IMapper _mapper;

        public HddMetricsController(
            IHddMetricsRepository hddMetricsRepository,
            ILogger<HddMetricsController> logger,
            IMapper mapper)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<GetHddMetricsResponse> GetHddMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok(new GetHddMetricsResponse
            {
                Metrics = _hddMetricsRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<HddMetricDto>(metric)).ToList()
            });
        }

        [HttpGet("all")]
        public ActionResult<GetHddMetricsResponse> GetAllCpuMetrics()
        {
            return Ok(_hddMetricsRepository.GetAll()
                .Select(metric => _mapper.Map<HddMetricDto>(metric)).ToList());
        }


    }
}

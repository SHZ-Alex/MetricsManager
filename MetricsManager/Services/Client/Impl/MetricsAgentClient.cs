using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Requests.Impl.Request;
using MetricsManager.Models.Requests.Impl.Response;
using MetricsManager.Services.Repositorys;
using MetricsManager.Services.Repositorys.Impl;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace MetricsManager.Services.Client.Impl
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly IAgentMetricsManagerRepository _agentMetricsRepository;
        private readonly HttpClient _httpClient;

        public MetricsAgentClient(
            HttpClient httpClient,
            IAgentMetricsManagerRepository agentMetricsRepository)
        {
            _httpClient = httpClient;
            _agentMetricsRepository = agentMetricsRepository;
        }


        public CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request)
        {
            AgentInfo agentInfo = _agentMetricsRepository.GetById(request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/cpu/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/" +
                $"{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                CpuMetricsResponse cpuMetricsResponse =
                    (CpuMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(CpuMetricsResponse));
                cpuMetricsResponse.AgentId = request.AgentId;
                return cpuMetricsResponse;
            }

            return null;
        }

        public HddMetricsResponse GetHddMetrics(HddMetricsRequest request)
        {
            AgentInfo agentInfo = _agentMetricsRepository.GetById(request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/hdd/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/" +
                $"{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                HddMetricsResponse cpuMetricsResponse =
                    (HddMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(HddMetricsResponse));
                cpuMetricsResponse.AgentId = request.AgentId;
                return cpuMetricsResponse;
            }

            return null;
        }

        public NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request)
        {
            AgentInfo agentInfo = _agentMetricsRepository.GetById(request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/network/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/" +
                $"{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                NetworkMetricsResponse cpuMetricsResponse =
                    (NetworkMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(NetworkMetricsResponse));
                cpuMetricsResponse.AgentId = request.AgentId;
                return cpuMetricsResponse;
            }

            return null;
        }

        public RamMetricsResponse GetRamMetrics(RamMetricsRequest request)
        {
            AgentInfo agentInfo = _agentMetricsRepository.GetById(request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/ram/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/" +
                $"{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                RamMetricsResponse cpuMetricsResponse =
                    (RamMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(RamMetricsResponse));
                cpuMetricsResponse.AgentId = request.AgentId;
                return cpuMetricsResponse;
            }

            return null;
        }
    }
}

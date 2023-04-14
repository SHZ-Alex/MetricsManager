using MetricsManager.Models;

namespace MetricsManager.Services.Repositorys
{
    public interface IAgentMetricsManagerRepository : IManagerRepository<AgentInfo>
    {
        public void Create(string newAgent);
    }
}

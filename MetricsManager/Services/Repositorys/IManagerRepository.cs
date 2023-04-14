namespace MetricsManager.Services.Repositorys
{
    public interface IManagerRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetById(int id);
    }
}

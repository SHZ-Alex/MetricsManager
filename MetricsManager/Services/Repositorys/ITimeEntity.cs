namespace MetricsManager.Services.Repositorys
{
    public interface ITimeEntity<T> where T : class
    {
        IList<T> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}

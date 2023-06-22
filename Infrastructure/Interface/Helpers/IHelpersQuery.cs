namespace Infrastructure.Interface.Helpers
{
    public interface IHelpersQuery
    {
        IEnumerable<T> ExecuteQuery<T, P>(string query, P parameters, string connectionString);
        Task<IEnumerable<T>> ExecuteQueryAsync<T, P>(string query, P parameters, string connectionString);
        IEnumerable<T> ExecuteQuery<T>(string query,string connectionString);
        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, string connectionString);
    }
}

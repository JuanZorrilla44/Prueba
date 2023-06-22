using Dapper;
using Infrastructure.Interface.Helpers;
using System.Data.SqlClient;

namespace Infrastructure.Helpers
{
    internal class HelpersQuery : IHelpersQuery
    {
        public IEnumerable<T> ExecuteQuery<T, P>(string query, P parameters, string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var result = connection.Query<T>(query, parameters);
            connection.Close();
            return result;
        }

        public IEnumerable<T> ExecuteQuery<T>(string query, string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var result = connection.Query<T>(query);
            connection.Close();
            return result;
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T, P>(string query, P parameters, string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            var result = await connection.QueryAsync<T>(query, parameters);
            await connection.CloseAsync();
            return result;
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            var result = await connection.QueryAsync<T>(query);
            await connection.CloseAsync();
            return result;
        }
    }
}

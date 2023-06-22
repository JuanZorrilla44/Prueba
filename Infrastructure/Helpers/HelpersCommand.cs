using Dapper;
using System.Data.SqlClient;

namespace Infrastructure.Helpers
{
    public class HelpersCommand : IHelpersCommand
    {
        public int ExecuteCommad<P>(string command, P parameters, string connectionString, string typeCommand)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            if (typeCommand == "insert")
            {
                int insertedId = connection.ExecuteScalar<int>(command, parameters);
                return insertedId;
            }

            if (typeCommand == "update" || typeCommand == "delete")
            {
                int rowsAffected = connection.Execute(command, parameters);
                return rowsAffected;
            }
            connection.Close();
            return 0;
        }

        public async Task<int> ExecuteCommadAsync<P>(string command, P parameters, string connectionString, string typeCommand)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            if (typeCommand == "insert")
            {
                int insertedId = await connection.ExecuteScalarAsync<int>(command, parameters);
                return insertedId;
            }

            if (typeCommand == "update" || typeCommand == "delete")
            {
                int rowsAffected = await connection.ExecuteAsync(command, parameters);
                return rowsAffected;
            }
            await connection.CloseAsync();
            return 0;
        }
    }
}

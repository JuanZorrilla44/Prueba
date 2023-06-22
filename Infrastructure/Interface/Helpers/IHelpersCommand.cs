namespace Infrastructure.Interface.Helpers
{
    public interface IHelpersCommand
    {
        int ExecuteCommad<P>(string command, P parameters, string connectionString, string typeCommand);
        Task<int> ExecuteCommadAsync<P>(string command, P parameters, string connectionString, string typeCommand);
    }
}

using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace CipherData.ApiMode
{
    public class SqlDataAcess : ISqlDataAcess
    {
        private readonly IConfiguration _config;

        public string ConnectionStringName { get; set; } = "Default";

        public SqlDataAcess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string connection_string = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connection_string))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            string connection_string = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connection_string))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}

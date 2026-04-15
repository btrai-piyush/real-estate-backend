using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace RealEstate.DataAccess.Dapper.Implementations
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly string _connectionString;

        public SqlDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default")!;
        }

        public async Task<List<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                List<T> rows = (await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure)).ToList();
                return rows;
            }
        }

        public async Task<bool> SaveDataAsync<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var success = await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                if (success > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<T> ExecuteScalarAsync<T, U>(string storedProcedure, U parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<T>> LoadDataWithQuery<T, U>(string sqlStatement, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                List<T> rows = (await connection.QueryAsync<T>(sqlStatement, parameters)).ToList();
                return rows;
            }
        }
    }
}


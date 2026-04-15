using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Dapper
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters);
        Task<bool> SaveDataAsync<T>(string storedProcedure, T parameters);
        Task<T> ExecuteScalarAsync<T, U>(string storedProcedure, U parameters);
        Task<List<T>> LoadDataWithQuery<T, U>(string sqlStatement, U parameters);
    }
}

using RealEstate.DataAccess.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Services.Implementations
{
    public class CommonService : ICommonService
    {
        private readonly ISqlDataAccess _db;

        public CommonService(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> CheckDuplicate(string tableName, string columnName, string value)
        {
            var result = await _db.ExecuteScalarAsync<int, dynamic>("spCommon_CheckDuplicate", new { TableName = tableName, ColumnName = columnName, Value = value });
            return result > 0;
        }
    }
}

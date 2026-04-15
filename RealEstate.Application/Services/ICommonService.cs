using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Services
{
    public interface ICommonService
    {
        Task<bool> CheckDuplicate(string tableName, string columnName, string value);
    }
}

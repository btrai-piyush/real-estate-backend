using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto
{
    public class CheckNameAvailabilityDto
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
    }
}

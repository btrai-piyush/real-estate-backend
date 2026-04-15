using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Property
{
    public class AdminPropertyQueryDto
    {
        public string? Keyword { get; set; } = null;
        public string? Filter { get; set; } = null;
        public int? PageSize { get; set; } = 10;
        public int? PageNumber { get; set; } = 1;
    }
}

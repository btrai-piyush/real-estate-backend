using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Property
{
    public class PropertyQueryDto
    {
        public string? Keyword { get; set; } = null;
        public string? Location { get; set; } = null;
        public string? SaleOption { get; set; } = null;
        public string? PropertyType { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public int? MinArea { get; set; } = null;
        public int? MaxArea { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}

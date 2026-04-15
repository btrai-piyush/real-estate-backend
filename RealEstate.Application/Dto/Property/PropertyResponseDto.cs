using RealEstate.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Property
{
    public class PropertyResponseDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string SaleOption { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Area { get; set; }
        public bool ListingStatus { get; set; }
        public bool IsFeatured { get; set; }
        public string Type { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string CoverPhoto { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
        public string AddedBy { get; set; }
        public int TotalCount { get; set; }
    }
}

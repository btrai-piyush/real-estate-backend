using Microsoft.AspNetCore.Http;

namespace RealEstate.Application.Dto.Property
{
    public class PropertyDto
    {
        public int ID {  get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Area { get; set; }
        public string City { get; set; }
        public string SaleOption { get; set; }
        public bool IsFeatured { get; set; }
        public string Type { get; set; }
        public string AddedBy { get; set; }
        public bool ListingStatus { get; set; }
        public string State { get; set; }
        public IFormFile? CoverPhoto { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string UpdatedBy { get; set; }
    }
}

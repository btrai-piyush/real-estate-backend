using Microsoft.AspNetCore.Http;
using RealEstate.Application.Dto.Property;

namespace RealEstate.Application.Services
{
    public interface IPropertyService
    {
        Task<PropertyResponseDto> GetPropertyByIdAsync(int id);
        Task<IEnumerable<PropertyResponseDto>> AdminGetAllPropertiesAsync(AdminPropertyQueryDto query);
        Task<IEnumerable<PropertyResponseDto>> GetLatestFeaturedPropertiesAsync();
        Task<IEnumerable<PropertyResponseDto>> GetLatestPropertiesBySaleOptionAsync(string saleOption);
        Task<IEnumerable<PropertyResponseDto>> GetFilteredPropertiesAsync(PropertyQueryDto query);
        Task<string> UploadPropertyImageAsync(IFormFile file, int propertyId, string propertyName,string propertyCity);  
        Task<(Stream stream, string contentType)> GetPropertyImageAsync(int propertyId);
        Task<int> AddPropertyAsync(PropertyDto PropertyDto);
        Task<int> DeletePropertyAsync(List<int> propertyIds);
        Task<int> ToggleFeaturedAsync(List<int> propertyIds);
        Task<int> ToggleListingStatusAsync(List<int> propertyIds);
        Task<int> UpdatePropertyAsync(PropertyDto PropertyDto);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Dto.Property;
using RealEstate.Application.Services;
using RealEstate.Application.Services.Implementations;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            try
            {
                var property = await _propertyService.GetPropertyByIdAsync(id);
                return Ok(property);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty([FromForm] PropertyDto property)
        {
            var result = await _propertyService.AddPropertyAsync(property);
            if (result == 0)
            {
                return BadRequest(new { message = "Property could not be added" });
            }

            return Ok(new { PropertyID = result });
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("admin-get-all")]
        public async Task<IActionResult> AdminGetAllProperties([FromBody] AdminPropertyQueryDto query)
        {
            try
            {
                var properties = await _propertyService.AdminGetAllPropertiesAsync(query);
                return Ok(properties);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("latest-by-sale-option")]
        public async Task<IActionResult> GetLatestPropertiesBySaleOption([FromQuery] string saleOption)
        {
            try
            {
                var properties = await _propertyService.GetLatestPropertiesBySaleOptionAsync(saleOption);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetLatestFeaturedProperties()
        {
            var properties = await _propertyService.GetLatestFeaturedPropertiesAsync();
            if (properties == null || !properties.Any())
            {
                return NotFound("No featured properties found.");
            }
            return Ok(properties);
        }

        [HttpPost("getfiltered")]
        public async Task<IActionResult> GetFilteredProperties([FromBody] PropertyQueryDto query)
        {
            try
            {
                var properties = await _propertyService.GetFilteredPropertiesAsync(query);
                return Ok(properties);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadPropertyImage([FromForm] UploadPropertyImageDto request)
        {
            if (request == null || request.File == null)
                return BadRequest("File is null");

            try
            {
                var result = await _propertyService.UploadPropertyImageAsync(request.File, request.PropertyId, request.PropertyName, request.PropertyCity);

                return Ok(new
                {
                    fileName = result,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("image")]
        public async Task<IActionResult> GetPropertyImage([FromQuery] int propertyId)
        {
            try
            {
                var result = await _propertyService.GetPropertyImageAsync(propertyId);
                return File(result.stream, result.contentType);
            }
            catch (FileNotFoundException)
            {
                return NotFound("File not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProperty([FromBody] List<int> propertyIds)
        {
            if (propertyIds == null || !propertyIds.Any())
            {
                return BadRequest("No property IDs provided.");
            }

            var result = await _propertyService.DeletePropertyAsync(propertyIds);
            if (result == 0)
            {
                return NotFound("No properties found to delete.");
            }

            return Ok(new { DeletedCount = result });
        }

        [HttpPost("toggle-featured")]
        public async Task<IActionResult> ToggleFeatured([FromBody] List<int> propertyIds)
        {
            if (propertyIds == null || !propertyIds.Any())
            {
                return BadRequest("No property IDs provided.");
            }
            var result = await _propertyService.ToggleFeaturedAsync(propertyIds);
            if (result == 0)
            {
                return NotFound("No properties found to toggle featured status.");
            }
            return Ok(new { UpdatedCount = result });
        }

        [HttpPost("toggle-listing-status")]
        public async Task<IActionResult> ToggleListingStatus([FromBody] List<int> propertyIds)
        {
            if (propertyIds == null || !propertyIds.Any())
            {
                return BadRequest("No property IDs provided.");
            }
            var result = await _propertyService.ToggleListingStatusAsync(propertyIds);
            if (result == 0)
            {
                return NotFound("No properties found to toggle listing status.");
            }
            return Ok(new { UpdatedCount = result });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProperty([FromForm] PropertyDto property)
        {
            if (property == null)
            {
                return BadRequest("Property ID is required for update.");
            }
            var result = await _propertyService.UpdatePropertyAsync(property);
            if (result == 0)
            {
                return NotFound("Property not found or no changes made.");
            }
            return Ok(new { UpdatedCount = result });
        }
    }
}
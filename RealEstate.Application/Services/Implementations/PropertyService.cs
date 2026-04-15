using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using RealEstate.Application.Dto.Property;
using RealEstate.Application.Helpers;
using RealEstate.Core.Entities;
using RealEstate.DataAccess.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Services.Implementations
{
    public class PropertyService : IPropertyService
    {
        private readonly ISqlDataAccess _db;
        private readonly IConfiguration _config;
        private readonly string _uploadPath;

        public PropertyService(ISqlDataAccess db, IConfiguration config)
        {
            _db = db;
            _uploadPath = config["FileStorage:PropertyImagePath"];
        }

        public async Task<PropertyResponseDto> GetPropertyByIdAsync(int id)
        {
            var result = await _db.LoadDataAsync<PropertyResponseDto, dynamic>("dbo.spProperty_GetById", new { Id = id });
            if (result == null || !result.Any())
            {
                throw new KeyNotFoundException($"Property with ID {id} not found.");
            }
            return result.First();
        }

        public async Task<IEnumerable<PropertyResponseDto>> AdminGetAllPropertiesAsync(AdminPropertyQueryDto query)
        {
            var result = await _db.LoadDataAsync<PropertyResponseDto, dynamic>("dbo.spProperty_AdminGetAll", new
            {
                query.Keyword,
                query.Filter,
                query.PageNumber,
                query.PageSize
            });
            if (result == null || !result.Any())
            {
                throw new Exception("No properties found.");
            }

            return result;
        }


        public async Task<IEnumerable<PropertyResponseDto>> GetFilteredPropertiesAsync(PropertyQueryDto query)
        {

            var result = await _db.LoadDataAsync<PropertyResponseDto, dynamic>("dbo.spProperty_GetFiltered", new
            {
                query.Keyword,
                query.Location,
                query.SaleOption,
                query.PropertyType,
                query.MinPrice,
                query.MaxPrice,
                query.MinArea,
                query.MaxArea,
                query.SortBy,
                query.IsDescending,
                query.PageNumber,
                query.PageSize
            });

            if (result == null || !result.Any())
            {
                throw new Exception("No properties found matching the criteria.");
            }

            return result;
        }

        public async Task<string> UploadPropertyImageAsync(IFormFile file, int propertyId, string propertyName, string propertyCity)
        {
            var fileName = await FileHelper.SaveImage(file, propertyId, propertyName, propertyCity, _uploadPath);

            var result = await _db.SaveDataAsync("dbo.spPropertyImages_Insert", new
            {
                PropertyId = propertyId,
                fileName = fileName,
            });

            if (result == false)
            {
                // If database insert fails, delete the uploaded file to avoid orphaned files
                var fullPath = Path.Combine(_uploadPath, fileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                throw new Exception("Failed to save image information to the database.");
            }

            return (fileName);
        }

        public async Task<(Stream stream, string contentType)> GetPropertyImageAsync(int propertyId)
        {
            var result = await _db.LoadDataAsync<PropertyImages, dynamic>("dbo.spPropertyImages_GetImage", new { PropertyId = propertyId });
            if (result == null || !result.Any())
            {
                throw new FileNotFoundException();
            }

            var fileName = result.First().FileName;
            fileName = Path.GetFileName(fileName);

            var fullPath = Path.Combine(_uploadPath, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException();

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fullPath, out string contentType))
            {
                contentType = "application/octet-stream";
            }

            var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

            return await Task.FromResult<(Stream, string)>((stream, contentType));
        }

        private async Task<string> GetPropertyImageFileNameAsync(int propertyId)
        {
            var result = await _db.LoadDataAsync<PropertyImages, dynamic>("dbo.spPropertyImages_GetImage", new { PropertyId = propertyId });
            if (result == null || !result.Any())
            {
                return "no-image-available";
            }
            return result.First().FileName;
        }

        public async Task<int> AddPropertyAsync(PropertyDto PropertyDto)
        {
            var imageFileName = await FileHelper.SaveImage(PropertyDto.CoverPhoto, PropertyDto.ID, PropertyDto.Title, PropertyDto.City, _uploadPath);
            var result = await _db.ExecuteScalarAsync<int, dynamic>("dbo.spProperty_Insert", new
            {
                Title = PropertyDto.Title,
                Price = PropertyDto.Price,
                Area = PropertyDto.Area,
                SaleOption = PropertyDto.SaleOption,
                IsFeatured = PropertyDto.IsFeatured,
                Type = PropertyDto.Type,
                AddedBy = PropertyDto.AddedBy,
                ListingStatus = PropertyDto.ListingStatus,
                City = PropertyDto.City,
                State = PropertyDto.State,
                Address = PropertyDto.Address,
                Latitude = PropertyDto.Latitude,
                Longitude = PropertyDto.Longitude,
                CoverPhoto = imageFileName
            });

            if (result < 1)
            {
                // If database insert fails, delete the uploaded file to avoid orphaned files
                var fullPath = Path.Combine(_uploadPath, imageFileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                throw new Exception("Failed to add property to the database.");
            }

            return result;
        }

        public async Task<int> DeletePropertyAsync(List<int> propertyIds)
        {
            var idTable = SqlHelper.ToIdTable(propertyIds);
            var result = await _db.ExecuteScalarAsync<int, dynamic>("dbo.spProperty_DeleteByIds", new { Ids = idTable });
            return result;
        }

        public Task<int> ToggleFeaturedAsync(List<int> propertyIds)
        {
            var idTable = SqlHelper.ToIdTable(propertyIds);
            var result = _db.ExecuteScalarAsync<int, dynamic>("dbo.spProperty_ToggleFeaturedByIds", new { Ids = idTable });
            return result;
        }

        public Task<int> ToggleListingStatusAsync(List<int> propertyIds)
        {
            var idTable = SqlHelper.ToIdTable(propertyIds);
            var result = _db.ExecuteScalarAsync<int, dynamic>("dbo.spProperty_ToggleListingStatusByIds", new { Ids = idTable });
            return result;
        }


        public Task<int> UpdatePropertyAsync(PropertyDto PropertyDto)
        {
            var newImageFileName = string.Empty;
            var oldImageFileName = string.Empty;
            if (PropertyDto.CoverPhoto != null)
            {
                newImageFileName = FileHelper.SaveImage(PropertyDto.CoverPhoto, PropertyDto.ID, PropertyDto.Title, PropertyDto.City, _uploadPath).Result;
            }
            else
            {
                oldImageFileName = _db.LoadDataAsync<string, dynamic>("dbo.spProperty_GetCoverPhoto", new { Id = PropertyDto.ID }).Result.First();
            }

            var result = _db.ExecuteScalarAsync<int, dynamic>("dbo.spProperty_UpdateById", new
            {
                Id = PropertyDto.ID,
                Title = PropertyDto.Title,
                Price = PropertyDto.Price,
                Area = PropertyDto.Area,
                City = PropertyDto.City,
                SaleOption = PropertyDto.SaleOption,
                IsFeatured = PropertyDto.IsFeatured,
                Type = PropertyDto.Type,
                UpdatedBy = PropertyDto.UpdatedBy,
                ListingStatus = PropertyDto.ListingStatus,
                State = PropertyDto.State,
                Address = PropertyDto.Address,
                Latitude = PropertyDto.Latitude,
                Longitude = PropertyDto.Longitude,
                CoverPhoto = string.IsNullOrEmpty(newImageFileName) ? oldImageFileName : newImageFileName,
            });

            if (result.Result < 1)
            {
                var newImagePath = Path.Combine(_uploadPath, newImageFileName);
                if (File.Exists(newImagePath))
                {
                    File.Delete(newImagePath);
                }
            }

            return result;
        }

        public async Task<IEnumerable<PropertyResponseDto>> GetLatestPropertiesBySaleOptionAsync(string saleOption)
        {
            var result = await _db.LoadDataAsync<PropertyResponseDto, dynamic>("dbo.spProperty_GetLatestBySaleOption", new { @SaleOption = saleOption });
            if (result == null || !result.Any())
            {
                throw new Exception("No properties found for the specified sale option.");
            }

            return result;
        }

        public async Task<IEnumerable<PropertyResponseDto>> GetLatestFeaturedPropertiesAsync()
        {
            var result = await _db.LoadDataAsync<PropertyResponseDto, dynamic>("dbo.spProperty_GetLatestFeatured", new { });
            if (result == null || !result.Any())
            {
                throw new Exception("No featured properties found.");
            }
            return result;
        }
    }
}


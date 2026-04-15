using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Dto;
using RealEstate.Application.Services;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpPost("name-exists")]
        public async Task<IActionResult> CheckNameAvailability([FromBody] CheckNameAvailabilityDto request)
        {
            bool isDuplicate = await _commonService.CheckDuplicate(request.TableName, request.ColumnName, request.Value);

            if (isDuplicate)
            {
                return Ok(new { IsAvailable = false, Message = $"{request.Value} already exists." });   
            }
            else
            {
                return Ok(new { IsAvailable = true, Message = $"{request.Value} is available." });
            }
        }
    }
}

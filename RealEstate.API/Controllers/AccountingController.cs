using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Dto.Accounting;
using RealEstate.Application.Services;
using RealEstate.Core.Entities;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AccountingController : ControllerBase
    {
        private readonly IAccountingService _accountingService;

        public AccountingController(IAccountingService accountingService)
        {
            _accountingService = accountingService;
        }


        [HttpGet("Branches")]
        public async Task<IActionResult> GetBranches()
        {
            try
            {
                var branches = await _accountingService.GetBranchesAsync();
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving branches.", Details = ex.Message });
            }
        }

        [HttpPost("Branch")]
        public async Task<IActionResult> InsertBranch([FromBody] CreateBranchDto request)
        {
            try
            {
                await _accountingService.InsertBranch(request);
                return Ok(new { Message = "Branch inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while inserting the branch.", Details = ex.Message });
            }
        }

        [HttpGet("MasterGroups")]
        public async Task<IActionResult> GetMasterGroups()
        {
            try
            {
                var masterGroups = await _accountingService.GetAllMasterGroup();
                return Ok(masterGroups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving master groups.", Details = ex.Message });
            }
        }

        [HttpPost("MasterGroup")]
        public async Task<IActionResult> InsertMasterGroup([FromBody] CreateMasterGroupDto request)
        {
            try
            {
                await _accountingService.InsertMasterGroup(request);
                return Ok(new { Message = "Master group inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while inserting the master group.", Details = ex.Message });
            }
        }

        [HttpGet("GLGroups")]
        public async Task<IActionResult> GetGLGroups()
        {
            try
            {
                var glGroups = await _accountingService.GetAllGLGroup();
                return Ok(glGroups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving GL groups.", Details = ex.Message });
            }
        }

        [HttpPost("GLGroup")]
        public async Task<IActionResult> InsertGLGroup([FromBody] CreateGLGroupDto request)
        {
            try
            {
                await _accountingService.InsertGLGroup(request);
                return Ok(new { Message = "GL group inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while inserting the GL group.", Details = ex.Message });
            }
        }

        [HttpGet("GLHeads")]
        public async Task<IActionResult> GetGLHeads()
        {
            try
            {
                var glHeads = await _accountingService.GetAllGLHead();
                return Ok(glHeads);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving GL heads.", Details = ex.Message });
            }
        }

        [HttpPost("GLHead")]
        public async Task<IActionResult> InsertGLHead([FromBody] CreateGLHeadDto request)
        {
            try
            {
                await _accountingService.InsertGLHead(request);
                return Ok(new { Message = "GL head inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while inserting the GL head.", Details = ex.Message });
            }
        }

        [HttpPost("VoucherQuery")]
        public async Task<IActionResult> GetJournalVouchers([FromBody] VoucherQueryDto request)
        {
            try
            {
                var journalVouchers = await _accountingService.GetAllVouchers(request);
                return Ok(journalVouchers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving journal vouchers.", Details = ex.Message });
            }
        }

        [HttpPost("Journal")]
        public async Task<IActionResult> InsertJournal([FromBody] CreateJournalDto request)
        {
            try
            {
                await _accountingService.InsertJournal(request);
                return Ok(new { Message = "Journal inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while inserting the journal.", Details = ex.Message });
            }
        }

        [HttpPatch("VerifyJournal")]
        public async Task<IActionResult> VerifyJournal([FromBody] VerifyJournalDto request)
        {
            try
            {
                await _accountingService.VerifyJournal(request);
                return Ok(new { Message = "Journal verified successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while verifying the journal.", Details = ex.Message });
            }
        }
    }
}
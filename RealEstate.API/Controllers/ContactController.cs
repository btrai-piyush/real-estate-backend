using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Dto.Contact;
using RealEstate.Application.Services;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("user-message")]
        public async Task<IActionResult> InsertContactMessage([FromBody] MessageDto contactDto)
        {
            await _contactService.InsertContactMessage(contactDto);
            return Ok(new { Message = "Contact message inserted successfully." });
        }

        [HttpGet("office-contact")]
        public async Task<IActionResult> GetContactInfo()
        {
            try
            {
                var officeContact = await _contactService.GetOfficeContact();
                return Ok(officeContact);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetAllMessages(int pageNumber)
        {
            try
            {
                var messages = await _contactService.GetAllMessages(pageNumber);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPatch("toggle-read-status")]
        public async Task<IActionResult> ToggleReadStatus([FromBody] string messageIds)
        {
            await _contactService.ToggleReadStatus(messageIds);
            return Ok(new { Message = "Read status toggled successfully." });
        }

        [HttpPatch("mark-all-read")]
        public async Task<IActionResult> MarkAllRead([FromBody] string messageIds)
        {
            await _contactService.MarkAllRead(messageIds);
            return Ok(new { Message = "All messages marked as read successfully." });
        }

        [HttpDelete("messages")]
        public async Task<IActionResult> DeleteMessage([FromBody] string messageIds)
        {
            await _contactService.DeleteMessage(messageIds);
            return Ok(new { Message = "Message deleted successfully." });
        }
    }
}

using RealEstate.Application.Dto.Contact;
using RealEstate.Core.Entities;
using RealEstate.DataAccess.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly ISqlDataAccess _db;

        public ContactService(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<Messages>> GetAllMessages(int pageNumber)
        {
            var result = await _db.LoadDataAsync<Messages, dynamic>("spMessages_GetAll", new { PageNumber=pageNumber});
            if (result == null || result.Count == 0)
            {
                throw new Exception(message: "No messages found.");
            }
            return result;
        }

        public async Task<OfficeContact> GetOfficeContact()
        {
            string query = "SELECT TOP 1 * FROM dbo.OfficeContact";
            var result = (await _db.LoadDataWithQuery<OfficeContact, dynamic>(query, new { })).FirstOrDefault();
            if (result == null)
            {
                throw new Exception(message: "Office contact details not found.");
            }
            return result;
        }

        public async Task InsertContactMessage(MessageDto request)
        {
            await _db.SaveDataAsync("spMessages_Insert", new
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Company = request.Company,
                Subject = request.Subject,
                Message = request.Message
            });
        }

        public Task InsertOfficeContact(OfficeContactDto request)
        {
            throw new NotImplementedException();
        }

        public async Task ToggleReadStatus(string messageIds)
        {
            await _db.SaveDataAsync("spMessages_ToggleReadStatus", new { Ids = messageIds });
        }

        public async Task MarkAllRead(string messageIds)
        {
            await _db.SaveDataAsync("spMessages_MarkAllRead", new { Ids = messageIds });
        }

        public async Task DeleteMessage(string messageIds)
        {
            await _db.SaveDataAsync("spMessages_Delete", new { Ids = messageIds });
        }
    }
}
using RealEstate.Application.Dto.Contact;
using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Services
{
    public interface IContactService
    {
        Task<OfficeContact> GetOfficeContact();
        Task InsertContactMessage(ContactDto request);
        Task InsertOfficeContact(OfficeContactDto request);
        Task<List<ContactDto>> GetAllContactMessages();

    }
}

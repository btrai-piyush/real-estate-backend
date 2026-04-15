using RealEstate.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Auth
{
    public class UserResponseDto
    {
        public Guid? ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public UserRole RoleID { get; set; }
    }
}

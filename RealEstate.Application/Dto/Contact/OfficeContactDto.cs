using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Contact
{
    public class OfficeContactDto
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}

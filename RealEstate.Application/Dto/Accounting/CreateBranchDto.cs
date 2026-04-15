using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Accounting
{
    public class CreateBranchDto
    {
        public DateTime RegisterDate { get; set; }
        public string? RegistrationNumber { get; set; } = null;
        public string? BranchCode { get; set; } = null;
        public string? BranchName { get; set; } = null;
        public string? BranchNameNep { get; set; } = null;
        public string? NickName { get; set; } = null;
        public string? Street { get; set; } = null;
        public string? District { get; set; } = null;
        public string? Zone { get; set; } = null;
        public string? Country { get; set; } = null;
        public string? Province { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        public string? FaxNumber { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? CellNumber { get; set; } = null;
        public string? PanNumber { get; set; } = null;
        public string? ZipCode { get; set; } = null;
        public string? Url { get; set; } = null;
    }
}

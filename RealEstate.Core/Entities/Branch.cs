using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities
{
    public class Branch
    {
        public int ID { get; set; }
        public DateTime RegisterDate { get; set; }
        public string? RegistrationNumber { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchNameNep { get; set; }
        public string NickName { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Zone { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public string PanNumber { get; set; }
        public string ZipCode { get; set; }
        public string Url { get; set; }
        public DateTime AuditTS { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities
{
    public class GLHead
    {
        public int ID { get; set; }
        public int BranchID { get; set; }
        public int GroupID { get; set; }
        public DateTime CreationDate { get; set; }
        public string GLName { get; set; }
        public string GLNameNepali { get; set; }
        public decimal OpeningBal { get; set; }
        public bool Killed { get; set; }
        public bool Hidden { get; set; }
        public bool Locked { get; set; }
        public bool IsSys { get; set; }
        public bool IsShare { get; set; }
        public decimal OpeningShareAmount { get; set; }
        public int OpeningShareQty { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public int Sex { get; set; }
        public bool IsBank { get; set; }
        public string ShareHolder { get; set; }
        public Guid AuditUserID { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}

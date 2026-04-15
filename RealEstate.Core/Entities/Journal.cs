using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities
{
    public class Journal
    {
        public int ID { get; set; }
        public Guid UserID { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ValueDate { get; set; }
        public int Verified { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime VerifiedDate { get; set; } = DateTime.Now;
        public DateTime OriginalDate { get; set; } = DateTime.Now;  
        public DateTime LastDate { get; set; } = DateTime.Now;
        public Guid LastEditedBy { get; set; }
        public string LastEditedByUser { get; set; }
        public int BranchID { get; set; }
    }
}

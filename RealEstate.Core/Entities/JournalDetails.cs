using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities
{
    public class JournalDetails
    {
        public int ID { get; set; }
        public int JournalID { get; set; }
        public int LedgerID { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Reference { get; set; }
        public int CustomerID { get; set; }
    }
}

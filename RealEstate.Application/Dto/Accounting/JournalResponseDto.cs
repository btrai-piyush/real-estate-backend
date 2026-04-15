using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Accounting
{
    public class JournalResponseDto
    {
        public int JournalID { get; set; }
        public string EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ValueDate { get; set; }
        public int Status { get; set; }
        public string VerifiedBy { get; set; }
        public string LedgerName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Reference { get; set; }
        public decimal DebitTotal { get; set; }
        public decimal CreditTotal { get; set; }
    }
}

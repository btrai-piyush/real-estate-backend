using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Accounting
{
    public class JournalDetailDto
    {
        public int LedgerID { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public string Reference { get; set; }
        public int? CustomerID { get; set; }
    }
}

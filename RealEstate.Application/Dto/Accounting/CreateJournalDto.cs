using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Accounting
{
    public class CreateJournalDto
    {
        public Guid UserID { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ValueDate { get; set; }
        public int BranchID { get; set; }

        public List<JournalDetailDto> JournalDetails { get; set; }
    }
}

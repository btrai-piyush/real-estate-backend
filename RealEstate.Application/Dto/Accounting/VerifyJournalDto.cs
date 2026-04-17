using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Accounting
{
    public class VerifyJournalDto
    {
        public int JournalID { get; set; }
        public int Verified { get; set; }
        public Guid VerifiedBy { get; set; }
    }
}

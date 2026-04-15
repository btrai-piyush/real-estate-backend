using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Accounting
{
    public class CreateGLGroupDto
    {
        public int MasterGroupID { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string GroupNepali { get; set; }
        public bool Killed { get; set; }
        public bool Hidden { get; set; }
    }
}

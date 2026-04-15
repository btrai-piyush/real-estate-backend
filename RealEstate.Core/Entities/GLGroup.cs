using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities
{
    public class GLGroup
    {
        public int ID { get; set; }
        public int MasterGroupID { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string GroupNepali { get; set; }
        public bool Killed { get; set; }
        public bool Hidden { get; set; }    
    }
}

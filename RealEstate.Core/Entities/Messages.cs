using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities
{
    public class Messages
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool ReadStatus { get; set; }
        public DateTime AddedOn { get; set; }
        public int TotalCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities
{
    public class PropertyImages
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string FileName { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime AddedOn { get; set; }
    }
}

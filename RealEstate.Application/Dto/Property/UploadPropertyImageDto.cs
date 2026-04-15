using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dto.Property
{
    public class UploadPropertyImageDto
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyCity { get; set; }
        public IFormFile File { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kritzel.WebCast.Models
{
    public class UploadViewModel
    {
        public string id;
        public IFormFile image;
    }
}

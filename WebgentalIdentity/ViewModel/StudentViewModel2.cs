using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadReCreate.ViewModel
{
    public class StudentViewModel2
    {
        public string Name { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}

using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormBuilderDemo.DTO
{
    public class FormDataDTO
    {
        public int FormId { get; set; }
        public List<IFormFile> Files { get; set; }
        public string FilesName { get; set; }
        public string FormValues { get; set; }
    }
}

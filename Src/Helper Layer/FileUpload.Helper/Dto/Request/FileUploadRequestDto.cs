using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Helper.Dto.Request
{
    public class FileUploadRequestDto
    {
        public string RandomNumber { get; set; }     
        public string FilePath { get; set; }     
        public string FileName { get; set; }     
        [Required]
        public IFormFile Document { get; set; }
    }
}

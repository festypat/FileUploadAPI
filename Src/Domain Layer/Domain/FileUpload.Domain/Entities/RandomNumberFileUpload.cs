using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Domain.Entities
{
    public class RandomNumberFileUpload : BaseEntity
    {
        public long RandomNumberFileUploadId { get; set; }
        public string Reference { get; set; }
        public string RandomNumber { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime DateEntered { get; set; } = DateTime.Now;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Domain.Entities
{
    public class NumberStatistics : BaseEntity
    {
        public long NumberStatisticsId { get; set; }
        public string CategoryOne { get; set; }
        public string ResultOne { get; set; }
        public string CategoryTwo { get; set; }
        public string ResultTwo { get; set; }
        public DateTime DateEntered { get; set; } = DateTime.Now;
    }
}

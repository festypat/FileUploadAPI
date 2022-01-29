using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Helper.ViewModel
{
    public class RanViewModel
    {
        public string Reference { get; set; }
        public string Statistics { get; set; }
        public int StatisticsValue { get; set; }
        public string Numbers { get; set; }
        public int NumbersValue { get; set; }
    }

    public class RanDefaultViewModel
    {
        public string Reference { get; set; }
        public string Statistics { get; set; }
        public double StatisticsValue { get; set; }
        public string Numbers { get; set; }
        public string NumbersValue { get; set; }
    }

    public class RanColumnViewModel
    {
        public string Statistics { get; set; }
        //public string StatisticsValue { get; set; }
        public string Numbers { get; set; }
        //public string NumbersValue { get; set; }
    }
}

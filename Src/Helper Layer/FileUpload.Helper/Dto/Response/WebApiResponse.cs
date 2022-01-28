using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Helper.Dto.Response
{
    public class WebApiResponse : WebApiDefaultResponse
    {
        public Object Data { get; set; }
    }
    public class WebApiDefaultResponse
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}

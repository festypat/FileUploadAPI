using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Helper.Dto.Response
{
    public class AppResponseCodes
    {
        public const string Success = "00";
        public const string Failed = "01";
        public const string InternalError = "02";
        public const string RecordNotFound = "03";
    }
    public class ResponseCodes
    {
        public const int Success = 200;
        public const int Badrequest = 400;
        public const int RecordNotFound = 404;
        public const int Duplicate = 409;
        public const int InternalError = 500;
    }
}

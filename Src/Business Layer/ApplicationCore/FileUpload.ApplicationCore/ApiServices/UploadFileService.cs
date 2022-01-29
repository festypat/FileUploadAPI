using ClosedXML.Excel;
using FileUpload.ApplicationCore.Interfaces.Services;
using FileUpload.Helper.Dto.Request;
using FileUpload.Helper.Dto.Response;
using FileUpload.Helper.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.ApplicationCore.ApiServices
{
    public class UploadFileService
    {
        private readonly IRandomFileUploadNumber _randomFileUploadNumber;
        private readonly IHostingEnvironment _hostingEnvironment;
        public UploadFileService(IRandomFileUploadNumber randomFileUploadNumber,
            IHostingEnvironment hostingEnvironment)
        {
            _randomFileUploadNumber = randomFileUploadNumber ?? throw new ArgumentNullException(nameof(randomFileUploadNumber));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        public async Task<WebApiResponse> UploadFileAsync(IFormFile formFile)
        {
            try
            {
                var fileUploadRequestDto = new FileUploadRequestDto();
                fileUploadRequestDto.Document = formFile;
                fileUploadRequestDto.FileName = $"{Guid.NewGuid().ToString()}{".xlsx"}";

                if (fileUploadRequestDto.Document.FileName.Split('.').Last() != "xls" && fileUploadRequestDto.Document.FileName.Split('.').Last() != "xlsx")
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "File was successfully uploaded", StatusCode = ResponseCodes.Success };


                string sWebRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, "ExcelDocuments");
              
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, fileUploadRequestDto.FileName));


                //string rootFolder = sWebRootFolder;
                //string fileName = Guid.NewGuid().ToString() + fileUploadRequestDto.Document.FileName;

               // FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

                if (file.Exists)
                {
                    file.Delete();
                    file = new FileInfo(Path.Combine(sWebRootFolder, fileUploadRequestDto.FileName));
                }
             
              //  fileUploadRequestDto.Document.CopyTo(new FileStream(file.DirectoryName, FileMode.Create, FileAccess.Write));

               // var imagePath = $"{sWebRootFolder}{@"\"}{"test.xlsx"}";


             
                using (var stream = new MemoryStream())
                {
                    fileUploadRequestDto.Document.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        package.SaveAs(file);
                        //save excel file in your wwwroot folder and get this excel file from wwwroot
                    }
                }

                using (var package = new ExcelPackage(new FileInfo(file.FullName)))
                {
                    var firstSheet = package.Workbook.Worksheets["SheetJS"];

                    var sb = new StringBuilder();

                    // Iterate through all worksheets in an Excel workbook.
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                    // Create DataTable from an Excel worksheet.
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var r1 = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim();
                        var r2 = (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim();
                       
                    }                
                }


                var model = new RandomNumberFileUploadViewModel
                {
                    //RandomNumber = fileUploadRequestDto.RandomNumber,
                    //Reference = Guid.NewGuid().ToString(),
                };

                await _randomFileUploadNumber.AddAsync(model);

                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "File was successfully uploaded", StatusCode = ResponseCodes.Success };

            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured", StatusCode = ResponseCodes.InternalError };
            }
        }

     
    }
}


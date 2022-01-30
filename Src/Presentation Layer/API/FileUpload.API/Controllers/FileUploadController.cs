using ExcelDataReader;
using FileUpload.ApplicationCore.ApiServices;
using FileUpload.Helper.Dto.Request;
using FileUpload.Helper.Notification;
using FileUpload.Persistence.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.StaticFiles;

namespace FileUpload.API.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class FileUploadController : BaseController
    {
        private readonly IFileUploadRepository _fileUploadRepository;
        public FileUploadController(INotification notification,
            IFileUploadRepository fileUploadRepository) : base(notification)
        {
            _fileUploadRepository = fileUploadRepository ?? throw new ArgumentNullException(nameof(fileUploadRepository));

        }

        [HttpPost]
        [Route("excel-document")]
        public async Task<IActionResult> RegisterUser() => Response(await _fileUploadRepository.UploadFileAsync(Request.Form.Files[0]).ConfigureAwait(false));
        //public async Task<IActionResult> RegisterUser(IFormFile formFile) => Response(await _fileUploadRepository.UploadFileAsync(formFile).ConfigureAwait(false));

        [HttpGet]
        [Route("get-numbers")]
        public async Task<IActionResult> GerNumbers() => Response(await _fileUploadRepository.GetNumbers().ConfigureAwait(false));

        [HttpGet]
        [Route("get-all-files")]
        public async Task<IActionResult> GetAllFiles()
        {
            return Ok(await _fileUploadRepository.GetFiles());           
        }

        [HttpGet]
        [Route("download-file")]
        public async Task<IActionResult> Download(string fileName)
        {

            var getFile = await _fileUploadRepository.GetFile(fileName);

            if(getFile == default)
                return NotFound();

            //string url = $"{getFile.FilePath}{@"\"}{getFile.FileName}"; 
            string url = ""; 

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), url);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), filePath);
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}

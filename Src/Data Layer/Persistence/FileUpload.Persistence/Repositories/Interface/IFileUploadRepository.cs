using FileUpload.Helper.Dto.Response;
using FileUpload.Helper.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Persistence.Repositories.Interface
{
    public interface IFileUploadRepository
    {
        Task<WebApiResponse> UploadFileAsync(IFormFile formFile);
        Task<IEnumerable<RandomNumberFileUploadViewModel>> GetFiles();
        Task<RandomNumberFileUploadViewModel> GetFile(string fileName);
    }
}

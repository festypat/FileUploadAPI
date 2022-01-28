using FileUpload.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.ApplicationCore.Interfaces.Services
{
    public interface IRandomFileUploadNumber
    {
        Task<List<RandomNumberFileUploadViewModel>> GetAllAsync();
        Task<RandomNumberFileUploadViewModel> GetByFileNameAsync(string number);
        Task<int> CountTotalUploadAsync();
        Task<bool> ExistsAsync(long Id);
        Task<RandomNumberFileUploadViewModel> AddAsync(RandomNumberFileUploadViewModel model);
        Task UpdateAsync(RandomNumberFileUploadViewModel model);
        Task DeleteAsync(int id);
    }
}

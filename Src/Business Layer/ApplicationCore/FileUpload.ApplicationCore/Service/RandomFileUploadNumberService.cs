using AutoMapper;
using FileUpload.ApplicationCore.Interfaces;
using FileUpload.ApplicationCore.Interfaces.Services;
using FileUpload.Domain.Entities;
using FileUpload.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.ApplicationCore.Service
{
    public class RandomFileUploadNumberService : IRandomFileUploadNumber
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<RandomNumberFileUpload> _randomFileUpload;

        public RandomFileUploadNumberService(IAsyncRepository<RandomNumberFileUpload> randomFileUpload, IMapper mapper)
        {
            _randomFileUpload = randomFileUpload ?? throw new ArgumentNullException(nameof(randomFileUpload));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RandomNumberFileUploadViewModel> AddAsync(RandomNumberFileUploadViewModel model)
        {
            var entity = _mapper.Map<RandomNumberFileUploadViewModel, RandomNumberFileUpload>(model);

            await _randomFileUpload.AddAsync(entity);

            return model;
        }

        public Task<int> CountTotalUploadAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RandomNumberFileUploadViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RandomNumberFileUploadViewModel> GetByFileNameAsync(string number)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(RandomNumberFileUploadViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}

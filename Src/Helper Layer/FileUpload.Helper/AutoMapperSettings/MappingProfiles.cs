using AutoMapper;
using FileUpload.Domain.Entities;
using FileUpload.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Helper.AutoMapperSettings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RandomNumberFileUpload, RandomNumberFileUploadViewModel>();
            CreateMap<RandomNumberFileUploadViewModel, RandomNumberFileUpload>();
         
        }
    }

}

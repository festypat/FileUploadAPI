using FileUpload.ApplicationCore.ApiServices;
using FileUpload.ApplicationCore.Interfaces;
using FileUpload.ApplicationCore.Interfaces.Repositories;
using FileUpload.ApplicationCore.Interfaces.Services;
using FileUpload.ApplicationCore.Service;
using FileUpload.Helper.AutoMapperSettings;
using FileUpload.Helper.Notification;
using FileUpload.Persistence.Repositories;
using FileUpload.Persistence.Repositories.Interface;
using FileUpload.Persistence.Repositories.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Extensions.Dependencies
{
    public static class ApplicationSevicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(Repository<>));


            services.AddScoped<INotification, Notification>();
            services.AddScoped<IFileUploadRepository, FileUploadRepository>();
            services.AddScoped<UploadFileService>();
           
            services.AddScoped<IRandomFileUploadNumber, RandomFileUploadNumberService>();          


            return services;
        }
    }

}

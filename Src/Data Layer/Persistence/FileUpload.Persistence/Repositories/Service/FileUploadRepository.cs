using FileUpload.Domain.Entities;
using FileUpload.Helper.Dto.Request;
using FileUpload.Helper.Dto.Response;
using FileUpload.Helper.ViewModel;
using FileUpload.Persistence.Context;
using FileUpload.Persistence.Repositories.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Persistence.Repositories.Service
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly FIleUploadDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileUploadRepository(FIleUploadDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));

        }

        public async Task<WebApiResponse> UploadFileAsync(IFormFile formFile)
        {

            try
            {

                if (formFile.FileName.Split('.').Last() != "xls" && formFile.FileName.Split('.').Last() != "xlsx")
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "File was successfully uploaded", StatusCode = ResponseCodes.Success };

                var fileName = $"{Guid.NewGuid().ToString()}{".xlsx"}";

                string sWebRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, "ExcelDocuments");

                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, fileName));

                if (file.Exists)
                {
                    file.Delete();
                    file = new FileInfo(Path.Combine(sWebRootFolder, fileName));
                }

                using (var stream = new MemoryStream())
                {
                    formFile.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        /// This is can be better saved in a blob like azure blob storage with containers
                        package.SaveAs(file);
                        //save excel file in your wwwroot folder and get this excel file from wwwroot
                    }
                }
                var model = new List<RandomNumberFileUpload>();

                var reference = $"{"CRUD-"}{DateTime.Now.ToString("yyyy-MM-dd")}{"-"}{Guid.NewGuid().ToString().Substring(0, 15)}";

                using (var package = new ExcelPackage(new FileInfo(file.FullName)))
                {

                    // Iterate through all worksheets in an Excel workbook.
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                    // Create DataTable from an Excel worksheet.
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        model.Add(new RandomNumberFileUpload
                        {
                            RandomNumber = Convert.ToInt32((worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim()),
                            FileName = file.Name,
                            FilePath = file.DirectoryName,
                            Reference = reference
                        });
                    }
                }

                var inMemoryList = new List<int>();

                foreach (var item in model)
                {
                    inMemoryList.Add(item.RandomNumber);
                }

                var evenNumberList = inMemoryList.Where(i => i % 2 == 0).ToList();

                var oddNumberList = inMemoryList.Where(i => i % 2 != 0).ToList();

                bool allAreEven = evenNumberList.All(x => x % 2 == 0);

                bool allAreOdd = oddNumberList.All(x => x % 2 != 0);

                var evenNumberData = String.Join(",", evenNumberList);

                var oddNumberData = String.Join(",", oddNumberList);

                var mode = evenNumberList.GroupBy(n => n).
                OrderByDescending(g => g.Count()).
                Select(g => g.Key).FirstOrDefault();

                var modeNumberData = String.Join(",", mode);

                double mean = oddNumberList.Average();

                var statisticsModelList = new List<NumberStatistics>();

                statisticsModelList.Add(new NumberStatistics
                {
                    CategoryOne = "Even Number",
                    ResultOne = evenNumberData,
                    CategoryTwo = "Mode",
                    ResultTwo = modeNumberData
                });
                //var mean = even1.Average();

                statisticsModelList.Add(new NumberStatistics
                {
                    CategoryOne = "Odd Number",
                    ResultOne = oddNumberData,
                    CategoryTwo = "Mean",
                    ResultTwo = mean.ToString()
                });

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await _context.RandomNumberFileUpload.AddRangeAsync(model);
                        await _context.SaveChangesAsync();

                        await _context.NumberStatistics.AddRangeAsync(statisticsModelList);
                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();

                        return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "File was successfully uploaded", StatusCode = ResponseCodes.Success };
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured", StatusCode = ResponseCodes.InternalError };
                    }
                }
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured", StatusCode = ResponseCodes.InternalError };
            }
        }

        public async Task<IEnumerable<RandomNumberFileUploadViewModel>> GetFiles()
        {
            var numbers = await _context.NumberStatistics.ToListAsync();

            IEnumerable<RandomNumberFileUploadViewModel> response = new List<RandomNumberFileUploadViewModel>();

            response = numbers.Select(a => new RandomNumberFileUploadViewModel
            {
                CategoryOne = a.CategoryOne,
                ResultOne = a.ResultOne,
                CategoryTwo = a.CategoryTwo,
                ResultTwo = a.ResultTwo
            }).ToList();

            return response;
        }

        public async Task<RandomNumberFileUploadViewModel> GetFile(string fileName)
        {
            try
            {
                var file = await _context.RandomNumberFileUpload.FirstOrDefaultAsync(x => x.FileName == fileName);

                if (file == default)
                    return new RandomNumberFileUploadViewModel();

                return new RandomNumberFileUploadViewModel
                {
                    //FileName = file.FileName,
                    //FilePath = file.FilePath
                };

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<RandomNumberFileUploadViewModel>> GetNumbers()
        {
            try
            {
                var numbers = await _context.NumberStatistics.OrderBy(x=>x.DateEntered).ToListAsync();

                IEnumerable<RandomNumberFileUploadViewModel> response = new List<RandomNumberFileUploadViewModel>();

                response = numbers.Select(a => new RandomNumberFileUploadViewModel
                {
                    CategoryOne = a.CategoryOne,
                    ResultOne = a.ResultOne,
                    CategoryTwo = a.CategoryTwo,
                    ResultTwo = a.ResultTwo
                }).ToList();

                return response;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}

using FileUpload.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Persistence.Context
{
    public class FIleUploadDbContext : DbContext
    {
        public FIleUploadDbContext(DbContextOptions<FIleUploadDbContext> options) : base(options)
        { }

        public DbSet<RandomNumberFileUpload> RandomNumberFileUpload { get; set; }
        public DbSet<NumberStatistics> NumberStatistics { get; set; }
    }
}

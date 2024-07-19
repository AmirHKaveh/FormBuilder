using FormBuilderDemo.Enities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormBuilderDemo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Form> Form_tb { get; set; }
        public DbSet<FormField> FormField_tb { get; set; }
        public DbSet<FormData> FormData_tb { get; set; }
    }
}

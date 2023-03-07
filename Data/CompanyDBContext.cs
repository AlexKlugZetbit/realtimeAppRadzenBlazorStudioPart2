using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using RealtimeData.Models.CompanyDB;

namespace RealtimeData.Data
{
    public partial class CompanyDBContext : DbContext
    {
        public CompanyDBContext()
        {
        }

        public CompanyDBContext(DbContextOptions<CompanyDBContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().ToTable(tb => tb.HasTrigger("TriggerName"));
            base.OnModelCreating(builder);
            this.OnModelBuilding(builder);
        }

        public DbSet<RealtimeData.Models.CompanyDB.Employee> Employees { get; set; }
    }
}
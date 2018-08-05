using Microsoft.EntityFrameworkCore;
using reqMan.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace reqMan.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("RequestTypeSequence", schema: "shared")
                .StartsAt(100000)
                .IncrementsBy(1);

            modelBuilder.Entity<RequestType>()
                .Property(o => o.RequestTypeSeq)
                .HasDefaultValueSql("NEXT VALUE FOR shared.RequestTypeSequence");

            modelBuilder.HasSequence<int>("RequestActionSequence", schema: "shared")
                .StartsAt(100000)
                .IncrementsBy(1);

            modelBuilder.Entity<RequestAction>()
                .Property(o => o.RequestActionSeq)
                .HasDefaultValueSql("NEXT VALUE FOR shared.RequestActionSequence");

            modelBuilder.HasSequence<int>("RequestSequence", schema: "shared")
                .StartsAt(100000)
                .IncrementsBy(1);

            modelBuilder.Entity<Request>()
                .Property(o => o.DateRequested)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Request>()
                .Property(o => o.DateModified)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<RequestAction>()
                .Property(o => o.Date)
                .HasDefaultValueSql("GETDATE()");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestAction> RequestActions { get; set; }
        public DbSet<RequestType> RequestType { get; set; }

        public int GetNextRequestSequence()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            Database.ExecuteSqlCommand(
                       "SELECT @result = (NEXT VALUE FOR shared.RequestSequence)", result);

            return (int)result.Value;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestApi.Models;

namespace TestApi
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) :base(options){

        }

        public DbSet<Comment>? Comments {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasData(
                new Comment()
                {
                    Id = 1,
                    Title = "Test",
                    Description = "Test",
                    Author = "Javier Garduño",
                    CreatedAt = new DateTime()
                },
                new Comment()
                {
                    Id = 2,
                    Title = "Test",
                    Description = "Test",
                    Author = "Alexis Gomez",
                    CreatedAt = new DateTime()
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
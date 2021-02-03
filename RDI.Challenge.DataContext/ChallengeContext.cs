using Microsoft.EntityFrameworkCore;
using RDI.Challenge.Domain.Entities;
using System;

namespace RDI.Challenge.DataContext
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext(DbContextOptions<ChallengeContext> options)
          : base(options)
        { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MenuItem> MenuItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<MenuItem>().HasData(new[] {
           //});            
        }
    }
}

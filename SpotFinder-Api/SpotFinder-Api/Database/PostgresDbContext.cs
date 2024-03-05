using Microsoft.EntityFrameworkCore;
using SpotFinder_Api.Models;
using System;

namespace SpotFinder_Api.Database
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }

        public DbSet<Spot> spots {  get; set; } 
    }
}

using EduTracker.Models.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EduTracker.Models.Data;

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> T) : base(T)
    {
        
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentTableConfiguration());
    }


    public DbSet<Student> Students { get; set; }
}
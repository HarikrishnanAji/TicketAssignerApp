using System.Data.Entity.ModelConfiguration.Conventions;
using TicketAssignerAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace TicketAssignerAPI.Context
{
    public class TicketAssignerDbContext: DbContext
    {
        public TicketAssignerDbContext(DbContextOptions<TicketAssignerDbContext> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<UserModel>().ToTable("Users");
        }
    }
}


using Microsoft.EntityFrameworkCore;
using TicketAssignerAPI.Model;

namespace TicketAssignerAPI.Context
{
    public class TicketAssignerDbContext: DbContext
    {
        public TicketAssignerDbContext(DbContextOptions<TicketAssignerDbContext> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=devDB;Trusted_Connection=True;Integrated Security = True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<UserModel>().ToTable("Users",schema:"App").HasIndex(u=>u.Username).IsUnique();
        }
    }
}

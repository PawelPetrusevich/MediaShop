using System.Data.Entity;
using MediaShop.Common.Models.User;

namespace MediaShop.DataAccess.Context
{
    public class UserContext : DbContext
    {
        public UserContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountProfile> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasRequired(c => c.Profile);

            modelBuilder.Entity<Account>().HasKey(p => p.Id);

            modelBuilder.Entity<Account>().Property(p => p.Login).IsRequired();
            modelBuilder.Entity<Account>().Property(p => p.Password).IsRequired();
            modelBuilder.Entity<AccountProfile>().Property(p => p.FirstName).IsRequired();
            modelBuilder.Entity<AccountProfile>().Property(p => p.Email).IsRequired();
            modelBuilder.Entity<AccountProfile>().Property(p => p.FirstName).HasMaxLength(30);
            modelBuilder.Entity<AccountProfile>().Property(p => p.LastName).HasMaxLength(30);
        }
    }
}
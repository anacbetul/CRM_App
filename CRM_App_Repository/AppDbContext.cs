using CRM_App_Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRM_App_Repository
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        DbSet<User> Users { get; set; }
        DbSet<ClientInfo> Clients{ get; set; }
        public DbSet<Meeting> Meetings{ get; set; }
        DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }

    }
}

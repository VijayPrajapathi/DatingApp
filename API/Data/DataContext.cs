using API.Entities;
using DatingApp.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.API.Data
{
    public class DataContext(DbContextOptions options) : IdentityDbContext<AppUser,AppRole, int,
    IdentityUserClaim<int>,AppUserRole,IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>> (options)
    {
        public DbSet<UserLike> Likes { get; set; 
        }
         public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
            .HasMany(u => u.UserRoles)
            .WithOne(u => u.User).
            HasForeignKey(ur => ur.UserId)
            .IsRequired();

            modelBuilder.Entity<AppRole>()
            .HasMany(u => u.UserRoles)
            .WithOne(u => u.Role).
            HasForeignKey(ur => ur.RoleId)
            .IsRequired();

            modelBuilder.Entity<UserLike>()
            .HasKey(ul => new { ul.SourceUserId, ul.TargetUserId });

            modelBuilder.Entity<UserLike>()
            .HasOne(ul => ul.SourceUser)
            .WithMany(l => l.LikedUsers)
            .HasForeignKey(s => s.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLike>()
            .HasOne(ul => ul.TargetUser)
            .WithMany(l => l.LikedBytheUsers)
            .HasForeignKey(s => s.TargetUserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
            .HasOne(x => x.Recipient)
            .WithMany(x => x.MessagesReceived)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Message>()
            .HasOne(x => x.Sender)
            .WithMany(x => x.MessageSent)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

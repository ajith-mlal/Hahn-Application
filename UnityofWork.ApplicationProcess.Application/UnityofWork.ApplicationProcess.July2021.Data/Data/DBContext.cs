
using Microsoft.EntityFrameworkCore;
using UnityofWork.ApplicationProcess.July2021.Data.Entities;

namespace UnityofWork.ApplicationProcess.July2021.Data.Data
{

    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserAssets> UserAssets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .OwnsMany(r => r.UserAssets, p =>
                {
                    p.WithOwner().HasForeignKey("UserId");
                    p.Property<int>("UserId");
                    p.HasKey("UserAssetId");
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}


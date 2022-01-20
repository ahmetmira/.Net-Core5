using GRTrkrar.Entities.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GRTekrar.DataAccess
{
    public class GRTekrarDbContext : IdentityDbContext<User,UserRole,int>
    {

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server=MIRA;Database=GRTekrar;uid=ahmet;pwd=1234;");
        //}

        public GRTekrarDbContext(DbContextOptions<GRTekrarDbContext> dbContextOptions): base(dbContextOptions)
        {

        }
        //public override int SaveChanges()
        //{
        //    AddAuditInfo();
        //    return base.SaveChanges();
        //}
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();

            return base.SaveChangesAsync(cancellationToken);
        }
        private void AddAuditInfo()
        {
            var entities = ChangeTracker.Entries<BaseEntities>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);
          // var userId = _userCurrentHelper.UserId;
            var utcNow = DateTime.UtcNow;
            //var user = _userCurrentHelper.FirstName + " " + _userCurrentHelper.LastName ?? appUser;
            //var ipAddress = _userCurrentHelper?.RemoteIpAddress;*/

            var list = entities.ToList();

            foreach (var entity in list)
            {
                if (entity.State == EntityState.Added)
                {
                    
                    entity.Entity.CreatedDate = utcNow;
                }

                if (entity.State == EntityState.Modified)
                {
             
                    entity.Entity.UpdatedDate = utcNow;
                }

            }
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

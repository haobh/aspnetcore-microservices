using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using Contracts.Domains.Interfaces;
using Carbon.Data.Protection;

namespace Product.API.Persistence
{
    /// <summary>
    /// Install-Package Pomelo.EntityFrameworkCore.MySql -Version 6.0.2
    /// </summary>
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<CatalogProduct> Products { get; set; }

        /// <summary>
        /// Save thông tin khi migare entity vào DB
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified ||
                e.State == EntityState.Added ||
                e.State == EntityState.Deleted);

            foreach (var item in modified)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        // Nếu entity mà có triển khai IDateTracking
                        if(item.Entity is IDateTracking addedEntity)
                        {
                            addedEntity.CreatedDate = DateTime.UtcNow;
                            item.State = EntityState.Added;
                        }
                        break;
                    case EntityState.Modified:
                        Entry(item.Entity).Property("Id").IsModified = false;
                        if (item.Entity is IDateTracking modifiedEntity)
                        {
                            modifiedEntity.LastModifiedDate = DateTime.UtcNow;
                            item.State = EntityState.Modified;
                        }
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentSold.WebTest.Data.EFConfig
{
    /// <summary>
    /// Configures the Product entity database schema.
    /// </summary>
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasIndex(x => x.ProductName);
            entity.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");
            entity.Property(x => x.UpdatedAt)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}

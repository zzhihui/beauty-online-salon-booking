using System.Data.Entity.ModelConfiguration;
using App.Data.Entities.Authentication;

namespace App.Data.Models.Mapping
{
    public class ShopUserMap : EntityTypeConfiguration<ShopUser>
    {
        public ShopUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);


            this.Property(t => t.UserName)
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("shopUser");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
            this.Property(t => t.UserName).HasColumnName("userName");
            this.Property(t => t.Password).HasColumnName("password");
            this.Property(t => t.IsActive).HasColumnName("isactive");
            this.Property(t => t.UserGroupId).HasColumnName("userGroupId");
            this.Property(t => t.Pin).HasColumnName("pin");
            
        }
    }
}

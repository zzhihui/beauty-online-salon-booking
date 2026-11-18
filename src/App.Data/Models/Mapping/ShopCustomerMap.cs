using System.Data.Entity.ModelConfiguration;
using App.Data.Entities.Authentication;

namespace App.Data.Models.Mapping
{
    public class ShopCustomerMap : EntityTypeConfiguration<ShopCustomer>
    {
        public ShopCustomerMap()
        {
            // Primary Key
            HasKey(t => t.Id);
        
            // Table & Column Mappings
            ToTable("Customer");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.ShopId).HasColumnName("shopId");
            Property(t => t.FirstName).HasColumnName("firstName");
            Property(t => t.LastName).HasColumnName("lastName");
            Property(t => t.StoreCredit).HasColumnName("storeCredit");
            Property(t => t.Created).HasColumnName("created");
            Property(t => t.Updated).HasColumnName("updated");
            Property(t => t.ContactName).HasColumnName("fullName");
            Property(t => t.PhoneNumber).HasColumnName("phone1");
            Property(t => t.Email).HasColumnName("email");
            Property(t => t.Password).HasColumnName("password");
            Property(t => t.Address).HasColumnName("address1");
            Property(t => t.IsActive).HasColumnName("isactive");
            Ignore(t => t.ClientToken);
            Ignore(t => t.TokenExpire);
        }
    }
}

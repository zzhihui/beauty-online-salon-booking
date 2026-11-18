using System.Data.Entity.ModelConfiguration;
using App.Data.Entities.Settings;

namespace App.Data.Models.Mapping
{
    public class ServerSettingMap : EntityTypeConfiguration<ServerSetting>
    {
        public ServerSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            // Table & Column Mappings
            this.ToTable("ServerSetting");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.SettingKey).HasColumnName("settingKey");
            this.Property(t => t.SettingValue).HasColumnName("settingValue");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
            this.Property(t => t.IsServer).HasColumnName("isServer");
        }
    }
}

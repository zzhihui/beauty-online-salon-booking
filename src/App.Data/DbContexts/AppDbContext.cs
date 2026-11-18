using System.Data.Entity;
using App.Data.Entities.Authentication;
using App.Data.Entities.Health;
using App.Data.Entities.Settings;
using App.Data.Entities.Shops;
using App.Data.Models.Mapping;
using log4net;

namespace App.Data.DbContexts
{
    public partial class AppDbContext : DbContext
    {
        private static readonly ILog Logger = LogManager.GetLogger("DbContextSql");

        static AppDbContext()
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer<AppDbContext>(null);
        }

        public AppDbContext() : base("Name=AppDbContext")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;

            Database.Log = sql => Logger.DebugFormat(sql);
        }
        
        public DbSet<ShopCustomer> ShopCustomers { get; set; }
        
        public DbSet<ServerSetting> ServerSettings { get; set; }
       
        public DbSet<ShopInfo> ShopInfoes { get; set; }

        public DbSet<ShopNews> ShopNews { get; set; }
      
        public DbSet<ShopUser> ShopUsers { get; set; }
        
        public DbSet<SyncStatus> SyncStatuses { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {           
            modelBuilder.Configurations.Add(new ShopCustomerMap());
            modelBuilder.Configurations.Add(new ServerSettingMap());
            modelBuilder.Configurations.Add(new ShopInfoMap());
            modelBuilder.Configurations.Add(new ShopNewsMap());
            modelBuilder.Configurations.Add(new ShopUserMap());
            modelBuilder.Configurations.Add(new SyncStatusMap());
            modelBuilder.Configurations.Add(new PaymentTypeMap());
        }
    }
}
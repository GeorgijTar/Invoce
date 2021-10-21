using InvoceDb.Entitys;
using Microsoft.EntityFrameworkCore;
namespace InvoceDb

{

    //    public class EFConfiguration : DbConfiguration
    //    {
    //        public EFConfiguration()
    //        {
    //            SetProviderFactory("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
    //            SetProviderFactory("System.Data.SQLite.EF6", System.Data.SQLite.EF6.SQLiteProviderFactory.Instance);
    //            SetProviderServices("System.Data.SQLite", (System.Data.Entity.Core.Common.DbProviderServices)System.Data.SQLite.EF6.SQLiteProviderFactory.Instance.GetService(typeof(System.Data.Entity.Core.Common.DbProviderServices)));
    //        }
    //    }

    //    [DbConfigurationType(typeof(EFConfiguration))]


    class InvoceDbContext : DbContext
    {
        public InvoceDbContext() : base()
        {
            Database.EnsureCreated();
        }

        public DbSet<Status> Status { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Counterparty> Counterparty { get; set; }
        public DbSet<PayDetail> PayDetail { get; set; }
        public DbSet<Invoce> Invoce { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source=invoceData.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            modelBuilder.Entity<Status>().HasData(GetDefaultStatus());
            base.OnModelCreating(modelBuilder);
        }

        private Status[] GetDefaultStatus()
        {
            return new Status[] {
         new Status(){Id=1, Name="Черновик"},
         new Status(){Id=2, Name="Автосанкционирован"},
         new Status(){Id=3, Name="Ожидает включения в реестр"},
         new Status(){Id=4, Name="Включен в реестр"},
         new Status(){Id=5, Name="Ожидает согласования"},
         new Status(){Id=6, Name="Отправлен на согласование"},
         new Status(){Id=7, Name="Согласован учредителем"},
         new Status(){Id=8, Name="Согласован по истечению срока"},
         new Status(){Id=9, Name="Частично согласован учредителем"},
         new Status(){Id=10, Name="Санкционирован учредителем"},
         new Status(){Id=11, Name="Санкционирован по истичению срока"},
         new Status(){Id=12, Name="Ожидает оплаты"},
         new Status(){Id=13, Name="Отправлен в банк"},
         new Status(){Id=14, Name="Оплачен"},
         new Status(){Id=15, Name="Частично исполнен"},
         new Status(){Id=16, Name="Исполнен"},
        };
        }
    }
}
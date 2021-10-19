using InvoceDb.Entitys;
using Microsoft.EntityFrameworkCore;

namespace InvoceDb
{
    class InvoceDbContext : DbContext
    {
        public InvoceDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Status> Status { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Counterparty> Counterparty { get; set; }
        public DbSet<PayDetail> PayDetail { get; set; }
        public DbSet<Invoce> Invoce { get; set; }


    }
}

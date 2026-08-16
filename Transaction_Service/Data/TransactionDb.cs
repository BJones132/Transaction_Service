using Microsoft.EntityFrameworkCore;
using Transaction_Service.Data.Models;

namespace Transaction_Service.Data
{
    public class TransactionDb : DbContext
    {
        public TransactionDb(DbContextOptions<TransactionDb> options):
            base(options) { }

        public DbSet<Transaction> Transactions => Set<Transaction>();
    }
}

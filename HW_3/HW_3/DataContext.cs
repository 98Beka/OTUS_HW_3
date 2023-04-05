using HW_3.Entities;
using Microsoft.EntityFrameworkCore;

namespace HW_3 {
    public class DataContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }
    }
}

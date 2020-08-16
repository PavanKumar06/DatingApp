using DatingApp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Value> Values { get; set; }// the name that we specify here is used to represent the table name that gets created what we scaffold our database
        public DbSet<User> Users { get; set; }    
        public DbSet<Photo> Photos { get; set; }
    }
}
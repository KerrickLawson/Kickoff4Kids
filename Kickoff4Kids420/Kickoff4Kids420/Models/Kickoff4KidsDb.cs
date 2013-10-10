using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kickoff4Kids420.Models
{
    public class Kickoff4KidsDb : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityTransaction> ActivityTransactions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
 

    }
}
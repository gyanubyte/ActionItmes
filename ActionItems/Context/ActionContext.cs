using ActionItems.Models;
using Microsoft.EntityFrameworkCore;

namespace ActionItems.Context
{
    public class ActionContext:DbContext
    {
       public ActionContext(DbContextOptions<ActionContext> options) : base(options)
       {
        
       }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Issus> Tasks { get; set; }

    }

   
}

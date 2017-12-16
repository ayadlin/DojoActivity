
using Microsoft.EntityFrameworkCore;

namespace DojoActivity.Models
{
    public class DojoActivityContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public DojoActivityContext(DbContextOptions<DojoActivityContext> options) : base(options) { }
        public DbSet<User> Users { get; set; } //Users id the database table

        public DbSet<Activity> Activities {get; set;}
        //Activities is the database table and class name
         public DbSet<RSVP> RSVPs {get; set;}
    }
}




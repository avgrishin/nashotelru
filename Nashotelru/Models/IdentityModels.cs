using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Nashotelru.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
      public virtual NoUserInfo NoUserInfo { get; set; }
    }

    public class NoUserInfo
    {
      public int Id { get; set; }
      
      [MaxLength(100)]
      public string FirstName { get; set; }
      [MaxLength(100)]
      public string LastName { get; set; }

      [MaxLength(200)]
      public string EMail { get; set; }
      public bool IsLocked { get; set; }

      [MaxLength(50)]
      public string ConfirmationToken { get; set; }
      public bool IsConfirmed { get; set; }
      [MaxLength(50)]
      public string ReminderToken { get; set; }
      public DateTime? ReminderDT { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<NoUserInfo> NoUserInfo { get; set; }
    }
}
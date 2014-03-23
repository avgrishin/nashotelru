using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nashotelru.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //  public virtual NoUserInfo NoUserInfo { get; set; }
    //}
    public class ApplicationUser : IdentityUser
    {
      public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
      {
        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        // Add custom user claims here
        return userIdentity;
      }
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
        : base("DefaultConnection", throwIfV1Schema: false)
      {
      }

      static ApplicationDbContext()
      {
        // Set the database intializer which is run once during application start
        // This seeds the database with admin user credentials and admin role
        Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
      }

      public static ApplicationDbContext Create()
      {
        return new ApplicationDbContext();
      }
      public DbSet<NoUserInfo> NoUserInfo { get; set; }
    }
    
  //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  //  {
  //      public ApplicationDbContext()
  //          : base("DefaultConnection")
  //      {
  //      }
  //      public DbSet<NoUserInfo> NoUserInfo { get; set; }
  //  }
}
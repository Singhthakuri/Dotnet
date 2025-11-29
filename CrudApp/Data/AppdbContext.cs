

using System.Security.Cryptography.Pkcs;
using CrudApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApp.Data
{
    public class AppdbContext:DbContext
    {
      public  AppdbContext(DbContextOptions<AppdbContext> options) : base(options) { }

       public   DbSet<Patient> Patients { get; set; }
       public DbSet<Address> AddressTable { get; set; }
    }
}

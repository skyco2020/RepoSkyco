using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SkyCoApi.Infraestructure
{
    public class SkyCoApiDBContext: IdentityDbContext<ApplicationUser>
    {
        public SkyCoApiDBContext()
              : base("SkyCoDbContext", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Skyco_Account> Skyco_Account { get; set; }   
        public virtual DbSet<Provinces> Provinces { get; set; }
        public virtual DbSet<Skyco_User> Skyco_User { get; set; }
        public virtual DbSet<Skyco_AccountType> Skyco_AccountType { get; set; }
        public virtual DbSet<Skyco_Address> Skyco_Address { get; set; }
        public virtual DbSet<Skyco_Phone> Skyco_Phone { get; set; }

        public static SkyCoApiDBContext Create()
        {
            return new SkyCoApiDBContext();
        }
    }
}
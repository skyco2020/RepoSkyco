namespace DataModal.DataClasses
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SkyCoDbContext : DbContext
    {
        public SkyCoDbContext()
            : base("name=SkyCoDbContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<Cities> City { get; set; }
        public virtual DbSet<Countries> Country { get; set; }
        public virtual DbSet<Locations> Location { get; set; }
        public virtual DbSet<StripeSubscribes> StripeSubscribes { get; set; }
        public virtual DbSet<Plans> Plans { get; set; }
        public virtual DbSet<Provinces> Provinces { get; set; }
        public virtual DbSet<Skyco_Accounts> Skyco_Account { get; set; }
        public virtual DbSet<Skyco_AccountTypes> Skyco_AccountType { get; set; }
        public virtual DbSet<Skyco_Addresses> Skyco_Address { get; set; }
        public virtual DbSet<Skyco_Phones> Skyco_Phone { get; set; }
        public virtual DbSet<Skyco_Users> Skyco_User { get; set; }
        public virtual DbSet<Perfils> Perfils { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public static SkyCoDbContext Create()
        {
            return new SkyCoDbContext();
        }
    }
}

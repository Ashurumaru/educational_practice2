﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace edic_practice
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ed_practiceEntities : DbContext
    {
        public ed_practiceEntities()
            : base("name=ed_practiceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CarMaintenance> CarMaintenance { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<CarStatuses> CarStatuses { get; set; }
        public virtual DbSet<CarTypes> CarTypes { get; set; }
        public virtual DbSet<RentalPayments> RentalPayments { get; set; }
        public virtual DbSet<RentalPrices> RentalPrices { get; set; }
        public virtual DbSet<Rentals> Rentals { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserReviews> UserReviews { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}

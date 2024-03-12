//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Cars
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cars()
        {
            this.CarMaintenance = new HashSet<CarMaintenance>();
            this.RentalPrices = new HashSet<RentalPrices>();
            this.Rentals = new HashSet<Rentals>();
        }
    
        public int CarID { get; set; }
        public int TypeID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int CarYears { get; set; }
        public string LicensePlate { get; set; }
        public Nullable<int> StatusID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarMaintenance> CarMaintenance { get; set; }
        public virtual CarStatuses CarStatuses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RentalPrices> RentalPrices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rentals> Rentals { get; set; }
        public virtual CarTypes CarTypes { get; set; }
    }
}
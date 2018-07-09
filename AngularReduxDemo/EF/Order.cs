namespace AngularReduxDemo.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            DrinkItems = new HashSet<DrinkItem>();
            MealItems = new HashSet<MealItem>();
        }

        public int Id { get; set; }

        public int? TableId { get; set; }

        public int? ServerId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DrinkItem> DrinkItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MealItem> MealItems { get; set; }

        public virtual Server Server { get; set; }

        public virtual Table Table { get; set; }

        //public int Id { get; set; }

        //public int? TableId { get; set; }

        //public int? ServerId { get; set; }

        //public virtual Server Server { get; set; }

        //public virtual Table Table { get; set; }
    }
}

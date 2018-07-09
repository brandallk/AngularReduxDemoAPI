namespace AngularReduxDemo.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DrinkItem")]
    public partial class DrinkItem
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }

        //public virtual Table Table { get; set; }
    }
}

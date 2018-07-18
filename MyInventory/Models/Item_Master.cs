namespace MyInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item_Master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item_Master()
        {
            Stocks = new HashSet<Stock>();
        }

        [Key]
        public int Item_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Item_Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public int Unit { get; set; }

        public int? Cat_ID { get; set; }

        [StringLength(100)]
        public string Photo { get; set; }

        public virtual Item_Category Item_Category { get; set; }

        public virtual UOM UOM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}

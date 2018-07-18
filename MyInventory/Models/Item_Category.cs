namespace MyInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item_Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item_Category()
        {
            Item_Master = new HashSet<Item_Master>();
        }

        [Key]
        public int Category_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Cat_Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public bool Aprroval_Needed { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item_Master> Item_Master { get; set; }
    }
}

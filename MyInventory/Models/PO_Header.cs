namespace MyInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PO_Header
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PO_Header()
        {
            PO_Detail = new HashSet<PO_Detail>();
        }

        [Key]
        public int PO_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string PO_Number { get; set; }

        [Required]
        [StringLength(50)]
        public string Supplier { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PO_Detail> PO_Detail { get; set; }
    }
}

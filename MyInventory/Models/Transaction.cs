namespace MyInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        [Key]
        public int Trn_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Trn_Type { get; set; }

        public int Loc_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Service_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Resource_ID { get; set; }

        public int LineItem_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Product_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Qty { get; set; }

        [Required]
        [StringLength(10)]
        public string Approver { get; set; }

        [StringLength(10)]
        public string E_Approver { get; set; }

        public virtual Stock Stock { get; set; }
    }
}

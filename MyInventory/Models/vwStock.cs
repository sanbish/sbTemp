namespace MyInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vwStock")]
    public partial class vwStock
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(203)]
        public string ProductName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Item_Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(150)]
        public string Location { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LineItem_ID { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_ID { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal Qty { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Loc_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WIP_Qty { get; set; }

        public DateTime? Warranty_Expiry { get; set; }

        public DateTime? Item_Expiry { get; set; }
    }
}

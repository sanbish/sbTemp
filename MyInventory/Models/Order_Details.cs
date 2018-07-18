namespace MyInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Details
    {
        public int Id { get; set; }

        public int Order_Line { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedDate { get; set; }

        public decimal? Discount { get; set; }

        public int OrderId { get; set; }

        public int StockId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        [StringLength(256)]
        public string UpdatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedDate { get; set; }

        [StringLength(128)]
        public string ApprovedBy { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Order_Master Order_Master { get; set; }

        public virtual Stock Stock { get; set; }
    }
}

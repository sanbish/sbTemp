namespace MyInventory.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class PO_Detail
    {
        [Key]
        public int POD_ID { get; set; }

        public int PO_ID { get; set; }

        public byte PO_Line { get; set; }

        public int Item_ID { get; set; }

        public int Qty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Unit_Price { get; set; }

        public virtual PO_Header PO_Header { get; set; }
    }
}

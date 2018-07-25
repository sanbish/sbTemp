using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyInventory.Models
{
    public class PO_Receive
    {
        [Key]
        public int POR_ID { get; set; }

        public int POD_ID { get; set; }

        public byte PO_Line { get; set; }

        public int Stock_ID { get; set; }

        public int QtyReceived { get; set; }

        public bool isRejected { get; set; }

        public string RejectReason { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Unit_Price { get; set; }

        public virtual PO_Detail PO_Detail { get; set; }
    }
}
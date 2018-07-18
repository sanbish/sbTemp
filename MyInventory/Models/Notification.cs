namespace MyInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification
    {
        [Key]
        public int Notification_ID { get; set; }

        [Required]
        [StringLength(128)]
        public string Notification_To { get; set; }

        [Column("Notification")]
        [Required]
        [StringLength(500)]
        public string Notification1 { get; set; }

        public bool Is_Read { get; set; }

        [StringLength(10)]
        public string Notification_Type { get; set; }
    }
}

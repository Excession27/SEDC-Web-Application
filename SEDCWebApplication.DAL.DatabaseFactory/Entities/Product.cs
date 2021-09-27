using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SEDCWebApplication.DAL.DatabaseFactory.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public bool IsDiscounted { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string Size { get; set; }

        public string ImagePath { get; set; }

        public string Description { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}

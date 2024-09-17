using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Service.Domain.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }


        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Service.Application.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CELLTECH_COM.Models
{
    public class SaleItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => Product.Price * Quantity;
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Models
{
    public class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int orderId { get; set; }
        public int productId { get; set; }
        public int productCategory { get; set; }
        public string productName { get; set; }
        public string productUnit { get; set; }
        public double productCost { get; set; }
        public double productPrice { get; set; }
        public int productCount { get; set; }
    }
}
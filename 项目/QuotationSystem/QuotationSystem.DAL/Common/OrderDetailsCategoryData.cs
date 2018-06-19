using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Common
{
    public class OrderDetailsCategoryData
    {
        public int categoryId { get; set; }
        public string categoryText { get; set; }
        public double totalPrice { get; set; }
        public double totalCost { get; set; }
    }
}
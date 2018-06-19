using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Common
{
    public class ProductFullInfo
    {
        public Product product { get; set; }
        public double[] prices { get; set; }
    }
}
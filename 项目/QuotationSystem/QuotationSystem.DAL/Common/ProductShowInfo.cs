using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Common
{
    public class ProductShowInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string describe { get; set; }
        public int category { get; set; }
        public string unit { get; set; }
        public double cost { get; set; }
        public double price { get; set; }
        public string img { get; set; }
    }
}
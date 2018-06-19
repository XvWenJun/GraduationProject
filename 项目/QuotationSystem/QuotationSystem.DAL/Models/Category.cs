using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Models
{
    public class Category
    {
        public int id { get; set; }
        public string text { get; set; }
        public int parentId { get; set; }
        public bool isLast { get; set; }
    }
}
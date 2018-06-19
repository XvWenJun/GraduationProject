using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Common
{
    public class CategoryTreeList
    {
        public int id { get; set; }
        public string text { get; set; }
        public List<CategoryTreeList> children { get; set; }
    }
}
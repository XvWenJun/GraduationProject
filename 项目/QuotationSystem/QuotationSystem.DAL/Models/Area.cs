using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Models
{
    public class Area
    {
        public int id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
    }
}
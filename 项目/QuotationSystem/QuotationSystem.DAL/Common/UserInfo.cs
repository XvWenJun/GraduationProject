using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Common
{
    public class UserInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public string level { get; set; }
        public string tel { get; set; }
        public string active { get; set; }
    }
}
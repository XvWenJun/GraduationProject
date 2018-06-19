using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Common
{
    public class DutyPermission
    {
        public int id { get; set; }
        public string name { get; set; }
        public string keyCode { get; set; }
        public int menuId { get; set; }
        public bool havePermission { get; set; }
    }
}
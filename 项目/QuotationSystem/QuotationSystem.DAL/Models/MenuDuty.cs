using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Models
{
    public class MenuDuty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string name { get; set; }
        public string keyCode { get; set; }
        public string iconCls { get; set; }
        public string type { get; set; }
        public int menuId { get; set; }
    }
}
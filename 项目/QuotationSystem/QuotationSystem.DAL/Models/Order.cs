using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public DateTime date { get; set; }
        public int staffId { get; set; }
        public string staffName { get; set; }
        public int agentId { get; set; }
        public string agentName { get; set; }
        public string state { get; set; }
    }
}
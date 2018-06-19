using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Models
{
    public class Notice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int receiver { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string url { get; set; }
        public DateTime datetime { get; set; }
        public bool state { get; set; }
    }
}
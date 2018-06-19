using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.DAL.Models
{
    public class PriceLevelChangeRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int priceLevel { get; set; }
        public string levelChangeCondition { get; set; }
        public double upgradeQuantity { get; set; }
        public double degradeQuantity { get; set; }
        public int count { get; set; }
    }
}
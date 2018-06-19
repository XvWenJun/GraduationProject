using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.BLL.Quotation
{
    public class PriceLevelChangeOperation
    {
        #region 查询

        public static List<PriceLevelChangeRule> GetLevelChangeRules()
        {
            DbOperator dbOperator = new DbOperator();
            return dbOperator.pricelevelchangerules.ToList();
        }

        public static PriceLevelChangeRule GetLevelRuleByPriceLevel(int priceLevel)
        {
            DbOperator dbOperator = new DbOperator();
            return dbOperator.pricelevelchangerules.Where(model => model.priceLevel == priceLevel).FirstOrDefault();
        }

        #endregion 查询

        #region 修改

        public static bool EditRules(List<PriceLevelChangeRule> newRules)
        {
            DbOperator dbOperator = new DbOperator();
            var rules = dbOperator.pricelevelchangerules;
            dbOperator.pricelevelchangerules.RemoveRange(rules);
            dbOperator.SaveChanges();
            foreach (var rule in newRules)
            {
                PriceLevelChangeRule exist = dbOperator.pricelevelchangerules.Where(model => model.priceLevel == rule.priceLevel).FirstOrDefault();
                if (exist != null)
                {
                    dbOperator.pricelevelchangerules.Remove(exist);
                    dbOperator.SaveChanges();
                }
                dbOperator.pricelevelchangerules.Add(rule);
                dbOperator.SaveChanges();
            }
            return true;
        }

        #endregion 修改
    }
}
using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.BLL.Procudts
{
    public class PriceSettingOperation
    {
        public static List<ProductPriceSetting> GetSettings()
        {
            DbOperator dbOperator = new DbOperator();
            return dbOperator.productpricesettings.OrderBy(model => model.id).ToList();
        }

        public static int GetPriceCount()
        {
            DbOperator dbOperator = new DbOperator();
            var setting = dbOperator.productpricesettings.Where(model => model.name == "PriceNumber").FirstOrDefault();
            return (int)setting.value;
        }

        public static double GetAutoIncreaseValue()
        {
            DbOperator dbOperator = new DbOperator();
            var setting = dbOperator.productpricesettings.Where(model => model.name == "AutoIncrease").FirstOrDefault();
            if (setting.enable)
                return setting.value;
            else
                return 0;
        }

        public static bool EditSetting(int count, bool enable, double value)
        {
            DbOperator dbOperator = new DbOperator();
            var settings = dbOperator.productpricesettings.OrderBy(model => model.id).ToList();
            settings[0].value = count;
            settings[1].enable = enable;
            settings[1].value = value;
            return dbOperator.SaveChanges() > 0;
        }
    }
}
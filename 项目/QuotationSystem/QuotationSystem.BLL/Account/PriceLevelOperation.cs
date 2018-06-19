using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.BLL.Account
{
    public class PriceLevelOperation
    {
        #region 查询

        public static int GetLeveIdByPriceLevel(int priceLevel)
        {
            DbOperator dbOperator = new DbOperator();
            Level level = dbOperator.levels.Where(model => model.priceLevel == priceLevel).FirstOrDefault();
            if (level != null)
            {
                return level.id;
            }
            return 0;
        }

        public static int GetPriceLevelById(int id)
        {
            DbOperator dbOperator = new DbOperator();
            Level level = dbOperator.levels.Where(model => model.id == id).FirstOrDefault();
            if (level != null)
            {
                return level.priceLevel;
            }
            return 0;
        }

        public static string GetNameById(int id)
        {
            DbOperator dbOperator = new DbOperator();
            Level level = dbOperator.levels.Where(model => model.id == id).FirstOrDefault();
            if (level != null)
            {
                return level.name;
            }
            return null;
        }

        public static GridRows<Level> GetList(GridPager pager, string condition, string query)
        {
            DbOperator dbOperator = new DbOperator();
            GridRows<Level> result = new GridRows<Level>();
            IEnumerable<Level> list = dbOperator.levels.OrderByDescending(model => model.time);

            //获取满足条件的总数
            if (!string.IsNullOrEmpty(query))
            {
                if (condition == "id")
                    list = list.Where(model => model.id.ToString().Contains(query));
                else if (condition == "name")
                    list = list.Where(model => model.name.Contains(query));
                result.total = list.Count();
            }
            else
                result.total = list.Count();

            result.rows = list.Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            return result;
        }

        public static List<Level> GetlevelList()
        {
            DbOperator dbOperator = new DbOperator();
            return dbOperator.levels.ToList();
        }

        #endregion 查询

        #region 添加

        public static bool AddLevel(string name, string describe, bool self, int? priceLevel)
        {
            DbOperator dbOperator = new DbOperator();
            Level level = new Level { name = name, describe = describe, self = self, time = DateTime.Now, priceLevel = priceLevel == null ? 0 : (int)priceLevel };
            dbOperator.levels.Add(level);
            return dbOperator.SaveChanges() > 0;
        }

        #endregion 添加

        #region 修改

        public static bool EditLevel(int id, string name, string describe, bool self, int? priceLevel)
        {
            DbOperator dbOperator = new DbOperator();
            Level level = dbOperator.levels.Where(model => model.id == id).FirstOrDefault();
            if (level != null)
            {
                level.name = name;
                level.describe = describe;
                level.time = DateTime.Now;
                level.self = self;
                level.priceLevel = priceLevel == null ? 0 : (int)priceLevel;
                return dbOperator.SaveChanges() > 0;
            }
            return false;
        }

        #endregion 修改

        #region 删除

        public static bool DeleteLevel(int id)
        {
            DbOperator dbOperator = new DbOperator();
            Level level = dbOperator.levels.Where(model => model.id == id).FirstOrDefault();
            if (level != null)
            {
                dbOperator.levels.Remove(level);
                return dbOperator.SaveChanges() > 0;
            }
            return false;
        }

        #endregion 删除
    }
}
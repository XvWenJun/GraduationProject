using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.BLL.System
{
    public class AreaOperation
    {
        public static IEnumerable<string> GetAreas(int level, string name = "")
        {
            DbOperator dbOperator = new DbOperator();
            if (name == "")
            {
                return dbOperator.areas.Where(model => model.level == level).Select(model => model.name);
            }
            else
            {
                Area area = dbOperator.areas.FirstOrDefault(model => model.name == name && model.level == level);
                if (area != null)
                {
                    return dbOperator.areas.Where(model => model.parentId == area.id).Select(model => model.name);
                }
            }
            return null;
        }
    }
}
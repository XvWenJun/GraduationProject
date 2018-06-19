using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.BLL.System
{
    public class MenuOperation
    {
        public static IEnumerable<Menu> GetMenusByParentId(int level, int parentId)
        {
            DbOperator dbOperator = new DbOperator();
            var menus = from menu in dbOperator.menus
                        join right in dbOperator.levelrights
                        on menu.id equals right.menuId
                        where menu.parid == parentId
                        where right.level == level
                        orderby menu.id
                        select menu;

            return menus;
        }

        public static int GetMenuByAttr(string attr)
        {
            DbOperator dbOperator = new DbOperator();
            var menu = dbOperator.menus.Where(model => model.attributes == attr).Select(model => model.id).FirstOrDefault();
            return menu;
        }

        public static string GetMenuById(int id)
        {
            DbOperator dbOperator = new DbOperator();
            var text = dbOperator.menus.FirstOrDefault(model => model.id == id).text;
            return text;
        }
    }
}
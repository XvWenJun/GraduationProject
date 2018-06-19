using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.BLL.System
{
    public class RightOperation
    {
        #region 查询

        public static IEnumerable<MenuDuty> GetMenuDuty(int level, int menuId)
        {
            DbOperator dbOperator = new DbOperator();

            var result = from duty in dbOperator.menuduties
                         join right in dbOperator.levelrightduties
                         on duty.id equals right.dutyId
                         where duty.menuId == menuId
                         where right.level == level
                         orderby duty.id
                         select duty;
            return result;
        }

        public static List<RightResult> GetRightList()
        {
            DbOperator dbOperator = new DbOperator();
            var rightList = dbOperator.menuduties.GroupBy(model => model.menuId).Select(model => new RightResult { id = model.Key }).ToList();
            foreach (var right in rightList)
            {
                right.text = MenuOperation.GetMenuById(right.id);
            }

            return rightList;
        }

        public static List<DutyPermission> GetDutyList(int level, int id)
        {
            DbOperator dbOperator = new DbOperator();
            var dutyList = dbOperator.menuduties.Where(model => model.menuId == id).Select(model => new DutyPermission { id = model.id, name = model.name, keyCode = model.keyCode, menuId = model.menuId, havePermission = false }).ToList();
            var dutyIdList = dutyList.Select(model => model.id);
            var rightDutyIdList = dbOperator.levelrightduties.Where(model => dutyIdList.Contains(model.dutyId) && model.level == level).Select(model => model.dutyId);
            foreach (var dutyId in rightDutyIdList)
            {
                dutyList.Where(model => model.id == dutyId).FirstOrDefault().havePermission = true;
            }
            return dutyList;
        }

        #endregion 查询

        #region 修改

        public static bool EditUserDuties(int level, List<DutyPermission> dutyList)
        {
            DbOperator dbOperator = new DbOperator();

            if (EditUserDuties(dbOperator, dutyList, level) != 0)
            {
                List<int> dutyIdList = dutyList.Select(model => model.id).ToList();
                int menuId = dutyList[0].menuId;

                EditUserDutyRight(dbOperator, dutyIdList, level, menuId);
                EditUserRights(dbOperator, level, menuId);

                return true;
            }
            return false;
        }

        private static int EditUserDuties(DbOperator dbOperator, List<DutyPermission> dutyList, int level)
        {
            foreach (var duty in dutyList)
            {
                var levelRightDuty = dbOperator.levelrightduties.Where(model => model.level == level && model.dutyId == duty.id).FirstOrDefault();
                if (duty.havePermission && levelRightDuty == null)
                {
                    levelRightDuty = new LevelRightDuty { level = level, dutyId = duty.id };
                    dbOperator.levelrightduties.Add(levelRightDuty);
                }
                else if (!duty.havePermission && levelRightDuty != null)
                {
                    dbOperator.levelrightduties.Remove(levelRightDuty);
                }
            }
            return dbOperator.SaveChanges();
        }

        private static void EditUserDutyRight(DbOperator dbOperator, List<int> dutyIdList, int level, int menuId)
        {
            int havePermisstionCount = dbOperator.levelrightduties.Where(model => model.level == level && dutyIdList.Contains(model.dutyId)).Count();
            LevelRight levelRight = dbOperator.levelrights.Where(model => model.level == level && model.menuId == menuId).FirstOrDefault();
            if (havePermisstionCount == 0 && levelRight != null)
            {
                dbOperator.levelrights.Remove(levelRight);
            }
            else if (havePermisstionCount != 0 && levelRight == null)
            {
                dbOperator.levelrights.Add(new LevelRight { level = level, menuId = menuId });
            }
            dbOperator.SaveChanges();
        }

        private static void EditUserRights(DbOperator dbOperator, int level, int menuId)
        {
            int parId = dbOperator.menus.Where(model => model.id == menuId).FirstOrDefault().parid;
            if (parId == 0)
                return;

            List<int> broIdList = dbOperator.menus.Where(model => model.parid == parId).Select(model => model.id).ToList();
            int LevelRightCount = dbOperator.levelrights.Where(model => model.level == level && broIdList.Contains(model.menuId)).Count();
            LevelRight levelRight = dbOperator.levelrights.Where(model => model.level == level && model.menuId == parId).FirstOrDefault();
            if (LevelRightCount > 0 && levelRight == null)
            {
                dbOperator.levelrights.Add(new LevelRight { level = level, menuId = parId });
            }
            else if (LevelRightCount == 0 && levelRight != null)
            {
                dbOperator.levelrights.Remove(levelRight);
            }
            dbOperator.SaveChanges();
            EditUserRights(dbOperator, level, parId);
        }

        #endregion 修改
    }
}
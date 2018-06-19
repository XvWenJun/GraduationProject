using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuotationSystem.DAL.Common;

namespace QuotationSystem.BLL.System
{
    public class NoticeOperation
    {
        #region 查询

        public static int GetNotReadNotices(int receiver)
        {
            DbOperator dbOperator = new DbOperator();
            IEnumerable<Notice> notices = dbOperator.notices.Where(model => model.receiver == receiver && model.state == false);
            if (notices != null)
                return notices.Count();
            else
                return 0;
        }

        public static GridRows<Notice> GetNoticeList(GridPager pager, int id, string condition, string query)
        {
            DbOperator dbOperator = new DbOperator();
            GridRows<Notice> result = new GridRows<Notice>();

            IEnumerable<Notice> notices = dbOperator.notices.Where(model => model.receiver == id).OrderByDescending(model => model.datetime);

            if (condition == "0")
            {
                notices = notices.Where(model => !model.state);
            }
            else if (condition == "1")
            {
                notices = notices.Where(model => model.state);
            }

            if (!string.IsNullOrWhiteSpace(query))
            {
                notices = notices.Where(model => model.title.Contains(query));
            }

            result.total = notices.Count();
            result.rows = notices.Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            return result;
        }

        #endregion 查询

        #region 添加

        public static bool AddNotice(Notice notice)
        {
            DbOperator dbOperator = new DbOperator();
            dbOperator.notices.Add(notice);
            dbOperator.SaveChanges();
            return true;
        }

        #endregion 添加

        #region 删除

        public static bool DeleteNotice(int id)
        {
            DbOperator dbOperator = new DbOperator();
            Notice notice = dbOperator.notices.Where(model => model.id == id).FirstOrDefault();
            if (notice != null)
            {
                dbOperator.notices.Remove(notice);
                dbOperator.SaveChanges();
                return true;
            }

            return false;
        }

        public static bool DeleteReadNotices(int receiver)
        {
            DbOperator dbOperator = new DbOperator();
            IEnumerable<Notice> notices = dbOperator.notices.Where(model => model.receiver == receiver && model.state == true);
            if (notices != null)
            {
                dbOperator.notices.RemoveRange(notices);
                dbOperator.SaveChanges();
                return true;
            }

            return false;
        }

        #endregion 删除

        #region 修改

        public static bool EditNoticeStateAlreadyRead(int id)
        {
            DbOperator dbOperator = new DbOperator();
            Notice notice = dbOperator.notices.Where(model => model.id == id).FirstOrDefault();
            if (notice != null)
            {
                notice.state = true;
                dbOperator.SaveChanges();
                return true;
            }

            return false;
        }

        public static bool ReadAllNotices(int receiver)
        {
            DbOperator dbOperator = new DbOperator();
            IEnumerable<Notice> notices = dbOperator.notices.Where(model => model.receiver == receiver);
            if (notices != null)
            {
                foreach (var notice in notices)
                {
                    notice.state = true;
                }
                dbOperator.SaveChanges();
                return true;
            }

            return false;
        }

        #endregion 修改
    }
}
using QuotationSystem.BLL.System;
using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Models;
using QuotationSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuotationSystem.Web.Controllers
{
    public class NoticeController : BaseController
    {
        // GET: Notice
        public ActionResult Index()
        {
            return View();
        }

        #region 查询

        [HttpPost]
        public JsonResult GetNoticeList(GridPager pager, string condition, string query)
        {
            int id = GetPriceLevel() <= 0 ? 0 : GetAccountId();
            GridRows<Notice> result = NoticeOperation.GetNoticeList(pager, id, condition, query);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetNotReadNotices()
        {
            int id = GetPriceLevel() <= 0 ? 0 : GetAccountId();
            var count = NoticeOperation.GetNotReadNotices(id);
            return Json(count);
        }

        #endregion 查询

        #region 删除

        [HttpPost]
        public JsonResult DeleteNoticeById(int id)
        {
            ResultView result = new ResultView { result = NoticeOperation.DeleteNotice(id) };
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteReadNotices()
        {
            int id = GetPriceLevel() <= 0 ? 0 : GetAccountId();
            ResultView result = new ResultView { result = NoticeOperation.DeleteReadNotices(id) };
            return Json(result);
        }

        #endregion 删除

        #region 修改

        [HttpPost]
        public JsonResult ReadAllNotices()
        {
            int id = GetPriceLevel() <= 0 ? 0 : GetAccountId();
            ResultView result = new ResultView { result = NoticeOperation.ReadAllNotices(id) };
            return Json(result);
        }

        [HttpPost]
        public JsonResult ReadNoticeById(int id)
        {
            ResultView result = new ResultView { result = NoticeOperation.EditNoticeStateAlreadyRead(id) };
            return Json(result);
        }

        #endregion 修改
    }
}
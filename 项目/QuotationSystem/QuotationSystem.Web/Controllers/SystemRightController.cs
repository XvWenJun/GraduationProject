using QuotationSystem.BLL.Account;
using QuotationSystem.BLL.System;
using QuotationSystem.DAL.Common;
using QuotationSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuotationSystem.Web.Controllers
{
    public class SystemRightController : BaseController
    {
        // GET: SystemRight
        public ActionResult Index()
        {
            List<string> results;
            ViewBag.duties = GetMenuDuties("/SystemRight/Index", out results);
            return View();
        }

        #region 查询

        [HttpPost]
        public JsonResult GetLevelList()
        {
            var result = PriceLevelOperation.GetlevelList();
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetRightList()
        {
            var result = RightOperation.GetRightList();
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetDutyList(int level, string id)
        {
            var result = RightOperation.GetDutyList(level, int.Parse(id));
            return Json(result);
        }

        #endregion 查询

        #region 修改

        [HttpPost]
        public JsonResult EditUserDuty(int level, List<DutyPermission> dutyList)
        {
            ResultView result = new ResultView { result = RightOperation.EditUserDuties(level, dutyList) };
            return Json(result);
        }

        #endregion 修改
    }
}
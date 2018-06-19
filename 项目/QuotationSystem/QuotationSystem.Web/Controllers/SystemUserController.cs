using QuotationSystem.BLL.Account;
using QuotationSystem.BLL.Encrypt;
using QuotationSystem.BLL.Procudts;
using QuotationSystem.BLL.System;
using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Models;
using QuotationSystem.Web.Concrete;
using QuotationSystem.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuotationSystem.Web.Controllers
{
    public class SystemUserController : BaseController
    {
        // GET: SystemUser
        public ActionResult Index()
        {
            List<string> results;
            ViewBag.duties = GetMenuDuties("/SystemUser/Index", out results);
            return View();
        }

        public ActionResult LevelIndex()
        {
            List<string> results;
            ViewBag.duties = GetMenuDuties("/SystemUser/LevelIndex", out results);
            return View();
        }

        //获取用户资料
        public ActionResult ShowUserInfo(int? id)
        {
            User userInfo = AccountOperation.GetUserInfo(id ?? GetAccountId());
            ViewBag.readOnly = (id != null);
            ViewBag.levelText = PriceLevelOperation.GetNameById(userInfo.level);
            return View("UserInfo", userInfo);
        }

        //编辑用户资料
        public ActionResult ShowUserInfoEdit(int? id, string create)
        {
            bool isCreate = create != null;
            User userInfo = isCreate ? AccountOperation.RegisterNewUser() : AccountOperation.GetUserInfo(id ?? GetAccountId());
            ViewBag.self = !isCreate && id == null;
            ViewBag.create = isCreate;
            ViewBag.type = isCreate ? "create" : "edit";
            ViewBag.isAdd = isCreate ? "true" : "false";
            return View("UserInfoEdit", userInfo);
        }

        #region 查询

        //获取区域信息
        [HttpPost]
        public JsonResult GetAreas(int level, string name)
        {
            IEnumerable<string> areas = AreaOperation.GetAreas(level, name);
            return Json(areas);
        }

        [HttpPost]
        public JsonResult GetUserList(GridPager pager, string condition, string query)
        {
            GridRows<UserInfo> result = AccountOperation.GetList(pager, condition, query);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetLevelList(GridPager pager, string condition, string query)
        {
            GridRows<Level> result = PriceLevelOperation.GetList(pager, condition, query);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetPriceCount()
        {
            int result = PriceSettingOperation.GetPriceCount(); ;
            return Json(result);
        }

        #endregion 查询

        #region 修改

        //改密码
        [HttpPost]
        public JsonResult EditPassword(string oldPwd, string newPwd)
        {
            User user = AccountOperation.AccountLogInResult(GetAccountId(), oldPwd);
            ResultView result = new ResultView { result = false };
            if (user == null)
            {
                result.msg = "旧密码不匹配！";
            }
            else if (!AccountOperation.EditUserPassword(user.id, newPwd))
            {
                result.msg = "更改失败，请重试！";
            }
            else
            {
                result.result = true;
                result.msg = "密码修改成功";
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult ResetPassword(int id)
        {
            ResultView result = new ResultView { result = AccountOperation.ResetPassword(id) };
            return Json(result);
        }

        [HttpPost]
        public void UploadAvatar(HttpPostedFileBase file)
        {
            string id = Request["imgId"];
            string src = Uploader.UploadSingleFile(file, id, Uploader.UploaderType.User);
            AccountOperation.EditUserAvatar(int.Parse(id), src);
        }

        [HttpPost]
        public JsonResult EditUserInfo(User user)
        {
            ResultView result = new ResultView { result = AccountOperation.EditUserInfo(user) };
            return Json(result);
        }

        [HttpPost]
        public JsonResult EditLevel(int? id, string name, string describe, bool self, int? priceLevel)
        {
            ResultView result = new ResultView();
            result.result = id == null ? PriceLevelOperation.AddLevel(name, describe, self, priceLevel) : PriceLevelOperation.EditLevel((int)id, name, describe, self, priceLevel);
            return Json(result);
        }

        #endregion 修改

        #region 删除

        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            ResultView result = new ResultView { result = AccountOperation.DeleteUser(id) };
            if (result.result)
            {
                Uploader.DeleteSingeFile(id.ToString(), Uploader.UploaderType.User);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteLevel(int id)
        {
            ResultView result = new ResultView { result = PriceLevelOperation.DeleteLevel(id) };
            return Json(result);
        }

        #endregion 删除
    }
}
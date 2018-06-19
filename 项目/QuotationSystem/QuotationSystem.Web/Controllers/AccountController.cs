using QuotationSystem.BLL.Account;
using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Models;
using QuotationSystem.Web.Concrete;
using QuotationSystem.Web.ViewModels;
using System.Web.Mvc;
using System.Web.Security;

namespace QuotationSystem.Web.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("LogIn");
        }

        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult LogInValidate(int id, string password, bool remeberMe)
        {
            User user = AccountOperation.AccountLogInResult(id, password);

            ResultView logResult = new ResultView();
            if (user == null)
            {
                logResult.result = false;
                logResult.msg = "你的账号或密码错误，请重新输入！";
            }
            else if (user.active == "禁用")
            {
                logResult.result = false;
                logResult.msg = "你的账号已被禁用，请联系管理员！";
            }
            else
            {
                logResult.result = true;
                logResult.msg = "登录成功！";
                FormsAuthentication.SetAuthCookie(user.id + ";" + user.name + ";" + remeberMe, remeberMe);
            }
            return Json(logResult);
        }

        [HttpPost]
        public void LogOut()
        {
            FormsAuthentication.SignOut();

            if (Session["UserView"] != null)
                Session["UserView"] = null;
            Session.Clear();
            Session.Abandon();
        }

        [Authorize]
        [HttpPost]
        public void ChangeUserCookie(string userName)
        {
            if (Session["UserView"] != null)
            {
                UserView user = (UserView)Session["UserView"];
                user.name = userName;
                Session["UserView"] = user;
                FormsAuthentication.SignOut();
                FormsAuthentication.SetAuthCookie(user.id + ";" + user.name + ";" + user.remeberMe, user.remeberMe);
            }
        }
    }
}
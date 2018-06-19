using QuotationSystem.BLL.Account;
using QuotationSystem.BLL.System;
using QuotationSystem.DAL.Models;
using QuotationSystem.Web.Concrete;
using QuotationSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QuotationSystem.Web.Controllers
{
    public class BaseController : Controller
    {
        public int GetAccountId()
        {
            if (Session["UserView"] != null)
            {
                UserView user = (UserView)Session["UserView"];
                return user.id;
            }
            return 0;
        }

        public string GetAccountName()
        {
            if (Session["UserView"] != null)
            {
                UserView user = (UserView)Session["UserView"];
                return user.name;
            }
            return string.Empty;
        }

        public int GetAccountLevel()
        {
            if (Session["UserView"] != null)
            {
                UserView user = (UserView)Session["UserView"];
                int level = AccountOperation.GetUserInfo(user.id).level;
                return level;
            }
            return 0;
        }

        public int GetPriceLevel()
        {
            return PriceLevelOperation.GetPriceLevelById(GetAccountLevel());
        }

        public string GetMenuDuties(string menuAttr, out List<string> results)
        {
            int menuid = MenuOperation.GetMenuByAttr(menuAttr);
            int level = GetAccountLevel();
            var result = RightOperation.GetMenuDuty(level, menuid);
            return GetMenuDutyHtml(result, out results);
        }

        private string GetMenuDutyHtml(IEnumerable<MenuDuty> duties, out List<string> results)
        {
            StringBuilder html = new StringBuilder();
            results = new List<string>();
            foreach (var duty in duties)
            {
                if (duty.type == "btn")
                    html.Append(AddBtn(duty));
                else if (duty.type == "combotree")
                    html.Append(AddCombotree(duty));
                else if (duty.type == "result")
                    results.Add(duty.keyCode);
            }
            if (html.Length > 0)
            {
                html.Append("<span  class=\"datagrid-btn-separator\" style=\"float:none;margin-left:5px;margin-right:5px\"></span>");
            }
            return html.ToString();
        }

        private string AddCombotree(MenuDuty duty)
        {
            return "<span class=\"datagrid-btn-separator\" style=\"float:none;margin-left:5px;margin-right:5px\"></span> <select id=\"" + duty.keyCode + "\" name=\"category\" class=\"easyui - combotree input\" style=\"width: 150px; height: 27px !important; \"></select>";
        }

        private string AddBtn(MenuDuty duty)
        {
            return "<span class=\"datagrid-btn-separator\" style=\"float:none;margin-left:5px;margin-right:5px\"></span><a id = \"" + duty.keyCode + "\" class=\"l-btn clearBg\" style=\"padding: 5px 8px;\"><span  class=\"" + duty.iconCls + "\" style=\"font-size:14px\"></span><span style = \"font-size:12px\" > &nbsp;" + duty.name + "</span></a>";
        }
    }
}
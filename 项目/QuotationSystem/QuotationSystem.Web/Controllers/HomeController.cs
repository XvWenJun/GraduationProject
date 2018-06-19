using QuotationSystem.BLL.Account;
using QuotationSystem.BLL.Procudts;
using QuotationSystem.BLL.Quotation;
using QuotationSystem.BLL.System;
using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using QuotationSystem.Web.Concrete;
using QuotationSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuotationSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var userStr = User.Identity.Name;
            string[] userStrArray = userStr.Split(';');
            UserView user = new UserView { id = int.Parse(userStrArray[0]), name = userStrArray[1], remeberMe = bool.Parse(userStrArray[2]) };
            Session["UserView"] = user;
            int level = GetPriceLevel();
            DateTime date = DateTime.Now;
            if (date.Day == 1)
            {
                DateTime newDate = date.AddMonths(-1);
                Notice notice = new Notice
                {
                    receiver = level <= 0 ? 0 : GetAccountId(),
                    title = "上月报表出炉",
                    datetime = date,
                    message = newDate.Year + "年" + newDate.Month + "月的报表已经出炉；点击下方链接查看上一个月的销售情况。",
                    url = "/Quotation/ShowQuotationEchart",
                    state = false
                };
                NoticeOperation.AddNotice(notice);
            }

            if (level > 0 && date.Day == 1)
            {
                UpdateAgentLevel(date, user.id, level);
            }

            ViewBag.receiver = level <= 0 ? 0 : user.id;
            return View(user);
        }

        public ActionResult HomePage()
        {
            ViewBag.agentId = GetPriceLevel() > 0 ? GetAccountId() : 0;
            return View();
        }

        #region 查询

        //获取菜单目录
        [HttpPost]
        public JsonResult GetMenu(int parentId = 0)
        {
            IEnumerable<Menu> menus = MenuOperation.GetMenusByParentId(GetAccountLevel(), parentId).ToList();
            return Json(menus);
        }

        [HttpPost]
        public JsonResult GetPageData()
        {
            int agentId = GetPriceLevel() > 0 ? GetAccountId() : 0;
            int count = OrderOperation.GetOrderTotalCountByDate(agentId, DateTime.Now);

            GridRows<Order> orders = OrderOperation.GetOrderPassList(agentId, DateTime.Now.ToLongDateString());
            int passCount = orders.rows.Count();

            List<OrderDetailsCategoryData> currentMonthDataList = GetCategoryData(agentId, DateTime.Now.ToLongDateString());
            List<OrderDetailsCategoryData> lastMonthDataList = GetCategoryData(agentId, DateTime.Now.AddMonths(-1).ToLongDateString());

            return Json(new { totalCount = count, passCount = passCount, curdata = currentMonthDataList, lastdata = lastMonthDataList });
        }

        #endregion 查询

        #region 修改

        private void UpdateAgentLevel(DateTime date, int id, int pricelevel)
        {
            var rule = PriceLevelChangeOperation.GetLevelRuleByPriceLevel(pricelevel);
            if (rule == null)
                return;

            int i, upgradeCount = 0, degradeCount = 0, newPriceLevel = pricelevel;
            for (i = 0; i < rule.count; i++)
            {
                date = date.AddMonths(-1);
                var dataList = GetCategoryData(id, date.ToLongDateString());
                var quantity = GetQuantity(rule.levelChangeCondition, dataList);
                if (quantity >= rule.upgradeQuantity)
                    upgradeCount++;
                else if (quantity <= rule.degradeQuantity)
                    degradeCount++;
                else
                    break;
            }

            if (upgradeCount == rule.count)
                newPriceLevel = Math.Max(1, --newPriceLevel);
            else if (degradeCount == rule.count)
                newPriceLevel = Math.Min(PriceSettingOperation.GetPriceCount(), ++newPriceLevel);
            else
                return;

            int level = PriceLevelOperation.GetLeveIdByPriceLevel(newPriceLevel);
            if (level == 0)
                return;

            if (AccountOperation.EditUserLevelById(id, level))
            {
                Notice notice = new Notice
                {
                    receiver = id,
                    title = newPriceLevel < pricelevel ? "用户升级" : "用户降级",
                    datetime = DateTime.Now,
                    url = "/SystemProduct/Index",
                    state = false
                };
                if (newPriceLevel < pricelevel)
                    notice.message = string.Format("恭喜你升级到{0}；由于你销售情况优秀，相较于之前的产品报价，你将获得更低的产品售价，希望你能继续保持，再接再厉，也祝我们合作愉快！点击下方链接查看新的售价。", PriceLevelOperation.GetNameById(level));
                else
                    notice.message = string.Format("很遗憾你降级到{0}；由于你销售情况不理想，相较于之前的产品报价，你将获得更高的产品售价，希望你今后能有一个好的销售情况，也祝我们合作愉快！点击下方链接查看新的售价。", PriceLevelOperation.GetNameById(level));

                NoticeOperation.AddNotice(notice);
            }
        }

        private List<OrderDetailsCategoryData> GetCategoryData(int id, string time)
        {
            GridRows<Order> orders = OrderOperation.GetOrderPassList(id, time);
            var orderList = from order in orders.rows
                            select order.id;
            List<OrderDetailsCategoryData> dataList = OrderDetailOperation.GetOrderDetailTotalData(orderList.ToList());
            return dataList;
        }

        private double GetQuantity(string type, List<OrderDetailsCategoryData> dataList)
        {
            double totalCost = 0, totalPrice = 0;
            foreach (var data in dataList)
            {
                totalCost += data.totalCost;
                totalPrice += data.totalPrice;
            }
            if (type == "销售额")
                return totalPrice;
            else
                return totalPrice - totalCost;
        }

        #endregion 修改
    }
}
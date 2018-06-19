using QuotationSystem.BLL.Quotation;
using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Models;
using QuotationSystem.Web.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;

using System;

using Xceed.Words.NET;
using System.Drawing;
using QuotationSystem.BLL.Word;
using System.Linq;
using QuotationSystem.Web.Concrete;
using QuotationSystem.BLL.System;
using QuotationSystem.BLL.Procudts;

namespace QuotationSystem.Web.Controllers
{
    public class QuotationController : BaseController
    {
        // GET: Quotation
        public ActionResult Index(int? id, bool? edit)
        {
            ViewBag.level = GetPriceLevel();
            ViewBag.readOnly = id != null && edit == null ? true : false;
            ViewBag.id = id;
            return View();
        }

        public ActionResult ShowQuotationInfo()
        {
            List<string> results;
            ViewBag.duties = GetMenuDuties("/Quotation/ShowQuotationInfo", out results);
            return View();
        }

        public ActionResult ShowQuotationEchart()
        {
            ViewBag.agentId = GetPriceLevel() > 0 ? GetAccountId() : 0;
            return View();
        }

        public ActionResult AgentLevelChangeView()
        {
            List<string> results;
            ViewBag.duties = GetMenuDuties("/Quotation/AgentLevelChangeView", out results);
            return View();
        }

        #region 修改

        [HttpPost]
        public JsonResult UpdateQuotationData(int orderId, List<OrderDetail> orderDetails)

        {
            ResultView result = new ResultView();

            if (orderId == 0)
                result.result = OrderDetailOperation.AddQuotaion(orderDetails, GetAccountId(), GetAccountName());
            else
                result.result = OrderDetailOperation.EditQuotation(orderId, orderDetails);

            if (result.result)
            {
                Notice notice = new Notice
                {
                    receiver = 0,
                    title = orderId == 0 ? "有新报价单" : "报价单有改动",
                    url = "/Quotation/ShowQuotationInfo",
                    datetime = DateTime.Now,
                    state = false
                };
                if (orderId == 0)
                    notice.message = string.Format("代理商已添加报价单；点击下方链接查看报价单。");
                else
                    notice.message = string.Format("代理商已修改{0}报价单；点击下方链接查看报价单。", orderId);

                NoticeOperation.AddNotice(notice);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult EditOrderState(int id, string state)
        {
            int agentId = OrderOperation.EditOrderState(id, state, GetAccountId(), GetAccountName());
            ResultView result = new ResultView { result = agentId != 0 };
            if (result.result)
            {
                Notice notice = new Notice
                {
                    receiver = agentId,
                    title = "订单审核结果",
                    message = "你的订单号为" + id + "的订单" + state + "；点击下方链接查看报价单。",
                    url = "/Quotation/ShowQuotationInfo",
                    datetime = DateTime.Now,
                    state = false
                };
                NoticeOperation.AddNotice(notice);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult EditPriceLevelChangeRules(List<PriceLevelChangeRule> rules)
        {
            ResultView result = new ResultView { result = PriceLevelChangeOperation.EditRules(rules) };
            return Json(result);
        }

        #endregion 修改

        #region 查询

        [HttpPost]
        public JsonResult GetOrderDetailTotalDataList(List<int> orderIdList)
        {
            var result = OrderDetailOperation.GetOrderDetailTotalData(orderIdList);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetOrderPassList(string date)
        {
            int agentId = GetPriceLevel() > 0 ? GetAccountId() : 0;
            GridRows<Order> result = OrderOperation.GetOrderPassList(agentId, date);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetOrderList(GridPager pager, string condition, string query, string date)
        {
            int agentId = GetPriceLevel() > 0 ? GetAccountId() : 0;
            GridRows<Order> result = OrderOperation.GetOrderList(pager, agentId, condition, query, date);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetOrderDetails(int id)
        {
            GridRows<OrderDetail> result = OrderDetailOperation.GetOrderDetailPagerList(id);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetOrderDateList()
        {
            int agentId = GetPriceLevel() <= 0 ? 0 : GetAccountId();
            var json = OrderOperation.GetOrderDateList(agentId);
            var result = from date in json
                         select new { id = date, text = date.Split('/')[0] + "年/" + date.Split('/')[1] + "月" };
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetLevelChangeRules()
        {
            var result = PriceLevelChangeOperation.GetLevelChangeRules();
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetPriceList()
        {
            var count = PriceSettingOperation.GetPriceCount();
            List<PriceLevelView> priceLevels = new List<PriceLevelView>();
            for (int i = 1; i <= count; i++)
            {
                priceLevels.Add(new PriceLevelView { value = i, text = i + "级售价" });
            }
            return Json(priceLevels);
        }

        #endregion 查询

        #region 下载报价单

        public void ExportWord(int orderId)
        {
            bool showProfit = GetPriceLevel() <= 0 ? true : false;
            string fileName = orderId + "号报价单--" + (showProfit ? "内部报价单" : "外部报价单") + ".docx";

            MemoryStream stream = WordOperation.BuildReportForm(orderId, showProfit);
            byte[] byteArray = stream.ToArray();

            Response.Clear();
            Response.AddHeader("Content-Length", byteArray.Length.ToString());
            Response.AppendHeader("Content-Disposition", string.Format("attachment;filename=" + fileName, DateTime.Now.ToString("yyMMddHHmmss")));
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            Response.BinaryWrite(byteArray);
            Response.Flush();
            Response.End();
        }

        #endregion 下载报价单

        #region 删除

        public JsonResult DeleteQuotationByOrderId(int orderId)
        {
            ResultView result = new ResultView { result = OrderOperation.DeleteOrderById(orderId) };
            return Json(result);
        }

        #endregion 删除
    }
}
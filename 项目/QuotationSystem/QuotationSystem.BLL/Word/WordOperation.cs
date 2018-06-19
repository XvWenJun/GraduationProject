using QuotationSystem.BLL.Account;
using QuotationSystem.BLL.Quotation;
using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xceed.Words.NET;

namespace QuotationSystem.BLL.Word
{
    public class WordOperation
    {
        public static MemoryStream BuildReportForm(int orderId, bool showProfit = false)
        {
            Order order;
            User staff, agent;
            List<OrderDetail> orderDetails;
            GetReportFormInfo(orderId, out order, out staff, out agent, out orderDetails);

            MemoryStream memStream = new MemoryStream();

            using (var document = DocX.Create(memStream))
            {
                BuildHeaderAndFooter(document);

                var title = document.InsertParagraph();
                title.Append("报价单").FontSize(25d).Bold().SpacingAfter(25d).Alignment = Alignment.center;
                title.InsertHorizontalLine();

                BuildUserTable(document, staff, agent);

                var explain = document.InsertParagraph();
                explain.Append("以下为贵公司询价产品明细，请详阅：如有凝问，请及时与我司联系，谢谢！").FontSize(10d).SpacingBefore(20d).SpacingAfter(30d).Alignment = Alignment.left;

                BuildProductTable(document, orderDetails, showProfit);

                var date = document.InsertParagraph();
                date.Append("日期：" + order.date.ToLongDateString()).FontSize(15d).SpacingBefore(50d).Alignment = Alignment.right;

                document.AddProtection(EditRestrictions.readOnly);

                document.Save();
            }
            return memStream;
        }

        private static void BuildHeaderAndFooter(DocX document)
        {
            document.AddHeaders();
            document.AddFooters();
            document.Headers.Even.InsertParagraph("百货公司报价系统报价单").Bold();
            document.Headers.Odd.InsertParagraph("百货公司报价系统报价单").Bold();
            document.Footers.Even.InsertParagraph("页数 P").AppendPageNumber(PageNumberFormat.normal);
            document.Footers.Odd.InsertParagraph("页数 P").AppendPageNumber(PageNumberFormat.normal);
        }

        private static void GetReportFormInfo(int orderId, out Order order, out User staff, out User agent, out List<OrderDetail> orderDetails)
        {
            order = OrderOperation.GetOrderById(orderId);
            staff = AccountOperation.GetUserInfo(order.staffId);
            agent = AccountOperation.GetUserInfo(order.agentId);
            orderDetails = OrderDetailOperation.GetOrderDetailsByOrderId(orderId);
        }

        private static void BuildUserTable(DocX document, User staff, User agent)
        {
            var userTable = document.AddTable(3, 4);

            userTable.SetWidths(new float[] { 200f, 400, 200f, 400f });
            userTable.Alignment = Alignment.center;
            userTable.Design = TableDesign.TableNormal;
            Border border = new Border(BorderStyle.Tcbs_single, BorderSize.one, 1, Color.Black);
            userTable.SetBorder(TableBorderType.Bottom, border);
            userTable.SetBorder(TableBorderType.InsideH, border);

            userTable.Rows[0].Cells[0].Paragraphs[0].Append("报价方：").FontSize(15d);
            userTable.Rows[0].Cells[2].Paragraphs[0].Append("询价方：").FontSize(15d);
            userTable.Rows[1].Cells[0].Paragraphs[0].Append("联系人：").FontSize(15d);
            userTable.Rows[1].Cells[2].Paragraphs[0].Append("联系人：").FontSize(15d);
            userTable.Rows[2].Cells[0].Paragraphs[0].Append("手  机：").FontSize(15d);
            userTable.Rows[2].Cells[2].Paragraphs[0].Append("手  机：").FontSize(15d);

            userTable.Rows[0].Cells[1].Paragraphs[0].Append(staff.company).FontSize(15d);
            userTable.Rows[0].Cells[3].Paragraphs[0].Append(agent.company).FontSize(15d);
            userTable.Rows[1].Cells[1].Paragraphs[0].Append(staff.name).FontSize(15d);
            userTable.Rows[1].Cells[3].Paragraphs[0].Append(agent.name).FontSize(15d);
            userTable.Rows[2].Cells[1].Paragraphs[0].Append(staff.tel).FontSize(15d);
            userTable.Rows[2].Cells[3].Paragraphs[0].Append(agent.tel).FontSize(15d);

            document.InsertTable(userTable);
        }

        private static void BuildProductTable(DocX document, List<OrderDetail> orderDetails, bool showProfit)
        {
            var productTable = document.AddTable(1, showProfit ? 8 : 6);

            productTable.Alignment = Alignment.center;
            productTable.Design = TableDesign.TableGrid;

            productTable.Rows[0].Cells[0].Paragraphs[0].Append("序号").FontSize(12d);
            productTable.Rows[0].Cells[1].Paragraphs[0].Append("名称").FontSize(12d);
            productTable.Rows[0].Cells[2].Paragraphs[0].Append("数量").FontSize(12d);
            productTable.Rows[0].Cells[3].Paragraphs[0].Append("单位").FontSize(12d);

            var nextIndex = 4;
            if (showProfit)
            {
                productTable.Rows[0].Cells[4].Paragraphs[0].Append("成本").FontSize(12d);
                productTable.Rows[0].Cells[7].Paragraphs[0].Append("总利润").FontSize(12d);
                nextIndex++;
            }

            productTable.Rows[0].Cells[nextIndex].Paragraphs[0].Append("售价").FontSize(12d);
            productTable.Rows[0].Cells[nextIndex + 1].Paragraphs[0].Append("总售价").FontSize(12d);

            double sumPrice = 0, sumProfit = 0;
            foreach (var detail in orderDetails)
            {
                double totalPrice = detail.productCount * detail.productPrice;
                double totalProfit = detail.productCount * (detail.productPrice - detail.productCost);
                sumPrice += totalPrice;
                sumProfit += totalProfit;

                var row = productTable.InsertRow();
                row.Cells[0].Paragraphs[0].Append(detail.productId.ToString()).FontSize(12d);
                row.Cells[1].Paragraphs[0].Append(detail.productName).FontSize(12d);
                row.Cells[2].Paragraphs[0].Append(detail.productCount.ToString()).FontSize(12d);
                row.Cells[3].Paragraphs[0].Append(detail.productUnit).FontSize(12d);
                var next = 4;
                if (showProfit)
                {
                    row.Cells[4].Paragraphs[0].Append(detail.productCost.ToString()).FontSize(12d);
                    row.Cells[7].Paragraphs[0].Append(totalProfit.ToString("f2")).FontSize(12d);
                    next++;
                }
                row.Cells[next].Paragraphs[0].Append(detail.productPrice.ToString()).FontSize(12d);
                row.Cells[next + 1].Paragraphs[0].Append(totalPrice.ToString("f2")).FontSize(12d);
            }

            var sum = productTable.InsertRow();
            sum.MergeCells(0, showProfit ? 5 : 4);
            sum.Cells[0].Paragraphs[0].Append("共计").FontSize(15d);
            sum.Cells[1].Paragraphs[0].Append(sumPrice.ToString("f2")).FontSize(15d);
            if (showProfit)
                sum.Cells[2].Paragraphs[0].Append(sumProfit.ToString("f2")).FontSize(15d);

            document.InsertTable(productTable);
        }
    }
}
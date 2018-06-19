using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.BLL.Quotation
{
    public class OrderOperation
    {
        #region 查询

        public static Order GetOrderById(int id)
        {
            DbOperator dbOperator = new DbOperator();
            Order order = dbOperator.orders.Where(model => model.id == id).FirstOrDefault();

            return order;
        }

        public static int GetOrderTotalCountByDate(int agentId, DateTime time)
        {
            DbOperator dbOperator = new DbOperator();
            IEnumerable<Order> orders = dbOperator.orders.Where(model => model.date.Year == time.Year && model.date.Month == time.Month);
            if (agentId != 0)
            {
                orders = orders.Where(model => model.agentId == agentId);
            }
            return orders.Count();
        }

        public static int GetOrderPassTotalPriceByDate(int agentId, DateTime time)
        {
            DbOperator dbOperator = new DbOperator();
            IEnumerable<Order> orders = dbOperator.orders.Where(model => model.date.Year == time.Year && model.date.Month == time.Month && model.state == "审核通过");
            if (agentId != 0)
            {
                orders = orders.Where(model => model.agentId == agentId);
            }
            return orders.Count();
        }

        public static GridRows<Order> GetOrderList(GridPager pager, int agentId, string condition, string query, string date)
        {
            DbOperator dbOperator = new DbOperator();
            GridRows<Order> result = new GridRows<Order>();
            IEnumerable<Order> list = dbOperator.orders;

            if (agentId != 0)
            {
                list = list.Where(model => model.agentId == agentId);
            }

            if (!string.IsNullOrWhiteSpace(date))
            {
                DateTime time = DateTime.Parse(date);
                list = list.Where(model => DateTime.Compare(model.date, time) >= 0);
            }

            //获取满足条件的总数
            if (!string.IsNullOrEmpty(query))
            {
                if (condition == "id")
                    list = list.Where(model => model.id.ToString().Contains(query));
                else if (condition == "state")
                    list = list.Where(model => model.state.Contains(query));
                else if (condition == "agentName")
                    list = list.Where(model => model.agentName.Contains(query));
                else if (condition == "staffName")
                    list = list.Where(model => model.staffName.Contains(query));
                result.total = list.Count();
            }
            else
                result.total = list.Count();

            //取得实际返回的数量
            if (pager.order == "desc")
                result.rows = list.OrderBy(model => model.date).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            else
                result.rows = list.OrderByDescending(model => model.date).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();

            return result;
        }

        public static GridRows<Order> GetOrderPassList(int agentId, string date)
        {
            DbOperator dbOperator = new DbOperator();
            GridRows<Order> result = new GridRows<Order>();
            IEnumerable<Order> list = dbOperator.orders.Where(model => model.state == "审核通过");

            if (agentId != 0)
            {
                list = list.Where(model => model.agentId == agentId);
            }

            if (!string.IsNullOrWhiteSpace(date))
            {
                DateTime time = DateTime.Parse(date);
                list = list.Where(model => model.date.Year == time.Year && model.date.Month == time.Month);
                result.total = list.Count();
            }
            else
            {
                result.total = list.Count();
            }
            result.rows = list.OrderByDescending(model => model.date).ToList();
            return result;
        }

        public static List<string> GetOrderDateList(int agentId = 0)
        {
            DbOperator dbOperator = new DbOperator();
            IEnumerable<Order> orders = dbOperator.orders.Where(model => model.state == "审核通过");
            if (agentId != 0)
            {
                orders = orders.Where(model => model.agentId == agentId);
            }

            var dateList = orders.GroupBy(model => model.date.Year + "/" + model.date.Month.ToString("00")).Select(model => model.Key).ToList();
            dateList.Sort();
            dateList.Reverse();
            return dateList;
        }

        #endregion 查询

        #region 添加

        public static int AddOrder(int agentId, string agentName)
        {
            DbOperator dbOperator = new DbOperator();
            Order order = dbOperator.orders.Add(new Order { date = DateTime.Now, agentId = agentId, agentName = agentName, state = "待审核" });
            dbOperator.SaveChanges();
            return order.id;
        }

        #endregion 添加

        #region 修改

        public static int EditOrderState(int id, string state, int staffId, string staffName)
        {
            DbOperator dbOperator = new DbOperator();
            Order order = dbOperator.orders.Where(model => model.id == id).FirstOrDefault();
            if (order != null)
            {
                order.state = state;
                order.staffId = staffId;
                order.staffName = staffName;
                dbOperator.SaveChanges();
                return order.agentId;
            }
            return 0;
        }

        public static bool UpdateOrderDate(int id)
        {
            DbOperator dbOperator = new DbOperator();
            Order order = dbOperator.orders.Where(model => model.id == id).FirstOrDefault();
            if (order != null)
            {
                order.date = DateTime.Now;
                order.state = "待审核";
                order.staffId = 0;
                order.staffName = "";
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion 修改

        #region 删除

        public static bool DeleteOrderById(int orderId)
        {
            DbOperator dbOperator = new DbOperator();
            Order order = dbOperator.orders.Where(model => model.id == orderId).FirstOrDefault();
            if (order != null)
            {
                OrderDetailOperation.DeleteOrderDetailsByOrderId(orderId);
                dbOperator.orders.Remove(order);
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion 删除
    }
}
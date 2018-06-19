using QuotationSystem.BLL.Procudts;
using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.BLL.Quotation
{
    public class OrderDetailOperation
    {
        #region 查询

        public static GridRows<OrderDetail> GetOrderDetailPagerList(int id)
        {
            DbOperator dbOperator = new DbOperator();
            GridRows<OrderDetail> result = new GridRows<OrderDetail>();
            IEnumerable<OrderDetail> details = dbOperator.orderdetails.Where(model => model.orderId == id);
            result.rows = details.ToList();
            result.total = result.rows.Count;
            return result;
        }

        public static List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            DbOperator dbOperator = new DbOperator();
            IEnumerable<OrderDetail> details = dbOperator.orderdetails.Where(model => model.orderId == orderId);
            return details.ToList();
        }

        public static List<OrderDetailsCategoryData> GetOrderDetailTotalData(List<int> orderIdList)
        {
            DbOperator dbOperator = new DbOperator();
            List<OrderDetail> orderDetails = dbOperator.orderdetails.Where(model => orderIdList.Contains(model.orderId)).ToList();
            List<OrderDetailsCategoryData> categoriesData = new List<OrderDetailsCategoryData>();
            foreach (var orderDatail in orderDetails)
            {
                var category = categoriesData.Find(value => value.categoryId == orderDatail.productCategory);
                if (category == null)
                {
                    OrderDetailsCategoryData data = new OrderDetailsCategoryData
                    {
                        categoryId = orderDatail.productCategory,
                        categoryText = CategoryOperation.GetCategoryTextById(orderDatail.productCategory),
                        totalPrice = orderDatail.productCount * orderDatail.productPrice,
                        totalCost = orderDatail.productCount * orderDatail.productCost
                    };
                    categoriesData.Add(data);
                }
                else
                {
                    category.totalPrice += orderDatail.productCount * orderDatail.productPrice;
                    category.totalCost += orderDatail.productCount * orderDatail.productCost;
                }
            }
            return categoriesData;
        }

        #endregion 查询

        #region 添加

        public static bool AddQuotaion(List<OrderDetail> orderDetails, int agentId, string agentName)
        {
            int orderId = OrderOperation.AddOrder(agentId, agentName);
            DbOperator dbOperator = new DbOperator();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.orderId = orderId;
            }

            dbOperator.orderdetails.AddRange(orderDetails);
            dbOperator.SaveChanges();
            return true;
        }

        #endregion 添加

        #region 修改

        public static bool EditQuotation(int orderId, List<OrderDetail> orderDetails)
        {
            OrderOperation.UpdateOrderDate(orderId);
            DbOperator dbOperator = new DbOperator();
            IEnumerable<OrderDetail> details = dbOperator.orderdetails.Where(model => model.orderId == orderId);
            dbOperator.orderdetails.RemoveRange(details);
            foreach (var order in orderDetails)
            {
                order.orderId = orderId;
            }
            dbOperator.orderdetails.AddRange(orderDetails);
            dbOperator.SaveChanges();
            return true;
        }

        #endregion 修改

        #region 删除

        public static bool DeleteOrderDetailsByOrderId(int orderId)
        {
            DbOperator dbOperator = new DbOperator();
            IEnumerable<OrderDetail> details = dbOperator.orderdetails.Where(model => model.orderId == orderId);
            dbOperator.orderdetails.RemoveRange(details);
            dbOperator.SaveChanges();
            return true;
        }

        #endregion 删除
    }
}
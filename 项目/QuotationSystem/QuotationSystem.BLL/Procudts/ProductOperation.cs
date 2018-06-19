using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.BLL.Procudts
{
    public class ProductOperation
    {
        #region 查询

        public static GridRows<ProductShowInfo> GetProductList(int priceLevel, GridPager pager, string condition, string query, List<int> checkedlist)
        {
            DbOperator dbOperator = new DbOperator();
            GridRows<ProductShowInfo> result = new GridRows<ProductShowInfo>();
            IEnumerable<Product> list = dbOperator.products;

            //分类查询
            if (checkedlist != null && checkedlist.Count != 0)
            {
                list = list.Where(model => checkedlist.Contains(model.category));
            }

            //获取满足条件的总数
            if (!string.IsNullOrEmpty(query))
            {
                if (condition == "id")
                    list = list.Where(model => model.id.ToString().Contains(query));
                else if (condition == "name")
                    list = list.Where(model => model.name.Contains(query));
                result.total = list.Count();
            }
            else
                result.total = list.Count();

            List<Product> product;
            //取得实际返回的数量
            if (pager.order == "desc")
                product = list.OrderBy(model => model.id).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            else
                product = list.OrderByDescending(model => model.id).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();

            result.rows = GetProductShowInfo(priceLevel, product);
            return result;
        }

        private static List<ProductShowInfo> GetProductShowInfo(int priceLevel, List<Product> products)
        {
            List<ProductShowInfo> showInfo = new List<ProductShowInfo>();
            foreach (var product in products)
            {
                showInfo.Add(new ProductShowInfo
                {
                    id = product.id,
                    name = product.name,
                    describe = product.describe,
                    category = product.category,
                    cost = product.cost,
                    img = product.img,
                    unit = product.unit,
                    price = GetProductPrice(product.id, priceLevel)
                });
            }
            return showInfo;
        }

        private static double GetProductPrice(int id, int priceLevel)
        {
            DbOperator dbOperator = new DbOperator();
            ProductPrice price = dbOperator.productprices.Where(model => model.productId == id && model.priceLevel == priceLevel).FirstOrDefault();
            if (price != null)
                return price.price;
            return 0;
        }

        public static ProductFullInfo GetProductById(int id)
        {
            DbOperator dbOperator = new DbOperator();
            Product product = dbOperator.products.Where(model => model.id == id).FirstOrDefault();

            var prices = dbOperator.productprices.Where(model => model.productId == id).OrderBy(model => model.priceLevel).Select(model => model.price).ToArray();
            int count = PriceSettingOperation.GetPriceCount();
            ProductFullInfo fullInfo = new ProductFullInfo { product = product, prices = new double[count] };

            for (int i = 0; i < count; i++)
            {
                fullInfo.prices[i] = prices.Length > i ? prices[i] : 0;
            }
            return fullInfo;
        }

        #endregion 查询

        #region 编辑

        public static bool EditUserAvatar(int id, string path)
        {
            DbOperator dbOperator = new DbOperator();
            Product user = dbOperator.products.FirstOrDefault(model => model.id == id);
            if (user != null)
            {
                user.img = path;
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool EditProductInfo(ProductFullInfo productFullInfo)
        {
            DbOperator dbOperator = new DbOperator();
            Product updateProduct = dbOperator.products.FirstOrDefault(model => model.id == productFullInfo.product.id);
            if (updateProduct != null)
            {
                updateProduct.name = productFullInfo.product.name;
                updateProduct.category = productFullInfo.product.category;
                updateProduct.describe = productFullInfo.product.describe;
                updateProduct.unit = productFullInfo.product.unit;
                updateProduct.cost = productFullInfo.product.cost;
                EditProductPrices(dbOperator, productFullInfo.product.id, productFullInfo.prices);
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        private static void EditProductPrices(DbOperator dbOperator, int id, double[] prices)
        {
            IEnumerable<ProductPrice> priceList = dbOperator.productprices.Where(model => model.productId == id);
            if (priceList.Count() != 0)
                dbOperator.productprices.RemoveRange(priceList);

            dbOperator.productprices.AddRange(GetPrices(id, prices));
        }

        private static IEnumerable<ProductPrice> GetPrices(int id, double[] prices)
        {
            List<ProductPrice> productPrices = new List<ProductPrice>();
            for (int i = 0; i < prices.Length; i++)
            {
                productPrices.Add(new ProductPrice { productId = id, priceLevel = i + 1, price = prices[i] });
            }
            return productPrices;
        }

        public static bool EditProductListCategory(List<int> productList, int category)
        {
            DbOperator dbOperator = new DbOperator();
            IEnumerable<Product> updateProductList = dbOperator.products.Where(model => productList.Contains(model.id));
            if (updateProductList != null)
            {
                foreach (var product in updateProductList)
                    product.category = category;
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion 编辑

        #region 删除

        public static bool DeleteProduct(int id)
        {
            DbOperator dbOperator = new DbOperator();
            Product product = dbOperator.products.Where(model => model.id == id).FirstOrDefault();
            if (product != null)
            {
                dbOperator.products.Remove(product);
                dbOperator.SaveChanges();
                return true;
            }
            return true;
        }

        public static bool DeleteProductList(List<int> idList)
        {
            DbOperator dbOperator = new DbOperator();
            IEnumerable<Product> productList = dbOperator.products.Where(model => idList.Contains(model.id));
            if (productList != null)
            {
                dbOperator.products.RemoveRange(productList);
                DeleteProductPrices(dbOperator, idList);
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        private static void DeleteProductPrices(DbOperator dbOperator, List<int> idList)
        {
            IEnumerable<ProductPrice> prices = dbOperator.productprices.Where(model => idList.Contains(model.productId));
            if (prices != null)
            {
                dbOperator.productprices.RemoveRange(prices);
            }
        }

        #endregion 删除

        #region 添加

        public static ProductFullInfo RegisterNewProduct()
        {
            DbOperator dbOperator = new DbOperator();

            Product product = new Product() { name = "默认" };
            dbOperator.products.Add(product);
            dbOperator.SaveChanges();

            ProductFullInfo fullInfo = new ProductFullInfo { product = product, prices = new double[PriceSettingOperation.GetPriceCount()] };
            return fullInfo;
        }

        public static int AddProductList(List<ProductFullInfo> productInfos)
        {
            DbOperator dbOperator = new DbOperator();
            int addCount = 0;
            foreach (var productInfo in productInfos)
            {
                int count = dbOperator.products.Where(model => model.name == productInfo.product.name).Count();
                if (count == 0)
                {
                    dbOperator.products.Add(productInfo.product);
                    dbOperator.SaveChanges();
                    EditProductPrices(dbOperator, productInfo.product.id, productInfo.prices);
                    addCount++;
                }
            }
            dbOperator.SaveChanges();
            return addCount;
        }

        #endregion 添加
    }
}
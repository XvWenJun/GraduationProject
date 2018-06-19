using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using System.IO;
using QuotationSystem.DAL.ExcelModels;
using LinqToExcel.Query;
using QuotationSystem.DAL.Models;
using QuotationSystem.BLL.Procudts;
using QuotationSystem.DAL.Common;

namespace QuotationSystem.BLL.Excel
{
    public class ExcelOperation
    {
        public static int count = 0;

        public static double rate = 0;

        public enum modelType
        {
            sample,
            error
        }

        public static int CheckImportExcelFile(string root, string path)
        {
            string fullPath = Path.Combine(root, path);
            if (!File.Exists(fullPath))
                return 0;

            var excelFile = new ExcelQueryFactory(fullPath);
            int rightCount = 0;
            count = PriceSettingOperation.GetPriceCount();
            rate = PriceSettingOperation.GetAutoIncreaseValue() / 100;
            var type = GetModelType(excelFile);
            if (type == modelType.sample)
            {
                var excelContent = AddMappingOnSampleModel(excelFile);
                rightCount = AddRightData(excelContent);
            }

            return rightCount;
        }

        public static modelType GetModelType(ExcelQueryFactory excelFile)
        {
            var excelCols = excelFile.GetColumnNames("Sheet1");
            int colCount = excelCols.Count();
            return colCount == 7 ? modelType.sample : modelType.error;
        }

        public static ExcelQueryable<SampleModel> AddMappingOnSampleModel(ExcelQueryFactory excelFile)
        {
            excelFile.AddMapping<SampleModel>(x => x.name, "名字");
            excelFile.AddMapping<SampleModel>(x => x.cost, "成本");
            excelFile.AddMapping<SampleModel>(x => x.price, "预售价");
            excelFile.AddMapping<SampleModel>(x => x.rate, "价格增长比例（%）");
            excelFile.AddMapping<SampleModel>(x => x.unit, "单位");
            excelFile.AddMapping<SampleModel>(x => x.category, "分类");
            excelFile.AddMapping<SampleModel>(x => x.describe, "备注");
            var excelContent = excelFile.Worksheet<SampleModel>(0);
            return excelContent;
        }

        public static int AddRightData(ExcelQueryable<SampleModel> excelContent)
        {
            int insertCount = 0;
            List<ProductFullInfo> products = new List<ProductFullInfo>();
            foreach (var row in excelContent)
            {
                ProductFullInfo product = GerProduct(row);
                if (product != null)
                    products.Add(product);
            }
            if (products.Count > 0)
                insertCount = ProductOperation.AddProductList(products);
            return insertCount;
        }

        public static ProductFullInfo GerProduct(SampleModel row)
        {
            if (EmptyValidate(row.name) || EmptyValidate(row.unit))
                return null;
            if (NumberValidate(row.cost) || NumberValidate(row.price))
                return null;
            if (RateValidate(row.rate) && rate == 0)
                return null;
            double price = double.Parse(row.price);
            double trueRate = RateValidate(row.rate) ? rate : double.Parse(row.rate) / 100;

            ProductFullInfo info = new ProductFullInfo
            {
                product = new Product
                {
                    name = row.name,
                    category = CategoryOperation.GetCategoryIdByText(string.IsNullOrWhiteSpace(row.category) ? "新类别" : row.category),
                    describe = row.describe,
                    cost = double.Parse(row.cost),
                    unit = row.unit
                },
                prices = new double[count]
            };
            for (int i = 0; i < count; i++)
            {
                info.prices[i] = price * (1 + i * trueRate);
            }
            return info;
        }

        private static bool EmptyValidate(string val)
        {
            return string.IsNullOrWhiteSpace(val);
        }

        private static bool NumberValidate(string val)
        {
            double parse;
            return !double.TryParse(val, out parse);
        }

        private static bool RateValidate(string val)
        {
            double parse;
            if (!double.TryParse(val, out parse))
                return true;
            else if (parse < 0 || parse > 100)
                return true;
            else
                return false;
        }
    }
}
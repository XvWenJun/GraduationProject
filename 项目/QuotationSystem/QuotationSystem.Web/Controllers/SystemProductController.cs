using QuotationSystem.BLL.Excel;
using QuotationSystem.BLL.Procudts;
using QuotationSystem.BLL.System;
using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Models;
using QuotationSystem.Web.Concrete;
using QuotationSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuotationSystem.Web.Controllers
{
    public class SystemProductController : BaseController
    {
        public ActionResult Index(bool? readOnly)
        {
            ViewBag.readOnly = readOnly ?? false;
            ViewBag.level = GetPriceLevel();
            List<string> results;
            ViewBag.duties = GetMenuDuties("/SystemProduct/Index", out results);
            ViewBag.editCategories = results.Contains("EditCategories");
            return View();
        }

        public ActionResult ProductPriceSetting()
        {
            var setting = PriceSettingOperation.GetSettings();
            ViewBag.count = setting[0].value;
            ViewBag.enable = setting[1].enable;
            ViewBag.value = setting[1].value;
            return View();
        }

        public ActionResult ShowProductInfoEditView(int? id)
        {
            ProductFullInfo product = (id == null) ? ProductOperation.RegisterNewProduct() : ProductOperation.GetProductById((int)id);
            ViewBag.isAdd = (id == null) ? "true" : "false";
            return View(product);
        }

        public ActionResult ShowProductInfo(int id)
        {
            ProductFullInfo product = ProductOperation.GetProductById(id);
            ViewBag.categoryText = CategoryOperation.GetCategoryTextById(product.product.category);
            ViewBag.priceLevel = GetPriceLevel();
            return View(product);
        }

        public ActionResult ShowFileUpload()
        {
            ViewBag.id = GetAccountId();
            return View("FileUpload");
        }

        #region 查询

        [HttpPost]
        public JsonResult GetCategories(string id)
        {
            int trueId = id == null ? 0 : int.Parse(id);
            List<CategoryTreeList> treelist = CategoryOperation.GetAllCategories(trueId);
            return Json(treelist);
        }

        [HttpPost]
        public JsonResult GetProcutList(GridPager pager, string condition, string query, List<int> list)
        {
            int priceLevel = GetPriceLevel() == 0 ? 1 : GetPriceLevel();
            GridRows<ProductShowInfo> result = ProductOperation.GetProductList(priceLevel, pager, condition, query, list);
            return Json(result);
        }

        #endregion 查询

        #region 修改

        [HttpPost]
        public JsonResult EditCategoryText(int id, string text)
        {
            ResultView result = new ResultView { result = CategoryOperation.EditCategoryText(id, text) };
            return Json(result);
        }

        [HttpPost]
        public JsonResult MoveCategory(int id, int newParent)
        {
            ResultView result = new ResultView { result = CategoryOperation.MoveCategory(id, newParent) };
            return Json(result);
        }

        [HttpPost]
        public void UploadAvatar(HttpPostedFileBase file)
        {
            string id = Request["imgId"];
            string src = Uploader.UploadSingleFile(file, id, Uploader.UploaderType.Product);
            ProductOperation.EditUserAvatar(int.Parse(id), src);
        }

        [HttpPost]
        public JsonResult EditProductInfo(ProductFullInfo product)
        {
            ResultView result = new ResultView { result = ProductOperation.EditProductInfo(product) };
            return Json(result);
        }

        [HttpPost]
        public JsonResult EditProductListCategory(List<int> idList, int category)
        {
            ResultView result = new ResultView { result = ProductOperation.EditProductListCategory(idList, category) };
            return Json(result);
        }

        [HttpPost]
        public void UploadExcelFile(HttpPostedFileBase file)
        {
            string id = Request["fileId"];
            string src = Uploader.UploadSingleFile(file, id, Uploader.UploaderType.Products);
            int rightCount = ExcelOperation.CheckImportExcelFile(Uploader.GetWebAbsolutePath(), src.Substring(1));
            Uploader.DeleteSingeFile(id, Uploader.UploaderType.Products);
            Response.Write(rightCount);
        }

        [HttpPost]
        public JsonResult EditPriceSettings(int count, bool enable, double value)
        {
            ResultView result = new ResultView { result = PriceSettingOperation.EditSetting(count, enable, value) };
            return Json(result);
        }

        #endregion 修改

        #region 添加

        [HttpPost]
        public JsonResult AddCategory(int parentId, string text)
        {
            int id = CategoryOperation.AddCategory(parentId, text);
            return Json(id);
        }

        #endregion 添加

        #region 删除

        [HttpPost]
        public JsonResult DeleteCategories(int id)
        {
            ResultView result = new ResultView { result = CategoryOperation.DeleteCategories(id) };
            if (!result.result)
                result.msg = "有产品在要删除的分类中，无法删除";
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            ResultView result = new ResultView { result = ProductOperation.DeleteProduct(id) };
            if (result.result)
            {
                Uploader.DeleteSingeFile(id.ToString(), Uploader.UploaderType.Product);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteProductList(List<int> idList)
        {
            ResultView result = new ResultView { result = ProductOperation.DeleteProductList(idList) };
            if (result.result)
            {
                Uploader.DeleteMultipleFiles(idList, Uploader.UploaderType.Product);
            }
            return Json(result);
        }

        #endregion 删除
    }
}
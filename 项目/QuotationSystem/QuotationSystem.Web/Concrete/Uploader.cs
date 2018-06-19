using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuotationSystem.Web.Concrete
{
    public class Uploader
    {
        public enum UploaderType
        {
            User,
            Product,
            Products
        };

        private static string baseAvatarPath = "Upload/Users";
        private static string baseProductPath = "Upload/Products";
        private static string baseProductExcelPath = "Upload/ProdcutExcels";

        public static string UploadSingleFile(HttpPostedFileBase file, string id, UploaderType type)
        {
            string relativePath = (type == UploaderType.User ? baseAvatarPath : (type == UploaderType.Product ? baseProductPath : baseProductExcelPath));
            string absolutePath = getAbsolutePath(type, relativePath);

            DeleteSingeFile(id, absolutePath);

            string fileName = id + Path.GetExtension(file.FileName);
            file.SaveAs(Path.Combine(absolutePath, fileName));
            return "/" + relativePath + "/" + fileName;
        }

        public static string GetWebAbsolutePath()
        {
            return HttpRuntime.AppDomainAppPath;
        }

        private static string getAbsolutePath(UploaderType type, string relativePath)
        {
            string absolutePath = HttpRuntime.AppDomainAppPath + relativePath;
            if (!Directory.Exists(absolutePath))
            {
                Directory.CreateDirectory(absolutePath);
            }
            return absolutePath;
        }

        #region 删除

        private static void DeleteSingeFile(string id, string absolutePath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(absolutePath);
            var files = dirInfo.GetFiles(id + ".*", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                file.Delete();
            }
        }

        public static void DeleteSingeFile(string id, UploaderType type)
        {
            string relativePath = (type == UploaderType.User ? baseAvatarPath : (type == UploaderType.Product ? baseProductPath : baseProductExcelPath));
            string absolutePath = getAbsolutePath(type, relativePath);
            DeleteSingeFile(id, absolutePath);
        }

        public static void DeleteMultipleFiles(List<int> idList, UploaderType type)
        {
            foreach (var id in idList)
                DeleteSingeFile(id.ToString(), type);
        }

        #endregion 删除
    }
}
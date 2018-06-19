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
    public class CategoryOperation
    {
        #region 查询

        public static List<CategoryTreeList> GetAllCategories(int parentId)
        {
            DbOperator dbOperator = new DbOperator();
            List<CategoryTreeList> treelist = GetCategoriesByParentId(dbOperator, parentId);
            return treelist;
        }

        private static List<CategoryTreeList> GetCategoriesByParentId(DbOperator dbOperator, int parentId)
        {
            List<Category> categories = dbOperator.categroies.Where(model => model.parentId == parentId).ToList();
            List<CategoryTreeList> treeList = new List<CategoryTreeList>();
            foreach (var category in categories)
            {
                CategoryTreeList treeNode = new CategoryTreeList { id = category.id, text = category.text };
                if (!category.isLast)
                    treeNode.children = GetCategoriesByParentId(dbOperator, category.id);
                treeList.Add(treeNode);
            }
            return treeList;
        }

        public static string GetCategoryTextById(int id)
        {
            DbOperator dbOperator = new DbOperator();
            string text = dbOperator.categroies.FirstOrDefault(model => model.id == id).text;
            return text;
        }

        public static int GetCategoryIdByText(string text)
        {
            DbOperator dbOperator = new DbOperator();
            Category category = dbOperator.categroies.Where(model => model.text == text).FirstOrDefault();
            if (category != null)
                return category.id;
            return AddCategory(0, text);
        }

        #endregion 查询

        #region 修改

        public static bool EditCategoryText(int id, string text)
        {
            DbOperator dbOperator = new DbOperator();
            Category category = dbOperator.categroies.Where(model => model.id == id).FirstOrDefault();
            if (category != null)
            {
                category.text = text;
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 节点移动操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newParent"></param>
        /// <returns></returns>
        public static bool MoveCategory(int id, int newParent)
        {
            DbOperator dbOperator = new DbOperator();
            Category current = dbOperator.categroies.Where(model => model.id == id).FirstOrDefault();
            int oldParent = current.parentId;
            if (current != null)
            {
                current.parentId = newParent;
                EditParentState(dbOperator, oldParent, false);
                EditParentState(dbOperator, newParent, true);
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改父节点的状态
        /// </summary>
        /// <param name="dbOperator"></param>
        /// <param name="parentId"></param>
        /// <param name="isAdd">是否进行添加操作</param>
        private static void EditParentState(DbOperator dbOperator, int parentId, bool isAdd)
        {
            Category category = dbOperator.categroies.Where(model => model.id == parentId).FirstOrDefault();
            if (category != null)
            {
                if (isAdd)
                {
                    category.isLast = false;
                }
                else
                {
                    int childrenCount = dbOperator.categroies.Where(model => model.parentId == parentId).Count();
                    if (childrenCount <= 1)
                    {
                        category.isLast = true;
                    }
                }
            }
        }

        #endregion 修改

        #region 添加

        public static int AddCategory(int parentId, string text)
        {
            DbOperator dbOperator = new DbOperator();
            EditParentState(dbOperator, parentId, true);
            Category newCategory = dbOperator.categroies.Add(new Category { text = text, parentId = parentId, isLast = true });
            dbOperator.SaveChanges();
            return newCategory.id;
        }

        #endregion 添加

        #region 删除

        public static bool DeleteCategories(int id)
        {
            DbOperator dbOperator = new DbOperator();

            List<int> idList = GetCatetoryIdList(dbOperator, id);
            idList.Add(id);

            //判断是否可以删除
            if (!CanBeDelete(dbOperator, idList))
                return false;

            //修改父节点状态
            Category current = dbOperator.categroies.Where(model => model.id == id).FirstOrDefault();
            EditParentState(dbOperator, current.parentId, false);

            //删除所有包括本身在内的子节点
            IEnumerable<Category> needDelete = dbOperator.categroies.Where(model => idList.Contains(model.id));
            dbOperator.categroies.RemoveRange(needDelete);
            dbOperator.SaveChanges();
            return true;
        }

        private static List<int> GetCatetoryIdList(DbOperator dbOperator, int id)
        {
            List<int> list = dbOperator.categroies.Where(model => model.parentId == id).Select(model => model.id).ToList();
            if (list.Count != 0)
            {
                List<int> children = new List<int>();
                foreach (var current in list)
                {
                    children.Add(current);
                    List<int> grandchildren = GetCatetoryIdList(dbOperator, current);
                    if (grandchildren.Count != 0)
                        children.AddRange(grandchildren);
                }
                return children;
            }
            return new List<int>();
        }

        private static bool CanBeDelete(DbOperator dbOperator, List<int> idList)
        {
            return dbOperator.products.Where(model => idList.Contains(model.category)).Count() > 0 ? false : true;
        }

        #endregion 删除
    }
}
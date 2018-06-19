using QuotationSystem.BLL.Encrypt;
using QuotationSystem.DAL.Common;
using QuotationSystem.DAL.Concrete;
using QuotationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuotationSystem.BLL.Account
{
    public class AccountOperation
    {
        #region 查询

        public static User GetUserInfo(int id)
        {
            DbOperator dbOperator = new DbOperator();
            User user = dbOperator.users.FirstOrDefault(model => model.id == id);
            return user;
        }

        public static GridRows<UserInfo> GetList(GridPager pager, string condition, string query)
        {
            DbOperator dbOperator = new DbOperator();
            GridRows<UserInfo> result = new GridRows<UserInfo>();
            IEnumerable<User> list = dbOperator.users;

            //获取满足条件的总数
            if (!string.IsNullOrEmpty(query))
            {
                if (condition == "id")
                    list = list.Where(model => model.id.ToString().Contains(query));
                else if (condition == "name")
                    list = list.Where(model => model.name.Contains(query));
                else if (condition == "company")
                    list = list.Where(model => model.company.Contains(query));
                else if (condition == "tel")
                    list = list.Where(model => model.tel.Contains(query));
                else if (condition == "active")
                    list = list.Where(model => model.active.Contains(query));

                result.total = list.Count();
            }
            else
                result.total = list.Count();

            //取得实际返回的数量
            if (pager.order == "desc")
                result.rows = list.OrderBy(model => model.id).Skip((pager.page - 1) * pager.rows).Take(pager.rows).Select(model => new UserInfo { id = model.id, name = model.name, active = model.active, tel = model.tel, company = model.company, level = PriceLevelOperation.GetNameById(model.level) }).ToList();
            else
                result.rows = list.OrderByDescending(model => model.id).Skip((pager.page - 1) * pager.rows).Take(pager.rows).Select(model => new UserInfo { id = model.id, name = model.name, active = model.active, tel = model.tel, company = model.company, level = PriceLevelOperation.GetNameById(model.level) }).ToList();

            return result;
        }

        public static User AccountLogInResult(int id, string password)
        {
            string encrypt = Md5Encrypt.Encrypt(password);
            DbOperator dbOperator = new DbOperator();
            User user = dbOperator.users.SingleOrDefault(model => model.id == id && model.password == encrypt);
            return user;
        }

        #endregion 查询

        #region 修改

        public static bool ResetPassword(int id)
        {
            DbOperator dbOperator = new DbOperator();
            User user = dbOperator.users.FirstOrDefault(model => model.id == id);
            if (user != null)
            {
                user.password = Md5Encrypt.GetInitPwd();
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool EditUserPassword(int id, string password)
        {
            DbOperator dbOperator = new DbOperator();
            User user = dbOperator.users.FirstOrDefault(model => model.id == id);
            if (user != null)
            {
                user.password = Md5Encrypt.Encrypt(password);
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool EditUserAvatar(int id, string path)
        {
            DbOperator dbOperator = new DbOperator();
            User user = dbOperator.users.FirstOrDefault(model => model.id == id);
            if (user != null)
            {
                user.avatarPath = path;
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool EditUserInfo(User user)
        {
            DbOperator dbOperator = new DbOperator();
            User updateUser = dbOperator.users.FirstOrDefault(model => model.id == user.id);
            if (user != null)
            {
                updateUser.name = user.name;
                updateUser.tel = user.tel;
                updateUser.company = user.company;
                updateUser.province = user.province;
                updateUser.city = user.city;
                updateUser.region = user.region;
                updateUser.area = user.area;
                updateUser.level = user.level;
                updateUser.active = user.active;
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool EditUserLevelById(int id, int level)
        {
            DbOperator dbOperator = new DbOperator();
            User user = dbOperator.users.Where(model => model.id == id).FirstOrDefault();
            if (user != null)
            {
                user.level = level;
                dbOperator.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion 修改

        #region 添加

        public static User RegisterNewUser()
        {
            DbOperator dbOperator = new DbOperator();

            User user = new User() { name = "默认", password = Md5Encrypt.GetInitPwd() };
            dbOperator.users.Add(user);
            dbOperator.SaveChanges();
            return user;
        }

        #endregion 添加

        #region 删除

        public static bool DeleteUser(int id)
        {
            DbOperator dbOperator = new DbOperator();
            User user = dbOperator.users.Where(model => model.id == id).FirstOrDefault();
            if (user != null)
            {
                dbOperator.users.Remove(user);
                dbOperator.SaveChanges();
                return true;
            }
            return true;
        }

        #endregion 删除
    }
}
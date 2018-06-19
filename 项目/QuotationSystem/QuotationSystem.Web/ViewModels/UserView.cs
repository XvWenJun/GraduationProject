using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotationSystem.Web.ViewModels
{
    public class UserView
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool remeberMe { get; set; }
    }
}
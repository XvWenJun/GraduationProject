using QuotationSystem.DAL.Models;
using System.Data.Entity;

namespace QuotationSystem.DAL.Concrete
{
    public class DbOperator : DbContext
    {
        public DbOperator() : base("name=DbConnection")
        { }

        public DbSet<Menu> menus { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Area> areas { get; set; }
        public DbSet<Category> categroies { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderdetails { get; set; }
        public DbSet<MenuDuty> menuduties { get; set; }
        public DbSet<LevelRight> levelrights { get; set; }
        public DbSet<LevelRightDuty> levelrightduties { get; set; }
        public DbSet<PriceLevelChangeRule> pricelevelchangerules { get; set; }
        public DbSet<Notice> notices { get; set; }
        public DbSet<Level> levels { get; set; }
        public DbSet<ProductPriceSetting> productpricesettings { get; set; }
        public DbSet<ProductPrice> productprices { get; set; }
    }
}
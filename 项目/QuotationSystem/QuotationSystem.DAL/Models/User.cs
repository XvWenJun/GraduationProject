using System.ComponentModel.DataAnnotations.Schema;

namespace QuotationSystem.DAL.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string password { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public int level { get; set; }
        public string tel { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string area { get; set; }
        public string avatarPath { get; set; }
        public string active { get; set; }
    }
}
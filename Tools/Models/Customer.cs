using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tools.Tools.CustomAttributes;

namespace Tools.Models
{
    //[Display(Name = "جدول العملاء")]
    [GridSetting("جدول العملاء", "Home/Manage", "Home/Delete", "Home/Paging")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "اسم العميل")]
        public string CustName { get; set; }
        [Display(Name = "الوظيفة")]
        public string CustJobTitle { get; set; }
        [Display(Name = "المدينة")]
        public string CustCity { get; set; }
        [Display(Name = "العمر")]
        public string CustAge { get; set; }
        //[Display(Name = "مدير")]
        //public bool IsManager { get; set; }

    }
}

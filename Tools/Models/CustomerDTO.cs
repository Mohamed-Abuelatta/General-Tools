using System.ComponentModel.DataAnnotations;
using Tools.Tools.CustomAttributes;

namespace Tools.Models
{
    //[GridSetting("جدول العملاء", "Home/Manage", "Home/Delete", "Home/Pager")]
    public class CustomerDTO
    {

        public int Id { get; set; }
        [Display(Name = "اسم العميل", Order = 0)]
        public string CustName { get; set; }
        [Display(Name = "الوظيفة", Order = 2)]
        public string CustJobTitle { get; set; }
        [Display(Name = "المدينة", Order = 1)]
        public string CustCity { get; set; }
        [Display(Name = "العمر", Order = 3)]
        public string CustAge { get; set; }
        //[Display(Name = "مدير", Order = 4)]
        //public bool IsManager { get; set; }
    }
}

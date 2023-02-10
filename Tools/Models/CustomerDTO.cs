
using Tools.Tools.CustomAttributes;

namespace Tools.Models
{
    //[GridSetting("جدول العملاء", "Home/Manage", "Home/Delete", "Home/Pager")]
    public class CustomerDTO
    {
        [ColumnSetting(0, "رقم العميل")]
        public int Id { get; set; }
        [ColumnSetting(1, "اسم العميل")]
        public string CustName { get; set; }
        [ColumnSetting(2, "المسمى الوظيفى للعميل")]
        public string CustJobTitle { get; set; }
        [ColumnSetting(4, "هل مدير")]
        public bool IsManager { get; set; }

        [ColumnSetting(4, "عمر العميل")]
        public AgeDTO ageDto { get; set; }
        [ColumnSetting(3, "مدينة تواجد العميل")]
        public CityDTO cityDto { get; set; }
    }
}

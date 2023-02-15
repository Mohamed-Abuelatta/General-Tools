
using Tools.Tools.CustomAttributes;
using static Tools.Tools.CustomAttributes.AttrEnum;

namespace Tools.Models
{
    [GridSetting(
        "جدول العملاء", "Customer",
        "Home/Manage", "Home/Delete", "Home/Paging",
        200, 100, 5, 10,
        PaginationTypeEnum.GetPageByPage, GridTypeEnum.InlineGrid)]
    public class CustomerDTO
    {
        [ColumnSetting(0, "رقم العميل", inputType: InputType.number )]
        public int Id { get; set; }

        [ColumnSetting(1, "اسم العميل", inputType: InputType.text)]
        public string CustName { get; set; }

        [ColumnSetting(1, "اسم العميل", inputType: InputType.ImgPath)]
        public string CustPic { get; set; }

        [ColumnSetting(2, "المسمى الوظيفى للعميل", inputType: InputType.text)]
        public string CustJobTitle { get; set; }

        [ColumnSetting(4, "هل مدير", inputType: InputType.checkbox)]
        public bool IsManager { get; set; }

        [ColumnSetting(5, "عمر العميل", inputType: InputType.number)]
        public int Age { get; set; }

        [ColumnSetting(3, "مدينة تواجد العميل", inputType: InputType.dropDownList)]
        public CityDTO city { get; set; }

    }
}

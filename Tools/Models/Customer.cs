using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tools.Tools.CustomAttributes;
using static Tools.Tools.CustomAttributes.AttrEnum;

namespace Tools.Models
{

    [GridSetting(
        "جدول العملاء", "Customer", 
        "Home/Manage", "Home/Delete", "Home/Paging", 
        200, 100, 5, 10, 
        PaginationTypeEnum.GetPageByPage, GridTypeEnum.InlineGrid)]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string CustName { get; set; }

        public string CustPic { get; set; }

        public string CustJobTitle { get; set; }

        public bool IsManager { get; set; }

        public int age { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City city { get; set; }


    }
}

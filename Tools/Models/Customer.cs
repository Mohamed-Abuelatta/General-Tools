﻿using System.ComponentModel.DataAnnotations;

using Tools.Tools.CustomAttributes;
using static Tools.Tools.CustomAttributes.AttrEnum;

namespace Tools.Models
{

    [GridSetting(
        "جدول العملاء", 
        "Home/Manage", "Home/Delete", "Home/Paging", 
        200, 100, 5, 10, 
        PaginationTypeEnum.GetPageByPage, GridTypeEnum.InlineGrid)]
    public class Customer
    {
        [Key]
        [ColumnSetting(0 ,"رقم العميل")]
        public int Id { get; set; }

        [ColumnSetting(1, "اسم العميل")]
        public string CustName { get; set; }

        [ColumnSetting(2, "المسمى الوظيفى للعميل")]
        public string CustJobTitle { get; set; }

        [ColumnSetting(3, "مدينة تواجد العميل")]
        public string CustCity { get; set; }

        [ColumnSetting(4, "عمر العميل")]
        public string CustAge { get; set; }
        //[Display(Name = "مدير")]
        //public bool IsManager { get; set; }

    }
}

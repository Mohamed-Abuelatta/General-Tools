using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tools.Tools.CustomAttributes;

namespace Tools.Models
{
    //[GridSetting("جدول العملاء", "Home/Manage", "Home/Delete", "Home/Pager")]
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string CustName { get; set; }
        public string CustJobTitle { get; set; }
        public bool IsManager { get; set; }

        public int AgeId { get; set; }
        public int CityId { get; set; }

        public AgeDTO age { get; set; }
        public CityDTO city { get; set; }
    }
}

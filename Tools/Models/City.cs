using System.ComponentModel.DataAnnotations;
using Tools.Tools.CustomAttributes;

namespace Tools.Models
{
    public class City
    {
        [Key]
        [ColumnSetting(0, "رقم المدينة")]
        public int Id { get; set; }

        public string CityName { get; set; }

        public List<Customer> customers { get; set; }
    }
}

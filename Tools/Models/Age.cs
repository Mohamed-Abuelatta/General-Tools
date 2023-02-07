using System.ComponentModel.DataAnnotations;
using Tools.Tools.CustomAttributes;

namespace Tools.Models
{
    public class Age
    {
        [Key]
        [ColumnSetting(0, "رقم العمر")]
        public int Id { get; set; }

        [ColumnSetting(1, "العمر")]
        public int AgeName { get; set; }

        public List<Customer> customers { get; set; }
    }
}

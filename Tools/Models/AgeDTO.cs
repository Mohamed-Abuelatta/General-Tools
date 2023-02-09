using System.ComponentModel.DataAnnotations;
using Tools.Tools.CustomAttributes;

namespace Tools.Models
{
    public class AgeDTO
    {
        public int Id { get; set; }
        public int AgeName { get; set; }
        public List<CustomerDTO> customerDTO { get; set; }
    }
}

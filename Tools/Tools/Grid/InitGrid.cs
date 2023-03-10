using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Newtonsoft.Json.Linq;
using Tools.Tools.CustomAttributes;

namespace Tools.Tools.Grid
{
    public class InitGrid
    {
        public GridSetting grid { get; set; }
        public List<ColumnSetting> columns { get; set; }
        public dynamic rows { get; set; }
        public Footer footer { get; set; }
        public JObject enums { get; set; } 
        public JObject ddls { get; set; } 
    }
}

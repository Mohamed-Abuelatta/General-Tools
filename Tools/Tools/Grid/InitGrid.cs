using System.Data;
using Tools.Tools.CustomAttributes;

namespace Tools.Tools.Grid
{
    public class InitGrid
    {
        public GridSetting grid { get; set; }
        public List<ColumnSetting> columns { get; set; }
        public object rows { get; set; }
        public Footer Footer { get; set; }
    }
}

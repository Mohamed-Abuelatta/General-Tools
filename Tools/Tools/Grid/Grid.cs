using System.Data;
using Tools.Tools.CustomAttributes;

namespace Tools.Tools.Grid
{
    public class Grid
    {
        public GridSetting grid { get; set; }
        public List<ColumnSetting> columns { get; set; }
        public DataTable rows { get; set; }
        public string Footer { get; set; }
    }
}

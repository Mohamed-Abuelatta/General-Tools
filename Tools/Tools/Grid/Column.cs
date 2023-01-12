
namespace Tools.Tools.Grid
{
    public class Column
    {
        public string ColName { get; set; }
        public int ColIndex { get; set; }
        public string ColDName { get; set; }
        public int ColWidth { get; set; } = 200;
        public bool IsVisable { get; set; } = false;

        public inputType InputType { get; set; } = inputType.text;
        public keyType KeyType { get; set; } = keyType.Normal;
    }

    public enum inputType
    {
        text,
        textaria,
        wysiwyg,
        link,
        dropdownList,
        number,
        date,
        checkbox,
        ImgPath,
        file,
        chart,
        html,
        conditional // function
    }
    public enum keyType
    {
        PK,
        FK,
        Normal
    }
}

using static Tools.Tools.CustomAttributes.AttrEnum;

namespace Tools.Tools.CustomAttributes
{
    // https://www.pluralsight.com/guides/how-to-create-custom-attributes-csharp

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class ColumnSetting : Attribute
    {

        public ColumnSetting( int ColIndex = 0, string ColTitle = "", string ColName = "", int ColWidth = 200, bool IsVisable = true, inputType InputType = inputType.text, keyType KeyType = keyType.Normal)
        {
            this.ColName = ColName;
            this.ColTitle = ColTitle;
            this.ColIndex = ColIndex;
            this.IsVisable = IsVisable;
            this.InputType = InputType;
            this.KeyType = KeyType;
        }

        public string ColName { get; set; }
        public string ColTitle { get; set; }
        public int ColIndex { get; set; }
        public int ColWidth { get; set; } = 200;
        public bool IsVisable { get; set; } = false;

        public inputType InputType { get; set; } = inputType.text;
        public keyType KeyType { get; set; } = keyType.Normal;

 
    }
}

using static Tools.Tools.CustomAttributes.AttrEnum;

namespace Tools.Tools.CustomAttributes
{
    // https://www.pluralsight.com/guides/how-to-create-custom-attributes-csharp

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class ColumnSetting : Attribute
    {

        public ColumnSetting( int ColIndex = 0, string ColTitle = "", string ColName = "", int ColWidth = 200, bool IsVisable = true, string InputType = "text", string KeyType = "Normal")
        {
            this.ColName = ColName;
            this.ColTitle = ColTitle;
            this.ColIndex = ColIndex;
            this.IsVisable = IsVisable;
            this.InputType = InputType;
            this.KeyType = KeyType;
            this.ColWidth = ColWidth;
        }

        public string ColName { get; set; }
        public string ColTitle { get; set; }
        public int ColIndex { get; set; }
        public int ColWidth { get; set; } = 200;
        public bool IsVisable { get; set; } = false;

        public string InputType { get; set; } = Enum.GetName(inputType.text);
        public string KeyType { get; set; } = Enum.GetName(keyType.Normal);

 
    }
}




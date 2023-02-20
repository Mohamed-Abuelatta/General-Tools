using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static Tools.Tools.CustomAttributes.AttrEnum;
using InputType = Tools.Tools.CustomAttributes.AttrEnum.InputType;

namespace Tools.Tools.CustomAttributes
{
    // https://www.pluralsight.com/guides/how-to-create-custom-attributes-csharp

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class ColumnSetting : Attribute
    {

        public ColumnSetting(int ColIndex = 0, string ColTitle = "", string ColName = "",
            int ColWidth = 200, bool IsVisible = true, InputType inputType = InputType.text, KeyType keyType = KeyType.Normal)
        {
            this.ColName = ColName;
            this.ColTitle = ColTitle;
            this.ColIndex = ColIndex;
            this.inputType = inputType;
            this.keyType = keyType;
            this.ColWidth = ColWidth;
            this.isvisible = IsVisible;
        }

        public string ColName { get; set; }
        public string ColTitle { get; set; }
        public int ColIndex { get; set; }
        public int ColWidth { get; set; }

        public bool isvisible { get; set; } 
        public InputType inputType { get; set; }
        public KeyType keyType { get; set; } 

 
    }
}




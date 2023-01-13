using static Tools.Tools.CustomAttributes.AttrEnum;

namespace Tools.Tools.CustomAttributes
{
    // https://www.pluralsight.com/guides/how-to-create-custom-attributes-csharp

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class GridSetting : Attribute
    {

        public GridSetting(string GridTitle, string OnSaveAction, string OnDeleteAction, string OnPagingAction, 
            int DefaultColumnWidth, int DefaultCtrlColumnWidth, int PagerSize, int ItemsPerPage, 
            PaginationTypeEnum PaginationType, GridTypeEnum GridType)
        {
            this.GridTitle = GridTitle;
            this.OnSaveAction = OnSaveAction;
            this.OnDeleteAction = OnDeleteAction;
            this.OnPagingAction = OnPagingAction;
            this.DefaultColumnWidth = DefaultColumnWidth;
            this.DefaultCtrlColumnWidth = DefaultCtrlColumnWidth;
            this.PagerSize = PagerSize;
            this.ItemsPerPage = ItemsPerPage;
            this.PaginationType = PaginationType;
            this.GridType = GridType;
        }

        public string GridTitle { get; set; }
        public int ItemsPerPage { get; set; }
        public int PagerSize { get; set; }
        public int DefaultColumnWidth { get; set; }
        public int DefaultCtrlColumnWidth { get; set; }
        public GridTypeEnum GridType { get; set; }
        public PaginationTypeEnum PaginationType { get; set; }

        public string OnSaveAction { get; set; }
        public string OnDeleteAction { get; set; }
        public string OnPagingAction { get; set; }



    }
}

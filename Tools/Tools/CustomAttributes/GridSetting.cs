namespace Tools.Tools.CustomAttributes
{
    // https://www.pluralsight.com/guides/how-to-create-custom-attributes-csharp

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class GridSetting : Attribute
    {
        public GridSetting(GridSetting gridSetting)
        {
            GridTitle = gridSetting.GridTitle;
            ItemsPerPage = gridSetting.ItemsPerPage;
            PagerSize = gridSetting.PagerSize;
            DefaultColumnWidth = gridSetting.DefaultColumnWidth;
            DefaultCtrlColumnWidth = gridSetting.DefaultCtrlColumnWidth;
            GridType = gridSetting.GridType;
            PaginationType = gridSetting.PaginationType;

            OnSaveAction = gridSetting.OnSaveAction;
            OnDeleteAction = gridSetting.OnDeleteAction;
            OnPagingAction = gridSetting.OnPagingAction;
        }

        public string GridTitle { get; set; }
        public int ItemsPerPage { get; set; }
        public int PagerSize { get; set; }
        public int DefaultColumnWidth { get; set; }
        public int DefaultCtrlColumnWidth { get; set; }
        public GridTypeEnum GridType { get; set; }
        public PaginationTypeEnum PaginationType { get; set; }

        public bool AllowAdd { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }

        public string OnSaveAction { get; set; }
        public string OnDeleteAction { get; set; }
        public string OnPagingAction { get; set; }

        public enum GridTypeEnum
        {
            InlineGrid,
            PopUpGrid,
            FormAndList
        }
        public enum PaginationTypeEnum
        {
            GetPageByPage,
            GetAll
        }

    }
}

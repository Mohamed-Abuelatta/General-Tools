namespace Tools.Tools.CustomAttributes
{
    // https://www.pluralsight.com/guides/how-to-create-custom-attributes-csharp

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class GridSetting : Attribute
    {
        public GridSetting(string gridTitle, string onSaveAction, string onDeleteAction, string onPagingAction,
            GridTypeEnum gridType = GridTypeEnum.InlineGrid, PaginationTypeEnum paginationType = PaginationTypeEnum.GetPageByPage,
            int defaultColumnWidth = 200, int defaultCtrlColumnWidth = 100, int itemsPerPage = 10, int pagerSize = 5)
        {
            GridTitle = gridTitle;
            ItemsPerPage = itemsPerPage;
            PagerSize = pagerSize;
            DefaultColumnWidth = defaultColumnWidth;
            DefaultCtrlColumnWidth = defaultCtrlColumnWidth;
            GridType = gridType;
            PaginationType = paginationType;

            OnSaveAction = onSaveAction;
            OnDeleteAction = onDeleteAction;
            OnPagingAction = onPagingAction;
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

namespace Tools.Tools.CustomAttributes
{
    public class AttrEnum
    {

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
        public enum hideClass
        {
            msg,
            noHide,
            pk,
            hide
        }
        public enum keyType
        {
            PK,
            FK,
            msg,
            ctrl,
            Normal
        }

    }
}

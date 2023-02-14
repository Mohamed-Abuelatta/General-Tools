using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tools.Tools.CustomAttributes
{
    public class AttrEnum
    {

        //this.GetType().GetProperties().Where(w => w.ReflectedType == typeof(Enum));
        [JsonConverter(typeof(StringEnumConverter))]
        public enum GridTypeEnum
        {
            InlineGrid,
            PopUpGrid,
            FormAndList
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PaginationTypeEnum
        {
            GetPageByPage,
            GetAll
        }

        public enum InputType
        {
            text,
            textaria,
            wysiwyg,
            link,
            dropDownList,
            number,
            date,
            checkbox,
            ImgPath,
            file,
            chart,
            html,
            conditional // function
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public enum HiddenClass
        {
            msg,
            noHide,
            pk,
            hide
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public enum KeyType
        {
            PK,
            FK,
            msg,
            ctrl,
            Normal
        }

    }
}

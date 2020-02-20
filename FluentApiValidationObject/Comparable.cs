using Newtonsoft.Json;

namespace FluentApiValidationObject
{
    public class Comparable : ComparableBase
    {

        public Comparable(object obj, object toBeCompared, ComparableBase _backLevel = null)
            : base(obj, toBeCompared, _backLevel)
        {

        }

        public Comparable AddField(string field)
        {
            _fields.Add(field);
            return this;
        }

        public Comparable ThenAddInclude(string field)
        {
            var comparable = new Comparable(((dynamic)JsonConvert.DeserializeObject(_obj.ToString()))[field], ((dynamic)JsonConvert.DeserializeObject(_toBeCompared.ToString()))[field], this);
            _chidrenLevel.Add(comparable);
            return comparable;
        }

        public ListComparable AddListField(string field)
        {
            var list = new ListComparable(((dynamic)JsonConvert.DeserializeObject(_obj.ToString()))[field], ((dynamic)JsonConvert.DeserializeObject(_toBeCompared.ToString()))[field], this);
            _chidrenLevel.Add(list);
            return list;
        }
    }
}

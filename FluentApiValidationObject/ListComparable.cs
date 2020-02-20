using System;
using System.Collections.Generic;

namespace FluentApiValidationObject
{
    public class ListComparable : ComparableBase
    {
        public ListComparable(object obj, object toBeCompared, ComparableBase _backLevel = null) 
            : base(obj, toBeCompared, _backLevel)
        {

        }

        public Comparable AddToPosition(int postion)
        {
            var position = new Comparable(((dynamic)_obj)[postion], ((dynamic)_toBeCompared)[postion], this);
            _chidrenLevel.Add(position);
            return position;
        }
    }
}

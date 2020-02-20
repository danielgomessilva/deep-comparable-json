using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentApiValidationObject
{
    public abstract class ComparableBase
    {
        public ComparableBase _backLevel;
        protected readonly List<ComparableBase> _chidrenLevel;
        protected readonly object _obj;
        protected readonly object _toBeCompared;
        protected readonly List<string> _fields;

        public ComparableBase(dynamic obj, dynamic toBeCompared, ComparableBase backLevel = null)
        {
            _obj = obj;
            _toBeCompared = toBeCompared;
            _fields = new List<string>();
            _backLevel = backLevel;
            _chidrenLevel = new List<ComparableBase>();
        }
        public T BackLevel<T>() where T : ComparableBase
        {
            if (_backLevel == null)
                throw new ApplicationException("There is no back level");
            return (T)_backLevel;
        }
        public bool IsEquivalent { get => CheckIfIsEquivalent(); }

        private bool CheckIfIsEquivalent()
        {
            if (_backLevel == null)
            {
                foreach (var field in _fields)
                {
                    if (((dynamic)JsonConvert.DeserializeObject(_obj.ToString()))[field] != ((dynamic)JsonConvert.DeserializeObject(_toBeCompared.ToString()))[field])
                        return false;
                }
                foreach (var children in _chidrenLevel)
                {
                    children._backLevel = null;
                    var isEquivalent = children.IsEquivalent;
                    if (!isEquivalent)
                        return false;
                }
                return true;
            }
            else
                return _backLevel.IsEquivalent;
            
        }
    }
}

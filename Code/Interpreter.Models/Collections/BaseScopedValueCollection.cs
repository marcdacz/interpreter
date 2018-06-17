using System.Collections.Generic;

namespace Interpreter.Models.Collections
{
    public class BaseScopedValueCollection<T>
    {
        private Dictionary<ScopedValue, T> _scopedValueDictionary;

        public Scopes Scopes { get; private set; }

        public BaseScopedValueCollection()
        {
            _scopedValueDictionary = new Dictionary<ScopedValue, T>(new ScopedValueComparer());
            Scopes = new Scopes();
        }

        public virtual void Add(string name, T value)
        {
            var key = GetScopeKeyOf(name);
            if (_scopedValueDictionary.ContainsKey(key))
            {
                _scopedValueDictionary[key] = value;
            }
            else
            {
                _scopedValueDictionary.Add(key, value);
            }
        }

        public void Clear()
        {
            _scopedValueDictionary.Clear();
        }

        public bool Contains(string name)
        {
            return _scopedValueDictionary.ContainsKey(GetScopeKeyOf(name));
        }

        public int Count()
        {
            return _scopedValueDictionary.Count;
        }

        public int CountCurrentScopeKeys()
        {
            return GetCurrentScopeKeys().Count;
        }

        private List<ScopedValue> GetCurrentScopeKeys()
        {
            var currentKeys = new List<ScopedValue>();
            foreach (var keyValuePair in _scopedValueDictionary)
            {
                if (keyValuePair.Key.Scope == Scopes.PeekCurrent())
                {
                    currentKeys.Add(keyValuePair.Key);
                }
            }
            return currentKeys;
        }

        public virtual ScopedValue GetScopeKeyOf(string value)
        {
            return new ScopedValue(Scopes.PeekCurrent(), value.ToLowerInvariant());
        }

        public virtual T GetValue(string name)
        {
            _scopedValueDictionary.TryGetValue(GetScopeKeyOf(name), out T value);
            return value;
        }

        public void Remove(string name)
        {
            _scopedValueDictionary.Remove(GetScopeKeyOf(name));
        }

        public void RemoveCurrentScopedValues()
        {
            var currentScopeKeys = GetCurrentScopeKeys();
            foreach (var key in currentScopeKeys)
            {
                _scopedValueDictionary.Remove(key);
            }
            Scopes.PopCurrent();
        }
    }
}

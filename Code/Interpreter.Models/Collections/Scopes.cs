using System.Collections.Generic;

namespace Interpreter.Models.Collections
{
    public class Scopes
    {
        private Stack<string> _scopes;

        public Scopes()
        {
            _scopes = new Stack<string>();
        }

        public void SetCurrent(string scope)
        {
            _scopes.Push(scope.ToLowerInvariant());
        }

        public string PeekCurrent()
        {
            return _scopes.Peek();
        }

        public void PopCurrent()
        {
            _scopes.Pop();
        }

        public void Clear()
        {
            _scopes.Clear();
        }

        public int Count()
        {
            return _scopes.Count;
        }
    }
}

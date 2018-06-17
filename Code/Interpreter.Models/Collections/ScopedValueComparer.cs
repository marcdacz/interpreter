using System.Collections.Generic;

namespace Interpreter.Models.Collections
{
    public class ScopedValueComparer : IEqualityComparer<ScopedValue>
    {
        public bool Equals(ScopedValue x, ScopedValue y)
        {
            return x.Scope == y.Scope && x.Value == y.Value;
        }

        public int GetHashCode(ScopedValue scopedValue)
        {
            return scopedValue.Scope.GetHashCode() + scopedValue.Value.GetHashCode();
        }
    }
}

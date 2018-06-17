namespace Interpreter.Models.Collections
{
    public class ScopedValue
    {
        public string Scope { get; private set; }

        public string Value { get; private set; }

        public ScopedValue(string scope, string value)
        {
            this.Scope = scope.ToLowerInvariant();
            this.Value = value.ToLowerInvariant();
        }
    }
}

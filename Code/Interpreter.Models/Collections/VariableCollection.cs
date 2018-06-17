using System;

namespace Interpreter.Models.Collections
{
    public class VariableCollection : BaseScopedValueCollection<object>
    {
        private VariableTypes GetVariableType(string name)
        {
            VariableTypes type = VariableTypes.Unknown;
            if (name.StartsWith("local", StringComparison.InvariantCultureIgnoreCase))
            {
                type = VariableTypes.Local;
            }
            else if (name.StartsWith("global", StringComparison.InvariantCultureIgnoreCase))
            {
                type = VariableTypes.Global;
            }
            else if (name.StartsWith("sys", StringComparison.InvariantCultureIgnoreCase))
            {
                type = VariableTypes.System;
            }
            return type;
        }

        public override ScopedValue GetScopeKeyOf(string name)
        {
            ScopedValue variableKey = null;
            if (GetVariableType(name) == VariableTypes.Local)
            {
                if (Scopes.Count() > 0)
                {
                    variableKey = new ScopedValue(Scopes.PeekCurrent(), name);
                }
            }
            else
            {
                variableKey = new ScopedValue("global", name);
            }
            return variableKey;
        }

        public override void Add(string name, object value)
        {
            var variableType = GetVariableType(name);
            if (variableType != VariableTypes.Unknown)
            {
                base.Add(name, value);
            }
            else
            {
                throw new FormatException($"Incorrect variable format for {name}. Allowed prefixes: local or global.");
            }
        }
    }
}

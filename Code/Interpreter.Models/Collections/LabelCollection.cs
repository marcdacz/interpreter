using System;
using System.Collections.Generic;

namespace Interpreter.Models.Collections
{
    public class LabelCollection : BaseScopedValueCollection<int>
    {
        public override void Add(string label, int value)
        {
            if (!Contains(label))
            {
                base.Add(label, value);
            }
            else
            {
                throw new ArgumentException($"Label {label} already exists.");
            }
        }

        public override int GetValue(string label)
        {
            int index = -1;
            if (Contains(label))
            {
                index = base.GetValue(label);
            }
            return index;
        }

        public int IndexOf(string label)
        {
            return GetValue(label);
        }
    }
}

using System.Collections.Generic;

namespace Interpreter.Models.Script
{
    public class BaseTestStepList
    {
        private string _name;

        public long? RowNumber { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value.ToLowerInvariant();
            }
        }

        public List<TestStep> TestSteps { get; set; }
    }
}

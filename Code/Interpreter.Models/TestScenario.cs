using System.Collections.Generic;

namespace Interpreter.Models
{
    public class TestScenario
    {
        private string _state;

        public long? RowNumber { get; set; }

        public string State
        {
            get
            {
                if (string.IsNullOrEmpty(_state) || _state == TestScenarioStates.Skip)
                {
                    _state = TestScenarioStates.Skip;
                }
                else
                {
                    _state = TestScenarioStates.Run;
                }
                return _state;
            }

            set
            {
                _state = value.ToLowerInvariant();
            }
        }

        public string Name { get; set; }

        public List<TestStep> TestSteps { get; set; }
    }
}

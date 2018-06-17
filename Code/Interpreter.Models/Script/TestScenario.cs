using System.Collections.Generic;

namespace Interpreter.Models.Script
{
    public class TestScenario: BaseTestStepList
    {
        private string _state;        

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
    }
}

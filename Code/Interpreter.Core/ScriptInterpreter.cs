using Interpreter.Models.Events;
using Interpreter.Models.Script;
using System;

namespace Interpreter.Core
{
    public class ScriptInterpreter
    {
        private TestScript _testScript;

        public ScriptInterpreter(TestScript testScript)
        {
            _testScript = testScript;
        }

        public void ExecuteScript()
        {
            OnEventTriggered(InterpreterEvents.TestScriptStarted);

            foreach (var suite in _testScript.TestSuites)
            {
                OnEventTriggered(InterpreterEvents.TestSuiteStarted, suite.Name);

                foreach (var scenario in suite.TestScenarios)
                {
                    if (scenario.State == TestScenarioStates.Run)
                    {
                        ExecuteScenario(scenario);
                    }
                    else
                    {
                        OnEventTriggered(InterpreterEvents.TestScenarioSkipped, scenario.Name);
                    }                                         
                }

                OnEventTriggered(InterpreterEvents.TestSuiteFinished, suite.Name);
            }

            OnEventTriggered(InterpreterEvents.TestScriptFinished);
        }

        public void ExecuteScenario(TestScenario scenario)
        {
            OnEventTriggered(InterpreterEvents.TestScenarioStarted, scenario.Name);

            for (int i = 0; i < scenario.TestSteps.Count; i++)
            {
                ExecuteStep(scenario.TestSteps[i], ref i);
            }

            OnEventTriggered(InterpreterEvents.TestScenarioFinished, scenario.Name);
        }

        public void ExecuteStep(TestStep step, ref int index)
        {
            OnEventTriggered(InterpreterEvents.TestStepStarted, step.Command);
        }

        public event EventHandler<InterpreterEventArgs> EventTriggered;

        protected virtual void OnEventTriggered(InterpreterEvents interpreterEvent, string message = null)
        {
            EventTriggered?.Invoke(this, new InterpreterEventArgs() { InterpreterEvent = interpreterEvent, Message = message });
        }
    }
}

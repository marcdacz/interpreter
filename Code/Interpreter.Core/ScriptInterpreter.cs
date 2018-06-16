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
                    OnEventTriggered(InterpreterEvents.TestScenarioStarted, scenario.Name);

                    foreach (var step in scenario.TestSteps)
                    {
                        OnEventTriggered(InterpreterEvents.TestStepStarted, step.Command);
                    }

                    OnEventTriggered(InterpreterEvents.TestScenarioFinished, scenario.Name);
                }

                OnEventTriggered(InterpreterEvents.TestSuiteFinished, suite.Name);
            }

            OnEventTriggered(InterpreterEvents.TestScriptFinished);
        }

        public event EventHandler<InterpreterEventArgs> EventTriggered;

        protected virtual void OnEventTriggered(InterpreterEvents interpreterEvent, string message = null)
        {
            EventTriggered?.Invoke(this, new InterpreterEventArgs() { InterpreterEvent = interpreterEvent, Message = message });
        }
    }
}

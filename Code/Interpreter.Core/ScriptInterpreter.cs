using Interpreter.Models.Events;
using Interpreter.Models.Script;
using System;
using System.Collections.Generic;

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
                        ExecuteStepsList(scenario);
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

        public void ExecuteStepsList(BaseTestStepList stepsList, bool isFunction = false)
        {
            var events = StepsListEvents(isFunction);

            OnEventTriggered(events[0], stepsList.Name);

            for (int i = 0; i < stepsList.TestSteps.Count; i++)
            {
                ExecuteStep(stepsList.TestSteps[i], ref i);
            }

            OnEventTriggered(events[1], stepsList.Name);
        }

        public List<InterpreterEvents> StepsListEvents(bool isFunction = false)
        {
            var eventsTriggered = new List<InterpreterEvents>();
            if (isFunction)
            {
                eventsTriggered.Add(InterpreterEvents.TestScenarioStarted);
                eventsTriggered.Add(InterpreterEvents.TestScenarioFinished);
            }
            else
            {
                eventsTriggered.Add(InterpreterEvents.TestFunctionStarted);
                eventsTriggered.Add(InterpreterEvents.TestFunctionFinished);
            }
            return eventsTriggered;
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

using FluentAssertions;
using Interpreter.Models.Events;
using Interpreter.Models.Script;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Interpreter.Core.UnitTests
{
    public class TestScriptTests
    {
        private TestScript _testScript;

        public TestScriptTests()
        {
            var testStep1 = new TestStep
            {
                Command = "command_1",
                Parameters = "parameters_1, parameters_2"
            };

            var testStep2 = new TestStep
            {
                Command = "command_2",
                Parameters = "parameters_1, parameters_2"
            };

            var testScenario1 = new TestScenario
            {
                Name = "Test Scenario 1",
                State = TestScenarioStates.Run,
                TestSteps = new List<TestStep>()
            };

            testScenario1.TestSteps.Add(testStep1);
            testScenario1.TestSteps.Add(testStep2);

            var testScenario2 = new TestScenario
            {
                Name = "Test Scenario 2",
                State = TestScenarioStates.Run,
                TestSteps = new List<TestStep>()
            };

            testScenario2.TestSteps.Add(testStep1);
            testScenario2.TestSteps.Add(testStep2);

            var testSuite = new TestSuite
            {
                Name = "Test Suite 1",
                TestScenarios = new List<TestScenario>()
            };

            testSuite.TestScenarios.Add(testScenario1);
            testSuite.TestScenarios.Add(testScenario2);

            var testFunction1 = new TestFunction
            {
                Name = "Test Scenario 1",
                TestSteps = new List<TestStep>()
            };
            
            testFunction1.TestSteps.Add(testStep1);
            testFunction1.TestSteps.Add(testStep2);

            var testFunctionGroup = new TestFunctionGroup
            {
                Name = "Test Suite 1",
                TestFunctions = new List<TestFunction>()
            };

            testFunctionGroup.TestFunctions.Add(testFunction1);

            _testScript = new TestScript
            {
                TestSuites = new List<TestSuite>(),
                TestFunctionGroups = new List<TestFunctionGroup>()
            };

            _testScript.TestSuites.Add(testSuite);
            _testScript.TestFunctionGroups.Add(testFunctionGroup);
        }

        [Fact]
        public void ShouldBeAbleToCreateTestScript()
        {      
            string serializedScript = JsonConvert.SerializeObject(_testScript);
            var deserializedScript = JsonConvert.DeserializeObject<TestScript>(serializedScript);
            deserializedScript.TestSuites.Count.Should().Be(1);
            deserializedScript.TestSuites[0].TestScenarios[0].TestSteps[0].Command.Should().Be("command_1");
        }

        [Fact]
        public void ShouldBeAbleToExecuteTestScript()
        {
            var scriptRunner = new ScriptInterpreter(_testScript);
            var receivedEvents = new List<string>();

            scriptRunner.EventTriggered += delegate (object sender, InterpreterEventArgs e)
            {
                receivedEvents.Add(Enum.GetName(typeof(InterpreterEvents), e.InterpreterEvent));
            }; 
            
            scriptRunner.ExecuteScript();

            receivedEvents.Where(x => x.Contains(Enum.GetName(typeof(InterpreterEvents), InterpreterEvents.TestScriptStarted)))
                .ToList().Count().Should().Be(1);

            receivedEvents.Where(x => x.Contains(Enum.GetName(typeof(InterpreterEvents), InterpreterEvents.TestScriptFinished)))
                .ToList().Count().Should().Be(1);

            receivedEvents.Where(x => x.Contains(Enum.GetName(typeof(InterpreterEvents), InterpreterEvents.TestSuiteStarted)))
                .ToList().Count().Should().Be(1);

            receivedEvents.Where(x => x.Contains(Enum.GetName(typeof(InterpreterEvents), InterpreterEvents.TestStepStarted)))
                .ToList().Count().Should().Be(4);
        }
    }
}

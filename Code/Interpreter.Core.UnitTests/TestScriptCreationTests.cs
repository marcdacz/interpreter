using FluentAssertions;
using Interpreter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace Interpreter.Core.UnitTests
{
    public class TestScriptCreationTests
    {
        [Fact]
        public void ShouldBeAbleToCreateTestScript()
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

            var testScript = new TestScript
            {
                TestSuites = new List<TestSuite>()
            };

            testScript.TestSuites.Add(testSuite);

            string serializedScript = JsonConvert.SerializeObject(testScript);

            var deserializedScript = JsonConvert.DeserializeObject<TestScript>(serializedScript);

            deserializedScript.TestSuites.Count.Should().Be(1);
            deserializedScript.TestSuites[0].TestScenarios[0].TestSteps[0].Command.Should().Be("command_1");
        }
    }
}

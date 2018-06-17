using Interpreter.Models.Script.TestHooks;
using System.Collections.Generic;

namespace Interpreter.Models.Script
{
    public class TestSuite
    {
        public string Name { get; set; }

        public BeforeTestSuite BeforeTestSuite { get; set; }

        public AfterTestSuite AfterTestSuite { get; set; }

        public BeforeTestScenario BeforeTestScenario { get; set; }

        public AfterTestScenario AfterTestScenario { get; set; }      

        public List<TestScenario> TestScenarios { get; set; }
    }
}

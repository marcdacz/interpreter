using System.Collections.Generic;

namespace Interpreter.Models.Script
{
    public class TestSuite
    {
        public string Name { get; set; }

        public List<TestScenario> TestScenarios { get; set; }
    }
}

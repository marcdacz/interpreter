using Interpreter.Models.Script.TestHooks;
using System.Collections.Generic;

namespace Interpreter.Models.Script
{
    public class TestScript
    {
        public string Name { get; set; }

        public BeforeTestScript BeforeTestScript { get; set; }

        public AfterTestScript AfterTestScript { get; set; }

        public List<TestSuite> TestSuites { get; set; }

        public List<TestFunctionGroup> TestFunctionGroups { get; set; }
    }
}

using System.Collections.Generic;

namespace Interpreter.Models.Script
{
    public class TestFunctionGroup
    {
        public string Name { get; set; }       

        public List<TestFunction> TestFunctions { get; set; }
    }
}

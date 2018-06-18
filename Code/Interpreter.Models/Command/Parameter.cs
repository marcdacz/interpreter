using System.Collections.Generic;

namespace Interpreter.Models.Command
{
    public class Parameter
    {
        public string AssignmentVariable { get; set; }

        public List<object> ParametersList { get; set; }
    }
}

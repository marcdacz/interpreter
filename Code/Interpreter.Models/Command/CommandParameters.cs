using System.Collections.Generic;

namespace Interpreter.Models.Command
{
    public class CommandParameters
    {
        public CommandParameters(List<string> parameters, string variable)
        {
            Variable = variable;
            Parameters = parameters;
        }

        public string Variable { get; }
        public List<string> Parameters { get; }
    }
}

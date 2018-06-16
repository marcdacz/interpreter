using System;

namespace Interpreter.Models.Events
{
    public class InterpreterEventArgs : EventArgs
    {
        public InterpreterEvents InterpreterEvent { get; set; }

        public string Message { get; set; }
    }
}

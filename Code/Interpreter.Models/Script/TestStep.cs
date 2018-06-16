namespace Interpreter.Models.Script
{
    public class TestStep
    {
        private string _command;

        public long? RowNumber { get; set; }

        public string Command
        {
            get
            {
                return _command;
            }

            set
            {
                _command = value.ToLowerInvariant();
            }
        }

        public string Parameters { get; set; }
    }
}

using System;

namespace Interpreter.Models.Report
{
    public class BaseReportResult
    {
        public BaseReportResult()
        {
            TestStart = DateTime.Now;
            _result = string.Empty;
        }

        public DateTime TestStart { get; private set; }

        public DateTime TestEnd { get; private set; }

        public TimeSpan ExecutionPeriod { get; private set; }

        private string _result;

        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                TestEnd = DateTime.Now;
                ExecutionPeriod = TestEnd.Subtract(TestStart);
                _result = value;
            }
        }
    }
}

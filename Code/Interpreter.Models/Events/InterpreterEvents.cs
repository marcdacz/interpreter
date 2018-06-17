namespace Interpreter.Models.Events
{
    public enum InterpreterEvents
    {
        TestScriptStarted,
        TestScriptPaused,
        TestScriptFinished,
        TestSuiteStarted,
        TestSuiteSkipped,
        TestSuiteFinished,
        TestScenarioStarted,
        TestScenarioSkipped,
        TestScenarioFinished,
        TestFunctionStarted,
        TestFunctionFinished,
        TestStepStarted,
        TestStepSkipped,
        TestStepFinished       
    }
}

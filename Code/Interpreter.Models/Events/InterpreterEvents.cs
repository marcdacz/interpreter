namespace Interpreter.Models.Events
{
    public enum InterpreterEvents
    {
        TestScriptStarted = 1,
        TestScriptPaused = 2,
        TestScriptFinished = 3,
        TestSuiteStarted = 4,
        TestSuiteSkipped = 5,
        TestSuiteFinished = 6,
        TestScenarioStarted = 7,
        TestScenarioSkipped = 8,
        TestScenarioFinished = 9,
        TestStepStarted = 10,
        TestStepSkipped = 11,
        TestStepFinished = 12       
    }
}

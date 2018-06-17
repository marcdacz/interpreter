using FluentAssertions;
using Interpreter.Models.Collections;
using Xunit;

namespace Interpreter.Core.UnitTests
{
    public class VariablesTests
    {
        private VariableCollection _variables;
        private string _globalVariableName = "globalVar1";
        private int _globalVariableValue = 25;
        private string _localVariableName = "localVar1";
        private string _localVariableValue = "marcDacz";        

        [Fact]
        public void ShouldBeAbleToAddVariables()
        {
            _variables = new VariableCollection();

            _variables.Scopes.SetCurrent("Test Scenario 1");

            _variables.Add(_globalVariableName, _globalVariableValue);
            _variables.Add(_localVariableName, _localVariableValue);

            _variables.Count().Should().Be(2);

            _variables.Contains(_globalVariableName).Should().BeTrue();
            _variables.GetValue(_globalVariableName).Should().Be(_globalVariableValue);

            _variables.Contains(_localVariableName).Should().BeTrue();
            _variables.GetValue(_localVariableName).Should().Be(_localVariableValue);
        }

        [Fact]
        public void ShouldBeAbleToAccessVariablesInCurrentScope()
        {
            _variables = new VariableCollection();

            _variables.Scopes.SetCurrent("Test Scenario 1");

            _variables.Add(_globalVariableName, _globalVariableValue);
            _variables.Add(_localVariableName, _localVariableValue);

            _variables.Count().Should().Be(2);
            _variables.CountCurrentScopeKeys().Should().Be(1);
            _variables.Contains(_globalVariableName).Should().BeTrue();
            _variables.GetValue(_globalVariableName).Should().Be(_globalVariableValue);
            _variables.Contains(_localVariableName).Should().BeTrue();
            _variables.GetValue(_localVariableName).Should().Be(_localVariableValue);

            _variables.Scopes.SetCurrent("Test Scenario 2");

            _variables.Count().Should().Be(2);
            _variables.CountCurrentScopeKeys().Should().Be(0);
            _variables.Contains(_globalVariableName).Should().BeTrue();
            _variables.GetValue(_globalVariableName).Should().Be(_globalVariableValue);
            _variables.Contains(_localVariableName).Should().BeFalse();
            _variables.GetValue(_localVariableName).Should().Be(null);
        }

        [Fact]
        public void ShouldBeAbleToClearVariablesOfCurrentScope()
        {
            _variables = new VariableCollection();

            _variables.Scopes.SetCurrent("Test Scenario 1");

            _variables.Add(_globalVariableName, _globalVariableValue);           

            _variables.Count().Should().Be(1);
            _variables.Contains(_globalVariableName).Should().BeTrue();
            _variables.GetValue(_globalVariableName).Should().Be(_globalVariableValue);

            _variables.Scopes.SetCurrent("Function 1");

            _variables.Add(_localVariableName, _localVariableValue);

            _variables.Count().Should().Be(2);
            _variables.CountCurrentScopeKeys().Should().Be(1);
            _variables.Contains(_globalVariableName).Should().BeTrue();
            _variables.GetValue(_globalVariableName).Should().Be(_globalVariableValue);
            _variables.Contains(_localVariableName).Should().BeTrue();
            _variables.GetValue(_localVariableName).Should().Be(_localVariableValue);

            _variables.RemoveCurrentScopedValues();
            _variables.Count().Should().Be(1);
            _variables.CountCurrentScopeKeys().Should().Be(0);
            _variables.Contains(_globalVariableName).Should().BeTrue();
            _variables.GetValue(_globalVariableName).Should().Be(_globalVariableValue);
            _variables.Contains(_localVariableName).Should().BeFalse();
            _variables.GetValue(_localVariableName).Should().Be(null);
        }
    }
}

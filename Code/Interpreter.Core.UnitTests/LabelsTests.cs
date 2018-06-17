using FluentAssertions;
using Interpreter.Models.Collections;
using Xunit;

namespace Interpreter.Core.UnitTests
{
    public class LabelsTests
    {
        private LabelCollection _labels;
        private string _label1Name = "label1";
        private int _label1Index = 25;
        private string _label2Name = "label2";
        private int _label2Index = 45;

        [Fact]
        public void ShouldBeAbleToAddLabel()
        {
            _labels = new LabelCollection();

            _labels.Scopes.SetCurrent("Test Scenario 1");

            _labels.Add(_label1Name, _label1Index);
            _labels.Add(_label2Name, _label2Index);

            _labels.Count().Should().Be(2);

            _labels.Contains(_label1Name).Should().BeTrue();
            _labels.GetValue(_label1Name).Should().Be(_label1Index);

            _labels.Contains(_label2Name).Should().BeTrue();
            _labels.GetValue(_label2Name).Should().Be(_label2Index);
        }

        [Fact]
        public void ShouldBeAbleToAccessVariablesInCurrentScope()
        {
            _labels = new LabelCollection();

            _labels.Scopes.SetCurrent("Test Scenario 1");

            _labels.Add(_label1Name, _label1Index);
            
            _labels.Count().Should().Be(1);

            _labels.Scopes.SetCurrent("Test Scenario 2");

            _labels.Add(_label2Name, _label2Index);

            _labels.Count().Should().Be(2);

            _labels.Contains(_label1Name).Should().BeFalse();
            _labels.GetValue(_label1Name).Should().Be(-1);

            _labels.Contains(_label2Name).Should().BeTrue();
            _labels.GetValue(_label2Name).Should().Be(_label2Index);
        }

        [Fact]
        public void ShouldBeAbleToClearVariablesOfCurrentScope()
        {
            _labels = new LabelCollection();

            _labels.Scopes.SetCurrent("Test Scenario 1");

            _labels.Add(_label1Name, _label1Index);

            _labels.Count().Should().Be(1);

            _labels.Scopes.SetCurrent("Function 2");

            _labels.Add(_label2Name, _label2Index);

            _labels.Count().Should().Be(2);

            _labels.Contains(_label1Name).Should().BeFalse();
            _labels.GetValue(_label1Name).Should().Be(-1);

            _labels.Contains(_label2Name).Should().BeTrue();
            _labels.GetValue(_label2Name).Should().Be(_label2Index);

            _labels.RemoveCurrentScopedValues();

            _labels.Count().Should().Be(1);

            _labels.Contains(_label2Name).Should().BeFalse();
            _labels.GetValue(_label2Name).Should().Be(-1);

            _labels.Contains(_label1Name).Should().BeTrue();
            _labels.GetValue(_label1Name).Should().Be(_label1Index);
        }
    }
}

using FluentAssertions;
using Interpreter.Models.Command;
using Sprache;
using Xunit;

namespace Interpreter.Tests
{
    public class ParameterParserTests
    {
        [Fact]
        public void ShouldParseDataProducerExpression()
        {
            var input = "globalVariabl1 = stringParam , 42.5, -12 ";
            var expression = ParameterParser.Expression.Parse(input);
            expression.Variable.Should().Be("globalVariabl1");
            expression.Parameters[0].Should().Be("stringParam");
            expression.Parameters[1].Should().Be("42.5");
            expression.Parameters[2].Should().Be("-12");
        }

        [Fact]
        public void ShouldParseDataConsumerExpression()
        {
            var input = "stringParam , 42.5, -12 ";
            var expression = ParameterParser.Expression.Parse(input);
            expression.Variable.Should().Be(null);
            expression.Parameters[0].Should().Be("stringParam");
            expression.Parameters[1].Should().Be("42.5");
            expression.Parameters[2].Should().Be("-12");
        }

        [Fact]
        public void ShouldParseParameterWithQuotes()
        {
            var input = "\"A quoted parameter\", \"Another 'quoted' parameter\"";
            var expression = ParameterParser.Expression.Parse(input);
            expression.Variable.Should().Be(null);
            expression.Parameters[0].Should().Be("A quoted parameter");
            expression.Parameters[1].Should().Be("Another 'quoted' parameter");
        }

        [Fact]
        public void ShouldParseEmptyParameters()
        {
            var input = "stringParam , , -12 ";
            var expression = ParameterParser.Expression.Parse(input);
            expression.Variable.Should().Be(null);
            expression.Parameters[0].Should().Be("stringParam");
            expression.Parameters[1].Should().Be("-12");
        }
    }
}

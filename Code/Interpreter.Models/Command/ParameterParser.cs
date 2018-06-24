using Sprache;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Models.Command
{
    public class ParameterParser
    {
        private static readonly Parser<char> Quote = Parse.Char('"');
        private static readonly Parser<char> Equal = Parse.Char('=');
        private static readonly Parser<char> Comma = Parse.Char(',');

        private static readonly Parser<string> Variable =
            from first in Parse.IgnoreCase("local").Or(Parse.IgnoreCase("global")).Or(Parse.IgnoreCase("sys"))
            from rest in Parse.LetterOrDigit.Many()
            select new string(first.Concat(rest).ToArray());

        private static readonly Parser<string> Parameter =
            from first in Quote.Optional()
            from text in Parse.AnyChar.Except(Quote).Except(Comma).Many().Text()
            from last in Quote.Optional()
            select text.Trim();

        private static readonly Parser<char> ListDelimiter =
            from leading in Parse.WhiteSpace.Many()
            from comma in Comma
            from trailing in Parse.WhiteSpace.Or(Comma).Many()
            select comma;

        private static readonly Parser<List<string>> Parameters =
            from p in Parameter.DelimitedBy(ListDelimiter)
            select p.ToList();

        public static readonly Parser<CommandParameters> Expression =
            (from variable in Variable
             from equal in Equal.Token()
             from parameters in Parameters
             select new CommandParameters(parameters, variable))
            .Or(from parameters in Parameters
                select new CommandParameters(parameters, null));
    }
}

using Sprache;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Models.Command
{
    public class ParameterParser
    {
        private static readonly Parser<char> Quote = Parse.Char('"');
        private static readonly Parser<char> QuoteEscape = Parse.Char('"');
        private static readonly Parser<char> Equal = Parse.Char('=');
        private static readonly Parser<char> Comma = Parse.Char(',');

        private static Parser<T> Escaped<T>(Parser<T> following)
        {
            return from escape in QuoteEscape
                   from f in following
                   select f;
        }

        private static readonly Parser<string> Variable =
            from first in Parse.IgnoreCase("local").Or(Parse.IgnoreCase("global")).Or(Parse.IgnoreCase("sys"))
            from rest in Parse.LetterOrDigit.Many()
            select new string(first.Concat(rest).ToArray());

        private static readonly Parser<string> LiteralParameterContent =
            from open in Parse.WhiteSpace.Many()
            from content in Parse.AnyChar.Except(Comma).XMany().Text()
            from end in Parse.WhiteSpace.Many()
            select content.Trim();

        private static readonly Parser<char> QuotedParameterContent = Parse.AnyChar.Except(Quote).Or(Escaped(Quote));

        private static readonly Parser<string> QuotedParameter =
            from open in Quote
            from content in QuotedParameterContent.Many().Text()
            from end in Quote
            select content;

        private static readonly Parser<string> Parameter =
            QuotedParameter.XOr(LiteralParameterContent);

        private static IEnumerable<T> Cons<T>(T head, IEnumerable<T> rest)
        {
            yield return head;
            foreach (var item in rest)
                yield return item;
        }

        private static readonly Parser<List<string>> Parameters =
            from leading in Parameter
            from rest in Comma.Then(_ => Parameter).Many()
            select Cons(leading, rest).ToList();

        private static readonly Parser<char> ListDelimiter =
            from leading in Parse.WhiteSpace.Many()
            from comma in Comma
            from trailing in Parse.WhiteSpace.Or(Comma).Many()
            select comma;

        public static readonly Parser<CommandParameters> Expression =
            (from variable in Variable
             from equal in Equal.Token()
             from parameters in Parameters
             select new CommandParameters(parameters, variable))
            .Or(from parameters in Parameters
                select new CommandParameters(parameters, null));
    }
}

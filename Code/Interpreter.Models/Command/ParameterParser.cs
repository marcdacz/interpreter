using Sprache;
using System;
using System.Collections.Generic;

namespace Interpreter.Models.Command
{
    public class ParameterParser
    {
        public static readonly Parser<char> Separator = Parse.Char(',');

        public static readonly Parser<char> QuotedParameterDelimiter = Parse.Char('"');

        public static readonly Parser<char> QuoteEscape = Parse.Char('"');

        public static Parser<T> Escaped<T>(Parser<T> following)
        {
            return from escape in QuoteEscape
                   from f in following
                   select f;
        }

        public static readonly Parser<char> QuotedParameterContent =
            Parse.AnyChar.Except(QuotedParameterDelimiter).Or(Escaped(QuotedParameterDelimiter));

        public static readonly Parser<char> LiteralParameterContent =
            Parse.AnyChar.Except(Separator).Except(Parse.String(Environment.NewLine));

        public static readonly Parser<string> QuotedParameter =
            from open in QuotedParameterDelimiter
            from content in QuotedParameterContent.Many().Text()
            from end in QuotedParameterDelimiter
            select content;

        public static readonly Parser<string> NewLine =
            Parse.String(Environment.NewLine).Text();

        public static readonly Parser<string> RecordTerminator =
            Parse.Return("").End().XOr(
            NewLine.End()).Or(
            NewLine);

        public static readonly Parser<string> Parameter =
            QuotedParameter.XOr(
            LiteralParameterContent.XMany().Text());

        public static readonly Parser<IEnumerable<string>> Record =
            from leading in Parameter
            from rest in Separator.Then(_ => Parameter).Many()
            from terminator in RecordTerminator
            select Cons(leading, rest);

        public static readonly Parser<IEnumerable<IEnumerable<string>>> Parameters =
            Record.XMany().End();

        public static IEnumerable<T> Cons<T>(T head, IEnumerable<T> rest)
        {
            yield return head;
            foreach (var item in rest)
                yield return item;
        }
    }
}

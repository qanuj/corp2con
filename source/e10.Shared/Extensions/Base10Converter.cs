using System;
using System.Collections.Generic;
using System.Linq;

namespace e10.Shared.Extensions
{
    public class Base10Converter
    {
        const char NullDigit = '\0';

        public Base10Converter(string digits, bool shouldSupportRoundTripping = false)
            : this(digits.ToCharArray(), shouldSupportRoundTripping)
        {
        }

        public Base10Converter(IEnumerable<char> digits, bool shouldSupportRoundTripping = false)
        {
            if (digits == null)
            {
                throw new ArgumentNullException("digits");
            }

            if (digits.Count() == 0)
            {
                throw new ArgumentException(
                    message: "The sequence is empty.",
                    paramName: "digits"
                    );
            }

            if (!digits.Distinct().SequenceEqual(digits))
            {
                throw new ArgumentException(
                    message: "There are duplicate characters in the sequence.",
                    paramName: "digits"
                    );
            }

            if (shouldSupportRoundTripping)
            {
                digits = (new[] { NullDigit }).Concat(digits);
            }

            _digitToIndexMap =
                digits
                    .Select((digit, index) => new { digit, index })
                    .ToDictionary(keySelector: x => x.digit, elementSelector: x => x.index);

            _radix = _digitToIndexMap.Count;

            _indexToDigitMap =
                _digitToIndexMap
                    .ToDictionary(keySelector: x => x.Value, elementSelector: x => x.Key);
        }

        readonly Dictionary<char, int> _digitToIndexMap;
        readonly Dictionary<int, char> _indexToDigitMap;
        readonly int _radix;

        public long StringToBase10(string number)
        {
            Func<char, int, long> selector =
                (c, i) =>
                {
                    int power = number.Length - i - 1;

                    int digitIndex;
                    if (!_digitToIndexMap.TryGetValue(c, out digitIndex))
                    {
                        throw new ArgumentException(
                            message: String.Format("Number contains an invalid digit '{0}' at position {1}.", c, i),
                            paramName: "number"
                            );
                    }

                    return Convert.ToInt64(digitIndex * Math.Pow(_radix, power));
                };

            return number.Select(selector).Sum();
        }

        public string Base10ToString(long number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(
                    message: "Value cannot be negative.",
                    paramName: "number"
                    );
            }

            string text = string.Empty;

            long remainder;
            do
            {
                number = Math.DivRem(number, _radix, out remainder);

                char digit;
                if (!_indexToDigitMap.TryGetValue((int)remainder, out digit) || digit == NullDigit)
                {
                    throw new ArgumentException(
                        message: "Value cannot be converted given the set of digits used by this converter.",
                        paramName: "number"
                        );
                }

                text = digit + text;
            }
            while (number > 0);

            return text;
        }
    }
}
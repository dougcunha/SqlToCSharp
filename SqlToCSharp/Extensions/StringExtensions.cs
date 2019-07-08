namespace SqlToCSharp.Extensions
{
    using System;
    using System.Linq;

    public static class StringExtensions
    {
        /// <summary>
        ///     Extension method on string array, which checks if there is any element in array which starts with specified value.
        /// </summary>
        /// <param name="items">string array</param>
        /// <param name="value">string value to compare with.</param>
        /// <returns>True, if there is any element in array which starts with specified value. </returns>
        public static bool ContainsStartingWith(this string[] items, string value)
            => items.Any(item => item.StartsWith(value));

        /// <summary>
        ///     Extension method on string, which checks if string value starts with any element present in specified string array.
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="items">String arrray aginst which comparson will be made.</param>
        /// <returns>True, if string value starts with any element present in specified string array.</returns>
        public static bool StartsWithAnItemInArray(this string value, string[] items)
            => items.Any(value.StartsWith);

        public static string FirstCharToUpper(this string @this)
            => char.ToUpper(@this[0]) + @this.Substring(1);

        public static string ToPascalCase(this string @this)
            => @this.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1))
                .Aggregate(string.Empty, func: (s1, s2) => s1 + s2);
    }
}
namespace SqlToCSharp.Extensions
{
    using System;
    using System.Linq;

    public static class TypeExtensions
    {
        /// <summary>
        ///     Extension method on 'Type', which returns display name of a 'Type'.
        /// </summary>
        /// <param name="t">Instance of type 'Type'</param>
        /// <returns>Display name of 'Type'.</returns>
        public static string GetDisplayName(this Type t)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                return $"{GetDisplayName(t.GetGenericArguments()[0])}?";

            if (t.IsGenericType)
                return
                $"{t.Name.Remove(t.Name.IndexOf('`'))}<{string.Join(",", t.GetGenericArguments().Select(at => at.GetDisplayName()))}>";

            return t.IsArray
            ? $"{GetDisplayName(t.GetElementType())}[{new string(',', t.GetArrayRank() - 1)}]"
            : t.Name;
        }
    }
}
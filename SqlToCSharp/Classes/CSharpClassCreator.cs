namespace SqlToCSharp.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using SqlToCSharp.Enums;
    using SqlToCSharp.Extensions;

    public class CSharpClassCreator : CSharpCreatorBase
    {
        /// <summary>
        ///     Generates C# code as per specified settings and properties.
        /// </summary>
        /// <param name="settings">C# generator settings</param>
        /// <param name="properties">Array of ClrProperty type</param>
        /// <returns>C# code body in string.</returns>
        public override string GenerateCSharpCode(CSharpSettings settings, ClrProperty[] properties)
        {
            if (settings == null)
                return string.Empty;

            if (properties == null)
                return string.Empty;

            if (string.IsNullOrEmpty(settings.ClassName))
                return string.Empty;

            ClassBuilder = new StringBuilder();

            //Add usings 
            AppendLine("using System;");

            if (settings.NullValueIgnoreHandling || settings.SnakeCaseNamingStrategy)
                AppendLine("using Newtonsoft.Json");

            if (settings.SnakeCaseNamingStrategy)
                AppendLine("using Newtonsoft.Json.Serialization");

            AppendLine();

            var hasNamespace = !string.IsNullOrEmpty(settings.Namespace);

            //Add Namespace block if present.
            if (hasNamespace)
            {
                AppendLine($"namespace {settings.Namespace}");
                OpenCurlyBraces();
            }

            var modifier = Enum.GetName(typeof(AccessModifiers), settings.AccessModifier)
                              ?.ToLower();

            if (!string.IsNullOrEmpty(modifier))
                modifier += " ";

            if (settings.SnakeCaseNamingStrategy || settings.NullValueIgnoreHandling)
            {
                var attributeValues = string.Empty;

                if (settings.NullValueIgnoreHandling)
                    attributeValues = "ItemNullValueHandling = NullValueHandling.Ignore";

                if (settings.SnakeCaseNamingStrategy)
                {
                    if (settings.NullValueIgnoreHandling)
                        attributeValues += ", NamingStrategyType = typeof(SnakeCaseNamingStrategy)";
                    else
                        attributeValues += "NamingStrategyType = typeof(SnakeCaseNamingStrategy)";
                }

                AppendLine($"[JsonObject({attributeValues})]");
            }

            //Add class name
            AppendLine($"{modifier}class {settings.ClassName}");

            //Class opens
            OpenCurlyBraces();

            var firstProperty = true;

            foreach (var p in properties)
            {
                var typeName = GetTypeKeyword(p.PropertyType.GetDisplayName());

                switch (settings.MemberType)
                {
                    case MemberTypes.AutoProperties:
                        AppendLine
                        (
                            $"{AccessModifiers.Public.ToString().ToLower()} {typeName} {GetNamePerConvention(p.Name, settings.PropertiesNamingConvention, settings.PropertiesPrefix)} {{ get; set; }}"
                        );

                        break;

                    case MemberTypes.FieldsOnly:
                        AppendLine
                        (
                            $"{AccessModifiers.Private.ToString().ToLower()} {typeName} {GetNamePerConvention(p.Name, settings.FieldNamingConvention, settings.FieldsPrefix)} {{ get; set; }}"
                        );

                        break;

                    case MemberTypes.FieldEncapsulatedByproperties:
                        if (!firstProperty)
                            AppendLine();

                        var fldName = GetNamePerConvention(p.Name, settings.FieldNamingConvention, settings.FieldsPrefix);

                        AppendLine($"{AccessModifiers.Private.ToString().ToLower()} {typeName} {fldName};");

                        if (settings.CustomLogicGetter.Length > 0 && settings.CustomLogicSetter.Length > 0)
                            AppendLine
                            (
                                $"{AccessModifiers.Public.ToString().ToLower()} {typeName} {GetNamePerConvention(p.Name, settings.PropertiesNamingConvention, settings.PropertiesPrefix)} {{ get {{{settings.CustomLogicGetter}; return {fldName};}} set {{{settings.CustomLogicSetter}; {fldName} = value;}} }}"
                            );
                        else if (settings.CustomLogicGetter.Length > 0)
                            AppendLine
                            (
                                $"{AccessModifiers.Public.ToString().ToLower()} {typeName} {GetNamePerConvention(p.Name, settings.PropertiesNamingConvention, settings.PropertiesPrefix)} {{ get {{{settings.CustomLogicGetter}; return {fldName};}} set => {fldName} = value; }}"
                            );
                        else if (settings.CustomLogicSetter.Length > 0)
                            AppendLine
                            (
                                $"{AccessModifiers.Public.ToString().ToLower()} {typeName} {GetNamePerConvention(p.Name, settings.PropertiesNamingConvention, settings.PropertiesPrefix)} {{ get => {fldName}; set {{{settings.CustomLogicSetter}; {fldName} = value;}} }}"
                            );
                        else
                            AppendLine
                            (
                                $"{AccessModifiers.Public.ToString().ToLower()} {typeName} {GetNamePerConvention(p.Name, settings.PropertiesNamingConvention, settings.PropertiesPrefix)} {{ get => {fldName}; set => {fldName} = value; }}"
                            );

                        break;
                }

                firstProperty = false;
            }

            //Class closes
            CloseCurlyBraces();

            if (hasNamespace)
                CloseCurlyBraces();

            return ClassBuilder.ToString();
        }

        private static string GetTypeKeyword(string typeName)
        {
            var nullable = typeName.EndsWith("?");
            typeName = typeName.TrimEnd('?');

            var dict = new Dictionary<string, string>
                {
                    ["Int32"] = "int",
                    ["String"] = "string",
                    ["Int16"] = "short",
                    ["Boolean"] = "bool",
                    ["Int64"] = "long",
                    ["Decimal"] = "float",
                    ["Double"] = "double"
                };

            var value = dict.TryGetValue(typeName, out var keyword)
            ? keyword
            : typeName;

            return nullable
            ? $"{value}?"
            : value;
        }
    }
}
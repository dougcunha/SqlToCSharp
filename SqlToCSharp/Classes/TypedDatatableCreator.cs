namespace SqlToCSharp.Classes
{
    using System;
    using SqlToCSharp.Enums;
    using SqlToCSharp.Extensions;

    /// <summary>
    ///     Class to create typed datatables.
    /// </summary>
    public class TypedDatatableCreator : CSharpCreatorBase
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

            AppendLine("using System;");
            AppendLine("using System.Data;");

            if (settings.NullValueIgnoreHandling || settings.SnakeCaseNamingStrategy)
                AppendLine("using Newtonsoft.Json");

            if (settings.SnakeCaseNamingStrategy)
                AppendLine("using Newtonsoft.Json.Serialization");

            AppendLine();

            var hasNamespace = !string.IsNullOrEmpty(settings.Namespace);

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

            AppendLine($"{modifier}class {settings.ClassName} : DataTable");

            //Class opens
            OpenCurlyBraces();

            AppendLine($"{modifier} {settings.ClassName}()");

            OpenCurlyBraces();
            AppendLine("this.TableName = this.GetType().Name;");

            foreach (var p in properties)
                AppendLine($"this.Columns.Add(\"{p.Name}\", typeof({p.PropertyType.GetDisplayName()}));");

            CloseCurlyBraces();

            var rowClassName = $"{settings.ClassName}Row";

            AppendLine();

            AppendLine("protected override Type GetRowType()");
            OpenCurlyBraces();

            AppendLine($"return typeof({rowClassName});");
            CloseCurlyBraces();

            AppendLine();

            AppendLine($"public {rowClassName} this[int rowIndex]");
            OpenCurlyBraces();

            AppendLine($"get {{ return ({rowClassName})Rows[rowIndex]; }}");
            CloseCurlyBraces();

            AppendLine();

            AppendLine($"public void AddRow({rowClassName} row)");
            OpenCurlyBraces();

            AppendLine("Rows.Add(row);");
            CloseCurlyBraces();

            AppendLine();

            AppendLine($"public new {rowClassName} NewRow()");
            OpenCurlyBraces();

            AppendLine($"return ({rowClassName})base.NewRow();");
            CloseCurlyBraces();

            //table class closes here
            CloseCurlyBraces();

            AppendLine();

            //Writing Row Class starts here
            AppendLine($"{modifier} class {rowClassName} : DataRow");
            OpenCurlyBraces();

            //Adding Constructor
            AppendLine();
            AppendLine($"public {rowClassName}(DataRowBuilder rowBuilder) : base(rowBuilder) {{ }}");
            AppendLine();

            //Adding Property accessors
            foreach (var p in properties)
            {
                AppendLine
                (
                    $"public {p.PropertyType.GetDisplayName()} {GetNamePerConvention(p.Name, settings.PropertiesNamingConvention, settings.PropertiesPrefix)} {{ get => ({p.PropertyType.GetDisplayName()})this[\"{p.Name}\"]; set => this[\"{p.Name}\"] = value;}}"
                );

                AppendLine();
            }

            //Row Class closes here
            CloseCurlyBraces();

            if (hasNamespace)
                CloseCurlyBraces();

            return ClassBuilder.ToString();
        }
    }
}
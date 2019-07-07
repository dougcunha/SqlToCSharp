namespace SqlToCSharp
{
    public static class AppStatic
    {
        public static string Database = string.Empty;
        public static string DbConnectionString = string.Empty;
        public static string Server = string.Empty;
    }

    public static class Constants
    {
        public const string CONTAINS = "Contains";
        public const string DOES_NOT_CONTAIN = "Does not Contain";
        public const string EQUALS = "Equals";

        public const string FILTERED_TEXT = " (filtered)";
        public const string STORED_PROCEDURES = "Stored Procedures";
        public const string TABLES = "Tables";
        public const string TABLE_VALUED_FUNCTIONS = "Table Valued Functions";
        public const string USER_DEFINED_TABLE_TYPES = "User-Defined Table Types";
        public const string VIEWS = "Views";
    }
}
namespace SqlToCSharp.Classes
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class ConnectionRequest
    {
        private const string FORMAT_DB_NAME = "Initial Catalog = {0}";
        private const string FORMAT_SQL = "Data Source={0}; User ID = {1}; Password={2}";

        private const string FORMAT_WIN = "Server= {0}; Integrated Security = SSPI;";

        public readonly string DbName;
        public readonly string Password;
        public readonly string ServerName;
        public readonly string Username;
        public readonly bool WinAuth;

        public ConnectionRequest(string server)
        {
            if (string.IsNullOrEmpty(server))
                throw new ArgumentException(@"Server cannot be null or empty string", nameof(server));

            SqlConn = null;
            ServerName = server;
            WinAuth = true;
        }

        public ConnectionRequest(string server, string dbName) : this(server)
            => DbName = dbName;

        public ConnectionRequest(string server, string user, string pass) : this(server)
        {
            if (string.IsNullOrEmpty(user))
                throw new ArgumentException(@"Username cannot be null or empty string", nameof(user));

            if (string.IsNullOrEmpty(pass))
                throw new ArgumentException(@"Password cannot be null or empty string", nameof(pass));

            WinAuth = false;
            Username = user;
            Password = pass;
        }

        public ConnectionRequest(string server, string user, string pass, string dbName) : this(server, user, pass)
            => DbName = dbName;

        public SqlConnection SqlConn { get; private set; }

        public bool TryConnect()
        {
            var connString = WinAuth
            ? string.Format(FORMAT_WIN, ServerName)
            : string.Format(FORMAT_SQL, ServerName, Username, Password);

            if (!string.IsNullOrEmpty(DbName))
                connString = $"{connString}; {string.Format(FORMAT_DB_NAME, DbName)}";

            if (string.IsNullOrEmpty(connString))
                throw new InvalidOperationException(@"Unable to create sql connection string.");

            SqlConnection sqlConn = null;

            try
            {
                sqlConn = new SqlConnection(connString);
                sqlConn.Open();
                SqlConn = sqlConn;

                return true;
            }
            catch (SqlException)
            {
                SqlConn = null;
            }
            catch
            {
                SqlConn = null;

                throw;
            }
            finally
            {
                if (sqlConn != null && sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();

                if (SqlConn == null)
                    sqlConn?.Dispose();
            }

            return false;
        }
    }
}
namespace SqlToCSharp.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using SqlToCSharp.Classes;
    using SqlToCSharp.Helpers;

    /// <summary>
    ///     Represents Form which will establish connection to Sql server database connnection.
    /// </summary>
    public sealed partial class SqlConnectionForm : Form
    {
        /// <summary>
        ///     Default constructor.
        /// </summary>
        public SqlConnectionForm()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Represents whether connection was established.
        /// </summary>
        public bool ConnectionSuccess { get; private set; }

        /// <summary>
        ///     Property of ConnectionRequest type.
        /// </summary>
        public ConnectionRequest ConnReq { get; private set; }

        /// <summary>
        ///     Click event handler of Cancel button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        ///     Click event handler of Connect button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        /// <exception cref="Exception"></exception>
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDb.SelectedValue == null || ddlDb.SelectedValue.ToString() == string.Empty)
                    throw new InvalidOperationException("Invalid database");

                var req = ddlAuth.SelectedIndex == 0
                ? new ConnectionRequest(txtServer.Text.Trim(), ddlDb.SelectedValue.ToString())
                : new ConnectionRequest(txtServer.Text.Trim(), txtUser.Text.Trim(), txtPass.Text.Trim());

                if (!req.TryConnect())
                    throw new InvalidOperationException("Sql Connection failed.");

                ConnectionSuccess = true;
                ConnReq = req;

                AppStatic.DbConnectionString = req.SqlConn.ConnectionString;
                AppStatic.Database = req.SqlConn.Database;
                AppStatic.Server = req.SqlConn.DataSource;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ConnectionSuccess = false;
                ErrorViewerForm.ShowError(ex, this);
                DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        ///     SelectedIndexChanged event handler for Authorization dropdownlist.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private void DdlAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblUser.Enabled = txtUser.Enabled = lblPass.Enabled = txtPass.Enabled = ddlAuth.SelectedIndex != 0;
        }

        /// <summary>
        ///     Enter event handler of Database combobox.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private void DdlDb_Enter(object sender, EventArgs e)
        {
            try
            {
                var serverConnReq = GetServerConnection();
                var sqlHelp = new SqlHelper(serverConnReq.SqlConn.ConnectionString);
                List<string> databases = sqlHelp.GetDatabaseList();

                ddlDb.DataSource = databases;
            }
            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);
            }
        }

        /// <summary>
        ///     Creates database connection from UI controls and tries to connect.
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <returns>Instance of ConnectionRequest type.</returns>
        private ConnectionRequest GetServerConnection()
        {
            var serverConnReq = ddlAuth.SelectedIndex == 0
            ? new ConnectionRequest(txtServer.Text.Trim())
            : new ConnectionRequest(txtServer.Text.Trim(), txtUser.Text.Trim(), txtPass.Text.Trim());

            if (!serverConnReq.TryConnect())
                throw new InvalidOperationException("Sql Connection failed.");

            return serverConnReq;
        }

        /// <summary>
        ///     Form load event handler.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private void SqlConnectionForm_Load(object sender, EventArgs e)
        {
            ddlAuth.SelectedIndex = 0;
            txtServer.Text = Environment.MachineName;
            ddlDb.Focus();
        }
    }
}
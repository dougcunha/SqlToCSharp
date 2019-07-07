namespace SqlToCSharp.Forms
{
    #region usings
    using System;
    using System.Windows.Forms;
    #endregion

    /// <summary>
    ///     This class represents the Filter criteria which will be applied on objects of TreeNode.
    /// </summary>
    public partial class FilterForm : Form
    {
        /// <summary>
        ///     Database name.
        /// </summary>
        private readonly string _database;
        /// <summary>
        ///     Database Server name.
        /// </summary>
        private readonly string _server;

        /// <summary>
        ///     Parametrized constructor.
        /// </summary>
        /// <param name="serverName">Server name.</param>
        /// <param name="dbName">Database name.</param>
        public FilterForm(string serverName, string dbName)
        {
            InitializeComponent();
            _server = serverName;
            _database = dbName;
        }

        /// <summary>
        ///     Filter string
        /// </summary>
        public string FilterText { get; set; }

        /// <summary>
        ///     Type of Filter e.g. 'Contains', 'Equals', 'Does not contain'
        /// </summary>
        public string FilterType { get; set; }

        /// <summary>
        ///     Apply action handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event argument.</param>
        private void BtnApply_Click(object sender, EventArgs e)
        {
            FilterText = txtFilter.Text;
            FilterType = ddlFilterType.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        ///     Close action handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event argument.</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        ///     Reset action handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event argument.</param>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            ddlFilterType.SelectedIndex = 0;
            txtFilter.Text = string.Empty;
        }

        /// <summary>
        ///     Load event handler of FilterForm.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event argument.</param>
        private void FilterForm_Load(object sender, EventArgs e)
        {
            ddlFilterType.SelectedIndex = 0;
            lblServer.Text = _server;
            lblDB.Text = _database;
        }
    }
}
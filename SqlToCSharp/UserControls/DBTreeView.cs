namespace SqlToCSharp.UserControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using SqlToCSharp.Enums;
    using SqlToCSharp.Extensions;
    using SqlToCSharp.Forms;

    public sealed partial class DbTreeView : UserControl
    {
        /// <summary>
        ///     Array of name of nodes, on which filter can be added.
        /// </summary>
        private readonly string[] _filterableNodes = new[]
            {
                Constants.TABLES, Constants.TABLE_VALUED_FUNCTIONS, Constants.VIEWS, Constants.STORED_PROCEDURES,
                Constants.USER_DEFINED_TABLE_TYPES
            };

        /// <summary>
        ///     Database name of current database connection.
        /// </summary>
        private string _dbName = string.Empty;
        /// <summary>
        ///     Dictionary to cache the database objects.
        /// </summary>
        private Dictionary<string, List<string[]>> _dbObjects;

        /// <summary>
        ///     Server name of currennt database connection.
        /// </summary>
        private string _server = string.Empty;

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public DbTreeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// </summary>
        public event EventHandler GenerateCSharpClass;

        /// <summary>
        /// </summary>
        public event EventHandler GenerateTypedDatatable;

        public event TreeViewEventHandler SelectedNodeChanged;

        /// <summary>
        ///     The TreeView control.
        /// </summary>
        public TreeView TreeView
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets database object type of selected database object.
        /// </summary>
        /// <returns>DBObjectType</returns>
        public DbObjectType GetDbObjectType()
        {
            var item = TreeView.SelectedNode;

            if (item?.Parent != null)
            {
                item = item.Parent;

                switch (item.Text.Trim())
                {
                    case Constants.TABLES: return DbObjectType.Table;
                    case Constants.VIEWS: return DbObjectType.Views;
                    case Constants.TABLE_VALUED_FUNCTIONS: return DbObjectType.Functions;
                    case Constants.STORED_PROCEDURES: return DbObjectType.StoredProcedure;
                    case Constants.USER_DEFINED_TABLE_TYPES: return DbObjectType.UserDefinedTableTypes;
                }
            }

            return DbObjectType.None;
        }

        /// <summary>
        ///     Gets name of selected database object, without schema.
        /// </summary>
        /// <returns>Name of database object, without schema.</returns>
        public string GetSelectedDbItem()
        {
            var item = TreeView.SelectedNode?.Text.Trim();
            string[] names = item?.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            return names?.Length > 1
            ? names[1]
            : string.Empty;
        }

        /// <summary>
        ///     Gets schema name of selected database object.
        /// </summary>
        /// <returns>Schema name of selected database object.</returns>
        public string GetSelectedDbItemSchema()
        {
            if (TreeView.SelectedNode == null)
                return string.Empty;

            var item = TreeView.SelectedNode.Text.Trim();

            return item.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        public string GetSelectedNode()
            => TreeView.SelectedNode.Text;

        /// <summary>
        ///     Populates data into TreeNode UI control.
        /// </summary>
        /// <param name="n">An object of type TreeNode, in which data has to be populated.</param>
        public void LoadTreeNode(TreeNode n)
        {
            List<string[]> listProc = _dbObjects[n.Text];

            var dbObjectFilter = string.Empty;
            var filterType = string.Empty;

            if (n.Tag is FilterForm filterForm)
            {
                dbObjectFilter = filterForm.FilterText;
                filterType = filterForm.FilterType;
            }

            foreach (string[] t in listProc)
                if (CanBeAdded(t[1], dbObjectFilter, filterType))
                    n.Nodes.Add(t[0] + "." + t[1]);
        }

        /// <summary>
        ///     Populates data into TreeView Control.
        /// </summary>
        /// <param name="dbItems">Dictionary of database items.</param>
        /// <param name="server">Server name.</param>
        /// <param name="database">Database name.</param>
        public void LoadTreeView(Dictionary<string, List<string[]>> dbItems, string server, string database)
        {
            _dbObjects = dbItems;
            _server = server;
            _dbName = database;

            TreeView.Nodes.Clear();

            var n = TreeView.Nodes.Add("Server(" + _server + ")");

            var serverNode = n.Nodes.Add(_dbName);

            string[] dbItemNames = _dbObjects.Keys.ToArray();

            foreach (var dbItemTypeNode in dbItemNames.Select(dbItemType => serverNode.Nodes.Add(dbItemType)))
                LoadTreeNode(dbItemTypeNode);

            n.Expand();
            serverNode.Expand();
        }

        /// <summary>
        ///     Decides where the input item can be added to TreeNode or TreeView as per filter specified.
        /// </summary>
        /// <param name="input">The database object name.</param>
        /// <param name="filter">Filter applied.</param>
        /// <param name="filterType">Type of filter.</param>
        /// <returns>If can be added then True else False.</returns>
        private bool CanBeAdded(string input, string filter, string filterType)
        {
            if (filter.Length == 0)
                return true;

            if (filterType.Length == 0)
                return true;

            switch (filterType)
            {
                case Constants.CONTAINS:
                    return input.ToLower()
                                .Contains(filter.ToLower());

                case Constants.DOES_NOT_CONTAIN:
                    return !input.ToLower()
                                 .Contains(filter.ToLower());

                case Constants.EQUALS:
                    return input.ToLower()
                                .Equals(filter.ToLower());
            }

            return true;
        }

        /// <summary>
        ///     Event handler of 'Copy Name' mmenu Item cllick.
        /// </summary>
        /// <param name="sender">sender of this event.</param>
        /// <param name="e">event argument of this event.</param>
        private void CopyNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (TreeView?.SelectedNode != null)
                    Clipboard.SetText(TreeView.SelectedNode.Text);
            }
            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);

                throw;
            }
        }

        /// <summary>
        ///     Applies Filter on TreeNode UI control as per specified filter string and filter type.
        /// </summary>
        /// <param name="tn">Object of TreeNode type.</param>
        /// <param name="filter">Filter string.</param>
        /// <param name="filterType">Type of Filter</param>
        private void FilterNode(TreeNode tn, string filter, string filterType)
        {
            var addList = new List<TreeNode>();

            for (var i = 0; i < tn.Nodes.Count; i++)
                if (CanBeAdded
                (
                    tn.Nodes[i]
                      .Text,
                    filter,
                    filterType
                ))
                    addList.Add(tn.Nodes[i]);

            tn.Nodes.Clear();

            foreach (var t in addList)
                tn.Nodes.Add(t);

            tn.Expand();
        }

        /// <summary>
        ///     Click event handler when 'Filter' menu item is clicked.
        /// </summary>
        /// <param name="sender">sender of this event.</param>
        /// <param name="e">Event argument of this handler.</param>
        private void FilterSettingToolItem_Click(object sender, EventArgs e)
        {
            try
            {
                var tn = TreeView.SelectedNode;

                if (tn?.Text.Trim()
                       .StartsWithAnItemInArray(_filterableNodes) == true)
                {
                    var filterForm = tn.Tag as FilterForm;

                    if (filterForm == null)
                    {
                        filterForm = new FilterForm(_server, tn.Parent.Text.Trim());
                        tn.Tag = filterForm;
                    }

                    if (filterForm.ShowDialog(this) == DialogResult.OK)
                    {
                        if (filterForm.FilterText.Length > 0)
                        {
                            var dbObjectFilter = filterForm.FilterText.Trim();
                            var filterType = filterForm.FilterType.Trim();
                            FilterNode(tn, dbObjectFilter, filterType);

                            if (!tn.Text.EndsWith(Constants.FILTERED_TEXT))
                                tn.Text = tn.Text + Constants.FILTERED_TEXT;
                        }
                        else
                        {
                            ResetFilter(tn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);
            }
        }

        private void GenerateCClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateCSharpClass?.Invoke(sender, e);
        }

        private void GenerateSimpleTypedDatatableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateTypedDatatable?.Invoke(sender, e);
        }

        /// <summary>
        ///     Resets filter criteria for specified TreeNode object.
        /// </summary>
        /// <param name="tn">The TreeNode object on which filter criteria has to be reset.</param>
        private void ResetFilter(TreeNode tn)
        {
            if (tn.Tag is FilterForm filterForm)
            {
                filterForm.Dispose();
                tn.Tag = null;
            }

            tn.Nodes.Clear();
            tn.Text = tn.Text.Replace(Constants.FILTERED_TEXT, string.Empty);
            LoadTreeNode(tn);
        }

        /// <summary>
        ///     Event handler when 'Reset filter' menu item is clicked.
        /// </summary>
        /// <param name="sender">sender of this event.</param>
        /// <param name="e">Event argument of this handler.</param>
        private void ResetFilterToolItem_Click(object sender, EventArgs e)
        {
            try
            {
                var tn = TreeView.SelectedNode;

                if (tn?.Text.Trim()
                       .StartsWithAnItemInArray(_filterableNodes) == true &&
                    tn.Tag != null)
                    ResetFilter(tn);
            }

            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);
            }
        }

        private void TvDbItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedNodeChanged?.Invoke(sender, e);
        }

        /// <summary>
        ///     MouseClick event handler to intercept the mouse right-click, to show Context Menu.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event argument</param>
        private void TvDbItems_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    TreeView.SelectedNode = e.Node;

                    if (e.Node.Text.Trim()
                         .StartsWithAnItemInArray(_filterableNodes))
                    {
                        filterSettingToolItem.Enabled = resetFilterToolItem.Enabled = true;
                        generateCClassToolStripMenuItem.Enabled = generateSimpleTypedDatatableToolStripMenuItem.Enabled = false;
                    }
                    else if (e.Node.Parent?.Text.Trim()
                              .StartsWithAnItemInArray(_filterableNodes) == true)
                    {
                        filterSettingToolItem.Enabled = resetFilterToolItem.Enabled = false;
                        generateCClassToolStripMenuItem.Enabled = generateSimpleTypedDatatableToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        filterSettingToolItem.Enabled = resetFilterToolItem.Enabled = false;
                        generateCClassToolStripMenuItem.Enabled = generateSimpleTypedDatatableToolStripMenuItem.Enabled = false;
                    }

                    cntxMenu.Show(TreeView, e.Location);
                }
            }
            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);
            }
        }
    }
}
namespace SqlToCSharp.Forms
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using Extensions;
    using SqlToCSharp.Classes;
    using SqlToCSharp.Helpers;
    using SqlToCSharp.UserControls;

    /// <summary>
    ///     The Main form which contains all the features of Sql to C# code generator.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        ///     C# Code generator base class.
        /// </summary>
        private CSharpCreatorBase _creator;

        /// <summary>
        ///     Dictionary of Database objects types and Database objects.
        /// </summary>
        private Dictionary<string, List<string[]>> _dbObjects;

        /// <summary>
        ///     Settings object.
        /// </summary>
        private CSharpSettings _settings;

        /// <summary>
        ///     Default Contructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            tabControl.Visible = false;
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cSharpCodeControl.Copy();
        }

        /// <summary>
        ///     Settings changed event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument.</param>
        private void CreatorSettings_ClassSettingChangedEventHandler(ClassGeneratorSettings sender, ClassGeneratorSettingsEventArgs e)
        {
            try
            {
                _settings = null;

                if (string.IsNullOrWhiteSpace(e.ClassName))
                    e.ClassName = dbTreeView.GetSelectedDbItem().ToPascalCase();

                if (_creator == null)
                    return;

                tabControl.Visible = true;
                _settings = CSharpSettings.GetCSharpSettings(e);
                var sql = new SqlHelper(AppStatic.DbConnectionString);

                var code = _creator.GenerateCSharpCode
                (
                    _settings,
                    sql.GetClrProperties
                    (
                        dbTreeView.GetSelectedDbItemSchema(),
                        dbTreeView.GetSelectedDbItem(),
                        dbTreeView.GetDbObjectType()
                    )
                );

                cSharpCodeControl.Text = code;

                tabPage1.Text = $@"{tabPage1.Text.Split
                (new[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0]} ({dbTreeView.GetSelectedNode()})";
            }
            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);
            }
        }

        private void CSharpCodeControl_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                    textBoxContextMenu.Show(cSharpCodeControl, e.Location);
            }
            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);
            }
        }

        /// <summary>
        ///     Database change menu-item click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument.</param>
        private void DbConnectionStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var sqlConnForm = new SqlConnectionForm();

                if (sqlConnForm.ShowDialog(this) == DialogResult.OK && sqlConnForm.ConnectionSuccess)
                {
                    AppStatic.DbConnectionString = sqlConnForm.ConnReq.SqlConn.ConnectionString;
                    AppStatic.Database = sqlConnForm.ConnReq.SqlConn.Database;
                    AppStatic.Server = sqlConnForm.ConnReq.SqlConn.DataSource;

                    FormLoad();
                }
            }
            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);
            }
        }

        private void DbTreeView_GenerateCSharpClass(object sender, EventArgs e)
        {
            PocoGenerateMenuItem_Click(sender, e);
        }

        private void DbTreeView_GenerateTypedDatatable(object sender, EventArgs e)
        {
            GenerateSimpleTypedDatatableToolStripMenuItem_Click(sender, e);
        }

        private void DbTreeView_SelectedNodeChanged(object sender, TreeViewEventArgs e)
        {
            classGeneratorSetting.ResetSettingsToDefault();
        }

        /// <summary>
        ///     Configures the form on load.
        /// </summary>
        private void FormLoad()
        {
            LoadData();
            dbTreeView.LoadTreeView(_dbObjects, AppStatic.Server, AppStatic.Database);
            _creator = null;
            tabControl.Visible = false;
        }

        /// <summary>
        ///     Generate Simple Typed Datatable menu-item click evennt handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument.</param>
        private void GenerateSimpleTypedDatatableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tabPage1.Text = @"Simple Typed Datatable";

                _creator = null;

                _creator = new TypedDatatableCreator();
                classGeneratorSetting.ApplySettings();
            }
            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);
            }
        }

        /// <summary>
        ///     Loads Database object types and Database object names to Dictionary object.
        /// </summary>
        private void LoadData()
        {
            if (_dbObjects != null)
            {
                _dbObjects.Clear();
                _dbObjects = null;
            }

            _dbObjects = new Dictionary<string, List<string[]>>();
            var sql = new SqlHelper(AppStatic.DbConnectionString);

            List<string[]> listDbItems = sql.GetTables();
            _dbObjects.Add(Constants.TABLES, listDbItems);

            listDbItems = sql.GetViews();
            _dbObjects.Add(Constants.VIEWS, listDbItems);

            listDbItems = sql.GetProcedures();
            _dbObjects.Add(Constants.STORED_PROCEDURES, listDbItems);

            listDbItems = sql.GetTableValuedFunctions();
            _dbObjects.Add(Constants.TABLE_VALUED_FUNCTIONS, listDbItems);

            listDbItems = sql.GetTableTypes();
            _dbObjects.Add(Constants.USER_DEFINED_TABLE_TYPES, listDbItems);
        }

        /// <summary>
        ///     Load event handler of Main Form.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (AppStatic.DbConnectionString.Length == 0)
                {
                    var sqlConnForm = new SqlConnectionForm();

                    if (sqlConnForm.ShowDialog(this) == DialogResult.OK && sqlConnForm.ConnectionSuccess)
                    {
                        AppStatic.DbConnectionString = sqlConnForm.ConnReq.SqlConn.ConnectionString;
                        AppStatic.Database = sqlConnForm.ConnReq.SqlConn.Database;
                        AppStatic.Server = sqlConnForm.ConnReq.SqlConn.DataSource;

                        FormLoad();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);
            }
        }

        /// <summary>
        ///     Generate C# Class menu-item click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument.</param>
        private void PocoGenerateMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tabPage1.Text = @"C# Class";

                _creator = null;

                _creator = new CSharpClassCreator();
                classGeneratorSetting.ApplySettings();
            }
            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);

                throw;
            }
        }

        /// <summary>
        ///     Saves code to .cs file.
        /// </summary>
        private void SaveCode()
        {
            var diagSave = new SaveFileDialog
                {
                    FileName = _settings.ClassName + ".cs",
                    Filter = @"C# files|*.cs"
                };

            if (diagSave.ShowDialog(this) == DialogResult.OK)
                File.WriteAllText(diagSave.FileName, cSharpCodeControl.Text);
        }

        /// <summary>
        ///     Save to file menu-item click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument.</param>
        private void SaveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCode();
            }
            catch (Exception ex)
            {
                ErrorViewerForm.ShowError(ex, this);
            }
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cSharpCodeControl.SelectAll();
        }
    }
}
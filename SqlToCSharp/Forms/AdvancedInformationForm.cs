namespace SqlToCSharp.Forms
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using SqlToCSharp.Classes;

    public partial class AdvancedInformationForm : Form
    {
        private AdvancedInformationForm()
        {
            InitializeComponent();
        }

        private AdvancedInformationForm(ExceptionWrapper ex) : this()
            => Error = ex;

        public ExceptionWrapper Error { get; }

        public static void ShowDetailsDialog(ExceptionWrapper ex, IWin32Window owner = null)
        {
            var form = new AdvancedInformationForm(ex);
            form.ShowDialog(owner);
        }

        private void AdvancedInformationForm_Load(object sender, EventArgs e)
        {
            LoadTreeView(Error);
        }

        private void BtnSaveLocally_Click(object sender, EventArgs e)
        {
            SaveDialog.FileName = $"Error_{DateTime.Now:yyyyMMdd}.error";

            if (SaveDialog.ShowDialog() == DialogResult.OK)
                File.WriteAllText(SaveDialog.FileName, Error.ToXmlString(), Encoding.ASCII);
        }

        private TreeNode GetTreeNode(ExceptionWrapper exWrap)
        {
            var tn = new TreeNode("Error");
            tn.Nodes.Add(new TreeNode(nameof(exWrap.Message)) { Tag = exWrap.Message });
            tn.Nodes.Add(new TreeNode(nameof(exWrap.Source)) { Tag = exWrap.Source });
            tn.Nodes.Add(new TreeNode(nameof(exWrap.Helplink)) { Tag = exWrap.Helplink });
            tn.Nodes.Add(new TreeNode(nameof(exWrap.StackTrace)) { Tag = exWrap.StackTrace });

            if (exWrap.InnerException != null)
            {
                var tnInner = GetTreeNode(exWrap.InnerException);
                tn.Nodes.Add(tnInner);
            }

            return tn;
        }

        private void LoadTreeView(ExceptionWrapper exWrap)
        {
            tvError.Nodes.Add(GetTreeNode(exWrap));
            tvError.ExpandAll();

            tvError.SelectedNode = tvError.Nodes[0]
                                          .FirstNode;
        }

        private void TvError_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var tn = e.Node;

            txtValue.Text = tn?.Tag?.ToString() ?? string.Empty;
        }
    }
}
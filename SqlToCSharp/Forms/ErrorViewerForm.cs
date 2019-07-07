namespace SqlToCSharp.Forms
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using SqlToCSharp.Classes;
    using SqlToCSharp.Properties;

    /// <summary>
    ///     This form will show Errors, Information, Warning and Sucsess messages.
    /// </summary>
    public partial class ErrorViewerForm : Form
    {
        private const int GWL_STYLE = -16;
        private const int WS_CLIPSIBLINGS = 1 << 26;
        private ExceptionWrapper _exWrapper;

        /// <summary>
        ///     Default Constructor.
        /// </summary>
        private ErrorViewerForm()
        {
            InitializeComponent();
            moreDetails.Visible = false;
            moreDetailsToolTip.SetToolTip(moreDetails, "Click to see details of error.");
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        /// <summary>
        ///     Static method, which configures the form to show "Error" and opens this form as a dialog.
        /// </summary>
        /// <param name="ex">Exception object</param>
        /// <param name="parent">Parent control of the caller.</param>
        public static void ShowError(Exception ex, Control parent = null)
        {
            var form = new ErrorViewerForm
                {
                    _exWrapper = new ExceptionWrapper(ex),
                    Text = parent == null
                    ? "Error -  Sql to C# Code generator"
                    : $"Error - {parent.Text}",
                    errorControl =
                        {
                            Text = ex.Message
                        },
                    moreDetails =
                        {
                            Visible = true
                        },
                    ShowIcon = true,
                    Icon = SystemIcons.Error,
                    pictureBox1 =
                        {
                            Image = SystemIcons.Error.ToBitmap()
                        }
                };

            form.ShowDialog(parent);
        }

        /// <summary>
        ///     Static method, which configures the form to show "Information" and opens this form as a dialog.
        /// </summary>
        /// <param name="message">Information to be shown on form.</param>
        /// <param name="parent">Parent control of the caller.</param>
        public static void ShowInformation(string message, Control parent)
        {
            var form = new ErrorViewerForm
                {
                    Text = $@"Information - {parent.Text}",
                    errorControl =
                        {
                            Text = message
                        },
                    Icon = SystemIcons.Information,
                    pictureBox1 =
                        {
                            Image = SystemIcons.Information.ToBitmap()
                        }
                };

            form.ShowDialog(parent);
        }

        /// <summary>
        ///     Static method, which configures the form to show "Success" and opens this form as a dialog.
        /// </summary>
        /// <param name="message">Success message to be shown on form.</param>
        /// <param name="parent">Parent control of the caller.</param>
        public static void ShowSuccess(string message, Form parent)
        {
            var form = new ErrorViewerForm
                {
                    Text = $@"Success - {parent.Text}",
                    errorControl =
                        {
                            Text = message
                        },
                    Icon = Resources.ok,
                    pictureBox1 =
                        {
                            Image = Resources.ok.ToBitmap()
                        }
                };

            form.ShowDialog(parent);
        }

        /// <summary>
        ///     Static method, which configures the form to show "Warning" and opens this form as a dialog.
        /// </summary>
        /// <param name="message">Warning to be shown on form.</param>
        /// <param name="parent">Parent control of the caller.</param>
        public static void ShowWarning(string message, Form parent)
        {
            var form = new ErrorViewerForm
                {
                    Text = $@"Warning - {parent.Text}",
                    errorControl =
                        {
                            Text = message
                        },
                    Icon = SystemIcons.Warning,
                    pictureBox1 =
                        {
                            Image = SystemIcons.Warning.ToBitmap()
                        }
                };

            form.ShowDialog(parent);
        }

        protected override void OnLoad(EventArgs e)
        {
            var style = (int)(long)GetWindowLong32(new HandleRef(this, Handle), GWL_STYLE);
            SetWindowLongPtr32(new HandleRef(this, Handle), GWL_STYLE, new HandleRef(null, (IntPtr)(style & ~WS_CLIPSIBLINGS)));

            base.OnLoad(e);
        }

        private void ErrorViewerForm_Load(object sender, EventArgs e)
        {
            if (errorControl.Text.Length > 250)
            {
                var newSize = new Size((int)(Size.Width * 1.2), (int)(Size.Height * 1.2));
                Size = newSize;
            }
            else if (errorControl.Text.Length > 150)
            {
                var newSize = new Size((int)(Size.Width * 1.1), (int)(Size.Height * 1.1));
                Size = newSize;
            }

            MinimumSize = MaximumSize = Size;
        }

        private void MoreDetails_Click(object sender, EventArgs e)
        {
            if (_exWrapper != null)
                AdvancedInformationForm.ShowDetailsDialog(_exWrapper);
        }
    }
}
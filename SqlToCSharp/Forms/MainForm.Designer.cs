﻿namespace SqlToCSharp.Forms
{
    using SqlToCSharp.UserControls;

    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.topMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbConnectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pocoGenerateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateSimpleTypedDatatableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dbTreeView = new DbTreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cSharpCodeControl = new FastColoredTextBoxNS.FastColoredTextBox();
            this.classGeneratorSetting = new SqlToCSharp.UserControls.ClassGeneratorSettings();
            this.textBoxContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.topMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cSharpCodeControl)).BeginInit();
            this.textBoxContextMenu.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // topMenuStrip
            // 
            this.topMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.pocoGenerateMenuItem,
            this.generateSimpleTypedDatatableToolStripMenuItem});
            this.topMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.topMenuStrip.Name = "topMenuStrip";
            this.topMenuStrip.Size = new System.Drawing.Size(900, 24);
            this.topMenuStrip.TabIndex = 0;
            this.topMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dbConnectionMenuItem,
            this.toolStripSeparator2,
            this.saveToFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.fileToolStripMenuItem.Text = "&Menu";
            // 
            // dbConnectionMenuItem
            // 
            this.dbConnectionMenuItem.Name = "dbConnectionMenuItem";
            this.dbConnectionMenuItem.Size = new System.Drawing.Size(187, 22);
            this.dbConnectionMenuItem.Text = "&Database Connection";
            this.dbConnectionMenuItem.Click += new System.EventHandler(this.DbConnectionStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(184, 6);
            // 
            // saveToFileToolStripMenuItem
            // 
            this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            this.saveToFileToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.saveToFileToolStripMenuItem.Text = "&Save to file";
            this.saveToFileToolStripMenuItem.Click += new System.EventHandler(this.SaveToFileToolStripMenuItem_Click);
            // 
            // pocoGenerateMenuItem
            // 
            this.pocoGenerateMenuItem.Name = "pocoGenerateMenuItem";
            this.pocoGenerateMenuItem.Size = new System.Drawing.Size(114, 20);
            this.pocoGenerateMenuItem.Text = "Generate &C# Class";
            this.pocoGenerateMenuItem.Click += new System.EventHandler(this.PocoGenerateMenuItem_Click);
            // 
            // generateSimpleTypedDatatableToolStripMenuItem
            // 
            this.generateSimpleTypedDatatableToolStripMenuItem.Name = "generateSimpleTypedDatatableToolStripMenuItem";
            this.generateSimpleTypedDatatableToolStripMenuItem.Size = new System.Drawing.Size(194, 20);
            this.generateSimpleTypedDatatableToolStripMenuItem.Text = "Generate Simple &Typed Datatable";
            this.generateSimpleTypedDatatableToolStripMenuItem.Click += new System.EventHandler(this.GenerateSimpleTypedDatatableToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dbTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(900, 514);
            this.splitContainer1.SplitterDistance = 164;
            this.splitContainer1.TabIndex = 1;
            // 
            // dbTreeView
            // 
            this.dbTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbTreeView.Location = new System.Drawing.Point(0, 0);
            this.dbTreeView.Name = "dbTreeView";
            this.dbTreeView.Size = new System.Drawing.Size(162, 512);
            this.dbTreeView.TabIndex = 0;
            this.dbTreeView.GenerateCSharpClass += new System.EventHandler(this.DbTreeView_GenerateCSharpClass);
            this.dbTreeView.GenerateTypedDatatable += new System.EventHandler(this.DbTreeView_GenerateTypedDatatable);
            this.dbTreeView.SelectedNodeChanged += new System.Windows.Forms.TreeViewEventHandler(this.DbTreeView_SelectedNodeChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.classGeneratorSetting);
            this.splitContainer2.Size = new System.Drawing.Size(732, 514);
            this.splitContainer2.SplitterDistance = 472;
            this.splitContainer2.TabIndex = 0;
            // 
            // cSharpCodeControl
            // 
            this.cSharpCodeControl.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.cSharpCodeControl.AutoIndentCharsPatterns = "\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);\n";
            this.cSharpCodeControl.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.cSharpCodeControl.BackBrush = null;
            this.cSharpCodeControl.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.cSharpCodeControl.CharHeight = 14;
            this.cSharpCodeControl.CharWidth = 8;
            this.cSharpCodeControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cSharpCodeControl.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.cSharpCodeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cSharpCodeControl.IsReplaceMode = false;
            this.cSharpCodeControl.Language = FastColoredTextBoxNS.Language.CSharp;
            this.cSharpCodeControl.LeftBracket = '(';
            this.cSharpCodeControl.LeftBracket2 = '{';
            this.cSharpCodeControl.Location = new System.Drawing.Point(3, 3);
            this.cSharpCodeControl.Name = "cSharpCodeControl";
            this.cSharpCodeControl.Paddings = new System.Windows.Forms.Padding(0);
            this.cSharpCodeControl.ReadOnly = true;
            this.cSharpCodeControl.RightBracket = ')';
            this.cSharpCodeControl.RightBracket2 = '}';
            this.cSharpCodeControl.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.cSharpCodeControl.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("cSharpCodeControl.ServiceColors")));
            this.cSharpCodeControl.Size = new System.Drawing.Size(456, 480);
            this.cSharpCodeControl.TabIndex = 1;
            this.cSharpCodeControl.Zoom = 100;
            this.cSharpCodeControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CSharpCodeControl_MouseClick);
            // 
            // classGeneratorSetting
            // 
            this.classGeneratorSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classGeneratorSetting.Location = new System.Drawing.Point(0, 0);
            this.classGeneratorSetting.Name = "classGeneratorSetting";
            this.classGeneratorSetting.Size = new System.Drawing.Size(254, 512);
            this.classGeneratorSetting.TabIndex = 0;
            this.classGeneratorSetting.ClassGeneratorSettingsChangedEventHandler += new SqlToCSharp.UserControls.ClassGeneratorSettings.ClassGeneratorSettingsEventHandler(this.CreatorSettings_ClassSettingChangedEventHandler);
            // 
            // textBoxContextMenu
            // 
            this.textBoxContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToFileToolStripMenuItem1});
            this.textBoxContextMenu.Name = "textBoxContextMenu";
            this.textBoxContextMenu.Size = new System.Drawing.Size(132, 76);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
            // 
            // saveToFileToolStripMenuItem1
            // 
            this.saveToFileToolStripMenuItem1.Name = "saveToFileToolStripMenuItem1";
            this.saveToFileToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.saveToFileToolStripMenuItem1.Text = "Save to file";
            this.saveToFileToolStripMenuItem1.Click += new System.EventHandler(this.SaveToFileToolStripMenuItem_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(470, 512);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cSharpCodeControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(462, 486);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 538);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.topMenuStrip);
            this.MainMenuStrip = this.topMenuStrip;
            this.Name = "MainForm";
            this.Text = "Sql to C# Code generator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.topMenuStrip.ResumeLayout(false);
            this.topMenuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cSharpCodeControl)).EndInit();
            this.textBoxContextMenu.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip topMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem dbConnectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pocoGenerateMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private UserControls.ClassGeneratorSettings classGeneratorSetting;
        private System.Windows.Forms.ToolStripMenuItem generateSimpleTypedDatatableToolStripMenuItem;
        private DbTreeView dbTreeView;
        private FastColoredTextBoxNS.FastColoredTextBox cSharpCodeControl;
        private System.Windows.Forms.ContextMenuStrip textBoxContextMenu;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
    }
}
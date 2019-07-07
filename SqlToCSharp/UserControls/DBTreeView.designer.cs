﻿namespace SqlToCSharp.UserControls
{
    sealed partial class DbTreeView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TreeView = new System.Windows.Forms.TreeView();
            this.cntxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.filterSettingToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetFilterToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.generateCClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateSimpleTypedDatatableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvDBItems
            // 
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeView.HideSelection = false;
            this.TreeView.Location = new System.Drawing.Point(0, 0);
            this.TreeView.Name = "TreeView";
            this.TreeView.Size = new System.Drawing.Size(271, 565);
            this.TreeView.TabIndex = 1;
            this.TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvDbItems_AfterSelect);
            this.TreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvDbItems_NodeMouseClick);
            // 
            // cntxMenu
            // 
            this.cntxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterSettingToolItem,
            this.resetFilterToolItem,
            this.copyNameToolStripMenuItem,
            this.toolStripSeparator1,
            this.generateCClassToolStripMenuItem,
            this.generateSimpleTypedDatatableToolStripMenuItem});
            this.cntxMenu.Name = "cntxMenu";
            this.cntxMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cntxMenu.Size = new System.Drawing.Size(250, 120);
            // 
            // filterSettingToolItem
            // 
            this.filterSettingToolItem.Name = "filterSettingToolItem";
            this.filterSettingToolItem.Size = new System.Drawing.Size(249, 22);
            this.filterSettingToolItem.Text = "Filter Settings";
            this.filterSettingToolItem.Click += new System.EventHandler(this.FilterSettingToolItem_Click);
            // 
            // resetFilterToolItem
            // 
            this.resetFilterToolItem.Name = "resetFilterToolItem";
            this.resetFilterToolItem.Size = new System.Drawing.Size(249, 22);
            this.resetFilterToolItem.Text = "Reset Filter";
            this.resetFilterToolItem.Click += new System.EventHandler(this.ResetFilterToolItem_Click);
            // 
            // copyNameToolStripMenuItem
            // 
            this.copyNameToolStripMenuItem.Name = "copyNameToolStripMenuItem";
            this.copyNameToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.copyNameToolStripMenuItem.Text = "Copy Name";
            this.copyNameToolStripMenuItem.Click += new System.EventHandler(this.CopyNameToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(246, 6);
            // 
            // generateCClassToolStripMenuItem
            // 
            this.generateCClassToolStripMenuItem.Name = "generateCClassToolStripMenuItem";
            this.generateCClassToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.generateCClassToolStripMenuItem.Text = "Generate &C# Class";
            this.generateCClassToolStripMenuItem.Click += new System.EventHandler(this.GenerateCClassToolStripMenuItem_Click);
            // 
            // generateSimpleTypedDatatableToolStripMenuItem
            // 
            this.generateSimpleTypedDatatableToolStripMenuItem.Name = "generateSimpleTypedDatatableToolStripMenuItem";
            this.generateSimpleTypedDatatableToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.generateSimpleTypedDatatableToolStripMenuItem.Text = "Generate Simple &Typed Datatable";
            this.generateSimpleTypedDatatableToolStripMenuItem.Click += new System.EventHandler(this.GenerateSimpleTypedDatatableToolStripMenuItem_Click);
            // 
            // DBTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TreeView);
            this.Name = "DbTreeView";
            this.Size = new System.Drawing.Size(271, 565);
            this.cntxMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cntxMenu;
        private System.Windows.Forms.ToolStripMenuItem filterSettingToolItem;
        private System.Windows.Forms.ToolStripMenuItem resetFilterToolItem;
        private System.Windows.Forms.ToolStripMenuItem copyNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem generateCClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateSimpleTypedDatatableToolStripMenuItem;
    }
}

// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2004 Scott Willeke (http://scott.willeke.com)
//
// Authors:
//	Scott Willeke (scott@willeke.com)
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using LessMsi.Msi;
using LessMsi.UI.Model;
using Misc.Windows.Forms;

namespace LessMsi.UI
{
    internal class MainForm : Form, IMainFormView
    {
        public MainForm(string defaultInputFile)
        {
            
            InitializeComponent();
            msiTableGrid.AutoGenerateColumns = false;
            msiPropertyGrid.AutoGenerateColumns = false;
            Presenter = new MainFormPresenter(this);
            Presenter.Initialize();
            
            if (!string.IsNullOrEmpty(defaultInputFile))
                txtMsiFileName.Text = defaultInputFile;
        }

        private MainFormPresenter Presenter { get; set; }

        #region IMainFormView Implementation
        public void AddFileGridColumn(string boundPropertyName, string headerText)
        {
            DataGridViewColumn col = new DataGridViewTextBoxColumn { DataPropertyName = boundPropertyName, HeaderText = headerText };
            fileGrid.Columns.Add(col);
        }

        public FileInfo SelectedMsiFile
        {
            get
            {
                var file = new FileInfo(txtMsiFileName.Text);
                if (!file.Exists)
                {
                    Presenter.Error(string.Concat("File \'", file.FullName, "\' does not exist."), null);
                    return null;
                }
                return file;
            }
        }

        public string SelectedTableName
        {
            get { return cboTable.Text; }
        }

        public void ChangeUiEnabled(bool doEnable)
        {
            btnExtract.Enabled = doEnable;
            cboTable.Enabled = doEnable;
        }

        public MsiPropertyInfo SelectedMsiProperty
        {
            get {
                if (msiPropertyGrid.SelectedRows.Count > 0)
                    return msiPropertyGrid.SelectedRows[0].DataBoundItem as MsiPropertyInfo;
                else
                    return null;
            }
        }

        public string PropertySummaryDescription 
        {
            get { return txtSummaryDescription.Text; }
            set { txtSummaryDescription.Text = value; }
        }

        public void ShowUserMessageBox(string message)
        {
            MessageBox.Show(this, message, "LessMSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region MSI Table Grid Stuff
        public void AddTableViewGridColumn(string headerText)
        {
            DataGridViewColumn col = new DataGridViewTextBoxColumn
                                     	{
                                     		HeaderText = headerText,
                                     		Resizable = DataGridViewTriState.True
                                     	};
        	msiTableGrid.Columns.Add(col);
        }

        public void ClearTableViewGridColumns()
        {
            msiTableGrid.Columns.Clear();
        }

        public void SetTableViewGridDataSource(IEnumerable<object[]> values)
        {
            msiTableGrid.Rows.Clear();
            foreach (var row in values)
            {
                msiTableGrid.Rows.Add(row);
            }
        }

        #region Property Grid Stuff
        public void SetPropertyGridDataSource(MsiPropertyInfo[] props)
        {
            msiPropertyGrid.DataSource = props;
        }
        
        public void AddPropertyGridColumn(string boundPropertyName, string headerText)
        {
            DataGridViewColumn col = new DataGridViewTextBoxColumn { DataPropertyName = boundPropertyName, HeaderText = headerText };
            msiPropertyGrid.Columns.Add(col);
        }
        #endregion

        #endregion

        #endregion

        #region Designer Stuff
        // ReSharper disable InconsistentNaming
        private TextBox txtMsiFileName;
        private Label label1;
        private Button btnBrowse;
        private TabControl tabs;
        private TabPage tabExtractFiles;
        private TabPage tabTableView;
        public ComboBox cboTable;
        private Label label2;
        private Panel panel1;
        private Button btnExtract;
        private FolderBrowserDialog folderBrowser;
        private OpenFileDialog openMsiDialog;
        private StatusBar statusBar1;
        internal StatusBarPanel statusPanelDefault;
        private StatusBarPanel statusPanelFileCount;
        private Button btnSelectAll;
        private Button btnUnselectAll;
        private TabPage tabSummary;
        private TextBox txtSummaryDescription;
        private GroupBox grpDescription;
        private Panel panel2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem preferencesToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        public DataGridView fileGrid;
        private DataGridView msiTableGrid;
        private DataGridView msiPropertyGrid;
        // ReSharper restore InconsistentNaming
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
			this.txtMsiFileName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.tabs = new System.Windows.Forms.TabControl();
			this.tabExtractFiles = new System.Windows.Forms.TabPage();
			this.fileGrid = new System.Windows.Forms.DataGridView();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnSelectAll = new System.Windows.Forms.Button();
			this.btnUnselectAll = new System.Windows.Forms.Button();
			this.btnExtract = new System.Windows.Forms.Button();
			this.tabTableView = new System.Windows.Forms.TabPage();
			this.msiTableGrid = new System.Windows.Forms.DataGridView();
			this.cboTable = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tabSummary = new System.Windows.Forms.TabPage();
			this.msiPropertyGrid = new System.Windows.Forms.DataGridView();
			this.grpDescription = new System.Windows.Forms.GroupBox();
			this.txtSummaryDescription = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
			this.openMsiDialog = new System.Windows.Forms.OpenFileDialog();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.statusPanelDefault = new System.Windows.Forms.StatusBarPanel();
			this.statusPanelFileCount = new System.Windows.Forms.StatusBarPanel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabs.SuspendLayout();
			this.tabExtractFiles.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tabTableView.SuspendLayout();
			this.tabSummary.SuspendLayout();
			this.grpDescription.SuspendLayout();
			this.panel1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtMsiFileName
			// 
			this.txtMsiFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtMsiFileName.Location = new System.Drawing.Point(46, 4);
			this.txtMsiFileName.Name = "txtMsiFileName";
			this.txtMsiFileName.Size = new System.Drawing.Size(257, 20);
			this.txtMsiFileName.TabIndex = 0;
			this.txtMsiFileName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ReloadCurrentUIOnEnterKeyPress);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(26, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "&File:";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnBrowse.Location = new System.Drawing.Point(309, 5);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(20, 17);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "...";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.tabExtractFiles);
			this.tabs.Controls.Add(this.tabTableView);
			this.tabs.Controls.Add(this.tabSummary);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Location = new System.Drawing.Point(0, 51);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(336, 299);
			this.tabs.TabIndex = 0;
			this.tabs.TabStop = false;
			// 
			// tabExtractFiles
			// 
			this.tabExtractFiles.Controls.Add(this.fileGrid);
			this.tabExtractFiles.Controls.Add(this.panel2);
			this.tabExtractFiles.Location = new System.Drawing.Point(4, 22);
			this.tabExtractFiles.Name = "tabExtractFiles";
			this.tabExtractFiles.Padding = new System.Windows.Forms.Padding(5);
			this.tabExtractFiles.Size = new System.Drawing.Size(328, 273);
			this.tabExtractFiles.TabIndex = 0;
			this.tabExtractFiles.Text = "Extract Files";
			// 
			// fileGrid
			// 
			this.fileGrid.AllowUserToAddRows = false;
			this.fileGrid.AllowUserToDeleteRows = false;
			this.fileGrid.AllowUserToOrderColumns = true;
			this.fileGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.fileGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fileGrid.Location = new System.Drawing.Point(5, 5);
			this.fileGrid.Name = "fileGrid";
			this.fileGrid.ReadOnly = true;
			this.fileGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.fileGrid.Size = new System.Drawing.Size(318, 231);
			this.fileGrid.TabIndex = 5;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btnSelectAll);
			this.panel2.Controls.Add(this.btnUnselectAll);
			this.panel2.Controls.Add(this.btnExtract);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(5, 236);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(318, 32);
			this.panel2.TabIndex = 4;
			// 
			// btnSelectAll
			// 
			this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSelectAll.Location = new System.Drawing.Point(0, 8);
			this.btnSelectAll.Name = "btnSelectAll";
			this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
			this.btnSelectAll.TabIndex = 1;
			this.btnSelectAll.Text = "Select &All";
			this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
			// 
			// btnUnselectAll
			// 
			this.btnUnselectAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnUnselectAll.Location = new System.Drawing.Point(88, 8);
			this.btnUnselectAll.Name = "btnUnselectAll";
			this.btnUnselectAll.Size = new System.Drawing.Size(75, 23);
			this.btnUnselectAll.TabIndex = 2;
			this.btnUnselectAll.Text = "&Unselect All";
			this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
			// 
			// btnExtract
			// 
			this.btnExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExtract.Enabled = false;
			this.btnExtract.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnExtract.Location = new System.Drawing.Point(241, 8);
			this.btnExtract.Name = "btnExtract";
			this.btnExtract.Size = new System.Drawing.Size(75, 23);
			this.btnExtract.TabIndex = 3;
			this.btnExtract.Text = "E&xtract";
			this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
			// 
			// tabTableView
			// 
			this.tabTableView.Controls.Add(this.msiTableGrid);
			this.tabTableView.Controls.Add(this.cboTable);
			this.tabTableView.Controls.Add(this.label2);
			this.tabTableView.Location = new System.Drawing.Point(4, 22);
			this.tabTableView.Name = "tabTableView";
			this.tabTableView.Size = new System.Drawing.Size(328, 273);
			this.tabTableView.TabIndex = 1;
			this.tabTableView.Text = "Table View";
			// 
			// msiTableGrid
			// 
			this.msiTableGrid.AllowUserToAddRows = false;
			this.msiTableGrid.AllowUserToDeleteRows = false;
			this.msiTableGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.msiTableGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.msiTableGrid.Location = new System.Drawing.Point(3, 34);
			this.msiTableGrid.Name = "msiTableGrid";
			this.msiTableGrid.ReadOnly = true;
			this.msiTableGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.msiTableGrid.Size = new System.Drawing.Size(322, 236);
			this.msiTableGrid.TabIndex = 10;
			// 
			// cboTable
			// 
			this.cboTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cboTable.Enabled = false;
			this.cboTable.Location = new System.Drawing.Point(40, 7);
			this.cboTable.Name = "cboTable";
			this.cboTable.Size = new System.Drawing.Size(285, 21);
			this.cboTable.TabIndex = 8;
			this.cboTable.Text = "File";
			this.cboTable.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			this.cboTable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ReloadCurrentUIOnEnterKeyPress);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(0, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "&Table";
			// 
			// tabSummary
			// 
			this.tabSummary.Controls.Add(this.msiPropertyGrid);
			this.tabSummary.Controls.Add(this.grpDescription);
			this.tabSummary.Location = new System.Drawing.Point(4, 22);
			this.tabSummary.Name = "tabSummary";
			this.tabSummary.Padding = new System.Windows.Forms.Padding(5);
			this.tabSummary.Size = new System.Drawing.Size(328, 273);
			this.tabSummary.TabIndex = 2;
			this.tabSummary.Text = "Summary";
			// 
			// msiPropertyGrid
			// 
			this.msiPropertyGrid.AllowUserToAddRows = false;
			this.msiPropertyGrid.AllowUserToDeleteRows = false;
			this.msiPropertyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.msiPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.msiPropertyGrid.Location = new System.Drawing.Point(5, 5);
			this.msiPropertyGrid.Name = "msiPropertyGrid";
			this.msiPropertyGrid.ReadOnly = true;
			this.msiPropertyGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.msiPropertyGrid.Size = new System.Drawing.Size(318, 171);
			this.msiPropertyGrid.TabIndex = 3;
			// 
			// grpDescription
			// 
			this.grpDescription.Controls.Add(this.txtSummaryDescription);
			this.grpDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.grpDescription.Location = new System.Drawing.Point(5, 176);
			this.grpDescription.Name = "grpDescription";
			this.grpDescription.Size = new System.Drawing.Size(318, 92);
			this.grpDescription.TabIndex = 2;
			this.grpDescription.TabStop = false;
			this.grpDescription.Text = "Description:";
			// 
			// txtSummaryDescription
			// 
			this.txtSummaryDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtSummaryDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSummaryDescription.Location = new System.Drawing.Point(3, 16);
			this.txtSummaryDescription.Multiline = true;
			this.txtSummaryDescription.Name = "txtSummaryDescription";
			this.txtSummaryDescription.ReadOnly = true;
			this.txtSummaryDescription.Size = new System.Drawing.Size(312, 73);
			this.txtSummaryDescription.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtMsiFileName);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.btnBrowse);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(336, 27);
			this.panel1.TabIndex = 0;
			// 
			// openMsiDialog
			// 
			this.openMsiDialog.DefaultExt = "msi";
			this.openMsiDialog.Filter = "msierablefiles|*.msi|All Files|*.*";
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 350);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusPanelDefault,
            this.statusPanelFileCount});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(336, 16);
			this.statusBar1.TabIndex = 2;
			// 
			// statusPanelDefault
			// 
			this.statusPanelDefault.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusPanelDefault.Name = "statusPanelDefault";
			this.statusPanelDefault.Width = 209;
			// 
			// statusPanelFileCount
			// 
			this.statusPanelFileCount.Name = "statusPanelFileCount";
			this.statusPanelFileCount.Width = 110;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(336, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.toolStripSeparator1,
            this.preferencesToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
			// 
			// preferencesToolStripMenuItem
			// 
			this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
			this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.preferencesToolStripMenuItem.Text = "&Preferences";
			this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(336, 366);
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(272, 184);
			this.Name = "MainForm";
			this.Text = "Less MSIérables";
			this.tabs.ResumeLayout(false);
			this.tabExtractFiles.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.tabTableView.ResumeLayout(false);
			this.tabTableView.PerformLayout();
			this.tabSummary.ResumeLayout(false);
			this.grpDescription.ResumeLayout(false);
			this.grpDescription.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
       
        #endregion

        #region UI Event Handlers
        private void OpenFileCommand()
        {
            if (DialogResult.OK != openMsiDialog.ShowDialog(this))
                return;
            txtMsiFileName.Text = openMsiDialog.FileName;
            Presenter.LoadCurrentFile();
			//to make sure shortcut keys for menuitems work properly select a grid:
			if (tabs.SelectedTab == tabExtractFiles)
				fileGrid.Select();
			else if (tabs.SelectedTab == tabTableView)
				msiTableGrid.Select();
			else if (tabs.SelectedTab == tabSummary)
				msiPropertyGrid.Select();
        }

        private void ReloadCurrentUIOnEnterKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                Presenter.LoadCurrentFile();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Presenter.UpdateMSiTableGrid();
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            //TODO: Refactor to Presenter
            var selectedFiles = new List<MsiFile>();
            if (fileGrid.SelectedRows.Count == 0)
            {
                ShowUserMessageBox("Please select some or all of the files to extract them.");
                return;
            }

            if (folderBrowser.SelectedPath == null || folderBrowser.SelectedPath.Length <= 0)
                folderBrowser.SelectedPath = SelectedMsiFile.DirectoryName;

            if (DialogResult.OK != folderBrowser.ShowDialog(this))
                return;

            btnExtract.Enabled = false;
            using (var progressDialog = BeginShowingProgressDialog())
            {
                try
                {
                    DirectoryInfo outputDir = new DirectoryInfo(folderBrowser.SelectedPath);
                    foreach (DataGridViewRow row in fileGrid.SelectedRows)
                    {
                        MsiFileItemView fileToExtract = (MsiFileItemView)row.DataBoundItem;
                        selectedFiles.Add(fileToExtract.File);
                    }

                    FileInfo msiFile = SelectedMsiFile;
                    if (msiFile == null)
                        return;
                    var filesToExtract = selectedFiles.ToArray();
                    Wixtracts.ExtractFiles(msiFile, outputDir, filesToExtract,
                                           new AsyncCallback(progressDialog.UpdateProgress));
                }
                catch (Exception err)
                {
                    MessageBox.Show(this,
                                    "The following error occured extracting the MSI: " + err.ToString(), "MSI Error!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                }
            }
            btnExtract.Enabled = true;
        }

        private ExtractionProgressDialog BeginShowingProgressDialog()
        {
            var progressDialog = new ExtractionProgressDialog(this);
            progressDialog.Show();
            progressDialog.Update();
            return progressDialog;
        }

        #endregion

        
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            Presenter.ToggleSelectAllFiles(true);
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            Presenter.ToggleSelectAllFiles(false);
        }

        private void msiPropertyGrid_SelectionChanged(object sender, EventArgs e)
        {
            Presenter.OnSelectedPropertyChanged();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new PreferencesForm();
            frm.ShowDialog(this);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileCommand();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileCommand();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // copying the currently selected row in the currently selected ListView here:
            var grid = ActiveControl as DataGridView;
            WinFormsHelper.CopySelectedDataGridRowsToClipboard(grid);
        }
    }
}
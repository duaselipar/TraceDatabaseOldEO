using System.Drawing;
using System.Windows.Forms;

namespace TraceDatababeOld4
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Connection
        private GroupBox gbConnect;
        private Label lblHost;
        private Label lblPort;
        private Label lblUser;
        private Label lblPass;
        private TextBox txtHost;
        private TextBox txtPort;
        private TextBox txtUser;
        private TextBox txtPass;
        private Button btnConnect;
        private Label lblConnStatus;

        // Database select
        private GroupBox gbDbSelect;
        private ComboBox cbDatabases;
        private Button btnSelectDb;

        // Trace panel
        private GroupBox gbTrace;
        private Label lblNpcActionId;
        private TextBox txtNpcActionId;
        private Button btnTrace;

        // Output
        private GroupBox gbOutput;
        private RichTextBox rtbOutput;
        private FlowLayoutPanel pnlOutputBtns;
        private Button btnSelectAll;
        private Button btnCopyAll;
        private Button btnClear;

        // Layout
        private TableLayoutPanel tableMain;
        private TableLayoutPanel rowDbTrace;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.gbConnect = new System.Windows.Forms.GroupBox();
            this.lblHost = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblConnStatus = new System.Windows.Forms.Label();
            this.rowDbTrace = new System.Windows.Forms.TableLayoutPanel();
            this.gbDbSelect = new System.Windows.Forms.GroupBox();
            this.cbDatabases = new System.Windows.Forms.ComboBox();
            this.btnSelectDb = new System.Windows.Forms.Button();
            this.gbTrace = new System.Windows.Forms.GroupBox();
            this.lblNpcActionId = new System.Windows.Forms.Label();
            this.txtNpcActionId = new System.Windows.Forms.TextBox();
            this.btnTrace = new System.Windows.Forms.Button();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.pnlOutputBtns = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnCopyAll = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.tableMain.SuspendLayout();
            this.gbConnect.SuspendLayout();
            this.rowDbTrace.SuspendLayout();
            this.gbDbSelect.SuspendLayout();
            this.gbTrace.SuspendLayout();
            this.gbOutput.SuspendLayout();
            this.pnlOutputBtns.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableMain
            // 
            this.tableMain.ColumnCount = 1;
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.Controls.Add(this.gbConnect, 0, 0);
            this.tableMain.Controls.Add(this.rowDbTrace, 0, 1);
            this.tableMain.Controls.Add(this.gbOutput, 0, 2);
            this.tableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableMain.Location = new System.Drawing.Point(0, 0);
            this.tableMain.Name = "tableMain";
            this.tableMain.Padding = new System.Windows.Forms.Padding(12);
            this.tableMain.RowCount = 3;
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.Size = new System.Drawing.Size(964, 935);
            this.tableMain.TabIndex = 0;
            // 
            // gbConnect
            // 
            this.gbConnect.Controls.Add(this.lblHost);
            this.gbConnect.Controls.Add(this.txtHost);
            this.gbConnect.Controls.Add(this.lblPort);
            this.gbConnect.Controls.Add(this.txtPort);
            this.gbConnect.Controls.Add(this.lblUser);
            this.gbConnect.Controls.Add(this.txtUser);
            this.gbConnect.Controls.Add(this.lblPass);
            this.gbConnect.Controls.Add(this.txtPass);
            this.gbConnect.Controls.Add(this.btnConnect);
            this.gbConnect.Controls.Add(this.lblConnStatus);
            this.gbConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbConnect.Location = new System.Drawing.Point(15, 15);
            this.gbConnect.Name = "gbConnect";
            this.gbConnect.Padding = new System.Windows.Forms.Padding(12);
            this.gbConnect.Size = new System.Drawing.Size(934, 94);
            this.gbConnect.TabIndex = 0;
            this.gbConnect.TabStop = false;
            this.gbConnect.Text = "Connection";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(16, 32);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(53, 15);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "IP / Host";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(100, 28);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(100, 23);
            this.txtHost.TabIndex = 1;
            this.txtHost.Text = "127.0.0.1";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(206, 32);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 15);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(290, 28);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 23);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "3306";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(396, 30);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(30, 15);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "User";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(480, 26);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 23);
            this.txtUser.TabIndex = 5;
            this.txtUser.Text = "test";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(585, 30);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(57, 15);
            this.lblPass.TabIndex = 6;
            this.lblPass.Text = "Password";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(669, 26);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(100, 23);
            this.txtPass.TabIndex = 7;
            this.txtPass.Text = "test123";
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(785, 24);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(110, 30);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            // 
            // lblConnStatus
            // 
            this.lblConnStatus.AutoSize = true;
            this.lblConnStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblConnStatus.Location = new System.Drawing.Point(16, 65);
            this.lblConnStatus.Name = "lblConnStatus";
            this.lblConnStatus.Size = new System.Drawing.Size(124, 15);
            this.lblConnStatus.TabIndex = 9;
            this.lblConnStatus.Text = "Status: Not connected";
            // 
            // rowDbTrace
            // 
            this.rowDbTrace.ColumnCount = 2;
            this.rowDbTrace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.87234F));
            this.rowDbTrace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.12766F));
            this.rowDbTrace.Controls.Add(this.gbDbSelect, 0, 0);
            this.rowDbTrace.Controls.Add(this.gbTrace, 1, 0);
            this.rowDbTrace.Dock = System.Windows.Forms.DockStyle.Top;
            this.rowDbTrace.Location = new System.Drawing.Point(12, 118);
            this.rowDbTrace.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.rowDbTrace.Name = "rowDbTrace";
            this.rowDbTrace.RowCount = 1;
            this.rowDbTrace.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.rowDbTrace.Size = new System.Drawing.Size(940, 90);
            this.rowDbTrace.TabIndex = 10;
            // 
            // gbDbSelect
            // 
            this.gbDbSelect.Controls.Add(this.cbDatabases);
            this.gbDbSelect.Controls.Add(this.btnSelectDb);
            this.gbDbSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDbSelect.Location = new System.Drawing.Point(0, 0);
            this.gbDbSelect.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.gbDbSelect.Name = "gbDbSelect";
            this.gbDbSelect.Padding = new System.Windows.Forms.Padding(12);
            this.gbDbSelect.Size = new System.Drawing.Size(444, 90);
            this.gbDbSelect.TabIndex = 1;
            this.gbDbSelect.TabStop = false;
            this.gbDbSelect.Text = "Select Database";
            // 
            // cbDatabases
            // 
            this.cbDatabases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabases.Enabled = false;
            this.cbDatabases.Location = new System.Drawing.Point(19, 35);
            this.cbDatabases.Name = "cbDatabases";
            this.cbDatabases.Size = new System.Drawing.Size(313, 23);
            this.cbDatabases.TabIndex = 0;
            // 
            // btnSelectDb
            // 
            this.btnSelectDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectDb.Enabled = false;
            this.btnSelectDb.Location = new System.Drawing.Point(341, 34);
            this.btnSelectDb.Name = "btnSelectDb";
            this.btnSelectDb.Size = new System.Drawing.Size(90, 26);
            this.btnSelectDb.TabIndex = 1;
            this.btnSelectDb.Text = "Select";
            // 
            // gbTrace
            // 
            this.gbTrace.Controls.Add(this.lblNpcActionId);
            this.gbTrace.Controls.Add(this.txtNpcActionId);
            this.gbTrace.Controls.Add(this.btnTrace);
            this.gbTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTrace.Location = new System.Drawing.Point(456, 0);
            this.gbTrace.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.gbTrace.Name = "gbTrace";
            this.gbTrace.Padding = new System.Windows.Forms.Padding(12);
            this.gbTrace.Size = new System.Drawing.Size(484, 90);
            this.gbTrace.TabIndex = 2;
            this.gbTrace.TabStop = false;
            this.gbTrace.Text = "Trace Action";
            // 
            // lblNpcActionId
            // 
            this.lblNpcActionId.AutoSize = true;
            this.lblNpcActionId.Location = new System.Drawing.Point(14, 40);
            this.lblNpcActionId.Name = "lblNpcActionId";
            this.lblNpcActionId.Size = new System.Drawing.Size(83, 15);
            this.lblNpcActionId.TabIndex = 0;
            this.lblNpcActionId.Text = "NPC Action ID";
            // 
            // txtNpcActionId
            // 
            this.txtNpcActionId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNpcActionId.Enabled = false;
            this.txtNpcActionId.Location = new System.Drawing.Point(118, 36);
            this.txtNpcActionId.Name = "txtNpcActionId";
            this.txtNpcActionId.Size = new System.Drawing.Size(262, 23);
            this.txtNpcActionId.TabIndex = 1;
            // 
            // btnTrace
            // 
            this.btnTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrace.Enabled = false;
            this.btnTrace.Location = new System.Drawing.Point(390, 35);
            this.btnTrace.Name = "btnTrace";
            this.btnTrace.Size = new System.Drawing.Size(75, 26);
            this.btnTrace.TabIndex = 2;
            this.btnTrace.Text = "Trace ->";
            // 
            // gbOutput
            // 
            this.gbOutput.Controls.Add(this.pnlOutputBtns);
            this.gbOutput.Controls.Add(this.rtbOutput);
            this.gbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOutput.Location = new System.Drawing.Point(15, 217);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Padding = new System.Windows.Forms.Padding(12);
            this.gbOutput.Size = new System.Drawing.Size(934, 703);
            this.gbOutput.TabIndex = 3;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "Output";
            // 
            // pnlOutputBtns
            // 
            this.pnlOutputBtns.Controls.Add(this.btnSelectAll);
            this.pnlOutputBtns.Controls.Add(this.btnCopyAll);
            this.pnlOutputBtns.Controls.Add(this.btnClear);
            this.pnlOutputBtns.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOutputBtns.Location = new System.Drawing.Point(12, 28);
            this.pnlOutputBtns.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlOutputBtns.Name = "pnlOutputBtns";
            this.pnlOutputBtns.Size = new System.Drawing.Size(910, 31);
            this.pnlOutputBtns.TabIndex = 0;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.AutoSize = true;
            this.btnSelectAll.Location = new System.Drawing.Point(3, 3);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 25);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "Select All";
            // 
            // btnCopyAll
            // 
            this.btnCopyAll.AutoSize = true;
            this.btnCopyAll.Location = new System.Drawing.Point(84, 3);
            this.btnCopyAll.Name = "btnCopyAll";
            this.btnCopyAll.Size = new System.Drawing.Size(75, 25);
            this.btnCopyAll.TabIndex = 1;
            this.btnCopyAll.Text = "Copy All";
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = true;
            this.btnClear.Location = new System.Drawing.Point(165, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 25);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            // 
            // rtbOutput
            // 
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbOutput.Location = new System.Drawing.Point(12, 28);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(910, 663);
            this.rtbOutput.TabIndex = 1;
            this.rtbOutput.Text = "";
            this.rtbOutput.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 935);
            this.Controls.Add(this.tableMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(980, 680);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trace Database | DuaSelipar Tools";
            this.tableMain.ResumeLayout(false);
            this.gbConnect.ResumeLayout(false);
            this.gbConnect.PerformLayout();
            this.rowDbTrace.ResumeLayout(false);
            this.gbDbSelect.ResumeLayout(false);
            this.gbTrace.ResumeLayout(false);
            this.gbTrace.PerformLayout();
            this.gbOutput.ResumeLayout(false);
            this.pnlOutputBtns.ResumeLayout(false);
            this.pnlOutputBtns.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

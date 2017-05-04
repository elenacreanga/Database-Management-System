namespace DbManagementSystem.UI.UserControls
{
    partial class QueryControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.dbComboBox = new System.Windows.Forms.ComboBox();
            this.qtComboBox = new System.Windows.Forms.ComboBox();
            this.tableComboBox = new System.Windows.Forms.ComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.whereColumnBox = new System.Windows.Forms.ComboBox();
            this.opBox = new System.Windows.Forms.ComboBox();
            this.whereValTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectPanel = new System.Windows.Forms.Panel();
            this.wherePanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.selectPanel.SuspendLayout();
            this.wherePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select databese";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Query Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Select Table";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Select columns and Values";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Select Where Columns Values";
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(266, 312);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 5;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // dbComboBox
            // 
            this.dbComboBox.FormattingEnabled = true;
            this.dbComboBox.Location = new System.Drawing.Point(235, 27);
            this.dbComboBox.Name = "dbComboBox";
            this.dbComboBox.Size = new System.Drawing.Size(121, 21);
            this.dbComboBox.TabIndex = 6;
            this.dbComboBox.SelectedIndexChanged += new System.EventHandler(this.dbComboBox_SelectedIndexChanged);
            // 
            // qtComboBox
            // 
            this.qtComboBox.FormattingEnabled = true;
            this.qtComboBox.Location = new System.Drawing.Point(235, 72);
            this.qtComboBox.Name = "qtComboBox";
            this.qtComboBox.Size = new System.Drawing.Size(121, 21);
            this.qtComboBox.TabIndex = 7;
            this.qtComboBox.SelectedIndexChanged += new System.EventHandler(this.qtComboBox_SelectedIndexChanged);
            // 
            // tableComboBox
            // 
            this.tableComboBox.FormattingEnabled = true;
            this.tableComboBox.Location = new System.Drawing.Point(235, 108);
            this.tableComboBox.Name = "tableComboBox";
            this.tableComboBox.Size = new System.Drawing.Size(121, 21);
            this.tableComboBox.TabIndex = 8;
            this.tableComboBox.SelectedIndexChanged += new System.EventHandler(this.tableComboBox_SelectedIndexChanged);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.value,
            this.check});
            this.dataGridView.Location = new System.Drawing.Point(144, 17);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView.Size = new System.Drawing.Size(308, 80);
            this.dataGridView.TabIndex = 9;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            // 
            // value
            // 
            this.value.HeaderText = "Value";
            this.value.Name = "value";
            // 
            // check
            // 
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.check.Width = 30;
            // 
            // whereColumnBox
            // 
            this.whereColumnBox.FormattingEnabled = true;
            this.whereColumnBox.Location = new System.Drawing.Point(159, 15);
            this.whereColumnBox.Name = "whereColumnBox";
            this.whereColumnBox.Size = new System.Drawing.Size(99, 21);
            this.whereColumnBox.TabIndex = 10;
            // 
            // opBox
            // 
            this.opBox.FormattingEnabled = true;
            this.opBox.Location = new System.Drawing.Point(268, 15);
            this.opBox.Name = "opBox";
            this.opBox.Size = new System.Drawing.Size(40, 21);
            this.opBox.TabIndex = 11;
            // 
            // whereValTextBox
            // 
            this.whereValTextBox.Location = new System.Drawing.Point(314, 15);
            this.whereValTextBox.Name = "whereValTextBox";
            this.whereValTextBox.Size = new System.Drawing.Size(76, 20);
            this.whereValTextBox.TabIndex = 12;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // selectPanel
            // 
            this.selectPanel.AutoScroll = true;
            this.selectPanel.Controls.Add(this.dataGridView);
            this.selectPanel.Controls.Add(this.label4);
            this.selectPanel.Location = new System.Drawing.Point(65, 145);
            this.selectPanel.Name = "selectPanel";
            this.selectPanel.Size = new System.Drawing.Size(483, 100);
            this.selectPanel.TabIndex = 14;
            // 
            // wherePanel
            // 
            this.wherePanel.Controls.Add(this.label5);
            this.wherePanel.Controls.Add(this.whereColumnBox);
            this.wherePanel.Controls.Add(this.opBox);
            this.wherePanel.Controls.Add(this.whereValTextBox);
            this.wherePanel.Location = new System.Drawing.Point(65, 251);
            this.wherePanel.Name = "wherePanel";
            this.wherePanel.Size = new System.Drawing.Size(412, 55);
            this.wherePanel.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(65, 341);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Import";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // QueryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.wherePanel);
            this.Controls.Add(this.selectPanel);
            this.Controls.Add(this.tableComboBox);
            this.Controls.Add(this.qtComboBox);
            this.Controls.Add(this.dbComboBox);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "QueryControl";
            this.Size = new System.Drawing.Size(608, 475);
            this.Load += new System.EventHandler(this.QueryControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.selectPanel.ResumeLayout(false);
            this.selectPanel.PerformLayout();
            this.wherePanel.ResumeLayout(false);
            this.wherePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.ComboBox dbComboBox;
        private System.Windows.Forms.ComboBox qtComboBox;
        private System.Windows.Forms.ComboBox tableComboBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox whereColumnBox;
        private System.Windows.Forms.ComboBox opBox;
        private System.Windows.Forms.TextBox whereValTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel selectPanel;
        private System.Windows.Forms.Panel wherePanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

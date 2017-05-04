namespace DbManagementSystem.UI.UserControls
{
    partial class TableControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableNameLabel = new System.Windows.Forms.Label();
            this.tableNameTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableGridView = new System.Windows.Forms.DataGridView();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.dbNameBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCancel = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tableGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableNameLabel
            // 
            this.tableNameLabel.AutoSize = true;
            this.tableNameLabel.Location = new System.Drawing.Point(78, 40);
            this.tableNameLabel.Name = "tableNameLabel";
            this.tableNameLabel.Size = new System.Drawing.Size(68, 13);
            this.tableNameLabel.TabIndex = 0;
            this.tableNameLabel.Text = " Table Name";
            // 
            // tableNameTextBox
            // 
            this.tableNameTextBox.Location = new System.Drawing.Point(158, 40);
            this.tableNameTextBox.Name = "tableNameTextBox";
            this.tableNameTextBox.Size = new System.Drawing.Size(121, 20);
            this.tableNameTextBox.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tableGridView
            // 
            this.tableGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnName,
            this.columnType,
            this.columnCancel});
            this.tableGridView.Location = new System.Drawing.Point(61, 66);
            this.tableGridView.Name = "tableGridView";
            this.tableGridView.Size = new System.Drawing.Size(366, 190);
            this.tableGridView.TabIndex = 3;
            this.tableGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableGridView_CellContentClick);
            // 
            // databaseLabel
            // 
            this.databaseLabel.AutoSize = true;
            this.databaseLabel.Location = new System.Drawing.Point(68, 16);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(84, 13);
            this.databaseLabel.TabIndex = 4;
            this.databaseLabel.Text = "Database Name";
            // 
            // dbNameBox
            // 
            this.dbNameBox.FormattingEnabled = true;
            this.dbNameBox.Location = new System.Drawing.Point(158, 13);
            this.dbNameBox.Name = "dbNameBox";
            this.dbNameBox.Size = new System.Drawing.Size(121, 21);
            this.dbNameBox.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(185, 262);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // columnName
            // 
            this.columnName.HeaderText = "Column Name";
            this.columnName.Name = "columnName";
            this.columnName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnName.Width = 150;
            // 
            // columnType
            // 
            this.columnType.HeaderText = "Type";
            this.columnType.Name = "columnType";
            // 
            // columnCancel
            // 
            this.columnCancel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "X";
            this.columnCancel.DefaultCellStyle = dataGridViewCellStyle2;
            this.columnCancel.HeaderText = "";
            this.columnCancel.Name = "columnCancel";
            this.columnCancel.ReadOnly = true;
            this.columnCancel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnCancel.Width = 30;
            // 
            // TableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.dbNameBox);
            this.Controls.Add(this.databaseLabel);
            this.Controls.Add(this.tableGridView);
            this.Controls.Add(this.tableNameTextBox);
            this.Controls.Add(this.tableNameLabel);
            this.Name = "TableControl";
            this.Size = new System.Drawing.Size(516, 366);
            this.Load += new System.EventHandler(this.TableControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tableNameLabel;
        private System.Windows.Forms.TextBox tableNameTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataGridView tableGridView;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.ComboBox dbNameBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnType;
        private System.Windows.Forms.DataGridViewButtonColumn columnCancel;
    }
}

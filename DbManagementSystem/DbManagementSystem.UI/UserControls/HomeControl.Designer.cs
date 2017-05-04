using System.Windows.Forms;

namespace DbManagementSystem.UI.UserControls
{
    partial class HomeControl
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
            this.tableListBox = new System.Windows.Forms.ListBox();
            this.dbListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // tableListBox
            // 
            this.tableListBox.FormattingEnabled = true;
            this.tableListBox.Location = new System.Drawing.Point(353, 98);
            this.tableListBox.Name = "tableListBox";
            this.tableListBox.Size = new System.Drawing.Size(214, 173);
            this.tableListBox.TabIndex = 6;
            this.tableListBox.SelectedIndexChanged += new System.EventHandler(this.tableListBox_SelectedIndexChanged);
            this.tableListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tableListBox_SelectedIndexChanged);
            // 
            // dbListBox
            // 
            this.dbListBox.FormattingEnabled = true;
            this.dbListBox.Location = new System.Drawing.Point(69, 98);
            this.dbListBox.Name = "dbListBox";
            this.dbListBox.Size = new System.Drawing.Size(213, 173);
            this.dbListBox.TabIndex = 5;
            this.dbListBox.SelectedIndexChanged += new System.EventHandler(this.dbListBox_SelectedIndexChanged);
            this.dbListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dbListBox_SelectedIndexChangedd);
            // 
            // HomeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableListBox);
            this.Controls.Add(this.dbListBox);
            this.Name = "HomeControl";
            this.Size = new System.Drawing.Size(640, 338);
            this.Load += new System.EventHandler(this.HomeControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox tableListBox;
        private System.Windows.Forms.ListBox dbListBox;
    }
}

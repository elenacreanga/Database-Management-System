namespace DbManagementSystem.UI.UserControls
{
    partial class RenameDBControl
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
            this.dBNameLabel = new System.Windows.Forms.Label();
            this.dBNameTextBox = new System.Windows.Forms.TextBox();
            this.saveDBNameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dBNameLabel
            // 
            this.dBNameLabel.AutoSize = true;
            this.dBNameLabel.Location = new System.Drawing.Point(105, 161);
            this.dBNameLabel.Name = "dBNameLabel";
            this.dBNameLabel.Size = new System.Drawing.Size(58, 13);
            this.dBNameLabel.TabIndex = 0;
            this.dBNameLabel.Text = "Db Name: ";
            // 
            // dBNameTextBox
            // 
            this.dBNameTextBox.Location = new System.Drawing.Point(169, 158);
            this.dBNameTextBox.Name = "dBNameTextBox";
            this.dBNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.dBNameTextBox.TabIndex = 1;
            // 
            // saveDBNameButton
            // 
            this.saveDBNameButton.Location = new System.Drawing.Point(294, 155);
            this.saveDBNameButton.Name = "saveDBNameButton";
            this.saveDBNameButton.Size = new System.Drawing.Size(75, 23);
            this.saveDBNameButton.TabIndex = 2;
            this.saveDBNameButton.Text = "Save";
            this.saveDBNameButton.UseVisualStyleBackColor = true;
            this.saveDBNameButton.Click += new System.EventHandler(this.saveDBNameButton_Click);
            // 
            // RenameDBControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.saveDBNameButton);
            this.Controls.Add(this.dBNameTextBox);
            this.Controls.Add(this.dBNameLabel);
            this.Name = "RenameDBControl";
            this.Size = new System.Drawing.Size(540, 318);
            this.Load += new System.EventHandler(this.RenameDBControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dBNameLabel;
        private System.Windows.Forms.TextBox dBNameTextBox;
        private System.Windows.Forms.Button saveDBNameButton;
    }
}

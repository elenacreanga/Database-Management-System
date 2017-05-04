namespace DbManagementSystem.UI.UserControls
{
    partial class ResultControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RowsAffectedLabel = new System.Windows.Forms.Label();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.resultdataView = new System.Windows.Forms.DataGridView();
            this.csvbutton = new System.Windows.Forms.Button();
            this.xmlButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.resultdataView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rows Affected : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Message : ";
            // 
            // RowsAffectedLabel
            // 
            this.RowsAffectedLabel.AutoSize = true;
            this.RowsAffectedLabel.Location = new System.Drawing.Point(130, 32);
            this.RowsAffectedLabel.Name = "RowsAffectedLabel";
            this.RowsAffectedLabel.Size = new System.Drawing.Size(74, 13);
            this.RowsAffectedLabel.TabIndex = 2;
            this.RowsAffectedLabel.Text = "RowsAffected";
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(130, 64);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(53, 13);
            this.MessageLabel.TabIndex = 3;
            this.MessageLabel.Text = "Message ";
            // 
            // resultdataView
            // 
            this.resultdataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultdataView.Location = new System.Drawing.Point(41, 98);
            this.resultdataView.Name = "resultdataView";
            this.resultdataView.Size = new System.Drawing.Size(447, 150);
            this.resultdataView.TabIndex = 4;
            // 
            // csvbutton
            // 
            this.csvbutton.Location = new System.Drawing.Point(41, 274);
            this.csvbutton.Name = "csvbutton";
            this.csvbutton.Size = new System.Drawing.Size(75, 23);
            this.csvbutton.TabIndex = 5;
            this.csvbutton.Text = "Export_CSV";
            this.csvbutton.UseVisualStyleBackColor = true;
            this.csvbutton.Click += new System.EventHandler(this.csvbutton_Click);
            // 
            // xmlButton
            // 
            this.xmlButton.Location = new System.Drawing.Point(186, 274);
            this.xmlButton.Name = "xmlButton";
            this.xmlButton.Size = new System.Drawing.Size(75, 23);
            this.xmlButton.TabIndex = 6;
            this.xmlButton.Text = "Export_XML";
            this.xmlButton.UseVisualStyleBackColor = true;
            this.xmlButton.Click += new System.EventHandler(this.xmlButton_Click);
            // 
            // ResultControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xmlButton);
            this.Controls.Add(this.csvbutton);
            this.Controls.Add(this.resultdataView);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.RowsAffectedLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ResultControl";
            this.Size = new System.Drawing.Size(555, 398);
            this.Load += new System.EventHandler(this.ResultControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.resultdataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label RowsAffectedLabel;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.DataGridView resultdataView;
        private System.Windows.Forms.Button csvbutton;
        private System.Windows.Forms.Button xmlButton;
    }
}

namespace DbManagementSystem.UI
{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.newDbButton = new System.Windows.Forms.Button();
            this.queryButton = new System.Windows.Forms.Button();
            this.NewTableButton = new System.Windows.Forms.Button();
            this.Home = new System.Windows.Forms.Button();
            this.container = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // newDbButton
            // 
            resources.ApplyResources(this.newDbButton, "newDbButton");
            this.newDbButton.Name = "newDbButton";
            this.newDbButton.UseVisualStyleBackColor = true;
            this.newDbButton.Click += new System.EventHandler(this.newDbButton_Click);
            // 
            // queryButton
            // 
            resources.ApplyResources(this.queryButton, "queryButton");
            this.queryButton.Name = "queryButton";
            this.queryButton.UseVisualStyleBackColor = true;
            this.queryButton.Click += new System.EventHandler(this.queryButton_Click);
            // 
            // NewTableButton
            // 
            resources.ApplyResources(this.NewTableButton, "NewTableButton");
            this.NewTableButton.Name = "NewTableButton";
            this.NewTableButton.UseVisualStyleBackColor = true;
            this.NewTableButton.Click += new System.EventHandler(this.NewTableButton_Click);
            // 
            // Home
            // 
            resources.ApplyResources(this.Home, "Home");
            this.Home.Name = "Home";
            this.Home.UseVisualStyleBackColor = true;
            this.Home.Click += new System.EventHandler(this.Home_Click);
            // 
            // container
            // 
            resources.ApplyResources(this.container, "container");
            this.container.Name = "container";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.container);
            this.Controls.Add(this.Home);
            this.Controls.Add(this.NewTableButton);
            this.Controls.Add(this.queryButton);
            this.Controls.Add(this.newDbButton);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newDbButton;
        private System.Windows.Forms.Button queryButton;
        private System.Windows.Forms.Button NewTableButton;
        private System.Windows.Forms.Button Home;
        private System.Windows.Forms.Panel container;
    }
}


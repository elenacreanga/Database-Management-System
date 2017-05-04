using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbManagementSystem.UI.UserControls
{
    public partial class RenameDBControl : UserControl
    {
        private IntegrationTestsRunner _runner;

        public  string alterDBName;

        public Delegate userSaveControl;

        public RenameDBControl(IntegrationTestsRunner runner)
        {
            InitializeComponent();
            _runner = runner;
        }

        private void saveDBNameButton_Click(object sender, EventArgs e)
        {
            if (alterDBName == String.Empty)
            {
                _runner.CreateDatabase(dBNameTextBox.Text);
            }
            else
            {
                _runner.AlterDatabase(alterDBName,dBNameTextBox.Text);
            }
            userSaveControl.DynamicInvoke();
        }

        public void setDBName(string name)
        {
            dBNameTextBox.Text = name;
            alterDBName = name;
        }

        private void RenameDBControl_Load(object sender, EventArgs e)
        {

        }
    }
}

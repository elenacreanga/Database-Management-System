using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace DbManagementSystem.UI.UserControls
{
    public partial class HomeControl : UserControl
    {
        private IntegrationTestsRunner _runner;
        public HomeControl(IntegrationTestsRunner runner)
        {
            InitializeComponent();
            _runner = runner;
        }
        
        public Delegate userFunctionPointer;
        public Delegate tableEditPointer;

        private void HomeControl_Load(object sender, EventArgs e)
        {
            Init(0);
        }

        public void Init(int id)
        {
            dbListBox.Items.Clear();
            var databases = _runner.GetDatabaseList();
            if (databases.Count() > 0)
            {
                foreach (var db in databases)
                {
                    dbListBox.Items.Add(db.Name);
                }
                dbListBox.SetSelected(id, true);
            }
          

        }


        private void dbListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableListBox.Items.Clear();
            var tables = _runner.GetDatabaseTableList(dbListBox.SelectedItem.ToString());

            foreach (var table in tables)
            {
                tableListBox.Items.Add(table.Name);
            }
        }

        private void dbListBox_SelectedIndexChangedd(object sender, EventArgs e)
        {
            userFunctionPointer.DynamicInvoke(dbListBox.SelectedItem.ToString());
        }


        private void tableListBox_SelectedIndexChanged(object sender, MouseEventArgs e)
        {
            tableEditPointer.DynamicInvoke(dbListBox.SelectedItem,tableListBox.SelectedItem);
        }

        private void tableListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbManagementSystem.Core.Database;

namespace DbManagementSystem.UI.UserControls
{
    public partial class TableControl : UserControl
    {
        private IntegrationTestsRunner _runner;

        public Delegate tablePointer;

        public TableControl(IntegrationTestsRunner runner)
        {
            InitializeComponent();
            _runner = runner;
        }

        private void TableControl_Load(object sender, EventArgs e)
        {

        }

        public void SetProperties(string dbName, string tableName)
        {
            Init();
            var databases = _runner.GetDatabaseList().ToList();
            if (databases.Count > 0)
            {
                foreach (var db in databases)
                {
                    dbNameBox.Items.Add(db.Name);
                }

                if (dbName == tableName && dbName == string.Empty)
                {
                    dbNameBox.SelectedIndex = 0;
                }
                else
                {
                    tableNameTextBox.Text = tableName;
                    tableNameTextBox.Enabled = false;
                    dbNameBox.Text = dbName;
                    dbNameBox.Enabled = false;
                    Dictionary<string, string> columns = _runner.GetTable(dbName, tableName).Columns;

                    CreateTableView(columns);

                }
            }
        }

        private void Init()
        {
            tableGridView.Rows.Clear();
            tableNameTextBox.Text=String.Empty;
            dbNameBox.Text = String.Empty;
            tableNameTextBox.Enabled = true;
            dbNameBox.Enabled = true;
            dbNameBox.Items.Clear();
        }

        private void CreateTableView(Dictionary<string, string> columns)
        {
            tableGridView.Rows.Clear();
            //tabetGridView.ColumnCount = previewList[0].Count;
            tableGridView.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            tableGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            string[] row = new string[3];
            foreach (var c in columns)
            {
                row[0] = c.Key;
                row[1] = c.Value;
                row[2] = "X";
                tableGridView.Rows.Add(row);
            }

        }

        private void tableGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tableGridView.Rows[e.RowIndex];

                if (row.Cells["ColumnCancel"].Value != null)
                {
                    var name = row.Cells["ColumnName"].Value.ToString();
                    var type = row.Cells["ColumnType"].Value.ToString();

                    _runner.AlterTable(dbNameBox.Text, tableNameTextBox.Text, "REMOVE", name);

                    SetProperties(dbNameBox.Text, tableNameTextBox.Text);
                }
                else
                {
                    try
                    {
                        tableGridView.Rows.Remove(row);
                    }
                    catch
                    {
                    }
                }

            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string columns ="";
            string name;
            string type;
            if (tableGridView.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in tableGridView.Rows)
                {
                    try
                    {
                        name = row.Cells["columnName"].Value.ToString();
                        type = row.Cells["columnType"].Value.ToString();
                        columns += name + ":" + type + ",";
                    }
                    catch
                    {
                      
                    }
                }
                columns = columns.Remove(columns.Length - 1, 1);
                if (dbNameBox.Text == String.Empty)
                {
                    MessageBox.Show("No Database");
                }
                else
                {
                    var result = _runner.CreateTable(dbNameBox.Text, tableNameTextBox.Text, columns);
                    if (result.Success)
                    {
                        tablePointer.DynamicInvoke();
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Columns");
            }
           
        }
        }
    }

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbManagementSystem.Core.Database;
using DbManagementSystem.Core.Database.TableImporters;

namespace DbManagementSystem.UI.UserControls
{
    public partial class QueryControl : UserControl
    {
        private IntegrationTestsRunner _runner;

        public Delegate queryPointer;

        public QueryControl(IntegrationTestsRunner runner)
        {
            InitializeComponent();
            _runner = runner;
            Init();
        }

        public void Init()
        {
            dbComboBox.Items.Clear();
           
            qtComboBox.Items.Clear();

            whereColumnBox.Text = String.Empty;
            opBox.Text = String.Empty;
            whereValTextBox.Text = String.Empty;


            qtComboBox.Items.Add("Select");
            qtComboBox.Items.Add("Insert");
            qtComboBox.Items.Add("Update");
            qtComboBox.Items.Add("Delete");
            qtComboBox.SelectedIndex = 0;

            var databases = _runner.GetDatabaseList();
            if (databases.Count() > 0)
            {
                foreach (var db in databases)
                {
                    dbComboBox.Items.Add(db.Name);
                }
                dbComboBox.SelectedIndex = 0;

                SetTableBox();

                SetWhereView();
            }

        }

        private void SetWhereView()
        {
            if (tableComboBox.Text != String.Empty && dbComboBox.Text != String.Empty)
            {
                whereColumnBox.Items.Clear();
                opBox.Items.Clear();
                Dictionary<string, string> columns = _runner.GetTable(dbComboBox.Text, tableComboBox.Text).Columns;
                if (columns.Count > 0)
                {
                    foreach (var col in columns)
                    {
                        whereColumnBox.Items.Add(col.Key);
                    }

                    opBox.Items.Add(">");
                    opBox.Items.Add("=");
                    opBox.Items.Add("<");
                    opBox.Items.Add("<>");
                    opBox.Items.Add(">=");
                    opBox.Items.Add("<=");

                    wherePanel.Enabled = true;
                }
            }
            else
            {
                wherePanel.Enabled = false;
            }
        }

        private void SetTableBox()
        {
            tableComboBox.Items.Clear();
            List<Table> tables = _runner.GetDatabaseTableList(dbComboBox.Text).ToList();
            if (tables.Count > 0)
            {
                foreach (var db in tables)
                {
                    tableComboBox.Items.Add(db.Name);
                }
                tableComboBox.SelectedIndex = 0;
            }
        }

        private void dbComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableComboBox.Items.Clear();
            tableComboBox.Text = String.Empty;
            SetTableBox();
            SetWhereView();
            qtComboBox_SelectedIndexChanged(sender, e);
        }

        private void qtComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tableComboBox.Text != String.Empty)
            {
                string qt = qtComboBox.Text;

                wherePanel.Enabled = true;
                selectPanel.Enabled = true;

                SetWhereView();

                switch (qt)
                {
                    case "Select":
                        CreteTableView(qt);
                        dataGridView.Columns[1].Visible = false;
                        break;
                    case "Update":
                        CreteTableView(qt);
                        break;
                    case "Insert":
                        wherePanel.Enabled = false;
                        CreteTableView(qt);
                        dataGridView.Columns[2].Visible = false;
                        break;
                    case "Delete":
                        selectPanel.Enabled = false;
                        break;
                }
            }

        }

        private void CreteTableView(string qt)
        {
            dataGridView.Rows.Clear();

            dataGridView.Columns[0].Visible = true;
            dataGridView.Columns[1].Visible = true;
            dataGridView.Columns[2].Visible = true;

            dataGridView.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // setDates
            // dataGridView.Columns[1].Visible = false;
            dataGridView.AutoSize = true;
            Dictionary<string, string> columns = _runner.GetTable(dbComboBox.Text, tableComboBox.Text).Columns;

            string[] row = new string[3];

            foreach (var c in columns)
            {
                row[0] = c.Key;
                dataGridView.Rows.Add(row);
            }

        }


        private void runButton_Click(object sender, EventArgs e)
        {
            
            string tableName = tableComboBox.Text;
            string dbName = dbComboBox.Text;
            string qt = qtComboBox.Text;
            string query = String.Empty;
            string columns = String.Empty;
            if (tableName == String.Empty || dbName == String.Empty || qt == String.Empty)
            {
                MessageBox.Show("Error");
            }
            else
            {
                query =  CreateQuery(qt,tableName, dbName);
                if (query != String.Empty)
                {
                  var result  =_runner.ExecuteQuery(query, dbName, tableName);
                  queryPointer.DynamicInvoke(result, query, dbName);
                }
                else
                {
                    MessageBox.Show("Error");
                }
                
            }
        }

        private string CreateQuery(string qt,string tableName, string dbName)
        {
            string name;
            string type;
            string query = String.Empty;
            string columns = String.Empty;
            string values = String.Empty;
            string col = String.Empty;
            string where = String.Empty;
            string check;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                try
                {
                    name = row.Cells["name"].Value ==null?"": row.Cells["name"].Value.ToString();
                    type = row.Cells["value"].Value == null?"": row.Cells["value"].Value.ToString();
                    check = row.Cells["check"].Value == null ? "" : row.Cells["check"].Value.ToString();
                    switch (qt)
                    {
                        case "Select":
                            if (check == "True")
                            {
                                columns += name + ",";
                            }
                            break;
                        case "Update":
                            if (check == "True")
                            {
                                columns += name + "=" + type + ",";
                            }
                            break;
                        case "Insert":
                            col += name + ",";
                            values += "'"+type+"'" + ",";
                            break;
                    }
                }
                catch
                {
                }
            }
            if (columns != String.Empty)
            {
                columns = columns.Remove(columns.Length - 1, 1);
            }
            if (columns == String.Empty && qt== "Select")
            {
                columns = "*" ;
            }

            if (whereColumnBox.Text != String.Empty && opBox.Text != String.Empty && whereValTextBox.Text != String.Empty)
            {
                where = " WHERE " + whereColumnBox.Text + opBox.Text + whereValTextBox.Text;
            }
            switch (qt)
            {
                case "Select":
                    query = qt + " " + columns + " FROM " + tableName + where;
                    break;
                case "Update":
                    query = qt + " " + tableName + " SET " + columns + where;
                    break;
                case "Insert":
                    col = col.Remove(col.Length - 1, 1);
                    values = values.Remove(values.Length - 1, 1);
                    query = qt + " " + "INTO " + tableName + "(" + col + ") VALUES(" + values + ")";
                    break;
                case "Delete":
                    query = qt + " " + "FROM " + tableName + where;
                    break;
            }
            return query;
        }

        private void QueryControl_Load(object sender, EventArgs e)
        {

        }

        private void tableComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  SetWhereView();
            qtComboBox_SelectedIndexChanged(sender,e);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ITableImporter importer;
                if (openFileDialog1.FileName.EndsWith("csv", StringComparison.OrdinalIgnoreCase))
                {
                    importer = new CsvTableImporter();
                }
                else
                {
                    importer = new XmlTableImporter();
                }

                string data = File.ReadAllText(openFileDialog1.FileName);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    var success = importer.Import(_runner.GetConn(dbComboBox.Text), tableComboBox.Text, data);
                    MessageBox.Show(data);
                }
                else
                {
                    MessageBox.Show("No content ");
                }
            }
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbManagementSystem.Core.Query;
using DbManagementSystem.Core.Query.QueryResultSerializers;

namespace DbManagementSystem.UI.UserControls
{
    public partial class ResultControl : UserControl
    {
        private IntegrationTestsRunner _runner;
        public IQueryResultSerializer serializer;
        public string Query;
        public string DbName;

        public ResultControl(IntegrationTestsRunner runner)
        {
            InitializeComponent();
            _runner = runner;
        }

        private void ResultControl_Load(object sender, EventArgs e)
        {

        }

        public void  SetDates(IQueryResult result,string query, string dbName)
        {
            Query = query;
            DbName = dbName;

            resultdataView.Rows.Clear();
            RowsAffectedLabel.Text = result.RowsAffected.ToString();
            MessageLabel.Text = result.Message;
          
                resultdataView.Enabled = true;
                csvbutton.Enabled = true;
                xmlButton.Enabled = true;

                PrintResultData(result);

            if (resultdataView.Rows.Count == 0)
            {
                resultdataView.Enabled = false;
                csvbutton.Enabled = false;
                xmlButton.Enabled = false;
            }
        }
        private  void PrintResultData(IQueryResult result)
        {
            resultdataView.Rows.Clear();
            resultdataView.Columns.Clear();
            //tabetGridView.ColumnCount = previewList[0].Count;
            resultdataView.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            resultdataView.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            resultdataView.AllowUserToAddRows = false;
            var columns = result.GetColumnNames();
            foreach (var col in columns)
            {
                DataGridViewColumn c = new DataGridViewColumn();
                c.Name = col;
                DataGridViewCell cell = new DataGridViewTextBoxCell();

               // cell.Style.BackColor = Color.Wheat;
                c.CellTemplate = cell;

                resultdataView.Columns.Add(c);
            }


            string[] row = new string[columns.Length];
           
            int contor;
            while (result.Read())
            {
                contor = 0;
                foreach (var column in columns)
                {
                    row[contor++] = result.GetValue(column).ToString();
                }
                resultdataView.Rows.Add(row);

            }
        }

        private void csvbutton_Click(object sender, EventArgs e)
        {
            var rez = _runner.ExecuteQuery(Query, DbName);
            serializer = new CsvQueryResultSerializer();
            var csv = serializer.Serialize(rez);
            MessageBox.Show(csv);
        }

        private void xmlButton_Click(object sender, EventArgs e)
        {
            var rez = _runner.ExecuteQuery(Query, DbName);
            serializer = new XmlQueryResultSerializer();
            var xml = serializer.Serialize(rez);
            MessageBox.Show(xml);
        }
    }
}

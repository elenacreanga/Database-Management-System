using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbManagementSystem.Core.Database;
using DbManagementSystem.Core.Query;
using DbManagementSystem.UI.UserControls;

namespace DbManagementSystem.UI
{
    public partial class MainForm : Form
    {
        private IntegrationTestsRunner _runner { get; set; }

        private HomeControl _home { get; set; }
        private RenameDBControl _renameDBControl { get; set; }
        private TableControl _tableControl { get; set; }
        private QueryControl _queryControl { get; set; }
        private ResultControl _resultControl{ get; set; }



        // public delegate void controlcall(object sender, EventArgs e);

        public delegate void controlcall(object sender, EventArgs e);
        public delegate void functioncall2(string dbName, string tableName);
        public delegate void functioncall(string message);
        public delegate void renamecall();
        public delegate void queryResultcall(IQueryResult result, string query, string dbName);

        private event functioncall2 editTablePointer;
        private event functioncall formFunctionPointer;
        private event renamecall renameDbNamePointer;
        private event queryResultcall queryPointer;
        private event renamecall tablePointer;



        public MainForm()
        {
            InitializeComponent();
            _runner = new IntegrationTestsRunner();
            InitUserControls(_runner);
        }


        private void InitUserControls(IntegrationTestsRunner runner)
        {
            _home = new HomeControl(runner);
            formFunctionPointer += new functioncall(RenameDB);
            editTablePointer += new functioncall2(EditTable);
            _home.userFunctionPointer = formFunctionPointer;
            _home.tableEditPointer = editTablePointer;
         //------------------------------------------------------------

            _renameDBControl = new RenameDBControl(runner);
             renameDbNamePointer += new renamecall(HomePage);
            _renameDBControl.userSaveControl = renameDbNamePointer;
        //------------------------------------------------------------
            _tableControl = new TableControl(runner);
            tablePointer += new renamecall(HomePage);
            _tableControl.tablePointer = tablePointer;
         //------------------------------------------------------------
            _queryControl = new QueryControl(runner);
            queryPointer += new queryResultcall(ShowResult);
            _queryControl.queryPointer = queryPointer;
            //------------------------------------------------------------
            _resultControl = new ResultControl(runner);    
        }

        private void ShowResult(IQueryResult result, string query, string dbName)
        {
            UpdatePanel(_resultControl);
            _resultControl.SetDates(result, query, dbName);
        }

        private void EditTable(string dbName, string tableName)
        {
            UpdatePanel(_tableControl);
            _tableControl.SetProperties(dbName, tableName);
            
        }

        private void HomePage()
        {
            UpdatePanel(_home);
           _home.Init(0);
        }

        private void RenameDB(string message)
        {
            UpdatePanel(_renameDBControl);
            _renameDBControl.alterDBName= String.Empty;
            _renameDBControl.setDBName(message);
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            container.Controls.Add(_home);
        }

        private void newDbButton_Click(object sender, EventArgs e)
        {
            UpdatePanel(_renameDBControl);
            _renameDBControl.setDBName(String.Empty);

        }

        // ******* user controll*****
      
        //*************************************
        private void Home_Click(object sender, EventArgs e)
        {
            HomePage();
        }

        private void UpdatePanel(UserControl panel)
        {
            container.Controls.Clear();
            container.Controls.Add(panel);
        }

        private void NewTableButton_Click(object sender, EventArgs e)
        {
            UpdatePanel(_tableControl);
            _tableControl.SetProperties(String.Empty, String.Empty);
        }

        private void queryButton_Click(object sender, EventArgs e)
        {
            UpdatePanel(_queryControl);
            _queryControl.Init();
        }
    }
}

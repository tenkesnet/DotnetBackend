using ADODB;
using Dapper;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JScript;
using MyDataGrid.Models;
using MyDataGrid.Services;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Convert = System.Convert;

namespace MyDataGrid
{
    public partial class MainForm : Form
    {
        string filePath;
        List<TableSheet> listExcelSheets = new List<TableSheet>();
        List<TableSheet> listExcelSheets2 = new List<TableSheet>();
        List<DataTable> dataTables = new List<DataTable>();
        ComboBox cmbBetoltAdatbazisbol = new ComboBox();
        TabControl tabCtrl = new TabControl();
        Procedures procedures = new Procedures();
        MultiReturn multiReturn = new MultiReturn();
        public DapperContext context = new DapperContext();

        public MainForm()
        {
            InitializeComponent();
            
        }
        public void button1_Click(object sender, EventArgs e)
        {
            filePath = procedures.dialogOpen(openFileDialog1);
            if (listExcelSheets.Count != null)
            {
                listExcelSheets.Clear();
                tabCtrl.TabPages.Clear();
                this.Controls.Remove(tabCtrl);
                listExcelSheets2.Clear();
            }
            listExcelSheets = procedures.getSheetNumbers(filePath);
            foreach (TableSheet tempSheet in listExcelSheets)
            {
                multiReturn = procedures.readFromExcel(tempSheet);
                tabcontrolFeltoltes(tempSheet, tempSheet.sheetName, multiReturn);
                dtgOszlopSzelessegIgazitas(tempSheet);
            }
        }
        public void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void button2_ClickAsync(object sender, EventArgs e)
        {
            procedures.writeToDatabase(filePath, listExcelSheets2);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            cmbBetoltAdatbazisbol = procedures.cmbBetolt();
            this.Controls.Add(cmbBetoltAdatbazisbol);
            cmbBetoltAdatbazisbol.SelectedIndexChanged += CmbBetoltAdatbazisbol_SelectedIndexChanged;
        }
        private void CmbBetoltAdatbazisbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabCtrl.TabPages.Clear();
            this.Controls.Remove(tabCtrl);
            string kivalasztottFajl = cmbBetoltAdatbazisbol.SelectedItem.ToString();
            int excelFileNameId = context.connection.QuerySingle<int>("select id from excel where excel_file_name=@fajl",new {fajl=kivalasztottFajl,});
            var fileSheets = context.connection.Query("select sheet_name from sheet_name " +
                    "where excel_file_name_id=@id",
                new
                {
                    id = excelFileNameId,
                });
            foreach (var sheetName in fileSheets)
            {
                TableSheet tempSheet = new TableSheet();
                multiReturn = procedures.readFromDataBase(sheetName);
                tabcontrolFeltoltes(tempSheet, sheetName.sheet_name, multiReturn);
                dtgOszlopSzelessegIgazitas(tempSheet);
            }
            //textBox1.Text = strtemp;
            //tabControlTabellak = procedures.cmbSelectedIndexChanged(tabControlTabellak, cmbBetoltAdatbazisbol);
            //this.Controls.Add(tabControlTabellak);
        }
        void dtgOszlopSzelessegIgazitas(TableSheet tempSheet1)
        {
            int q = 0;
            while (q < tempSheet1.dtg.Columns.Count)
            {
                tempSheet1.dtg.Columns[q].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                q++;
            }
        }
        void tabcontrolFeltoltes(TableSheet tempSheet, string tabName, MultiReturn multiReturn1)
        {
            tempSheet.dtg = new DataGridView();
            dataTables.Add(multiReturn1.multiReturnDataTable);
            this.Controls.Add(tabCtrl);
            tabCtrl.Height = 550;
            tabCtrl.Width = 800;
            tabCtrl.Location = new Point(20, 80);
            tempSheet.dtg.DataSource = multiReturn1.multiReturnDataTable;
            tempSheet.dtg.Size = new Size(800, 400);
            tempSheet.dtg.Location = new Point(10, 10);
            tempSheet.dtg.Name = tabName;
            TabPage page = new TabPage(tabName);
            page.Controls.Add(tempSheet.dtg);
            tabCtrl.TabPages.Add(page);
            listExcelSheets2.Add(multiReturn1.multiReturnTableSheet);
            tabCtrl.SelectedIndexChanged += TabCtrl_SelectedIndexChanged;
        }

        private void TabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textBox1.Text = dataTables[tabCtrl.SelectedIndex].Rows[2].Sum(x => x.    tabCtrl.SelectedIndex.ToString();
            //DataRow dtrow = dataTables[tabCtrl.SelectedIndex].Rows[0];
            //textBox2.Text = dtrow[2].ToString();
            //throw new NotImplementedException();
        }
        /* public double[] statisztika(int sti)     //A megkapott index a megyét adja, azaz egyúttal a megyei tabellák listájának indexét
    {
        double[] tomb = new double [3];
        double merkozosenkentiGolatlag;
        double gol;
        double lottgol;
        double merkozes;
        double atlagCsapatGol;
        double merkozesSzam;

        atlagCsapatGol = Math.Round(tabellak[sti].tableSor.Average(x => x.lott_golok), 3);
        merkozesSzam = tabellak[sti].tableSor.Sum(x => x.osszes_merkozes) / 2;
        lottgol = tabellak[sti].tableSor.Sum(y => y.lott_golok);
        merkozes = tabellak[sti].tableSor.Sum(x => x.osszes_merkozes) / 2;
        gol = lottgol / merkozes;
        merkozosenkentiGolatlag = Math.Round(gol, 3);
        tomb[0] = atlagCsapatGol;
        tomb[1] = merkozesSzam;
        tomb[2] = merkozosenkentiGolatlag;
        return tomb;
    }*/
    }
}
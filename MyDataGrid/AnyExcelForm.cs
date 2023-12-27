using Dapper;
using ExcelDataReader;
using MyDataGrid.Models;
using MyDataGrid.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDataGrid
{
    public partial class AnyExcelForm : Form
    {
        //MainForm mainForm=new MainForm();
        string filePath;
        //TableSheet sheet;
        List<TableSheet> listExcelSheets = new List<TableSheet>();
        List<TableSheet> listExcelSheets2 = new List<TableSheet>();
        List<DataTable> dataTables = new List<DataTable>();
        ComboBox cmbBetoltAdatbazisbol = new ComboBox();
        TabControl tabCtrl = new TabControl();
        Procedures procedures = new Procedures();
        MultiReturn multiReturn = new MultiReturn();
        public DapperContext context = new DapperContext();

        public AnyExcelForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            filePath = procedures.dialogOpen(openFileDialog1);
            if (listExcelSheets.Count!=null)
            {
                listExcelSheets.Clear();
                tabCtrl.TabPages.Clear();
                this.Controls.Remove(tabCtrl);
                listExcelSheets2.Clear();
            }
            listExcelSheets = procedures.getSheetNumbers(filePath);
            foreach (TableSheet tempSheet in listExcelSheets)
            {
                multiReturn =procedures.readFromExcel(tempSheet);
                tabcontrolFeltoltes(tempSheet, tempSheet.sheetName, multiReturn);
                dtgOszlopSzelessegIgazitas(tempSheet);
            }       
        }
        private void button2_Click(object sender, EventArgs e)
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
            var excelFileName = context.connection.QuerySingle<string>("select excel_file_name from excel where id=@id",
                new
                {
                    id = cmbBetoltAdatbazisbol.SelectedIndex + 1,
                });
            var fileSheets = context.connection.Query("select sheet_name from sheet_name " +
                    "where excel_file_name_id=@id",
                new
                {
                    id = cmbBetoltAdatbazisbol.SelectedIndex + 1,
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
            tabCtrl.Width = 1230;
            tabCtrl.Location = new Point(20, 80);
            tempSheet.dtg.DataSource = multiReturn1.multiReturnDataTable;
            tempSheet.dtg.Size = new Size(800, 400);
            tempSheet.dtg.Location = new Point(10, 10);
            tempSheet.dtg.Name = tabName;
            TabPage page = new TabPage(tabName);
            page.Controls.Add(tempSheet.dtg);
            tabCtrl.TabPages.Add(page);
            listExcelSheets2.Add(multiReturn1.multiReturnTableSheet);
        }
    }
}

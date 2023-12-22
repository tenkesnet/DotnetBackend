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
        MainForm mainForm=new MainForm();
        string filePath;
        TableSheet sheet;
        List<TableSheet> listExcelSheets = new List<TableSheet>();
        List<TableSheet> listExcelSheets2 = new List<TableSheet>();
        List<DataTable> dataTables = new List<DataTable>();
        ComboBox cmbBetoltAdatbazisbol = new ComboBox();
        TabControl tabCtrl = new TabControl();
        Procedures procedures = new Procedures();
        
        public AnyExcelForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //TableSheet tblSheet = new TableSheet();
            filePath = procedures.dialogOpen(openFileDialog1);
            if (listExcelSheets.Count!=null)
            {
                listExcelSheets.Clear();
                tabCtrl.TabPages.Clear();
                this.Controls.Remove(tabCtrl);
                listExcelSheets2.Clear();
            }
            listExcelSheets = procedures.getSheetNumbers(filePath);
            tabCtrl.Height = 500;
            tabCtrl.Width = 930;
            tabCtrl.Location = new Point(20, 80);
            this.Controls.Add(tabCtrl);
            foreach (TableSheet tempSheet in listExcelSheets)
            {
                tempSheet.dtg = new DataGridView();
                MultiReturn multiReturn = new MultiReturn();
                DataTable dt = new DataTable();
                multiReturn=procedures.readFromExcel(tempSheet);
                multiReturn.multiReturnDataTable.TableName = tempSheet.sheetName;
                dataTables.Add(multiReturn.multiReturnDataTable);
                tempSheet.dtg.DataSource = multiReturn.multiReturnDataTable;
                tempSheet.dtg.Size = new Size(800, 400);
                tempSheet.dtg.Location = new Point(10, 10);
                tempSheet.dtg.Name = tempSheet.sheetName;
                TabPage page = new TabPage(tempSheet.sheetName);
                multiReturn.multiReturnTableSheet.sheetName = tempSheet.sheetName;
                page.Controls.Add(tempSheet.dtg);
                tabCtrl.TabPages.Add(page);
                listExcelSheets2.Add(multiReturn.multiReturnTableSheet);
                int q = 0;
                while (q < tempSheet.dtg.Columns.Count)
                {
                    
                    tempSheet.dtg.Columns[q].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    q++;
                }
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
            string strtemp;
            tabCtrl=procedures.readFromDataBase(cmbBetoltAdatbazisbol.SelectedIndex+1);
            this.Controls.Add(tabCtrl);
            tabCtrl.Height = 550;
            tabCtrl.Width = 1230;
            tabCtrl.Location = new Point(20, 80);

            //textBox1.Text = strtemp;
            //tabControlTabellak = procedures.cmbSelectedIndexChanged(tabControlTabellak, cmbBetoltAdatbazisbol);
            //this.Controls.Add(tabControlTabellak);
        }
    }
}

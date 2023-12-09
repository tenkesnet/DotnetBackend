using MyDataGrid.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        //public DataTable dtExcelTable;
        Sheet sheet;
        //public DataRow dr;
        List<Sheet> listExcelSheets = new List<Sheet>();
        public ComboBox cmbBetoltAdatbazisbol = new ComboBox();
        Procedures procedures = new Procedures();
        
        public AnyExcelForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filePath = procedures.dialogOpen(openFileDialog1);
            listExcelSheets = procedures.getSheetNumbers(filePath);
            TabControl tabCtrl = new TabControl();
            tabCtrl.Height = 500;
            tabCtrl.Width = 930;
            tabCtrl.Location = new Point(20, 80);
            this.Controls.Add(tabCtrl);
            foreach (Sheet tempSheet in listExcelSheets)
            {
                tempSheet.dtg = new DataGridView();
                sheet =procedures.readFromExcel(tempSheet);
                sheet.dtg.Size = new Size(800, 400);
                sheet.dtg.Location = new Point(10, 10);
                sheet.dtg.Name = sheet.sheetName;
                TabPage page = new TabPage(sheet.sheetName);     
                page.Controls.Add(sheet.dtg);
                tabCtrl.TabPages.Add(page);
                int q = 0;
                while (q < sheet.dtg.Columns.Count)
                {
                    sheet.dtg.Columns[q].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    q++;
                }
            }      
        }
        private void button2_Click(object sender, EventArgs e)
        {
            procedures.writeToDatabase(filePath, listExcelSheets );
        }
    }
}

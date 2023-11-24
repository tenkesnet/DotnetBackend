using ADODB;
using Dapper;
using NPOI.HSSF.UserModel;
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

namespace MyDataGrid
{
    public partial class MainForm : Form
    {
        string fileName;
        DataTable dtExcelTable;
        DataRow dr;
        List<Sheet> tabellak = new List<Sheet>();
        TabControl tabControlTabellak = new TabControl();
        List<TabellaSor> tabellaSorLista;
        List<List<TabellaSor>> tabellaSorListaLista = new List<List<TabellaSor>>();
        DapperContext context= new DapperContext();
        public MainForm()
        {
            InitializeComponent();
            setTabControl(tabControlTabellak);          //Létrehozza futásidőben a TabControl-t
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel Munkafüzet (*.xls)|*.xlsx|Minden fájl(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                List<Sheet> tabellak = getSheetNumbers();
                foreach (Sheet tabella in tabellak)
                {
                    tabella.dtg = new DataGridView();
                    tabella.dtg.Size = new Size(800, 400);
                    tabella.dtg.Location = new Point(10, 10);


                    tabella.dtg.Name = tabella.sheetName;
                    TabPage page = new TabPage(tabella.sheetName);
                    page.Controls.Add(tabella.dtg);
                    tabControlTabellak.TabPages.Add(page);

                    if (tabella != null)
                        GetRequestsDataFromExcel(tabella);

                }
            }
        }
        private List<Sheet> getSheetNumbers()
        {
            var fileExtension = Path.GetExtension(fileName);
            string sheetName;
            ISheet sheet = null;
            switch (fileExtension)
            {
                case ".xlsx":
                    using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        var wb = new XSSFWorkbook(fs);
                        for (int i = 0; i < wb.NumberOfSheets; i++)
                        {
                            sheetName = wb.GetSheetAt(i).SheetName;
                            sheet = (XSSFSheet)wb.GetSheet(sheetName);
                            tabellak.Add(new Sheet(sheetName, sheet));
                        }
                        return tabellak;
                    }
                    break;
                case ".xls":
                    using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        var wb = new HSSFWorkbook(fs);
                        for (int i = 0; i < wb.NumberOfSheets; i++)
                        {
                            sheetName = wb.GetSheetAt(i).SheetName;
                            sheet = (HSSFSheet)wb.GetSheet(sheetName);
                            tabellak.Add(new Sheet(sheetName, sheet));
                        }
                        return tabellak;
                    }
                    break;
            }
            return null;
        }

        private void GetRequestsDataFromExcel(Sheet tabella)
        {
            var sh = tabella.sheet;
            try
            {
                dtExcelTable = new DataTable();
                tabellaSorLista = tabella.tabellaSorok;
                dtExcelTable.Rows.Clear();
                dtExcelTable.Columns.Clear();
                var headerRow = sh.GetRow(0);
                int colCount = headerRow.LastCellNum;
                for (var c = 0; c < colCount; c++)
                    dtExcelTable.Columns.Add(headerRow.GetCell(c).ToString());
                var i = 1;
                var currentRow = sh.GetRow(i);
                while (currentRow != null)
                {
                    TabellaSor tabellaSor = new TabellaSor();
                    dr = dtExcelTable.NewRow();
                    for (var j = 0; j < currentRow.Cells.Count; j++)
                    {
                        var cell = currentRow.GetCell(j);
                        if (cell != null)
                            switch (cell.CellType)
                            {
                                case CellType.Numeric:
                                    dr[j] = DateUtil.IsCellDateFormatted(cell)
                                        ? cell.DateCellValue.ToString(CultureInfo.InvariantCulture)
                                        : cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
                                    break;
                                case CellType.String:
                                    dr[j] = cell.StringCellValue;
                                    break;
                                case CellType.Blank:
                                    dr[j] = string.Empty;
                                    break;
                            }
                        switch (j)
                        {
                            case 0:
                                tabellaSor.helyezes = Convert.ToInt16(dr[j]);
                                break;
                            case 1:
                                tabellaSor.csapat = Convert.ToString(dr[j]);
                                break;
                            case 2:
                                tabellaSor.osszesMerkozes = Convert.ToInt16(dr[j]);
                                break;
                            case 3:
                                tabellaSor.gyozelem = Convert.ToInt16(dr[j]);
                                break;
                            case 4:
                                tabellaSor.dontetlen = Convert.ToInt16(dr[j]);
                                break;
                            case 5:
                                tabellaSor.vereseg = Convert.ToInt16(dr[j]);
                                break;
                            case 6:
                                tabellaSor.lottGolok = Convert.ToInt16(dr[j]);
                                break;
                            case 7:
                                tabellaSor.kapottGolok = Convert.ToInt16(dr[j]);
                                break;
                            case 8:
                                tabellaSor.golKulonbseg = Convert.ToInt16(dr[j]);
                                break;
                            case 9:
                                tabellaSor.pontszam = Convert.ToInt16(dr[j]);
                                break;
                        }
                    }
                    dtExcelTable.Rows.Add(dr);
                    tabellaSorLista.Add(tabellaSor);
                    i++;
                    currentRow = sh.GetRow(i);
                }
                //tabellaSorListaLista.Add(tabellaSorLista);
                tabella.tabellaSorok = tabellaSorLista;
                statisztika(0);
                tabella.dtg.DataSource = dtExcelTable;
                int q = 0;
                while (q < tabella.dtg.Columns.Count)                 //A DataGridView-k oszlopszélességét állítja
                {
                    tabella.dtg.Columns[q].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    q++;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private void statisztika(int sti)     //A megkapott index a megyét adja, azaz egyúttal a megyei tabellák listájának indexét
        {
            string merkozosenkentiGolatlag;
            double gol;
            double lottgol;
            double merkozes;
            double atlagCsapatGol;
            double merkozesSzam;

            atlagCsapatGol = Math.Round(tabellak[sti].tabellaSorok.Average(x => x.lottGolok), 3);
            merkozesSzam = tabellak[sti].tabellaSorok.Sum(x => x.osszesMerkozes) / 2;
            lottgol = tabellak[sti].tabellaSorok.Sum(y => y.lottGolok);
            merkozes = tabellak[sti].tabellaSorok.Sum(x => x.osszesMerkozes) / 2;
            gol = lottgol / merkozes;
            merkozosenkentiGolatlag = Convert.ToString(Math.Round(gol, 3));
            textBox1.Text = Convert.ToString(atlagCsapatGol);
            textBox2.Text = Convert.ToString(merkozesSzam);
            textBox3.Text = Convert.ToString(merkozosenkentiGolatlag);
            //return text;
        }
        private void setTabControl(TabControl tabCtrl)
        {
            tabCtrl.Height = 500;
            tabCtrl.Width = 930;
            tabCtrl.Location = new Point(20, 60);
            tabCtrl.SelectedIndexChanged += SelectedIndexChanged;  //A létrehozott TabControl lapjainak változásakor a SelectedIndexChanged metódust hívja meg
            this.Controls.Add(tabControlTabellak);
        }
        private void SelectedIndexChanged(Object sender, EventArgs e)  //A TabControl lapjainak változásakor hívódik meg
        {
            statisztika(tabControlTabellak.SelectedIndex);   //A kiválasztott TabPage indexét adja át a statisztikát számoló metódusnak
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_ClickAsync(object sender, EventArgs e)
        {
            if (fileName==null)
                return;
            var sql = "INSERT INTO excel (excel_file_name) VALUES (@excel_file_name) returning id";
            var excelFileNameParameter = new { excel_file_name = fileName };
            var excelId= context.connection.QuerySingle<int>(sql, excelFileNameParameter);

            foreach ( var sheet in tabellak ) {
                sql = "INSERT INTO sheet_name (excel_file_name_id, sheet_name) VALUES (@excel_id, @sheet_name) returning id";
                var excelSheetNameParameter = new { excel_id=excelId, sheet_name=sheet.sheetName};
                var excelSheetNameId = context.connection.QuerySingle<int>(sql, excelSheetNameParameter);
                foreach (var row in sheet.tabellaSorok)
                {
                    sql = "INSERT INTO sheet_row (helyezes, csapat, osszes_merkozes, gyozelem, dontetlen, vereseg," +
                    "lott_golok, kapott_golok, golkulonbseg, pontszam, sheet_name_id  ) VALUES (@helyezes, @csapat," +
                    "@osszes_merkozes, @gyozelem, @dontetlen, @vereseg, @lott_golok, @kapott_golok, @golkulonbseg, @pontszam," +
                    "@sheet_name_id)";
                    var rowParameter = new
                    {
                        helyezes = row.helyezes,
                        csapat = row.csapat,
                        osszes_merkozes = row.osszesMerkozes,
                        gyozelem = row.gyozelem,
                        dontetlen = row.dontetlen,
                        vereseg = row.vereseg,
                        lott_golok = row.lottGolok,
                        kapott_golok = row.kapottGolok,
                        golkulonbseg = row.golKulonbseg,
                        pontszam = row.pontszam,
                        sheet_name_id = excelSheetNameId
                    };
                    context.connection.Execute(sql, rowParameter);
                }
            }
            

        }
            
    
    }
}


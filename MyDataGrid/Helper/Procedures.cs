﻿using Azure;
using Dapper;
using MathNet.Numerics;
using Microsoft.Ajax.Utilities;
using Microsoft.Identity.Client;
using Microsoft.JScript;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using Convert = System.Convert;

namespace MyDataGrid.Services
{
    public class Procedures
    {

        public List<Sheet> tabellak = new List<Sheet>();
        public string filePath;
        public DataTable dtExcelTable;
        public DataRow dr;
        public List<TableCell> tabellaSorLista = new List<TableCell>();
        ComboBox cmbBetoltAdatbazisbol = new ComboBox();
        public DapperContext context = new DapperContext();

        Form formTemp;
        public string dialogOpen(OpenFileDialog openFileDialog)
        {
            
            openFileDialog.Filter = "Excel Munkafüzet (*.xls)|*.xlsx|Minden fájl(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            return filePath;
        }
        public TabControl tabellakFeltoltese(string filePath)
        {
            TabControl tabCtrl = new TabControl();
            tabellak.Clear();
            tabellak = getSheetNumbers(filePath);
            tabCtrl.Height = 500;
            tabCtrl.Width = 930;
            tabCtrl.Location = new Point(20, 80);
            foreach (Sheet tabella in tabellak)
            {
                tabella.dtg = new DataGridView();
                tabella.dtg.Size = new Size(800, 400);
                tabella.dtg.Location = new Point(10, 10);
                tabella.dtg.Name = tabella.sheetName;
                TabPage page = new TabPage(tabella.sheetName);      //TabPage létrehozása dinamikusan
                page.Controls.Add(tabella.dtg);
                tabCtrl.TabPages.Add(page);
                tabCtrl.Visible=true;
                if (tabella != null)
                    GetRequestsDataFromExcel(tabella);
            }
            return tabCtrl;
        }
        
        public List<Sheet> getSheetNumbers(string filePath)
        {
            var fileExtension = Path.GetExtension(filePath);
            string sheetName;
            List<Sheet> result = new List<Sheet>();
            ISheet sheet = null;
            switch (fileExtension)
            {
                case ".xlsx":
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        var wb = new XSSFWorkbook(fs);
                        for (int i = 0; i < wb.NumberOfSheets; i++)
                        {
                            sheetName = wb.GetSheetAt(i).SheetName;
                            sheet = (XSSFSheet)wb.GetSheet(sheetName);
                            result.Add(new Sheet(sheetName, sheet));
                        }
                        return  result;
                    }
                    break;
                case ".xls":
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        var wb = new HSSFWorkbook(fs);
                        for (int i = 0; i < wb.NumberOfSheets; i++)
                        {
                            sheetName = wb.GetSheetAt(i).SheetName;
                            sheet = (HSSFSheet)wb.GetSheet(sheetName);
                            result.Add(new Sheet(sheetName, sheet));
                        }
                        return result;
                    }
                    break;
            }
            return null;
        }
        public void GetRequestsDataFromExcel(Sheet tabella)
        {
            var sh = tabella.sheet;
            try
            {
                dtExcelTable = new DataTable();
                tabellaSorLista = tabella.tableSor;
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
                    TableCell tabellaSor = new TableCell();
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
                                tabellaSor.osszes_merkozes = Convert.ToInt16(dr[j]);
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
                                tabellaSor.lott_golok = Convert.ToInt16(dr[j]);
                                break;
                            case 7:
                                tabellaSor.kapott_golok = Convert.ToInt16(dr[j]);
                                break;
                            case 8:
                                tabellaSor.golkulonbseg = Convert.ToInt16(dr[j]);
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

                tabella.tableSor = tabellaSorLista;
                //statisztika(0);
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


        public double[] statisztika(int sti)     //A megkapott index a megyét adja, azaz egyúttal a megyei tabellák listájának indexét
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
        }
        public void insertToDatabase(string filepath)
        {
            var sql = "INSERT INTO excel (excel_file_name) VALUES (@excel_file_name) returning id";

            var excelFajlMentettList = context.connection.Query("select excel_file_name from excel");
            foreach (var fajl in excelFajlMentettList)
            {
                if (fajl.excel_file_name == filePath)
                {
                    MessageBox.Show("Ez az excel fájl már fel lett töltve!");
                    return;
                }
            }
            var excelFileNameParameter = new { excel_file_name = filePath };
            var excelId = context.connection.QuerySingle<int>(sql, excelFileNameParameter);
            foreach (var sheet in tabellak)
            {
                sql = "INSERT INTO sheet_name (excel_file_name_id, sheet_name) VALUES (@excel_id, @sheet_name) returning id";
                var excelSheetNameParameter = new { excel_id = excelId, sheet_name = sheet.sheetName };
                var excelSheetNameId = context.connection.QuerySingle<int>(sql, excelSheetNameParameter);
                foreach (var row in sheet.tableSor)
                {
                    sql = "INSERT INTO sheet_row (helyezes, csapat, osszes_merkozes, gyozelem, dontetlen, vereseg," +
                    "lott_golok, kapott_golok, golkulonbseg, pontszam, sheet_name_id  ) VALUES (@helyezes, @csapat," +
                    "@osszes_merkozes, @gyozelem, @dontetlen, @vereseg, @lott_golok, @kapott_golok, @golkulonbseg, @pontszam," +
                    "@sheet_name_id)";
                    var rowParameter = new
                    {
                        helyezes = row.helyezes,
                        csapat = row.csapat,
                        osszes_merkozes = row.osszes_merkozes,
                        gyozelem = row.gyozelem,
                        dontetlen = row.dontetlen,
                        vereseg = row.vereseg,
                        lott_golok = row.lott_golok,
                        kapott_golok = row.kapott_golok,
                        golkulonbseg = row.golkulonbseg,
                        pontszam = row.pontszam,
                        sheet_name_id = excelSheetNameId
                    };
                    context.connection.Execute(sql, rowParameter);
                }
            }
        }
        public ComboBox cmbBetolt()
        {
            cmbBetoltAdatbazisbol.Location = new Point(700, 45);
            cmbBetoltAdatbazisbol.Height = 35;
            cmbBetoltAdatbazisbol.Width = 100;
            int excelCount = context.connection.QuerySingle<int>("select count(*) from excel");
            for (int i = 1; i <= excelCount; i++)
            {
                var result = context.connection.QuerySingle<string>("select excel_file_name from excel where id=@id",
                    new
                    {
                        id = i,
                    });
                string fileName = Path.GetFileNameWithoutExtension(result);
                cmbBetoltAdatbazisbol.Items.Add(fileName);
                cmbBetoltAdatbazisbol.SelectedIndex = 0;
            }
            
            return cmbBetoltAdatbazisbol;
        }
        public TabControl cmbSelectedIndexChanged(TabControl tabControl)
        {
            tabControl.TabPages.Clear();
            tabControl.Height = 500;
            tabControl.Width = 930;
            tabControl.Location = new Point(20, 80);
            tabControl.Visible = true;
            tabellak.Clear();
            var megyeList = context.connection.Query("select id from sheet_name " +
                    "where excel_file_name_id=@id",
                    new
                    {
                        id = cmbBetoltAdatbazisbol.SelectedIndex + 1,
                    });
            foreach (var megye_name in megyeList)
            {

                var megye = context.connection.QuerySingle<string>("select sheet_name from sheet_name " +
                    "where id=@id",
                    new
                    {
                        id = megye_name.id,

                    });
                Sheet tabellaSheet = new Sheet(megye);
                tabellaSheet.dtg = new DataGridView();
                tabellaSheet.dtg.Size = new Size(800, 400);
                tabellaSheet.dtg.Location = new Point(10, 10);
                tabellaSheet.dtg.Name = tabellaSheet.sheetName;
                TabPage page = new TabPage(megye);
                page.Controls.Add(tabellaSheet.dtg);
                tabControl.TabPages.Add(page);
                DataTable tabellaTabla = new DataTable();
                addColumn("Hely", typeof(int), tabellaTabla);
                addColumn("Csapat", typeof(string), tabellaTabla);
                addColumn("Össz", typeof(int), tabellaTabla);
                addColumn("Győz", typeof(int), tabellaTabla);
                addColumn("Dönt", typeof(int), tabellaTabla);
                addColumn("Ver", typeof(int), tabellaTabla);
                addColumn("Lőtt", typeof(int), tabellaTabla);
                addColumn("Kapott", typeof(int), tabellaTabla);
                addColumn("Gólk", typeof(int), tabellaTabla);
                addColumn("Pont", typeof(int), tabellaTabla);
                int sheetRowCount = context.connection.QuerySingle<int>("select count(*) from sheet_row " +
                    "where sheet_name_id=@id",
                    new
                    {
                        id = megye_name.id,
                    });
                var csapatList = context.connection.Query("select id from sheet_row " +
                    "where sheet_name_id=@id",
                    new
                    {
                        id = megye_name.id,
                    });
                foreach (var csapat in csapatList)
                {
                    dr = tabellaTabla.NewRow();
                    TableCell result = context.connection.QueryFirst<TableCell>("select * from sheet_row where id=@id",
                        new
                        {
                            id = csapat.id,
                        });
                    if (result.sheet_name_id == megye_name.id)
                    {
                        dr[0] = result.helyezes;
                        dr[1] = result.csapat;
                        dr[2] = result.osszes_merkozes;
                        dr[3] = result.gyozelem;
                        dr[4] = result.dontetlen;
                        dr[5] = result.vereseg;
                        dr[6] = result.lott_golok;
                        dr[7] = result.kapott_golok;
                        dr[8] = result.golkulonbseg;
                        dr[9] = result.pontszam;
                        tabellaTabla.Rows.Add(dr);
                        tabellaSheet.tableSor.Add(result);
                    }
                }
                tabellaSheet.dtg.DataSource = tabellaTabla;

                //tabellaSheet.dtg.Sort(tabellaSheet.dtg.Columns["Hely"], ListSortDirection.Ascending);

                tabellak.Add(tabellaSheet);
                int q = 0;
                while (q < tabellaSheet.dtg.Columns.Count)
                {
                    tabellaSheet.dtg.Columns[q].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    q++;
                }
                
                //statisztika(0);
            }
            return tabControl;
        }
        private void addColumn(string columnName, Type type, DataTable dtable)
        {
            DataColumn t1 = new DataColumn(columnName);
            t1.DataType = type;
            dtable.Columns.Add(t1);
        }
        public Sheet readFromExcel(Sheet excelSheet)
        {
            var sh = excelSheet.sheet;
            dtExcelTable = new DataTable();
            dtExcelTable.Rows.Clear();
            dtExcelTable.Columns.Clear();
            var headerRow = sh.GetRow(0);       
            int colCount = headerRow.LastCellNum;
            for (var c = 0; c < colCount; c++)
            {
                SheetColumn sheetColumn = new SheetColumn();
                sheetColumn.columnName = headerRow.GetCell(c).ToString();
                sheetColumn.columnType = headerRow.GetCell(c).CellType.ToString();
                dtExcelTable.Columns.Add(headerRow.GetCell(c).ToString());
                var sql = "INSERT INTO sheet_column (order_number, sheet_id, column_name, column_type) VALUES" +
                    " (@order_number, @ sheet_id, @ column_name, @ column_type) returning id";
                var excelSheetNameParameter = new { excel_id = excelId, sheet_name = sheet.sheetName };
                sheetColumn.id = context.connection.QuerySingle<int>(sql, excelFileNameParameter);
                
            }

           
            var i = 1;
            var currentRow = sh.GetRow(i);
            while (currentRow != null)
            {
                TableCell tabellaSor = new TableCell();
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
                }
                dtExcelTable.Rows.Add(dr);
                i++;
                currentRow = sh.GetRow(i);
            }
            excelSheet.dataTable = dtExcelTable;

            excelSheet.dtg.DataSource = dtExcelTable;
            return excelSheet;
        }
        public void writeToDatabase(string filePath, List<Sheet> sheetLista)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            var excelFileNameParameter = new { excel_file_name = fileName };
            var sql = "INSERT INTO excel (excel_file_name) VALUES (@excel_file_name) returning id";
            var excelId = context.connection.QuerySingle<int>(sql, excelFileNameParameter);
            foreach (var sheet in sheetLista)
            {
                sql = "INSERT INTO sheet_name (excel_file_name_id, sheet_name) VALUES (@excel_id, @sheet_name) returning id";
                var excelSheetNameParameter = new { excel_id = excelId, sheet_name = sheet.sheetName };
                var excelSheetNameId = context.connection.QuerySingle<int>(sql, excelSheetNameParameter);
                int columnCount=sheet.dtg.Columns.Count;
                int rowCount = sheet.dtg.Rows.Count;
                var sh = sheet.dtg;
                int k= 0;
                foreach (DataGridViewColumn col in sh.Columns)
                {
                    sql = "INSERT INTO sheet_column (column_number, sheet_name_id, column_name, column_type) " +
                        "VALUES (@column_number, @sheet_name_id, @column_name, @column_type)";
                    var rowParameter = new
                    {
                        column_number = k,
                        sheet_name_id = excelSheetNameId,
                        column_name = col.Name,
                        column_type = Convert.ToString(sheet.dtg.Rows[0].Cells[k].ValueType)
                    };
                    context.connection.Execute(sql, rowParameter);
                    k++;
                };
                for (int j=0; j<rowCount; j++)
                {
                    for (int i = 0; i < columnCount; i++)
                    {
                        sql = "INSERT INTO sheet_row (row_number, sheet_column_number, value) " +
                            "VALUES (@row_number, @column_number_id, @value)";
                        var rowParameter = new
                        {
                            row_number = j,
                            column_number_id = i,
                            value = sh.Rows[j].Cells[i].Value,
                        };
                        context.connection.Execute(sql, rowParameter);
                    };
                    
                }
            }
        }
    }
}

using Azure;
using Dapper;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using Microsoft.Ajax.Utilities;
using Microsoft.Identity.Client;
using Microsoft.JScript;
using MyDataGrid.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.Streaming.Values;
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
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using Convert = System.Convert;


namespace MyDataGrid.Services
{
    public class Procedures
    {

        public List<TableSheet> tabellak = new List<TableSheet>();
        public string filePath;
        public DataRow dr;
        public List<TableCell> tabellaSorLista = new List<TableCell>();

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
            foreach (TableSheet tabella in tabellak)
            {
                tabella.dtg = new DataGridView();
                tabella.dtg.Size = new Size(800, 400);
                tabella.dtg.Location = new Point(10, 10);
                tabella.dtg.Name = tabella.sheetName;
                TabPage page = new TabPage(tabella.sheetName);      //TabPage létrehozása dinamikusan
                page.Controls.Add(tabella.dtg);
                tabCtrl.TabPages.Add(page);
                tabCtrl.Visible=true;
                //if (tabella != null)
                    //GetRequestsDataFromExcel(tabella);
            }
            return tabCtrl;
        }
        
        public List<TableSheet> getSheetNumbers(string filePath)
        {
            var fileExtension = Path.GetExtension(filePath);
            string sheetName;
            List<TableSheet> result = new List<TableSheet>();
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
                            result.Add(new TableSheet(sheetName, sheet));
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
                            result.Add(new TableSheet(sheetName, sheet));
                        }
                        return result;
                    }
                    break;
            }
            return null;
        }
        /*public void GetRequestsDataFromExcel(TableSheet tabella)
        {
            DataTable dataTable = new DataTable();
            var sh = tabella.sheet;
            try
            {
                dataTable = new DataTable();
                //tabellaSorLista = tabella.tableSor;
                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                var headerRow = sh.GetRow(0);
                int colCount = headerRow.LastCellNum;
                for (var c = 0; c < colCount; c++)
                    dataTable.Columns.Add(headerRow.GetCell(c).ToString());
                var i = 1;
                var currentRow = sh.GetRow(i);
                while (currentRow != null)
                {
                    TableCell tabellaSor = new TableCell();
                    dr = dataTable.NewRow();

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
                        
                        /*switch (j)
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
                    dataTable.Rows.Add(dr);
                    tabella.Add(tabellaSor);
                    tabella.
                    i++;
                    currentRow = sh.GetRow(i);
                }

                tabella.tableSor = tabellaSorLista;
                //statisztika(0);
                tabella.dtg.DataSource = dataTable;
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
        }*/


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
                    /*var rowParameter = new
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
                    context.connection.Execute(sql, rowParameter);*/
                }
            }
        }
        public ComboBox cmbBetolt()
        {
            ComboBox cmbBetoltAdatbazisbol = new ComboBox();
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
                string fileName = result;
                cmbBetoltAdatbazisbol.Items.Add(fileName);
                cmbBetoltAdatbazisbol.SelectedIndex = 0;
            }
            return cmbBetoltAdatbazisbol;
        }
        public TabControl cmbSelectedIndexChanged(TabControl tabControl, ComboBox cmbBetoltAdatbazisbol)
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
                TableSheet tabellaSheet = new TableSheet(megye);
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
                    /*if (result.sheet_name_id == megye_name.id)
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
                    }*/
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
        public MultiReturn readFromExcel(TableSheet excelSheet)
        {
            var sh = excelSheet.sheet;
            MultiReturn multiReturn = new MultiReturn();
            DataTable dataTable = new DataTable();
            TableSheet tblSheet = new TableSheet();

            dataTable.Rows.Clear();
            dataTable.Columns.Clear();
            tblSheet.sorok.Clear();
            tblSheet.sheetColumns.Clear();
                        
            var headerRow = sh.GetRow(0);
            DataColumn dataColumn = new DataColumn();
            dataColumn.DefaultValue = sh.GetRow(1).GetCell(0);
            int colCount = headerRow.LastCellNum;
            
            for (int c = 0; c < colCount; c++)
            {
                SheetColumn sheetColumn = new SheetColumn();
                sheetColumn.order_number=c;
                sheetColumn.column_name = headerRow.GetCell(c).ToString();
                sheetColumn.column_type = Convert.ToString(headerRow.GetCell(c).CellType);
                dataTable.Columns.Add(headerRow.GetCell(c).ToString());
                tblSheet.sheetColumns.Add(sheetColumn);
            }
            var i = 1;
            var currentRow = sh.GetRow(i);
            while (currentRow != null)
            {
                TableCell tabellaSor = new TableCell();
                dr = dataTable.NewRow();
                TableRow tblRow = new TableRow();
                for (var j = 0; j < currentRow.Cells.Count; j++)
                {
                    var cell = currentRow.GetCell(j);

                    TableCell tblCell= new TableCell();
                    if (cell != null)
                        switch (cell.CellType)
                        {
                            case CellType.Numeric:
                                string columnType = String.Empty;
                                if (DateUtil.IsCellDateFormatted(cell))
                                {
                                    dr[j] = cell.DateCellValue.ToString(CultureInfo.InvariantCulture);
                                    columnType = "date";
                                }
                                else
                                {
                                    dr[j]= cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
                                    columnType = "numeric";
                                }
                                tblCell.value = dr[j].ToString();
                                tblCell.columnNumber = j;
                                tblSheet.sheetColumns[j].column_type = columnType;
                                tblCell.cellType = columnType;
                                break;
                            case CellType.String:
                                dr[j] = cell.StringCellValue;
                                tblCell.value = cell.StringCellValue.ToString();
                                tblCell.columnNumber = j;
                                tblSheet.sheetColumns[j].column_type = Convert.ToString(cell.CellType);
                                tblCell.cellType = Convert.ToString(cell.CellType);
                                break;
                            case CellType.Blank:
                                dr[j] = string.Empty;
                                break;
                        }
                    tblRow.tableRow.Add(tblCell);
                }
                dataTable.Rows.Add(dr);
                tblSheet.sorok.Add(tblRow);
                i++;
                currentRow = sh.GetRow(i);
            }
            multiReturn.multiReturnTableSheet = tblSheet;
            multiReturn.multiReturnDataTable= dataTable;
            return multiReturn;
        }
        public void  writeToDatabase(string filePath, List<TableSheet> tableSheetsWriteToDatabase)
        {
            List<int> columnList = new List<int>();  
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            var excelFajlMentettList = context.connection.Query("select excel_file_name from excel");
            foreach (var fajl in excelFajlMentettList)
            {
                if (fajl.excel_file_name == fileName)
                {
                    MessageBox.Show("Ez az excel fájl már fel lett töltve!");
                    return;
                }
            }
            var excelFileNameParameter = new { excel_file_name = fileName };
            var sql = "INSERT INTO excel (excel_file_name) VALUES (@excel_file_name) returning id";
            var excelId = context.connection.QuerySingle<int>(sql, excelFileNameParameter);
            foreach (TableSheet foreachTableSheet in tableSheetsWriteToDatabase)
            {
                columnList.Clear();
                sql = "INSERT INTO sheet_name (excel_file_name_id, sheet_name) VALUES (@excel_id, @sheet_name) returning id";
                var excelSheetNameParameter = new { excel_id = excelId, sheet_name = foreachTableSheet.sheetName};
                var excelSheetNameId = context.connection.QuerySingle<int>(sql, excelSheetNameParameter);
                for (int i = 0; i<foreachTableSheet.sheetColumns.Count; i++)
                {
                    sql = "INSERT INTO sheet_column (order_number, sheet_id, column_name, column_type) " +
                        "VALUES (@order_number, @sheet_id, @column_name, @column_type) returning id";
                    var columnParameter = new
                    {
                        order_number = i,
                        sheet_id = excelSheetNameId,
                        column_name = foreachTableSheet.sheetColumns[i].column_name,
                        column_type = foreachTableSheet.sheetColumns[i].column_type,
                    };
                    var colId = context.connection.QuerySingle<int>(sql, columnParameter);
                    columnList.Add(colId);
                }
                int j = 0;
                foreach (TableRow row in foreachTableSheet.sorok)
                {
                    int k = 0;
                    foreach (TableCell column in row.tableRow)
                    {
                        sql = "INSERT INTO sheet_row (row_order_number, column_number, sheet_column_id, value) " +
                            "VALUES (@row_order_number, @column_number, @sheet_column_id, @value)";
                        var rowParameter = new
                        {
                            row_order_number = j,
                            column_number = k,
                            sheet_column_id = columnList[k],
                            value = column.value,
                        };

                        context.connection.Execute(sql, rowParameter);
                        k++;
                    }
                    j++;
                }
            }
        }
        public TabControl readFromDataBase(int cmbIndex)
        {
            TabControl tabControl = new TabControl();
            DataTable dataTable = new DataTable();


            var excelFileName = context.connection.QuerySingle<string>("select excel_file_name from excel where id=@id",
                new
                {
                    id = cmbIndex,
                });
            var fileSheetList = context.connection.Query("select sheet_name from sheet_name " +
                    "where excel_file_name_id=@id",
                new
                {
                    id = cmbIndex,
                });
            foreach (var sheetName in fileSheetList)
            {

                TableSheet tblSheet = new TableSheet();

                string kerdes = sheetName.sheet_name;
                var sql = "select id from sheet_name where sheet_name=@sheetname";
                var excelFileSheetIdParameter = new { sheetname = kerdes };
                var fileSheetId = context.connection.QuerySingle<int>(sql, excelFileSheetIdParameter);
                
                tblSheet.dtg = new DataGridView();
                tblSheet.dtg.Size = new Size(800, 400);
                tblSheet.dtg.Location = new Point(10, 10);
                tblSheet.dtg.Name = tblSheet.sheetName;
                TabPage page = new TabPage(sheetName.sheet_name);
                page.Controls.Add(tblSheet.dtg);
                tabControl.TabPages.Add(page);
                DataTable dataTable1 = new DataTable();
                var columnList = context.connection.Query<SheetColumn>("select id, column_name, column_type from sheet_column " +
                        "where sheet_id=@id",
                    new
                    {
                        id = fileSheetId,
                    });
                List<SheetColumn> columnIdList = new List<SheetColumn>(columnList);
                
                for (int c = 0; c < columnIdList.Count; c++)
                {
                    SheetColumn sheetColumn = new SheetColumn();
                    sheetColumn.order_number = c;
                    sheetColumn.column_name = columnIdList[c].column_name;

                    sheetColumn.column_type = columnIdList[c].column_type;
                    dataTable.Columns.Add(columnIdList[c].column_name);
                    tblSheet.sheetColumns.Add(sheetColumn);
                }
                foreach (var column in columnList)
                {
                    SheetColumn sheetColumn = new SheetColumn();
                    sheetColumn.id = column.id;
                    sheetColumn.column_type = context.connection.QuerySingle<string>("select column_type from sheet_column " +
                        "where id=@id",
                        new
                        {
                            id = column.id,
                        });
                    sheetColumn.column_name = context.connection.QuerySingle<string>("select column_name from sheet_column " +
                        "where id=@id",
                        new
                        {
                            id = column.id,
                        });
                    if (sheetColumn.column_type == "numeric")
                    {
                        addColumn(sheetColumn.column_name, typeof(decimal), dataTable1);
                      
                    }
                    if (sheetColumn.column_type == "String")
                    {
                        addColumn(sheetColumn.column_name, typeof(string), dataTable1);
                 
                    }
                    if (sheetColumn.column_type == "date")
                    {
                        addColumn(sheetColumn.column_name, typeof(string), dataTable1);

                    }
                    columnIdList.Add(sheetColumn);
                }

                int sorokSzama = context.connection.QuerySingle<int>("select count(*) from sheet_row " +
                    "where sheet_column_id=@id",
                    new
                    {
                        id = columnIdList[0].id,
                    });
                
                for (int m = 0; m < sorokSzama; m++)
                {
                    dr = dataTable1.NewRow();
                    int l = 0;
                    foreach (var col in columnIdList)
                    {
                        
                        var columnType = context.connection.QuerySingle<string>("select column_type from sheet_column " +
                            "where id=@id",
                            new
                            {
                                id = col.id,
                            });
                        if (columnType == "numeric")
                        {
                            var result = context.connection.QuerySingle<float>("select value from sheet_row " +
                                "where row_order_number=@row_order_number and sheet_column_id=@sheetcolumnid",
                                new
                                {
                                    row_order_number = m,
                                    sheetcolumnid = col.id,
                                });
                            dr[l] = result;
                        }
                        if (columnType == "String")
                        {
                            var result = context.connection.QuerySingle<string>("select value from sheet_row " +
                                "where row_order_number=@row_order_number and sheet_column_id=@sheetcolumnid",
                                new
                                {
                                    row_order_number = m,
                                    sheetcolumnid = col.id,
                                });
                            dr[l] = result;
                        }
                        if (columnType == "date")
                        {
                            var result = context.connection.QuerySingle<string>("select value from sheet_row " +
                                "where row_order_number=@row_order_number and sheet_column_id=@sheetcolumnid",
                                new
                                {
                                    row_order_number = m,
                                    sheetcolumnid = col.id,
                                });
                            dr[l] = result;
                        }

                        l++;
                    }
                    dataTable1.Rows.Add(dr);
                    
                }
                tblSheet.dtg.DataSource = dataTable1;
                int q = 0;
                while (q < tblSheet.dtg.Columns.Count)
                {
                    tblSheet.dtg.Columns[q].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    q++;
                }
            }
            return tabControl;
        }

    }
}


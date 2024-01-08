using Azure;
using Dapper;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using Microsoft.Ajax.Utilities;
using Microsoft.Identity.Client;
using Microsoft.JScript;
using MyDataGrid.Models;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.Streaming.Values;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
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
        public string dialogOpen(OpenFileDialog openFileDialog)
        { 
            openFileDialog.Filter = "Excel Munkafüzet (*.xls)|*.xlsx|Minden fájl(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            return filePath;
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
        public ComboBox cmbBetolt()
        {
            ComboBox cmbBetoltAdatbazisbol = new ComboBox();
            cmbBetoltAdatbazisbol.Location = new Point(850, 65);
            cmbBetoltAdatbazisbol.Height = 35;
            cmbBetoltAdatbazisbol.Width = 100;
            int excelCount = context.connection.QuerySingle<int>("select max(id) from excel");
            var idList = context.connection.Query<int>("select id from excel");
            foreach (int i in idList)
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
            tblSheet.sheetName =excelSheet.sheetName;
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
                //dataTable.Columns.Add(headerRow.GetCell(c).ToString());
                //addColumn(sheetColumn.column_name, sheetColumn.column_type, dataTable)
                var currentRow2 = sh.GetRow(2);
                string type = "String";
                if (currentRow2!=null)
                {
                    double typeTemp;
                    if (Convert.ToString(currentRow2.GetCell(c).CellType) == "Numeric"  && !DateUtil.IsCellDateFormatted(currentRow2.GetCell(c)))
                    {

                        type = Convert.ToString(currentRow2.GetCell(c).CellType);
                    }
                    
                    
                }
                if (type == "Numeric")
                {
                    addColumn(sheetColumn.column_name, typeof(decimal), dataTable);
                }
                if (type == "String")
                {
                    addColumn(sheetColumn.column_name, typeof(string), dataTable);
                }
                if (type == "date")
                {
                    addColumn(sheetColumn.column_name, typeof(string), dataTable);
                }

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
                                    tblCell.value = dr[j].ToString();
                                }
                                else
                                {
                                    dr[j]= cell.NumericCellValue;
                                    columnType = "numeric";
                                    //tblCell.value = ((decimal)cell.NumericCellValue);
                                }
                                //tblCell.value = dr[j].ToString();
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
            var p = tblSheet.sorok.Find(x => x.tableRow.Find(y => y.value!=null && y.value.Equals("HARTA SE")) !=null);
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
        public MultiReturn readFromDataBase(dynamic sheetName)
        {
            MultiReturn multiReturn = new MultiReturn();
            TabControl tabControl = new TabControl();
            DataTable dataTable = new DataTable();
            dataTable.Rows.Clear();
            dataTable.Columns.Clear();
            TableSheet tblSheet = new TableSheet();
            string kerdes = sheetName.sheet_name;
            var sql = "select id from sheet_name where sheet_name=@sheetname";
            var excelFileSheetIdParameter = new { sheetname = kerdes };
            var fileSheetId = context.connection.QuerySingle<int>(sql, excelFileSheetIdParameter);
            tblSheet.dtg = new DataGridView();
            List<SheetColumn> columnIdList = new List<SheetColumn>();
            var columnList = context.connection.Query<SheetColumn>("select id, column_name, column_type from sheet_column " +
                    "where sheet_id=@id",
                new
                {
                    id = fileSheetId,
                });
            columnIdList.Clear();
            columnIdList = new List<SheetColumn>(columnList);
            for (int c = 0; c < columnIdList.Count; c++)
            {
                SheetColumn sheetColumn = new SheetColumn();
                sheetColumn.order_number = c;
                sheetColumn.column_name = columnIdList[c].column_name;
                sheetColumn.column_type = columnIdList[c].column_type;
                if (sheetColumn.column_type == "numeric")
                {
                    addColumn(columnIdList[c].column_name, typeof(decimal), dataTable);
                }
                if (sheetColumn.column_type == "String")
                {
                    addColumn(columnIdList[c].column_name, typeof(string), dataTable);
                }
                if (sheetColumn.column_type == "date")
                {
                    addColumn(columnIdList[c].column_name, typeof(string), dataTable);
                }
                tblSheet.sheetColumns.Add(sheetColumn);
            }
            int sorokSzama = context.connection.QuerySingle<int>("select count(*) from sheet_row " +
                "where sheet_column_id=@id",
                new
                {
                    id = columnIdList[0].id,
                });
            for (int m = 0; m < sorokSzama; m++)
            {
                dr = dataTable.NewRow();
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
                        var result = context.connection.QuerySingle<decimal>("select value from sheet_row " +
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
                dataTable.Rows.Add(dr);
            }
            tblSheet.dtg.DataSource = dataTable;
            multiReturn.multiReturnTableSheet = tblSheet;
            multiReturn.multiReturnDataTable = dataTable;
            return multiReturn;
        }
    }
}


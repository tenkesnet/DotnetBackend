using MyDataGrid.Models;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDataGrid
{
    public class TableSheet
    {
        public List<TableRow> sorok { get; set; }
        public List<TableCell> tableSor;
        public List<SheetColumn> sheetColumns { get; set; }

        public string sheetName;
        public DataGridView dtg { get; set; }
        public ISheet sheet { get; set; }
        public DataTable dataTable   { get; set; }

        public  TableSheet (string sName, ISheet sheet)
        {
            sorok = new List<TableRow>();
            sheetColumns = new List<SheetColumn>();
            sheetName = sName;
            this.sheet = sheet;
        }
        public TableSheet(string sName)
        {
            sorok = new List<TableRow>();
            sheetColumns = new List<SheetColumn>();
            sheetName = sName;
            //this.sheet = sheet;
        }
        public TableSheet()
        {
            sorok = new List<TableRow>();
            sheetColumns= new List<SheetColumn>();
        }

    }
}

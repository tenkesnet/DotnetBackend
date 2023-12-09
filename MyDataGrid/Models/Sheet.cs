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
    public class Sheet
    {
        public List<TableCell> tableSor;
        public List<List<TableCell>> tableSorok;
        public List<SheetColumn> sheetColumns { get; set; }

        public string sheetName;
        public DataGridView dtg { get; set; }
        public ISheet sheet { get; set; }
        public DataTable dataTable   { get; set; }

        public  Sheet (string sName, ISheet sheet)
        {
            tableSor = new List<TableCell>();
            sheetColumns = new List<SheetColumn>();
            sheetName = sName;
            this.sheet = sheet;
        }
        public Sheet(string sName)
        {
            tableSor = new List<TableCell>();
            sheetColumns = new List<SheetColumn>();
            sheetName = sName;
            //this.sheet = sheet;
        }


    }
}

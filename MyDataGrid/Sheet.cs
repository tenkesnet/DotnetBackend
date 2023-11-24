using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDataGrid
{
    public class Sheet
    {
        public List<TabellaSor> tabellaSorok;
        //public List<List<TabellaSor>> tabellaSorListList;
        public string sheetName;
        public DataGridView dtg { get; set; }
        public ISheet sheet { get; set; }
        public  Sheet (string sName, ISheet sheet)
        {
            tabellaSorok = new List<TabellaSor>();
            sheetName = sName;
            this.sheet = sheet;
        }
      
    }
}

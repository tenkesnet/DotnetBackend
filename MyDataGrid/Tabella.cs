using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDataGrid
{
    public class Tabella
    {
        public List<TabellaSor> tabellaSorok;
        public List<List<TabellaSor>> tabellaSorListList;
        public string sheetName;
        public DataGridView dtg { get; set; }
        public ISheet sheet { get; set; }
        public  Tabella (string sName, ISheet sheet)
        {
            tabellaSorok = new List<TabellaSor>();
            sheetName = sName;
            this.sheet = sheet;
        }
      
    }
}

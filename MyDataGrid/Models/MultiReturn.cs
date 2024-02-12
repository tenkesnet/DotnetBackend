using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataGrid.Models
{
    public class MultiReturn
    {
        public DataTable multiReturnDataTable;
        public TableSheet multiReturnTableSheet;
        public MultiReturn()
        {
            multiReturnDataTable = new DataTable();
            multiReturnTableSheet = new TableSheet();
            multiReturnTableSheet.sorok = new List<TableRow>();
        }
    }
}

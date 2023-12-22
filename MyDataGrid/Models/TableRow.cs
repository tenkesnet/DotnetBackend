using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataGrid.Models
{
    public class TableRow
    {
        public List<TableCell> tableRow = new List<TableCell>();
        public int sheetColumnId { get; set; }
        public TableRow()
        { 
            
        }
    }
}

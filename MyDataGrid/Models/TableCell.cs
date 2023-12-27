using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataGrid
{
    public class TableCell
    {

        public dynamic value { get; set; }
        public int columnNumber { get; set; }
        //public int columnOrderNumber { get; set; }
        public string cellType { get; set; }
        public TableCell()
        {
            
        }

    }
}

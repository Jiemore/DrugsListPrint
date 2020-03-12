using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintReport
{
    public class Print
    {
        private DataTable DataTable;
        public void InitDataSource(DataTable dt) {
            DataTable = dt;
        }

    }
}

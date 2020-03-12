using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoodsListPrint
{
    public partial class AddPrint : Form
    {
        private int rowIndex;
        private string GoodsName;
        private int GoodsCount = 0;
        CallBack AddPrintItem;
        public AddPrint(CallBack AddPrint,int RowIndex,string GoodsName,int GoodsCount)
        {
            InitializeComponent();

            rowIndex = RowIndex;
            this.GoodsName = GoodsName;
            this.GoodsCount = GoodsCount;
            AddPrintItem = AddPrint;
        }

        private void AddPrint_Load(object sender, EventArgs e)
        {
            txtGoodsName.Text = GoodsName;
        }

        private void btnconfirm_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0) ;

            else if (GoodsCount < numericUpDown1.Value)
            {
                MessageBox.Show("数量超出库存！打印请求被拒绝，请输入比库存量小的数");
            }

            else
            {
                AddPrintItem(rowIndex, Convert.ToInt32(numericUpDown1.Value));
                this.Close();
            }
        }
    }
}

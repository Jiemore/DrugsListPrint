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
    public partial class PrintSetting : Form
    {
        //ReportParameterCostume
        Dictionary<string, string> keys;
        public PrintSetting(ref Dictionary<string,string> keys)
        {

            InitializeComponent();
            this.keys = keys;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBlue.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            txtDelivery.Text = string.Empty;
            txtDrawer.Text = string.Empty;
            txtID.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtRecAddr.Text = string.Empty;
            txtRed.Text = string.Empty;
            txtSalesman.Text = string.Empty;
            txtSaver.Text = string.Empty;
            txtVerification.Text = string.Empty;
            txtWhite.Text = string.Empty;
            txtYellow.Text = string.Empty;
            //清除后重新绑定
            btnPrintSetting_Click(null,null);
        }

        private void btnPrintSetting_Click(object sender, EventArgs e)
        {
            keys["ReportParameterCostume"] = txtCustomer.Text.Trim();
            keys["ReportParameterID"] = txtID.Text.Trim();
            keys["ReportParameterRed"] = txtRed.Text.Trim();
            keys["ReportParameterDrawer"] = txtDrawer.Text.Trim();
            keys["ReportParameterPhone"] = txtPhone.Text.Trim();
            keys["ReportParameterRecAddr"] = txtRecAddr.Text.Trim();
            keys["ReportParameterWhite"] = txtWhite.Text.Trim();
            keys["ReportParameterBlue"] = txtBlue.Text.Trim();
            keys["ReportParameterSalesman"] = txtSalesman.Text.Trim();
            keys["ReportParameterVerification"] = txtVerification.Text.Trim();
            keys["ReportParameterDelivery"] = txtDelivery.Text.Trim();
            keys["ReportParameterSaver"] = txtSaver.Text.Trim();
            keys["ReportParameterYellow"] = txtYellow.Text.Trim();
          MessageBox.Show("设置完毕！");
        }

        private void PrintSetting_Load(object sender, EventArgs e)
        {
            txtCustomer.Text = keys["ReportParameterCostume"].ToString();
            txtID.Text = keys["ReportParameterID"].ToString();
            txtRed.Text= keys["ReportParameterRed"].ToString();
            txtDrawer.Text= keys["ReportParameterDrawer"].ToString();
            txtPhone.Text = keys["ReportParameterPhone"].ToString();
            txtRecAddr.Text = keys["ReportParameterRecAddr"].ToString();
            txtWhite.Text = keys["ReportParameterWhite"].ToString();
            txtBlue.Text = keys["ReportParameterBlue"].ToString();
            txtSalesman.Text = keys["ReportParameterSalesman"].ToString();
            txtVerification.Text = keys["ReportParameterVerification"].ToString();
            txtDelivery.Text = keys["ReportParameterDelivery"].ToString();
            txtSaver.Text = keys["ReportParameterSaver"].ToString();
            txtYellow.Text = keys["ReportParameterYellow"].ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

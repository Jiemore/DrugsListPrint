using Access.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoodsListPrint
{
    public partial class AddGoodsForm : Form
    {
        public AddGoodsForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string StrSql = "insert into DrugsTable (DrugsName,Specification,Model,Unti,Price,Origin,Batch,ProductionDate,Validity,Stock,Quality)values(@DrugsName,@Specificatio,@Model,@Unti,@Price,@Origin,@Batch,@ProductionDate,@Validity,@Stock,@Quality)";
            try
            {
                if (AccessDbHelper.ExecuteNonQuery(StrSql,
                                                    new OleDbParameter("@DrugsName", txtDrugsName.Text.Trim()),
                                                    new OleDbParameter("@Specification", txtSpecification.Text.Trim()),
                                                    new OleDbParameter("@Model", txtModel.Text.Trim()),
                                                    new OleDbParameter("@Unti", txtUnti.Text.Trim()),
                                                    new OleDbParameter("@Price", txtPrice.Text.Trim()==string.Empty?"0":txtPrice.Text.Trim()),
                                                    new OleDbParameter("@Origin", txtOrigin.Text.Trim()),
                                                    new OleDbParameter("@Batch", txtBatch.Text.Trim()),
                                                    new OleDbParameter("@ProductionDate", dtpProductionDate.Value.ToShortDateString()),
                                                    new OleDbParameter("@Validity", dtpValidity.Value.ToShortDateString()),
                                                    new OleDbParameter("@Stock", txtStock.Text.Trim()==string.Empty?"0":txtStock.Text.Trim()),
                                                    new OleDbParameter("@Quality", txtQuality.Text.Trim())
                                                    ) > 0){
                    txtDrugsName.Text = string.Empty;
                    txtBatch.Text = string.Empty;
                    txtModel.Text = string.Empty;
                    txtOrigin.Text = string.Empty;
                    txtPrice.Text = string.Empty;
                    txtQuality.Text = string.Empty;
                    txtSpecification.Text = string.Empty;
                    txtStock.Text = string.Empty;
                    txtUnti.Text = string.Empty;
                    MessageBox.Show("添加成功！");
                }
                else
                {
                    MessageBox.Show("添加失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败！");
            }
        }
    }
}

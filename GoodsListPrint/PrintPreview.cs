using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoodsListPrint
{
    public partial class PrintPreview : Form
    {
        private DataTable dt;//数据集
        ReportParameter[] parameters;//参数集
        public PrintPreview(DataTable dt, ReportParameter[] parameters)
        {
            this.dt = dt;
            this.parameters = parameters;
            InitializeComponent();
        }

        private void PrintPreview_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            reportViewer1.LocalReport.ReportPath = Environment.CurrentDirectory + @"\shengxingReport.rdlc";//@"C:\Users\R\source\repos\DrugsListPrint\PrintReport\shengxingReport.rdlc";
            ReportDataSource rds = new ReportDataSource
            {
                Name = "DataTableZhCN",
                Value = dt
            };
            //参数集
            this.reportViewer1.LocalReport.SetParameters(parameters);
            //数据集
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            //刷新
            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // dataGridView1.DataSource = dt;

        }

        private void btnOutputExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.Empty;
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog()
                {
                    Filter = "Files (*.xls)|*.xls",//如果需要筛选txt文件（"Files (*.txt)|*.txt"）
                    FileName= "output.xls"
                };
                var result = saveFileDialog.ShowDialog();
                if (result == true)
                {
                    path = saveFileDialog.FileName;
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;

                    byte[] bytes = reportViewer1.LocalReport.Render(
                       "Excel", null, out mimeType, out encoding,
                        out extension,
                       out streamids, out warnings);
                    FileStream fs = new FileStream(path, FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    MessageBox.Show(string.Format("导出完毕！"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("导出失败:{0}",ex.Message));
            }

        }
    }
}

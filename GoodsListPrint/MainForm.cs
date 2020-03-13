using Access.Repository;
using ADOX;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace GoodsListPrint
{
    //定义委托
    public delegate void CallBack(int RowIndex, int GoodsCount);//回调函数
    public partial class MainForm : Form
    {
        //传入报表的参数 部分用户自定义
        Dictionary<string, string> PrintParams = new Dictionary<string, string>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;  
            //ReportParameterCostume ReportParameterPrintDate ReportParameterID ReportParameterRed ReportParameterCountAll
            //ReportParameterCountAllCN ReportParameterDrawer ReportParameterPhone ReportParameterRecAddr ReportParameterWhite ReportParameterBlue 
            //ReportParameterSalesman ReportParameterVerification ReportParameterDelivery ReportParameterSaver ReportParameterPrintTime
            PrintParams.Add("ReportParameterCostume", "");
            PrintParams.Add("ReportParameterPrintDate", "");
            PrintParams.Add("ReportParameterID", "");
            PrintParams.Add("ReportParameterRed", "");
            PrintParams.Add("ReportParameterCountAll", "");
            PrintParams.Add("ReportParameterCountAllCN", "");
            PrintParams.Add("ReportParameterDrawer", "");
            PrintParams.Add("ReportParameterPhone", "");
            PrintParams.Add("ReportParameterRecAddr", "");
            PrintParams.Add("ReportParameterWhite", "");
            PrintParams.Add("ReportParameterBlue", "");
            PrintParams.Add("ReportParameterSalesman", "");
            PrintParams.Add("ReportParameterVerification", "");
            PrintParams.Add("ReportParameterDelivery", "");
            PrintParams.Add("ReportParameterSaver", "");
            PrintParams.Add("ReportParameterPrintTime", "");
            PrintParams.Add("ReportParameterYellow", "");
            
            #region 初始化数据库
            string filePath = Environment.CurrentDirectory + @"\DrugsDatabase.mdb";
            //初始化数据库，若数据库不存在则创建
            if (!File.Exists(filePath))
            {
                if (AccessDbHelper.CreateAccessDb(filePath))
                {
                    #region 初始化表字段
                    //药品名称 DrugsName
                    ADOX.ColumnClass DrugsName = new ADOX.ColumnClass
                    {
                        //ParentCatalog = catalog,
                        Name = "DrugsName",
                        Attributes = ColumnAttributesEnum.adColNullable, //允许空值
                        Type = DataTypeEnum.adVarWChar,
                        DefinedSize = 255,
                    };
                    //规格 Specification
                    ADOX.ColumnClass Specification = new ADOX.ColumnClass
                    {
                        //ParentCatalog = catalog,
                        Name = "Specification",
                        Attributes = ColumnAttributesEnum.adColNullable, //允许空值
                        Type = DataTypeEnum.adVarWChar,
                        DefinedSize = 255,
                    };
                    //型号 Model
                    ADOX.ColumnClass Model = new ADOX.ColumnClass
                    {
                        //ParentCatalog = catalog,
                        Name = "Model",
                        Attributes = ColumnAttributesEnum.adColNullable, //允许空值
                        Type = DataTypeEnum.adVarWChar,
                        DefinedSize = 50,
                    };
                    //单位 Unti
                    ADOX.ColumnClass Unti = new ADOX.ColumnClass
                    {
                        //ParentCatalog = catalog,
                        Name = "Unti",
                        Attributes = ColumnAttributesEnum.adColNullable, //允许空值
                        Type = DataTypeEnum.adVarWChar,
                        DefinedSize = 50,
                    };
                    //单价 Price
                    ADOX.ColumnClass Price = new ADOX.ColumnClass
                    {
                        //ParentCatalog = catalog,
                        Name = "Price",
                        Attributes = ColumnAttributesEnum.adColNullable, //允许空值
                        Type = DataTypeEnum.adDouble,
                        DefinedSize = 50,
                    };
                    //产地 Origin
                    ADOX.ColumnClass Origin = new ADOX.ColumnClass
                    {
                        //ParentCatalog = catalog,
                        Name = "Origin",
                        Attributes = ColumnAttributesEnum.adColNullable, //允许空值
                        Type = DataTypeEnum.adVarWChar,
                        DefinedSize = 255,
                    };
                    //批准批号 Batch
                    ADOX.ColumnClass Batch = new ADOX.ColumnClass
                    {
                        //ParentCatalog = catalog,
                        Name = "Batch",
                        Attributes = ColumnAttributesEnum.adColNullable, //允许空值
                        Type = DataTypeEnum.adVarWChar,
                        DefinedSize = 50,
                    };
                    //生产日期 ProductionDate 
                    ADOX.ColumnClass ProductionDate = new ADOX.ColumnClass
                    {
                        //ParentCatalog = catalog,
                        Name = "ProductionDate",
                        Attributes = ColumnAttributesEnum.adColNullable, //允许空值
                        Type = ADOX.DataTypeEnum.adDate,
                    };
                    //有效日期 Validity
                    ADOX.ColumnClass Validity = new ADOX.ColumnClass
                    {
                        //ParentCatalog = catalog,
                        Name = "Validity",
                        Attributes = ColumnAttributesEnum.adColNullable, //允许空值
                        Type = ADOX.DataTypeEnum.adDate,
                    };
                    //库存 Stock
                    ADOX.ColumnClass Stock = new ADOX.ColumnClass
                    {
                        //ParentCatalog = catalog,
                        Name = "Stock",
                        Attributes = ColumnAttributesEnum.adColNullable, //允许空值
                        Type = ADOX.DataTypeEnum.adInteger,
                        DefinedSize = 100
                    };
                    //质量状况 Quality
                    ADOX.ColumnClass Quality = new ADOX.ColumnClass
                    {
                        //ParentCatalog = catalog,
                        Name = "Quality",
                        Attributes = ColumnAttributesEnum.adColNullable, //允许空值
                        Type = ADOX.DataTypeEnum.adVarWChar,
                        DefinedSize = 255
                    };

                    #endregion
                    List<ADOX.ColumnClass> columns = new List<ADOX.ColumnClass>();
                    columns.Add(DrugsName);
                    columns.Add(Specification);
                    columns.Add(Model);
                    columns.Add(Unti);
                    columns.Add(Price);
                    columns.Add(Origin);
                    columns.Add(Batch);
                    columns.Add(ProductionDate);
                    columns.Add(Validity);
                    columns.Add(Stock);
                    columns.Add(Quality);
                    AccessDbHelper.CreateAccessTable(filePath, "DrugsTable", columns);

                }
                else
                {
                    MessageBox.Show("数据库创建失败！请添加数据[DrugsDatabase.mdb]到程序运行目录下。");
                }
            }
            else
            {
                ReflashDataGridView();
            }
            #endregion
            #region DataGridView 按钮渲染
            DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
            btnDel.Name = "colbtnDel";
            btnDel.HeaderText = "操作";
            btnDel.DefaultCellStyle.NullValue = "删除";

            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            btnEdit.Name = "colbtnEdit";
            btnEdit.HeaderText = "操作";
            btnEdit.DefaultCellStyle.NullValue = "编辑";

            DataGridViewButtonColumn btnPrint = new DataGridViewButtonColumn();
            btnPrint.Name = "colbtnPrint";
            btnPrint.HeaderText = "操作";
            btnPrint.DefaultCellStyle.NullValue = "打印";

            dataGridView1.Columns.Add(btnDel);
            dataGridView1.Columns.Add(btnEdit);
            dataGridView1.Columns.Add(btnPrint);

            DataGridViewButtonColumn btnPrintDel = new DataGridViewButtonColumn();
            btnPrintDel.Name = "btnPrintDel";
            btnPrintDel.HeaderText = "操作";
            btnPrintDel.DefaultCellStyle.NullValue = "删除";
            dataGridView3.Columns.Add(btnPrintDel);

            #endregion
        }

        private void AddGoods_Click(object sender, EventArgs e)
        {
            new AddGoodsForm().ShowDialog();
            ReflashDataGridView();
        }

        private void ReflashDataGridView() {
            string strSQL = "select *from DrugsTable";
            dataGridView1.DataSource = AccessDbHelper.ExecuteDataTable(strSQL);
        }

        private void btnSelectDrugs_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select *from DrugsTable"); ;
            if (txtDrugs.Text.Trim() != string.Empty)
                sql += string.Format(" where DrugsName like '%{0}%'", txtDrugs.Text.Trim());
            DataTable dt = AccessDbHelper.ExecuteDataTable(sql);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //删除
            if (dataGridView1.Columns[e.ColumnIndex].Name == "colbtnDel")
            {
                int RowIndex = Convert.ToInt32(e.RowIndex);
                //点击按钮操作
                string DrugsID = dataGridView1.Rows[RowIndex].Cells["DrugsID"].Value.ToString();
                string DrugsName = dataGridView1.Rows[RowIndex].Cells["DrugsName"].Value.ToString();

                DialogResult dr = MessageBox.Show(string .Format("确认删除[{0}]吗", DrugsName), "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    //数据删除
                    string strSQL = string.Format("DELETE FROM DrugsTable WHERE DrugsID = {0} ", DrugsID);
                    if (AccessDbHelper.ExecuteNonQuery(strSQL) > 0)
                    {
                        //刷新DataGridView
                        btnSelectDrugs_Click(null, null);
                    }
                    else
                        MessageBox.Show(string.Format("删除时 [{0}] 失败！", DrugsName));
                }
                
            }
            //编辑
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "colbtnEdit")
            {
                try
                {
                    int RowIndex = Convert.ToInt32(e.RowIndex);
                    //主键
                    string DrugsID = dataGridView1.Rows[RowIndex].Cells["DrugsID"].Value.ToString();
                    //取得其他字段值
                    string DrugsName = dataGridView1.Rows[RowIndex].Cells["DrugsName"].Value.ToString(); ;
                    string Specification = dataGridView1.Rows[RowIndex].Cells["Specification"].Value.ToString();
                    string Model = dataGridView1.Rows[RowIndex].Cells["Model"].Value.ToString();
                    string Unti = dataGridView1.Rows[RowIndex].Cells["Unti"].Value.ToString();
                    string Price = dataGridView1.Rows[RowIndex].Cells["Price"].Value.ToString();
                    string Origin = dataGridView1.Rows[RowIndex].Cells["Origin"].Value.ToString();
                    string Batch = dataGridView1.Rows[RowIndex].Cells["Batch"].Value.ToString();
                    string ProductionDate = dataGridView1.Rows[RowIndex].Cells["ProductionDate"].Value.ToString();
                    string Validity = dataGridView1.Rows[RowIndex].Cells["Validity"].Value.ToString();
                    string Stock = dataGridView1.Rows[RowIndex].Cells["Stock"].Value.ToString();
                    string Quality = dataGridView1.Rows[RowIndex].Cells["Quality"].Value.ToString();

                    //构造执行语句
                    string strSQL = string.Format("UPDATE DrugsTable SET DrugsName='{0}',Specification='{1}',Model='{2}',Unti='{3}',Price='{4}',Origin='{5}',Batch='{6}',ProductionDate='{7}',Validity='{8}',Stock='{9}',Quality='{10}' WHERE DrugsID={11}",
                        DrugsName,
                        Specification,
                        Model,
                        Unti,
                        Price == null || Price == string.Empty?"0":Price,
                        Origin,
                        Batch,
                        ProductionDate,
                        Validity,
                        Stock == null || Stock == string.Empty ? "0" : Stock,
                        Quality,
                        DrugsID
                        );
                    if (AccessDbHelper.ExecuteNonQuery(strSQL) > 0)
                    {
                        MessageBox.Show("修改成功！");
                        //刷新DataGridView
                        btnSelectDrugs_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("修改失败！请检查数据是否填写正确");
                    }
                }
                catch
                {
                    MessageBox.Show("修改失败！");
                }
            }
            //打印
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "colbtnPrint")
            {
                try
                {
                    int RowIndex = Convert.ToInt32(e.RowIndex);
                    //主键
                    string DrugsID = dataGridView1.Rows[RowIndex].Cells["DrugsID"].Value.ToString();
                    string DrugsName = dataGridView1.Rows[RowIndex].Cells["DrugsName"].Value.ToString();
                    int GoodsCount = Convert.ToInt32(dataGridView1.Rows[RowIndex].Cells["Stock"].Value);
                    CallBack AddPrint_CallBack = new CallBack(AddPrintItemToGridView);

                    new AddPrint(AddPrint_CallBack,RowIndex, DrugsName, GoodsCount).ShowDialog();

                    //MessageBox.Show(dataGridView3.Rows[0].Cells[0].Value.ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //取消打印
            if (dataGridView3.Columns[e.ColumnIndex].Name == "btnPrintDel")
            {

                int RowIndex = Convert.ToInt32(e.RowIndex);
                string DrugsID = dataGridView3.Rows[RowIndex].Cells[0].Value.ToString();
                string SelectSQL = string.Format("select *from DrugsTable where DrugsID={0}", DrugsID);
                string UpdateSQL;
                DataTable dt = AccessDbHelper.ExecuteDataTable(SelectSQL);

                //库存数据变更

                //查询现有库存数量
                int stock = Convert.ToInt32(dt.Rows[0]["Stock"]) ;

                //删除打印的数量与现有库存相加
                stock += Convert.ToInt32(dataGridView3.Rows[RowIndex].Cells[10].Value);
                //更新现有库存数据                
                UpdateSQL = string.Format("UPDATE DrugsTable SET Stock='{0}' WHERE DrugsID={1}", stock, DrugsID);
                if (!(AccessDbHelper.ExecuteNonQuery(UpdateSQL) > 0))
                    MessageBox.Show("更新库存失败！");

                //删除打印区的数据
                DataGridViewRow row = dataGridView3.Rows[RowIndex];
                dataGridView3.Rows.Remove(row);

                //重新计算价格
                CountNum();
                //刷新库存
                btnSelectDrugs_Click(null, null);
            }

        }
        //清除打印区
        private void btnClearPrint_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn Columns in dataGridView3.Columns)
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    //获取取消打印行的数据的主键
                    string DrugsID = row.Cells[0].Value.ToString();
                    //查询取消打印行现有库存数量
                    string SelectSQL = string.Format("select *from DrugsTable where DrugsID={0}", DrugsID);
                    DataTable dt = AccessDbHelper.ExecuteDataTable(SelectSQL);
                    int stock = Convert.ToInt32(dt.Rows[0]["Stock"]);
                    //删除打印的数量与现有库存相加
                    stock += Convert.ToInt32(row.Cells[10].Value);
                    //更新现有库存数据                
                    string UpdateSQL = string.Format("UPDATE DrugsTable SET Stock='{0}' WHERE DrugsID={1}", stock, DrugsID);
                    if (!(AccessDbHelper.ExecuteNonQuery(UpdateSQL) > 0))
                        MessageBox.Show("更新库存失败！");

                    dataGridView3.Rows.Remove(row);//删除行    
                }
            }
            //刷新表格数据
            btnSelectDrugs_Click(null, null);
            //清空：计算价格
            CountNum();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(string.Format("购货单位：{0}", PrintParams["ReportParameterCostume"]));
                #region 构造数据集参数
                DataTable dt = new DataTable();
                string[] heard = new string[] { "药品名称", "规格", "型号", "单位", "数量", "单价", "金额", "产地", "批准批号", "生产日期", "有效期", "质量状况" };
                foreach (var item in heard)
                {
                    if (item == "金额")
                        dt.Columns.Add(item, typeof(double));
                    else
                        dt.Columns.Add(item, typeof(string));

                }
                //提取GridView数据处理
                for (int i = 0; i < dataGridView3.RowCount; i++)
                {
                    dt.Rows.Add(new object[] {
                    dataGridView3.Rows[i].Cells[1].Value.ToString(),//药品名称
                    dataGridView3.Rows[i].Cells[2].Value.ToString(),//规格
                    dataGridView3.Rows[i].Cells[3].Value.ToString(),//型号
                    dataGridView3.Rows[i].Cells[4].Value.ToString(),//单位
                    dataGridView3.Rows[i].Cells[10].Value.ToString(),//数量
                    dataGridView3.Rows[i].Cells[5].Value.ToString(),//单价
                    Convert.ToDouble(Convert.ToInt32(dataGridView3.Rows[i].Cells[10].Value)*Convert.ToDouble(dataGridView3.Rows[i].Cells[5].Value)).ToString("0.00"),//金额
                    dataGridView3.Rows[i].Cells[6].Value.ToString(),//产地
                    dataGridView3.Rows[i].Cells[7].Value.ToString(),//批准批号
                    dataGridView3.Rows[i].Cells[8].Value.ToString(),//生产日期
                    dataGridView3.Rows[i].Cells[9].Value.ToString(),//有日期
                    dataGridView3.Rows[i].Cells[11].Value.ToString()//质量状况
                });
                }
                #endregion

                //ReportParameterCostume ReportParameterPrintDate ReportParameterID ReportParameterRed ReportParameterCountAll
                //ReportParameterCountAllCN ReportParameterDrawer ReportParameterPhone ReportParameterRecAddr ReportParameterWhite ReportParameterBlue 
                //ReportParameterSalesman ReportParameterVerification ReportParameterDelivery ReportParameterSaver ReportParameterPrintTime
                #region 构造打印参数
                PrintParams["ReportParameterCountAll"] = CountNum().ToString();
                PrintParams["ReportParameterCountAllCN"] = Money.MoneyToUpper(CountNum().ToString("0.00"));
                PrintParams["ReportParameterPrintTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                PrintParams["ReportParameterPrintDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                ReportParameter Costume = new ReportParameter("ReportParameterCostume", string.Format("购货单位:{0}", PrintParams["ReportParameterCostume"]));
                ReportParameter PrintDate = new ReportParameter("ReportParameterPrintDate", string.Format("单据日期:{0}", PrintParams["ReportParameterPrintDate"]));
                ReportParameter ID = new ReportParameter("ReportParameterID", string.Format("单号:{0}", PrintParams["ReportParameterID"]));
                ReportParameter Red = new ReportParameter("ReportParameterRed", string.Format("{0}", PrintParams["ReportParameterRed"]));
                ReportParameter CountAll = new ReportParameter("ReportParameterCountAll", string.Format("合计:{0}", PrintParams["ReportParameterCountAll"]));
                ReportParameter CountAllCN = new ReportParameter("ReportParameterCountAllCN", string.Format("合计大写:{0}", PrintParams["ReportParameterCountAllCN"]));
                ReportParameter Drawer = new ReportParameter("ReportParameterDrawer", string.Format("开票员:{0}", PrintParams["ReportParameterDrawer"]));
                ReportParameter Phone = new ReportParameter("ReportParameterPhone", string.Format("联系电话:{0}", PrintParams["ReportParameterPhone"]));
                ReportParameter RecAddr = new ReportParameter("ReportParameterRecAddr", string.Format("收货地址:{0}", PrintParams["ReportParameterRecAddr"]));
                ReportParameter White = new ReportParameter("ReportParameterWhite", string.Format("白联:{0}", PrintParams["ReportParameterWhite"]));
                ReportParameter Blue = new ReportParameter("ReportParameterBlue", string.Format("蓝联:{0}", PrintParams["ReportParameterBlue"]));
                ReportParameter Salesman = new ReportParameter("ReportParameterSalesman", string.Format("业务员:{0}", PrintParams["ReportParameterSalesman"]));
                ReportParameter Verification = new ReportParameter("ReportParameterVerification", string.Format("复合:{0}", PrintParams["ReportParameterVerification"]));
                ReportParameter Delivery = new ReportParameter("ReportParameterDelivery", string.Format("送货:{0}", PrintParams["ReportParameterDelivery"]));
                ReportParameter Saver = new ReportParameter("ReportParameterSaver", string.Format("保管:{0}", PrintParams["ReportParameterSaver"]));
                ReportParameter PrintTime = new ReportParameter("ReportParameterPrintTime", string.Format("打印时间:{0}", PrintParams["ReportParameterPrintTime"]));
                ReportParameter Yellow = new ReportParameter("ReportParameterYellow", string.Format("黄联:{0}", PrintParams["ReportParameterYellow"]));

                ReportParameter[] parameters = new ReportParameter[] {
                Costume, PrintDate, ID, Red, CountAll, CountAllCN, Drawer, Phone,
                RecAddr, White, Blue, Salesman, Verification,Delivery,Saver,PrintTime,Yellow};
                        
                #endregion
                new PrintPreview(dt, parameters).ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            new PrintSetting(ref PrintParams).ShowDialog();
        }

        //统计打印区药品的价格
        public double CountNum() {
            double num = 0;
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                num += (Convert.ToDouble(dataGridView3.Rows[i].Cells[5].Value) * Convert.ToInt32(dataGridView3.Rows[i].Cells[10].Value));
            }
            lbCountNumRMB.Text = string.Format("合计:{0}",num.ToString("0.00"));
            lbCountRMBCN.Text = string.Format("合计大写:{0}", Money.MoneyToUpper(num.ToString("0.00")));
            return num;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RowIndex">第RowIndex行</param>
        /// <param name="GoodsCount">出仓货物数量</param>
        void AddPrintItemToGridView(int RowIndex,int GoodsCount) {
            //获取主键标识
            string DrugsID = dataGridView1.Rows[RowIndex].Cells["DrugsID"].Value.ToString();
            int surplus = Convert.ToInt32(dataGridView1.Rows[RowIndex].Cells["Stock"].Value) - GoodsCount;
            string strSQL = string.Format("UPDATE DrugsTable SET Stock='{0}' WHERE DrugsID={1}", surplus, DrugsID);
            try
            {
                //数据库更新库存数据

                //BUG 执行成功依然返回0
                //if (AccessDbHelper.ExecuteNonQuery(strSQL) > 0)
                //{
                //    //只提示用户库存未更新！不影响用户使用打印数据
                //    MessageBox.Show("库存信息更新失败！您依然可以进行打印，但库存信息有错误！");
                //}

                //更新库存
                AccessDbHelper.ExecuteNonQuery(strSQL);
                //取得其他字段值
                string DrugsName = dataGridView1.Rows[RowIndex].Cells["DrugsName"].Value.ToString();
                string Specification = dataGridView1.Rows[RowIndex].Cells["Specification"].Value.ToString();
                string Model = dataGridView1.Rows[RowIndex].Cells["Model"].Value.ToString();
                string Unti = dataGridView1.Rows[RowIndex].Cells["Unti"].Value.ToString();
                string Price = dataGridView1.Rows[RowIndex].Cells["Price"].Value.ToString();
                string Origin = dataGridView1.Rows[RowIndex].Cells["Origin"].Value.ToString();
                string Batch = dataGridView1.Rows[RowIndex].Cells["Batch"].Value.ToString();
                string ProductionDate = Convert.ToDateTime(dataGridView1.Rows[RowIndex].Cells["ProductionDate"].Value).ToString("yyyy-MM-dd");
                string Validity = Convert.ToDateTime(dataGridView1.Rows[RowIndex].Cells["Validity"].Value).ToString("yyyy-MM-dd");
                string Stock = GoodsCount.ToString();
                string Quality = dataGridView1.Rows[RowIndex].Cells["Quality"].Value.ToString();
                dataGridView3.Rows.Add(DrugsID, DrugsName, Specification, Model, Unti, Price, Origin, Batch, ProductionDate, Validity, Stock, Quality);
                //清空：计算价格
                CountNum();
            }
            catch (Exception ex)
            {
                MessageBox.Show("库存信息更新失败！请稍后再试。错误信息:" + ex.Message);
            }
            finally {
                //刷新表格数据
                btnSelectDrugs_Click(null,null);
            }

        }
    }
}

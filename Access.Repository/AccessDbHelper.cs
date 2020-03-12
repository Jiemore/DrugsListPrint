using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOX;
namespace Access.Repository
{
    public static class AccessDbHelper
    {
        static string connStr = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=DrugsDatabase.mdb;jet oledb:database password=123;";
        private static OleDbConnection accessConnection; //Access数据库连接
        private static string tableName;
        //@"Provider= Microsoft.Jet.OLEDB.4.0;Data Source = " + Environment.CurrentDirectory + @"\DrugsDatabase.mdb";

        /// <summary>
        /// 创建access数据库
        /// </summary>
        /// <param name="filePath">数据库文件的全路径，如 D:\\NewDb.mdb</param>
        public static bool CreateAccessDb(string filePath)
        {
            ADOX.Catalog catalog = new Catalog();
            if (!File.Exists(filePath))
            {
                try
                {
                    catalog.Create("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=DrugsDatabase.mdb;jet oledb:database password=123;");

                }
                catch (System.Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
        //在指定的Access数据库中穿点指定的表格
        public static bool CreateAccessTable(string filePath, string tbName,List<ADOX.ColumnClass> columns)
        {
            ADOX.Catalog catalog = new Catalog();
            //数据库文件不存在则创建
            try
            {
                ADODB.Connection cn = new ADODB.Connection();
                try
                {
                    cn.Open(connStr, null, null, -1);
                }
                catch (System.Exception ex)
                {
                    Trace.TraceWarning("Access连接打开失败", ex);
                    return false;
                }

                catalog.ActiveConnection = cn;
                ADOX.Table table = new ADOX.Table();
                table.ParentCatalog = catalog;
                table.Name = tbName;

                //主键
                ADOX.ColumnClass DrugsID = new ADOX.ColumnClass();
                DrugsID.ParentCatalog = catalog;
                DrugsID.Type = ADOX.DataTypeEnum.adInteger; // 必须先设置字段类型 
                DrugsID.Name = "DrugsID";
                DrugsID.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                DrugsID.Properties["AutoIncrement"].Value = true;
                DrugsID.Type = ADOX.DataTypeEnum.adInteger;
                table.Columns.Append(DrugsID, ADOX.DataTypeEnum.adInteger, 0);

                foreach (var column in columns)
                {
                    table.Columns.Append(column); //默认数据类型和字段大小
                }

                //主键
                table.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "DrugsID", "", "");
                catalog.Tables.Append(table);

                //accessConnection = new OleDbConnection(cn.ConnectionString);
                //tableName = tbName;
                //try
                //{
                //    accessConnection.Open();
                //}
                //catch (System.Exception ex)
                //{
                //    Trace.TraceWarning("Access连接打开失败", ex);
                //    return false;
                //}
                cn.Close();
            }
            catch (System.Exception e)
            {
                Trace.TraceWarning("创建Access表出错", e);
                return false;
            }
            return true;
        }
        #region 执行查询指令，获取返回DataTable
        /// <summary>
        /// 执行查询指令，获取返回DataTable
        /// </summary>
        /// <param name="sql">查询sql语句</param>
        /// <param name="param">sql语句的参数</param>
        /// <returns></returns> 
        public static DataTable ExecuteDataTable(string sql, params OleDbParameter[] param)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    DataTable dt = new DataTable();
                    OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
                    sda.Fill(dt);
                    return (dt);
                }
            }
        }

        #endregion
        #region 执行增加、删除、修改指令
        /// <summary>
        /// 执行增加、删除、修改指令
        /// </summary>
        /// <param name="sql">增加、删除、修改的sql语句</param>
        /// <param name="param">sql语句的参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params OleDbParameter[] param)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    conn.Open();
                    return (cmd.ExecuteNonQuery());
                }
            }
        }
        /// <summary>
        /// 执行增加、删除、修改指令
        /// </summary>
        /// <param name="sql">增加、删除、修改的sql语句</param>
        /// <param name="param">sql语句的参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    conn.Open();
                    return (cmd.ExecuteNonQuery());
                }
            }
        }
        #endregion
        #region 执行查询指令，获取返回的首行首列的值
        /// <summary>
        /// 执行查询指令，获取返回的首行首列的值
        /// </summary>
        /// <param name="sql">查询sql语句</param>
        /// <param name="param">sql语句的参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params OleDbParameter[] param)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    conn.Open();
                    return (cmd.ExecuteScalar());
                }
            }
        }
        #endregion
        #region 执行查询指令，获取返回的datareader
        /// <summary>
        /// 执行查询指令，获取返回的datareader
        /// </summary>
        /// <param name="sql">查询sql语句</param>
        /// <param name="param">sql语句的参数</param>
        /// <returns></returns>
        public static OleDbDataReader ExecuteReader(string sql, params OleDbParameter[] param)
        {
            OleDbConnection conn = new OleDbConnection(connStr);
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            if (param != null)
            {
                cmd.Parameters.AddRange(param);
            }
            conn.Open();
            return (cmd.ExecuteReader(CommandBehavior.CloseConnection));
        }
        #endregion
    }
}

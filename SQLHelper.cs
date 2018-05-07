using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class SQLHelper
    {
        public string connStr = @"Data Source=.\SQLExpress;Initial Catalog=CameraDB;Persist Security Info=True;User ID=sa;Password=123456";
        public SqlCommand cmd = null;
        #region 配置数据库连接
        public SqlConnection GetSqlConnection()
        {
            //abcdefg
            SqlConnection sqlCnn = new SqlConnection(connStr);
            sqlCnn.Open();
            return sqlCnn;
        }
        #endregion

        #region 查询数据库并返回dataset
        public DataSet GetDataSet(string strSql)
        {
            using (SqlConnection sqlCnn = GetSqlConnection())
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(strSql, sqlCnn);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }
        #endregion

        #region 对一条记录进行增，删，修改
        public int ExecuteCommand(string strSql)
        {
            SqlConnection sqlCnn = new SqlConnection(connStr);
            sqlCnn.Open();
            try
            {
                SqlCommand SqlCmm = new SqlCommand(strSql, sqlCnn);
                int temp = SqlCmm.ExecuteNonQuery();
                sqlCnn.Close();
                return temp;
            }
            catch (Exception ex)
            {
                sqlCnn.Close();
                throw new Exception(ex.Message);
            }
        }
        #endregion
        /// <summary>
        /// 增   
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public int EditSql(string strsql)
        {
            SqlConnection sqlCnn = new SqlConnection(connStr);
            cmd = new SqlCommand(strsql, sqlCnn);
            try
            {
                sqlCnn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                return -1;
            }
            finally
            {
                sqlCnn.Close();
            }
        }

    }
}

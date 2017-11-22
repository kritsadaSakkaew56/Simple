using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using System.Configuration;

namespace PagingControl.DataAccess
{
    internal class DataBase
    {
        private string conString = "";
        public string ConnectionString
        {
            get
            {
                if (conString == "")
                    conString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                return conString;
            }
            set
            {
                conString = value;
            }
        }

        SqlConnection con;
        private CommandType commandType = CommandType.StoredProcedure;
        private SqlTransaction trans;
        public CommandType SQLCommandType
        {
            get 
            {
                return commandType;
            }
            set
            {
                commandType = value;
            }
        }
       public List<SqlParameter> Param = new List<SqlParameter>();
      
        public DataBase()
        {
            con = new SqlConnection(this.ConnectionString);
        }
        public void BeginTransaction()
        {
            if (con.State == ConnectionState.Closed) ;
            con.Open();
            trans = con.BeginTransaction();

        }

        public void ComitTransaction()
        {

            trans.Commit();
            con.Close();
        }
        public void RollbackTransaction()
        {
            trans.Rollback();
            con.Close();
        }
        // ใช้สำหรับการประมวลผลคำสั่ง SQL 
        public int ExecuteNonQuery(string sql)
        {
            return this.ExecuteNonQuery(sql, this.Param);
        }
        private int ExecuteNonQuery(string sql, List<SqlParameter> param)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = this.SQLCommandType;

            int retValue = 0;

            for (int i = 0; i < param.Count; i++)
            {
                cmd.Parameters.Add(param[i]);
            }

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                retValue = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                    con.Close();
            }

            return retValue;
        }

        //ใช้ Execute คำสั่ง ที่ต้องการผลลัพย์ ที่เป็น ค่าคงที่ ค่าเดียวส่วนมากจะใช้ รัน คำสั่ง aggregate function พวก MIN, MAX, AVE, COUNT
        public object ExecuteScalar(string sql)
        {
            return this.ExecuteScalar(sql, this.Param);
        }
        public object ExecuteScalar(string sql, List<SqlParameter> param)
        {
            SqlCommand cmm = new SqlCommand(sql, con);
            cmm.CommandType = this.SQLCommandType;
            object retValue = 0;

            for (int i = 0; i < param.Count; i++)
            {
                cmm.Parameters.Add(param[i]);
            }
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                retValue = cmm.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                    con.Close();
            }

            return retValue;
        }

        public DataTable ExecuteDataTable(string sql)
        {
            return this.ExecuteDataTable(sql, this.Param);
        }
        public DataTable ExecuteDataTable(string sql, List<SqlParameter> param)
        {
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.SelectCommand.CommandType = this.SQLCommandType;

            for (int i = 0; i < param.Count; i++)
            {
                ad.SelectCommand.Parameters.Add(param[i]);
            }
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                ad.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                    con.Close();
            }

            return dt;
        }

        public DataSet ExecuteDataSet(string sql)
        {
            return this.ExecuteDataSet(sql, this.Param);
        }
        public DataSet ExecuteDataSet(string sql, List<SqlParameter> param)
        {
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            ad.SelectCommand.CommandType = this.SQLCommandType;

            for (int i = 0; i < param.Count; i++)
            {
                ad.SelectCommand.Parameters.Add(param[i]);
            }
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                ad.Fill(ds);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                    con.Close();
            }

            return ds;
        }


    }


}
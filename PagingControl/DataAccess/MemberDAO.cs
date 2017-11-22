using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PagingControl.DataAccess
{
    public class MemberDAO
    {
        public DataSet GetUse()
        {
            DataBase db = new DataBase();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@type", "BindMasterDataAll"));
            return db.ExecuteDataSet("SP_ExpendSuspend", param);

        }
        public DataTable GetUseByOrder(string AgreementNo)
        {
            DataBase db = new DataBase();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@AgreementNo", AgreementNo));
            param.Add(new SqlParameter("@type", "BindMasterDataByOrder"));
            return db.ExecuteDataTable("SP_ExpendSuspend",param);

        }
        public DataTable GetUseDetialByOrder(string AgreementNo)
        {
            DataBase db = new DataBase();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@AgreementNo", AgreementNo));
            param.Add(new SqlParameter("@type", "BindDetialDataByOrder"));
            return db.ExecuteDataTable("SP_ExpendSuspend", param);

        }

    }
}




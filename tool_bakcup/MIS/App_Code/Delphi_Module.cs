using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
/*
/// <summary>
/// Delphi_Issue Base:
/// Created by: Naresh
/// Creation Date: 28 May 14
/// </summary>
 * */
public class Delphi_Module
{
        private Delphi_Customer oCust;
        private Delphi_Common oCom;
        private datasourceIBSQL oSql;
        private datasourceSQL oSql1;
        private string sSql = "";
 
	public Delphi_Module()
	{
		
        oCust = new Delphi_Customer();
        oCom = new Delphi_Common();
        oSql = new datasourceIBSQL();
	}

   public string InsertModule(string[] aModule,int intNoOfTimes)
    {
        string Status = "";
        try
        {
            if (intNoOfTimes > 0)
            {
                for (int i = 0; i < intNoOfTimes; i++)
                {
                   
                    SqlParameter[] param = new SqlParameter[6];
                    sSql = "[spInsertModule]";
                    param[0] = oSql.NewParameter("@PROJECTNO", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aModule[0]);
                    param[1] = oSql.NewParameter("@CUSTNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[1]);
                    param[2] = oSql.NewParameter("@moponumber", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[2]);
                    param[3] = oSql.NewParameter("@DNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[3]);
                    param[4] = oSql.NewParameter("@STNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[4]);
                    param[5] = oSql.NewParameter("@STYPENO", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aModule[5]);

                    SqlParameter sqlparm = new SqlParameter();
                    sqlparm.ParameterName = "@OUTPUT";
                    sqlparm.SqlDbType = SqlDbType.VarChar;
                    sqlparm.Direction = ParameterDirection.Output;
                    sqlparm.SqlValue = "";
                    Status = oSql.Execute_SP(sSql, param, sqlparm);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;

    }
}
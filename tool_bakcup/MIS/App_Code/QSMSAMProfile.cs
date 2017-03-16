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
/// <summary>
/// Summary description for QSMSAMProfile
/// </summary>
public class QSMSAMProfile
{
    private CustomerBase oCust;
    private Common oCom;
    private datasourceSQL oSql;
    private string sSql;
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;
	public QSMSAMProfile()
	{
        oCust = new CustomerBase();
        oCom = new Common();
        oSql = new datasourceSQL();
		
	}
    public DataSet fnGetAllCustomers()
    {
        return this.oCust.getAllCustomers();
    }
    
    public DataSet getJournalsByCustomer(string sCustomerID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetJournalList]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getSAMProfile(string strCustid, string strJrnlid)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
           // sSql = "[sp_select_rows_QMSSAMProfile]";
            sSql = "[spGet_QMSSAMProfile]";
            param[0] = oSql.NewParameter("@Customer_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, strCustid);
            param[1] = oSql.NewParameter("@Journal_Code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, strJrnlid);
            ds = oSql.FillDataSet_SP(sSql, param);
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        { 
        }
        return ds;
    }
    public DataSet getSAMProfileHistry(string strSAMProfileid, string strCustid, string strJrnlid)
    {
        
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spget_SAMProfile_Histry]";
            param[0] = oSql.NewParameter("@SAMProfile_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, strSAMProfileid);
            param[1] = oSql.NewParameter("@Customer_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, strCustid);
            param[2] = oSql.NewParameter("@Journal_Code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, strJrnlid);
            
            ds = oSql.FillDataSet_SP(sSql, param);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
        }
        return ds;

    }
    public int selectrowQSMSAMProfile(string[] aSAMProfile)
    {
        SqlConnection oCon = null;
        SqlCommand oCmd = null;
        SqlDataAdapter oAdp = null;
       // SqlDataReader oRdr = null;
        string oConstr = ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString;
        //int Rtnvalue=0;
        int oRtnvalue=0;
        try
        {
            oCon = new SqlConnection(oConstr);
           // oCon.Open();
            oCmd = new SqlCommand("spSelectRow_QMSSAMProfile",oCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param1 = new SqlParameter("@Customer_code", SqlDbType.Int);
            param1.Direction = ParameterDirection.Input;
            param1.Value = aSAMProfile[0];
            oCmd.Parameters.Add(param1);
            SqlParameter param2 = new SqlParameter("@Journal_Code", SqlDbType.Int);
            param2.Direction = ParameterDirection.Input;
            param2.Value = aSAMProfile[1];
            oCmd.Parameters.Add(param2);
            SqlParameter param3 = new SqlParameter("@OUTPUT", SqlDbType.Int);
            param3.Direction = ParameterDirection.ReturnValue;
            param3.Value = aSAMProfile[2];
            oCmd.Parameters.Add(param3);
            oCon.Open();
            //oRtnvalue =Convert.ToInt32(oCmd.ExecuteScalar());
            oCmd.ExecuteNonQuery();
            //oRtnvalue = int.Parse( param3.Value.ToString());
            oRtnvalue = (int)oCmd.Parameters["@OUTPUT"].Value;

            //oAdp = new SqlDataAdapter(oCmd);
            //DataSet oDST = new DataSet();
            //oAdp.Fill(oDST);
            //oRtnvalue = Convert.ToInt32(oDST.Tables[0].Rows[0][0].ToString());

            //oCon.Close();
            //oCon.Open();
            //oRdr = oCmd.ExecuteReader();
            //while(oRdr.Read())
            //{
            //    oRtnvalue = Convert.ToInt32(param3.Value);
            //}
           
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            //if (oRdr != null) oRdr.Close();
            if (oCon != null && oCon.State == ConnectionState.Open) oCon.Close();
        }
        return oRtnvalue;

       //// int Rslt;
       //////string output;
       //// sSql = "";
       //// DataSet ds = new DataSet();
       //// SqlParameter[] param = new SqlParameter[3];
       //// SqlParameter outparam=new SqlParameter();
       //// try
       //// {
       ////     sSql = "[spSelectRow_QMSSAMProfile]";
       ////     param[0] = oSql.NewParameter("@Customer_code", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[0]);
       ////     param[1] = oSql.NewParameter("@Journal_Code", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[1]);
       ////     param[2] = oSql.NewParameter("@OUTPUT", SqlDbType.Int, int.MaxValue, ParameterDirection.Output, 0);
       ////     Rslt = Convert.ToInt32(oSql.Execute_SP(sSql, param, outparam));
       //// }
       //// catch (Exception ex)
       //// {
       ////     throw new Exception(ex.Message);
       //// }
       //// finally
       //// { }
       //// return Rslt;
    }

    public bool insertQMSSAMProfile(string[] aSAMProfile)
    {
        bool status;
        SqlParameter[] param=new SqlParameter[6];
        try
        {
            //sSql="[sp_Insert_QMSSAMProfile]";
            sSql = "[spInsert_QMSSAMProfile]";
            param[0] = oSql.NewParameter("@Customer_code", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[0]);
            param[1] = oSql.NewParameter("@Journal_Code", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[1]);
            param[2] = oSql.NewParameter("@SAMProfile_Title_Name", SqlDbType.NVarChar, int.MaxValue, ParameterDirection.Input, aSAMProfile[2]);
            param[3] = oSql.NewParameter("@SAMProfile_Title_Desc", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aSAMProfile[3]);
            param[4] = oSql.NewParameter("@Created_by", SqlDbType.NVarChar, int.MaxValue, ParameterDirection.Input, aSAMProfile[4]);
            param[5] = oSql.NewParameter("@SAMProfile_code", SqlDbType.Int, int.MaxValue, ParameterDirection.Output, 0);
            status= oSql.Execute_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new  Exception(ex.Message);
        }
        return status;
    }
    public bool updateQMSSAMProfile(string[] aSAMProfile)
    {
        bool status;
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            //Live
          //  sSql = "[sp_Update_QMSSAMProfile]";
            sSql = "[spUpdate_QMSSAMProfile]";
            param[0] = oSql.NewParameter("@SAMProfile_code", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[0]);
            param[1] = oSql.NewParameter("@Customer_code", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[1]);
            param[2] = oSql.NewParameter("@Journal_Code", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[2]);
            param[3] = oSql.NewParameter("@SAMProfile_Title_Name", SqlDbType.NVarChar, int.MaxValue, ParameterDirection.Input, aSAMProfile[3]);
            param[4] = oSql.NewParameter("@SAMProfile_Title_Desc", SqlDbType.NVarChar, int.MaxValue, ParameterDirection.Input, aSAMProfile[4]);
            param[5] = oSql.NewParameter("@Updated_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[5]);
            status = oSql.Execute_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return status;
    }
    public bool SPupdateQMSSAMProfile(string[] aSAMProfile)
    {
        bool status;
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            
            // sSql = "[sp_Update_QMSSAMProfile]";
            sSql = "[sp_Update_QMSSAMProfile]";
            param[0] = oSql.NewParameter("@SAMProfile_code", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[0]);
            param[1] = oSql.NewParameter("@Customer_code", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[1]);
            param[2] = oSql.NewParameter("@Journal_Code", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[2]);
            param[3] = oSql.NewParameter("@SAMProfile_Title_Name", SqlDbType.NVarChar, int.MaxValue, ParameterDirection.Input, aSAMProfile[3]);
            param[4] = oSql.NewParameter("@SAMProfile_Title_Desc", SqlDbType.NVarChar, int.MaxValue, ParameterDirection.Input, aSAMProfile[4]);
            param[5] = oSql.NewParameter("@Updated_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aSAMProfile[5]);
            param[6] = oSql.NewParameter("@Updated_Date", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aSAMProfile[6]);
            status = oSql.Execute_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return status;
    }

    public DataSet getJournalsByCW(string sJournal)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetCWJournalList]";
            param[0] = oSql.NewParameter("@sJournal", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournal);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    private void opencon()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString;
        oConn = new SqlConnection(sConStr);
        oConn.Open();
    }

    private void closecon()
    {
        if (oConn != null)
        {
            if (oConn.State != ConnectionState.Closed)
                oConn.Close();
            oConn.Dispose();
        }
    }
    public int ExcSProcedure(string sProcName, string[,] sparameter, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        SqlTransaction sqltrans = null;
        char[] separator = new char[] { ',' };
        bool flg = false;
        string OutparamName = "";
        try
        {
            opencon();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
            sqltrans = oConn.BeginTransaction();
            ocmd.Transaction = sqltrans;
            ocmd.Parameters.Clear();
            int i;
            if (sparameter != null)
            {
                for (i = 0; i < sparameter.GetLength(0); i++)
                {
                    if (sparameter[i, 1].ToString().ToUpper() == "OUTPUT")
                    {

                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), "").Direction = ParameterDirection.Output;
                        flg = true;
                        OutparamName = sparameter[i, 0].ToString();
                    }
                    else if (sparameter[i, 1] == null || sparameter[i, 1].ToString() == "" || sparameter[i, 1].ToString() == "0")
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), DBNull.Value);
                    else
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), sparameter[i, 1]);
                }
            }
            ocmd.ExecuteNonQuery();
            sqltrans.Commit();
            if (flg == true)
                return Convert.ToInt32(ocmd.Parameters[OutparamName].Value);
            else
                return 0;
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            throw ex;
        }
        finally
        {
            ocmd = null;
            closecon();
        }

    }
}

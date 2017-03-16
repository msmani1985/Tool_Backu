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
/// Summary description for Sales_Local
/// </summary>
public class Sales_Local
{
    private datasourceSQL oSql = new datasourceSQL();
    private string sSql = "";
	public Sales_Local()
	{
		 oSql = new datasourceSQL();
	}
    public DataSet getPublishersList()
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "[spGetSalesLead_Local]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getPublishersList(char IsGroupMode)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetSalesLead_Local]";
            param[0] = oSql.NewParameter("@groupmode", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, IsGroupMode);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getPublishersSummary(string sCountry)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetSalesLeadFollowup_Local]";
            param[0] = oSql.NewParameter("@slcountry", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCountry);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getPublishersSummary1(string sCountry)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetSalesLeadSummary_Local]";
            param[0] = oSql.NewParameter("@slcountry", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCountry);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getPublishersDetails(string sPublisherID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetSalesLead_local]";
            param[0] = oSql.NewParameter("@salesleadno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sPublisherID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getLeadStatusTypes()
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "[spGetSalesStatusTypes_Local]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getLeadCategory()
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "[spGetSalesLeadCategory_Local]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getCountryList()
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "[spGetSalesCountry_Local]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getCommunicationTypes()
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "[spGetCommunicationTypes_Local]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getContactList(string sSaledLeadNo)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetSalesContact_Local]";
            param[0] = oSql.NewParameter("@salesleadno", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sSaledLeadNo);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getContactInfo(string sContactID, string sSaledLeadNo)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetSalesContact_Local]";
            param[0] = oSql.NewParameter("@ci_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sContactID);
            param[1] = oSql.NewParameter("@salesleadno", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sSaledLeadNo);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getCommuncationList(string sContactID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetCommunication_Local]";
            param[0] = oSql.NewParameter("@ci_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sContactID);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getCommuncationDetails(string sCommunicationID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetCommunication_Local]";
            param[0] = oSql.NewParameter("@co_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sCommunicationID);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getEventsList(string sStartDate, string sEndDate)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetSalesEventList_Local]";
            param[0] = oSql.NewParameter("@StartDate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStartDate);
            param[1] = oSql.NewParameter("@EndDate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEndDate);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getEvent(string sCurrentDate)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetSalesEvent_Local]";
            param[0] = oSql.NewParameter("@CurrentDate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCurrentDate);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public string InsertPublisherInfo(string[] aPubInfo)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[14];
        try
        {
            sSql = "[spInsertSalesLead_Local]";
            param[0] = oSql.NewParameter("@slcompany", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[0]);
            param[1] = oSql.NewParameter("@leadstatustype", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aPubInfo[1]);
            param[2] = oSql.NewParameter("@line1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[2]);
            param[3] = oSql.NewParameter("@slcity", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[3]);
            param[4] = oSql.NewParameter("@slstate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[4]);
            param[5] = oSql.NewParameter("@slpocode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[5]);
            param[6] = oSql.NewParameter("@slcountry", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[6]);
            param[7] = oSql.NewParameter("@sldescription", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aPubInfo[7]);
            param[8] = oSql.NewParameter("@sales_job_type_IDs", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[8]);
            param[9] = oSql.NewParameter("@sales_category_type_IDs", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[9]);
            param[10] = oSql.NewParameter("@createdby", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[10]);
            param[11] = oSql.NewParameter("@line2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[11]);
            param[12] = oSql.NewParameter("@line3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[12]);
            param[13] = oSql.NewParameter("@line4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[13]);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string UpdatePublisherInfo(string[] aPubInfo)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[14];
        try
        {
            sSql = "[spUpdateSalesLead_Local1]";
            param[0] = oSql.NewParameter("@salesleadno", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aPubInfo[0]);
            param[1] = oSql.NewParameter("@leadstatustype", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aPubInfo[1]);
            param[2] = oSql.NewParameter("@line1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[2]);
            param[3] = oSql.NewParameter("@slcity", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[3]);
            param[4] = oSql.NewParameter("@slstate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[4]);
            param[5] = oSql.NewParameter("@slpocode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[5]);
            param[6] = oSql.NewParameter("@slcountry", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[6]);
            param[7] = oSql.NewParameter("@sldescription", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aPubInfo[7]);
            param[8] = oSql.NewParameter("@sales_job_type_IDs", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[8]);
            param[9] = oSql.NewParameter("@sales_category_type_IDs", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[9]);
            param[10] = oSql.NewParameter("@line2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[10]);
            param[11] = oSql.NewParameter("@line3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[11]);
            param[12] = oSql.NewParameter("@line4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[12]);
            param[13] = oSql.NewParameter("@Company", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[13]);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string InsertSalesContact(string[] aContact)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            sSql = "[spInsertSalesContact_Local]";
            param[0] = oSql.NewParameter("@ci_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[0]);
            param[1] = oSql.NewParameter("@ci_title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[1]);
            param[2] = oSql.NewParameter("@ci_phone", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[2]);
            param[3] = oSql.NewParameter("@ci_fax", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[3]);
            param[4] = oSql.NewParameter("@ci_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[4]);
            param[5] = oSql.NewParameter("@ci_web", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[5]);
            param[6] = oSql.NewParameter("@salesleadno", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContact[6]);
            param[7] = oSql.NewParameter("@createdby", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[7]);
            param[8] = oSql.NewParameter("@skypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[8]);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string UpdateSalesContact(string[] aContact)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            sSql = "[spUpdateSalesContact_Local]";
            param[0] = oSql.NewParameter("@ci_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[0]);
            param[1] = oSql.NewParameter("@ci_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[1]);
            param[2] = oSql.NewParameter("@ci_title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[2]);
            param[3] = oSql.NewParameter("@ci_phone", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[3]);
            param[4] = oSql.NewParameter("@ci_fax", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[4]);
            param[5] = oSql.NewParameter("@ci_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[5]);
            param[6] = oSql.NewParameter("@ci_web", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[6]);
            param[7] = oSql.NewParameter("@salesleadno", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContact[7]);
            param[8] = oSql.NewParameter("@skypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[8]);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public DataSet getStateList(string ctyno)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetSales_State]";
            param[0] = oSql.NewParameter("@ctyno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, ctyno);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public void InsertState(string StName, string CTYNO)
    {
        oSql.ExcSProcedure("spInsertState", new string[,] { { "@StName", StName }, { "@ABRV", CTYNO } }, CommandType.StoredProcedure);
    }
}
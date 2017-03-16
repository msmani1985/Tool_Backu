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

/* Creatin Date: Saturday, June 05, 2010
 * Created by:  Royson 
 */
/// <summary>
/// Summary description for Sales
/// </summary>
public class Sales
{
    private datasourceSQL oSql = new datasourceSQL();
    private string sSql = "";
	public Sales(){
        oSql = new datasourceSQL();
	}
    public DataSet getPublishersList()
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "[spGetSalesLead]";
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
            sSql = "[spGetSalesLead]";
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
            sSql = "[spGetSalesLeadSummary]";
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
            sSql = "[spGetSalesLead]";
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
            sSql = "[spGetSalesStatusTypes]";
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
            sSql = "[spGetSalesLeadCategory]";
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
            sSql = "[spGetSalesCountry]";
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
            sSql = "[spGetCommunicationTypes]";
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
            sSql = "[spGetSalesContact]";
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
            sSql = "[spGetSalesContact]";
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
            sSql = "[spGetCommunication]";
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
            sSql = "[spGetCommunication]";
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
            sSql = "[spGetSalesEventList]";
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
            sSql = "[spGetSalesEvent]";
            param[0] = oSql.NewParameter("@CurrentDate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCurrentDate);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getInvoiceSalesAnlysis(string sCustID, string sJobTypeID, string sStartDate, string sEndDate)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            sSql = "[SPGET_SALES_ANALYSIS]";
            if (sCustID == "" || sCustID == "0")
                param[0] = oSql.NewParameter("@custid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            else
                param[0] = oSql.NewParameter("@custid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustID);
            if (sJobTypeID == "" || sJobTypeID == "0")
                param[1] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            else
                param[1] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[2] = oSql.NewParameter("@sdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStartDate);
            param[3] = oSql.NewParameter("@edate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEndDate);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public bool HasPayments(string sCustno)
    {
        bool status = false;
        switch (sCustno)
        {
            case "NULL":
                status = true;
                break;
            case "94":
                status = true;
                break;
            case "79":
                status = true;
                break;
            case "115":
                status = true;
                break;
            case "136":
                status = true;
                break;
            default:
                status = false;
                break;
        }
        return status;
    }
    public DataSet getInvoicePaymentReceived(string sCustID, string sJobTypeID, string sStartDate, string sEndDate)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            sSql = "[SPGET_PAYMENT_RECEIVED]";
            if (sCustID == "" || sCustID == "0")
                param[0] = oSql.NewParameter("@custid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            else
                param[0] = oSql.NewParameter("@custid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustID);
            if (sJobTypeID == "" || sJobTypeID == "0")
                param[1] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            else
                param[1] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[2] = oSql.NewParameter("@sdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStartDate);
            param[3] = oSql.NewParameter("@edate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEndDate);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getInvoiceOutstanding(string sCustID, string sJobTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[SPGET_OUTSTANDING_PAYMENTS]";
            if (sCustID == "" || sCustID == "0")
                param[0] = oSql.NewParameter("@custid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            else
                param[0] = oSql.NewParameter("@custid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustID);
            if (sJobTypeID == "" || sJobTypeID == "0")
                param[1] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            else
                param[1] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }

    public DataSet getCustomerPaymentOnAccount(string sCustID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetPaymentOnAccount]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustID);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }

    //-- Insert, Update, Delete

    public bool AddUpdatePaymentOnAccount(System.Collections.ArrayList list, string sModifiedBy)
    {
        int i = 0;
        object[] oQrys = new object[list.Count];
        bool status = false; string sSql = "";
        try
        {
            foreach (System.Web.UI.WebControls.ListItem item in list){
                if (item.Text == "insert")
                    sSql = "insert into credit_on_account(creditid,custno,credited_value,credited_date) select (select MAX(creditid)+1 from credit_on_account)," + item.Value.Split('|')[1] + "," + item.Value.Split('|')[2] + ",'" + item.Value.Split('|')[3] + "'";
                else
                    sSql = "update credit_on_account set custno = " + item.Value.Split('|')[1] + ",credited_value=" + item.Value.Split('|')[2] + ",credited_date='" + item.Value.Split('|')[3] + "' where creditid=" + item.Value.Split('|')[0];
                oQrys[i] = sSql; i++;
            }
            if (oQrys.Length > 0 && this.oSql.Execute_Sql(oQrys))
                status = true;
        }
        catch (Exception ex){
            throw new Exception(ex.Message);
        }
        return status;
    }

    public bool DeletePaymentOnAccount(string sCreditID, string sModifiedBy)
    {
        object[] oQrys = new object[1];
        oQrys[0] = "update credit_on_account set obsolete = getdate(), PAYMENT_CONFIRM_BY='" + sModifiedBy + "' where creditid=" + sCreditID;
        return this.oSql.Execute_Sql(oQrys);
    }    

    public bool ApprovePayments(System.Collections.ArrayList acceptlist, string sModifiedBy)
    {
        int i = 0;
        object[] oQrys = new object[acceptlist.Count];
        bool status = false;
        string sSql = "", sDateNow = DateTime.Now.ToString("yyyy-MM-dd");
        foreach (System.Web.UI.WebControls.ListItem item in acceptlist)
        {
            sSql = "";
            switch (item.Text)
            {
                case "credit":
                    sSql = "UPDATE CREDIT_ON_ACCOUNT SET PAYMENT_DATE='" + sDateNow + "',PAYMENT_CONFIRMED_BY=" + sModifiedBy + " WHERE CREDITID=" + item.Value + ";";
                    break;
                default:
                    sSql = "UPDATE JOB_PARENT SET PAYMENT_DATE='" + sDateNow + "',PAYMENT_CONFIRMED_BY=" + sModifiedBy + " WHERE INVOICE_NO=" + item.Value + ";";
                    break;                
            }
            oQrys[i] = sSql; i++;
        }
        if (oQrys.Length > 0 && this.oSql.Execute_Sql(oQrys))
            status = true;

        return status;
    }
    
    public bool CancelPayments(System.Collections.ArrayList cancellist, string sModifiedBy)
    {
        int i = 0;
        object[] oQrys = new object[cancellist.Count];
        bool status = false; string sSql = "";
        foreach (System.Web.UI.WebControls.ListItem item in cancellist)
        {
            sSql = "";
            switch (item.Text)
            {

                case "credit":
                    sSql = "UPDATE CREDIT_ON_ACCOUNT SET PAYMENT_DATE=NULL,PAYMENT_CONFIRMED_BY=" + sModifiedBy + " WHERE CREDITID=" + item.Value + ";";
                    break;
                default:
                    sSql = "UPDATE JOB_PARENT SET PAYMENT_DATE=NULL,PAYMENT_CONFIRMED_BY=" + sModifiedBy + " WHERE INVOICE_NO=" + item.Value + ";";
                    break;
            }
            oQrys[i] = sSql; i++;
        }
        if (oQrys.Length > 0 && this.oSql.Execute_Sql(oQrys))
            status = true;

        return status;
    }

    public string InsertPublisherInfo(string[] aPubInfo)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[11];
        try
        {
            sSql = "[spInsertSalesLead]";
            param[0] = oSql.NewParameter("@slcompany", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[0]);
            param[1] = oSql.NewParameter("@leadstatustype", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aPubInfo[1]);
            param[2] = oSql.NewParameter("@slfulladdress", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aPubInfo[2]);
            param[3] = oSql.NewParameter("@slcity", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[3]);
            param[4] = oSql.NewParameter("@slstate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[4]);
            param[5] = oSql.NewParameter("@slpocode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[5]);
            param[6] = oSql.NewParameter("@slcountry", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[6]);
            param[7] = oSql.NewParameter("@sldescription", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aPubInfo[7]);
            param[8] = oSql.NewParameter("@sales_job_type_IDs", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[8]);
            param[9] = oSql.NewParameter("@sales_category_type_IDs", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[9]);
            param[10] = oSql.NewParameter("@createdby", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[10]);            

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
        SqlParameter[] param = new SqlParameter[10];
        try
        {
            sSql = "[spUpdateSalesLead]";
            param[0] = oSql.NewParameter("@salesleadno", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aPubInfo[0]);
            param[1] = oSql.NewParameter("@leadstatustype", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aPubInfo[1]);
            param[2] = oSql.NewParameter("@slfulladdress", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aPubInfo[2]);
            param[3] = oSql.NewParameter("@slcity", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[3]);
            param[4] = oSql.NewParameter("@slstate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[4]);
            param[5] = oSql.NewParameter("@slpocode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[5]);
            param[6] = oSql.NewParameter("@slcountry", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[6]);
            param[7] = oSql.NewParameter("@sldescription", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aPubInfo[7]);
            param[8] = oSql.NewParameter("@sales_job_type_IDs", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[8]);
            param[9] = oSql.NewParameter("@sales_category_type_IDs", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPubInfo[9]);            

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
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            sSql = "[spInsertSalesContact]";
            param[0] = oSql.NewParameter("@ci_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[0]);
            param[1] = oSql.NewParameter("@ci_title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[1]);
            param[2] = oSql.NewParameter("@ci_phone", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[2]);
            param[3] = oSql.NewParameter("@ci_fax", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[3]);
            param[4] = oSql.NewParameter("@ci_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[4]);
            param[5] = oSql.NewParameter("@ci_web", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[5]);
            param[6] = oSql.NewParameter("@salesleadno", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContact[6]);
            param[7] = oSql.NewParameter("@createdby", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[7]);

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
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            sSql = "[spUpdateSalesContact]";
            param[0] = oSql.NewParameter("@ci_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[0]);
            param[1] = oSql.NewParameter("@ci_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[1]);
            param[2] = oSql.NewParameter("@ci_title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[2]);
            param[3] = oSql.NewParameter("@ci_phone", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[3]);
            param[4] = oSql.NewParameter("@ci_fax", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[4]);
            param[5] = oSql.NewParameter("@ci_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[5]);
            param[6] = oSql.NewParameter("@ci_web", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContact[6]);
            param[7] = oSql.NewParameter("@salesleadno", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContact[7]);

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

    public string InsertCommunication(string[] aCommn)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[11];
        try
        {
            sSql = "[spInsertCommuncation]";
            param[0] = oSql.NewParameter("@co_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCommn[0]);
            param[1] = oSql.NewParameter("@salesleadno", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCommn[1]);
            param[2] = oSql.NewParameter("@leadstatustype", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCommn[2]);
            param[3] = oSql.NewParameter("@ci_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCommn[3]);
            param[4] = oSql.NewParameter("@co_type", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCommn[4]);
            param[5] = oSql.NewParameter("@ftype", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCommn[5]);
            param[6] = oSql.NewParameter("@co_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCommn[6]);
            param[7] = oSql.NewParameter("@fdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCommn[7]);
            param[8] = oSql.NewParameter("@reply", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aCommn[8]);
            param[9] = oSql.NewParameter("@communication", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aCommn[9]);
            param[10] = oSql.NewParameter("@createdby", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCommn[10]);

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
    public string UpdateCommunication(string[] aCommn)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            sSql = "[spUpdateCommuncation]";
            param[0] = oSql.NewParameter("@co_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCommn[0]);
            param[1] = oSql.NewParameter("@co_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCommn[1]);
            param[2] = oSql.NewParameter("@salesleadno", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCommn[2]);
            param[3] = oSql.NewParameter("@leadstatustype", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCommn[3]);            
            param[4] = oSql.NewParameter("@co_type", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCommn[4]);
            param[5] = oSql.NewParameter("@ftype", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCommn[5]);
            param[6] = oSql.NewParameter("@fdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCommn[6]);
            param[7] = oSql.NewParameter("@reply", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aCommn[7]);
            param[8] = oSql.NewParameter("@communication", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aCommn[8]);            

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
    public string InsertUpdateEvent(string sEventDate, string sEventDescription)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spInsertUpdateSalesEvent]";
            param[0] = oSql.NewParameter("@e_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEventDate);
            param[1] = oSql.NewParameter("@e_desc", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, sEventDescription);

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
}

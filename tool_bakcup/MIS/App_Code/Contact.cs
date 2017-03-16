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

/* Creatin Date: February 03, 2009
 * Created by:  Royson 
 */
public class Contact
{
    private datasourceSQL oSql = new datasourceSQL();
    private Common oCom = new Common();
    CustomerBase oCust = new CustomerBase();
    private string sSql = "";
	public Contact(){}
    public DataSet getAllCustomers() { return oCust.getAllCustomers(); }
    public DataSet getCustomerByID(string sCustID) { return oCust.getCustomerByID(sCustID); }
    public DataSet getContactTypes() { return oCom.getContactTypes(); }
    public DataSet getStageTypes(string sJobTypeID) { return oCom.getStageTypes(sJobTypeID); }
    public DataSet getContactsByName(string sFirstName,string sCustomerID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetContactList]";
            param[0] = oSql.NewParameter("@first_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sFirstName);
            param[1] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }

    public DataSet getContactsByID(string sContactID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetContact]";
            param[0] = oSql.NewParameter("@contact_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sContactID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }    
    public DataSet getJobContacts(string sCustID, string sParentJobTypeID, string sChildJobID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGetJobContacts]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustID);
            param[1] = oSql.NewParameter("@parent_job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobTypeID);
            param[2] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sChildJobID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getJobsByParentJobID(string sParentJobID, string sChildJobTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetJobs]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[1] = oSql.NewParameter("@child_job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sChildJobTypeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getJobByJobID(string sJobID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetJobDetails]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public string InsertContact(string[] aContactInfo)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[16];
        try
        {
            sSql = "[spInsertContact]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[0]);
            param[1] = oSql.NewParameter("@title", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aContactInfo[1]);
            param[2] = oSql.NewParameter("@first_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[2]);
            param[3] = oSql.NewParameter("@last_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[3]);
            param[4] = oSql.NewParameter("@sur_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[4]);
            param[5] = oSql.NewParameter("@display_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[5]);
            param[6] = oSql.NewParameter("@address", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[6]);
            param[7] = oSql.NewParameter("@phone1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[7]);
            param[8] = oSql.NewParameter("@phone2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[8]);
            param[9] = oSql.NewParameter("@phone3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[9]);
            param[10] = oSql.NewParameter("@fax1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[10]);
            param[11] = oSql.NewParameter("@fax2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[11]);
            param[12] = oSql.NewParameter("@email1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[12]);
            param[13] = oSql.NewParameter("@email2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[13]);
            param[14] = oSql.NewParameter("@email3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[14]);
            param[15] = oSql.NewParameter("@description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[15]);

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

    public string UpdateContact(string[] aContactInfo)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[17];
        try
        {
            sSql = "[spUpdateContact]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[0]);
            param[1] = oSql.NewParameter("@title", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aContactInfo[1]);
            param[2] = oSql.NewParameter("@first_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[2]);
            param[3] = oSql.NewParameter("@last_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[3]);
            param[4] = oSql.NewParameter("@sur_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[4]);
            param[5] = oSql.NewParameter("@display_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[5]);
            param[6] = oSql.NewParameter("@address", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[6]);
            param[7] = oSql.NewParameter("@phone1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[7]);
            param[8] = oSql.NewParameter("@phone2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[8]);
            param[9] = oSql.NewParameter("@phone3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[9]);
            param[10] = oSql.NewParameter("@fax1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[10]);
            param[11] = oSql.NewParameter("@fax2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[11]);
            param[12] = oSql.NewParameter("@email1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[12]);
            param[13] = oSql.NewParameter("@email2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[13]);
            param[14] = oSql.NewParameter("@email3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[14]);
            param[15] = oSql.NewParameter("@description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[15]);
            param[16] = oSql.NewParameter("@contact_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[16]);

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
    public string InsertJobContact(string[] aJobContactInfo)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            sSql = "[spInsertJobContact]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aJobContactInfo[0]);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aJobContactInfo[1]);
            param[2] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobContactInfo[2]);
            param[3] = oSql.NewParameter("@contact_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobContactInfo[3]);
            param[4] = oSql.NewParameter("@contact_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobContactInfo[4]);
            param[5] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobContactInfo[5]);            

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
    public string UpdateJobContact(string[] aJobContactInfo)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            sSql = "[spUpdateJobContact]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aJobContactInfo[0]);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aJobContactInfo[1]);
            param[2] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobContactInfo[2]);
            param[3] = oSql.NewParameter("@contact_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobContactInfo[3]);
            param[4] = oSql.NewParameter("@contact_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobContactInfo[4]);
            param[5] = oSql.NewParameter("@job_contact_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobContactInfo[5]);

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
    public string DeleteJobContact(string sJobContactID)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spDeleteJobContacts]";
            param[0] = oSql.NewParameter("@job_contact_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sJobContactID);
            
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

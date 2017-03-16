using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Xml;
using System.Data.SqlClient;
using System.Text;
using System.Web.SessionState;
using System.Collections;

/// <summary>
/// Summary description for Launch
/// </summary>
public class Launch
{
    LaunchSQL lSql = new LaunchSQL();
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;
    private string sSql = "";
    public Launch()
    {

    }
    private void opencon()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrSQLLaunch"].ConnectionString;
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
    private void addParamsINT(SqlCommand oCmmd, string sName, int sValue, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }
    public DataSet GetJobDetails(int Customerid,int month,int year)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spGetJOBlist";
            oCmd.Parameters.Clear();
           
            addParamsINT(oCmd, "@custid", Customerid, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@month", month, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@year", year, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "JOB");
            oCmd = null;
            return ds;
        }
        catch (Exception oEx)
        {
            return null;
        }
        finally
        {
            closecon();
        }
    }
    public DataSet overview(int Customerid, int LocationID, int month, int year)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "overview";
            oCmd.Parameters.Clear();

            addParamsINT(oCmd, "@custid", Customerid, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@locationid", LocationID, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@month", month, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@year", year, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "JOB");
            oCmd = null;
            return ds;
        }
        catch (Exception oEx)
        {
            return null;
        }
        finally
        {
            closecon();
        }
    }
    public DataSet OverAllReport(int Customerid, int month, int year)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "OverAll_Report";
            oCmd.Parameters.Clear();

            addParamsINT(oCmd, "@custid", Customerid, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@month", month, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@year", year, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "JOB");
            oCmd = null;
            return ds;
        }
        catch (Exception oEx)
        {
            return null;
        }
        finally
        {
            closecon();
        }
    }
    public DataSet LaunchWIP(string Status,string month,string year)
    {
       
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[LaunchWIP]";
            param[0] = lSql.NewParameter("@Status", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Status);
            param[1] = lSql.NewParameter("@month", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, month);
            param[2] = lSql.NewParameter("@year", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, year);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getAllSoftware()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            sSql = "spGetSoftware";
            ds = lSql.FillDataSet_SP(sSql, null);
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
    public DataSet getAllLocation()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            sSql = "spGetLocation";
            ds = lSql.FillDataSet_SP(sSql, null);
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
    public DataSet getContactsByName(string sFirstName, string sCustomerID, string sLocationID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGetContactList_Launch]";
            param[0] = lSql.NewParameter("@first_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sFirstName);
            param[1] = lSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            param[2] = lSql.NewParameter("@location_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sLocationID);
            ds = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getAllCustomers()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            sSql = "spGetCustomers";
            ds = lSql.FillDataSet_SP(sSql, null);
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
    public DataSet getAllLocations()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            sSql = "spGetLocation";
            ds = lSql.FillDataSet_SP(sSql, null);
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
    public DataSet getCustomerByID(string sLocationID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLocation";
            param[0] = lSql.NewParameter("@Location_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sLocationID);
            ds = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }

    public DataSet getJobDetailsByID(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetJobDetailsbyID";
            param[0] = lSql.NewParameter("@projectid", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getTimeDetails(string shrs, string sMin, string sID, string sZone)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            sSql = "test";
            param[0] = lSql.NewParameter("@hrs", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, shrs);
            param[1] = lSql.NewParameter("@min", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sMin);
            param[2] = lSql.NewParameter("@location_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            param[3] = lSql.NewParameter("@time_zone", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sZone);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public void UpdateValues(string projectname, string task, string time)
    {
        lSql.ExcSProcedure("update lp_task_group set time_taken='" + time + "'  where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "') and taskname='" + task + "'", null, CommandType.Text);
    }
    public void UpdateHrsValues(string projectname, string task, string hrs)
    {
        lSql.ExcSProcedure("update lp_task_group set h_rate='" + hrs + "'  where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "') and taskname='" + task + "'", null, CommandType.Text);
    }
    public void UpdatePageValues(string projectname, string task, string Page)
    {
        lSql.ExcSProcedure("update lp_task_group set p_rate='" + Page + "'  where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "') and taskname='" + task + "'", null, CommandType.Text);
    }
    public DataSet getValues(string sProjectName)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetQuoteValues";
            param[0] = lSql.NewParameter("@Projectname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectName);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getQuoteValues(string sProjectId)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetQuoteValues";
            param[0] = lSql.NewParameter("@Projectname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectId);
            ds = lSql.FillDataSet_SP(sSql, param);
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
 
    public DataSet getQuoteValue(string sJobId)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "sp_insert_Quotevalue";
            param[0] = lSql.NewParameter("@projectname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobId);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet UpdateFonts(string sJobId)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "SP_FontDetails";
            param[0] = lSql.NewParameter("@projectname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobId);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getFonts(string Fonts)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "test234";
            param[0] = lSql.NewParameter("@Font", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Fonts);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    public DataSet getTaskName(string Task)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "Task";
            param[0] = lSql.NewParameter("@Font", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Task);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    public DataSet UsedFonts(string Projectname,string Fonts)
    {
        sSql = "";
        DataSet dsuf = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "sp_Font_group";
            param[0] = lSql.NewParameter("@Projectname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Projectname);
            param[1] = lSql.NewParameter("@Font", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Fonts);
            dsuf = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return dsuf;
    }
    public DataSet UpdateSoft(string Projectname)
    {
        sSql = "";
        DataSet dsuf = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "SP_GetSoftDetails";
            param[0] = lSql.NewParameter("@Projectname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Projectname);
            dsuf = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return dsuf;
    }
    public DataSet UpdateDelivery(string Projectname)
    {
        sSql = "";
        DataSet dsuf = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "SP_GetDeliveryDetails";
            param[0] = lSql.NewParameter("@Projectname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Projectname);
            dsuf = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return dsuf;
    }
    public DataSet Getusedlangname(string name,string lang)
    {
        return lSql.ExcProcedure("select * from LP_lang_GROUP where lang_name='"+lang+"' and Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + name + "') ", null, CommandType.Text);
    }
    public DataSet GetusedlangTaskWise(string name, string lang,string task,string soft)
    {
        return lSql.ExcProcedure("select * from LP_lang_GROUP where software_id='"+soft+"' and taskname='"+task+"' and lang_name='" + lang + "' and Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + name + "') ", null, CommandType.Text);
    }
    //public DataSet GetSoftname(string name)
    //{
    //    return lSql.ExcProcedure("select * from LP_Soft_GROUP where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + name + "') ", null, CommandType.Text);
    //}
    public DataSet GetSoftname(string Projectname)
    {
        sSql = "";
        DataSet dsuf = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGet_SoftGroups";
            param[0] = lSql.NewParameter("@proname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Projectname);
            dsuf = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return dsuf;
    }
    public DataSet GetSoftwareVer()
    {
        return lSql.ExcProcedure("select * from Software_version ", null, CommandType.Text);
    }
    public DataSet empname(string name)
    {
        return lSql.ExcProcedure("select fname+' '+surname as empname from dbo.LP_LAUNCH_INFO l, dp_mis_live.dbo.employee e where l.createdby_job=e.employee_id and l.projectname='"+ name +"'", null, CommandType.Text);
    }
    public void insertQueries(string projectname, string queries,string response)
    {
        lSql.ExcSProcedure("insert into LP_LAUNCH_QUERIES (Pro_id,queries,response) select (select Pro_id from LP_LAUNCH_INFO where Projectname='" + projectname + "'),'" + queries + "','" + response + "'", null, CommandType.Text);
    }
    public void insertmissfonts(string fonts)
    {
        lSql.ExcSProcedure("insert into LP_MISSING_FONTS (fonts) values ('" + fonts+ "')", null, CommandType.Text);
    }
    public void insertusagefonts(string projectname,string fonts)
    {
        lSql.ExcSProcedure("insert into LP_USAGEFONTS_GROUP (Pro_id,fonts) select (select Pro_id from LP_LAUNCH_INFO where Projectname='" + projectname + "'),'" + fonts + "'", null, CommandType.Text);
    }
    public void deleteusagefonts(string projectname, string fonts)
    {
        lSql.ExcSProcedure("delete from LP_USAGEFONTS_GROUP where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "') and fonts='" + fonts + "'", null, CommandType.Text);
    }
    public void deleteusagelang(string projectname, string lang)
    {
        lSql.ExcSProcedure("delete from LP_lang_GROUP where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "') and lang_name='" + lang + "'", null, CommandType.Text);
    }
    public void deletelang(string projectname)
    {
        lSql.ExcSProcedure("delete from LP_lang_GROUP where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "')", null, CommandType.Text);
    }
    public DataSet Chkmissfonts(string fonts)
    {
        return lSql.ExcProcedure("select * from  LP_MISSING_FONTS where fonts = ('" + fonts + "')", null, CommandType.Text);
    }
    public DataSet Chkusagefonts(string projectname,string fonts)
    {
        return lSql.ExcProcedure("select * from  LP_USAGEFONTS_GROUP where fonts = ('" + fonts + "') and Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "')", null, CommandType.Text);
    }
    public DataSet Chkfonts(string projectname, string fonts)
    {
        return lSql.ExcProcedure("select * from  LP_FONTS_GROUP where fonts = ('" + fonts + "') and Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "')", null, CommandType.Text);
    }
    public void insertfonts(string projectname, string fonts)
    {
        lSql.ExcSProcedure("insert into LP_FONTS_GROUP (Pro_id,fonts) select (select Pro_id from LP_LAUNCH_INFO where Projectname='" + projectname + "'),'" + fonts + "'", null, CommandType.Text);
    }
    public void deletefonts(string projectname, string fonts)
    {
        lSql.ExcSProcedure("delete from LP_FONTS_GROUP where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "') and fonts='" + fonts + "'", null, CommandType.Text);
    }
    public DataSet Chkmfonts(string projectname, string fonts)
    {
        return lSql.ExcProcedure("select * from  LP_MISSFONTS_GROUP where fonts = ('" + fonts + "') and Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "')", null, CommandType.Text);
    }
    //public DataSet Getsofttask(string projectname, string task)
    //{
    //    return lSql.ExcProcedure("select * from  LP_Soft_GROUP where Taskname = ('" + task + "') and Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "')", null, CommandType.Text);
    //}
    public DataSet Getsofttask(string Projectname, string task)
    {
        sSql = "";
        DataSet dsuf = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "spGet_Softtask";
            param[0] = lSql.NewParameter("@proname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Projectname);
            param[1] = lSql.NewParameter("@task", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, task);
            dsuf = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return dsuf;
    }
    public void insertmfonts(string projectname, string fonts)
    {
        lSql.ExcSProcedure("insert into LP_MISSFONTS_GROUP (pro_id,fonts) select (select Pro_id from LP_LAUNCH_INFO where Projectname='" + projectname + "'),'" + fonts + "'", null, CommandType.Text);
    }
    public void deletemfonts(string projectname, string fonts)
    {
        lSql.ExcSProcedure("delete from LP_MISSFONTS_GROUP where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "') and fonts='" + fonts + "'", null, CommandType.Text);
    }
    public void deletetask(string projectname)
    {
        lSql.ExcSProcedure("delete from LP_Soft_GROUP where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "')", null, CommandType.Text);
    }
    public void deleteQuote(string projectname)
    {
        lSql.ExcSProcedure("delete from LP_Launch_Quote where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "') and status=0", null, CommandType.Text);
    }
    public void UpdateQuoteStatus(string projectname,int status)
    {
        lSql.ExcSProcedure("update LP_Launch_Quote set status='"+status+"' where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "')", null, CommandType.Text);
    }
    public void UpdateQuoteStatus1(string projectname, string task, int soft, int status)
    {
        lSql.ExcSProcedure("update LP_Launch_Quote set status='" + status + "' where taskname='" + task + "' and software_id='" + soft + "' and Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "')", null, CommandType.Text);
    }
    public void insertQuote(string projectname, string task,int soft)
    {
        lSql.ExcSProcedure("insert into LP_Launch_Quote (pro_id,taskname,software_id) select (select Pro_id from LP_LAUNCH_INFO where Projectname='" + projectname + "'),'" + task + "','" + soft + "'", null, CommandType.Text);
    }
    public void insertlocation(string name)
    {
        lSql.ExcSProcedure("insert into location_master (location_name) values ('" + name + "'))", null, CommandType.Text);
    }
    public void inserttimezone(int id, string zone, string hrs, string min)
    {
        lSql.ExcSProcedure("insert into LP_TIMEZONE_MASTER (location_id,time_zone,time,min) values ('" + id + "','" + zone + "',cast('" + hrs + "' as numeric(20,2)),cast('" + min + "' as numeric(20,2)))", null, CommandType.Text);
    }
    public void insertCustloc(int cust,int loc)
    {
        lSql.ExcSProcedure("insert into lp_customers_location (location_id,Cust_id) values ('" + loc + "','" + cust + "')", null, CommandType.Text);
    }
    public void updatelocation(string name,int id)
    {
        lSql.ExcSProcedure("update location_master set location_name='" + name + "' where location_id ='"+ id +"' ", null, CommandType.Text);
    }
    public DataSet GetLaunchQuote(string projectname, string task, int soft)
    {
        return lSql.ExcProcedure("select * from LP_Launch_Quote where taskname='"+task+"' and software_id='"+soft+"' and Pro_id=(select Pro_id from LP_LAUNCH_INFO where  projectname='" + projectname + "')", null, CommandType.Text);
    }
    public DataSet GetQueries(string projectname)
    {
        return lSql.ExcProcedure("select * from LP_LAUNCH_QUERIES where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "')", null, CommandType.Text);
    }
    public DataSet chkCustLoc(int cust,int loc)
    {
        return lSql.ExcProcedure("select * from lp_customers_location where location_id='" + loc + "' and cust_id='"+ cust +"' ", null, CommandType.Text);
    }
    public DataSet GetSoftwareVers(int id)
    {
        return lSql.ExcProcedure("select * from Software_version where software_id='" + id + "' ", null, CommandType.Text);
    }
    public DataSet GetSoftVers(string id)
    {
        return lSql.ExcProcedure("select * from Software_version where software_id in (" + id + ") ", null, CommandType.Text);
    }
    public DataSet GetTimeZone(int id)
    {
        return lSql.ExcProcedure("select * from LP_TIMEZONE_MASTER where Location_id='" + id + "' ", null, CommandType.Text);
    }
    public DataSet ChkLocation(string name)
    {
        return lSql.ExcProcedure("select * from Location_Master where Location_name='" + name + "' ", null, CommandType.Text);
    }
    public DataSet SearchLocation(int name)
    {
        return lSql.ExcProcedure("select * from Location_Master where Location_id='" + name + "' ", null, CommandType.Text);
    }
    public DataSet GetLocation()
    {
        return lSql.ExcProcedure("select * from Location_Master  ", null, CommandType.Text);
    }
    public DataSet GetLocationCust(int id)
    {
        return lSql.ExcProcedure("select * from lp_customers_location l,location_master m where m.location_id=l.location_id and l.cust_id='" + id + "' ", null, CommandType.Text);
    }
    public DataSet GetProid(string name)
    {
        return lSql.ExcProcedure("select * from lp_Launch_Info where  projectname='" + name + "' ", null, CommandType.Text);
    }
    public DataSet GetDeliveryStatus(int id)
    {
        return lSql.ExcProcedure("select * from lp_Launch_Info where  pro_id='" + id + "' ", null, CommandType.Text);
    }
    //public void insertStageWIP(int Pro_id, string task, string Status)
    //{
    //    lSql.ExcSProcedure("insert into LP_LaunchWIP (pro_id,Stage,Status) values ('" + Pro_id + "','" + task + "','" + Status + "')", null, CommandType.Text);
    //}
    public void insertStageWIP(string Pro_id, string task, string Status,string DelDate,string DelTime)
    {
        lSql.ExcSProcedure("spInsertLaunchWIP", new string[,] { { "@Pro_id", Pro_id }, { "@task", task }, { "@Status", Status }, { "@DelDate", DelDate }, { "@DelTime", DelTime } }, CommandType.StoredProcedure);
    }
    public DataSet GetStageWIP(int id)
    {
        sSql = "";
        DataSet dsuf = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "LaunchWIP1";
            param[0] = lSql.NewParameter("@Pro_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, id);
            dsuf = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return dsuf;
    }
    public DataSet GetTimeZones()
    {
        return lSql.ExcProcedure("select * from Location_Master ", null, CommandType.Text);
    }

    //public DataSet GetComplexReason(int id, string complex)
    //{
    //    return lSql.ExcProcedure("select  r.* from dbo.LP_LAUNCH_INFO l, dbo.LP_COMPLEXITY_REASON r,dbo.LP_TASK_GROUP t, dbo.LP_FORMAT_GROUP f where r.Category=f.format_id and t.pro_id=l.pro_id and f.pro_id=l.pro_id and r.taskname =t.taskname  and l.pro_id=" + id + " and r.obsolete is null and r.COMPLEX_LEVEL='" + complex + "' ", null, CommandType.Text);
    //}
    public DataSet GetComplexReason(int id, string complex)
    {
        sSql = "";
        DataSet dsuf = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "spGetReasons";
            param[0] = lSql.NewParameter("@Pro_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, id);
            param[1] = lSql.NewParameter("@Complex", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, complex);
            dsuf = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return dsuf;
    }
    public DataSet GetComplexReasons(int id)
    {
        return lSql.ExcProcedure("select  r.* from dbo.LP_LAUNCH_INFO l, dbo.LP_COMPLEXITY_REASON r,dbo.LP_TASK_GROUP t, dbo.LP_FORMAT_GROUP f where r.Category=f.format_id and t.pro_id=l.pro_id and f.pro_id=l.pro_id and r.taskname =t.taskname  and l.pro_id='" + id + "' and r.obsolete is null ", null, CommandType.Text);
    }
    public DataSet GetComplexReasonold( string complex)
    {
        return lSql.ExcProcedure("select  * from dbo.LP_COMPLEXITY_REASON where obsolete is not null and COMPLEX_LEVEL='" + complex + "' ", null, CommandType.Text);
    }
    public DataSet GetComplexReasonsold()
    {
        return lSql.ExcProcedure("select  * from dbo.LP_COMPLEXITY_REASON where obsolete is not null ", null, CommandType.Text);
    }
  
    public DataSet searchlangname()
    {
        return lSql.ExcProcedure("select * from LP_LANG_MASTER", null, CommandType.Text);
    }
    public DataSet GetTask()
    {
        return lSql.ExcProcedure("select * from TASK_MASTER", null, CommandType.Text);

    }
    public DataSet GetTaskStage(int pro_id)
    {
        return lSql.ExcProcedure("select g.taskname from TASK_MASTER t , LP_TASK_GROUP g where t.taskname=g.taskname and g.pro_id='" + pro_id + "'", null, CommandType.Text);

    }
    public DataSet GetTaskid(string name)
    {
        return lSql.ExcProcedure("select * from TASK_MASTER where taskname in ('"+name+"')", null, CommandType.Text);

    }
    public DataSet GetusedLang(string name)
    {
        return lSql.ExcProcedure("select pro_id,lang_name from lp_lang_group where pro_id =  (select pro_id from lp_launch_info where projectname ='" + name + "') group by pro_id,lang_name", null, CommandType.Text);

    }
    public DataSet GetFormat(string id)
    {
        return lSql.ExcProcedure("select * from FORMAT_MASTER f, task_master t where t.taskname=f.taskname and t.task_id in ( "+id+" )", null, CommandType.Text);

    }
    public DataSet GetFormats(string name)
    {
        return lSql.ExcProcedure("select * from FORMAT_MASTER where taskname in ('" + name + "')", null, CommandType.Text);
    }
    public DataSet GetSoft(string id)
    {
        return lSql.ExcProcedure("select * from SOFTWARE_VERSION where software_id in ('" + id + "')", null, CommandType.Text);
    }
    public DataSet GetMissFonts()
    {
        return lSql.ExcProcedure("select * from LP_MISSING_FONTS order by Fonts asc", null, CommandType.Text);
    }
    public DataSet GetUsageFonts(string name)
    {
        return lSql.ExcProcedure("select * from LP_USAGEFONTS_GROUP where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + name + "')", null, CommandType.Text);
    }
    public DataSet GetFonts(string name)
    {
        return lSql.ExcProcedure("select * from LP_FONTS_GROUP where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + name + "')", null, CommandType.Text);
    }
    public DataSet GetMFonts(string name)
    {
        return lSql.ExcProcedure("select * from LP_MISSFONTS_GROUP where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + name + "')", null, CommandType.Text);
    }
  
    public DataSet GetQuoteDetails(string sProcName, string[,] paramcollection, CommandType CmdType)
    {

        DataSet ds = new DataSet();
        ds = lSql.ExcProcedure(sProcName, paramcollection, CmdType);
        return ds;
    }
    public DataSet GetComReasonOthers(int id)
    {
        return lSql.ExcProcedure("select * from LP_COMPLEXITY_REASON where Reason_id ='"+id+"'", null, CommandType.Text);

    }
    public bool Update_ProjectModule(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {
            
            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al)
                {
                 
                    string t;
                    if(ht["TIME_TAKEN"].ToString()=="")
                        t="0";
                    else
                        t=ht["TIME_TAKEN"].ToString();
                    umodule = "update LP_TASK_GROUP set TIME_TAKEN='"
                    + t.ToString() + "',LANG_COUNT='" + ht["LANG_COUNT"].ToString() + "',PAGES_COUNT='"
                    + ht["PAGES_COUNT"].ToString() + "',TOTAL_PAGES='" + ht["TOTAL_PAGES"].ToString()
                    + "',HOUR_RATE='" + ht["HOUR_RATE"].ToString() + "',PAGE_RATE='" + ht["PAGE_RATE"].ToString() 
                    + "',PAGEYN='" + ht["PAGEYN"].ToString() + "',HOURYN='" + ht["HOURYN"].ToString()
                    + "',P_RATE='" + ht["P_RATE"].ToString() + "',H_RATE='" + ht["H_RATE"].ToString()
                    + "' where Pro_id=(select Pro_id from LP_LAUNCH_INFO where ProjectName='" + ht["ProjectName"].ToString() + "') and  TASKNAME='" + ht["TASKNAME"].ToString() + "'";
                    ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }                                                     
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;

    }
    public bool Update_LangFile(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {

            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al)
                {
                    umodule = "update LP_Lang_GROUP set FILE_COUNT='"
                    + ht["FILE_COUNT"].ToString() + "',PAGES_COUNT='"
                    + ht["PAGES_COUNT"].ToString() + "' where Software_id='" + ht["Software_ID"].ToString() + "' and  taskname='" + ht["Taskname"].ToString() + "' and Pro_id='" + ht["Pro_ID"].ToString() + "' and  lang_name='" + ht["LANG_NAME"].ToString() + "'";
                    ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;

    }
    public bool Insert_Software(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {

            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al)
                {
                    umodule = "insert into LP_Soft_GROUP(Pro_ID,TaskName,Software_id,Version_id) select (select Pro_id from LP_LAUNCH_INFO where ProjectName='" + ht["ProjectName"].ToString() + "'),'" + ht["TaskName"].ToString() + "'," + ht["Software_id"].ToString() + "," + ht["Version_id"].ToString() + "";
                     ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;

    }
    public bool Update_ImageFile(System.Collections.ArrayList al1)
    {
        string umodule = string.Empty;
        if (al1 != null && al1.Count > 0)
        {

            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al1)
                {
                    umodule = "update LP_Lang_GROUP set Editable='"
                    + ht["Editable"].ToString() + "',Scanned='"
                    + ht["Scanned"].ToString() + "',Non_Local_Image='"
                    + ht["Non_Local_Image"].ToString() + "',Images='"
                    + ht["Images"].ToString() + "' where Software_id='" + ht["Software_ID"].ToString() + "' and  taskname='" + ht["Taskname"].ToString() + "' and Pro_id='" + ht["Pro_ID"].ToString() + "' and  lang_NAME='" + ht["LANG_NAME"].ToString() + "'";
                    ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;

    }
    public bool Update_DeliveryStatus(System.Collections.ArrayList al1)
    {
        string umodule = string.Empty;
        if (al1 != null && al1.Count > 0)
        {

            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al1)
                {
                    umodule = "update LP_Launch_Info set Delivery_Status='"
                    + ht["Status"].ToString() + "' where Pro_id='" + ht["ID"].ToString() + "' ";
                    ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;

    }
    public string InsertContact(string[] aContactInfo)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[17];
        try
        {
            sSql = "[spInsertContactLaunch]";
            param[0] = lSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[0]);
            param[1] = lSql.NewParameter("@title", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aContactInfo[1]);
            param[2] = lSql.NewParameter("@first_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[2]);
            param[3] = lSql.NewParameter("@last_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[3]);
            param[4] = lSql.NewParameter("@sur_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[4]);
            param[5] = lSql.NewParameter("@display_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[5]);
            param[6] = lSql.NewParameter("@address", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[6]);
            param[7] = lSql.NewParameter("@phone1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[7]);
            param[8] = lSql.NewParameter("@phone2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[8]);
            param[9] = lSql.NewParameter("@phone3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[9]);
            param[10] = lSql.NewParameter("@fax1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[10]);
            param[11] = lSql.NewParameter("@fax2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[11]);
            param[12] = lSql.NewParameter("@email1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[12]);
            param[13] = lSql.NewParameter("@email2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[13]);
            param[14] = lSql.NewParameter("@email3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[14]);
            param[15] = lSql.NewParameter("@description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[15]);
            param[16] = lSql.NewParameter("@Location_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[16]);

            Status = lSql.Execute_SP(sSql, param,
               lSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
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
        SqlParameter[] param = new SqlParameter[18];
        try
        {
            sSql = "[spUpdateContactLaunch]";
            param[0] = lSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[0]);
            param[1] = lSql.NewParameter("@title", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aContactInfo[1]);
            param[2] = lSql.NewParameter("@first_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[2]);
            param[3] = lSql.NewParameter("@last_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[3]);
            param[4] = lSql.NewParameter("@sur_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[4]);
            param[5] = lSql.NewParameter("@display_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[5]);
            param[6] = lSql.NewParameter("@address", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[6]);
            param[7] = lSql.NewParameter("@phone1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[7]);
            param[8] = lSql.NewParameter("@phone2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[8]);
            param[9] = lSql.NewParameter("@phone3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[9]);
            param[10] = lSql.NewParameter("@fax1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[10]);
            param[11] = lSql.NewParameter("@fax2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[11]);
            param[12] = lSql.NewParameter("@email1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[12]);
            param[13] = lSql.NewParameter("@email2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[13]);
            param[14] = lSql.NewParameter("@email3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[14]);
            param[15] = lSql.NewParameter("@description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[15]);
            param[16] = lSql.NewParameter("@contact_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[16]);
            param[17] = lSql.NewParameter("@Location_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[17]);

            Status = lSql.Execute_SP(sSql, param,
               lSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
   public DataSet getContactsByID(string sContactID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetContactLaunch]";
            param[0] = lSql.NewParameter("@contact_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sContactID);
            ds = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public LaunchDSnew ProductivityDetails(string qry, string[,] param)
    {
        LaunchDSnew LaDS = new LaunchDSnew();
        SqlDataAdapter proDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            proDA = new SqlDataAdapter(qry, oConn);
            proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
                {
                    if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
                    else
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
                }
            }
            proDA.Fill(LaDS);
            return LaDS;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            proDA.Dispose();
            closecon();
        }

    }
    public LaunchQuery LaunchQueryDetails(string qry, string[,] param)
    {
        LaunchQuery LaQ = new LaunchQuery();
        SqlDataAdapter proDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            proDA = new SqlDataAdapter(qry, oConn);
            proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
                {
                    if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
                    else
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
                }
            }
            proDA.Fill(LaQ);
            return LaQ;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            proDA.Dispose();
            closecon();
        }

    }
    public LaunchQuote LaunchQuoteDetails(string qry, string[,] param)
    {
        LaunchQuote LaQu = new LaunchQuote();
        SqlDataAdapter proDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            proDA = new SqlDataAdapter(qry, oConn);
            proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
                {
                    if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
                    else
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
                }
            }
            proDA.Fill(LaQu);
            return LaQu;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            proDA.Dispose();
            closecon();
        }

    }
    public LaunchLang LaunchLangDetails(string qry, string[,] param)
    {
        LaunchLang LaLg = new LaunchLang();
        SqlDataAdapter LaDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            LaDA = new SqlDataAdapter(qry, oConn);
            LaDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
                {
                    if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
                        LaDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
                    else
                        LaDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
                }
            }
            LaDA.Fill(LaLg);
            return LaLg;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            LaDA.Dispose();
            closecon();
        }

    }
    public LaunchQuoteDesc LaunchQuoteDesc(string qry, string[,] param)
    {
        LaunchQuoteDesc LaDs = new LaunchQuoteDesc();
        SqlDataAdapter LaDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            LaDA = new SqlDataAdapter(qry, oConn);
            LaDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
                {
                    if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
                        LaDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
                    else
                        LaDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
                }
            }
            LaDA.Fill(LaDs);
            return LaDs;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            LaDA.Dispose();
            closecon();
        }

    }
    public DataSet getTaskGroup(string Task)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGet_taskGroups";
            param[0] = lSql.NewParameter("@proname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Task);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    public DataSet getFormatGroup(string format)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGet_formatGroups";
            param[0] = lSql.NewParameter("@proname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, format);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    public DataSet getInputGroup(string Input)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGet_InputGroups";
            param[0] = lSql.NewParameter("@proname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Input);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    public DataSet getLangGroup(string Lang)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGet_LangGroups";
            param[0] = lSql.NewParameter("@proname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Lang);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    public DataSet getComplexReason(int reason)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetComplexReason";
            param[0] = lSql.NewParameter("@pro_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, reason);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    public DataSet getMFontGroup(string mfont)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGet_missfontsGroups";
            param[0] = lSql.NewParameter("@proname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, mfont);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    public DataSet getUFontGroup(string ufont)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGet_usagefontsGroups";
            param[0] = lSql.NewParameter("@proname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, ufont);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    public DataSet getLFontGroup(string Lfont)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGet_fontsGroups";
            param[0] = lSql.NewParameter("@proname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Lfont);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    public DataSet getDeliveryGroup(string Delivery)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGet_deliverytypeGroups";
            param[0] = lSql.NewParameter("@proname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Delivery);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    public DataSet GetLangFile(string name)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGet_LangGroups";
            param[0] = lSql.NewParameter("@proname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, name);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
    //public DataSet GetLangFile(string name)
    //{
    //    return lSql.ExcProcedure("select * from LP_lang_group  where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + name + "') ", null, CommandType.Text);
    //}
    public DataSet UpdateLaunchQuote(string Projectname, int Lang_count)
    {
        sSql = "";
        DataSet dsuf = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "UpdateLaunchQuote";
            param[0] = lSql.NewParameter("@Projectname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Projectname);
            param[1] = lSql.NewParameter("@Lang_count", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, Lang_count);
            dsuf = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return dsuf;
    }
    public bool Update_LaunchQuote(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {

            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al)
                {

                    string t;
                    if (ht["TIME_TAKEN"].ToString() == "")
                        t = "0";
                    else
                        t = ht["TIME_TAKEN"].ToString();
                    umodule = "update lp_launch_quote set TIME_TAKEN='"
                    + t.ToString() + "',LANG_COUNT='" + ht["LANG_COUNT"].ToString() + "',PAGES_COUNT='"
                    + ht["PAGES_COUNT"].ToString() 
                    + "',HOUR_RATE='" + ht["HOUR_RATE"].ToString() + "',PAGE_RATE='" + ht["PAGE_RATE"].ToString()
                    + "',PAGEYN='" + ht["PAGEYN"].ToString() + "',HOURYN='" + ht["HOURYN"].ToString()
                    + "',P_RATE='" + ht["P_RATE"].ToString() + "',H_RATE='" + ht["H_RATE"].ToString()
                    + "' where Pro_id=(select Pro_id from LP_LAUNCH_INFO where ProjectName='" + ht["ProjectName"].ToString() + "') and  TASKNAME='" + ht["TASKNAME"].ToString() + "' and  Software_id=(select software_id from software_master where software_name= '" + ht["Software_id"].ToString() + "')";
                    ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;

    }
    public void UpdateTimeTaken(string projectname, string task, string time,string soft)
    {
        lSql.ExcSProcedure("update lp_Launch_Quote set time_taken='" + time + "'  where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "') and taskname='" + task + "' and software_id=(select software_id from software_master where software_name= '" + soft + "')", null, CommandType.Text);
    }
    public void UpdateHrs(string projectname, string task, string hrs, string soft)
    {
        lSql.ExcSProcedure("update lp_Launch_Quote set H_Rate='" + hrs + "'  where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "') and taskname='" + task + "' and software_id=(select software_id from software_master where software_name= '" + soft + "')", null, CommandType.Text);
    }
    public void UpdatePage(string projectname, string task, string Page, string soft)
    {
        lSql.ExcSProcedure("update lp_Launch_Quote set P_Rate='" + Page + "'  where Pro_id=(select Pro_id from LP_LAUNCH_INFO where projectname='" + projectname + "') and taskname='" + task + "' and software_id=(select software_id from software_master where software_name= '" + soft + "')", null, CommandType.Text);
    }
}


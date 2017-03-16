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

/* Creatin Date: Tuesday, February 03, 2009
 * Created by:  Royson 
 */
public class Common
{
    private datasourceSQL oSql = new datasourceSQL();
    private string sSql = "";
	public Common(){}

    /* Announcements */
    /*public DataSet getAnnouncementListByTeam(string sTeamID, string sEmployeeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            if (sTeamID == "") sTeamID = "0";
            else sTeamID = "0," + sTeamID;
            sSql = "[spGetAnnouncementList]";
            param[0] = oSql.NewParameter("@TeamID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sTeamID);
            param[1] = oSql.NewParameter("@EmployeeID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmployeeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }*/
    //by subbu
    public DataSet getAnnouncementListByTeam(string sTeamID, string sEmployeeID,string mode)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            if (sTeamID == "") sTeamID = "0";
            else sTeamID = "0," + sTeamID;
            sSql = "[spGetAnnouncementList_New]";
            param[0] = oSql.NewParameter("@TeamID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sTeamID);
            param[1] = oSql.NewParameter("@EmployeeID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmployeeID);
            param[2] = oSql.NewParameter("@Mode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, mode);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getAnnouncementByID(string sAnnouncementID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetAnnouncement]";
            param[0] = oSql.NewParameter("@AnnouncementID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sAnnouncementID);            
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getTeamList()
    {
        DataSet ds = new DataSet();
        sSql = "";        
        try
        {
            sSql = "[spGet_Teams]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getTeamListByOwner(string sOwnerID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGet_TeamMembers]";
            param[0] = oSql.NewParameter("@team_owner_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sOwnerID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getAnouncementTypes()
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "[spGetAnnouncementTypes]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public string InsertAnnouncement(string[] aAnnouncement)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            sSql = "[spInsertAnnouncement]";
            param[0] = oSql.NewParameter("@announcement_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aAnnouncement[0]);
            param[1] = oSql.NewParameter("@announcement_title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aAnnouncement[1]);
            param[2] = oSql.NewParameter("@announcement", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aAnnouncement[2]);
            param[3] = oSql.NewParameter("@announced_by", SqlDbType.Int, 1, ParameterDirection.Input, aAnnouncement[3]);
            param[4] = oSql.NewParameter("@announced_to", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aAnnouncement[4]);
            param[5] = oSql.NewParameter("@startdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aAnnouncement[5]);
            param[6] = oSql.NewParameter("@expirydate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aAnnouncement[6]);
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    
    public string DeleteAnnouncement(string sAnnouncementID)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spDeleteAnnouncement]";
            param[0] = oSql.NewParameter("@announcement_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sAnnouncementID);            
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string ResetEmployeePassword(string sEmployeeID)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spResetPassword]";
            param[0] = oSql.NewParameter("@employee_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sEmployeeID);
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    /* End Announcements */

    /* Begin Seasonal Events */

    public DataSet getBirthDayBuddies()
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "[spGetBirthDayBuddies]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }

    /* End Seasonal Events */

    /* Common methods used throught MIS */
    public DataSet getAllStagesByJournal(string sJournalID, string sJobTypeID, string sModeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGET_NEXT_STAGE];4";
            param[0] = oSql.NewParameter("@journalid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[1] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[2] = oSql.NewParameter("@modeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sModeID);
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
    public DataSet getStagesByJobType(int JobID,int JobTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGET_NEXT_JOB_STAGE]";
            param[0] = oSql.NewParameter("@JOB_ID", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, JobID);
            param[1] = oSql.NewParameter("@JOB_TYPE_ID", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, JobTypeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getStagesByCustomer(string CustomerID, string JobTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetDefaultJobStage]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, CustomerID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, JobTypeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getTypesettingPlatformList()
    {
        DataSet ds = new DataSet();
        sSql = "";
        //SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetTypesetPlatformList]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getServiceTypes()
    {
        DataSet ds = new DataSet();
        sSql = "";
        //SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetServiceTypeList]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getGraphicTypes()
    {
        DataSet ds = new DataSet();
        sSql = "";
        //SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetGraphicTypes]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getFigureTypes()
    {
        DataSet ds = new DataSet();
        sSql = "";
        //SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetFigureTypes]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getDocumentTypes(string sJobType)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetDocTypes]";
            //@Job_type
            param[0] = oSql.NewParameter("@Job_type", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobType);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getDocumentItemTypes(string sDocTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetDocItemTypes]";
            //@Job_type
            param[0] = oSql.NewParameter("@document_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDocTypeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getContactTypes()
    {
        DataSet ds = new DataSet();
        sSql = "";
        //SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetContactType]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }    
    public DataSet getStageTypes()
    {
        DataSet ds = new DataSet();
        sSql = "";
        //SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetJobStage]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getStageTypes(string sJobTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetJobStage]";
            param[0] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);

            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getCategoryTypes()
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetCategory]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }

    public DataSet getOnHoldTypes()
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetOnHoldType]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getNumberSystem()
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetNumberSystem]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getDepartment()
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetDepartmentList]";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getDepartmentTask(string sDepartmentID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetDepartmentTask]";
            param[0] = oSql.NewParameter("@department_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDepartmentID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getJobTypeTask(string sDepartmentID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetJobTypeTask]";
            param[0] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDepartmentID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getNonInvoicedJobs(string sJournalID, string sJobTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetNonInvoicedJobs]";
            param[0] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getNonInvoicedJobsByStage(string sJournalID, string sJobTypeID, string sStageID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetNonInvoicedJobs]";
            param[0] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getJobEventsHistory(string sCustomerID, string sJournalID, string sParentJobID, string sJobTypeID, string sJobStageID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            sSql = "[spGetJobEventsHistory]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            param[1] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[2] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[3] = oSql.NewParameter("@parent_job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[4] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobStageID);

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
    public DataSet getJobEventsHistory(string sCustomerID, string sJournalID, string sParentJobID, string sJobTypeID, string sJobStageID, string sModeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            sSql = "[spGetJobEventsHistory]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            param[1] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[2] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[3] = oSql.NewParameter("@parent_job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[4] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobStageID);
            param[5] = oSql.NewParameter("@modeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sModeID);

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
    public DataSet getJobEventsHistory_PR(string sCustomerID, string sJournalID, string sJobTypeID, string sJobStageID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            sSql = "[spGetJobEventsHistory_PR]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            param[1] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[3] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobStageID);

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
    public DataSet getJobEmailHistory(string sCustomerID, string sJournalID, string sJobStageID, string sStartDate, string sEndDate)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            sSql = "[spGetJobEmailHistory]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            param[1] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[2] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobStageID);
            param[3] = oSql.NewParameter("@start_sate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStartDate);
            param[4] = oSql.NewParameter("@end_sate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEndDate);

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
    public DataSet getJobEmailHistoryPreview(string sJobID, string sJobTypeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetJobEmailHistory_Prev]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);

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
    public DataSet getJobAssignableStages(string sJobID, string sJobTypeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetCustJobReassignStages]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);

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
    public DataSet getSalesJobGroups()
    {
        DataSet ds = new DataSet();
        sSql = "";
        //SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetSalesJobGroup]";            

            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    //JOB INVOICE FOR BOOKDEETAILS
    //public DataSet getJobInvoiceGroups()
    //{
    //    DataSet ds = new DataSet();
    //    sSql = "";
    //    try
    //    {
    //        sSql = "[spgetjobinvoicegroup]";
    //        ds = oSql.FillDataSet_SP(sSql, null);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //    return ds;
    //}



    public DataSet getJobDispSummary(string sCustomerID, string sJournalID, string sJobTypeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGetCustJobDispatched]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            param[1] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);

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
    public DataSet getJobEventsHistory(string sCustomerID, string sJournalID, string sParentJobID, string sJobTypeID, string sJobStageID, string sStatusID, string sModeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            sSql = "[spGetJobEventsHistory]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            param[1] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[2] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[3] = oSql.NewParameter("@parent_job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[4] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobStageID);
            param[5] = oSql.NewParameter("@status_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStatusID);
            param[6] = oSql.NewParameter("@modeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sModeID);

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
    public DataSet getCustStatusByStage(string sStageID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetCustStatus]";
            param[0] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStageID);

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
    public bool IsJobInvoiced(string sJobID, string sJobTypeID)
    {
        bool status = false;
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetJobInvoiceStatus]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            DataSet ds = oSql.FillDataSet_SP(sSql, param);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString().Trim()!="")
            {
                status = true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return status;
    }


    //----------- Insert/Update/Delete --------------------//
    public string InsertDepartmentTask(string sDepartmentID, string sTaskIDs)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spInsertDepartmentTask]";
            param[0] = oSql.NewParameter("@department_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sDepartmentID);
            param[1] = oSql.NewParameter("@task_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sTaskIDs);
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string UpdateDepartmentTask(string sDepartmentID, string sTaskIDs)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spUpdateDepartmentTask]";
            param[0] = oSql.NewParameter("@department_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sDepartmentID);
            param[1] = oSql.NewParameter("@task_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sTaskIDs);
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string UpdateJobTypeTask(string sjobtypeID, string sTaskIDs)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spUpdateJobTypeTask]";
            param[0] = oSql.NewParameter("@jobtype_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sjobtypeID);
            param[1] = oSql.NewParameter("@task_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sTaskIDs);
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public bool UpdateCustJobStage(string sJobID, string sJobTypeID, string sCurrJobStageID
        , string sNewJobStageID, string sRecDate, string sDueDate, string sHlfDueDate, string sEmployeeID)
    {
        bool Status = false;
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            sSql = "[spUpdateCustJobStage]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[2] = oSql.NewParameter("@curr_job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCurrJobStageID);
            param[3] = oSql.NewParameter("@new_job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sNewJobStageID);
            param[4] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sRecDate);
            param[5] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDueDate);
            param[6] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sHlfDueDate);
            param[7] = oSql.NewParameter("@employee_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmployeeID);
            Status = oSql.Execute_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    /* End Common methods*/
}

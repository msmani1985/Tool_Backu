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
using System.Net.Mail;
/// <summary>
/// Summary description for EmailConfig
/// </summary>
public class EmailConfig
{
    private datasourceSQL oSql = new datasourceSQL();
    private Common oCom = new Common();
    CustomerBase oCust = new CustomerBase();
    private string sSql = "";
    public EmailConfig() { }
    public DataSet getJobEvents(string sJobEventID, string sParentJobID, string sParentJobTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGetJobEvents]";
            param[0] = oSql.NewParameter("@job_event_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobEventID);
            param[1] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[2] = oSql.NewParameter("@parent_job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobTypeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getJobEventsSummary(string sJobEventID, string sParentJobID, string sParentJobTypeID,
        string sChildJobID, string sChildJobTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            sSql = "[spGetJobEvents]";
            param[0] = oSql.NewParameter("@job_event_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobEventID);
            param[1] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[2] = oSql.NewParameter("@parent_job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobTypeID);
            param[3] = oSql.NewParameter("@child_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sChildJobID);
            param[4] = oSql.NewParameter("@child_job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sChildJobTypeID);
            param[5] = oSql.NewParameter("@mode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "summary");
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getEmailLetter(string sEmailLetterId, string sParentJobTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetEmailLetter]";
            param[0] = oSql.NewParameter("@email_letter_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmailLetterId);
            param[1] = oSql.NewParameter("@parent_job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobTypeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getEmailGroup(string sEmailGroupID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetEmailGroup]";
            param[0] = oSql.NewParameter("@email_group_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmailGroupID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getEmailMessages(string sParentJobID, string sParentJobTypeID, string sJobEventTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGetEmailMessages]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[1] = oSql.NewParameter("@parent_job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobTypeID);
            param[2] = oSql.NewParameter("@job_event_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobEventTypeID);            
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getEmailMessages_Journal(string sParentJobID, string sParentJobTypeID, string sJobEventTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGetEmailMessages_J]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobTypeID);
            param[2] = oSql.NewParameter("@job_event_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobEventTypeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getEmailSentLog(string sEmailSentID, string sParentJobID, string sParentJobTypeID, 
        string sFromDate, string sToDate)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            sSql = "[spGetEmailSent]";
            param[0] = oSql.NewParameter("@email_sent_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmailSentID);
            param[1] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[2] = oSql.NewParameter("@parent_job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobTypeID);
            param[3] = oSql.NewParameter("@from_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sFromDate);
            param[4] = oSql.NewParameter("@to_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sToDate);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }

    /*
     *Insert, Update, Delete* 
     */
     
    public string InsertJobEvent(string[] aJobEvent, System.Collections.ArrayList lstEmailRecip)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[11];
        try
        {
            sSql = "[spInsertJobEvents]";
            param[0] = oSql.NewParameter("@job_event_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[0]);
            param[1] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[1]);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[2]);
            if (aJobEvent[3] == "true") param[3] = oSql.NewParameter("@iscontributed", SqlDbType.Bit, int.MaxValue, ParameterDirection.Input, true);
            else param[3] = oSql.NewParameter("@iscontributed", SqlDbType.Bit, int.MaxValue, ParameterDirection.Input, false);
            param[4] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[4]);
            param[5] = oSql.NewParameter("@addnl_keywords", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[5]);
            param[6] = oSql.NewParameter("@follow_up_job_event_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[6]);
            param[7] = oSql.NewParameter("@created_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aJobEvent[7]);
            param[8] = oSql.NewParameter("@from_contact_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[8]);
            param[9] = oSql.NewParameter("@email_letter_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[9]);
            param[10] = oSql.NewParameter("@hasattachment", SqlDbType.Bit, int.MaxValue, ParameterDirection.Input, Convert.ToBoolean(aJobEvent[10]));

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
            if (Status != "") this.InsertEmailReceipients(Status, lstEmailRecip);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    private string InsertEmailReceipients(string sJobEventId, System.Collections.ArrayList lstEmailReceipients)
    {
        sSql = "";
        int i = 0;
        object[] oRecip = new object[lstEmailReceipients.Count];
        try
        {
            foreach (string[] a in lstEmailReceipients)
            {
                if (a[1].Split('|')[0] == "CT")
                {
                    sSql = "insert into job_event_receipients (email_field_id,job_event_id,contact_type_id) values (" + a[0] + "," + sJobEventId + "," + a[1].Split('|')[1].Trim() + ")";
                }
                else if (a[1].Split('|')[0] == "EG")
                {
                    sSql = "insert into job_event_receipients (email_field_id,job_event_id,email_group_id) values (" + a[0] + "," + sJobEventId + "," + a[1].Split('|')[1].Trim() + ")";
                }
                else
                {
                    sSql = "insert into job_event_receipients (email_field_id,job_event_id,email_address_id) values (" + a[0] + "," + sJobEventId + "," + a[1].Split('|')[1].Trim() + ")";
                }
                oRecip[i] = sSql;
                i++;
            }
            if (i > 0) oSql.Execute_Sql(oRecip);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return "true";
    }
    public string UpdateJobFtpDetails(string sParentJobId, string sJobType, string sFilePath, string sWebPath ,string sFtpPath,
        string sUser, string sPwd)
    {
        string Status = "";
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            sSql = "[spUpdateJobFtp]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, int.Parse(sParentJobId));
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, int.Parse(sJobType));
            param[2] = oSql.NewParameter("@file_path", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sFilePath);
            param[3] = oSql.NewParameter("@web_path", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sWebPath);
            param[4] = oSql.NewParameter("@ftp_path", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sFtpPath);
            param[5] = oSql.NewParameter("@ftp_user", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sUser);
            param[6] = oSql.NewParameter("@ftp_pwd", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sPwd);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return Status;
    }
    public string UpdateJobEvent(string[] aJobEvent, System.Collections.ArrayList lstEmailRecip)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[11];
        try
        {
            sSql = "[spUpdateJobEvents]";
            param[0] = oSql.NewParameter("@job_event_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[0]);
            param[1] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[1]);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[2]);
            if (aJobEvent[3] == "true") param[3] = oSql.NewParameter("@iscontributed", SqlDbType.Bit, int.MaxValue, ParameterDirection.Input, true);
            else param[3] = oSql.NewParameter("@iscontributed", SqlDbType.Bit, int.MaxValue, ParameterDirection.Input, false);
            param[4] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[4]);
            param[5] = oSql.NewParameter("@addnl_keywords", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[5]);
            param[6] = oSql.NewParameter("@follow_up_job_event_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[6]);
            param[7] = oSql.NewParameter("@job_event_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aJobEvent[7]);
            param[8] = oSql.NewParameter("@from_contact_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[8]);
            param[9] = oSql.NewParameter("@email_letter_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aJobEvent[9]);
            param[10] = oSql.NewParameter("@hasattachment", SqlDbType.Bit, int.MaxValue, ParameterDirection.Input, Convert.ToBoolean(aJobEvent[10]));

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
            if (Status != ""){
                if (oSql.Execute_Sql(new object[] { "delete from job_event_receipients where job_event_id=" + Status }))
                    this.InsertEmailReceipients(Status, lstEmailRecip);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string DeleteJobEvent(string sJobEventID)
    {
        sSql = "";
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spDeleteJobEvent]";
            param[0] = oSql.NewParameter("@job_event_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobEventID);

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
    public string InsertEmailAddress(string sEmailAliasName, string sEmailAddress)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spInsertEmailAddress]";
            param[0] = oSql.NewParameter("@email_address", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmailAddress);
            param[1] = oSql.NewParameter("@alias_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmailAliasName);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string InsertEmailGroup(string sEmailGroupName, System.Collections.ArrayList lstGroupReceipients)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spInsertEmailGroup]";
            param[0] = oSql.NewParameter("@email_group_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmailGroupName);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
            if (!Status.ToLower().Contains("already exists")){
                this.oSql.Execute_Sql(new object[] { "delete from email_group_receipients where email_group_id=" + Status });
                this.InsertEmailGroupReceipients(Status, lstGroupReceipients);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    private void InsertEmailGroupReceipients(string sEmailGroupID, System.Collections.ArrayList lstGrpRecip)
    {
        sSql = "";
        int i = 0;
        object[] oRecip = new object[lstGrpRecip.Count];
        try
        {
            foreach (ListItem item in lstGrpRecip)
            {
                sSql = "insert into email_group_receipients (email_group_id,email_address_id) values (" + sEmailGroupID + "," + item.Value + ")";
                oRecip[i] = sSql;
                i++;
            }
            if (i > 0) oSql.Execute_Sql(oRecip);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public string UpdateEmailGroup(string sEmailGroupID, string sEmailGroupName, System.Collections.ArrayList lstGroupReceipients)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spUpdateEmailGroup]";
            param[0] = oSql.NewParameter("@email_group_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmailGroupID);
            param[1] = oSql.NewParameter("@email_group_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmailGroupName);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
            if (!Status.ToLower().Contains("does not exists"))
            {
                this.oSql.Execute_Sql(new object[] { "delete from email_group_receipients where email_group_id=" + Status });
                this.InsertEmailGroupReceipients(Status, lstGroupReceipients);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string LogEmailSent(string sJobEventID, string sJobID, string sJobTypeID, char HasAttachment, string sEmailHeader, string sEmailChunk, string sSentBy, string sSentToName, string sContactID)
    {        
        string Status = "";
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            sSql = "[spInsertEmailSent]";
            param[0] = oSql.NewParameter("@job_event_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobEventID);
            param[1] = oSql.NewParameter("@hasattachments", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, HasAttachment);
            param[2] = oSql.NewParameter("@email_header", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmailHeader);
            param[3] = oSql.NewParameter("@email_chunk", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmailChunk);
            param[4] = oSql.NewParameter("@email_sent_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sSentBy);
            param[5] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobID);
            param[6] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[7] = oSql.NewParameter("@email_sent_to", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sSentToName);
            param[8] = oSql.NewParameter("@email_sent_contact_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sContactID);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }

    public string SendMessage(string Body, object Attachments, bool Priority)
    {
        EMailer Email=new EMailer();
        return Email.SendEmail("royson@datapage.org", "royson@datapage.org;sivaraj@datapage.org", "", "", "testing", Body, Attachments, Priority);
    }
    public string SendMessage(string sFrom, string Body, object Attachments, bool Priority)
    {
        EMailer Email = new EMailer();
        return Email.SendEmail(sFrom, "royson@datapage.org;sivaraj@datapage.org", "", "", "testing", Body, Attachments, Priority);
    }
    public string SendMessage(string sFrom, string sTo, string sCc, string sBcc, string sSubject,
        string Body, object Attachments, bool Priority){
        EMailer Email = new EMailer();
        return Email.SendEmail(sFrom, sTo, sCc, sBcc, sSubject, Body, Attachments, Priority);
    }
    private class EMailer
    {
        public string SendEmail(string sFrom, string sTo, string sCc, string sBcc, string sSubject, string sBody, object Attachments, bool IsPriority)
        {
            string sStatus = "";
            try
            {
                if (!String.IsNullOrEmpty(sFrom) && !String.IsNullOrEmpty(sTo)){
                    MailMessage oMail = new MailMessage();
                    SmtpClient oSmtp;
                    //if (sFrom.ToLower() == "john@sanlucasmedical.com"){
                    //    oSmtp = new SmtpClient("auth.myhosting.com", 25);
                    //    oSmtp.Credentials = new System.Net.NetworkCredential(sFrom, "jnewman");
                    //}
                    //else
                    //{
                    oSmtp = new SmtpClient(ConfigurationManager.AppSettings["CRM-SMTP-IP"].ToString().Trim(), Convert.ToInt32(ConfigurationManager.AppSettings["CRM-SMTP-PORT"].ToString().Trim()));
                    //}

                    oMail.From = new MailAddress(sFrom);                    
                    ParseAddress(sTo, oMail.To);
                    ParseAddress(sCc, oMail.CC);
                    ParseAddress(sBcc, oMail.Bcc);
                    oMail.Subject = sSubject;
                    oMail.IsBodyHtml = true;
                    //oMail.Body = sBody;
                    //AlternateView plain = AlternateView.CreateAlternateViewFromString(sBody, new System.Net.Mime.ContentType("text/plain"));
                    //AlternateView html = AlternateView.CreateAlternateViewFromString(sBody, new System.Net.Mime.ContentType("text/html"));
                    //oMail.AlternateViews.Add(plain);
                    //oMail.AlternateViews.Add(html);
                    oMail.Body = sBody;

                    //oMail.BodyEncoding = System.Text.Encoding.UTF8;
                    //oMail.BodyEncoding = System.Text.Encoding.GetEncoding(28591);
                    ParseAttachment(Attachments, oMail.Attachments);                    
                    if (IsPriority) oMail.Priority = MailPriority.High;
                    oSmtp.Send(oMail);
                    sStatus = "true";
                }
                else
                {
                    sStatus = "Cannot send email without From/To eamil address!";
                }                
            }
            catch (Exception ex)
            {
                sStatus = ex.Message;
            }
            finally
            {

            }
            return sStatus;
        }
        private void ParseAddress(string mailAddresses, MailAddressCollection mailAddressColl){            
            if (!String.IsNullOrEmpty(mailAddresses)){
                mailAddresses = mailAddresses.Replace(',', ';').TrimEnd(',', ';');
                string[] addresses = mailAddresses.Split(';');
                foreach (string address in addresses) mailAddressColl.Add(new MailAddress(address));                
            }            
        }
        private void ParseAttachment(object mailAttachments, AttachmentCollection mailAttachColl)
        {
            if (mailAttachments.GetType() == Type.GetType("System.String")){
                string sAttachments = mailAttachments.ToString();
                if (!String.IsNullOrEmpty(sAttachments)){
                    sAttachments = sAttachments.Replace(',', ';').TrimEnd(',', ';');
                    string[] attachments = sAttachments.Split(';');
                    foreach (string attach in attachments) mailAttachColl.Add(new Attachment(attach));
                }
            }
            else if (mailAttachments.GetType() == new System.Collections.ArrayList().GetType()){
                foreach (EmailerAttachment attachment in (System.Collections.ArrayList)mailAttachments){
                    Attachment att = new Attachment(attachment.Text);
                    if (!String.IsNullOrEmpty(attachment.UploadFileName)) att.ContentDisposition.FileName = attachment.UploadFileName;
                    mailAttachColl.Add(att);
                }
            }
            else { /* some other to come */ }
        }
        //private void ParseAttachment(string mailAttachments, AttachmentCollection mailAttachColl){
            
        //}
        //private void ParseAttachment(EmailerAttachment[] EmailAttachCollection, AttachmentCollection mailAttachColl){
            
        //}
    }    
}
public class EmailerAttachment
{
    private string _text = "";
    private string _value = "";
    private string _upload_file_name = "";
    private bool _must_attach = false;
    public EmailerAttachment() { }
    public EmailerAttachment(string text, string value, string uploadfilename, bool mustattach){
        this._text = text;
        this._value = value;
        this._upload_file_name = uploadfilename;
        this._must_attach = mustattach;
    }
    public string Text
    {
        get { return _text; }
        set { _text = value; }
    }
    public string Value
    {
        get { return _value; }
        set { _value = value; }
    }
    public string UploadFileName
    {
        get { return _upload_file_name; }
        set { _upload_file_name = value; }
    }
    public bool MustAttach
    {
        get { return _must_attach; }
        set { _must_attach = value; }
    }
}

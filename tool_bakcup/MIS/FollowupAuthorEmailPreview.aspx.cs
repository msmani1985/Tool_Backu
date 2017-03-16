using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;


public partial class FollowupAuthorEmailPreview : System.Web.UI.Page
{
    string job_id;
    string job_id2;
    string job_type_id = "5";
    string emp_id;
    bool follow_email = false;
    bool remainder_email = false;
    bool MailSent = false;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        else
        {
            emp_id = Session["employeeid"].ToString();
            job_id = Request.QueryString["JOBID"].ToString().Trim();
           // job_id = Session["jobid"].ToString(); 
            
            //job_id2 = Request.QueryString["JOBID2"].ToString().Trim();


            if (!Page.IsPostBack)
            {
                //emp_id = Request.QueryString["EMPID"].ToString().Trim();

                // job_type_id = Request.QueryString["JOBTYPEID"].ToString().Trim();
                FollowUpMailSent();

            }
        }
    }

    protected void FollowUpMailSent()
    {
        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        dst = dsql.ExcProcedure("spGet_SAM_Email_Content", new string[,] { { "@jobid", job_id.ToString() }, { "@jobtypeid", job_type_id.ToString() }, { "@followemail", follow_email.ToString() }, { "@reminderemail", remainder_email.ToString() } }, CommandType.StoredProcedure);
        //string fromaddress = ConfigurationManager.AppSettings["SAMFollowupmail_fromaddress"].ToString();

        if (dst != null)
        {

            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    xd.Load(Server.MapPath("").ToString() + @"\SAMFollowup_Emailconfig_sql.xml");

                    //xn = xd.DocumentElement.SelectSingleNode("//followup").SelectSingleNode("customer[@custno='" + Request.QueryString["CUSTNO"].ToString().Trim() + "']");

                    xn = xd.DocumentElement.SelectSingleNode("//followup").FirstChild;

                    if ((xn != null) && (samrow != null))
                    {
                        childnode = xn.SelectSingleNode("to");
                       // lblToAddress.Text = "software@datapage.org";
                        lblToAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        //lblToAddress.Text = (Request.QueryString["AEMAIL"].ToString().Trim() != "") ? Request.QueryString["AEMAIL"].ToString().Trim() : (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        childnode = xn.SelectSingleNode("cc");
                        lblCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        //lblCCAddress.Text = (Request.QueryString["INVCONEMAIL"].ToString().Trim() != "") ? Request.QueryString["INVCONEMAIL"].ToString().Trim() : (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        childnode = xn.SelectSingleNode("bcc");
                        lblBCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        //lblBCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        childnode = xn.SelectSingleNode("body");
                        lblBody.InnerHtml = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        lblBody.InnerHtml = (childnode != null && childnode.InnerXml != "") ? childnode.InnerXml.ToString() : "";
                        lblBody.InnerHtml = lblBody.InnerHtml.Replace("[AUTHORNAME]", samrow["AFIRSTAUTHOR"].ToString().Trim());
                        lblBody.InnerHtml = lblBody.InnerHtml.Replace("[AUTHOREMAIL]", samrow["AEMAIL"].ToString().Trim());
                        lblBody.InnerHtml = lblBody.InnerHtml.Replace("[AUTHORTITLE]", samrow["AMNSTITLE"].ToString().Trim());
                        lblBody.InnerHtml = lblBody.InnerHtml.Replace("[PENAME]", samrow["INVDISPLAYNAME"].ToString().Trim());
                        lblBody.InnerHtml = lblBody.InnerHtml.Replace("[PEEMAIL]", samrow["INVCONEMAIL"].ToString().Trim());
                        //lblBody.InnerHtml = lblBody.InnerHtml.Replace("[AUTHORCORRECTON]", samrow["AUTHORCORRECTION"].ToString().Trim());
                        //lblBody.InnerHtml = lblBody.InnerHtml.Replace("[EDITORCORRECTION]", samrow["EDITORCORRECTION"].ToString().Trim());
                        lblBody.InnerHtml = lblBody.InnerHtml.Replace("[ARTICLEID]", samrow["AMANUSCRIPTID"].ToString().Trim());
                        lblBody.InnerHtml = lblBody.InnerHtml.Replace("[JOURNALTITLE]", samrow["JOURNAL_TITLE"].ToString().Trim());
                        lblSubject.Text = Request.QueryString["mailstring"].ToString() + " - " + samrow["ARTICLECODE"].ToString().Trim();
                    }

                }
            }



            catch (Exception ex)
            { throw ex; }
            finally
            { xd = null; childnode = null; xn = null; }
        }
        else
            alert("No Records Found");
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        MailMessage mmsg = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        datasourceIB dobj = new datasourceIB();
        try
        {
            smtp.Host = ConfigurationManager.AppSettings["Invoicemail_host"].ToString();
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Invoicemail_port"].ToString());
            mmsg = new MailMessage();
            if (!addmailcollection(lblToAddress.Text.Trim().ToString(), mmsg.To))
            { alert("Your To Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblCCAddress.Text.Trim().ToString(), mmsg.CC))
            { alert("Your CC Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblBCCAddress.Text.Trim().ToString(), mmsg.Bcc))
            { alert("Your BCC Address is not Valid, Please check"); return; }
            mmsg.From = new MailAddress(lblFromAddress.Text.ToString(), ConfigurationManager.AppSettings["SAMFollowupmail_fromaddress"].ToString());
           // mmsg.Subject = Request.QueryString["mailstring"].ToString() + " - " + samrow["ARTICLECODE"].ToString().Trim();
            mmsg.Subject = lblSubject.Text.Trim().ToString();
            mmsg.Body = lblBody.InnerText.Trim().ToString();
            mmsg.IsBodyHtml = true;
            smtp.Send(mmsg);
            MailSent = true;
            Session["mailsent"] = MailSent.ToString();
            if (Convert.ToBoolean(Session["mailsent"]) == true)
            {
                
                if (Convert.ToInt32(Request.QueryString["category"]) == 1)
                {
                    datasourceSQL dsql = new datasourceSQL();
                    dsql.ExcSProcedure("sp_UpdateSamFollowMailSent", new string[,] { { "@author_samfollow_sent_by", emp_id }, { "@job_id", job_id.ToString() } }, CommandType.StoredProcedure);
                }
                else if (Convert.ToInt32(Request.QueryString["category"]) == 2)
                {
                    datasourceSQL dsql = new datasourceSQL();
                    dsql.ExcSProcedure("sp_UpdateSamFollowReminderSent", new string[,] { { "@author_samfollow_reminder_sent_by", emp_id }, { "@job_id", job_id.ToString() } }, CommandType.StoredProcedure);
                }

            }

            alert("Mailsent Successfully");
            
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mmsg = null; smtp = null; }
        

    }


    private bool addmailcollection(string mailaddress, MailAddressCollection mac)
    {
        if (mailaddress != "")
        {
            string[] address = mailaddress.Split(';');
            for (int maddcnt = 0; address.Length > maddcnt; maddcnt++)
            {
                if (address[maddcnt].ToString().Trim() != "")
                {
                    if (!Regex.IsMatch(address[maddcnt].Trim().ToString(), @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                        return false;
                    else
                        mac.Add(address[maddcnt].Trim().ToString());
                }
            }
        }
        return true;
    }
    private void alert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');window.close();</script>");
    }
}

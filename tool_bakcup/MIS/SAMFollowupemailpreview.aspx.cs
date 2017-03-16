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
public partial class SAMFollowupemailpreview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            XmlDocument xd = new XmlDocument();
            XmlNode childnode = null;
            XmlNode xn = null;

            string isToday = System.DateTime.Now.DayOfWeek.ToString();  

            try
            {
                if (isToday == "Friday" || isToday == "Saturday" || isToday == "Sunday")  
                {
                    xd.Load(Server.MapPath("").ToString() + @"\SAMFollowup_Emailconfig_Friday.xml");
                }
                else
                {
                    xd.Load(Server.MapPath("").ToString() + @"\SAMFollowup_Emailconfig.xml");
                }
                xn = xd.DocumentElement.SelectSingleNode("//followup").SelectSingleNode("customer[@custno='" + Request.QueryString["CUSTNO"].ToString().Trim() + "']");
                if (xn != null)
                {
                    childnode = xn.SelectSingleNode("to");
                    lblToAddress.Text = (Request.QueryString["AEMAIL"].ToString().Trim() != "") ? Request.QueryString["AEMAIL"].ToString().Trim() : (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                    childnode = xn.SelectSingleNode("cc");
                    lblCCAddress.Text = (Request.QueryString["INVCONEMAIL"].ToString().Trim() != "") ? Request.QueryString["INVCONEMAIL"].ToString().Trim() : (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                    childnode = xn.SelectSingleNode("bcc");
                    lblBCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                    childnode = xn.SelectSingleNode("body");
                    lblBody.InnerHtml = (childnode != null && childnode.InnerXml != "") ? childnode.InnerXml.ToString() : "";
                    lblBody.InnerHtml = lblBody.InnerHtml.Replace("[AUTHORNAME]", Request.QueryString["AFIRSTAUTHOR"].ToString().Trim());
                    lblBody.InnerHtml = lblBody.InnerHtml.Replace("[AUTHOREMAIL]", Request.QueryString["AEMAIL"].ToString().Trim());
                    lblBody.InnerHtml = lblBody.InnerHtml.Replace("[AUTHORTITLE]", Request.QueryString["AMNSTITLE"].ToString().Trim());
                    lblBody.InnerHtml = lblBody.InnerHtml.Replace("[PENAME]", Request.QueryString["INVDISPLAYNAME"].ToString().Trim());
                    lblBody.InnerHtml = lblBody.InnerHtml.Replace("[PEEMAIL]", Request.QueryString["INVCONEMAIL"].ToString().Trim());
                    lblBody.InnerHtml = lblBody.InnerHtml.Replace("[AUTHORCORRECTON]", Request.QueryString["AUTHORCORRECTION"].ToString().Trim());
                    lblBody.InnerHtml = lblBody.InnerHtml.Replace("[EDITORCORRECTION]", Request.QueryString["EDITORCORRECTION"].ToString().Trim());
                    lblBody.InnerHtml = lblBody.InnerHtml.Replace("[ARTICLEID]", Request.QueryString["AMANUSCRIPTID"].ToString().Trim());
                    lblBody.InnerHtml = lblBody.InnerHtml.Replace("[JOURNALTITLE]", Request.QueryString["JOURNAL_TITLE"].ToString().Replace('_', '&')).Trim();
                    //lblBody.InnerHtml = lblBody.InnerHtml.Replace("<br />", "");
                    lblSubject.Text = Request.QueryString["mailstring"].ToString() + " - " + Request.QueryString["ARTICLECODE"].ToString().Trim();
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { xd = null; childnode = null; xn = null; }
        }
    }
    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {

        MailMessage mmsg = new MailMessage();
        SmtpClient smt = new SmtpClient();
        datasourceIBSQL dobj = new datasourceIBSQL();
        try
        {
            smt.Host = ConfigurationManager.AppSettings["Invoicemail_host"].ToString();
            smt.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Invoicemail_port"].ToString());
            mmsg = new MailMessage();
            if (!addmailcollection(lblToAddress.Text.Trim().ToString(), mmsg.To))
            { alert("Your To Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblCCAddress.Text.Trim().ToString(), mmsg.CC))
            { alert("Your CC Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblBCCAddress.Text.Trim().ToString(), mmsg.Bcc))
            { alert("Your BCC Address is not Valid, Please check"); return; }
            mmsg.From = new MailAddress(lblFromAddress.Text.ToString(), ConfigurationManager.AppSettings["SAMFollowupmail_aliasname"].ToString());
            //mmsg.Subject = Request.QueryString["mailstring"].ToString() + " - " + Request.QueryString["ARTICLECODE"].ToString().Trim();
            mmsg.Subject = lblSubject.Text.Trim().ToString();
            mmsg.Body = lblBody.InnerText.Trim().ToString();
            mmsg.IsBodyHtml = true;
            smt.Send(mmsg);
            if (Request.QueryString["mailtype"].ToString() == "1")
            {
                if (!dobj.updateSAMAuthormailsent("update article_dp set authormailsent='" + DateTime.Now.ToShortDateString() + "',authormailsentby=" + Session["employeenumber"] + " where ano in(" + Request.QueryString["ano"].ToString() + ")", CommandType.Text))
                { alert("Updation Failed"); return; }
            }
            else if (Request.QueryString["mailtype"].ToString() == "2")
            {
                if (!dobj.updateSAMAuthormailsent("update article_dp set author_reminder_mailsent='" + DateTime.Now.ToShortDateString() + "' where ano in(" + Request.QueryString["ano"].ToString() + ")", CommandType.Text))
                    alert("Updation Failed");
            }
            alert("Mailsent Successfully");

        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mmsg = null; smt = null; }
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
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
    }
}

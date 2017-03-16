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
using System.Net.NetworkInformation;

public partial class FPMAuthorQueryEmailPreview : System.Web.UI.Page
{
    string job_id;
    string job_id2;
    string job_type_id = "5";
    string emp_id;
    bool follow_email = false;
    bool remainder_email = false;
    bool MailSent = false;
    string strFileName_path = ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString();

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
                    xd.Load(Server.MapPath("").ToString() + @"\FPMFollowup_Emailconfig.xml");

                    //xn = xd.DocumentElement.SelectSingleNode("//followup").SelectSingleNode("customer[@custno='" + Request.QueryString["CUSTNO"].ToString().Trim() + "']");

                    xn = xd.DocumentElement.SelectSingleNode("//followup").FirstChild;

                    if ((xn != null) && (samrow != null))
                    {
                        childnode = xn.SelectSingleNode("to");
                        // lblToAddress.Text = "software@datapage.org";
                        lblToAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        // lblToAddress.Text = (Request.QueryString["AEMAIL"].ToString().Trim() != "") ? Request.QueryString["AEMAIL"].ToString().Trim() : (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        lblFromAddress.Text = samrow["INVCONEMAIL"].ToString().Trim();
                        lblToAddress.Text = samrow["AEMAIL"].ToString().Trim();
                        //childnode = xn.SelectSingleNode("cc");
                        //lblCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        //lblCCAddress.Text = samrow["INVCONEMAIL"].ToString().Trim();
                        childnode = xn.SelectSingleNode("bcc");
                        lblBCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        lblBCCAddress.Text = "group1@datapage.org;jincy@datapage.org;bharath@datapage.org;" + samrow["INVCONEMAIL"].ToString().Trim();
                        childnode = xn.SelectSingleNode("body");
                        lblBody.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        lblBody.Text = (childnode != null && childnode.InnerXml != "") ? childnode.InnerXml.ToString() : "";
                        lblBody.Text = lblBody.Text.Replace("[AUTHORNAME]", samrow["AFIRSTAUTHOR"].ToString().Trim());
                        lblBody.Text = lblBody.Text.Replace("[AUTHOREMAIL]", samrow["AEMAIL"].ToString().Trim());
                        lblBody.Text = lblBody.Text.Replace("[AUTHORTITLE]", samrow["AMNSTITLE"].ToString().Trim());
                        lblBody.Text = lblBody.Text.Replace("[PENAME]", samrow["INVDISPLAYNAME"].ToString().Trim());
                        lblBody.Text = lblBody.Text.Replace("[PEEMAIL]", samrow["INVCONEMAIL"].ToString().Trim());
                        lblBody.Text = lblBody.Text.Replace("[AUTHORCORRECTON]", samrow["SAMAUTHORQUERY"].ToString().Trim());
                        //lblBody.InnerHtml = lblBody.InnerHtml.Replace("[EDITORCORRECTION]", samrow["EDITORCORRECTION"].ToString().Trim());
                        lblBody.Text = lblBody.Text.Replace("[ARTICLEID]", samrow["AMANUSCRIPTID"].ToString().Trim());
                        lblBody.Text = lblBody.Text.Replace("[JOURNALTITLE]", samrow["JOURNAL_TITLE"].ToString().Trim());
                        lblBody.Text = lblBody.Text.Replace("[ARTICLECODE]", samrow["ARTICLECODE"].ToString().Trim());
                        lblBody.Text = lblBody.Text.Replace("[FPMJOURNAME]", samrow["INVDISPLAYNAME"].ToString().Trim());
                        lblBody.Text = lblBody.Text.Replace("[JournalCode]", samrow["JOURCODE"].ToString().Trim());
                        lblBody.Text = lblBody.Text.Replace("[JournalMailID]", samrow["INVCONEMAIL"].ToString().Trim());
                        //lblSubject.Text = Request.QueryString["mailstring"].ToString() + " - " + samrow["ARTICLECODE"].ToString().Trim();
                        lblSubject.Text = samrow["ARTICLECODE"].ToString().Trim() + " - " + Request.QueryString["mailstring"].ToString();
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

    static string GetMacAddress()
    {
        string macAddresses = "";
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            // Only consider Ethernet network interfaces, thereby ignoring any
            // loopback devices etc.
            if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddresses;
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        MailMessage mmsg = new MailMessage();
        SmtpClient smt = new SmtpClient();
        // datasourceSQL dobj = new datasourceSQL();
        string mac_id = GetMacAddress();
        string Mac_address = ConfigurationManager.AppSettings["Mac_address"].ToString();
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
            mmsg.From = new MailAddress(lblFromAddress.Text.ToString());
            //mmsg.Subject = Request.QueryString["mailstring"].ToString() + " - " + Request.QueryString["ARTICLECODE"].ToString().Trim();
            mmsg.Subject = lblSubject.Text.Trim().ToString();
            mmsg.Body = lblBody.Text.Trim().ToString();
            mmsg.IsBodyHtml = true;

            string strFileName = "";
            if (File_upload.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }
            if (File_upload2.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload2.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload2.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }
            //if (Mac_address == mac_id)
            //{
            smt.Send(mmsg);

            datasourceSQL dsql = new datasourceSQL();
            dsql.ExcSProcedure("sp_UpdateSamFollowMailSent", new string[,] { { "@author_samfollow_sent_by", emp_id }, { "@job_id", job_id.ToString() } }, CommandType.StoredProcedure);

            //if (!System.IO.Directory.Exists(strFileName_path + strFileName))
            //{
            //    File.Delete(strFileName_path + System.IO.Path.GetFileName(File_upload.PostedFile.FileName));
            //}


            alert("Mail Sent Successfully");
            //Response.Write("<script type='text/javascript'>alert('');window.close();</script>");

            //}
            //else
            //    alert("Mail sent Failed - You dont have permission to send mail");

        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mmsg = null; smt = null; }
    }

    private void click_link()
    {
        HttpPostedFile attFile = File_upload.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }

        strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload.PostedFile.FileName);
        File_upload.SaveAs(strFileName_path);
        LinkButton lnkbtn = new LinkButton();
        lnkbtn.Text = strFileName_path;
        lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
        lnkbtn.Click += new EventHandler(Attachment_View_Click);

    }

    private void click_link1()
    {
        HttpPostedFile attFile = File_upload2.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }

        strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload2.PostedFile.FileName);
        File_upload.SaveAs(strFileName_path);
        LinkButton lnkbtn = new LinkButton();
        lnkbtn.Text = strFileName_path;
        lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
        lnkbtn.Click += new EventHandler(Attachment_View_Click);

    }



    private LinkButton AttachmentLinkAttributes(LinkButton attachlink, string filepath)
    {
        ExcelPDFExport(Path.GetExtension(strFileName_path), strFileName_path);
        attachlink.CommandArgument = attachlink.Text;
        return attachlink;
    }

    protected void Attachment_View_Click(object sender, EventArgs e)
    {
        LinkButton Attachmemt_LnkBtn = sender as LinkButton;
        string pathfname = Attachmemt_LnkBtn.CommandArgument.ToString();

        pathfname = strFileName_path;
        ExcelPDFExport(Path.GetExtension(pathfname), pathfname);
    }

    private void ExcelPDFExport(string sType, string sFileName)
    {
        try
        {

            if (File.Exists(sFileName))
            {
                if (sType == ".xls")
                    Response.ContentType = "application/vnd.xls";
                else if (sType == ".pdf")
                    Response.ContentType = "application/pdf";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Buffer = true;
                Response.Charset = "";
                Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(sFileName));
                this.EnableViewState = false;
                Response.WriteFile(sFileName);
                Response.Flush();
                Response.Close();

                //Response.End();
            }
            else
            { lblMessage.Text = (sFileName.IndexOf("File Missing") > 0) ? "File Missing" : Path.GetFileName(sFileName) + ", not found!"; }
        }
        catch (Exception oex)
        { lblMessage.Text = oex.Message; }

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
    protected void HlnkloadFile_Click(object sender, EventArgs e)
    {
        click_link();
    }
    protected void HlnkloadFile_Click1(object sender, EventArgs e)
    {
        click_link1();
    }
}

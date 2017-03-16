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
using System.Net.Mime;
using System.Net;

public partial class Lead_EmailPreview : System.Web.UI.Page
{
    string Lead_id;
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
            Lead_id = Request.QueryString["Salesleadno"].ToString().Trim();
            if (!Page.IsPostBack)
            {
                FollowUpMailSent();
            }
        }
    }
    protected void FollowUpMailSent1()
    {
        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        dst = dsql.ExcProcedure("spGetSalesLeadFollowup_Details", new string[,] { { "@salesleadno", Lead_id.ToString() } }, CommandType.StoredProcedure);
        if (dst != null)
        {
            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    xd.Load(Server.MapPath("").ToString() + @"\Lead_Emailconfig.xml");
                    if (droplang.SelectedValue == "Spanish")
                        xn = xd.DocumentElement.SelectSingleNode("//followup/Spanish").FirstChild;
                    else if (droplang.SelectedValue == "English")
                        xn = xd.DocumentElement.SelectSingleNode("//followup/English").FirstChild;
                    else if (droplang.SelectedValue == "Portuguese")
                        xn = xd.DocumentElement.SelectSingleNode("//followup/Portuguese").FirstChild;
                    else if (droplang.SelectedValue == "French")
                        xn = xd.DocumentElement.SelectSingleNode("//followup/French").FirstChild;

                    if ((xn != null) && (samrow != null))
                    {
                        childnode = xn.SelectSingleNode("body");
                        lblBody.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        lblBody.Text = (childnode != null && childnode.InnerXml != "") ? childnode.InnerXml.ToString() : "";
                        lblBody.Text = lblBody.Text.Replace("[COMPANY]", samrow["slcompany"].ToString().Trim());
                        childnode = xn.SelectSingleNode("Sub");
                        lblSubject.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
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
    protected void FollowUpMailSent()
    {
        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        dst = dsql.ExcProcedure("spGetSalesLeadFollowup_Details", new string[,] { { "@salesleadno", Lead_id.ToString() } }, CommandType.StoredProcedure);
        if (dst != null)
        {
            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    xd.Load(Server.MapPath("").ToString() + @"\Lead_Emailconfig.xml");
                    xn = xd.DocumentElement.SelectSingleNode("//followup/English").FirstChild;

                    if ((xn != null) && (samrow != null))
                    {
                        childnode = xn.SelectSingleNode("to");
                        lblToAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        lblToAddress.Text = samrow["ci_email"].ToString().Trim();
                        childnode = xn.SelectSingleNode("cc");
                        lblCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        lblCCAddress.Text = "";
                        childnode = xn.SelectSingleNode("bcc");
                        lblBCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        childnode = xn.SelectSingleNode("body");
                        lblBody.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        lblBody.Text = (childnode != null && childnode.InnerXml != "") ? childnode.InnerXml.ToString() : "";
                        lblBody.Text = lblBody.Text.Replace("[COMPANY]", samrow["slcompany"].ToString().Trim());
                        childnode = xn.SelectSingleNode("Sub");
                        lblSubject.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        //lblSubject.Text = "DTP Services";
                        
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
    protected void HlnkloadFile_Click(object sender, EventArgs e)
    {
        click_link();
    }
    protected void HlnkloadFile_Click1(object sender, EventArgs e)
    {
        click_link1();
    }
    private void click_link()
    {
        HttpPostedFile attFile = File_upload.PostedFile;
        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload.PostedFile.FileName);
            File_upload.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }
    }

    private void click_link1()
    {
        HttpPostedFile attFile = File_upload2.PostedFile;
        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload2.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload2.PostedFile.FileName);
            File_upload.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }
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
    static string GetMacAddress()
    {
        string macAddresses = "";
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
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
        string mac_id = GetMacAddress();
        string Mac_address = ConfigurationManager.AppSettings["Mac_address"].ToString();
        try
        {
            smt.Host = ConfigurationManager.AppSettings["Invoicemail_host"].ToString();
            smt.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Invoicemail_port"].ToString());
            smt.Credentials = new System.Net.NetworkCredential(lblFromAddress.Text.Trim().ToString(), "D@tap@g#@987");
            smt.EnableSsl = true;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }; 
            mmsg = new MailMessage();
            if (!addmailcollection(lblToAddress.Text.Trim().ToString(), mmsg.To))
            { alert("Your To Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblCCAddress.Text.Trim().ToString(), mmsg.CC))
            { alert("Your CC Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblBCCAddress.Text.Trim().ToString(), mmsg.Bcc))
            { alert("Your BCC Address is not Valid, Please check"); return; }
            mmsg.From = new MailAddress(lblFromAddress.Text.ToString());
            mmsg.Subject = lblSubject.Text.Trim().ToString();
            mmsg.Body = lblBody.Text.Trim().ToString();
            mmsg.IsBodyHtml = true;

            byte[] imgByte = null;
            using (var webClient = new WebClient())
            {
            webClient.UseDefaultCredentials = true;
            Uri uri = new Uri(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + "Datapage.png");
            imgByte = webClient.DownloadData(uri);
            }

            Stream memStream = new MemoryStream(imgByte);
            LinkedResource pic6 = new LinkedResource(memStream, "image/png");
            pic6.ContentId = "myImageID";
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(lblBody.Text.Trim().ToString(), null, System.Net.Mime.MediaTypeNames.Text.Html);
            htmlView.LinkedResources.Add(pic6);
            mmsg.AlternateViews.Add(htmlView);

            //byte[] reader = File.ReadAllBytes(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + "Datapage.jpg");
            //MemoryStream image1 = new MemoryStream(reader);

            //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(lblBody.Text.Trim().ToString(), null, System.Net.Mime.MediaTypeNames.Text.Html);
            //LinkedResource theEmailImage = new LinkedResource(image1, System.Net.Mime.MediaTypeNames.Image.Jpeg);
            //theEmailImage.ContentId = "myImageID";
            //theEmailImage.ContentType = new ContentType("image/jpg");
            //theEmailImage.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            //htmlView.LinkedResources.Add(pic6);
            //mmsg.AlternateViews.Add(htmlView);

            //LinkedResource inline = new LinkedResource(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + "Datapage.jpg");
            ////inline.ContentId = "myImageID";
            //string htmlBody = lblBody.Text.Trim().ToString().Replace("cid:myImageID", ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + "Datapage.jpg");
            //AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            //alternateView.LinkedResources.Add(inline);
            //mmsg.Body = htmlBody;
            
            //mmsg.AlternateViews.Add(getEmbeddedImage(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + "Datapage.jpg", lblBody.Text.Trim().ToString()));
            
            System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + "Applications.jpg");
            mmsg.Attachments.Add(attach);
            System.Net.Mail.Attachment attach1 = new System.Net.Mail.Attachment(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + "Languages.jpg");
            mmsg.Attachments.Add(attach1);

            smt.Send(mmsg);
            datasourceSQL dsql = new datasourceSQL();
            dsql.ExcSProcedure("sp_UpdateLeadFollowMailSent", new string[,] { { "@EMP_ID", emp_id }, { "@lead_id", Lead_id.ToString() } }, CommandType.StoredProcedure);
            dsql.ExcSProcedure("sp_InsertEmailLogs",
                new string[,] { 
                    { "@From", lblFromAddress.Text.ToString() }, { "@To", lblToAddress.Text.Trim().ToString() },
                    { "@CC", lblCCAddress.Text.Trim().ToString() }, { "@Bcc", lblBCCAddress.Text.Trim().ToString() },
                    { "@Sub", lblSubject.Text.Trim().ToString() }, { "@Body", lblBody.Text.Trim().ToString() },
                    { "@EMPID", emp_id }, { "@lead_id", Lead_id.ToString() } }, CommandType.StoredProcedure);
            alert("Mail Sent Successfully");
            lblMessage.Text = "Mail Sent Successfully";
            btnSubmit.Enabled = false;
            btnSubmit.Visible = false;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mmsg = null; smt = null; }
    }
    private AlternateView getEmbeddedImage(String filePath, string body)
    {
        LinkedResource inline = new LinkedResource(HttpContext.Current.Server.MapPath("/images/Datapage.jpg"));
        inline.ContentId = "myImageID";
        inline.ContentType.MediaType = "image/jpg";
        string htmlBody = body;
        AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
        alternateView.LinkedResources.Add(inline);
        lblBody.Text = htmlBody;
        return alternateView;
    }
    protected void droplang_SelectedIndexChanged(object sender, EventArgs e)
    {
        FollowUpMailSent1();
    }
}
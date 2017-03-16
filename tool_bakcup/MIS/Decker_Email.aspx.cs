using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Decker_Email : System.Web.UI.Page
{
    string strFileName_path = ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString();
    bool authornamestatus = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showPanel(tabSAGHE);
            mailbody();
        }
    }
    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        MailMessage mmsg = new MailMessage();
        SmtpClient smt = new SmtpClient();
        datasourceIBSQL dobj = new datasourceIBSQL();
        try
        {
            //smt.Host = "192.168.2.1";
            smt.Host = "mail.nimbox.co.uk";
            //smt.Credentials = new NetworkCredential("software@datapage.org", "Reset*123");
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,System.Security.Cryptography.X509Certificates.X509Certificate certificate,System.Security.Cryptography.X509Certificates.X509Chain chain,System.Net.Security.SslPolicyErrors sslPolicyErrors)
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
            mmsg.Body = lblBody.InnerText.Trim().ToString();
            mmsg.IsBodyHtml = true;
            string strFileName = "";

            //string uqry = "UPDATE Article_dp SET SREP_Email_Flag=1,SREP_Sent_By=" + Convert.ToInt32(Session["employeeid"]) + ",SREP_Sent_On='" + DateTime.Now + "'  WHERE ano =" + Convert.ToInt32(Session["jobid"]);
            //dobj.ExcuteProc(uqry);

            if (File_upload_1.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_1.PostedFile;
                int attachFileLength = attFile.ContentLength;


                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_1.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                       // System.IO.File.Delete(strFileName_path + "\\" + strFileName);
                    { }

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_1.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);
                }

            }
            if (File_upload_2.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_2.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_2.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                    {}
                       // System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_2.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }
            if (File_upload_3.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_3.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_3.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_3.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }
            if (File_upload_4.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_4.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_4.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_4.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }
            if (File_upload_5.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_5.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_5.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_5.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }

            smt.Send(mmsg);
            alert("Mailsent Successfully");


        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mmsg = null; smt = null; authornamestatus = false; }
    }

    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabSAGHE":
                miSAGHE.Attributes.Add("class", "current");
                miMI.Attributes.Add("class", "");
                miSAE.Attributes.Add("class", "");
                miSAM.Attributes.Add("class", "");
                this.tabSAGHE.Visible = true;
                this.tabMI.Visible = false;
                this.tabSAE.Visible = false;
                this.tabSAM.Visible = false;
                break;
            case "tabMI":
                miSAGHE.Attributes.Add("class", "");
                miMI.Attributes.Add("class", "current");
                miSAE.Attributes.Add("class", "");
                miSAM.Attributes.Add("class", "");
                this.tabSAGHE.Visible = false;
                this.tabMI.Visible = true;
                this.tabSAE.Visible = false;
                this.tabSAM.Visible = false;
                break;
            case "tabSAE":
                miSAGHE.Attributes.Add("class", "");
                miMI.Attributes.Add("class", "");
                miSAE.Attributes.Add("class", "current");
                miSAM.Attributes.Add("class", "");
                this.tabSAGHE.Visible = false;
                this.tabMI.Visible = false;
                this.tabSAE.Visible = true;
                this.tabSAM.Visible = false;
                break;
            case "tabSAM":
                miSAGHE.Attributes.Add("class", "");
                miMI.Attributes.Add("class", "");
                miSAE.Attributes.Add("class", "");
                miSAM.Attributes.Add("class", "current");
                this.tabSAGHE.Visible = false;
                this.tabMI.Visible = false;
                this.tabSAE.Visible = false;
                this.tabSAM.Visible = true;
                break;

            default:
                miSAGHE.Attributes.Add("class", "current");
                miMI.Attributes.Add("class", "");
                this.tabSAGHE.Visible = true;
                this.tabMI.Visible = false;
                break;
        }
    }

    private void click_link_1()
    {
        HttpPostedFile attFile = File_upload_1.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_1.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_1.PostedFile.FileName);
            File_upload_1.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_2()
    {
        HttpPostedFile attFile = File_upload_2.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_2.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_2.PostedFile.FileName);
            File_upload_2.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_3()
    {
        HttpPostedFile attFile = File_upload_3.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_3.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_3.PostedFile.FileName);
            File_upload_3.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_4()
    {
        HttpPostedFile attFile = File_upload_4.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_4.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_4.PostedFile.FileName);
            File_upload_4.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_5()
    {
        HttpPostedFile attFile = File_upload_5.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_5.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_5.PostedFile.FileName);
            File_upload_5.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    public void mailbody()
    {

        //string sname = Session["filname"].ToString();
        //string surl = Session["url"].ToString();
        //string sAuthor = Session["Author"].ToString();
        //lblSubject.Text = "Proofs of Scientific American Neurology";
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please find attached the formatted page proofs for your chapter that we plan to publish in an upcoming issue of Scientific American Gastroenterology, Hepatology, and Endoscopy. Decker Intellectual Properties is working to update the look of the chapters. Accordingly, we have added color to existing black and white images and to the tables. Please review the material in the author package first and follow the instructions carefully. Review all text for accuracy ensuring that figures and tables are placed correctly, table data are aligned correctly, and legends correspond with the figures. Note that if your drawings have been rerendered or have had color added to them, please check them to ensure that they are accurate. If you have any serious reservations about the use of color or how the color was applied, please indicate so clearly. Note that we CANNOT accept any changes to the text and references unless they are corrections to outright factual errors as they will delay the production of your chapter and result in cost implications to the book. At this stage, we cannot guarantee that all corrections you suggest to the text will be applied, unless they were oversights on the part of Decker Intellectual Properties.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Please note the deadline for returning materials is 3 business days. If you have any difficulty meeting this deadline, please contact Klaudia Bednarczyk at ");
        MyStringBuilder.Append("(kbednarczyk@deckerip.com) to see if it is possible to arrange another date. Otherwise, please return the materials to our associate publisher Charlesworth.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Sincerely, <br/>");
        MyStringBuilder.Append("Decker Proofing<br/>");
        MyStringBuilder.Append("Charlesworth India<br/>");
        MyStringBuilder.Append("email: project.manager@charlesworth-group.in<br/>");
        MyStringBuilder.Append("Fax: 0044 1484 536032<br/>");
        MyStringBuilder.Append("Phone: 044 42267215<br/>");

        lblBody.InnerText = MyStringBuilder.ToString();
        //  lblBody. = MyStringBuilder.ToString();


    }

    public void mailbody1()
    {

        //string sname = Session["filname"].ToString();
        //string surl = Session["url"].ToString();
        //string sAuthor = Session["Author"].ToString();
        //lblSubject.Text = "Proofs of Scientific American Neurology";
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please find attached the formatted page proofs of your article, in the form of a low resolution PDF file, that we are preparing for an upcoming issue of Molecular Imaging and the accompanying author package. Note that we CANNOT accept any changes to the text and references unless they are corrections to outright factual errors, as they will delay the production of your article and result in cost implications to the journal. At this stage, we cannot guarantee that all corrections you suggest to the text will be applied, unless they are corrections to factual errors or oversights on the part of Decker Proofing Inc.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Please note the deadline for returning materials is 2 business days.  If you have any difficulty meeting this deadline, please contact me to see if it is possible to arrange another date. If we do not hear from you within this time range, we will assume your article is correct and will publish it accordingly.");

        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Sincerely, <br/>");
        MyStringBuilder.Append("Decker Proofing<br/>");
        MyStringBuilder.Append("Charlesworth India<br/>");
        MyStringBuilder.Append("email: project.manager@charlesworth-group.in<br/>");
        MyStringBuilder.Append("Fax: 0044 1484 536032<br/>");
        MyStringBuilder.Append("Phone: 044 42267215<br/>");

        lblBody.InnerText = MyStringBuilder.ToString();
        //  lblBody. = MyStringBuilder.ToString();


    }

    public void mailbodySAE()
    {
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please find attached the formatted page proofs for your chapter that we plan to publish in an upcoming issue of Scientific American Emergency Medicine. Decker Intellectual Properties is working to update the look of the chapters. Accordingly, we have added color to existing black and white images and to the tables. Please review the material in the author package first and follow the instructions carefully. Review all text for accuracy ensuring that figures and tables are placed correctly, table data are aligned correctly, and legends correspond with the figures. Note that if your drawings have been rerendered or have had color added to them, please check them to ensure that they are accurate. If you have any serious reservations about the use of color or how the color was applied, please indicate so clearly. Note that we CANNOT accept any changes to the text and references unless they are corrections to outright factual errors as they will delay the production of your chapte  r and result in cost implications to the book. At this stage, we cannot guarantee that all corrections you suggest to the text will be applied, unless they were oversights on the part of Decker Intellectual Properties.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Please note the deadline for returning materials is 3 business days. If you have any difficulty meeting this deadline, please contact Lindsay Karpenko at lkarpenko@deckerip.com to see if it is possible to arrange another date. Otherwise, please return the materials to our associate publisher Charlesworth.");

        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Sincerely, <br/>");
        MyStringBuilder.Append("Decker Proofing<br/>");
        MyStringBuilder.Append("Charlesworth India<br/>");
        MyStringBuilder.Append("email: project.manager@charlesworth-group.in<br/>");
        MyStringBuilder.Append("Fax: 0044 1484 536032<br/>");
        MyStringBuilder.Append("Phone: 044 42267215<br/>");

        lblBody.InnerText = MyStringBuilder.ToString();
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
                else if (sType == ".doc")
                    Response.ContentType = "application/ms-word";
                else if (sType == ".docx")
                    Response.ContentType = "application/vnd.ms-word.document";
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
    protected void HlnkloadFile_Click_1(object sender, EventArgs e)
    {
        click_link_1();
    }
    protected void HlnkloadFile_Click_2(object sender, EventArgs e)
    {
        click_link_2();
    }
    protected void HlnkloadFile_Click_3(object sender, EventArgs e)
    {
        click_link_3();
    }
    protected void HlnkloadFile_Click_4(object sender, EventArgs e)
    {
        click_link_4();
    }
    protected void HlnkloadFile_Click_5(object sender, EventArgs e)
    {
        click_link_5();
    }

    protected void lnkSAGHE_Click(object sender, EventArgs e)
    {
        showPanel(tabSAGHE);
    }
    protected void lnkMI_Click(object sender, EventArgs e)
    {
        showPanel(tabMI);
        mailbody1();
    }
    protected void lnkSAE_Click(object sender, EventArgs e)
    {
        showPanel(tabSAE);
        mailbodySAE();
    }


    protected void HlnkloadFile_Click_11(object sender, EventArgs e)
    {
        click_link_11();
    }
    protected void HlnkloadFile_Click_12(object sender, EventArgs e)
    {
        click_link_12();
    }
    protected void HlnkloadFile_Click_13(object sender, EventArgs e)
    {
        click_link_13();
    }
    protected void HlnkloadFile_Click_14(object sender, EventArgs e)
    {
        click_link_14();
    }
    protected void HlnkloadFile_Click_15(object sender, EventArgs e)
    {
        click_link_15();
    }


    protected void HlnkloadFile_Click_111(object sender, EventArgs e)
    {
        click_link_111();
    }
    protected void HlnkloadFile_Click_112(object sender, EventArgs e)
    {
        click_link_112();
    }
    protected void HlnkloadFile_Click_113(object sender, EventArgs e)
    {
        click_link_113();
    }
    protected void HlnkloadFile_Click_114(object sender, EventArgs e)
    {
        click_link_114();
    }
    protected void HlnkloadFile_Click_115(object sender, EventArgs e)
    {
        click_link_115();
    }





    private void click_link_11()
    {
        HttpPostedFile attFile = File_upload_11.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_11.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_11.PostedFile.FileName);
            File_upload_11.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_12()
    {
        HttpPostedFile attFile = File_upload_12.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_12.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_12.PostedFile.FileName);
            File_upload_12.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_13()
    {
        HttpPostedFile attFile = File_upload_13.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_13.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_13.PostedFile.FileName);
            File_upload_13.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_14()
    {
        HttpPostedFile attFile = File_upload_14.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_14.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_14.PostedFile.FileName);
            File_upload_14.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_15()
    {
        HttpPostedFile attFile = File_upload_15.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_15.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_15.PostedFile.FileName);
            File_upload_15.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }


    private void click_link_111()
    {
        HttpPostedFile attFile = File_upload_111.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_111.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_111.PostedFile.FileName);
            File_upload_111.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_112()
    {
        HttpPostedFile attFile = File_upload_112.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_112.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_112.PostedFile.FileName);
            File_upload_112.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_113()
    {
        HttpPostedFile attFile = File_upload_113.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_113.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_113.PostedFile.FileName);
            File_upload_113.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_114()
    {
        HttpPostedFile attFile = File_upload_114.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_114.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_114.PostedFile.FileName);
            File_upload_114.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_115()
    {
        HttpPostedFile attFile = File_upload_115.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_115.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_115.PostedFile.FileName);
            File_upload_115.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }


    protected void btnSubmit1_Click(object sender, ImageClickEventArgs e)
    {
        MailMessage mmsg = new MailMessage();
        SmtpClient smt = new SmtpClient();
        datasourceIBSQL dobj = new datasourceIBSQL();
        try
        {
            smt.Host = "203.193.157.116";
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,System.Security.Cryptography.X509Certificates.X509Certificate certificate,System.Security.Cryptography.X509Certificates.X509Chain chain,System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            mmsg = new MailMessage();
            if (!addmailcollection(lblToAddress1.Text.Trim().ToString(), mmsg.To))
            { alert("Your To Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblCCAddress1.Text.Trim().ToString(), mmsg.CC))
            { alert("Your CC Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblBCCAddress1.Text.Trim().ToString(), mmsg.Bcc))
            { alert("Your BCC Address is not Valid, Please check"); return; }
            mmsg.From = new MailAddress(lblFromAddress1.Text.ToString());
            mmsg.Subject = lblSubject1.Text.Trim().ToString();
            mmsg.Body = lblBody1.InnerText.Trim().ToString();
            mmsg.IsBodyHtml = true;
            string strFileName = "";

            //string uqry = "UPDATE Article_dp SET SREP_Email_Flag=1,SREP_Sent_By=" + Convert.ToInt32(Session["employeeid"]) + ",SREP_Sent_On='" + DateTime.Now + "'  WHERE ano =" + Convert.ToInt32(Session["jobid"]);
            //dobj.ExcuteProc(uqry);

            if (File_upload_11.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_11.PostedFile;
                int attachFileLength = attFile.ContentLength;


                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_11.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_11.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);
                }

            }
            if (File_upload_12.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_12.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_12.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_12.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }
            if (File_upload_13.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_13.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_13.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_13.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }
            if (File_upload_14.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_14.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_14.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_14.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }
            if (File_upload_15.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_15.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_15.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_15.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }

            smt.Send(mmsg);
            alert("Mailsent Successfully");


        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mmsg = null; smt = null; authornamestatus = false; }
    }


    protected void btnSubmitSAE_Click(object sender, ImageClickEventArgs e)
    {
        MailMessage mmsg = new MailMessage();
        SmtpClient smt = new SmtpClient();
        datasourceIBSQL dobj = new datasourceIBSQL();
        try
        {
            smt.Host = "203.193.157.116";
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,System.Security.Cryptography.X509Certificates.X509Certificate certificate,System.Security.Cryptography.X509Certificates.X509Chain chain,System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            mmsg = new MailMessage();
            if (!addmailcollection(lblToAddressSAE.Text.Trim().ToString(), mmsg.To))
            { alert("Your To Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblCCAddressSAE.Text.Trim().ToString(), mmsg.CC))
            { alert("Your CC Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblBCCAddressSAE.Text.Trim().ToString(), mmsg.Bcc))
            { alert("Your BCC Address is not Valid, Please check"); return; }
            mmsg.From = new MailAddress(lblFromAddressSAE.Text.ToString());
            mmsg.Subject = lblSubjectSAE.Text.Trim().ToString();
            mmsg.Body = lblBodySAE.InnerText.Trim().ToString();
            mmsg.IsBodyHtml = true;
            string strFileName = "";

            //string uqry = "UPDATE Article_dp SET SREP_Email_Flag=1,SREP_Sent_By=" + Convert.ToInt32(Session["employeeid"]) + ",SREP_Sent_On='" + DateTime.Now + "'  WHERE ano =" + Convert.ToInt32(Session["jobid"]);
            //dobj.ExcuteProc(uqry);

            if (File_upload_111.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_111.PostedFile;
                int attachFileLength = attFile.ContentLength;


                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_111.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_111.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);
                }

            }
            if (File_upload_112.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_112.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_112.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_112.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }
            if (File_upload_113.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_113.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_113.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_113.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }
            if (File_upload_114.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_114.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_114.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_114.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }
            if (File_upload_115.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_115.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_115.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_115.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);

                }

            }

            smt.Send(mmsg);
            alert("Mailsent Successfully");


        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mmsg = null; smt = null; authornamestatus = false; }
    }

    private void click_link_116()
    {
        HttpPostedFile attFile = File_upload_116.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_116.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_116.PostedFile.FileName);
            File_upload_116.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_117()
    {
        HttpPostedFile attFile = File_upload_117.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_117.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_117.PostedFile.FileName);
            File_upload_117.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_118()
    {
        HttpPostedFile attFile = File_upload_118.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_118.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_118.PostedFile.FileName);
            File_upload_118.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_119()
    {
        HttpPostedFile attFile = File_upload_119.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_119.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_119.PostedFile.FileName);
            File_upload_119.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }

    private void click_link_120()
    {
        HttpPostedFile attFile = File_upload_120.PostedFile;


        if (!System.IO.Directory.Exists(strFileName_path))
        {
            System.IO.Directory.CreateDirectory(strFileName_path);
        }
        if (System.IO.Path.GetFileName(File_upload_120.PostedFile.FileName) != "")
        {
            strFileName_path = strFileName_path + System.IO.Path.GetFileName(File_upload_120.PostedFile.FileName);
            File_upload_120.SaveAs(strFileName_path);
            LinkButton lnkbtn = new LinkButton();
            lnkbtn.Text = strFileName_path;
            lnkbtn = AttachmentLinkAttributes(lnkbtn, strFileName_path);
            lnkbtn.Click += new EventHandler(Attachment_View_Click);
        }

    }
    protected void HlnkloadFile_Click_116(object sender, EventArgs e)
    {
        click_link_116();
    }
    protected void HlnkloadFile_Click_117(object sender, EventArgs e)
    {
        click_link_117();
    }
    protected void HlnkloadFile_Click_118(object sender, EventArgs e)
    {
        click_link_118();
    }
    protected void HlnkloadFile_Click_119(object sender, EventArgs e)
    {
        click_link_119();
    }
    protected void HlnkloadFile_Click_120(object sender, EventArgs e)
    {
        click_link_120();
    }
    protected void btnSubmitSAM_Click(object sender, ImageClickEventArgs e)
    {
        MailMessage mmsg = new MailMessage();
        SmtpClient smt = new SmtpClient();
        datasourceIBSQL dobj = new datasourceIBSQL();
        try
        {
            smt.Host = "203.193.157.116";
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,System.Security.Cryptography.X509Certificates.X509Certificate certificate,System.Security.Cryptography.X509Certificates.X509Chain chain,System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            mmsg = new MailMessage();
            if (!addmailcollection(lblToAddressSAM.Text.Trim().ToString(), mmsg.To))
            { alert("Your To Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblCCAddressSAM.Text.Trim().ToString(), mmsg.CC))
            { alert("Your CC Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblBCCAddressSAM.Text.Trim().ToString(), mmsg.Bcc))
            { alert("Your BCC Address is not Valid, Please check"); return; }
            mmsg.From = new MailAddress(lblFromAddressSAM.Text.ToString());
            mmsg.Subject = lblSubjectSAM.Text.Trim().ToString();
            mmsg.Body = lblBodySAM.InnerText.Trim().ToString();
            mmsg.IsBodyHtml = true;
            string strFileName = "";

            //string uqry = "UPDATE Article_dp SET SREP_Email_Flag=1,SREP_Sent_By=" + Convert.ToInt32(Session["employeeid"]) + ",SREP_Sent_On='" + DateTime.Now + "'  WHERE ano =" + Convert.ToInt32(Session["jobid"]);
            //dobj.ExcuteProc(uqry);

            if (File_upload_116.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_116.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_116.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_116.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);
                }
            }
            if (File_upload_117.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_117.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_117.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_117.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);
                }
            }
            if (File_upload_118.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_118.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_118.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_118.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);
                }
            }
            if (File_upload_119.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_119.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_119.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_119.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);
                }
            }
            if (File_upload_120.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_120.PostedFile;
                int attachFileLength = attFile.ContentLength;

                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_120.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_120.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg.Attachments.Add(attach);
                }
            }

            smt.Send(mmsg);
            alert("Mailsent Successfully");
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mmsg = null; smt = null; authornamestatus = false; }
    }
    protected void lnkSAM_Click(object sender, EventArgs e)
    {
        showPanel(tabSAM);
        mailbodySAM();
    }
    public void mailbodySAM()
    {
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please find attached the formatted page proofs for your chapter that we plan to publish in an upcoming issue of Scientific American Medicine. Decker Intellectual Properties is working to update the look of the chapters.  Accordingly, we have added color to existing black and white images and to the tables.  Please review the material in the author package first and follow the instructions carefully. Review all text for accuracy ensuring that figures and tables are placed correctly, table data are aligned correctly, and legends correspond with the figures.  Note that if your drawings have been rerendered or have had color added to them, please check them to ensure that they are accurate. If you have any serious reservations about the use of color or how the color was applied, please indicate so clearly. Note that we CANNOT accept any changes to the text and references unless they are corrections to outright factual errors as they will delay the production of your chapter and result in cost implications to the book. At this stage, we cannot guarantee that all corrections you suggest to the text will be applied, unless they were oversights on the part of Decker Intellectual Properties.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Please note the deadline for returning materials is 3 business days. <b>If you have any difficulty meeting this deadline, please contact Emily Hayes at ehayes@deckerip.com to see if it is possible to arrange another date</b>. Otherwise, please return the materials to our associate publisher Charlesworth.");

        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Sincerely, <br/>");
        MyStringBuilder.Append("Decker Proofing<br/>");
        MyStringBuilder.Append("Charlesworth India<br/>");
        MyStringBuilder.Append("email: project.manager@charlesworth-group.in<br/>");
        MyStringBuilder.Append("Fax: 0044 1484 536032<br/>");
        MyStringBuilder.Append("Phone: 044 42267215<br/>");

        lblBody.InnerText = MyStringBuilder.ToString();
    }
    protected void dropJournals_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCCAddress.Text = "";
        lblBCCAddress.Text = "";
        if (dropJournals.SelectedValue == "SAGHE")
        {
            mailbody();
            lblDeckerSAGHE.Text = dropJournals.SelectedValue;
        }
        else if (dropJournals.SelectedValue == "MI")
        {
            mailbody1();
            lblDeckerSAGHE.Text = dropJournals.SelectedValue;
        }
        else if (dropJournals.SelectedValue == "SAE")
        {
            mailbodySAE();
            lblDeckerSAGHE.Text = dropJournals.SelectedValue;
        }
        else if (dropJournals.SelectedValue == "SAM")
        {
            mailbodySAM();
            lblDeckerSAGHE.Text = dropJournals.SelectedValue;
        }
        else if (dropJournals.SelectedValue == "SAN")
        {
            mailbodySAN();
            lblDeckerSAGHE.Text = dropJournals.SelectedValue;
        }
        else if (dropJournals.SelectedValue == "SAS")
        {
            mailbodySAS();
            lblDeckerSAGHE.Text = dropJournals.SelectedValue;
        }
        else if (dropJournals.SelectedValue == "SAVS")
        {
            mailbodySAVS();
            lblDeckerSAGHE.Text = dropJournals.SelectedValue;
        }
        else if (dropJournals.SelectedValue == "MGEN")
        {
            mailbodyMGEN();
            lblDeckerSAGHE.Text = dropJournals.SelectedValue;
            lblCCAddress.Text = "cwgsgm@charlesworth-group.com;";
            lblBCCAddress.Text = "SGMPublishing-production@sgm.ac.uk;";
        }
        else if (dropJournals.SelectedValue == "MGEN With Images")
        {
            mailbodyMGENWithImages();
            lblDeckerSAGHE.Text = dropJournals.SelectedValue;
            lblCCAddress.Text = "cwgsgm@charlesworth-group.com;";
            lblBCCAddress.Text = "SGMPublishing-production@sgm.ac.uk;";
        }
        else if (dropJournals.SelectedValue == "JMMCR")
        {
            mailbodyJMMCR();
            lblDeckerSAGHE.Text = dropJournals.SelectedValue;
            lblCCAddress.Text = "cwgsgm@charlesworth-group.com;";
            lblBCCAddress.Text = "SGMPublishing-production@sgm.ac.uk;";
        }
        else if (dropJournals.SelectedValue == "JMMCR With Images")
        {
            mailbodyJMMCRWithImages();
            lblDeckerSAGHE.Text = dropJournals.SelectedValue;
            lblCCAddress.Text = "cwgsgm@charlesworth-group.com;";
            lblBCCAddress.Text = "SGMPublishing-production@sgm.ac.uk;";
        }
    }
    public void mailbodySAN()
    {
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please find attached the formatted page proofs for your chapter that we plan to publish in an upcoming issue of Scientific American Neurology. Decker Intellectual Properties is working to update the look of the chapters. Accordingly, we have added color to existing black and white images and to the tables. Please review the material in the author package first and follow the instructions carefully. Review all text for accuracy ensuring that figures and tables are placed correctly, table data are aligned correctly, and legends correspond with the figures. Note that if your drawings have been rerendered or have had color added to them, please check them to ensure that they are accurate. If you have any serious reservations about the use of color or how the color was applied, please indicate so clearly. Note that we CANNOT accept any changes to the text and references unless they are corrections to outright factual errors as they will delay the production of your chapter and res  ult in cost implications to the book. At this stage, we cannot guarantee that all corrections you suggest to the text will be applied, unless they were oversights on the part of Decker Intellectual Properties.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Please note the deadline for returning materials is 3 business days. If you have any difficulty meeting this deadline, please contact Kim Elliott at kelliott@DeckerIP.com to see if it is possible to arrange another date. Otherwise, please return the materials to our associate publisher Charlesworth.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Sincerely, <br/>");
        MyStringBuilder.Append("Decker Proofing<br/>");
        MyStringBuilder.Append("Charlesworth India<br/>");
        MyStringBuilder.Append("email: project.manager@charlesworth-group.in<br/>");
        MyStringBuilder.Append("Fax: 0044 1484 536032<br/>");
        MyStringBuilder.Append("Phone: 044 42267215<br/>");

        lblBody.InnerText = MyStringBuilder.ToString();
    }
    public void mailbodySAS()
    {
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please find attached the formatted page proofs for your chapter that we plan to publish in an upcoming issue of ACS Surgery/Scientific American Surgery. Please review the material in the author package first and follow the instructions carefully. Review all text for accuracy ensuring that figures and tables are placed correctly, table data are aligned correctly, and legends correspond with the figures.  Note that we CANNOT accept any changes to the text and references unless they are corrections to outright factual errors, as they will delay the production of your chapter and result in cost implications to the book. At this stage, we cannot guarantee that all corrections you suggest to the text will be applied, unless they were oversights on the part of Decker Intellectual Properties.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Please note the deadline for returning materials is 3 business days. If you have any difficulty meeting this deadline, please contact Ben Shragge at bshragge@deckerip.com to see if it is possible to arrange another date. Otherwise, please return the materials to our associate publisher Charlesworth.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Sincerely, <br/>");
        MyStringBuilder.Append("Decker Proofing<br/>");
        MyStringBuilder.Append("Charlesworth India<br/>");
        MyStringBuilder.Append("email: project.manager@charlesworth-group.in<br/>");
        MyStringBuilder.Append("Fax: 0044 1484 536032<br/>");
        MyStringBuilder.Append("Phone: 044 42267215<br/>");

        lblBody.InnerText = MyStringBuilder.ToString();
    }
    public void mailbodySAVS()
    {
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please find attached the formatted page proofs for your chapter that we plan to publish in an upcoming issue of Scientific American Vascular Surgery. Please review the material in the author package first and follow the instructions carefully. Review all text for accuracy ensuring that figures and tables are placed correctly, table data are aligned correctly, and legends correspond with the figures. Note that we CANNOT accept any changes to the text and references unless they are corrections to outright factual errors, as they will delay the production of your chapter and result in cost implications to the book. At this stage, we cannot guarantee that all corrections you suggest to the text will be applied, unless they were oversights on the part of Decker Intellectual Properties.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Please note the deadline for returning materials is 3 business days. If you have any difficulty meeting this deadline, please contact Ben Shragge at bshragge@deckerip.com to see if it is possible to arrange another date. Otherwise, please return the materials to our associate publisher Charlesworth.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Sincerely, <br/>");
        MyStringBuilder.Append("Decker Proofing<br/>");
        MyStringBuilder.Append("Charlesworth India<br/>");
        MyStringBuilder.Append("email: project.manager@charlesworth-group.in<br/>");
        MyStringBuilder.Append("Fax: 0044 1484 536032<br/>");
        MyStringBuilder.Append("Phone: 044 42267215<br/>");

        lblBody.InnerText = MyStringBuilder.ToString();
    }
    public void mailbodyMGEN()
    {
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Re <Insert article title here – available in PDF/metadata>");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("The proofs for your paper for Microbial Genomics are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please return any required corrections and answers to editorial queries within 2 days of receipt of this message to cwgsgm@charlesworth-group.com.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please mark your corrections and add comments to the PDF by following the instructions attached. If you are unable to make the corrections directly on the PDF, then please list corrections and answers to editorial queries clearly in the body of your email. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Only typographical and absolutely essential factual corrections will be accepted at this stage. ");
        MyStringBuilder.Append("Stylistic changes that are not in line with SGM house style will not be made, and neither will changes relating to large portions of text. You may be charged for correction of your non-typographical errors. This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("When your paper is ready for publication, you will receive an email with details of how to access the final PDF of your manuscript and how to order reprints using the SGM Reprint Service.");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Sincerely, <br/>");
        MyStringBuilder.Append("Decker Proofing<br/>");
        MyStringBuilder.Append("Charlesworth India<br/>");
        MyStringBuilder.Append("email: project.manager@charlesworth-group.in<br/>");
        MyStringBuilder.Append("Fax: 0044 1484 536032<br/>");
        MyStringBuilder.Append("Phone: 044 42267215<br/>");

        lblBody.InnerText = MyStringBuilder.ToString();
    }

    public void mailbodyMGENWithImages()
    {
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Re <Insert article title here – available in PDF/metadata>");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("The proofs for your paper for Microbial Genomics are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please return any required corrections and answers to editorial queries within 2 days of receipt of this message to sgmprod@charlesworth-group.com.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please mark your corrections and add comments to the PDF by following the instructions below. If you are unable to make the corrections directly on the PDF, then please list corrections and answers to editorial queries clearly in the body of your email. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Only typographical and absolutely essential factual corrections will be accepted at this stage. Stylistic changes that are not in line with SGM house style will not be made, and neither will changes relating to large portions of text. You may be charged for correction of your non-typographical errors. This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("When your paper is ready for publication, you will receive an email with details of how to access the final PDF of your manuscript and how to order reprints using the SGM Reprint Service.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Using Adobe Mark-up Tools");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To mark up corrections using Adobe Reader (it can be downloaded for free if you don't have it), you just need to make sure the correct toolbars on the PDF are activated. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please mark up the text using the tools in the 'Comment and mark ups' toolbar. This includes tools such as a sticky note, text edits tools, arrows and boxes. To make this appear, click on 'Comments' at the top of the PDF, then select 'Show Comment & Markup Toolbar'. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("<img src=\"images/MGEN.jpg\"></img>");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("If you can't get the mark-up tools to work, there's possibility that the version of the document that we sent you has mistakenly not been activated to permit editing. In that case please get in touch immediately and we'll send you over a replacement file, with editing enabled.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please note that in order to make sure the copy-editors and typesetters understand your corrections, all corrections should be very clear and all instructions unambiguous. In order to assure this, we ask you to mark up corrections using the methods listed below.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Specific Text Changes");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("For specific changes to the text, i.e. deletions, insertions and replacements, please use the 'Text edits' tools as much as possible (you can select these from the Comments & Mark-up toolbar). These are much more accurate than sticky notes. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To insert text, you need to click at the exact place you want the text to be inserted, then start typing. The text will then appear in a window so you can see what you're typing.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To replace text, highlight the text to be replaced, then start typing (again, the text will appear in a window as you type). When you insert or amend a character the mark on the page will be a very small blue or red marking.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To add a note highlight the text in question and right-click on your mouse. In the drop-down box select \"Add sticky note\" and type your note in the box which appears.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To format the text you've inserted or replaced (i.e. change it to bold or italics, or back to Roman) you should highlight the text and add a sticky note as above, specifying whether it should be changed to bold, italic etc.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To delete text, highlight the text for deletion and press delete. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please make sure that all of your mark ups are given as instructions for the typesetter. If you do have any queries/issues you'd like the editorial team to look at, please put these in a sticky note and begin with 'NOTE TO EDITOR'.");

        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Sincerely, <br/>");
        MyStringBuilder.Append("Decker Proofing<br/>");
        MyStringBuilder.Append("Charlesworth India<br/>");
        MyStringBuilder.Append("email: project.manager@charlesworth-group.in<br/>");
        MyStringBuilder.Append("Fax: 0044 1484 536032<br/>");
        MyStringBuilder.Append("Phone: 044 42267215<br/>");

        lblBody.InnerText = MyStringBuilder.ToString();
    }

    public void mailbodyJMMCR()
    {
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Re <Insert article title here – available in PDF/metadata>");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("The proofs for your paper for JMM Case Reports are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please return any required corrections and answers to editorial queries within 2 days of receipt of this message to cwgsgm@charlesworth-group.com.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please mark your corrections and add comments to the PDF by following the instructions attached. If you are unable to make the corrections directly on the PDF, then please list corrections and answers to editorial queries clearly in the body of your email. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Only typographical and absolutely essential factual corrections will be accepted at this stage. ");
        MyStringBuilder.Append("Stylistic changes that are not in line with SGM house style will not be made, and neither will changes relating to large portions of text. You may be charged for correction of your non-typographical errors. This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("When your paper is ready for publication, you will receive an email with details of how to access the final PDF of your manuscript and how to order reprints using the SGM Reprint Service.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("<br/><br/>");

        MyStringBuilder.Append("Sincerely, <br/>");
        MyStringBuilder.Append("Decker Proofing<br/>");
        MyStringBuilder.Append("Charlesworth India<br/>");
        MyStringBuilder.Append("email: project.manager@charlesworth-group.in<br/>");
        MyStringBuilder.Append("Fax: 0044 1484 536032<br/>");
        MyStringBuilder.Append("Phone: 044 42267215<br/>");

        lblBody.InnerText = MyStringBuilder.ToString();
    }

    public void mailbodyJMMCRWithImages()
    {
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Re <Insert article title here – available in PDF/metadata>");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("The proofs for your paper for JMM Case Reports are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please return any required corrections and answers to editorial queries within 2 days of receipt of this message to sgmprod@charlesworth-group.com.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please mark your corrections and add comments to the PDF by following the instructions below. If you are unable to make the corrections directly on the PDF, then please list corrections and answers to editorial queries clearly in the body of your email. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Only typographical and absolutely essential factual corrections will be accepted at this stage. Stylistic changes that are not in line with SGM house style will not be made, and neither will changes relating to large portions of text. You may be charged for correction of your non-typographical errors. This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("When your paper is ready for publication, you will receive an email with details of how to access the final PDF of your manuscript and how to order reprints using the SGM Reprint Service.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Using Adobe Mark-up Tools");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To mark up corrections using Adobe Reader (it can be downloaded for free if you don't have it), you just need to make sure the correct toolbars on the PDF are activated. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please mark up the text using the tools in the 'Comment and mark ups' toolbar. This includes tools such as a sticky note, text edits tools, arrows and boxes. To make this appear, click on 'Comments' at the top of the PDF, then select 'Show Comment & Markup Toolbar'. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("<img src=\"images/MGEN.jpg\"></img>");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("If you can't get the mark-up tools to work, there's possibility that the version of the document that we sent you has mistakenly not been activated to permit editing. In that case please get in touch immediately and we'll send you over a replacement file, with editing enabled.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please note that in order to make sure the copy-editors and typesetters understand your corrections, all corrections should be very clear and all instructions unambiguous. In order to assure this, we ask you to mark up corrections using the methods listed below.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Specific Text Changes");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("For specific changes to the text, i.e. deletions, insertions and replacements, please use the 'Text edits' tools as much as possible (you can select these from the Comments & Mark-up toolbar). These are much more accurate than sticky notes. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To insert text, you need to click at the exact place you want the text to be inserted, then start typing. The text will then appear in a window so you can see what you're typing.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To replace text, highlight the text to be replaced, then start typing (again, the text will appear in a window as you type). When you insert or amend a character the mark on the page will be a very small blue or red marking.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To add a note highlight the text in question and right-click on your mouse. In the drop-down box select \"Add sticky note\" and type your note in the box which appears.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To format the text you've inserted or replaced (i.e. change it to bold or italics, or back to Roman) you should highlight the text and add a sticky note as above, specifying whether it should be changed to bold, italic etc.");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To delete text, highlight the text for deletion and press delete. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Please make sure that all of your mark ups are given as instructions for the typesetter. If you do have any queries/issues you'd like the editorial team to look at, please put these in a sticky note and begin with 'NOTE TO EDITOR'.");

        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Sincerely, <br/>");
        MyStringBuilder.Append("Decker Proofing<br/>");
        MyStringBuilder.Append("Charlesworth India<br/>");
        MyStringBuilder.Append("email: project.manager@charlesworth-group.in<br/>");
        MyStringBuilder.Append("Fax: 0044 1484 536032<br/>");
        MyStringBuilder.Append("Phone: 044 42267215<br/>");

        lblBody.InnerText = MyStringBuilder.ToString();
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class IJAM_Emailer : System.Web.UI.Page
{
    bool linkstatus = false;
    string pdfName,pdfName1 = "";
    string strFileName_path = ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            showPanel(tabeProofs);
            if (linkstatus == false )
            {
                alert("Please Enter Article Id before sent mail !");
            }
            else
            {
               
                mailbody();
                lblSubject.Text = "IJAM Proof";
            }
        }
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        MailMessage mmsg = new MailMessage();
        SmtpClient smt = new SmtpClient();
        datasourceIBSQL dobj = new datasourceIBSQL();
        try
        {
            smt.Host = "192.168.2.1";
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
            smt.Send(mmsg);
            alert("Mailsent Successfully");
            string strFileName = "";

        }
        catch(Exception ex)
        {
            alert(ex.ToString());
        }

    }
    public void mailbody()
    {
        pdfName = txtArticleId.Text.Trim();
        string strUrl = "ftp://ijam_author_proof:acfijam@ftp.charlesworth-group.com/" + pdfName + ".pdf";
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Dear Author<br/>");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("International Journal of Agricultural Management <br/><br/>");
        MyStringBuilder.Append("Your edited and typeset article is now available for your review as an electronic proof (eProof), which is provided as a PDF file - for further information on viewing PDFs please see the notes (below).  ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("To retrieve your eProof, please click on the following link: ");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<a href=" + strUrl.ToString() + " >" + strUrl + "</a>");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Proofing your article:");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Please read the manuscript carefully, checking for accuracy, verifying the reference order, and double checking figures and tables. The enclosed eProof is not to be regarded as an opportunity to alter, amend, or revise your paper; it is intended to be for correction purposes only.  <br/><br/>");
        MyStringBuilder.Append("Changes that do not require a copy of the proof can be sent by email (please be as specific as possible) to: IJAM@charlesworth-group.com. <br/><br/> ");
        MyStringBuilder.Append("If you are happy with the proof as it stands, please email IJAM@charlesworth-group.com to confirm this. <br/><br/>");
        MyStringBuilder.Append("In the event there are substantive changes, we may ask the author to incur additional editing and typesetting charges.<br/><br/> ");
        MyStringBuilder.Append("If you have any problems accessing your eProof please refer to the notes below. If your question is not answered there, please email:");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("IJAM@charlesworth-group.com.   ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Thank you in advance for your prompt attention and cooperation during this phase of preparing your article for publication. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Kind regards, <br/><br/>");
        MyStringBuilder.Append("The Charlesworth Group<br/>");
        MyStringBuilder.Append("On behalf of the International Journal of Agricultural Management<br/><br/><br/>");
        MyStringBuilder.Append("Notes: <br/>");
        MyStringBuilder.Append("<ol>    <li> To view and print the PDF of your article you must have Adobe Acrobat Reader software installed on your computer. This program is freely available and can be downloaded from http://get.adobe.com/uk/reader/ <li>The PDF file enclosed is typeset to look like the final print pages. <br>However, all images are lower resolution to reduce the PDF's file size. High resolution files, which give sharper images, will be used in final versions of the article.  </li><li>Please note that while this is a typeset page, the layout of your article may be slightly different in the final journal. </li> </ol>");
        MyStringBuilder.Append("<br/><br/>");
       

        lblBody.InnerText = MyStringBuilder.ToString();

        lblCCAddress.Text = "editor.ijam@gmail.com; IJAM@charlesworth-group.com;Janine.Tinker-Ives@charlesworth-group.com";
        lblBCCAddress.Text = "project.manager@charlesworth-group.in";
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
    protected void btnGenLink_Click(object sender, EventArgs e)
    {
        if(txtArticleId.Text=="")
        {
            alert("Article Id Should not empty");
            lblBody.InnerText = "";
        }
        else
        {
            mailbody();
            lblSubject.Text = "IJAM Proof";
            string surl = "ftp://ijam_author_proof:acfijam@ftp.charlesworth-group.com/" + pdfName + ".pdf";
            linkstatus = true;
            HyperLink1.Text = surl.ToString().Replace("\\", "//");
            HyperLink1.NavigateUrl = surl.ToString().Replace("\\", "//");
        }
    }
    protected void lnkeProof_Click(object sender, EventArgs e)
    {
        showPanel(tabeProofs);
    }
   
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabeProofs":
                mieProof.Attributes.Add("class", "current");
             
                this.tabeProofs.Visible = true;
              
                break;
            
            
            default:
                mieProof.Attributes.Add("class", "current");
               
                this.tabeProofs.Visible = true;
               
                break;
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
   
  
}
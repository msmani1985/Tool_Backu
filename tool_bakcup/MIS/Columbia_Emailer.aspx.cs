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

public partial class Columbia_Emailer : System.Web.UI.Page
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
                lblSubject.Text = "Proof for Tremor and Other Hyperkinetic Movements";
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
            smt.Host = "mail.nimbox.co.uk";
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
        //pdfName = txtArticleId.Text.Trim();
        //string strUrl = "ftp://c0lumb1a:charlesw0rth@60.10.59.134/" + pdfName + ".pdf";
        //StringBuilder MyStringBuilder = new StringBuilder();
        //MyStringBuilder.Append("Tremor and Other Hyperkinetic Movements eProof system");
        //MyStringBuilder.Append("<br/><br/>");
        //MyStringBuilder.Append("PLEASE RETURN PROOF CORRECTIONS WITHIN 48 HOURS OF RECEIPT TO: <br/>");
        //MyStringBuilder.Append("Email: columbia@charlesworth-group.com<br/><br/>");
        //MyStringBuilder.Append("Dear XXXX<br/>");
        //MyStringBuilder.Append("<br/><br/>");
        //MyStringBuilder.Append("Your edited and typeset article is now available for your review as an electronic proof (eProof), which is provided as a PDF file - for further information on viewing PDFs please see the notes (below). <br/><br/>");
        //MyStringBuilder.Append("Retrieving your proof: Click on the following link to download your eProof: ");
        //MyStringBuilder.Append("<br/><br/>");
        //MyStringBuilder.Append("<a href=" + strUrl.ToString() + " >" + strUrl + "</a>");
        //MyStringBuilder.Append("<br/><br/>");
        //MyStringBuilder.Append("Proofing your article: Please read the page proof carefully, checking for accuracy, verifying the reference order, responding to any author queries in the eProof, and <br/>");
        //MyStringBuilder.Append("double-checking figures and tables. When reviewing your page proofs please keep in mind that a professional copyeditor edited your manuscript to comply with the style <br/>");
        //MyStringBuilder.Append("requirements of the journal. The enclosed eProof is not to be regarded as an opportunity to alter, amend, or revise your paper; it is intended to be for correction purposes only. <br/><br/>");
        //MyStringBuilder.Append("If you are happy with the proof as it stands, please email to confirm this. Changes that do not require a copy of the proof can be sent by email (please be as specific as<br/>");
        //MyStringBuilder.Append("possible). Failure to return the eProof within 48 hours may result in significant delays in the publication of your manuscript. If you do not respond within 96 hours, your <br/>");
        //MyStringBuilder.Append("eProofs will be considered approved and passed on to the journal Editor-in-Chief for final approval.<br/> ");
        //MyStringBuilder.Append("Email: columbia@charlesworth-group.com ");
        //MyStringBuilder.Append("<br/><br/>");
        //MyStringBuilder.Append("Reprints: If you wish to purchase reprints of your article, please contact info@cdrs.columbia.edu.  ");
        //MyStringBuilder.Append("<br/><br/>");
        //MyStringBuilder.Append("Thank you in advance for your prompt attention and co-operation during this phase of preparing your article for publication. ");
        //MyStringBuilder.Append("<br/><br/>");
        //MyStringBuilder.Append("Kind regards,<br/>");
        //MyStringBuilder.Append("Tremor and Other Hyperkinetic Movements<br/><br/>");
        //MyStringBuilder.Append("If you have any problems accessing your eProof please refer to the notes below. If your question is not answered there, please email: info@cdrs.columbia.edu<br/><br/>");
        //MyStringBuilder.Append("Notes: <br/>");
        //MyStringBuilder.Append("<ol>    <li> To view and print the PDF of your article you must have Adobe Acrobat Reader software installed on your computer. This program is freely available and can be downloaded (see instructions below).     <li> The PDF file enclosed is typeset to look like the final print pages. However, all images are lower resolution to reduce the PDF's file size. High resolution files, which give sharper images, will be used in the printed article.     <li>Please note that while this is a typeset page, the layout of your article may be slightly different in the final journal.  </ol>");
        //MyStringBuilder.Append("<br/><br/>");
        //MyStringBuilder.Append("How to install Adobe Acrobat Reader<br/>");
        //MyStringBuilder.Append("The Adobe Acrobat Reader is the free viewing companion to Adobe Acrobat software and allows you to view, navigate and print PDF files. To download the latest version of the <br/>");
        //MyStringBuilder.Append("Acrobat Reader:  <br/>");
        //MyStringBuilder.Append("<ol>    <li> Use your browser to go directly to the address http://www.adobe.com/products/acrobat/readstep2.html     <li> Review the selection options carefully to match the proper Acrobat Reader version to your computers configuration.      <li>All PDF files produced for the eProof System are Acrobat version 4.x files. Do not use older versions of Acrobat Reader. <li>Troubleshooting PDF Viewing Problems:<ol><li type='disc'> Acrobat Reader comes with a help file. Please review the READER.PDF file located in your Acrobat/Help directory  <li  type='disc'>If the Acrobat Reader help file does not provide you a solution, try the Adobe Support Database at </br> http://www.adobe.com/support/products/acrreader.html  <li  type='disc'>Additional information about Acrobat Reader can be found at </br> http://www.adobe.com/products/reader </ol>   </ol>");

        pdfName = txtArticleId.Text.Trim();
        string strUrl = "ftp://columbia:176cup731@ftp.charlesworth-group.com/From_Charlesworth/5_Author_Proofs/" + pdfName + ".pdf";
        StringBuilder MyStringBuilder = new StringBuilder();
        MyStringBuilder.Append("Tremor and Other Hyperkinetic Movements eProof system");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("PLEASE RETURN PROOF CORRECTIONS WITHIN 48 HOURS OF RECEIPT TO: <br/>");
        MyStringBuilder.Append("Email: columbia@charlesworth-group.com<br/><br/>");
        MyStringBuilder.Append("Dear XXXX<br/>");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Your edited and typeset article is now available for your review as an electronic proof (eProof), which is provided as a PDF file - for further information on viewing PDFs please see the notes (below). <br/><br/>");
        MyStringBuilder.Append("Retrieving your proof: Click on the following link to download your eProof: ");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("<a href=" + strUrl.ToString() + " >" + strUrl + "</a>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("(Note: You can copy/paste the URL to access your PDF proof if the link doesn't work) When your article is published, it will be posted on our website (http://www.tremorjournal.org/).");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Proofing your article: Please read the page proof carefully, checking for accuracy, verifying the reference order, responding to any author queries in the eProof, and ");
        MyStringBuilder.Append("double-checking figures and tables. When reviewing your page proofs please keep in mind that a professional copyeditor edited your manuscript to comply with the style ");
        MyStringBuilder.Append("requirements of the journal. The enclosed eProof is not to be regarded as an opportunity to alter, amend, or revise your paper; it is intended to be for correction purposes only. <br/><br/>");
        MyStringBuilder.Append("If you are happy with the proof as it stands, please email to confirm this. Changes that do not require a copy of the proof can be sent by email (please be as specific as ");
        MyStringBuilder.Append("possible). Failure to return the eProof within 48 hours may result in significant delays in the publication of your manuscript. If you do not respond within 96 hours, your ");
        MyStringBuilder.Append("eProofs will be considered approved and passed on to the journal Editor-in-Chief for final approval. ");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("Email: columbia@charlesworth-group.com ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Reprints: If you wish to purchase reprints of your article, please contact info@cdrs.columbia.edu.  ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Thank you in advance for your prompt attention and co-operation during this phase of preparing your article for publication. ");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("Kind regards,<br/>");
        MyStringBuilder.Append("Tremor and Other Hyperkinetic Movements<br/><br/>");
        MyStringBuilder.Append("If you have any problems accessing your eProof please refer to the notes below. If your question is not answered there, please email: info@cdrs.columbia.edu<br/><br/>");
        MyStringBuilder.Append("Notes:");
        MyStringBuilder.Append("<ol>    <li> To view and print the PDF of your article you must have Adobe Acrobat Reader software installed on your computer. This program is freely available and can be downloaded (see instructions below).     <li> The PDF file enclosed is typeset to look like the final print pages. However, all images are lower resolution to reduce the PDF's file size. High resolution files, which give sharper images, will be used in the printed article.     <li>Please note that while this is a typeset page, the layout of your article may be slightly different in the final journal.  </ol>");
        MyStringBuilder.Append("<br/>");
        MyStringBuilder.Append("How to install Adobe Acrobat Reader<br/>");
        MyStringBuilder.Append("The Adobe Acrobat Reader is the free viewing companion to Adobe Acrobat software and allows you to view, navigate and print PDF files. To download the latest version of the <br/>");
        MyStringBuilder.Append("Acrobat Reader:");
        MyStringBuilder.Append("<ol>    <li> Use your browser to go directly to the address http://www.adobe.com/products/acrobat/readstep2.html     <li> Review the selection options carefully to match the proper Acrobat Reader version to your computers configuration.      <li>All PDF files produced for the eProof System are Acrobat version 4.x files. Do not use older versions of Acrobat Reader. <li>Troubleshooting PDF Viewing Problems:<ol><li type='disc'> Acrobat Reader comes with a help file. Please review the READER.PDF file located in your Acrobat/Help directory  <li  type='disc'>If the Acrobat Reader help file does not provide you a solution, try the Adobe Support Database at </br> http://www.adobe.com/support/products/acrreader.html  <li  type='disc'>Additional information about Acrobat Reader can be found at </br> http://www.adobe.com/products/reader </ol>   </ol>");

        lblBody.InnerText = MyStringBuilder.ToString();

        lblCCAddress.Text = "ko2336@columbia.edu;tremor.admin@libraries.cul.columbia.edu";
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
            lblSubject.Text = "Proof for Tremor and Other Hyperkinetic Movements";
           // string surl = "ftp://c0lumb1a:charlesw0rth@60.10.59.134/" + pdfName + ".pdf";
            string surl = "ftp://columbia:176cup731@ftp.charlesworth-group.com/From_Charlesworth/5_Author_Proofs/" + pdfName + ".pdf";
            linkstatus = true;
            HyperLink1.Text = surl.ToString().Replace("\\", "//");
            HyperLink1.NavigateUrl = surl.ToString().Replace("\\", "//");
        }
    }
    protected void lnkeProof_Click(object sender, EventArgs e)
    {
        showPanel(tabeProofs);
    }
    protected void lnkRemainder_Click(object sender, EventArgs e)
    {
        mailbody_1();
        txtSubject1.Text = "Proof for Tremor and Other Hyperkinetic Movements";
        showPanel(tabeProofsRemainder);
    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabeProofs":
                mieProof.Attributes.Add("class", "current");
                miRemainder.Attributes.Add("class", "");
                this.tabeProofs.Visible = true;
                this.tabeProofsRemainder.Visible = false;
                break;
            case "tabeProofsRemainder":
                mieProof.Attributes.Add("class", "");
                miRemainder.Attributes.Add("class", "current");
                this.tabeProofs.Visible = false;
                this.tabeProofsRemainder.Visible = true;
                break;
            
            default:
                mieProof.Attributes.Add("class", "current");
                miRemainder.Attributes.Add("class", "");
                this.tabeProofs.Visible = true;
                this.tabeProofsRemainder.Visible = false;
                break;
        }
    }

    protected void btnSubmit_Remainder_Click(object sender, ImageClickEventArgs e)
    {
        MailMessage mmsg1 = new MailMessage();
        string strFileName = "";
        SmtpClient smt1 = new SmtpClient();
        datasourceIBSQL dobj1 = new datasourceIBSQL();
        try
        {
            smt1.Host = "mail.nimbox.co.uk";
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,System.Security.Cryptography.X509Certificates.X509Certificate certificate,System.Security.Cryptography.X509Certificates.X509Chain chain,System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            mmsg1 = new MailMessage();
            if (!addmailcollection(txtTo1.Text.Trim().ToString(), mmsg1.To))
            { alert("Your To Address is not Valid, Please check"); return; }
            if (!addmailcollection(txtCC1.Text.Trim().ToString(), mmsg1.CC))
            { alert("Your CC Address is not Valid, Please check"); return; }
            if (!addmailcollection(txtBCC1.Text.Trim().ToString(), mmsg1.Bcc))
            { alert("Your BCC Address is not Valid, Please check"); return; }
            mmsg1.From = new MailAddress(lblFrom1.Text.ToString());
            mmsg1.Subject = txtSubject1.Text.Trim().ToString();
            mmsg1.Body = lblBody1.InnerText.Trim().ToString();
            mmsg1.IsBodyHtml = true;

            if (File_upload_1.PostedFile != null)
            {
                HttpPostedFile attFile = File_upload_1.PostedFile;
                int attachFileLength = attFile.ContentLength;


                if (attachFileLength > 0)
                {
                    strFileName = Path.GetFileName(File_upload_1.PostedFile.FileName);
                    if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                    FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                    File_upload_1.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                    mmsg1.Attachments.Add(attach);
                }

            }
            smt1.Send(mmsg1);
            alert("Mailsent Successfully");
          

        }
        catch (Exception ex)
        {
            alert(ex.ToString());
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
   
    public void mailbody_1()
    {

        //pdfName1 = txtArticleId1.Text.Trim();
        //string strUrl = "ftp://c0lumb1a:charlesw0rth@60.10.59.134/" + pdfName1 + ".pdf";
        StringBuilder MyStringBuilder1 = new StringBuilder();
        MyStringBuilder1.Append("Dear");
        MyStringBuilder1.Append("<br/><br/>");
        MyStringBuilder1.Append("Your edited and typeset article for Tremor and Other Hyperkinetic Movements ");
        MyStringBuilder1.Append("is now available for your review as an electronic proof (eProof). We first ");
        MyStringBuilder1.Append("sent the proof to you on 15th Jan., and this is our second attempt at ");
        MyStringBuilder1.Append("soliciting your feedback. ");
        MyStringBuilder1.Append("<br/><br/>");
        MyStringBuilder1.Append("Your eProof is attached to this email. Please review the PDF and respond to ");
        MyStringBuilder1.Append("all author queries within 48 hours. <br/><br/>");
        MyStringBuilder1.Append("If you have any problems please let us know.  ");
        MyStringBuilder1.Append("<br/><br/>");
        MyStringBuilder1.Append("Kind regards, <br/>");
        MyStringBuilder1.Append("Tremor and Other Hyperkinetic Movements");

        lblBody1.InnerText = MyStringBuilder1.ToString();
        txtCC1.Text = "ko2336@columbia.edu;tremor.admin@libraries.cul.columbia.edu";
        txtBCC1.Text = "project.manager@charlesworth-group.in";
    }
    protected void HlnkloadFile_Click_1(object sender, EventArgs e)
    {
        click_link_1();
    }
}
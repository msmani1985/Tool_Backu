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
using System.Text;

public partial class NatureEmail : System.Web.UI.Page
{
    string strFileName_path = ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString();
    string filename,job_id = string.Empty;
    string spath = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        // mailbody();
        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        if (!Page.IsPostBack)
        {
            XmlDocument xd = new XmlDocument();
            XmlNode childnode = null;
            XmlNode xn = null;

            string isToday = System.DateTime.Now.DayOfWeek.ToString();
            filename = Request.QueryString["filename"].ToString().Trim();
            job_id = Request.QueryString["JOBID"].ToString().Trim();
            Session["jobid"] = job_id;
            DataSet dst = new DataSet();
            datasourceSQL dsql = new datasourceSQL();
            dst = dsql.ExcProcedure("spGet_Nature_Email_Content", new string[,] { { "@jobid", job_id.ToString() } }, CommandType.StoredProcedure);
            if (dst != null)
            {
                try
                {
                    foreach (DataRow samrow in dst.Tables[0].Rows)
                    {
                        Session["filname"] = samrow["pdf_name"].ToString().Trim();
                        Session["Author"] = samrow["Author"].ToString().Trim();
                        Session["Aemail"] = samrow["AEMAIL"].ToString().Trim();
                        spath = @"http:\\182.71.236.121:3060\nature\production\" + samrow["pdf_name"].ToString().Trim() + ".pdf";
                        Session["url"] = @"http:\\182.71.236.121:3060\nature\production\" + samrow["pdf_name"].ToString().Trim() + ".pdf";
                        if (isToday == "Friday" || isToday == "Saturday" || isToday == "Sunday")
                        {
                            xd.Load(Server.MapPath("").ToString() + @"\Nature_Emailconfig.xml");
                        }
                        else
                        {
                            xd.Load(Server.MapPath("").ToString() + @"\Nature_Emailconfig.xml");
                        }
                        xn = xd.DocumentElement.SelectSingleNode("//followup");
                        if (xn != null)
                        {
                            childnode = xn.SelectSingleNode("to");
                            lblToAddress.Text = samrow["AEMAIL"].ToString().Trim(); //(childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                            childnode = xn.SelectSingleNode("cc");
                            lblCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                            childnode = xn.SelectSingleNode("bcc");
                            lblBCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                           // childnode = xn.SelectSingleNode("body");
                            mailbody();
                           // lblSubject.Text = "SREP - Update to proof notification email text";
                            lblSubject.Text = "Scientific Reports Proofs for Review " + "-" + samrow["jourcode"].ToString().Trim() + "-" + samrow["AMANUSCRIPTID"].ToString().Trim();
                        }
                    }
                }
                catch
                {

                }
            }
        }
    }

    public void mailbody()
    {

        string sname = Session["filname"].ToString();
        string surl = Session["url"].ToString();
        string sAuthor = Session["Author"].ToString();

        StringBuilder MyStringBuilder = new StringBuilder("Dear " + sAuthor + "");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("The proofs for your <i> Scientific Reports </i> article are now ready for checking. You can access your article proofs** using the following link: <a href=" + surl.ToString() + " >" + sname + "</a>");
        MyStringBuilder.Append("<br/><br/>");
        MyStringBuilder.Append("<b>To ensure prompt publication, your proofs must be returned within 48 hours. If we have not heard from you within this time frame, publication of your paper may be delayed.</b><br/><br/>");
        MyStringBuilder.Append("Please ensure that only <b><u>one</u></b> author communicates with us and that only <b><u>one</u></b> set of corrections is returned. The corresponding (or nominated) author is responsible on behalf of all co-authors for the accuracy of all content, including spelling of names and current affiliations.<br/><br/>");
        MyStringBuilder.Append("There is only one round of proofing, so please check the title, author list and affiliations thoroughly to ensure that they are correct. For the main text, only errors that have been introduced during the production process or those that directly compromise the scientific integrity of the paper may be corrected at this stage.<br/><br/>");
        MyStringBuilder.Append("Please make any necessary corrections, as clearly as possible, using <b><u>one</u></b> of the following three methods. Proof feedback that is ambiguous or that is not supplied as specified may be returned.<br/><br/>");
        MyStringBuilder.Append(" <ol>        <li>          Use Adobe Reader's comments and/or editing tools to annotate the PDF and return via email to scientificreportsproofs@nature.com OR<br/>        </li>        <li>          Email (to scientificreportsproofs@nature.com) the necessary corrections as a list which cites the page and line number where a correction needs to be made, how the text stands  in the proof and what it should be changed to OR<br/>        </li>        <li>          Use clear proof marks to indicate changes on a printout of the PDF and email a scan of the pages to: scientificreportsproofs@nature.com,<br/><br/>        </li>      </ol>");
        MyStringBuilder.Append("All corrections will be checked by the publishing team.<i> Scientific Reports</i> reserves the right to make the final decision on all changes to the manuscript. Formatting changes that contravene the style guide will not be made");
        MyStringBuilder.Append("  Please note:<br/>");
        MyStringBuilder.Append("<ul type=circle>        <li>          <b> Changes to supplementary information:</b> supplementary information is published as supplied with the accepted version of the manuscript and as such is not proofed.        </li>        <li>          <b>Changes to figures:</b> should a change be required to a figure file you must supply the corrected figure file, with a brief justification of the changes. We accept final figures in .eps, .tiff or .jpeg.        </li>        <li>         <b>Changes to scientific content/data:</b> corrections to scientific content or data post-peer review will likely require editorial approval. As such, please provide justification for any requested changes.                 </li>     </ul><br/>");
        MyStringBuilder.Append("The link provided in this email will take you to a PDF of your article. Adobe Reader is required in order to read these files. If you don't have this program it can be downloaded free of charge from: http://www.adobe.com/products/acrobat/readstep2.html. A form to order reprints of your Article is available at http://www.nature.com/reprints/author-reprints.html. To obtain the special author reprint rate, orders must be made within a month of the publication date. After that, reprints are charged at the normal (commercial) rate.<br/><br/>");
        MyStringBuilder.Append(" Thank you for choosing to publish your article with us.<br/><br/>");
        MyStringBuilder.Append("  Regards, <br/>");
        MyStringBuilder.Append(" <i>Scientific Reports</i><br/> ");
        MyStringBuilder.Append(" The Macmillan Building<br/> ");
        MyStringBuilder.Append("  4 Crinan Street<br/> ");
        MyStringBuilder.Append("  London, N1 9XW<br/><br/>");
        MyStringBuilder.Append(" scientificreportsproofs@nature.com<br/><br/>");
        MyStringBuilder.Append(" **Please note this PDF file may not be offered for commercial sale or for any systematic external distribution by a third party.");
        lblBody.InnerText = MyStringBuilder.ToString();
        hyperlink1.Text = surl.ToString().Replace("\\", "//");
        hyperlink1.NavigateUrl = surl.ToString().Replace("\\","//");
 

        

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
            mmsg.From =new MailAddress(lblFromAddress.Text.ToString());
            mmsg.Subject = lblSubject.Text.Trim().ToString();
            mmsg.Body = lblBody.InnerText.Trim().ToString();
            mmsg.IsBodyHtml = true;
            string strFileName = "";
             smt.Send(mmsg);
            alert("Mailsent Successfully");
            string uqry = "UPDATE Article_dp SET SREP_Email_Flag=1,SREP_Sent_By=" + Convert.ToInt32(Session["employeeid"]) + ",SREP_Sent_On='" + DateTime.Now + "'  WHERE ano =" + Convert.ToInt32(Session["jobid"]);
            dobj.ExcuteProc(uqry);
           

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


    protected void lnkPreview_Click(object sender, EventArgs e)
    {
        Response.Redirect("www.google.com");
    }
}
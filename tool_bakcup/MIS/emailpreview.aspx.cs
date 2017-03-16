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
using System.Net.Mail;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
//using Rebex.Net;



public partial class emailpreview : System.Web.UI.Page
{
    string sTestFile = "testmail.pdf";
    string sVirtualPath = @"D:\";
    string sSite = @"Dublin\";
    string pemail = "";
    int CustCategory;
    int bno;
    string projectno;
    int custno;
    string sJobName = "";
    int invoiceno;
    DataSet emailds = null;
    string ptitle = "";
    string btitle = "";
    string coaction_body_content = "";
    static string new_attachstr = string.Empty;
    string xml_path = "";
    string Local_path = "";
    string Local_path_XML = "";
    string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        sVirtualPath = Server.MapPath(@"InvoiceTemplates\");
        lblFromAddress.Text = ConfigurationManager.AppSettings["Invoice_from"].ToString();
        if (!Page.IsPostBack)
        {
            //string custlist = File.ReadAllText(Server.MapPath("") + @"\Email_txtFile\Email_ManuallyAttachment_CustList.txt");
            StreamReader sr = new StreamReader(Server.MapPath("") + @"\Email_txtFile\Email_ManuallyAttachment_CustList.txt");
            string line = "";
            bool flag = false;

            if (Request.QueryString["custno"].ToString() == "2556")
            {
                btnUpload_PDF.Visible = true;
                btnUpload_XML.Visible = true;
            }
            else
            {
                btnUpload_PDF.Visible = false;
                btnUpload_XML.Visible = false;
            }

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Split(',').GetValue(0).ToString() == Request.QueryString["custno"].ToString())
                {
                    flag = true;
                    break;
                }
            }
            sr.Close();
            sr.Dispose();

            if (flag == true)//For Logoscript
                div_newattachheader.Visible = true;
            else
                div_newattachheader.Visible = false;

            bool flg = false;

            Datasource_IBSQL emailobj = new Datasource_IBSQL();
            emailds = new DataSet();
            switch (Convert.ToInt32(Request.QueryString["category"])) //For Invoice value not update in invoice_value file
            {
                case 4://For WIP
                case 1:
                    //For TandF || Co-Action || LUND - Ecology Building || San Lucas Medical Limited || Agricultural Institute of Canada || Royal Institute of Academy
                    if (Convert.ToInt32(Request.QueryString["custno"]) == 1023012 || Convert.ToInt32(Request.QueryString["custno"]) == 10211 || Convert.ToInt32(Request.QueryString["custno"]) == 10231 || Convert.ToInt32(Request.QueryString["custno"]) == 10216 || Convert.ToInt32(Request.QueryString["custno"]) == 10228 || Convert.ToInt32(Request.QueryString["custno"]) == 10191 || Convert.ToInt32(Request.QueryString["custno"]) == 2556 || Convert.ToInt32(Request.QueryString["custno"]) == 10190 || Convert.ToInt32(Request.QueryString["custno"]) == 10081 || Convert.ToInt32(Request.QueryString["custno"]) == 2070 || Convert.ToInt32(Request.QueryString["custno"]) == 2052 || Convert.ToInt32(Request.QueryString["custno"]) == 10105 || custno == 10089)
                    {
                        emailds = emailobj.GetDispatchedjobMail(Convert.ToInt32(Request.QueryString["custno"].ToString()), Convert.ToInt32(Request.QueryString["category"].ToString()), Convert.ToInt32(Request.QueryString["conno"].ToString()), Convert.ToInt32(Request.QueryString["Mailflg"].ToString()));
                        if (emailds != null)
                        {
                            for (int emailds_cnt = 0; emailds_cnt < emailds.Tables[0].Rows.Count; emailds_cnt++)
                            {
                                if (CheckInvoiceItem(emailds.Tables[0].Rows[emailds_cnt]["jourcode"].ToString().Trim() + emailds.Tables[0].Rows[emailds_cnt]["IISSUENO"].ToString().Trim(), "i")) flg = true;
                                if (CheckInvoiceItem(emailds.Tables[0].Rows[emailds_cnt]["jourcode"].ToString().Trim() + emailds.Tables[0].Rows[emailds_cnt]["IISSUENO"].ToString().Trim(), "d")) flg = true;
                            }
                        }
                    }
                    else
                    {
                        if (CheckInvoiceItem(Request.QueryString["jourcode"].ToString().Trim() + Request.QueryString["issueno"].ToString().Trim(), "i")) flg = true;
                        if (CheckInvoiceItem(Request.QueryString["jourcode"].ToString().Trim() + Request.QueryString["issueno"].ToString().Trim(), "d")) flg = true;
                    }
                    break;
                case 2: if (CheckInvoiceItem(Request.QueryString["BNUMBER"].ToString().Trim(), "i")) flg = true;
                        if (CheckInvoiceItem(Request.QueryString["BNUMBER"].ToString().Trim(), "d")) flg = true;
                    break;
                case 3:
                    if (
                        Request.QueryString["custno"] == "10104" || Request.QueryString["custno"] == "10108" ||
                        Request.QueryString["custno"] == "10110" || Request.QueryString["custno"] == "10111" ||
                        Request.QueryString["custno"] == "10114" || Request.QueryString["custno"] == "10115" ||
                        Request.QueryString["custno"] == "10118" || Request.QueryString["custno"] == "10125" ||
                        Request.QueryString["custno"] == "10128" || Request.QueryString["custno"] == "10136" ||
                        Request.QueryString["custno"] == "10137" || Request.QueryString["custno"] == "10139" ||
                        Request.QueryString["custno"] == "10145" || Request.QueryString["custno"] == "10148" ||
                        Request.QueryString["custno"] == "10150" || Request.QueryString["custno"] == "10151" ||
                        Request.QueryString["custno"] == "10162" || Request.QueryString["custno"] == "10163" ||
                        Request.QueryString["custno"] == "10176" || Request.QueryString["custno"] == "10180" ||
                        Request.QueryString["custno"] == "10181" || Request.QueryString["custno"] == "10195" ||
                        Request.QueryString["custno"] == "10197" || Request.QueryString["custno"] == "10199" ||
                        Request.QueryString["custno"] == "10243" || Request.QueryString["custno"] == "10244" ||
                        Request.QueryString["custno"] == "10246" || Request.QueryString["custno"] == "10250" ||
                        Request.QueryString["custno"] == "10251" || Request.QueryString["custno"] == "10252" ||
                        Request.QueryString["custno"] == "10253" || Request.QueryString["custno"] == "10255" ||
                        Request.QueryString["custno"] == "10257" || Request.QueryString["custno"] == "10260" ||
                        Request.QueryString["custno"] == "10261" || Request.QueryString["custno"] == "10263" ||
                        Request.QueryString["custno"] == "10267" || Request.QueryString["custno"] == "10267" ||
                        Request.QueryString["custno"] == "10273" || Request.QueryString["custno"] == "10277" ||
                        Request.QueryString["custno"] == "10282" || Request.QueryString["custno"] == "10159" ||
                        Request.QueryString["custno"] == "10160" || Request.QueryString["custno"] == "10161" ||
                        Request.QueryString["custno"] == "10172" || Request.QueryString["custno"] == "10143" ||
                        Request.QueryString["custno"] == "10153" || Request.QueryString["custno"] == "10154" ||
                        Request.QueryString["custno"] == "10155" || Request.QueryString["custno"] == "10156" ||
                        Request.QueryString["custno"] == "10157" || Request.QueryString["custno"] == "10158" ||
                        Request.QueryString["custno"] == "10182" || Request.QueryString["custno"] == "10245"
                        )
                    {
                        if (CheckInvoiceItem(Request.QueryString["ptitle"].ToString().Trim(), "i")) flg = true;
                        if (CheckInvoiceItem(Request.QueryString["ptitle"].ToString().Trim(), "d")) flg = true;
                    }
                    else
                    {
                        if (CheckInvoiceItem(Request.QueryString["pcode"].ToString().Trim(), "i")) flg = true;
                        if (CheckInvoiceItem(Request.QueryString["pcode"].ToString().Trim(), "d")) flg = true;
                    }
                    break;
            }

            if (flg == true)//For Invoice value not update in invoice_value file
                div_notupdateinvoice.InnerHtml += "<br/> <b><font color='red'>(or)</font></b> <br/> <b><font color='red'>Further assistant, Please contact IT Team</font></b>";
            Div_emailpreview.Visible = true;
            //Div_error.Visible = false;
            string sHTMLTemplate = sVirtualPath + sTestFile;
            if (Session["location"] != null && (Session["location"].ToString() == "d" || Session["location"].ToString() == "i"))
            {
                try
                {
                    pemail = Request.QueryString["pemail"];
                    custno = Convert.ToInt32(Request.QueryString["custno"]);
                    CustCategory = Convert.ToInt32(Request.QueryString["category"]);

                    if (CustCategory == 1 || CustCategory == 4)//Journal || 4 For WIP
                        invoiceno = Convert.ToInt32(Request.QueryString["iinvoiceno"]);
                    else if (CustCategory == 2)//Books
                        invoiceno = Convert.ToInt32(Request.QueryString["binvoiceno"]);
                    else if (CustCategory == 3)//Projects
                        invoiceno = Convert.ToInt32(Request.QueryString["invno"]);


                    sSite = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString();
                    if (Request.QueryString["jourcode"] != null && Request.QueryString["jourcode"].ToString() != "")
                        sJobName = Request.QueryString["jourcode"].ToString().Trim() + Request.QueryString["issueno"].ToString().Trim();
                    if (Request.QueryString["BNUMBER"] != null && Request.QueryString["BNUMBER"].ToString().Trim() != "")
                    {
                        bno = Convert.ToInt32(Request.QueryString["BNO"]);
                        sJobName = Request.QueryString["BNUMBER"].ToString().Trim();
                    }
                    if (Request.QueryString["pcode"] != null && Request.QueryString["pcode"].ToString().Trim() != "")
                    {
                        projectno = Request.QueryString["projectno"].ToString();
                        sJobName = Request.QueryString["pcode"].ToString().Trim();
                    }

                    EmailAddress();
                    EmailSubjectBody(sJobName);

                    HlnkloadFile.Visible = false;

                    switch (CustCategory)
                    {
                        case 4://For WIP
                        case 1: GetEmailDetails(Convert.ToInt32(Request.QueryString["finsiteno"]), "journal");
                            break;
                        case 2: GetEmailDetails(Convert.ToInt32(Request.QueryString["finsiteno"]), "book");
                            break;
                        case 3: GetEmailDetails(Convert.ToInt32(Request.QueryString["finsiteno"]), "project");
                            break;
                    }
                }

                catch (Exception oex)
                {
                    emailtable.Visible = false;
                    divInvoiceHTML.InnerHtml = "<P style='color:red'>" + oex.Message + "\n\n contact [software@datapage.org]</P>";
                }
            }
            else
            {
                divInvoiceHTML.InnerHtml = "<P style='color:red'>Your Session has been expired. Please login.</P>";
                emailtable.Visible = false;
            }
        }
        else
        {
            if (Request.QueryString["Mailflg"] != null && Request.QueryString["Mailflg"].ToString() == "2" && Request.QueryString["custno"].ToString() != "2556")
                BulkAttachementTandF(Convert.ToInt32(Request.QueryString["custno"]),Request.QueryString["Mailflg"].ToString());
            else if (Request.QueryString["custno"].ToString() != "2556")
                CreateAttachmentLink(Convert.ToInt32(Request.QueryString["custno"]), Convert.ToInt32(Request.QueryString["category"]), Convert.ToInt32(Request.QueryString["conno"]));
            addnewattachment(new_attachstr);
        }

    }
    private bool CheckInvoiceItem(string invoice_item, string location)
    {
            XmlDocument invval_doc = new XmlDocument();
            XmlNode inv_node = null;
            string msg = "";
            if (location.ToUpper() == "I")
            {
                invval_doc.Load(ConfigurationManager.ConnectionStrings["indiaINVXML"].ToString());
                msg = "<b><font color='red'>Invoice value not update. So please preview India invoice for " +
                    invoice_item + " Issue </font></b><br/>";
            }
            else
            {
                invval_doc.Load(ConfigurationManager.ConnectionStrings["dublinINVXML"].ToString());
                msg = "<b><font color='red'>Invoice value not update. So please preview Dublin invoice for " +
                    invoice_item + " Issue </font></b><br/>";
            }
            inv_node = invval_doc.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICEITEM='" + invoice_item + "']");
            if (inv_node == null)
            {
                Div_emailpreview.Visible = false;
                //Div_error.Visible = true;
                div_notupdateinvoice.Visible = true;
                div_notupdateinvoice.InnerHtml += msg;
                return true;
            }
            return false;
        
    }
    private Boolean AddMailAddress(string Mail_Address, MailAddressCollection Mail_Collection)
    {
        string[] EmailAddress = null;
        if(!string.IsNullOrEmpty(Mail_Address))
        {
            if (Mail_Address.IndexOf(";") > 0)
            {
                EmailAddress = Mail_Address.Split(';');
                for (int Address_cnt = 0; Address_cnt < EmailAddress.Length; Address_cnt++)
                {
                    if (EmailAddress[Address_cnt].ToString().Trim() != "")
                    {
                        if (!Regex.IsMatch(EmailAddress[Address_cnt].ToString().Trim(), @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                            return false;
                        Mail_Collection.Add(EmailAddress[Address_cnt].ToString().Trim());
                    }
                }
            }
            else
            {
                if (!Regex.IsMatch(Mail_Address.ToString().Trim(), @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                    return false;
                Mail_Collection.Add(Mail_Address.ToString().Trim());
            }
            return true;
        }
        return true;
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
         MailMessage oMsg = new MailMessage();
         SmtpClient oSmtp = new SmtpClient();
         string Alert_Msg = "";
         try
         {
             CustCategory = Convert.ToInt32(Request.QueryString["category"]);
             custno = Convert.ToInt32(Request.QueryString["custno"]);
             bno = Convert.ToInt32(Request.QueryString["BNO"]);
             projectno = Request.QueryString["projectno"];
             lblMessage.Text = "";
             //oSmtp.Host = "192.9.200.221";
             //oSmtp.Port = 25;
             oSmtp.Host = ConfigurationManager.AppSettings["Invoicemail_host"].ToString();
             oSmtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Invoicemail_port"]);
             

             //oMsg.From = new MailAddress(lblFromAddress.Text, "Datapage Accounts");
             oMsg.From = new MailAddress(ConfigurationManager.AppSettings["Invoice_from"].ToString());
             oSmtp.Credentials = new System.Net.NetworkCredential(oMsg.From.ToString(), ConfigurationManager.AppSettings["accounts_password"].ToString());
             oSmtp.EnableSsl = true;
             if (!AddMailAddress(lblToAddress.Text.Trim(), oMsg.To))
             {
                 showmessage("Your To address is not valid, Please check");
                 return;
             }

             if (!AddMailAddress(lblBCCAddress.Text.Trim(), oMsg.Bcc))
             {
                 showmessage("Your BCC address is not valid, Please check");
                 return;
             }

             if (!AddMailAddress(ConfigurationManager.AppSettings["BCCAddress"].ToString(), oMsg.Bcc))
             {
                 showmessage("Your BCC address is not valid, Please check");
                 return;
             }

             if (!AddMailAddress(lblCCAddress.Text.Trim(), oMsg.CC))
             {
                 showmessage("Your CC address is not valid, Please check");
                 return;
             }
            
             oMsg.Subject = lblSubject.Text.Trim();
             oMsg.Body = lblBody.Text.Trim();
            //Add Attachment
             if (Convert.ToInt32(Request.QueryString["custno"]) == 10098)//For Medknow
                 sSite = ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString();
             else
                 sSite = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString();
             string InvoicenoforUpdate = "";
             string invoiceupdatewip = "";
             string Project_InvoiceUpdate = "";
             try
             {
                 for (int div_control_cnt = 0; div_control_cnt < div_Attachments.Controls.Count; div_control_cnt++)
                 {
                     if (div_Attachments.Controls[div_control_cnt].GetType().ToString().Equals("System.Web.UI.WebControls.LinkButton"))
                     {
                         LinkButton cntrlbtn = (LinkButton)div_Attachments.FindControl(div_Attachments.Controls[div_control_cnt].ID.ToString());
                         if (File.Exists(sSite + cntrlbtn.Text))
                         {
                             oMsg.Attachments.Add(new Attachment(sSite + cntrlbtn.Text));
                             if (cntrlbtn.Text.Contains("WIP"))
                             {
                                 if (!string.IsNullOrEmpty(invoiceupdatewip))
                                     invoiceupdatewip += ", ";
                                 invoiceupdatewip += cntrlbtn.ID.ToString();
                             }
                             else
                             {
                                 if (!string.IsNullOrEmpty(InvoicenoforUpdate))
                                     InvoicenoforUpdate += ", ";
                                 InvoicenoforUpdate += cntrlbtn.ID.ToString();
                             }

                             if (Request.QueryString["custno"] == "10248")
                             {
                                 File.Copy(sSite + cntrlbtn.Text, ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + "\\Invoice\\" + cntrlbtn.Text.Replace(".pdf", "_Copy1.pdf"), true);
                                 File.Copy(sSite + cntrlbtn.Text, ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + "\\Invoice\\" + cntrlbtn.Text.Replace(".pdf", "_Copy2.pdf"), true);
                                 oMsg.Attachments.Add(new Attachment(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + "\\Invoice\\" + cntrlbtn.Text.Replace(".pdf", "_Copy1.pdf")));
                                 oMsg.Attachments.Add(new Attachment(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + "\\Invoice\\" + cntrlbtn.Text.Replace(".pdf", "_Copy2.pdf")));
                             }
                         }
                         //comment for test 26 May 2010
                         else
                         {
                             lblMessage.Text = "Unable to attach File.Please check and confirm the file is exists in particular path";
                             return;
                         }

                     }
                        //For Update in Projects if TandF exists in Projects
                     else if (div_Attachments.Controls[div_control_cnt].GetType().ToString().Equals("System.Web.UI.WebControls.Label"))
                     {
                         Label cntrl_Lbl = (Label)div_Attachments.FindControl(div_Attachments.Controls[div_control_cnt].ID.ToString());
                         int val=0;
                         if (int.TryParse(cntrl_Lbl.Text, out val))
                             Project_InvoiceUpdate = (string.IsNullOrEmpty(Project_InvoiceUpdate)) ? cntrl_Lbl.Text.ToString() : Project_InvoiceUpdate + "," + cntrl_Lbl.Text.ToString();
                       
                     }
                 }

                 //For Logoscript
                 StreamReader sr = new StreamReader(Server.MapPath("") + @"\Email_txtFile\Email_ManuallyAttachment_CustList.txt");
                 string line = "";
                 bool flag = false;
                 while ((line = sr.ReadLine()) != null)
                 {
                     if (line.Split(',').GetValue(0).ToString() == Request.QueryString["custno"].ToString())
                     {
                         flag = true;
                         break;
                     }
                 }
                 sr.Close();
                 sr.Dispose();
                 
                 if (flag == true)//For Logoscript extra attachment

                     if (!string.IsNullOrEmpty(new_attachstr))
                     {
                         string[] mail_attach;
                         mail_attach = new_attachstr.Split(',');
                         for (int attcnt = 0; mail_attach.Length > attcnt; attcnt++)
                             if (!string.IsNullOrEmpty(mail_attach[attcnt].ToString()))
                                 if (File.Exists(mail_attach[attcnt].ToString())) oMsg.Attachments.Add(new Attachment(mail_attach[attcnt].ToString()));
                                 else throw new ArgumentException("File does not exists <br/>" + mail_attach[attcnt].ToString());
                     }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally { }

             //Comment for test 05 May 10
             System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
             {
                 return true;
             }; 
             oSmtp.Send(oMsg);
             new_attachstr = string.Empty;
             Alert_Msg = "Mail Sent Successfully";
             lblMessage.Text = "Mail Sent Successfully";

             if (CustCategory == 1 || CustCategory == 4)///////This is For Journal || 4 For WIP
             {
                 InvClear_Click(sender, e);
             }
             if (CustCategory == 2)//////////For Books//////
             {
                 Report obj = new Report();
                 int testbno = bno;
                 if (obj.UpdateFinalInvoicedBooks(bno) == false)
                     lblMessage.Text += "<br/>Invoice status not as completed! Please contact IT Team [software@datapage.org]";
                 obj = null;
             }
             if (CustCategory == 3 || Project_InvoiceUpdate!="")//////////For Projects//////
             {
                 Report obj = new Report();
                 if (obj.UpdateFinalInvoicedProjects((string.IsNullOrEmpty(Project_InvoiceUpdate)) ? InvoicenoforUpdate : Project_InvoiceUpdate) == false)
                     lblMessage.Text += "<br/>Invoice status not as completed! Please contact IT Team [software@datapage.org]";
                 obj = null;
             }
             btnSubmit.Enabled = false;
             btnSubmit.Visible = false;
         }
         catch (Exception oex)
         {
             lblMessage.Text += oex.Message;
             Alert_Msg = oex.Message.ToString();
         }
        finally
        {
            oMsg.Dispose();
            showmessage(Alert_Msg);
        }
 
    }


    private void showmessage(string msg)
    {
        if (!string.IsNullOrEmpty(msg))
        {
            lblMessage.Text = "<font color=Red><b>" + msg + "</b></font>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
        }
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
            { lblMessage.Text =(sFileName.IndexOf("File Missing")>0)?"File Missing":Path.GetFileName( sFileName) + ", not found!"; }
        }
        catch (Exception oex)
        { lblMessage.Text = oex.Message;}

    }

    private void EmailSubjectBody(string sInvJobName)
    {
        lblSubject.Text = "Datapage International Ltd - Invoice of Job: " + sInvJobName.Trim();

        if (custno == 2556 || custno == 10072)
        {
            lblSubject.Text = "Datapage International Ltd - Invoice spreadsheet for " + sInvJobName.Trim();
        }

        if (custno == 10057 && CustCategory == 1 || CustCategory == 4)//4 For WIP
        {
            lblSubject.Text = "Datapage International Ltd - Rechnung für " + sInvJobName.Trim();
            lblBody.Text = "Sehr geehrte Frau " + Request.QueryString["pename"].ToString().Trim() + ",\n\n";
            lblBody.Text += "Anbei finden Sie die rechnung für " + sJobName.ToString().Trim();//+ " which you requested. \n\n\n";
            lblBody.Text += "Mit freundlichen Grüßen,\n\n";
        }
        else
        {
            lblBody.Text = (Request.QueryString["pename"] != null) ? "Dear " + Request.QueryString["pename"].ToString().Trim() + ",\n\n" : "";
            lblBody.Text += "Please find attached copies of invoice of Job: " + sJobName.ToString().Trim() + " which you requested. \n\n\n";
            //lblBody.Text += "Best Wishes,\n\n";
        }
        
        lblBody.Text += "Best Regards\n";
        lblBody.Text += "Accounts Department\n\n";
        lblBody.Text += "Datapage\n";
        lblBody.Text += "Tel.: +353 1 6572576\n";
        lblBody.Text += "Fax.: +91 44 42085158\n";
        lblBody.Text += "Web: www.datapage.ie";
    }

    private void EmailAddress()
    {
        lblToAddress.Text = (pemail!=null)?pemail.ToString():"";
        //lblToAddress.Text = "sivaraj@datapage.org";
        //lblToAddress.Text = "subbulakshmi@datapage.org";
        //lblToAddress.Text = "lorraine@donnelly.iol.ie";
        lblBCCAddress.Text = "accounts@datapage.org;Nisha.Rahul@datapage.org";
        lblCCAddress.Text = "";
        lblBCCAddress.Text = "";
    }

    private void GetEmailDetails(int finsiteno,string category)
    {
        XmlDocument xdoc = new XmlDocument();
        XmlNode xnode = null;
        XmlNode xn2=null;
        string Job_Name = "";
        try
        {
            xdoc.Load(Server.MapPath("") + @"\emailconfig.xml");

            //For Categorywise(journal,book,project)
            xnode = xdoc.SelectSingleNode("//" + category);
            if (xnode != null)
                GetNodeVal(xnode);
            //For customerwise
            if (Request.QueryString["category"].ToString().Trim() == "1" && Request.QueryString["custno"].ToString().Trim() == "10132")
            {
                if (Request.QueryString["jourcode"].ToString().Trim() == "AJBM" || Request.QueryString["jourcode"].ToString().Trim() == "GRA" || Request.QueryString["jourcode"].ToString().Trim() == "GYA")
                {
                    xn2 = xnode.SelectSingleNode("customer[@finsiteno='" + finsiteno + "' and @jourcode='" + Request.QueryString["jourcode"].ToString().Trim() + "']");
                    if (xn2 != null)
                        GetNodeVal(xn2);
                }
                else
                {
                    xn2 = xnode.SelectSingleNode("customer[@finsiteno='" + finsiteno + "' and @jourcode='']");
                    if (xn2 != null)
                        GetNodeVal(xn2);
                }
            }
            else
            {
                xn2 = xnode.SelectSingleNode("customer[@finsiteno='" + finsiteno + "']");
                if (xn2 != null)
                    GetNodeVal(xn2);
            }
            if(Request.QueryString["Mailflg"]!=null && Request.QueryString["Mailflg"].ToString()=="2" && Request.QueryString["custno"] != "2556")
                Job_Name = BulkAttachementTandF(custno, Request.QueryString["Mailflg"]);
            else if(Request.QueryString["custno"] != "2556")
                Job_Name = CreateAttachmentLink(Convert.ToInt32(Request.QueryString["custno"]), CustCategory, Convert.ToInt32(Request.QueryString["conno"]));
            lblBody.Text = lblBody.Text.Replace("[JOBNAME]", Job_Name);
            lblSubject.Text = lblSubject.Text.Replace("[JOBNAME]", Job_Name);
            if (Request.QueryString["custno"] == "10143" || Request.QueryString["custno"] == "10153" ||
                Request.QueryString["custno"] == "10154" || Request.QueryString["custno"] == "10155" ||
                Request.QueryString["custno"] == "10156" || Request.QueryString["custno"] == "10157" ||
                Request.QueryString["custno"] == "10158" || Request.QueryString["custno"] == "10182" ||
                Request.QueryString["custno"] == "10245" || Request.QueryString["custno"] == "10269" ||
                Request.QueryString["custno"] == "10115" || Request.QueryString["custno"] == "10159" ||
                Request.QueryString["custno"] == "10160" || Request.QueryString["custno"] == "10161" ||
                Request.QueryString["custno"] == "10172")
            {
                lblBody.Text = lblBody.Text.Replace("[FCONTACT]", "Hi");
            }
            else
            {
                lblBody.Text = lblBody.Text.Replace("[FCONTACT]", "Dear " + Request.QueryString["pename"].Trim());
            }
            
            lblSubject.Text = lblSubject.Text.Replace("[INVOICENO]", (invoiceno.ToString() == "") ? "" : invoiceno.ToString());
            lblBody.Text = lblBody.Text.Replace("[INVOICENO]", (invoiceno.ToString() == "") ? "" : invoiceno.ToString());
            lblSubject.Text = lblSubject.Text.Replace("[PTITLE]", ptitle);
            lblBody.Text = lblBody.Text.Replace("[PTITLE]", ptitle);
            lblSubject.Text = lblSubject.Text.Replace("[BTITLE]", btitle);
            lblBody.Text = lblBody.Text.Replace("[BTITLE]", btitle);
            lblCCAddress.Text = lblCCAddress.Text.Replace("[PEEMAIL]", (Request.QueryString["pemail"]!=null && Request.QueryString["pemail"].ToString()!="")?Request.QueryString["pemail"]:"[PEEMAIL]");
            if (!string.IsNullOrEmpty(coaction_body_content))
                lblBody.Text = lblBody.Text.Replace("[Invoice-Content]", coaction_body_content);

            //kalimuthu Start
            if (Request.QueryString["custno"] == "10211" || Request.QueryString["custno"] == "10221" || 
                Request.QueryString["custno"] == "10238" || Request.QueryString["custno"] == "10214" || 
                Request.QueryString["custno"] == "10219" || Request.QueryString["custno"] == "10210" || 
                Request.QueryString["custno"] == "10229" || Request.QueryString["custno"] == "10201" ||
                Request.QueryString["custno"] == "10191" || Request.QueryString["custno"] == "10227" ||
                Request.QueryString["custno"] == "10216" || Request.QueryString["custno"] == "10231" ||
                Request.QueryString["custno"] == "10233" || Request.QueryString["custno"] == "10226" ||
                Request.QueryString["custno"] == "10235" || Request.QueryString["custno"] == "10228" ||
                Request.QueryString["custno"] == "10224" || Request.QueryString["custno"] == "10222" ||
                Request.QueryString["custno"] == "10254" || Request.QueryString["custno"] == "10213" ||
                Request.QueryString["custno"] == "10205" || Request.QueryString["custno"] == "10236" ||
                Request.QueryString["custno"] == "10264" || Request.QueryString["custno"] == "10230"
                )//,
            {
                lblToAddress.Text = "Simon.Fitzpatrick@charlesworth-group.com";
                lblCCAddress.Text = "Rohit.Choudhary@charlesworth-group.com;"+lblCCAddress.Text.Trim();
                lblBody.Text = lblBody.Text.Replace("Dear Rohit", "Dear Simon");
            }
            else if (Request.QueryString["custno"] == "10143" || Request.QueryString["custno"] == "10153" ||
                Request.QueryString["custno"] == "10154" || Request.QueryString["custno"] == "10155" ||
                Request.QueryString["custno"] == "10156" || Request.QueryString["custno"] == "10157" ||
                Request.QueryString["custno"] == "10158" || Request.QueryString["custno"] == "10182" ||
                Request.QueryString["custno"] == "10245" || Request.QueryString["custno"] == "10269" 
                )
            {
                lblToAddress.Text = "vijay.kumar@datapage.org";
                lblCCAddress.Text = "";
                lblBCCAddress.Text = "Lorraine.gillespie@datapage.ie;accounts@datapage.org";
            }
            else if (Request.QueryString["custno"] == "10115" || Request.QueryString["custno"] == "10159" ||
                     Request.QueryString["custno"] == "10160" || Request.QueryString["custno"] == "10161" ||
                     Request.QueryString["custno"] == "10172")
            {
                lblToAddress.Text = "vijay.kumar@datapage.org";
                lblCCAddress.Text = "";
                lblBCCAddress.Text = "";
            }
            else if (lblBody.Text.Contains("Dear Rohit"))
            {
                lblCCAddress.Text = "Simon.Fitzpatrick@charlesworth-group.com;" + lblCCAddress.Text;
            }
            else if (Request.QueryString["custno"] == "10248")
            {
                lblSubject.Text = "Attention Ms. Liliana - " + lblSubject.Text;
                lblCCAddress.Text = "vazquezd@paho.org;neilmar@paho.org;prem@datapage.org";
                lblBody.Text = lblBody.Text.Replace("Dear Marita", "Dear Ms. Ana V. Leone, KBR");
            }
            else if (Request.QueryString["custno"] == "10194")
            {
                lblCCAddress.Text = "";
            }
            //else
            //    lblBody.Text = lblBody.Text.Replace("[FCONTACT]", "Dear " + Request.QueryString["pename"].Trim());
            ////end
            
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            xn2 = null;
            xnode = null;
            xdoc = null; 
        }
    }
    private void GetNodeVal(XmlNode xn)
    {
        XmlNode x2 = null;

        x2 = xn.SelectSingleNode("to");
        lblToAddress.Text = (x2 != null && x2.InnerText != "") ? x2.InnerText : lblToAddress.Text;
        x2 = xn.SelectSingleNode("bcc");
        lblBCCAddress.Text = (x2 != null && x2.InnerText != "") ? x2.InnerText : lblBCCAddress.Text;
        x2 = xn.SelectSingleNode("cc");
        lblCCAddress.Text = (x2 != null && x2.InnerText != "") ? x2.InnerText : lblCCAddress.Text;
        x2 = xn.SelectSingleNode("subject");
        lblSubject.Text = (x2 != null && x2.InnerText != "") ? x2.InnerText : lblSubject.Text;
        x2 = xn.SelectSingleNode("body");
        lblBody.Text = (x2 != null && x2.InnerText != "") ? x2.InnerText : lblBody.Text;
        x2 = xn.SelectSingleNode("fcontact");
        //lblBody.Text = lblBody.Text.Replace("[FCONTACT]", (x2 != null && x2.InnerText != "" && x2.InnerText.ToUpper() != "ACCOUNTS DEPARTMENT") ? "Dear " + x2.InnerText :(x2 != null && x2.InnerText != "" && x2.InnerText.ToUpper() == "ACCOUNTS DEPARTMENT")?"Hi" : "[FCONTACT]");
        lblBody.Text = lblBody.Text.Replace("[FCONTACT]", (x2 != null && x2.InnerText != "" && x2.InnerText.ToUpper() == "ACCOUNTS DEPARTMENT") ? "Hi," : (x2 != null && x2.InnerText != "" && x2.InnerText.ToUpper() != "[FCONTACT]") ? "Dear " + x2.InnerText : "[FCONTACT]");
        x2 = xn.SelectSingleNode("attach");
        Lbl_attachtype.Text = (x2 != null && x2.Attributes.GetNamedItem("id") != null && x2.Attributes.GetNamedItem("id").Value != "") ? x2.Attributes.GetNamedItem("id").Value : Lbl_attachtype.Text;
        if (Request.QueryString["Mailflg"] != null && Request.QueryString["Mailflg"].ToString()=="1" && Request.QueryString["custno"] == "2556")
        {
            x2 = xn.SelectSingleNode("second_to");
            lblToAddress.Text = (x2 != null && x2.InnerText != "") ? x2.InnerText : lblToAddress.Text;
            x2 = xn.SelectSingleNode("second_cc");
            lblCCAddress.Text = (x2 != null && x2.InnerText != "") ? x2.InnerText : lblCCAddress.Text;
            x2 = xn.SelectSingleNode("second_subject");
            lblSubject.Text = (x2 != null && x2.InnerText != "") ? x2.InnerText : lblSubject.Text;
            //lblSubject.Text = lblSubject.Text.Replace("[MONTHYEAR]", DateTime.Now.ToString("MMMMM") + " " + DateTime.Now.Year.ToString());
            x2 = xn.SelectSingleNode("second_body");
            lblBody.Text = (x2 != null && x2.InnerText != "") ? x2.InnerText : lblBody.Text;
            if(DateTime.Now.Day<=15)
            {
                lblBody.Text = lblBody.Text.Replace("[BATCH]","first");
                lblSubject.Text = lblSubject.Text.Replace("[BATCH]", "First");
            }
            else
            {
                lblBody.Text = lblBody.Text.Replace("[BATCH]", "second");
                lblSubject.Text = lblSubject.Text.Replace("[BATCH]", "Second");
            }
            //lblBody.Text = lblBody.Text.Replace("[MONTHYEAR]", DateTime.Now.ToString("MMMMM") + " " + DateTime.Now.Year.ToString());
        }
        lblSubject.Text = lblSubject.Text.Replace("[MONTHYEAR]", DateTime.Now.ToString("MMMMM") + " " + DateTime.Now.Year.ToString());
        lblBody.Text = lblBody.Text.Replace("[MONTHYEAR]", DateTime.Now.ToString("MMMMM") + " " + DateTime.Now.Year.ToString());
    }
    private string CreateAttachmentLink(int custno,int CustCategory,int contactno)
    {
        emailds = new DataSet();
        DataSet projectds = new DataSet();
        DataSet bookds = new DataSet();
        Datasource_IBSQL emailobj = new Datasource_IBSQL();
        if(custno==10098)//For Medknow customer
            sSite = ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString();
        else
            sSite = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString();
        string pname = "";
        string ForfileMissingAddJourcode = "";
        coaction_body_content = "";
         bool Mail_sent_flg = false;
        try
        {
            XmlDocument xattachdoc = new XmlDocument();
            XmlNode xattachnode;
            xattachdoc.Load(Server.MapPath("") + @"\email_multipleattachment.xml");
            if (CustCategory == 1 || CustCategory == 4)//For Journal || 4 For WIP
            {
                div_Attachments.InnerText = "";
                xattachnode=xattachdoc.SelectSingleNode("emailattachment/journal").SelectSingleNode("customer[@custno='"+ custno +"']");
                //For Multiple attachment
                if (xattachnode!=null && xattachnode.Attributes.GetNamedItem("custno").Value.ToString() == custno.ToString())
                //if (custno == 2556 || custno == 10081 || custno == 2052 || custno == 10105 || custno == 10089 || custno == 10124 || custno == 10119 )
                //For TandF || Co-Action || LUND - Ecology Building || San Lucas Medical Limited || Agricultural Institute of Canada || The African Field Epidemiology Network || Canadian Science Publishing 
                {
                    emailds = emailobj.GetDispatchedjobMail(custno, Convert.ToInt32(CustCategory), contactno,Convert.ToInt32(Request.QueryString["Mailflg"]));
                    if (emailds != null)
                    {
                        if (emailds.Tables[0].Rows.Count > 0 && custno == 10081)//For Co-Action
                            create_Label("Lbl_Coaction", "Co-Action - Issue");

                        if (emailds.Tables[0].Rows.Count > 0 && custno == 2052)//For LUND - Ecology Building
                            create_Label("Lbl_Coaction", "LUND - Ecology Building - Issue");

                        if (emailds.Tables[0].Rows.Count > 0 && custno == 10105)//For San Lucas Medical Limited
                            create_Label("Lbl_Coaction", "San Lucas Medical Limited - Issue");

                        if (emailds.Tables[0].Rows.Count > 0 && custno == 10089)//For Agricultural Institute of Canada
                            create_Label("Lbl_Coaction", "Agricultural Institute of Canada - Issue");

                        if (emailds.Tables[0].Rows.Count > 0 && custno == 2070)//For Agricultural Institute of Canada
                            create_Label("Lbl_Coaction", "Royal Irish Academy - Issue");

                        //if (custno == 10081)//For Co-Action
                        //{
                            lblSubject.Text = lblSubject.Text.Replace("[INVOICE]", (emailds.Tables[0].Rows.Count > 1) ? "Invoices" : "Invoice");
                            lblBody.Text = lblBody.Text.Replace("[INVOICE]", (emailds.Tables[0].Rows.Count > 1) ? "invoices" : "invoice");
                        //}

                        lblBody.Text = lblBody.Text.Replace("[SPREADSHEET]", (emailds.Tables[0].Rows.Count > 1) ? "spreadsheets" : "spreadsheet");
                        lblSubject.Text = lblSubject.Text.Replace("[SPREADSHEET]",(emailds.Tables[0].Rows.Count > 1) ? "spreadsheets" : "spreadsheet");
                        if (custno == 2556)//For tandf and wip only
                        {
                            lblBody.Text = lblBody.Text.Replace("[ISSUE]", (emailds.Tables[0].Rows.Count > 1) ? "jobs" : "job");
                            lblSubject.Text = lblSubject.Text.Replace("[ISSUE]", (emailds.Tables[0].Rows.Count > 1) ? "jobs" : "job");
                        }
                        else
                        {
                            lblBody.Text = lblBody.Text.Replace("[ISSUE]", (emailds.Tables[0].Rows.Count > 1) ? "issues" : "issue");
                            lblSubject.Text = lblSubject.Text.Replace("[ISSUE]", (emailds.Tables[0].Rows.Count > 1) ? "issues" : "issue");
                        }
                        lblSubject.Text = lblSubject.Text.Replace("[INVOICE]", (emailds.Tables[0].Rows.Count > 1) ? "Invoices" : "Invoice");
                        lblBody.Text = lblBody.Text.Replace("[INVOICE]", (emailds.Tables[0].Rows.Count > 1) ? "Invoices" : "Invoice");
                       
                        for (int c = 0; c < emailds.Tables[0].Rows.Count; c++)
                        {
                            if (Request.QueryString["custno"].ToString().Trim() == "10132")
                            {
                                if ((emailds.Tables[0].Rows[c]["IINNO"].ToString() == Request.QueryString["iinvoiceno"].ToString().Trim()))
                                {
                                    if (emailds.Tables[0].Rows[c]["INV_EMAIL_SENT1"].ToString() == "Y")//Check if Mail sent
                                        Mail_sent_flg = true;
                                    //if (emailds.Tables[0].Rows[c]["INV_EMAIL_SENT1"].ToString() != "Y")//Check For Already sent First Mail. Only allowed which are not sent First Mail
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(pname))
                                            pname += ", ";
                                        LinkButton Attach_LinkBtn = new LinkButton();
                                        ForfileMissingAddJourcode = emailds.Tables[0].Rows[c]["JOURCODE"].ToString().Trim() + emailds.Tables[0].Rows[c]["IISSUENO"].ToString().Trim();
                                        pname += ForfileMissingAddJourcode;
                                        Attach_LinkBtn.ID = emailds.Tables[0].Rows[c]["INO"].ToString();
                                        coaction_body_content += "Invoice No. " + emailds.Tables[0].Rows[c]["IINNO"].ToString() + " - " + ForfileMissingAddJourcode + "\r\n";
                                        string btntxt = (ForfileMissingAddJourcode.Contains("WIP")) ? "WIP_" + emailds.Tables[0].Rows[c]["IINNO"].ToString().Trim() : emailds.Tables[0].Rows[c]["IINNO"].ToString().Trim();
                                        Attach_LinkBtn.Text = (Lbl_attachtype.Text.IndexOf("xls") > 0 && Lbl_attachtype.Text != "") ? Lbl_attachtype.Text.Replace("[INVOICENO]", btntxt) : emailds.Tables[0].Rows[c]["JOURCODE"].ToString().Trim() + emailds.Tables[0].Rows[c]["IISSUENO"].ToString().Trim().Replace("/", "_") + ".pdf";
                                        Attach_LinkBtn = AttachmentLinkAttributes(Attach_LinkBtn, sSite + @"\" + Attach_LinkBtn.Text, ForfileMissingAddJourcode);

                                        div_Attachments.Controls.Add(Attach_LinkBtn);
                                        //div_Attachments.Controls.Add(new LiteralControl("<br/>"));
                                    }
                                }
                            }
                            else if (Request.QueryString["custno"].ToString().Trim() == "1023012" || Request.QueryString["custno"].ToString().Trim() == "10211" || Request.QueryString["custno"].ToString().Trim() == "1023221" || Request.QueryString["custno"].ToString().Trim() == "1021226")
                            {
                                if ((emailds.Tables[0].Rows[c]["IINNO"].ToString() == Request.QueryString["iinvoiceno"].ToString().Trim()))
                                {
                                    if (emailds.Tables[0].Rows[c]["INV_EMAIL_SENT1"].ToString() == "Y")//Check if Mail sent
                                        Mail_sent_flg = true;
                                    //if (emailds.Tables[0].Rows[c]["INV_EMAIL_SENT1"].ToString() != "Y")//Check For Already sent First Mail. Only allowed which are not sent First Mail
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(pname))
                                            pname += ", ";
                                        LinkButton Attach_LinkBtn = new LinkButton();
                                        ForfileMissingAddJourcode = emailds.Tables[0].Rows[c]["JOURCODE"].ToString().Trim() + emailds.Tables[0].Rows[c]["IISSUENO"].ToString().Trim();
                                        pname += ForfileMissingAddJourcode;
                                        Attach_LinkBtn.ID = emailds.Tables[0].Rows[c]["INO"].ToString();
                                        coaction_body_content += "Invoice No. " + emailds.Tables[0].Rows[c]["IINNO"].ToString() + " - " + ForfileMissingAddJourcode + "\r\n";
                                        string btntxt = (ForfileMissingAddJourcode.Contains("WIP")) ? "WIP_" + emailds.Tables[0].Rows[c]["IINNO"].ToString().Trim() : emailds.Tables[0].Rows[c]["IINNO"].ToString().Trim();
                                        Attach_LinkBtn.Text = ((Lbl_attachtype.Text.IndexOf("xls") > 0 && Lbl_attachtype.Text != "") ? Lbl_attachtype.Text.Replace("[INVOICENO]", btntxt) : emailds.Tables[0].Rows[c]["JOURCODE"].ToString().Trim() + emailds.Tables[0].Rows[c]["IISSUENO"].ToString().Trim().Replace("/", "_") + ".pdf").Replace(".xls",".pdf");
                                        Attach_LinkBtn = AttachmentLinkAttributes(Attach_LinkBtn, sSite + @"\" + Attach_LinkBtn.Text, ForfileMissingAddJourcode);

                                        div_Attachments.Controls.Add(Attach_LinkBtn);
                                        //div_Attachments.Controls.Add(new LiteralControl("<br/>"));
                                    }
                                }
                            }
                            else
                            {
                                if (emailds.Tables[0].Rows[c]["INV_EMAIL_SENT1"].ToString() == "Y")//Check if Mail sent
                                    Mail_sent_flg = true;
                                //if (emailds.Tables[0].Rows[c]["INV_EMAIL_SENT1"].ToString() != "Y")//Check For Already sent First Mail. Only allowed which are not sent First Mail
                                else
                                {
                                    if (!string.IsNullOrEmpty(pname))
                                        pname += ", ";
                                    LinkButton Attach_LinkBtn = new LinkButton();
                                    ForfileMissingAddJourcode = emailds.Tables[0].Rows[c]["JOURCODE"].ToString().Trim() + emailds.Tables[0].Rows[c]["IISSUENO"].ToString().Trim();
                                    pname += ForfileMissingAddJourcode;
                                    Attach_LinkBtn.ID = emailds.Tables[0].Rows[c]["INO"].ToString();
                                    coaction_body_content += "Invoice No. " + emailds.Tables[0].Rows[c]["IINNO"].ToString() + " - " + ForfileMissingAddJourcode + "\r\n";
                                    string btntxt = (ForfileMissingAddJourcode.Contains("WIP")) ? "WIP_" + emailds.Tables[0].Rows[c]["IINNO"].ToString().Trim() : emailds.Tables[0].Rows[c]["IINNO"].ToString().Trim();
                                    Attach_LinkBtn.Text = (Lbl_attachtype.Text.IndexOf("xls") > 0 && Lbl_attachtype.Text != "") ? Lbl_attachtype.Text.Replace("[INVOICENO]", btntxt) : emailds.Tables[0].Rows[c]["JOURCODE"].ToString().Trim() + emailds.Tables[0].Rows[c]["IISSUENO"].ToString().Trim().Replace("/", "_") + ".pdf";
                                    Attach_LinkBtn = AttachmentLinkAttributes(Attach_LinkBtn, sSite + @"\" + Attach_LinkBtn.Text, ForfileMissingAddJourcode);

                                    div_Attachments.Controls.Add(Attach_LinkBtn);
                                    //div_Attachments.Controls.Add(new LiteralControl("<br/>"));
                                }
                            }
                            
                        }
                        
                    }

                    if (Mail_sent_flg == true)
                    {
                        btnSubmit.Visible = false;
                        div_Attachments.InnerHtml = "<font color='red' size='4'><b>Already sent mail for this invoice</b></font>";
                    }

                    if (custno == 10081 || custno == 2070)//For Co-Action
                    {
                        div_Attachemt_Details.InnerHtml += "<br/><font size='2'><b>For Reference:</b></font>";
                        div_Attachemt_Details.InnerHtml += "<br/><font color='green'><b>Number of Issue Attachment: " + emailds.Tables[0].Rows.Count + "</b></font>";
                        //pname += (string.IsNullOrEmpty(pname)) ? ProjectAttachment_Withjournal("Co-Action - Project", custno, 3) : ", " + ProjectAttachment_Withjournal("Co-Action - Project", custno, 3);
                        

                    }
                 

                }
                else//For Other Journal
                {
                    emailds = emailobj.GetDispatchedjobMail(custno, Convert.ToInt32(CustCategory), contactno, Convert.ToInt32(Request.QueryString["Mailflg"]));
                    if (Request.QueryString["custno"].ToString().Trim() == "1023012")
                    {
                        if (emailds != null && emailds.Tables[0].Rows.Count == 0)
                        {
                            btnSubmit.Visible = false;
                            div_Attachments.InnerHtml = "<font color='red' size='4'><b>Already sent mail for this invoice</b></font>";
                            return "";
                        }
                        //if (!string.IsNullOrEmpty(pname))
                        //    pname += ", ";
                        LinkButton Attach_LinkBtn = new LinkButton();
                        ForfileMissingAddJourcode = emailds.Tables[0].Rows[0]["JOURCODE"].ToString().Trim() + emailds.Tables[0].Rows[0]["IISSUENO"].ToString().Trim();
                        pname += ForfileMissingAddJourcode;
                        Attach_LinkBtn.ID = emailds.Tables[0].Rows[0]["INO"].ToString();
                        coaction_body_content += "Invoice No. " + emailds.Tables[0].Rows[0]["IINNO"].ToString() + " - " + ForfileMissingAddJourcode + "\r\n";
                        string btntxt = (ForfileMissingAddJourcode.Contains("WIP")) ? "WIP_" + emailds.Tables[0].Rows[0]["IINNO"].ToString().Trim() : emailds.Tables[0].Rows[0]["IINNO"].ToString().Trim();
                        Attach_LinkBtn.Text = ((Lbl_attachtype.Text.IndexOf("xls") > 0 && Lbl_attachtype.Text != "") ? Lbl_attachtype.Text.Replace("[INVOICENO]", btntxt) : emailds.Tables[0].Rows[0]["JOURCODE"].ToString().Trim() + emailds.Tables[0].Rows[0]["IISSUENO"].ToString().Trim().Replace("/", "_") + ".pdf").Replace(".xls", ".pdf");
                        Attach_LinkBtn = AttachmentLinkAttributes(Attach_LinkBtn, sSite + @"\" + Attach_LinkBtn.Text, ForfileMissingAddJourcode);

                        div_Attachments.Controls.Add(Attach_LinkBtn);

                        //LinkButton Attach_LinkBtn = new LinkButton();
                        //ForfileMissingAddJourcode = emailds.Tables[0].Rows[0]["JOURCODE"].ToString().Trim() + emailds.Tables[0].Rows[0]["IISSUENO"].ToString().Trim();
                        //pname = ForfileMissingAddJourcode + ".pdf";
                        //Attach_LinkBtn.ID = emailds.Tables[0].Rows[0]["ino"].ToString();
                        //coaction_body_content += "Invoice No. " + emailds.Tables[0].Rows[0]["IINNO"].ToString() + " - " + ForfileMissingAddJourcode + "\r\n";
                        //Attach_LinkBtn.Text = (Lbl_attachtype.Text.IndexOf("xls") > 0 && Lbl_attachtype.Text != "") ? Lbl_attachtype.Text.Replace("[INVOICENO]", Request.QueryString["iinvoiceno"].ToString()) :  emailds.Tables[0].Rows[0]["JOURCODE"].ToString().Trim() + emailds.Tables[0].Rows[0]["IISSUENO"].ToString().Trim().Replace("/", "_") + ".pdf";
                        //Attach_LinkBtn = AttachmentLinkAttributes(Attach_LinkBtn, sSite + @"\" + ForfileMissingAddJourcode + ".pdf", ForfileMissingAddJourcode + ".pdf");
                        //div_Attachments.Controls.Add(Attach_LinkBtn);
                    }
                    else
                    {
                        if (emailds != null && emailds.Tables[0].Rows.Count == 0)
                        {
                            btnSubmit.Visible = false;
                            div_Attachments.InnerHtml = "<font color='red' size='4'><b>Already sent mail for this invoice</b></font>";
                            return "";
                        }
                        LinkButton Attach_LinkBtn = new LinkButton();
                        ForfileMissingAddJourcode = Request.QueryString["jourcode"].ToString().Trim() + Request.QueryString["issueno"].ToString().Trim();
                        pname = ForfileMissingAddJourcode;
                        Attach_LinkBtn.ID = Request.QueryString["ino"].ToString();
                        coaction_body_content += "Invoice No. " + emailds.Tables[0].Rows[0]["IINNO"].ToString() + " - " + ForfileMissingAddJourcode + "\r\n";
                        Attach_LinkBtn.Text = (Lbl_attachtype.Text.IndexOf("xls") > 0 && Lbl_attachtype.Text != "") ? Lbl_attachtype.Text.Replace("[INVOICENO]", Request.QueryString["iinvoiceno"].ToString()) : Request.QueryString["jourcode"].ToString().Trim() + Request.QueryString["issueno"].ToString().Trim().Replace("/", "_") + ".pdf";
                        Attach_LinkBtn = AttachmentLinkAttributes(Attach_LinkBtn, sSite + @"\" + Attach_LinkBtn.Text, ForfileMissingAddJourcode);
                        div_Attachments.Controls.Add(Attach_LinkBtn);
                    }
                    //storedprocedure(P_DISPATCHED_APPROVED_ITEMS_J) return only email flag ='N' so here check count =0
                    
                }
            }

            else if (CustCategory == 2)//For Book
            {
                emailds = emailobj.GetDispatchedjobMail(custno, Convert.ToInt32(CustCategory),Convert.ToInt32(Request.QueryString["BNO"]), Convert.ToInt32(Request.QueryString["Mailflg"]));
                if (emailds != null && emailds.Tables[0].Rows.Count == 0)
                {
                    btnSubmit.Visible = false;
                    div_Attachments.InnerHtml = "<font color='red' size='4'><b>Already sent mail for this invoice</b></font>";
                    return "";
                }
                LinkButton lbn = new LinkButton();
                pname += ForfileMissingAddJourcode;
                lbn.ID = Request.QueryString["BNO"];
                projectds = emailobj.GetEmailconfigDetails(Convert.ToInt32(Request.QueryString["BNO"]), CustCategory);
                btitle = projectds.Tables[0].Rows[0]["BTITLE"].ToString().Trim();
                //if (invoiceno != 27163 && invoiceno != 27164 && custno == 10085 && invoiceno >= 27122)//For Independent College
                if (custno == 10085 && Convert.ToInt32(Request.QueryString["binvoiceno"]) >= 27122)//For Independent College
                    ForfileMissingAddJourcode = Convert.ToInt32(Request.QueryString["binvoiceno"]).ToString().Trim();
                else
                    ForfileMissingAddJourcode = projectds.Tables[0].Rows[0]["BNUMBER"].ToString().Trim();
                lbn.Text = ForfileMissingAddJourcode + ".pdf";
                lbn = AttachmentLinkAttributes(lbn, sSite + @"\" + lbn.Text, ForfileMissingAddJourcode);
                div_Attachments.InnerText = "";
                div_Attachments.Controls.Add(lbn);
                lblSubject.Text = lblSubject.Text.Replace("[INVOICE]", (emailds.Tables[0].Rows.Count > 1) ? "Invoices" : "Invoice");
                lblBody.Text = lblBody.Text.Replace("[INVOICE]", (emailds.Tables[0].Rows.Count > 1) ? "Invoices" : "Invoice");

            }
            else if (CustCategory == 3)//For Project
            {
                div_Attachments.InnerText = "";
                xattachnode = xattachdoc.SelectSingleNode("emailattachment/project").SelectSingleNode("customer[@custno='" + custno + "']");
                if (xattachnode!=null && xattachnode.Attributes.GetNamedItem("custno").Value.ToString() == custno.ToString())
                //if (custno == 10081 || custno == 10127 || custno == 10119 || custno == 10130) //Bulk mail attachment based on conno 
                    //for coaction || Mercatus Center || Canadian Science Publishing || Termedia Publishing House
                {
                    emailds = emailobj.GetDispatchedjobMail_coaction(custno, Convert.ToInt32(CustCategory), Convert.ToInt32(Request.QueryString["conno"]), Convert.ToInt32(Request.QueryString["Mailflg"]));
                    lblSubject.Text = lblSubject.Text.Replace("[INVOICE]", (emailds.Tables[0].Rows.Count > 1) ? "Invoices" : "Invoice");
                    lblBody.Text = lblBody.Text.Replace("[INVOICE]", (emailds.Tables[0].Rows.Count > 1) ? "invoices" : "invoice");
                    for (int c = 0; c < emailds.Tables[0].Rows.Count; c++)
                    {
                        LinkButton lbn = new LinkButton();
                       
                        lbn.ID = emailds.Tables[0].Rows[c]["projectno"].ToString();
                        projectds = emailobj.GetEmailconfigDetails(Convert.ToInt32(emailds.Tables[0].Rows[c]["projectno"].ToString()), CustCategory);
                        ptitle = projectds.Tables[0].Rows[0]["PTITLE"].ToString().Trim();
                        if (custno == 10085 && Convert.ToInt32(Request.QueryString["invno"]) >= 27122)//For Independent College
                            ForfileMissingAddJourcode = Convert.ToInt32(Request.QueryString["invno"]).ToString().Trim();
                        else
                            ForfileMissingAddJourcode = projectds.Tables[0].Rows[0]["PCODE"].ToString().Trim();
                        if (!string.IsNullOrEmpty(pname))
                            pname = pname + "," + ForfileMissingAddJourcode;
                        else
                            pname = ForfileMissingAddJourcode;
                        coaction_body_content += "Invoice No. " + emailds.Tables[0].Rows[c]["invno"].ToString() + " - " + ForfileMissingAddJourcode + "\r\n";
                        lbn.Text = ForfileMissingAddJourcode + ".pdf";
                        lbn = AttachmentLinkAttributes(lbn, sSite + @"\" + lbn.Text, ForfileMissingAddJourcode);
                        div_Attachments.Controls.Add(lbn);
                    }
                    
                }
                else//Other projects
                {
                    emailds = emailobj.GetDispatchedjobMail(custno, Convert.ToInt32(CustCategory), Convert.ToInt32(Request.QueryString["projectno"]), Convert.ToInt32(Request.QueryString["Mailflg"]));
                    LinkButton lbn = new LinkButton();

                    pname += ForfileMissingAddJourcode;
                    lbn.ID = emailds.Tables[0].Rows[0]["projectno"].ToString();
                    projectds = emailobj.GetEmailconfigDetails(Convert.ToInt32(emailds.Tables[0].Rows[0]["projectno"].ToString()), CustCategory);
                    ptitle = projectds.Tables[0].Rows[0]["PTITLE"].ToString().Trim();
                    if (custno == 10085 && Convert.ToInt32(Request.QueryString["invno"]) >= 27122)//For Independent College
                        ForfileMissingAddJourcode = Convert.ToInt32(Request.QueryString["invno"]).ToString().Trim();
                    else
                        ForfileMissingAddJourcode = projectds.Tables[0].Rows[0]["PCODE"].ToString().Trim();
                    lbn.Text = ForfileMissingAddJourcode.Replace("/","_") + ".pdf";
                    lbn = AttachmentLinkAttributes(lbn, sSite + @"\" + lbn.Text, ForfileMissingAddJourcode);
                    div_Attachments.Controls.Add(lbn);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            emailds = null;
            emailobj = null;
            projectds = null;
            bookds = null;
        }
        return pname;

    }
    private LinkButton AttachmentLinkAttributes(LinkButton attachlink,string filepath,string FileMissingJourcode)
    {
        //Comment for test 26 May 2010
        if (!File.Exists(filepath.Trim()))
        {
            attachlink.Text = "File Missing!... Please generate Invoice for " + FileMissingJourcode;
            lblMessage.Text = "Attachment Missing, Unable to proceed...";
            btnSubmit.Enabled = false;
        }
        attachlink.CssClass = "error";
        attachlink.Attributes.Add("style", "display:block;text-align:left;Font-size:10pt;");
        attachlink.Click += new EventHandler(Attachment_View_Click);
        attachlink.CommandArgument = attachlink.Text;
        return attachlink;
    }
    private string BulkAttachementTandF(int custno,string flg)
    {
        datasourceIB tandf_obj = new datasourceIB();
        DataSet tandf_ds = new DataSet();
        sSite = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString();
        string ForfileMissingAddJourcode = "";
        string pname = "";
        int TandF_Attach_Count = 0;
        int WIP_Attach_Count = 0;
        int Psychology_Attach_Count = 0;
        
        try
        {
            tandf_ds = tandf_obj.GetDispatchedjobMail(custno, Convert.ToInt32(Request.QueryString["category"]), 0, Convert.ToInt32(Request.QueryString["Mailflg"]));
            if (tandf_ds != null)
            {
                Boolean Sent_Email_Flag = false;
                string Str_TandF = "";
                string Str_Psycho = "";
                for (int rd_cnt = 0; rd_cnt < tandf_ds.Tables[0].Rows.Count; rd_cnt++)
                {
                    if (tandf_ds.Tables[0].Rows[rd_cnt]["INV_EMAIL_SENT2"].ToString() == "Y")//set Flag for already Mail sent invoice
                        Sent_Email_Flag = true;
                    else
                    {
                        LinkButton Attach_LinkBtn = new LinkButton();
                        ForfileMissingAddJourcode = tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim() + tandf_ds.Tables[0].Rows[rd_cnt]["IISSUENO"].ToString().Trim();
                        pname += ForfileMissingAddJourcode;
                        Attach_LinkBtn.ID = tandf_ds.Tables[0].Rows[rd_cnt]["INO"].ToString();
                        if (tandf_ds.Tables[0].Rows[rd_cnt]["cno"].ToString() == "2556")//For TandF
                        {
                            if(string.IsNullOrEmpty(Str_TandF))
                            {
                                Str_TandF = "TandF";
                                create_Label("Lbl_TandF", "TandF - Issue");
                            }
                            if (tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim().Contains("WIP"))
                            {
                                Attach_LinkBtn.Text = tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim() + ".pdf";
                                WIP_Attach_Count += 1;
                            }
                            else
                            {
                                Attach_LinkBtn.Text = tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim() + tandf_ds.Tables[0].Rows[rd_cnt]["IISSUENO"].ToString().Trim().Replace("/", "_") + " - " + tandf_ds.Tables[0].Rows[rd_cnt]["IINNO"].ToString() + ".pdf";
                                TandF_Attach_Count += 1;
                            }
                        }
                        else //Psychology Press
                        {
                            if (string.IsNullOrEmpty(Str_Psycho))
                            {
                                Str_Psycho = "Psychology";
                                create_Label("Lbl_Psycho", "Psychology Press");
                            }
                            Attach_LinkBtn.Text = tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim() + tandf_ds.Tables[0].Rows[rd_cnt]["IISSUENO"].ToString().Trim().Replace("/", "_") + ".pdf";
                            Psychology_Attach_Count += 1;
                        }
                        Attach_LinkBtn = AttachmentLinkAttributes(Attach_LinkBtn, sSite + Attach_LinkBtn.Text, ForfileMissingAddJourcode);

                        div_Attachments.Controls.Add(Attach_LinkBtn);
                    }
                }
                tandf_ds = null;

                //For Attachment Count details
                if (TandF_Attach_Count > 0 || Psychology_Attach_Count > 0)
                {
                    div_Attachemt_Details.InnerHtml = "<br/><font size='2'><b>For Reference:</b></font>";
                    if (TandF_Attach_Count > 0)
                        div_Attachemt_Details.InnerHtml += "<br/><font color='green'><b>No.of TandF-Issue Attachment: " + TandF_Attach_Count + "</b></font>";
                    if (WIP_Attach_Count > 0)
                        div_Attachemt_Details.InnerHtml += "<br/><font color='green'><b>No.of WIP Attachment: " + WIP_Attach_Count + "</b></font>";
                    if (Psychology_Attach_Count > 0)
                        div_Attachemt_Details.InnerHtml += "<br/><font color='green'><b>No.of Psychology Press Attachment: " + Psychology_Attach_Count + "</b></font>";

                }

                //Add Invoice if tandf customer exists in projects
                ProjectAttachment_Withjournal("TandF - Projects", custno, 3);

               
                if (Sent_Email_Flag == true)
                {
                    btnSubmit.Visible = false;
                    div_Attachments.InnerHtml = "<font color='red' size='4'><b>Already sent mail for this invoice</b></font>";
                }
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { tandf_ds = null; tandf_obj = null; }
        return pname;
    }
    private string ProjectAttachment_Withjournal(string Project_Lbl, int cust_no,int category)
    {

        //Add Invoice if exists in projects
        datasourceIB project_obj = new datasourceIB();
        DataSet project_ds = null;
        string ForfileMissingAddJourcode = "";
        string pname = "";
        int Project_Attach_Count = 0;
        project_ds = project_obj.GetDespatchedJobs2(cust_no, category);
        if (project_ds != null)
        {
            //For Project Label add
            if (project_ds.Tables[0].Rows.Count > 0)
                create_Label("Lbl_PTandF", Project_Lbl);

            for (int ptandf_cnt = 0; ptandf_cnt < project_ds.Tables[0].Rows.Count; ptandf_cnt++)
            {
                Label pattach_Lbl = new Label();
                LinkButton pAttach_Lbtn = new LinkButton();
                ForfileMissingAddJourcode = project_ds.Tables[0].Rows[ptandf_cnt]["pcode"].ToString().Trim();
                pname += ForfileMissingAddJourcode;
                pAttach_Lbtn.ID = project_ds.Tables[0].Rows[ptandf_cnt]["projectno"].ToString();
                pattach_Lbl.ID = project_ds.Tables[0].Rows[ptandf_cnt]["projectno"].ToString() + "_Lbl";
                pattach_Lbl.Text = project_ds.Tables[0].Rows[ptandf_cnt]["projectno"].ToString();
                pattach_Lbl.Visible = false;
                coaction_body_content += "Invoice No. " + project_ds.Tables[0].Rows[ptandf_cnt]["INVNO"].ToString() + " - " + ForfileMissingAddJourcode + "\r\n" ;
                pAttach_Lbtn.Text = ForfileMissingAddJourcode + ".pdf";
                pAttach_Lbtn = AttachmentLinkAttributes(pAttach_Lbtn, sSite + @"\" + pAttach_Lbtn.Text, ForfileMissingAddJourcode);
                div_Attachments.Controls.Add(pattach_Lbl);
                div_Attachments.Controls.Add(pAttach_Lbtn);
                Project_Attach_Count += 1;
            }
            if (Project_Attach_Count > 0)
                div_Attachemt_Details.InnerHtml += "<br/><font color='green' ><b>No.of " + Project_Lbl + " Attachment: " + Project_Attach_Count + "</b></font><br/><br/>";
        }
        return pname;//Return for co-action

    }
    private void create_Label(string Label_id,string Label_txt)
    {

        Label Lbl_Category = new Label();
        Lbl_Category.ID = Label_id;
        Lbl_Category.Text = "<font size='2'><b>" + Label_txt + "</b></font>";
        div_Attachments.Controls.Add(Lbl_Category);
    }
    protected void Attachment_View_Click(object sender, EventArgs e)
    {
        LinkButton Attachmemt_LnkBtn = sender as LinkButton;
        string pathfname = Attachmemt_LnkBtn.CommandArgument.ToString();
        if(Convert.ToInt32(Request.QueryString["custno"])==10098)//For Medknow
            sSite = ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString();
        else
            sSite = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString();
        pathfname = sSite + @"\" + pathfname;
        ExcelPDFExport(Path.GetExtension(pathfname), pathfname);
    }

    protected void Btn_ok_Click(object sender, EventArgs e)
    {
        try
        {
            if (File.Exists(file_upload.PostedFile.FileName))
            {
                new_attachstr += file_upload.PostedFile.FileName.ToString() + ",";
                addnewattachment(new_attachstr);
            }
            else
                throw new ArgumentException("File does not exists <br/>" + file_upload.PostedFile.FileName.ToString());

        }
        catch (Exception ex)
        { div_notupdateinvoice.InnerHtml += "<font color='red'>" + ex.Message.ToString() + "</font>"; }
    }

    private void addnewattachment(string attach)
    {
        if (!string.IsNullOrEmpty(attach))
        {
            string[] nattach;
            nattach = attach.Split(',');
            div_newattachment.InnerText = "";
            Table t = new Table();
            t.CellPadding = 0;
            t.CellSpacing = 0;
            t.CssClass = "bordertable";
            TableHeaderRow th = new TableHeaderRow();
            TableHeaderCell thc = new TableHeaderCell();
            thc.Text = "Attachment File List";
            thc.Attributes.Add("style", "border-bottom: 1px solid green;");
            th.Cells.Add(thc);
            thc = new TableHeaderCell();
            thc.Text = "";
            thc.Attributes.Add("style", "border-bottom: 1px solid green;");
            th.Cells.Add(thc);
            thc = new TableHeaderCell();
            thc.Text = "Delete";
            thc.Attributes.Add("style", "border-bottom: 1px solid green;");
            th.Cells.Add(thc);
            t.Rows.Add(th);

            for (int acnt = 0; nattach.Length > acnt; acnt++)
            {
                if (!string.IsNullOrEmpty(nattach[acnt].ToString()))
                {

                    TableRow tr = new TableRow();
                    TableCell tc = new TableCell();
                    HtmlAnchor hlink = new HtmlAnchor();
                    hlink.HRef = nattach[acnt].ToString();
                    hlink.InnerText = nattach[acnt].ToString();
                    //hlink.ID = file_upload.FileName.ToString();
                    hlink.Target = "_blank";
                    tc.Controls.Add(hlink);
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    ImageButton img = new ImageButton();
                    img.ImageUrl = @"images/tools/no.png";
                    img.ToolTip = "Delete";
                    img.ID = "Img_" + acnt;
                    img.Click += new ImageClickEventHandler(Delete_attachment);
                    img.CommandArgument = nattach[acnt].ToString();
                    tc.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                    tc.Controls.Add(img);
                    tr.Cells.Add(tc);
                    tr.Attributes.Add("style", "padding-top:5px;");
                    t.Rows.Add(tr);

                }
            }
            div_newattachment.Controls.Add(t);
        }
        else
            div_newattachment.InnerText = "";
    }

    protected void Delete_attachment(object sender, EventArgs e)
    {
        ImageButton imgbtn = sender as ImageButton;
        if (new_attachstr.IndexOf(imgbtn.CommandArgument) >= 0)
        {
            new_attachstr = new_attachstr.Replace(imgbtn.CommandArgument + ",", "");
            addnewattachment(new_attachstr);
        }
    }
    protected void btnUploadPDF_Click(object sender, ImageClickEventArgs e)
    {
        //FtpWebRequest requestFTPUploader;
        //FileInfo fileInfo;
        //FileStream fileStream;
        //Stream uploadStream;
        //int bufferLength = 2048;
        //int contentLength;

        Datasource_IBSQL tandf_obj = new Datasource_IBSQL();
        DataSet tandf_ds = new DataSet();
        sSite = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString();
        Local_path = ConfigurationManager.ConnectionStrings["PDFFileLocalCopy_PDF"].ToString();
        string ForfileMissingAddJourcode = "";
        string Filename = "";
        string Filename_copy = "";
       
         
        bool updates = false;
        try
        {
            tandf_ds = tandf_obj.GetDispatchedjobMail(Convert.ToInt32(Request.QueryString["custno"]), Convert.ToInt32(Request.QueryString["category"]), 0, Convert.ToInt32(Request.QueryString["Mailflg"]));
            if (tandf_ds != null)
            {
                Boolean Sent_Email_Flag = false;
                for (int rd_cnt = 0; rd_cnt < tandf_ds.Tables[0].Rows.Count; rd_cnt++)
                {
                    if (tandf_ds.Tables[0].Rows[rd_cnt]["INV_EMAIL_SENT2"].ToString() == "Y" && tandf_ds.Tables[0].Rows[rd_cnt]["INV_EMAIL_SENT1"].ToString() == "Y")//set Flag for already Mail sent invoice
                        Sent_Email_Flag = true;
                    else
                    {
                        ForfileMissingAddJourcode = tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim() + tandf_ds.Tables[0].Rows[rd_cnt]["IISSUENO"].ToString().Trim();
                        // Attach_LinkBtn.ID = tandf_ds.Tables[0].Rows[rd_cnt]["INO"].ToString();
                        if (tandf_ds.Tables[0].Rows[rd_cnt]["cno"].ToString() == "2556")//For TandF
                        {

                            if (tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim().Contains("WIP"))
                            {
                                Filename_copy = "Datapage_WIP_" + tandf_ds.Tables[0].Rows[rd_cnt]["IINNO"].ToString().Trim() + ".pdf";
                            }
                            else
                            {
                                Filename_copy = "Datapage_" + tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim() + tandf_ds.Tables[0].Rows[rd_cnt]["IISSUENO"].ToString().Trim().Replace("/", "_") + " - " + tandf_ds.Tables[0].Rows[rd_cnt]["IINNO"].ToString() + ".pdf";

                            }
                        }

                    }


                    
                    Filename = sSite + Filename_copy;


                    if (Directory.Exists(Local_path))
                    {
                        File.Copy(Path.Combine(sSite, Filename_copy), Path.Combine(Local_path, Filename_copy), true);
                    }
                    else
                        msg = "Path not found, Contact IT team!";

                   // Filename = "";

                    //UploadFileToFTP(Filename);
                   
                }
                msg = "File copied successfully";
                tandf_ds = null;
            }
        }
        catch (Exception ex)
        {
            msg = ex.ToString() + "Contact IT team";
            ClientScript.RegisterStartupScript(this.GetType(), "MsgBox", "<script language='javascript'>alert('" + msg + "');</script>");
            //throw ClientScript.RegisterStartupScript(this.GetType(), "MsgBox", "<script language='javascript'>alert('" + ex.ToString() + "');</script>");
        }
        finally
        {
            ClientScript.RegisterStartupScript(this.GetType(), "MsgBox", "<script language='javascript'>alert('" + msg + "');</script>");
            tandf_ds = null; tandf_obj = null;
        
        }
    }
    private static void UploadFileToFTP(string source)
    {
        string sftp_Servername = "informa.ftpstream.com";
        string sftp_username = "datapageprod";
        string sftp_password = "P4g3dat4!";
        int sftp_port = 22;
        //Sftp SFTP = new Rebex.Net.Sftp();
        
        try
        {
            // Sftp SFTP = new Tamir.SharpSsh.Sftp(sftp_Servername, sftp_username, sftp_password);
            // SFTP.Connect();           
            //SFTP.Put(source, sftp_Servername);
            // //Access = true;
            // SFTP.Close();
            //Sftp SFTP = new Sftp();
            ////SshPrivateKey sshkey = new SshPrivateKey(
            //Rebex.Licensing.Key = "==As1+I55Nz6fpF5AibA0/hVZ8krUjFztUawil6Z8KPFeg==";
            //SFTP.Connect(sftp_Servername);
            //SFTP.Login(sftp_username, sftp_password);

            //SFTP.PutFile(




        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
           
        }
    }
    protected void btnUploadXML_Click(object sender, ImageClickEventArgs e)
    {
        //FtpWebRequest requestFTPUploader;
        //FileInfo fileInfo;
        //FileStream fileStream;
        //Stream uploadStream;
        //int bufferLength = 2048;
        //int contentLength;

        Datasource_IBSQL tandf_obj = new Datasource_IBSQL();
        DataSet tandf_ds = new DataSet();
        xml_path = ConfigurationManager.ConnectionStrings["XMLFilePathDub"].ToString();
        Local_path_XML = ConfigurationManager.ConnectionStrings["PDFFileLocalCopy_XML"].ToString();
        string ForfileMissingAddJourcode = "";
        string Filename = "";
        string Filename_copy_XML = "";
        try
        {
            tandf_ds = tandf_obj.GetDispatchedjobMail(Convert.ToInt32(Request.QueryString["custno"]), Convert.ToInt32(Request.QueryString["category"]), 0, Convert.ToInt32(Request.QueryString["Mailflg"]));
            if (tandf_ds != null)
            {
                Boolean Sent_Email_Flag = false;
                for (int rd_cnt = 0; rd_cnt < tandf_ds.Tables[0].Rows.Count; rd_cnt++)
                {
                    bool updates = false;
                    if (tandf_ds.Tables[0].Rows[rd_cnt]["INV_EMAIL_SENT2"].ToString() == "Y" && tandf_ds.Tables[0].Rows[rd_cnt]["INV_EMAIL_SENT1"].ToString() == "Y")//set Flag for already Mail sent invoice
                        Sent_Email_Flag = true;
                    else
                    {
                        ForfileMissingAddJourcode = tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim() + tandf_ds.Tables[0].Rows[rd_cnt]["IISSUENO"].ToString().Trim();
                        // Attach_LinkBtn.ID = tandf_ds.Tables[0].Rows[rd_cnt]["INO"].ToString();
                        if (tandf_ds.Tables[0].Rows[rd_cnt]["cno"].ToString() == "2556")//For TandF
                        {

                            if (tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim().Contains("WIP"))
                            {
                                Filename_copy_XML = "Datapage_WIP_" + tandf_ds.Tables[0].Rows[rd_cnt]["IINNO"].ToString().Trim() + ".xml";
                            }
                            else
                            {
                                Filename_copy_XML = "Datapage_" + tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim() + tandf_ds.Tables[0].Rows[rd_cnt]["IISSUENO"].ToString().Trim().Replace("/", "_") + " - " + tandf_ds.Tables[0].Rows[rd_cnt]["IINNO"].ToString() + ".xml";

                            }
                        }

                    }


                    Filename = xml_path + Filename_copy_XML;


                    if (Directory.Exists(Local_path_XML))
                    {
                        File.Copy(Path.Combine(xml_path, Filename_copy_XML), Path.Combine(Local_path_XML, Filename_copy_XML), true);
                    }
                    else
                        msg = "Path not found, Contact IT team!";

                    // Filename = "";

                    //UploadFileToFTP(Filename);

                }
                msg = "File copied successfully";

                //Filename = xml_path + Filename;

                // UploadToFTP("ftp://182.72.98.20", Filename, "athena", "A+l7n@21");
                // Filename = "";
                // UploadFileToFTP(Filename);
                //if (updates == true)
                //{
                //    if (tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim().Contains("WIP"))
                //    {

                //        biz_IB bib = new biz_IB();

                //        if (bib.UpdatewipInvCompleted(invoiceupdatewip, Convert.ToInt32(Request.QueryString["custno"]), Convert.ToInt32(Request.QueryString["Mailflg"])) == false)

                //            lblMessage.Text += "<br/>Invoice status not as completed! Please contact IT Team [software@datapage.org]";


                //    }

                //}
            }
        }
        // tandf_ds = null;
        catch (Exception ex)
        { throw ex; }
        finally
        { }
    }

    protected void InvClear_Click(object sender, ImageClickEventArgs e)
    {
        Datasource_IBSQL tandf_obj = new Datasource_IBSQL();
        DataSet tandf_ds = new DataSet();
        try
        {
            tandf_ds = tandf_obj.GetDispatchedjobMail(Convert.ToInt32(Request.QueryString["custno"]), Convert.ToInt32(Request.QueryString["category"]), 0, Convert.ToInt32(Request.QueryString["Mailflg"]));
            if (tandf_ds != null)
            {
                for (int rd_cnt = 0; rd_cnt < tandf_ds.Tables[0].Rows.Count; rd_cnt++)
                {

                    if (tandf_ds.Tables[0].Rows[rd_cnt]["cno"].ToString() == "2556")//For TandF
                    {
                        if (tandf_ds.Tables[0].Rows[rd_cnt]["JOURCODE"].ToString().Trim().Contains("WIP"))
                        {
                            string uqry = "UPDATE wiparticles_dp SET EMAIL_FLAG='Y',INV_EMAIL_SENT1='Y',INV_EMAIL_SENT2='Y' WHERE WINVOICENO in (" + tandf_ds.Tables[0].Rows[rd_cnt]["iinno"].ToString() + ")";
                            tandf_obj.ExcuteProc(uqry);
                        }
                        else
                        {
                            string uqry = "UPDATE ISSUE_DP SET IINVOICED = 'Y',IEMAIL_FLAG='Y',IEMAIL_FLAG_IND='Y',INV_EMAIL_SENT1='Y',INV_EMAIL_SENT2='Y' WHERE IINVOICENO in(" + tandf_ds.Tables[0].Rows[rd_cnt]["iinno"].ToString() + ")";
                            tandf_obj.ExcuteProc(uqry);

                        }
                    }
                    else
                    {
                        if (Request.QueryString["custno"].ToString().Trim() == "10132")
                        {
                            if ((tandf_ds.Tables[0].Rows[rd_cnt]["IINNO"].ToString() == Request.QueryString["iinvoiceno"].ToString().Trim()))
                            {
                                string uqry = "UPDATE ISSUE_DP SET IINVOICED = 'Y',IEMAIL_FLAG='Y',IEMAIL_FLAG_IND='Y',INV_EMAIL_SENT1='Y',INV_EMAIL_SENT2='Y' WHERE IINVOICENO in(" + tandf_ds.Tables[0].Rows[rd_cnt]["iinno"].ToString() + ")";
                                tandf_obj.ExcuteProc(uqry);
                            }
                        }
                        else
                        {
                            string uqry = "";
                            if (Request.QueryString["custno"].ToString().Trim() == "10229" || Request.QueryString["custno"].ToString().Trim() == "10236" ||
                                Request.QueryString["custno"].ToString().Trim() == "10201" || Request.QueryString["custno"].ToString().Trim() == "10216" ||
                                Request.QueryString["custno"].ToString().Trim() == "10081" || Request.QueryString["custno"].ToString().Trim() == "10190" ||
                                Request.QueryString["custno"].ToString().Trim() == "10119" || Request.QueryString["custno"].ToString().Trim() == "10117" ||
                                Request.QueryString["custno"].ToString().Trim() == "10233")
                            {
                                 uqry = "UPDATE ISSUE_DP SET IINVOICED = 'Y',IEMAIL_FLAG='Y',IEMAIL_FLAG_IND='Y',INV_EMAIL_SENT1='Y',INV_EMAIL_SENT2='Y' WHERE IINVOICENO in(" + tandf_ds.Tables[0].Rows[rd_cnt]["iinno"].ToString() + ")";
                            }
                            else
                            {
                                 uqry = "UPDATE ISSUE_DP SET IINVOICED = 'Y',IEMAIL_FLAG='Y',IEMAIL_FLAG_IND='Y',INV_EMAIL_SENT1='Y',INV_EMAIL_SENT2='Y' WHERE IINVOICENO in(" + Request.QueryString["iinvoiceno"].ToString() + ")";
                            }
                            tandf_obj.ExcuteProc(uqry);
                        }
                    }
                    
                }
                tandf_ds = null;
            }
        }
        catch (Exception ex)
        {
            
        }
        finally
        {
            tandf_ds = null; tandf_obj = null;

        }
    }
}

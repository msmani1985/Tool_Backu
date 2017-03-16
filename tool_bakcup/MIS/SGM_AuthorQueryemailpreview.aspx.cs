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
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Net.Mime;


public partial class SGM_AuthorQueryemailpreview : System.Web.UI.Page
{
    string job_id;
    string job_id2;
    string job_type_id = "5";
    string emp_id;
    bool follow_email = false;
    bool remainder_email = false;
    bool MailSent = false;
    string strFileName_path = ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString();
    string spath = "";
    string jourcode, stypename,mailtype = "";
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

            if (!Page.IsPostBack)
            {

                jourcode = Request.QueryString["jourcode"].ToString().Trim();
                stypename = Request.QueryString["stypeno"].ToString().Trim();
                mailtype = Request.QueryString["mailtype"].ToString().Trim();
                if (mailtype == "1")
                {
                    if (jourcode == "JMMCR")
                    {
                        if (stypename == "S100")
                        {
                            //FollowUpMailSent_JMMCRwithoutImage();
                            FollowUpMailSent_JMMCRwithImage();
                        }
                        else
                        {
                            FollowUpMailSent_JMMCRwithImage();
                            Session["srcPath"] = "";
                        }
                    }
                    else if (jourcode == "MGEN")
                    {
                        if (stypename == "S100")
                        {
                            //FollowUpMailSent_MGENwithoutImage();
                            FollowUpMailSent_MGENwithImage();
                        }
                        else
                        {
                            FollowUpMailSent_MGENwithImage();
                            Session["srcPath"] = "";
                        }

                    }
                    else if (jourcode == "IJS")
                    {
                        if (stypename == "S100")
                        {
                            //FollowUpMailSent_MGENwithoutImage();
                            FollowUpMailSent_IJSEMwithImage();
                        }
                        else
                        {
                            FollowUpMailSent_IJSEMwithImage();
                            Session["srcPath"] = "";
                        }

                    }
                    else
                    {
                       // FollowUpMailSent();
                    }
                }
                else if (mailtype=="2")
                {
                    Template_2();
                }
                else if (mailtype == "3")
                {
                    Template_3();
                }
                else if (mailtype == "4")
                {
                    Template_4();
                }
            }

        }
    }

    protected void Template_2()
    {
        StringBuilder MyStringBuilder = new StringBuilder();

        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        AlternateView View;
        LinkedResource resource;
     
        dst = dsql.ExcProcedure("spGet_SGM_Email_Content", new string[,] { { "@jobid", job_id.ToString() }, { "@jobtypeid", job_type_id.ToString() }, { "@followemail", follow_email.ToString() }, { "@reminderemail", remainder_email.ToString() } }, CommandType.StoredProcedure);

        if (dst != null)
        {
            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    lblToAddress.Text = samrow["AEMAIL"].ToString().Trim();
                    lblCCAddress.Text = "cwgms@charlesworth-group.com";
                    lblBCCAddress.Text = "sgmpublishing-production@microbiologysociety.org";
                    
                     string strDIONo = Convert.ToString(samrow["DOINO"]);
                     string jourcode = Convert.ToString(samrow["JOURCODE"]);

                    if (strDIONo.IndexOf("/") != -1)
                    {
                        strDIONo = strDIONo.Substring(strDIONo.IndexOf("/") + 1).TrimEnd();
                        //strDIONo = strDIONo.Replace(".0.", "");
                        strDIONo = strDIONo.Substring(strDIONo.Length - 6);
                    }

                    lblSubject.Text = "" + jourcode.TrimEnd() + " Author Proof " + strDIONo;
 


                    string strArticle_Tittle = Convert.ToString(samrow["AMNsTITLE"]);

                   
                    if (samrow["job_stage_name"].ToString().Trim() == "S100")
                    {
                        Session["srcPath"] = "";
                    }

                    string strLog = string.Empty;

                    strLog = "<font face='Calibri';font-size:11pt;'> Dear Author";
                    strLog += "<br/><br/>";
                    strLog += "Re <b>" + strArticle_Tittle + "</b>";
                    strLog += "<br/><br/>";
                    strLog += " We recently sent you the PDF proof for the manuscript above. According to our records, we have not yet received any proof corrections from you. The proofs for your paper are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html. ";
                    strLog += "<br/><br/>";
                    strLog += "Please return any required corrections and answers to editorial queries (if present) to the Production Team at Charlesworth Group by email (cwgms@charlesworth-group.com) within 2 days of receipt of this message. Note that this is different to the address that this mail has been sent from. Please copy and paste this email address into the ‘to’ field of your reply rather than replying to this message.";
                    strLog += "<br/><br/>";
                    strLog += "Please read the PDF (you can print it if you wish). To mark your corrections, you can either choose to send your corrections in an email, detailing clearly each correction with a page number, or you can add your comments to the PDF by following the instructions below. Please return your comments by email to cwgms@charlesworth-group.com. Simple changes not requiring a copy of the proof can be sent by email. Keep a copy of your corrections for your own records.";
                    strLog += "<br/><br/>";
                    strLog += "Only typographical and absolutely essential factual corrections will be accepted at this stage. Stylistic changes, particularly if these relate to large portions of text will not be made. You may be charged for correction of your non-typographical errors.";
                    strLog += "<br/><br/>";
                    strLog += "This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.";
                    strLog += "<br/><br/>";
                    strLog += "Return your corrected proof to:";
                    strLog += "<br/><br/>";
                    strLog += "email: cwgms@charlesworth-group.com";
                    strLog += "<br/><br/>";

                    strLog += "<font color='Blue'> Using Adobe Mark-up Tools</font>";

                    strLog += "<br/><br/>";
                    strLog += "To mark up corrections using Adobe Reader (it can be downloaded for free if you don't have it), you just need to make sure the correct toolbars on the PDF are activated. ";
                    strLog += "<br/><br/>";
                    strLog += "Please mark up the text using the tools in the 'Comment and mark ups' toolbar. This includes tools such as a sticky note, text edits tools, arrows and boxes. To make this appear, click on the 'Comment' drag down bar at the top of the PDF, then select 'Show Comment & Markup Toolbar'. ";
                    strLog += "<br/><br/>";
                    //  strLog+="<img src=\"images/MGEN.png\"></img>";
                    strLog += "<img src=cid:companylogo>";
                    strLog += "<br/><br/>";
                    strLog += "If you can't get the mark-up tools to work, there's possibility that the version of the document that we sent you has mistakenly not been activated to permit editing. In that case please get in touch immediately and we'll send you over a replacement file, with editing enabled.";
                    strLog += "<br/><br/>";
                    strLog += "Please note that in order to make sure the editors and typesetters understand your corrections, all mark ups should be very clear, and all instructions unambiguous. In order to assure this, we ask you to mark up corrections in ways listed below.";
                    strLog += "<br/><br/>";
                    strLog += "<font color='Blue'>Specific Text Changes</font>";
                    strLog += "<br/><br/>";
                    strLog += "For specific changes to the text, i.e. deletions, insertions and replacements, please use the 'Text edits' tools as much as possible (you can select these from the Comments & Mark-up toolbar). These are much more accurate than sticky notes. ";
                    strLog += "<br/><br/>";
                    strLog += "To <b>insert text,</b> , you need to click at the exact place you want the text to be inserted, then start typing. The text will then appear in a window so you can see what you're typing.";
                    strLog += "<br/><br/>";

                    strLog += "To <b>replace text,</b> highlight the text to be replaced, then start typing (again, the text will appear in a window as you type). When you insert or amend a character the marking on the page will be a very small blue or red marking.";
                    strLog += "<br/><br/>";

                    strLog += "To <b>format the text</b> you've inserted or replaced (i.e. change it to bold or italics, or back to Roman) you should type the additional/replacement text as described above so that it appears in a window, then highlight the text and right click with the mouse. You can then select 'text styles' and select bold, italic etc.";
                    strLog += "<br/><br/>";
                    strLog += "To <b>delete text</b>, highlight the text for deletion and press delete. You don't need to worry about the typesetters missing these marks, as they have a way of generating a list of all the comments added to the document to ensure they perform a thorough check and pick up all the corrections (rather than scanning the document for where changes have been added). ";
                    strLog += "<br/><br/>";
                    strLog += "Please make sure that all of your mark ups are given as instructions for the typesetter; don't write your corrections in a way that the editor has to read them all and change them for the typesetter. If you do have any queries/issues you'd like the editor to look at, please either put these in a sticky note and begin with 'NOTE TO EDITOR', or list your queries in an email, giving the exact paragraph and page number your query relates to.";
                    strLog += "<br/><br/></font>";
                    //strLog += "Best wishes, <br/>";
                    //strLog += "Soundar<br/> </font>";
                   



                    string mailbody = "<html><body><code  style='font-family:Calibri;font-size:11pt;'>" + strLog.ToString() + "</code></body></html>";

                    lblBody.Text = strLog.ToString();



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
    protected void Template_3()
    {
        StringBuilder MyStringBuilder = new StringBuilder();

        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        AlternateView View;
        LinkedResource resource;
        dst = dsql.ExcProcedure("spGet_SGM_Email_Content", new string[,] { { "@jobid", job_id.ToString() }, { "@jobtypeid", job_type_id.ToString() }, { "@followemail", follow_email.ToString() }, { "@reminderemail", remainder_email.ToString() } }, CommandType.StoredProcedure);

        if (dst != null)
        {
            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    lblToAddress.Text = samrow["AEMAIL"].ToString().Trim();
                    lblCCAddress.Text = "cwgms@charlesworth-group.com";
                    lblBCCAddress.Text = "sgmpublishing-production@microbiologysociety.org";

                    string strDIONo = Convert.ToString(samrow["DOINO"]);
                    string jourcode = Convert.ToString(samrow["JOURCODE"]);

                    if (strDIONo.IndexOf("/") != -1)
                    {
                        strDIONo = strDIONo.Substring(strDIONo.IndexOf("/") + 1).TrimEnd();
                        //strDIONo = strDIONo.Replace(".0.", "");
                        strDIONo = strDIONo.Substring(strDIONo.Length - 6);
                    }

                    lblSubject.Text = "" + jourcode.TrimEnd() + " Author Proof " + strDIONo;



                    string strArticle_Tittle = Convert.ToString(samrow["AMNsTITLE"]);


                    if (samrow["job_stage_name"].ToString().Trim() == "S100")
                    {
                        Session["srcPath"] = "";
                    }

                    string strLog = string.Empty;

                    strLog = "<font face='Calibri';font-size:11pt;'> Dear Author";
                    strLog += "<br/><br/>";
                    strLog += "Re <b>" + strArticle_Tittle + "</b>";
                    strLog += "<br/><br/>";
                    strLog += " We recently sent you the PDF proof for the manuscript above and a reminder about this; according to our records, we have not yet received any proof corrections from you.  ";
                    strLog += "<br/><br/>";
                    strLog += "Please return any required corrections and answers to editorial queries (if present) to the Production Team at Charlesworth Group by email (cwgms@charlesworth-group.com) urgently. If we do not hear from you within the next 24 hours, we may not be able to include your corrections in your paper, and it will be published in its current form. Note that the address to use is different to the address that this mail has been sent from. Please copy and paste this email address into the ‘to’ field of your reply rather than replying to this message.";
                    strLog += "<br/><br/>";
                    strLog += "The proofs for your paper are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html.";
                    strLog += "<br/><br/>";
                    strLog += "Please read the PDF (you can print it if you wish). To mark your corrections, you can either choose to send your corrections in an email, detailing clearly each correction with a page number, or you can add your comments to the PDF by following the instructions below. Please return your comments by email to cwgms@charlesworth-group.com. Simple changes not requiring a copy of the proof can be sent by email. Keep a copy of your corrections for your own records.";
                    strLog += "<br/><br/>";
                    strLog += "Only typographical and absolutely essential factual corrections will be accepted at this stage. Stylistic changes, particularly if these relate to large portions of text will not be made. You may be charged for correction of your non-typographical errors.";
                    strLog += "<br/><br/>";
                    strLog += "This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.";
                    strLog += "<br/><br/>";
                    strLog += "Return your corrected proof to:";
                    strLog += "<br/><br/>";
                    strLog += "email: cwgms@charlesworth-group.com";
                    strLog += "<br/><br/>";

                    strLog += "<font color='Blue'> Using Adobe Mark-up Tools</font>";

                    strLog += "<br/><br/>";
                    strLog += "To mark up corrections using Adobe Reader (it can be downloaded for free if you don't have it), you just need to make sure the correct toolbars on the PDF are activated. ";
                    strLog += "<br/><br/>";
                    strLog += "Please mark up the text using the tools in the 'Comment and mark ups' toolbar. This includes tools such as a sticky note, text edits tools, arrows and boxes. To make this appear, click on the 'Comment' drag down bar at the top of the PDF, then select 'Show Comment & Markup Toolbar'. ";
                    strLog += "<br/><br/>";
                    //  strLog+="<img src=\"images/MGEN.png\"></img>";
                    strLog += "<img src=cid:companylogo>";
                    strLog += "<br/><br/>";
                    strLog += "If you can't get the mark-up tools to work, there's possibility that the version of the document that we sent you has mistakenly not been activated to permit editing. In that case please get in touch immediately and we'll send you over a replacement file, with editing enabled.";
                    strLog += "<br/><br/>";
                    strLog += "Please note that in order to make sure the editors and typesetters understand your corrections, all mark ups should be very clear, and all instructions unambiguous. In order to assure this, we ask you to mark up corrections in ways listed below.";
                    strLog += "<br/><br/>";
                    strLog += "<font color='Blue'>Specific Text Changes</font>";
                    strLog += "<br/><br/>";
                    strLog += "For specific changes to the text, i.e. deletions, insertions and replacements, please use the 'Text edits' tools as much as possible (you can select these from the Comments & Mark-up toolbar). These are much more accurate than sticky notes. ";
                    strLog += "<br/><br/>";
                    strLog += "To <b>insert text,</b> , you need to click at the exact place you want the text to be inserted, then start typing. The text will then appear in a window so you can see what you're typing.";
                    strLog += "<br/><br/>";

                    strLog += "To <b>replace text,</b> highlight the text to be replaced, then start typing (again, the text will appear in a window as you type). When you insert or amend a character the marking on the page will be a very small blue or red marking.";
                    strLog += "<br/><br/>";
                    strLog += "To <b>format the text</b> you've inserted or replaced (i.e. change it to bold or italics, or back to Roman) you should type the additional/replacement text as described above so that it appears in a window, then highlight the text and right click with the mouse. You can then select 'text styles' and select bold, italic etc.";
                    strLog += "<br/><br/>";
                    strLog += "To <b>delete text</b>, highlight the text for deletion and press delete. You don't need to worry about the typesetters missing these marks, as they have a way of generating a list of all the comments added to the document to ensure they perform a thorough check and pick up all the corrections (rather than scanning the document for where changes have been added).";
                    strLog += "<br/><br/>";
                    strLog += "Please make sure that all of your mark ups are given as instructions for the typesetter; don't write your corrections in a way that the editor has to read them all and change them for the typesetter. If you do have any queries/issues you'd like the editor to look at, please either put these in a sticky note and begin with 'NOTE TO EDITOR', or list your queries in an email, giving the exact paragraph and page number your query relates to.";
                    strLog += "<br/><br/></font>";
                    //strLog += "Best wishes, <br/>";
                    //strLog += "Soundar<br/> </font>";



                    string mailbody = "<html><body><code  style='font-family:Calibri;font-size:11pt;'>" + strLog.ToString() + "</code></body></html>";

                    lblBody.Text = strLog.ToString();



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
    protected void Template_4()
    {
        StringBuilder MyStringBuilder = new StringBuilder();

        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        AlternateView View;
        LinkedResource resource;
        dst = dsql.ExcProcedure("spGet_SGM_Email_Content", new string[,] { { "@jobid", job_id.ToString() }, { "@jobtypeid", job_type_id.ToString() }, { "@followemail", follow_email.ToString() }, { "@reminderemail", remainder_email.ToString() } }, CommandType.StoredProcedure);

        if (dst != null)
        {
            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    lblToAddress.Text = samrow["AEMAIL"].ToString().Trim();
                    lblCCAddress.Text = "cwgms@charlesworth-group.com";
                    lblBCCAddress.Text = "sgmpublishing-production@microbiologysociety.org";

                    string strDIONo = Convert.ToString(samrow["DOINO"]);
                    string jourcode = Convert.ToString(samrow["JOURCODE"]);

                    if (strDIONo.IndexOf("/") != -1)
                    {
                        strDIONo = strDIONo.Substring(strDIONo.IndexOf("/") + 1).TrimEnd();
                        //strDIONo = strDIONo.Replace(".0.", "");
                        strDIONo = strDIONo.Substring(strDIONo.Length - 6);
                    }

                    lblSubject.Text = "" + jourcode.TrimEnd() + " Author Proof " + strDIONo;



                    string strArticle_Tittle = Convert.ToString(samrow["AMNsTITLE"]);


                    if (samrow["job_stage_name"].ToString().Trim() == "S100")
                    {
                        Session["srcPath"] = "";
                    }

                    string strLog = string.Empty;

                    strLog = "<font face='Calibri';font-size:11pt;'> Dear Author";
                    strLog += "<br/><br/>";
                    strLog += "Re <b>" + strArticle_Tittle + "</b>";
                    strLog += "<br/><br/>";
                    strLog += " We recently sent you the PDF proof for the manuscript above and two reminders about this; according to our records, we have not yet received any proof corrections from you. ";
                    strLog += "<br/><br/>";
                    strLog += "Please return any required corrections and answers to editorial queries (if present) to the Production Team at Charlesworth Group by email (cwgms@charlesworth-group.com) urgently. Please respond urgently otherwise your paper will be published as it stands. Note that the address to use is different to the address that this mail has been sent from. Please copy and paste this email address into the ‘to’ field of your reply rather than replying to this message.";
                    strLog += "<br/><br/>";
                    strLog += "The proofs for your paper are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html.";
                    strLog += "<br/><br/>";
                    strLog += "Please read the PDF (you can print it if you wish). To mark your corrections, you can either choose to send your corrections in an email, detailing clearly each correction with a page number, or you can add your comments to the PDF by following the instructions below. Please return your comments by email to cwgms@charlesworth-group.com. Simple changes not requiring a copy of the proof can be sent by email. Keep a copy of your corrections for your own records.";
                    strLog += "<br/><br/>";
                    strLog += "Only typographical and absolutely essential factual corrections will be accepted at this stage. Stylistic changes, particularly if these relate to large portions of text will not be made. You may be charged for correction of your non-typographical errors.";
                    strLog += "<br/><br/>";
                    strLog += "This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.";
                    strLog += "<br/><br/>";
                    strLog += "Return your corrected proof to:";
                    strLog += "<br/><br/>";
                    strLog += "email: cwgms@charlesworth-group.com";
                    strLog += "<br/><br/>";

                    strLog += "<font color='Blue'> Using Adobe Mark-up Tools</font>";

                    strLog += "<br/><br/>";
                    strLog += "To mark up corrections using Adobe Reader (it can be downloaded for free if you don't have it), you just need to make sure the correct toolbars on the PDF are activated.";
                    strLog += "<br/><br/>";
                    strLog += "Please mark up the text using the tools in the 'Comment and mark ups' toolbar. This includes tools such as a sticky note, text edits tools, arrows and boxes. To make this appear, click on the 'Comment' drag down bar at the top of the PDF, then select 'Show Comment & Markup Toolbar'. ";
                    strLog += "<br/><br/>";
                    //  strLog+="<img src=\"images/MGEN.png\"></img>";
                    strLog += "<img src=cid:companylogo>";
                    strLog += "<br/><br/>";
                    strLog += "If you can't get the mark-up tools to work, there's possibility that the version of the document that we sent you has mistakenly not been activated to permit editing. In that case please get in touch immediately and we'll send you over a replacement file, with editing enabled.";
                    strLog += "<br/><br/>";
                    strLog += "Please note that in order to make sure the editors and typesetters understand your corrections, all mark ups should be very clear, and all instructions unambiguous. In order to assure this, we ask you to mark up corrections in ways listed below.";
                    strLog += "<br/><br/>";
                    strLog += "<font color='Blue'>Specific Text Changes</font>";
                    strLog += "<br/><br/>";
                    strLog += "For specific changes to the text, i.e. deletions, insertions and replacements, please use the 'Text edits' tools as much as possible (you can select these from the Comments & Mark-up toolbar). These are much more accurate than sticky notes. ";
                    strLog += "<br/><br/>";
                    strLog += "To <b>insert text,</b> , you need to click at the exact place you want the text to be inserted, then start typing. The text will then appear in a window so you can see what you're typing.";
                    strLog += "<br/><br/>";

                    strLog += "To <b>replace text,</b> highlight the text to be replaced, then start typing (again, the text will appear in a window as you type). When you insert or amend a character the marking on the page will be a very small blue or red marking.";
                    strLog += "<br/><br/>";
                    strLog += "To <b>format the text ,</b> you've inserted or replaced (i.e. change it to bold or italics, or back to Roman) you should type the additional/replacement text as described above so that it appears in a window, then highlight the text and right click with the mouse. You can then select 'text styles' and select bold, italic etc.";
                    strLog += "<br/><br/>";

                    strLog += "To <b>delete text</b>, highlight the text for deletion and press delete. You don't need to worry about the typesetters missing these marks, as they have a way of generating a list of all the comments added to the document to ensure they perform a thorough check and pick up all the corrections (rather than scanning the document for where changes have been added)";
                    strLog += "<br/><br/>";
                    strLog += "Please make sure that all of your mark ups are given as instructions for the typesetter; don't write your corrections in a way that the editor has to read them all and change them for the typesetter. If you do have any queries/issues you'd like the editor to look at, please either put these in a sticky note and begin with 'NOTE TO EDITOR', or list your queries in an email, giving the exact paragraph and page number your query relates to.";
                    strLog += "<br/><br/></font>";
                    //strLog += "Best wishes, <br/>";
                    //strLog += "Soundar<br/> </font>";



                    string mailbody = "<html><body><code  style='font-family:Calibri;font-size:11pt;'>" + strLog.ToString() + "</code></body></html>";

                    lblBody.Text = strLog.ToString();



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
        dst = dsql.ExcProcedure("spGet_SGM_Email_Content", new string[,] { { "@jobid", job_id.ToString() }, { "@jobtypeid", job_type_id.ToString() }, { "@followemail", follow_email.ToString() }, { "@reminderemail", remainder_email.ToString() } }, CommandType.StoredProcedure);
        

        if (dst != null)
        {

            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    lblToAddress.Text = samrow["AEMAIL"].ToString().Trim();
                    lblCCAddress.Text = "cwgbookin@datapage.org";
                   // lblBCCAddress.Text = "sgmpublishing-production@microbiologysociety.org;";

                    string strDIONo = Convert.ToString(samrow["DOINO"]);

                    if (strDIONo.IndexOf("/") != -1)
                    {
                        strDIONo = strDIONo.Substring(strDIONo.IndexOf("/") + 1);
                        strDIONo = strDIONo.Replace(".0.", "");
                    }
                    if (samrow["job_stage_name"].ToString().Trim() == "S100")
                    {
                        Session["srcPath"] = strFileName_path + "\\" + "SGM_eProofs\\Marking up corrections on PDFs.pdf";
                        ////string strFileName = "";
                        ////strFileName = Path.GetFileName(File_upload.PostedFile.FileName);
                        ////if (System.IO.File.Exists(strFileName_path + "\\" + strFileName) == true)
                        ////    System.IO.File.Delete(strFileName_path + "\\" + strFileName);

                        ////FileInfo fsource_att = new FileInfo(strFileName_path + "\\" + strFileName);
                        ////File_upload.PostedFile.SaveAs(strFileName_path + "\\" + strFileName);
                        ////System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(strFileName_path + "\\" + strFileName);
                        // mmsg.Attachments.Add(attach);


                         
                        //////object filename = @"C:\Users\dp0928\Desktop\SGM eProofing templates\IJSEM eproof_without images.doc";

                        //////Microsoft.Office.Interop.Word.ApplicationClass AC = new Microsoft.Office.Interop.Word.ApplicationClass();
                        //////Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
                        //////object readOnly = false;
                        //////object isVisible = true;
                        //////object missing = System.Reflection.Missing.Value;
                        ////////Reating the word document and adding it to textbox
                        //////try
                        //////{
                        //////    doc = AC.Documents.Open(ref filename, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible);
                        //////    lblBody.Text = doc.Content.Text;
                        //////    AC.Documents.Close();
                        //////}
                        //////catch (Exception ex)
                        //////{
                        //////    //Error Handling
                        //////}




                    }

                    StringBuilder MyStringBuilder = new StringBuilder();
                    MyStringBuilder.Append("Dear Author");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Re " + strDIONo);
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("The proofs for your paper for JMM Case Reports are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html.");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Please return any required corrections and answers to editorial queries within 2 days of receipt of this message to sgmprod@charlesworth-group.com.");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Please mark your corrections and add comments to the PDF by following the instructions below. If you are unable to make the corrections directly on the PDF, then please list corrections and answers to editorial queries clearly in the body of your email. ");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Only typographical and absolutely essential factual corrections will be accepted at this stage. Stylistic changes that are not in line with Microbiology Society house style will not be made, and neither will changes relating to large portions of text. You may be charged for correction of your non-typographical errors. This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.");
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
                    //MyStringBuilder.Append("Best wishes, <br/>");
                    //MyStringBuilder.Append("Soundar<br/>");
                    

                    lblBody.Text = MyStringBuilder.ToString();
 

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
           
            if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddresses;
    }

    protected void FollowUpMailSent_JMMCRwithoutImage()
    {
        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        dst = dsql.ExcProcedure("spGet_SGM_Email_Content", new string[,] { { "@jobid", job_id.ToString() }, { "@jobtypeid", job_type_id.ToString() }, { "@followemail", follow_email.ToString() }, { "@reminderemail", remainder_email.ToString() } }, CommandType.StoredProcedure);

        if (dst != null)
        {
            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    lblToAddress.Text = samrow["AEMAIL"].ToString().Trim();
                    lblCCAddress.Text = "cwgbookin@datapage.org";
                    // lblBCCAddress.Text = "sgmpublishing-production@microbiologysociety.org;";

                    string strDIONo = Convert.ToString(samrow["DOINO"]);

                    if (strDIONo.IndexOf("/") != -1)
                    {
                        strDIONo = strDIONo.Substring(strDIONo.IndexOf("/") + 1);
                        strDIONo = strDIONo.Replace(".0.", "");
                    }
                    if (samrow["job_stage_name"].ToString().Trim() == "S100")
                    {
                        Session["srcPath"] = strFileName_path + "\\" + "SGM_eProofs\\Marking up corrections on PDFs.pdf";
                    }

                    StringBuilder MyStringBuilder = new StringBuilder();
                    MyStringBuilder.Append("<font face='Calibri';font-size:11pt;'> Dear Author");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Re " + strDIONo);
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("The proofs for your paper for JMM Case Reports are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html.");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Please return any required corrections and answers to editorial queries within 2 days of receipt of this message to cwgms@charlesworth-group.com.");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Please mark your corrections and add comments to the PDF by following the instructions attached. If you are unable to make the corrections directly on the PDF, then please list corrections and answers to editorial queries clearly in the body of your email.");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Only typographical and absolutely essential factual corrections will be accepted at this stage. Stylistic changes that are not in line with Microbiology Society house style will not be made, and neither will changes relating to large portions of text. You may be charged for correction of your non-typographical errors. This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("When your paper is ready for publication, you will receive an email with details of how to access the final PDF of your manuscript and how to order reprints using the SGM Reprint Service.");
                    MyStringBuilder.Append("<br/><br/>");
                    
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("<br/><br/></font>");
                    //MyStringBuilder.Append("Best wishes, <br/>");
                    //MyStringBuilder.Append("Soundar<br/> </font>");
                  

                    lblBody.Text = MyStringBuilder.ToString();


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

    protected void FollowUpMailSent_JMMCRwithImage()
    {
        StringBuilder MyStringBuilder = new StringBuilder();
        
        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        AlternateView View ;
        LinkedResource resource;
        dst = dsql.ExcProcedure("spGet_SGM_Email_Content", new string[,] { { "@jobid", job_id.ToString() }, { "@jobtypeid", job_type_id.ToString() }, { "@followemail", follow_email.ToString() }, { "@reminderemail", remainder_email.ToString() } }, CommandType.StoredProcedure);

        if (dst != null)
        {
            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    lblToAddress.Text = samrow["AEMAIL"].ToString().Trim();
                    lblCCAddress.Text = "cwgms@charlesworth-group.com";
                    lblBCCAddress.Text = "sgmpublishing-production@microbiologysociety.org";

                    string strDIONo = Convert.ToString(samrow["DOINO"]);
                    string jourcode = Convert.ToString(samrow["JOURCODE"]);

                    if (strDIONo.IndexOf("/") != -1)
                    {
                        strDIONo = strDIONo.Substring(strDIONo.IndexOf("/") + 1).TrimEnd();
                        //strDIONo = strDIONo.Replace(".0.", "");
                        strDIONo = strDIONo.Substring(strDIONo.Length - 6);
                    }

                    lblSubject.Text = "" + jourcode.TrimEnd() + " Author Proof " + strDIONo;



                    string strArticle_Tittle = Convert.ToString(samrow["AMNsTITLE"]);

                    if (samrow["job_stage_name"].ToString().Trim() == "S100")
                    {
                        Session["srcPath"] = "";// strFileName_path + "\\" + "SGM_eProofs\\Marking up corrections on PDFs.pdf";
                    }
                    else
                    {
                        Session["srcPath"] = "";
                    }
                    
                  
                    string strLog = string.Empty;

                        strLog = "<font face='Calibri';font-size:11pt;'> Dear Author";
                        strLog += "<br/><br/>";
                        strLog += "Re <b>" + strArticle_Tittle + "</b>";
                        strLog+="<br/><br/>";
                        strLog += " The proofs for your paper for JMM Case Reports are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html. ";
                        strLog+="<br/><br/>";
                        strLog += "Please return any required corrections and answers to editorial queries within 2 days of receipt of this message to sgmprod@charlesworth-group.com.";
                        strLog+="<br/><br/>";
                        strLog += "Please mark your corrections and add comments to the PDF by following the instructions below. If you are unable to make the corrections directly on the PDF, then please list corrections and answers to editorial queries clearly in the body of your email. ";
                        strLog+="<br/><br/>";
                        strLog += "Only typographical and absolutely essential factual corrections will be accepted at this stage. Stylistic changes that are not in line with Microbiology Society house style will not be made, and neither will changes relating to large portions of text. You may be charged for correction of your non-typographical errors. This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.";
                        strLog+="<br/><br/>";
                                               
                        strLog += "<font color='Blue'> Using Adobe Mark-up Tools</font>";
       
                        strLog+="<br/><br/>";
                        strLog += "To mark up corrections using Adobe Reader (it can be downloaded for free if you don't have it), you just need to make sure the correct toolbars on the PDF are activated.  ";
                        strLog+="<br/><br/>";
                        strLog += "Please mark up the text using the tools in the 'Comment and mark ups' toolbar. This includes tools such as a sticky note, text edits tools, arrows and boxes. To make this appear, click on 'Comments' at the top of the PDF, then select 'Show Comment & Markup Toolbar'.  ";
                        strLog+="<br/><br/>";
                        //  strLog+="<img src=\"images/MGEN.png\"></img>";
                        strLog+="<img src=cid:companylogo>";
                        strLog+="<br/><br/>";
                        strLog += "If you can't get the mark-up tools to work, there's possibility that the version of the document that we sent you has mistakenly not been activated to permit editing. In that case please get in touch immediately and we'll send you over a replacement file, with editing enabled.";
                        strLog+="<br/><br/>";
                        strLog += "Please note that in order to make sure the copy-editors and typesetters understand your corrections, all corrections should be very clear and all instructions unambiguous. In order to assure this, we ask you to mark up corrections using the methods listed below.";
                        strLog+="<br/><br/>";
                        strLog += "<font color='Blue'>Specific Text Changes</font>";
                        strLog+="<br/><br/>";
                        strLog += "For specific changes to the text, i.e. deletions, insertions and replacements, please use the 'Text edits' tools as much as possible (you can select these from the Comments & Mark-up toolbar). These are much more accurate than sticky notes. ";
                        strLog+="<br/><br/>";
                        strLog += "To <b>insert text,</b> you need to click at the exact place you want the text to be inserted, then start typing. The text will then appear in a window so you can see what you're typing.";
                        strLog+="<br/><br/>";

                        strLog += "To <b>replace text,</b> highlight the text to be replaced, then start typing (again, the text will appear in a window as you type). When you insert or amend a character the mark on the page will be a very small blue or red marking.";
                        strLog+="<br/><br/>";
                        strLog+="To <b>add a note,</b> highlight the text in question and right-click on your mouse. In the drop-down box select ''Add sticky note'' and type your note in the box which appears.";
                        strLog+="<br/><br/>";
                        strLog += "To <b>format the text</b> you've inserted or replaced (i.e. change it to bold or italics, or back to Roman) you should highlight the text and add a sticky note as above, specifying whether it should be changed to bold, italic etc.";
                        strLog+="<br/><br/>";
                        strLog += "To <b>delete text</b>, highlight the text for deletion and press delete. ";
                        strLog+="<br/><br/>";
                        strLog += "Please make sure that all of your mark ups are given as instructions for the typesetter. If you do have any queries/issues you'd like the editorial team to look at, please put these in a sticky note and begin with 'NOTE TO EDITOR'.";
                        strLog+="<br/><br/>";
                        //strLog+="Sincerely, <br/>";
                        //strLog+="SGM Proofing<br/>";
                        //strLog+="Charlesworth India<br/>";
                        //strLog+="email: project.manager@charlesworth-group.in<br/>";
                        //strLog+="Fax: 0044 1484 536032<br/>";
                        //strLog += "Phone: 044 42267215<br/> </font>";
                        strLog += "<br/><br/></font>";
                        //strLog += "Best wishes, <br/>";
                        //strLog += "Soundar<br/> </font>";



                        string mailbody = "<html><body><code  style='font-family:Calibri;font-size:11pt;'>" + strLog.ToString() + "</code></body></html>";

                        lblBody.Text = strLog.ToString();
                     


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

    protected void FollowUpMailSent_MGENwithoutImage()
    {
        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        dst = dsql.ExcProcedure("spGet_SGM_Email_Content", new string[,] { { "@jobid", job_id.ToString() }, { "@jobtypeid", job_type_id.ToString() }, { "@followemail", follow_email.ToString() }, { "@reminderemail", remainder_email.ToString() } }, CommandType.StoredProcedure);

        if (dst != null)
        {
            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    lblToAddress.Text = samrow["AEMAIL"].ToString().Trim();
                    lblCCAddress.Text = "cwgms@charlesworth-group.com";
                    lblBCCAddress.Text = "sgmpublishing-production@microbiologysociety.org";

                    string strDIONo = Convert.ToString(samrow["DOINO"]);
                    string jourcode = Convert.ToString(samrow["JOURCODE"]);

                    if (strDIONo.IndexOf("/") != -1)
                    {
                        strDIONo = strDIONo.Substring(strDIONo.IndexOf("/") + 1).TrimEnd();
                        //strDIONo = strDIONo.Replace(".0.", "");
                        strDIONo = strDIONo.Substring(strDIONo.Length - 6);
                    }

                    lblSubject.Text = "" + jourcode.TrimEnd() + " Author Proof " + strDIONo;



                    string strArticle_Tittle = Convert.ToString(samrow["AMNsTITLE"]);
                    if (samrow["job_stage_name"].ToString().Trim() == "S100")
                    {
                        Session["srcPath"] = "";// strFileName_path + "\\" + "SGM_eProofs\\Marking up corrections on PDFs.pdf";
                    }
                    else
                    {
                        Session["srcPath"] = "";
                    }

                    StringBuilder MyStringBuilder = new StringBuilder();
                    MyStringBuilder.Append("<font face='Calibri';font-size:11pt;'> Dear Author");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Re <font color='Red'>" + strArticle_Tittle + "</font>");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("The proofs for your paper for Microbial Genomics are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html.");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Please return any required corrections and answers to editorial queries within 2 days of receipt of this message to cwgms@charlesworth-group.com.");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Please mark your corrections and add comments to the PDF by following the instructions attached. If you are unable to make the corrections directly on the PDF, then please list corrections and answers to editorial queries clearly in the body of your email. ");
                    MyStringBuilder.Append("<br/><br/>");
                    MyStringBuilder.Append("Only typographical and absolutely essential factual corrections will be accepted at this stage. Stylistic changes that are not in line with Microbiology Society house style will not be made, and neither will changes relating to large portions of text. You may be charged for correction of your non-typographical errors. This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.");
                    MyStringBuilder.Append("<br/><br/>");
                    

                    
                    MyStringBuilder.Append("<br/><br/></font>");
                    //MyStringBuilder.Append("Best wishes,<br/>");
                    //MyStringBuilder.Append("Soundar<br/> </font>");
                  

                    lblBody.Text = MyStringBuilder.ToString();


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

    protected void FollowUpMailSent_MGENwithImage()
    {
        StringBuilder MyStringBuilder = new StringBuilder();

        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        AlternateView View;
        LinkedResource resource;
        dst = dsql.ExcProcedure("spGet_SGM_Email_Content", new string[,] { { "@jobid", job_id.ToString() }, { "@jobtypeid", job_type_id.ToString() }, { "@followemail", follow_email.ToString() }, { "@reminderemail", remainder_email.ToString() } }, CommandType.StoredProcedure);

        if (dst != null)
        {
            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    lblToAddress.Text = samrow["AEMAIL"].ToString().Trim();
                    lblCCAddress.Text = "cwgms@charlesworth-group.com";
                    lblBCCAddress.Text = "sgmpublishing-production@microbiologysociety.org";

                    string strDIONo = Convert.ToString(samrow["DOINO"]);
                    string jourcode = Convert.ToString(samrow["JOURCODE"]);

                    if (strDIONo.IndexOf("/") != -1)
                    {
                        strDIONo = strDIONo.Substring(strDIONo.IndexOf("/") + 1).TrimEnd();
                        //strDIONo = strDIONo.Replace(".0.", "");
                        strDIONo = strDIONo.Substring(strDIONo.Length - 6);
                    }

                    lblSubject.Text = "" + jourcode.TrimEnd() + " Author Proof " + strDIONo;



                    string strArticle_Tittle = Convert.ToString(samrow["AMNsTITLE"]);
                   
                    if (samrow["job_stage_name"].ToString().Trim() == "S100")
                    {
                        Session["srcPath"] = "";//; strFileName_path + "\\" + "SGM_eProofs\\Marking up corrections on PDFs.pdf";
                    }
                    else
                    {
                        Session["srcPath"] = "";
                    }
                    
                    string strLog = string.Empty;

                    strLog = "<font face='Calibri';font-size:11pt;'> Dear Author";
                    strLog += "<br/><br/>";
                    strLog += "Re <b>" + strArticle_Tittle + "</b>";
                    strLog += "<br/><br/>";
                    strLog += " The proofs for your paper for Microbial Genomics are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from http://www.adobe.com/products/acrobat/readstep.html. ";
                    strLog += "<br/><br/>";
                    strLog += "Please return any required corrections and answers to editorial queries within 2 days of receipt of this message to sgmprod@charlesworth-group.com.";
                    strLog += "<br/><br/>";
                    strLog += "Please mark your corrections and add comments to the PDF by following the instructions below. If you are unable to make the corrections directly on the PDF, then please list corrections and answers to editorial queries clearly in the body of your email.  ";
                    strLog += "<br/><br/>";
                    strLog += "Only typographical and absolutely essential factual corrections will be accepted at this stage. Stylistic changes that are not in line with Microbiology Society house style will not be made, and neither will changes relating to large portions of text. You may be charged for correction of your non-typographical errors. This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.";
                    strLog += "<br/><br/>";
                                       
                    strLog += "<font color='Blue'> Using Adobe Mark-up Tools</font>";

                    strLog += "<br/><br/>";
                    strLog += "To mark up corrections using Adobe Reader (it can be downloaded for free if you don't have it), you just need to make sure the correct toolbars on the PDF are activated.  ";
                    strLog += "<br/><br/>";
                    strLog += "Please mark up the text using the tools in the 'Comment and mark ups' toolbar. This includes tools such as a sticky note, text edits tools, arrows and boxes. To make this appear, click on 'Comments' at the top of the PDF, then select 'Show Comment & Markup Toolbar'.  ";
                    strLog += "<br/><br/>";
                    //  strLog+="<img src=\"images/MGEN.png\"></img>";
                    strLog += "<img src=cid:companylogo>";
                    strLog += "<br/><br/>";
                    strLog += "If you can't get the mark-up tools to work, there's possibility that the version of the document that we sent you has mistakenly not been activated to permit editing. In that case please get in touch immediately and we'll send you over a replacement file, with editing enabled.";
                    strLog += "<br/><br/>";
                    strLog += "Please note that in order to make sure the copy-editors and typesetters understand your corrections, all corrections should be very clear and all instructions unambiguous. In order to assure this, we ask you to mark up corrections using the methods listed below.";
                    strLog += "<br/><br/>";
                    strLog += "<font color='Blue'>Specific Text Changes</font>";
                    strLog += "<br/><br/>";
                    strLog += "For specific changes to the text, i.e. deletions, insertions and replacements, please use the 'Text edits' tools as much as possible (you can select these from the Comments & Mark-up toolbar). These are much more accurate than sticky notes. ";
                    strLog += "<br/><br/>";
                    strLog += "To <b>insert text,</b> you need to click at the exact place you want the text to be inserted, then start typing. The text will then appear in a window so you can see what you're typing.";
                    strLog += "<br/><br/>";

                    strLog += "To <b>replace text,</b> highlight the text to be replaced, then start typing (again, the text will appear in a window as you type). When you insert or amend a character the mark on the page will be a very small blue or red marking.";
                    strLog += "<br/><br/>";
                    strLog += "To <b>add a note,</b> highlight the text in question and right-click on your mouse. In the drop-down box select ''Add sticky note'' and type your note in the box which appears.";
                    strLog += "<br/><br/>";
                    strLog += "To <b>format the text</b> you've inserted or replaced (i.e. change it to bold or italics, or back to Roman) you should highlight the text and add a sticky note as above, specifying whether it should be changed to bold, italic etc.";
                    strLog += "<br/><br/>";
                    strLog += "To <b>delete text</b>, highlight the text for deletion and press delete. ";
                    strLog += "<br/><br/>";
                    strLog += "Please make sure that all of your mark ups are given as instructions for the typesetter. If you do have any queries/issues you'd like the editorial team to look at, please put these in a sticky note and begin with 'NOTE TO EDITOR'.";
                    strLog += "<br/><br/></font>";
                    //strLog += "Best wishes, <br/>";
                    //strLog += "Soundar<br/> </font>";



                    string mailbody = "<html><body><code  style='font-family:Calibri;font-size:11pt;'>" + strLog.ToString() + "</code></body></html>";

                    lblBody.Text = strLog.ToString();



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


    protected void FollowUpMailSent_IJSEMwithImage()
    {
        StringBuilder MyStringBuilder = new StringBuilder();

        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        AlternateView View;
        LinkedResource resource;
        dst = dsql.ExcProcedure("spGet_SGM_Email_Content", new string[,] { { "@jobid", job_id.ToString() }, { "@jobtypeid", job_type_id.ToString() }, { "@followemail", follow_email.ToString() }, { "@reminderemail", remainder_email.ToString() } }, CommandType.StoredProcedure);

        if (dst != null)
        {
            try
            {
                foreach (DataRow samrow in dst.Tables[0].Rows)
                {
                    lblToAddress.Text = samrow["AEMAIL"].ToString().Trim();
                    lblCCAddress.Text = "cwgms@charlesworth-group.com";
                    lblBCCAddress.Text = "sgmpublishing-production@microbiologysociety.org";

                    string strDIONo = Convert.ToString(samrow["DOINO"]);
                    string jourcode = Convert.ToString(samrow["JOURCODE"]);

                    if (strDIONo.IndexOf("/") != -1)
                    {
                        strDIONo = strDIONo.Substring(strDIONo.IndexOf("/") + 1).TrimEnd();
                        //strDIONo = strDIONo.Replace(".0.", "");
                        strDIONo = strDIONo.Substring(strDIONo.Length - 6);
                    }

                    lblSubject.Text = "" + jourcode.TrimEnd() + " Author Proof " + strDIONo;



                    string strArticle_Tittle = Convert.ToString(samrow["AMNsTITLE"]);

                    if (samrow["job_stage_name"].ToString().Trim() == "S100")
                    {
                        Session["srcPath"] = "";//; strFileName_path + "\\" + "SGM_eProofs\\Marking up corrections on PDFs.pdf";
                    }
                    else
                    {
                        Session["srcPath"] = "";
                    }

                    string strLog = string.Empty;

                    strLog = "<font face='Calibri';font-size:11pt;'> Dear Author";
                    strLog += "<br/><br/>";
                    strLog += "Re <b>" + strArticle_Tittle + "</b>";
                    strLog += "<br/><br/>";
                    strLog += " The proofs for your paper for the International Journal of Systematic and Evolutionary Microbiology are attached as a single PDF file. You will need Acrobat Reader to open the file. If you do not already have this software installed you can download it free from  ";
                    strLog += "<br/><br/>";
                    strLog += " http://www.adobe.com/products/acrobat/readstep.html.  ";
                    strLog += "<br/><br/>";
                    strLog += " Please return any required corrections and answers to editorial queries within 2 days of receipt of this message to cwgms@charlesworth-group.com.";
                    strLog += "<br/><br/>";
                    strLog += " Please mark your corrections and add comments to the PDF by following the instructions below. If you are unable to make the corrections directly on the PDF, then please list corrections and answers to editorial queries clearly in the body of your email.  ";
                    strLog += "<br/><br/>";
                    strLog += " Please check through the proof of your paper carefully as it will not be processed by an external proofreader.";
                    strLog += "<br/><br/>";
                    strLog += " Only typographical and absolutely essential factual corrections will be accepted at this stage. Stylistic changes that are not in line with Microbiology Society house style will not be made, and neither will changes relating to large portions of text. You may be charged for correction of your non-typographical errors. This PDF file must not be offered for commercial sale or used for systematic external distribution by a third party.";
                    strLog += "<br/><br/>";
                   
                    strLog += "<font color='Blue'> Using Adobe Mark-up Tools</font>";

                    strLog += "<br/><br/>";
                    strLog += " To mark up corrections using Adobe Reader (it can be downloaded for free if you don't have it), you just need to make sure the correct toolbars on the PDF are activated.   ";
                    strLog += "<br/><br/>";
                    strLog += " Please mark up the text using the tools in the 'Comment and mark ups' toolbar. This includes tools such as a sticky note, text edits tools, arrows and boxes. To make this appear, click on 'Comments' at the top of the PDF, then select 'Show Comment & Markup Toolbar'.  ";
                    strLog += "<br/><br/>";
                    //  strLog+="<img src=\"images/MGEN.png\"></img>";
                    strLog += "<img src=cid:companylogo>";
                    strLog += "<br/><br/>";
                    strLog += " If you can't get the mark-up tools to work, there's possibility that the version of the document that we sent you has mistakenly not been activated to permit editing. In that case please get in touch immediately and we'll send you over a replacement file, with editing enabled.";
                    strLog += "<br/><br/>";
                    strLog += " Please note that in order to make sure the copy-editors and typesetters understand your corrections, all corrections should be very clear and all instructions unambiguous. In order to assure this, we ask you to mark up corrections using the methods listed below.";
                    strLog += "<br/><br/>";
                    strLog += "<font color='Blue'>Specific Text Changes</font>";
                    strLog += "<br/><br/>";
                    strLog += " For specific changes to the text, i.e. deletions, insertions and replacements, please use the 'Text edits' tools as much as possible (you can select these from the Comments & Mark-up toolbar). These are much more accurate than sticky notes.  ";
                    strLog += "<br/><br/>";
                    strLog += "To <b>insert text,</b> you need to click at the exact place you want the text to be inserted, then start typing. The text will then appear in a window so you can see what you're typing.";
                    strLog += "<br/><br/>";

                    strLog += "To <b>replace text,</b> highlight the text to be replaced, then start typing (again, the text will appear in a window as you type). When you insert or amend a character the mark on the page will be a very small blue or red marking.";
                    strLog += "<br/><br/>";
                    strLog += "To <b>add a note,</b> highlight the text in question and right-click on your mouse. In the drop-down box select 'Add sticky note' and type your note in the box which appears.";
                    strLog += "<br/><br/>";
                    strLog += "To <b>format the text</b> you've inserted or replaced (i.e. change it to bold or italics, or back to Roman) you should highlight the text and add a sticky note as above, specifying whether it should be changed to bold, italic etc.";
                    strLog += "<br/><br/>";
                    strLog += "To <b>delete text</b>, highlight the text for deletion and press delete. ";
                    strLog += "<br/><br/>";
                    strLog += " Please make sure that all of your mark ups are given as instructions for the typesetter. If you do have any queries/issues you'd like the editorial team to look at, please put these in a sticky note and begin with 'NOTE TO EDITOR'.";
                    strLog += "<br/><br/></font>";
                    //strLog += "Best wishes, <br/>";
                    //strLog += "Soundar<br/> </font>";



                    string mailbody = "<html><body><code  style='font-family:Calibri;font-size:11pt;'>" + strLog.ToString() + "</code></body></html>";

                    lblBody.Text = strLog.ToString();



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
        System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
        SmtpClient smt = new SmtpClient();
        
        string mac_id = GetMacAddress();
        string Mac_address = ConfigurationManager.AppSettings["Mac_address"].ToString();
        try
        {
            smt.Host = "mail.nimbox.co.uk"; //ConfigurationManager.AppSettings["Invoicemail_host"].ToString();
            //smt.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Invoicemail_port"].ToString());
            mmsg = new System.Net.Mail.MailMessage();
            if (!addmailcollection(lblToAddress.Text.Trim().ToString(), mmsg.To))
            { alert("Your To Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblCCAddress.Text.Trim().ToString(), mmsg.CC))
            { alert("Your CC Address is not Valid, Please check"); return; }
            if (!addmailcollection(lblBCCAddress.Text.Trim().ToString(), mmsg.Bcc))
            { alert("Your BCC Address is not Valid, Please check"); return; }
            mmsg.From = new MailAddress(lblFromAddress.Text.ToString());
           
            mmsg.Subject = lblSubject.Text.Trim().ToString();


         

            mmsg.Body = lblBody.Text.ToString();
            mmsg.IsBodyHtml = true;

            string strFileName = "";
            //if (Session["srcPath"].ToString() != "")
            //{
            //    spath = Session["srcPath"].ToString();
            //}
            //else
            //{
            //    spath = "";
            //}
            if (spath != "")
            {
                //System.Net.Mail.Attachment attachNew = new System.Net.Mail.Attachment(spath);
                //mmsg.Attachments.Add(attachNew);
            }
            else
            {
                AlternateView av = AlternateView.CreateAlternateViewFromString(lblBody.Text.ToString(), null, MediaTypeNames.Text.Html);
                LinkedResource lr = new LinkedResource(Server.MapPath("images/MGEN.png"), "image/png");//MediaTypeNames.Image.Tiff);
                lr.ContentId = "companylogo";
                av.LinkedResources.Add(lr);
                mmsg.AlternateViews.Add(av);


                //System.Net.Mail.AlternateView plainTextView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(lblBody.Text.Trim(), null, "text/plain");
                //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(lblBody.Text.Trim() + "<image src=images/MGEN.jpg>", null, "text/html");
                ////Add image to HTML version
                //System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath("images/MGEN.jpg"), "image/jpg");
                //imageResource.ContentId = "HDIImage";
                //htmlView.LinkedResources.Add(imageResource);
                ////Add two views to message.
                //mmsg.AlternateViews.Add(plainTextView);
                //mmsg.AlternateViews.Add(htmlView);
            }
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
            //smt.Credentials = new NetworkCredential("software@datapage.org", "Reset*123");
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            smt.Send(mmsg);

            datasourceSQL dsql = new datasourceSQL();
            datasourceIBSQL dobj = new datasourceIBSQL();

            if (Request.QueryString["mailtype"].ToString().TrimEnd() == "1")
            {
                dsql.ExcSProcedure("sp_UpdateSamFollowMailSent", new string[,] { { "@author_samfollow_sent_by", emp_id }, { "@job_id", job_id.ToString() } }, CommandType.StoredProcedure);
                dsql.ExcSProcedure("sp_UpdateSamFollowMailSent_1", new string[,] { { "@job_id", job_id.ToString() } }, CommandType.StoredProcedure);
            }
            else if (Request.QueryString["mailtype"].ToString().TrimEnd() == "2")
            {
                dsql.ExcSProcedure("sp_UpdateSamFollowMailSent_2", new string[,] { { "@job_id", job_id.ToString() } }, CommandType.StoredProcedure);
            }
            else if (Request.QueryString["mailtype"].ToString().TrimEnd() == "3")
            {
                dsql.ExcSProcedure("sp_UpdateSamFollowMailSent_3", new string[,] { { "@job_id", job_id.ToString() } }, CommandType.StoredProcedure);
            }
            else if (Request.QueryString["mailtype"].ToString().TrimEnd() == "4")
            {
               
                dsql.ExcSProcedure("sp_UpdateSamFollowMailSent_4", new string[,] { { "@job_id", job_id.ToString() } }, CommandType.StoredProcedure);

            }
            
            alert("Mail Sent Successfully");
            
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
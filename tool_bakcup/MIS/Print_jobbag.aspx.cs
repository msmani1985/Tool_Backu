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
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

public partial class Print_jobbag : System.Web.UI.Page
{
    static string cust_id = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            jobdetaildiv.Visible = false;
            if (Request.QueryString["jobid"] != null && Request.QueryString["jobtypeid"] != null)
            {
                jobdetaildiv.Visible = true;
                DDLjobtype.SelectedValue = Request.QueryString["jobtypeid"];
                Txtjobno.Text = Request.QueryString["jobid"];
                Btnlognow_Click(sender, e);
                jobdetaildiv.Visible = true;
                if (Request.QueryString["print"] != null && Request.QueryString["print"].ToString() == "1")//For Print
                { divheader.Visible = false; getjobnodiv.Visible = false; }
                else
                { divheader.Visible = true; getjobnodiv.Visible = true; }
            }
        }

    }
    protected void ondatabound(object send, GridViewRowEventArgs e)
    {
        HtmlAnchor ha = (HtmlAnchor)(e.Row.Cells[0].FindControl("a_jobname"));
        if (ha != null) ha.HRef = "Print_jobbag.aspx?jobid=" + DataBinder.Eval(e.Row.DataItem, "job_id") + "&jobtypeid=" + DataBinder.Eval(e.Row.DataItem, "job_type_id") + "&print=" + Request.QueryString["print"];
    }
    protected void Btnlognow_Click(object sender, EventArgs e)
    {
        if (Txtjobno.Text == "")
        {
            detaildiv.Visible = false;
            errMessage.Visible = true;
            errMessage.InnerHtml = "Job Number is Empty";
        }
        else
        {

            jobdetaildiv.Visible = true;//main div
            errMessage.Visible = false;
            detaildiv.Visible = true;
            datasourceSQL jsql = new datasourceSQL();
            DataSet jds = new DataSet();
            try
            {
                jds = jsql.ExcProcedure("spGet_JobBag_Book", new string[,] { { "@bno", Txtjobno.Text }}, CommandType.StoredProcedure);
                if (jds != null)
                {
                    //For Mail send

                    //cust_id = jds.Tables[0].Rows[0]["customer_id"].ToString();
                    //LoadEditorManager(peditor1, peditor2, jds);
                    LoadDiv(jds);

                    //Eventdetailsgv.DataSource = jds.Tables[2];
                    //Eventdetailsgv.DataBind();
                    //displaydiv(divEventDetails, jds.Tables[2]);

                    jobhistroygv.DataSource = jds.Tables[0];
                    jobhistroygv.DataBind();
                    //displaydiv(jobhistorydiv, jds.Tables[3]);

                    ////childjobgv.DataSource = jds.Tables[5];
                    ////childjobgv.DataBind();
                    //displaydiv(ChildJobs, jds.Tables[5]);

                    ////graphsgv.DataSource = jds.Tables[6];
                    ////graphsgv.DataBind();
                    //displaydiv(GraphsDetailsdiv, jds.Tables[6]);

                    ////commentgv.DataSource = jds.Tables[7];
                    ////commentgv.DataBind();
                    //displaydiv(commentsdiv, jds.Tables[7]);

                    ////onholdgv.DataSource = jds.Tables[8];
                    ////onholdgv.DataBind();
                    //displaydiv(onholddiv, jds.Tables[8]);

                }
                else
                {
                    jobdetaildiv.Visible = false;
                    errMessage.Visible = true;
                    errMessage.InnerHtml = "Job Number Does not Exist";

                }

            }
            catch (Exception ex)
            { throw ex; }
            finally
            { jsql = null; jds = null; }
        }

    }
    private void LoadEditorManager(Label LblEditor, Label LblManager, DataSet cnds)
    {
        ////Tables[2] for CONTACT Details
        if (cnds.Tables[4].Rows.Count > 0)
        {
            if (cnds.Tables[4].Rows.Count > 1)
            {
                LblEditor.Text = cnds.Tables[4].Rows[0]["display_name"].ToString();
                LblManager.Text = cnds.Tables[4].Rows[1]["display_name"].ToString();
                contact1.Text = cnds.Tables[4].Rows[0]["contact_type_name"].ToString();
                contact2.Text = cnds.Tables[4].Rows[1]["contact_type_name"].ToString();

            }
            else
            {
                LblEditor.Text = cnds.Tables[4].Rows[0]["display_name"].ToString();
                contact1.Text = cnds.Tables[4].Rows[0]["contact_type_name"].ToString();
            }
            /*else if (cnds.Tables[2].Rows[0]["contact_type_name"].ToString().ToUpper() == "PRODUCTION EDITOR")
                LblEditor.Text = (cnds.Tables[2].Rows[0]["display_name"].ToString() == "" || cnds.Tables[2].Rows[0]["display_name"] == null) ? "&nbsp;" : cnds.Tables[2].Rows[0]["display_name"].ToString();
            else if (cnds.Tables[2].Rows[0]["contact_type_name"].ToString().ToUpper() == "PRODUCTION MANAGER")
                LblManager.Text = (cnds.Tables[2].Rows[0]["display_name"].ToString() == "" || cnds.Tables[2].Rows[0]["display_name"] == null) ? "&nbsp;" : cnds.Tables[2].Rows[0]["display_name"].ToString();
             * */
        }
        else
        { LblEditor.Text = "&nbsp;"; LblManager.Text = "&nbsp;"; }


    }
    private string createcolumn(DataColumn dc, DataRow dr)
    {
        if (dc.ColumnName.ToString().ToUpper() == ("NAME") || dc.ColumnName.ToString().ToUpper() == ("NAME1"))
            return "<td><font style='font-size:16pt;font-weight:bold'>" + dr[dc].ToString() + "</font></td>";
        else if (dc.ColumnName.ToString().ToUpper().IndexOf("EMPTY") >= 0)
            return "<td class='captiontext'>&nbsp;</td>";
        else if (dc.ColumnName.ToString().ToUpper() == "PARENT_JOB_NAME")
            return "<td class='captiontext'>" + dc.ColumnName.ToString().Replace("_", " ").Trim() + " : <a href='jobbag.aspx?jobid=" + dr["parent_job_id"].ToString() + "&jobtypeid=" + dr["parent_job_type_id"].ToString() + "&print=" + Request.QueryString["print"] + "' id='linkparentjob' runat='server'>" + dr[dc].ToString() + "</a></td>";

        else
            return "<td class='captiontext'>" + dc.ColumnName.ToString().Replace("_", " ").Trim() + " : <font class='labeltxt'>" + dr[dc].ToString() + "</font></td>";


    }
    private void LoadDiv(DataSet jobds)
    {
        string divcnt = "<table width='600px' cellpadding='2'  cellspacing='2'>";
        divcnt += "<tr><td class='captiontext'>CAT ID # : <font class='labeltxt'>" + jobds.Tables[0].Rows[0]["Bnumber"] + "</font></td><td class='captiontext'>Job Type : <font class='labeltxt'>" + "Book" + "</font></td>";
        divcnt += "</tr>";
        divcnt += "<tr><td class='captiontext'>Job Title : <font class='labeltxt' size='15'><b>" + jobds.Tables[0].Rows[0]["BTitle"] + "</b></font></td><td class='captiontext'>Stage : <font class='labeltxt'>" + jobds.Tables[0].Rows[0]["STYPENAME"] + "</font></td>";
        divcnt += "</tr>";
        divcnt += "<tr><td class='captiontext'>Customer Name : <font class='labeltxt'>" + jobds.Tables[0].Rows[0]["Custname"] + "</font></td><td class='captiontext'>Format : <font class='labeltxt'>" + jobds.Tables[0].Rows[0]["BSTYLE"] + "</font></td>";
        divcnt += "</tr>";
        //for (int c = 0; c < jobds.Tables[0].Columns.Count; c += 2)
        //{
        //    divcnt += "<tr>";
        //    if ((jobds.Tables[0].Columns[c].ColumnName.ToString().IndexOf("id") == -1) && (jobds.Tables[0].Columns[c].ColumnName.ToString().ToUpper().IndexOf("IS_") == -1) && (c < jobds.Tables[0].Columns.Count))
        //        divcnt += createcolumn(jobds.Tables[0].Columns[c], jobds.Tables[0].Rows[0]);
        //    if ((c + 1 < jobds.Tables[0].Columns.Count) && (jobds.Tables[0].Columns[c + 1].ColumnName.ToString().IndexOf("id") == -1) && (jobds.Tables[0].Columns[c + 1].ColumnName.ToString().ToUpper().IndexOf("IS_") == -1))//For second td
        //        divcnt += createcolumn(jobds.Tables[0].Columns[c + 1], jobds.Tables[0].Rows[0]);
        //    divcnt += "</tr>";

        //}

        //For Contact Details
        //if (jobds != null && jobds.Tables[1].Rows.Count > 0)
        //{
        //    divcnt += "<tr style='padding-top:50px;margin-top:20px;'><td colspan='2' class='HeaderText'>Contact Details</td></tr>";
        //    for (int cc = 0; cc < jobds.Tables[1].Columns.Count; cc++)
        //    {

        //        if (jobds.Tables[1].Columns[cc].ColumnName.ToString().ToUpper() == "CUSTOMER_NAME")
        //            divcnt += "<tr><td class='captiontext'>" + jobds.Tables[1].Columns[cc].ColumnName.ToString() + " : " +
        //                    "<a href=\"customerinformation.aspx?custid=" + jobds.Tables[0].Rows[0]["customer_id"].ToString() + "\"target='_blank' onclick=\"window.open(this.href,'CustomerInformation','width=450,height=600,scrollbars=yes,resizable=0,top=20,left=400');return false;\" runat='server' id='customerlink'>" +
        //                    "<font class='labeltxt'>" + jobds.Tables[1].Rows[0][cc].ToString() + "</font></a></td>";
        //        else if (jobds.Tables[1].Columns[cc].ColumnName.ToString().ToUpper() == "JOURNAL_TITLE" || jobds.Tables[1].Columns[cc].ColumnName.ToString().ToUpper() == "JOURNAL_NAME")
        //            divcnt += "<tr><td class='captiontext'>" + jobds.Tables[1].Columns[cc].ColumnName.ToString().Replace("_", " ").Trim() + " : <a id='journallink2' href=\"journalinformation.aspx?journal_id=" + jobds.Tables[0].Rows[0]["journal_id"].ToString() + "\"target='_blank' onclick=\"window.open(this.href,'JournalInformation','width=400,height=500,scrollbars=yes,resizable=0,top=20,left=400');return false;\" runat='server'>" +
        //                    "  <font class='labeltxt'>" + jobds.Tables[1].Rows[0][cc].ToString() + "</font></a></td>";
        //        Label cnt = (Label)FindControl("contact" + (cc + 1));
        //        Label cntval = (Label)FindControl("peditor" + (cc + 1));
        //        if ((cnt != null) && (cntval != null) && cnt.Text != "" && cntval.Text != "")
        //            divcnt += "<td class='captiontext'>" + cnt.Text + " : <font class='labeltxt'>" + cntval.Text.ToString() + "</font></td>";
        //        divcnt += "</tr>";

        //    }
        //}
        divcnt += "</table>";
        detaildiv.InnerHtml = divcnt;


    }
    private void displaydiv(HtmlGenericControl divid, DataTable dt)
    {
        if (dt.Rows.Count == 0)
            divid.Style.Add(HtmlTextWriterStyle.Display, "none");
        //divid.Visible = false;
        else
            divid.Style.Add(HtmlTextWriterStyle.Display, "''");
        //divid.Visible = true;

    }
    private string displayimg(DataSet imgds)
    {
        string imgcnt = string.Empty;
        //For copyEdit,SAM and OnHold Parent Job
        imgcnt = "<table>";
        if (imgds.Tables[0].Columns.Contains("is_sam"))
            if (imgds.Tables[0].Rows[0]["Is_Sam"].ToString() == "True")
                imgcnt += @"<tr><td valign='top'><img src='images\jobbag\img_SAM.bmp' /></td></tr>";
        if (imgds.Tables[0].Columns.Contains("is_copyedit"))
            if (imgds.Tables[0].Rows[0]["Is_CopyEdit"].ToString() == "True")
                imgcnt += @"<tr><td valign='top'><img src='images\jobbag\img_COPYEDIT.bmp' /></td></tr>";
        if (imgds.Tables[0].Columns.Contains("is_sensitive"))
            if (imgds.Tables[0].Rows[0]["Is_Sensitive"].ToString() == "True")
                imgcnt += @"<tr><td valign='top'><img src='images\jobbag\img_sensitive.bmp' /></td></tr>";
        if (imgds.Tables[0].Rows[0]["onhold_history_id"].ToString() != "" && imgds.Tables[0].Rows[0]["onhold_history_id"] != null)
            imgcnt += @"<tr><td><img src='images\jobbag\img_HOLD.bmp'/></td></tr>";
        //For stages
        if (imgds.Tables[0].Rows[0]["job_stage_id"].ToString() != "" && imgds.Tables[0].Rows[0]["job_stage_id"] != null)
            imgcnt += @"<tr><td><img src='images\jobbag\img" + imgds.Tables[0].Rows[0]["job_stage_id"].ToString() + ".bmp'/></td></tr>";
        //For Early XML PDF request
        if (imgds.Tables[0].Columns.Contains("is_epdf"))
            if (imgds.Tables[0].Rows[0]["Is_epdf"].ToString() == "True")
                imgcnt += @"<tr><td valign='top'><img src='images\jobbag\img_epdf.bmp' /></td></tr>";
        //For Extra CopyEdit
        if (imgds.Tables[0].Columns.Contains("Is_ExtraCEdit"))
            if (imgds.Tables[0].Rows[0]["Is_ExtraCEdit"].ToString() == "True")
                imgcnt += @"<tr><td valign='top'><img src='images\jobbag\img_ExtraCEdit.bmp' /></td></tr>";
        imgcnt += "</table>";
        return imgcnt;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void ibtn_email_click(object sender, EventArgs e)
    {
        XmlDocument xdoc = new XmlDocument();
        XmlNode xnode = null;
        string toaddress = string.Empty;
        string ccaddress = string.Empty;
        try
        {
            xdoc.Load(MapPath("") + "\\jobbagemail_config.xml");
            xnode = xdoc.SelectSingleNode("jobbag/emailgroup1").SelectSingleNode("customer[@custid='" + cust_id.ToString() + "']");
            if (xnode != null)
            {
                toaddress = xdoc.SelectSingleNode("jobbag/emailgroup1").SelectSingleNode("to").InnerText.Trim().ToString();
                ccaddress = (xdoc.SelectSingleNode("jobbag/emailgroup1").SelectSingleNode("cc") != null) ? xdoc.SelectSingleNode("jobbag/emailgroup1").SelectSingleNode("cc").InnerText.Trim().ToString() : "";
            }
            else
            {
                xnode = xdoc.SelectSingleNode("jobbag/emailgroup2").SelectSingleNode("customer[@custid='" + cust_id.ToString() + "']");
                if (xnode != null)
                {
                    toaddress = xdoc.SelectSingleNode("jobbag/emailgroup2").SelectSingleNode("to").InnerText.Trim().ToString();
                    ccaddress = (xdoc.SelectSingleNode("jobbag/emailgroup2").SelectSingleNode("cc") != null) ? xdoc.SelectSingleNode("jobbag/emailgroup2").SelectSingleNode("cc").InnerText.Trim().ToString() : "";
                }
            }
            MailMessage msg = new MailMessage();
            SmtpClient smt = new SmtpClient();
            //smt.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Invoicemail_port"]);
            //smt.Host = ConfigurationManager.AppSettings["Invoicemail_host"].ToString();
            smt.Port = Convert.ToInt32(xdoc.SelectSingleNode("jobbag").SelectSingleNode("mail_port").InnerText.Trim().ToString());
            smt.Host = xdoc.SelectSingleNode("jobbag").SelectSingleNode("mail_host").InnerText.Trim().ToString();
            msg.From = new MailAddress(xdoc.SelectSingleNode("jobbag").SelectSingleNode("from").InnerText.Trim().ToString());
            //msg.To.Add("subbulakshmi@datapage.org");
            if (!addmailaddress(msg.To, toaddress))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Your To address is not valid, Please check');</script>");
                return;
            }
            if (!addmailaddress(msg.CC, ccaddress))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Your To address is not valid, Please check');</script>");
                return;
            }
            msg.Subject = "Job Bag";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            jobdetaildiv.RenderControl(hw);
            //msg.Body = jobdetaildiv.InnerHtml.ToString();
            msg.Body = sw.GetStringBuilder().ToString();
            msg.IsBodyHtml = true;
            smt.Send(msg);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Mail Sent Successfully');</script>");
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { xdoc = null; xnode = null; }
    }
    protected Boolean addmailaddress(MailAddressCollection mc, string mail_address)
    {
        string[] mad = null;
        if (!string.IsNullOrEmpty(mail_address))
        {
            if (mail_address.IndexOf(";") > 0)
            {
                mad = mail_address.Split(';');
                for (int mcnt = 0; mad.Length > mcnt; mcnt++)
                {
                    if (mad[mcnt].ToString() != "")
                    {
                        if (!Regex.IsMatch(mad[mcnt].ToString(), @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                            return false;
                        mc.Add(mad[mcnt].ToString().Trim());
                    }
                }
            }
            else
            {
                if (!Regex.IsMatch(mail_address.ToString(), @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                    return false;
                mc.Add(mail_address.ToString().Trim());

            }
            return true;
        }
        return true;
    }
    protected void ibtn_email_click(object sender, ImageClickEventArgs e)
    {

    }
}

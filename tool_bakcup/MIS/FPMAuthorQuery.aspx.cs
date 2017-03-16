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

public partial class FPMAuthorQuery : System.Web.UI.Page
{
    static DataSet samfollowupds = null;
    string sSortExp = "";
    string sSortDir = "desc";
    static string jobid = "";
    string emp_id;
    string emp_team_id;
    bool isTeamLead;
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        else
        {
            emp_team_id = Session["employeeteamid"].ToString();
            emp_id = Session["employeeid"].ToString();
            isTeamLead = Convert.ToBoolean(Session["timesheet"]);



            if (!Page.IsPostBack)
            {
                emp_id = Session["employeeid"].ToString();
                ViewState["sortOrder"] = "desc";
                sSortExp = "";
                sSortDir = "desc";
                LoadGrid();
            }
        }
    }
    private void LoadGrid()
    {
        datasourceSQL fobj = new datasourceSQL();
        DataSet fds = new DataSet();
        try
        {
            fds = fobj.ExcProcedure("spGet_FPM_Author_Query", null, CommandType.StoredProcedure);

            div_samdetails.Visible = true;
            div_error.Visible = false;
            gv_samfollowup.DataSource = fds;
            gv_samfollowup.DataBind();
            Session["RptDs"] = fds;
            samfollowupds = fds.Copy();

        }
        catch (Exception ex)
        { }
        finally
        { fds = null; fobj = null; }

    }
   
    protected void gridview_rowdatabound(object sender, GridViewRowEventArgs e)
    {
        int follow_days = 2;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DateTime date = Convert.ToDateTime(e.Row.Cells[5].Text.Trim());
            if (e.Row.Cells[6].Text.ToString() != "&nbsp;" && e.Row.Cells[6].Text.Trim() != "")
            {
                DateTime odt = (Convert.ToDateTime(e.Row.Cells[6].Text.Trim()));
                e.Row.Cells[6].Text = GetNextBussinessDate(odt, 3).ToShortDateString();
            }
        }
    

        //    DateTime oStart = Convert.ToDateTime(e.Row.Cells[4].Text);
        //    DateTime oStart1 = Convert.ToDateTime(e.Row.Cells[4].Text.ToString());
        //   if (oStart.DayOfWeek == DayOfWeek.Saturday || oStart.DayOfWeek == DayOfWeek.Sunday)
        //    //if (e.Row.Cells[4].Text.ToString() == DayOfWeek.Saturday)
        //    {

        //        oStart1 = Convert.ToDateTime(e.Row.Cells[4].Text.ToString()).AddDays(2);

        //    }

        //}


        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //if (GetNextBussinessDate(Convert.ToDateTime(e.Row.Cells[3].Text.ToString()), Convert.ToInt16(e.Row.Cells[5].Text.ToString())).Date <= DateTime.Now.Date)
        //{
        //e.Row.Visible = true;
        //e.Row.Cells[5].Text = GetNextBussinessDate(Convert.ToDateTime(e.Row.Cells[4].Text.ToString()), Convert.ToInt16(follow_days)).ToShortDateString();

        // e.Row.Cells[5].Text = GetNextBussinessDate(Convert.ToDateTime(e.Row.Cells[4].Text.ToString(), Convert.ToInt16(follow_days).ToString())).ToShortDateString();
        // }
        //else
        //    e.Row.Visible = false;
        // }
        //if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells[6].Text.ToString() != "")
        //    if (Convert.ToDateTime(e.Row.Cells[2].Text.ToString()).AddDays(10) <= DateTime.Now && e.Row.Cells[7].Text == "&nbsp;")
        //    { e.Row.BackColor = System.Drawing.Color.Pink; }



        //if (e.Row.RowType == DataControlRowType.DataRow)
        //    if (DataBinder.Eval(e.Row.DataItem, "STYPENAME").ToString().Trim().ToLower() != "proof")
        //        if (e.Row.Cells[12].Text.ToString() == "&nbsp;")
        //        {
        //            e.Row.BackColor = System.Drawing.Color.Pink;
        //        }

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.Cells[13].Text.ToString() != "&nbsp;" && e.Row.Cells[11].Text.ToString() != "")
        //    {
        //        DateTime odt = (Convert.ToDateTime(e.Row.Cells[11].Text.Trim()));
        //        e.Row.Cells[13].Text = GetNextBussinessDate(odt, 3).ToShortDateString();
        //        if (Convert.ToDateTime(e.Row.Cells[11].Text.ToString()) >= DateTime.Now)
        //        {
        //            e.Row.Cells[14].Text = "";
        //        }
        //    }
            //    //  e.Row.Cells[13].Text = e.Row.Cells[7].Text;
            //}

            //if (e.Row.Cells[13].Text != "&nbsp;")
            //{
            //    DropDownList peddl = (DropDownList)e.Row.Cells[14].FindControl("dd_pemailsent");
            //    if (peddl != null)
            //    {
            //        peddl.SelectedValue = "Yes";
            //        peddl.Enabled = false;
            //    }
            //}

       // }
    }
    private DateTime GetNextBussinessDate(DateTime oStart, int iCounter)
    {

        while (oStart.DayOfWeek == DayOfWeek.Saturday || oStart.DayOfWeek == DayOfWeek.Sunday)
            oStart = oStart.AddDays(1);
        for (int i = 1; i <= iCounter; i++)
        {
            oStart = oStart.AddDays(1);
            while (oStart.DayOfWeek == DayOfWeek.Saturday || oStart.DayOfWeek == DayOfWeek.Sunday)
                oStart = oStart.AddDays(1);
        }
        return oStart;
    }

    protected void gridview_rowcommand(object sender, GridViewCommandEventArgs e)
    {
        //GridViewRow row = (GridViewRow)((Control)((GridViewCommandEventArgs)e).CommandSource).Parent.Parent;
        DataView samdv = new DataView();
        samdv = samfollowupds.Tables[0].DefaultView;
        samdv.RowFilter = "JOB_ID=" + e.CommandArgument.ToString();
        datasourceIBSQL dobj = new datasourceIBSQL();
        try
        {
            if (e.CommandName == "AuthorMail")
            {
                Createmail(samdv.ToTable().Rows[0], "SAM Follow up Mail");
                if (!dobj.updateSAMAuthormailsent("update article_dp set authormailsent='" + DateTime.Now.ToShortDateString() + "',authormailsentby=" + Session["employeenumber"] + " where ano in(" + e.CommandArgument.ToString() + ")", CommandType.Text))
                    alert("Updation Failed");
            }
            else if (e.CommandName == "ReminderMail")
            {
                Createmail(samdv.ToTable().Rows[0], "SAM Follow up Reminder Mail");
                if (!dobj.updateSAMAuthormailsent("update article_dp set author_reminder_mailsent='" + DateTime.Now.ToShortDateString() + "' where ano in(" + e.CommandArgument.ToString() + ")", CommandType.Text))
                    alert("Updation Failed");
            }
            else if (e.CommandName == "Remove")
            {
                if (!dobj.updateSAMAuthormailsent("update article_dp set removeon_from_samfollowup='" + DateTime.Now.ToShortDateString() + "', removedby=" + Session["employeenumber"] + " where ano in(" + e.CommandArgument.ToString() + ")", CommandType.Text))
                    alert("Remove failed");
            }



            LoadGrid();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { dobj = null; }
    }

    private void Createmail(DataRow samrow, string mailsubject)
    {
        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        MailMessage mmsg = new MailMessage();
        SmtpClient smt = new SmtpClient();
        string toaddress = string.Empty;
        string fromaddress = ConfigurationManager.AppSettings["SAMFollowupmail_fromaddress"].ToString();
        string ccaddress = string.Empty;
        string bccaddress = string.Empty;
        string mbody = string.Empty; string msubject = string.Empty;
        try
        {
            xd.Load(Server.MapPath("").ToString() + @"\SAM_AuthorQuery_Emailconfig.xml");
            smt.Host = ConfigurationManager.AppSettings["Invoicemail_host"].ToString();
            smt.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Invoicemail_port"].ToString());
            xn = xd.DocumentElement.SelectSingleNode("//followup").SelectSingleNode("customer[@custno='" + samrow["CUSTNO"].ToString().Trim() + "']");
            if (xn != null)
            {
                childnode = xn.SelectSingleNode("to");
                toaddress = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                childnode = xn.SelectSingleNode("cc");
                ccaddress = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                childnode = xn.SelectSingleNode("bcc");
                bccaddress = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                childnode = xn.SelectSingleNode("body");
                mbody = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                mbody = mbody.Replace("[AUTHORNAME]", samrow["AFIRSTAUTHOR"].ToString().Trim());
                mbody = mbody.Replace("[AUTHOREMAIL]", samrow["AEMAIL"].ToString().Trim());
                mbody = mbody.Replace("[AUTHORTITLE]", samrow["AMNSTITLE"].ToString().Trim());
                mbody = mbody.Replace("[PENAME]", samrow["INVDISPLAYNAME"].ToString().Trim());
                mbody = mbody.Replace("[PEEMAIL]", samrow["INVCONEMAIL"].ToString().Trim());
                mbody = mbody.Replace("[AUTHORCORRECTON]", samrow["AUTHORCORRECTION"].ToString().Trim());
                mbody = mbody.Replace("[EDITORCORRECTION]", samrow["EDITORCORRECTION"].ToString().Trim());
                mbody = mbody.Replace("[ARTICLEID]", samrow["AMANUSCRIPTID"].ToString().Trim());
                mbody = mbody.Replace("[JOURNALTITLE]", samrow["JOURNAL_TITLE"].ToString().Trim());
                msubject = mailsubject + " - " + samrow["ARTICLECODE"].ToString().Trim();
                mmsg = new MailMessage();
                if (!addmailcollection(toaddress, mmsg.To))
                { alert("Your To Address is not Valid, Please check"); return; }
                if (!addmailcollection(ccaddress, mmsg.CC))
                { alert("Your CC Address is not Valid, Please check"); return; }
                mmsg.From = new MailAddress(fromaddress, ConfigurationManager.AppSettings["SAMFollowupmail_aliasname"].ToString());
                mmsg.Subject = msubject;
                mmsg.Body = mbody;
                smt.Send(mmsg);

                alert("Mailsent Successfully");

            }
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { xd = null; childnode = null; xn = null; mmsg = null; smt = null; }

    }
    private void alert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
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
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment;filename=SAMFollowupReport.xls");
            this.EnableViewState = false;
            System.IO.StringWriter strwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);

            HtmlForm htmlfrm = new HtmlForm();
            gv_samfollowup.Parent.Controls.Add(htmlfrm);
            //gv_samfollowup.Columns[12].Visible = false;
            //gv_samfollowup.Columns[2].Visible = false;
            //gv_samfollowup.Columns[7].Visible = false;
            //gv_samfollowup.Columns[16].Visible = false;
            htmlfrm.Attributes["runat"] = "server";
            htmlfrm.Controls.Add(gv_samfollowup);
            htmlfrm.RenderControl(txtwriter);
            Response.Write(strwriter);
            Response.End();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ibtn_save_Click(object sender, ImageClickEventArgs e)
    {
        string updateano = string.Empty;
        foreach (GridViewRow gvr in gv_samfollowup.Rows)
        {
            DropDownList ddlist = (DropDownList)(gvr.Cells[14].FindControl("dd_pemailsent"));
            if (ddlist != null && ddlist.Enabled && ddlist.SelectedValue.ToString() == "Yes")
                updateano += (updateano != "") ? "," + ((HiddenField)(gvr.Cells[14].FindControl("hf_ano"))).Value : ((HiddenField)(gvr.Cells[14].FindControl("hf_ano"))).Value;
        }
        if (updateano != "")
        {
            datasourceIB samobj = new datasourceIB();
            try
            {
                if (samobj.updatesampemailsent("update article_dp set PEMAILSENT ='" + DateTime.Now.ToShortDateString() + "' where ano in(" + updateano + ")", CommandType.Text))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Successfully updated');</script>");
                    LoadGrid();
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Updation failed');</script>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                samobj = null;
            }
        }
    }


    private string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
                ViewState["sortOrder"] = "asc";
            else
                ViewState["sortOrder"] = "desc";

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
    protected void GV_OnSortdata(object sender, GridViewSortEventArgs e)
    {
        sSortExp = e.SortExpression;
        sSortDir = sortOrder;
        ds = new DataSet();
        if (Session["RptDs"] != null)
            ds = (DataSet)Session["RptDs"];
        DataView dv = new DataView();
        dv = ds.Tables[0].DefaultView;
        dv.Sort = string.Format("{0} {1}", sSortExp, sSortDir);
        gv_samfollowup.DataSource = dv;
        gv_samfollowup.DataBind();
    }
}
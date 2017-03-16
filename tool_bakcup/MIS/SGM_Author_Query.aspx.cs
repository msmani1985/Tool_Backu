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

public partial class SGM_Author_Query : System.Web.UI.Page
{
    static DataSet fds;
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
            fds = fobj.ExcProcedure("spGet_SGM_Author_Query_New", null, CommandType.StoredProcedure);
            Session["dateVal"] = fds;
            div_sgmdetails.Visible = true;
            div_error.Visible = false;
            gv_sgmfollowup.DataSource = fds;
            gv_sgmfollowup.DataBind();
            Session["RptDs1"] = fds;

        }
        catch (Exception ex)
        { }
        finally
        { fds = null; fobj = null; }

    }

    protected void gridview_rowdatabound(object sender, GridViewRowEventArgs e)
    {
       
        System.Web.UI.HtmlControls.HtmlImage img1 = (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("Author_mail_sent1");
        System.Web.UI.HtmlControls.HtmlImage img2 = (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("Author_mail_sent2");
        System.Web.UI.HtmlControls.HtmlImage img3 = (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("Author_mail_sent3");
        System.Web.UI.HtmlControls.HtmlImage img4 = (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("Author_mail_sent4");

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                int index = e.Row.RowIndex;

                DataSet dst = new DataSet();
                dst = (DataSet)Session["dateVal"];
                if (dst != null)
                {
                    string day1 = Convert.ToString(dst.Tables[0].Rows[index]["day1"]);
                    string day2 = Convert.ToString(dst.Tables[0].Rows[index]["day2"]);
                    string day3 = Convert.ToString(dst.Tables[0].Rows[index]["day3"]);
                    string day4 = Convert.ToString(dst.Tables[0].Rows[index]["day4"]);

                    string sfollow_email_sent = Convert.ToString(dst.Tables[0].Rows[index]["follow_email_sent"]);
                    string sfollow_email_reminder_sent = Convert.ToString(dst.Tables[0].Rows[index]["follow_email_reminder_sent"]);
                    string sfollow_email_reminder_sent1 = Convert.ToString(dst.Tables[0].Rows[index]["follow_email_reminder_sent1"]);
                    string sfollow_email_reminder_sent2 = Convert.ToString(dst.Tables[0].Rows[index]["follow_email_reminder_sent2"]);



                    if (day1 != "")
                    {
                        DateTime dt1 = Convert.ToDateTime(day1);
                        if (sfollow_email_sent == "")
                        {
                            if (DateTime.Now > Convert.ToDateTime(day1))
                            {
                                img1.Visible = true;
                                img2.Visible = false;
                                img3.Visible = false;
                                img4.Visible = false;
                            }
                            else
                            {
                                img1.Visible = false;
                                img2.Visible = false;
                                img3.Visible = false;
                                img4.Visible = false;
                            }
                        }
                        else if (sfollow_email_sent != "" && sfollow_email_reminder_sent == "")
                        {
                            if ((DateTime.Now > Convert.ToDateTime(day2)))
                            {
                                img1.Visible = false;
                                img2.Visible = true;
                                img3.Visible = false;
                                img4.Visible = false;
                            }
                            else
                            {
                                img1.Visible = false;
                                img2.Visible = false;
                                img3.Visible = false;
                                img4.Visible = false;
                            }
                        }
                        else if (sfollow_email_sent != "" && sfollow_email_reminder_sent != "" && sfollow_email_reminder_sent1 == "")
                        {
                            if ((DateTime.Now > Convert.ToDateTime(day3)))
                            {
                                img1.Visible = false;
                                img2.Visible = false;
                                img3.Visible = true;
                                img4.Visible = false;
                            }
                            else
                            {
                                img1.Visible = false;
                                img2.Visible = false;
                                img3.Visible = false;
                                img4.Visible = false;
                            }
                        }
                        else if (sfollow_email_sent != "" && sfollow_email_reminder_sent != "" && sfollow_email_reminder_sent1 != "" && sfollow_email_reminder_sent2 == "")
                        {
                            if ((DateTime.Now > Convert.ToDateTime(day4)))
                            {
                                img1.Visible = false;
                                img2.Visible = false;
                                img3.Visible = false;
                                img4.Visible = true;
                            }
                            else
                            {
                                img1.Visible = false;
                                img2.Visible = false;
                                img3.Visible = false;
                                img4.Visible = false;
                            }
                        }
                        else
                        {
                            if (img1.Visible)
                            {
                                img2.Visible = false;
                                img3.Visible = false;
                                img4.Visible = false;
                            }
                            else if (img2.Visible)
                            {
                                img1.Visible = false;
                                img3.Visible = false;
                                img4.Visible = false;
                            }
                            else if (img3.Visible)
                            {
                                img1.Visible = false;
                                img2.Visible = false;
                                img4.Visible = false;
                            }
                            else if (img4.Visible)
                            {

                                img1.Visible = false;
                                img2.Visible = false;
                                img3.Visible = false;
                            }
                        }


                    }
                    else
                    {
                        img1.Visible = true;
                        img2.Visible = false;
                        img3.Visible = false;
                        img4.Visible = false;
                    }

                }
            }
        }
        catch
        {
            img1.Visible = false;
            img2.Visible = false;
            img3.Visible = false;
            img4.Visible = false;
        }
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
        fds = (DataSet)Session["RptDs1"];
        samdv = fds.Tables[0].DefaultView;
        samdv.RowFilter = "job_id =" + e.CommandArgument.ToString();
        datasourceSQL dobj = new datasourceSQL();
        try
        {
            if (e.CommandName == "Remove")
            {
                dobj.ExcSProcedure("update article_dp set author_samfollow_removed = '" + DateTime.Now.ToShortDateString() + "', author_samfollow_removed_by =" + Session["employeenumber"] + " where ano in  (" + e.CommandArgument.ToString() + ")", null, CommandType.Text);
                // if (!dobj.updateSAMAuthormailsent("update article_dp set removeon_from_samfollowup='" + DateTime.Now.ToShortDateString() + "', removedby=" + Session["employeenumber"] + " where ano in(" + e.CommandArgument.ToString() + ")", CommandType.Text))
                alert("Removed Successfully");
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
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.xls";
            //Response.AddHeader("content-disposition", "attachment;filename=SAMFollowupReport.xls");
            //this.EnableViewState = false;
            //System.IO.StringWriter strwriter = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
            //HtmlForm htmlfrm = new HtmlForm();
            //div_sgmdetails.Parent.Controls.Add(htmlfrm);
            //htmlfrm.Attributes["runat"] = "server";
            //htmlfrm.Controls.Add(gv_sgmfollowup);
            //htmlfrm.RenderControl(txtwriter);
            //Response.Write(strwriter);
            //Response.End();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "SGMAuthorQuery.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv_sgmfollowup.AllowPaging = false;
            
            //Change the Header Row back to white color
            gv_sgmfollowup.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            ////for (int i = 0; i < gv_sgmfollowup.HeaderRow.Cells.Count; i++)
            ////{
            ////    gv_sgmfollowup.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            ////}
            gv_sgmfollowup.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        catch (Exception ex)
        {

        }
    }
    protected void ibtn_save_Click(object sender, ImageClickEventArgs e)
    {
        string updateano = string.Empty;
        foreach (GridViewRow gvr in gv_sgmfollowup.Rows)
        {
            DropDownList ddlist = (DropDownList)(gvr.Cells[14].FindControl("dd_pemailsent"));
            if (ddlist != null && ddlist.Enabled && ddlist.SelectedValue.ToString() == "Yes")
                updateano += (updateano != "") ? "," + ((HiddenField)(gvr.Cells[14].FindControl("hf_ano"))).Value : ((HiddenField)(gvr.Cells[14].FindControl("hf_ano"))).Value;
        }
        if (updateano != "")
        {
            datasourceIBSQL samobj = new datasourceIBSQL();
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
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
        if (Session["RptDs1"] != null)
            ds = (DataSet)Session["RptDs1"];
        DataView dv = new DataView();
        dv = ds.Tables[0].DefaultView;
        dv.Sort = string.Format("{0} {1}", sSortExp, sSortDir);
        gv_sgmfollowup.DataSource = dv;
        gv_sgmfollowup.DataBind();
    }
}
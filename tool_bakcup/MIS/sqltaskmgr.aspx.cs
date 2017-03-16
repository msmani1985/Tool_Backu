using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Collections.Generic;   

public partial class sqltaskmgr : System.Web.UI.Page
{
    protected int id = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            divheader.InnerHtml = "Event Logger of " + Session["fullname"].ToString();
            LoadGrid();
        }

    }
    [WebMethod]
    public static List<string> Article(string ID, string JobTypeid)
    {
        List<string> empResult = new List<string>();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "spGetJob_search1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();
                cmd.Parameters.AddWithValue("@JobName", ID);
                cmd.Parameters.AddWithValue("@JobTypeID", JobTypeid);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    empResult.Add(dr["JobCode"].ToString());
                }
                con.Close();
                return empResult;
            }
        }
    }   
    private void LoadGrid()
    {
        datasourceSQL tsql = new datasourceSQL();   
        DataSet tds = new DataSet();
        DataSet comds = new DataSet();
        try
        {
            //Session["employeeid"] = null;
            if (Session["employeeid"] == null)
                throw new ArgumentException("Session Expired, Please relogin");

            string[,] TI_param={{"@employee_id",Session["employeeid"].ToString()},{"@dept_id",Session["departmentid"].ToString()}};
            tds = tsql.GetTaskItem(TI_param);
            ddlstyles.DataSource = tds;
            ddlstyles.DataBind();
            ddltask_type.DataSource = tds;
            ddltask_type.DataBind();
            tds = null;
            string[,] lparam ={ { "@employee_id", Session["employeeid"].ToString() } };
            tds = tsql.ExcProcedure("spGet_LoggedEvents_Journal", lparam, CommandType.StoredProcedure);
            agvLogEvents.DataSource = tds;
            agvLogEvents.DataBind();
            drpType.SelectedValue = "Common";
            txtJobNumber.Text = "";
            txtArtCode.Text = "";
        }
        catch (ArgumentException ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + ex.Message.ToString() + "');window.open('login.aspx','_top');</script>");

        }
        catch (Exception ex)
        { throw ex; }
        finally
        { tsql = null; tds = null; comds = null; }
    }
    protected void btnSubmit_click(object sender, EventArgs e)
    {
        //if (txtJobNumber.Text == "" && ddltask_type.Items[ddlstyles.SelectedIndex].Value.ToString() != "4" && ddltask_type.Items[ddlstyles.SelectedIndex].Value.ToString() != "5")// Equal to 4,5 is Time Sheet (break,absent,Meeting) so it may be no jobno
        //    errMessage.InnerHtml = "Job Number is empty";
        //else
        {
            datasourceSQL lsql = new datasourceSQL();
            DataSet lds = new DataSet();
            string[,] lparam;
            try
            {
                if (drpType.SelectedValue == "Common" || drpType.SelectedValue == "Input")
                {
                    lparam = new string[,] { { "@task_id", ddlstyles.SelectedValue.ToString() }, { "@job_id", txtJobNumber.Text }, { "@job_type_id",  ddljobtype.SelectedValue.ToString() },
                    {"@job_history_id","" },{"@emp_id",Session["employeeid"].ToString()},
                    {"@cpno",""},{"@type","1"},{"@logid",""} ,
                    {"@stime",""},{"@etime",""},{"@comments",""},{"@pages",""}};//logid,stime,etime etc for timesheet split option

                    if (lparam != null)
                    {
                        lsql.ExcSProcedure("spInsert_LoggedEvents", lparam, CommandType.StoredProcedure);
                        LoadGrid();
                    }
                }
                else
                {
                    lparam = new string[,] { { "@job_id", txtJobNumber.Text }, { "@job_type", ddljobtype.SelectedValue.ToString() } };
                    lds = lsql.ExcProcedure("spGet_JobNo_Details", lparam, CommandType.StoredProcedure);
                    errMessage.InnerHtml = "";
                    lparam = null;

                    //if (lds == null)
                    //{
                    //    errMessage.InnerHtml = "Job Number is not valid";
                    //    return;
                    //}

                    lparam = new string[,] { 
                                {"@task_id", ddlstyles.SelectedValue.ToString() }, {"@job_id", txtJobNumber.Text }, 
                                {"@job_type_id",  ddljobtype.SelectedValue.ToString() },{"@cpno",drpChapName.SelectedValue},
                                {"@job_history_id",(lds!=null && lds.Tables[0].Rows.Count>0)?lds.Tables[0].Rows[0]["job_history_id"].ToString():"" },
                                {"@emp_id",Session["employeeid"].ToString()},{"@type","1"},{"@logid",""} ,{"@stime",""},
                                {"@etime",""},{"@comments",""},{"@pages",""}
                            };

                    if (lparam != null)
                    {
                        lsql.ExcSProcedure("spInsert_LoggedEvents", lparam, CommandType.StoredProcedure);
                        LoadGrid();
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                lsql=null;lds=null;
            }
        }
    }
    protected void agvLogEvents_RowDataBound(object sender,GridViewRowEventArgs e)
    {
        try
        {
            datasourceSQL lsql = new datasourceSQL();
            if (e.Row.RowType==DataControlRowType.DataRow)
            {
                //e.Row.Cells[6].Enabled = false;
                //e.Row.Cells[7].Enabled = false;
                //e.Row.Cells[8].Enabled = false;
                //e.Row.Cells[9].Visible = false;

                Button Btn = e.Row.FindControl("btnEndLog") as Button;
                DropDownList ddl_WorkStatus = e.Row.FindControl("ddl_WorkStatus") as DropDownList;
                HiddenField LENO = e.Row.FindControl("hf_logid") as HiddenField;
                HiddenField PID = e.Row.FindControl("hf_Plogid") as HiddenField;
                HiddenField hf_jobid = e.Row.FindControl("hf_jobid") as HiddenField;
                DataSet ds = new DataSet();
                if (PID.Value != "")
                {
                    ds = lsql.GetPlogPendstatus(Convert.ToInt32(PID.Value));
                    if (ds.Tables[0].Rows[0]["PendingYN"].ToString() == "1")
                    {
                        Btn.Text = "Start";
                        ddl_WorkStatus.Enabled = false;
                        ddl_WorkStatus.SelectedValue = "2";
                        e.Row.Cells[6].Enabled = false;
                        e.Row.Cells[7].Enabled = false;
                        e.Row.Cells[5].Enabled = false;
                    }
                    else
                    {
                        Btn.Text = "End";
                        ddl_WorkStatus.Enabled = true;
                        e.Row.Cells[5].Enabled = false;
                        ddl_WorkStatus.SelectedValue = "0";
                    }
                    if (ds.Tables[0].Rows[0]["PENDDATE"].ToString() != "")
                    {
                        e.Row.Cells[5].Enabled = false;
                        e.Row.Cells[6].Enabled = false;
                        e.Row.Cells[7].Enabled = false;
                        e.Row.Cells[8].Visible = false;
                        ddl_WorkStatus.SelectedValue = "1";
                    }
                }
                else
                {
                    if (hf_jobid.Value=="7")
                        ds = lsql.GetlogPendstatusBook(Convert.ToInt32(LENO.Value));
                    else
                        ds = lsql.GetlogPendstatus(Convert.ToInt32(LENO.Value));
                    if (ds.Tables[0].Rows[0]["PendingYN"].ToString() == "1")
                    {
                        Btn.Text = "Start";
                        ddl_WorkStatus.Enabled = false;
                        ddl_WorkStatus.SelectedValue = "2";
                        e.Row.Cells[6].Enabled = false;
                        e.Row.Cells[7].Enabled = false;
                        e.Row.Cells[5].Enabled = false;
                    }
                    else
                    {
                        Btn.Text = "End";
                        ddl_WorkStatus.Enabled = true;
                        ddl_WorkStatus.SelectedValue = "0";
                    }
                    if (ds.Tables[0].Rows[0]["LENDDATE"].ToString() != "")
                    {
                        e.Row.Cells[5].Enabled = false;
                        e.Row.Cells[6].Enabled = false;
                        e.Row.Cells[7].Enabled = false;
                        e.Row.Cells[8].Visible = false;
                        ddl_WorkStatus.SelectedValue = "1";
                    }
                }
            }
        }
        catch (Exception ex)
         { throw ex; }
        finally { }
    }
    protected void agvLogEvents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)((Button)e.CommandSource).Parent.Parent;
        DropDownList oList_WorkStatus = (DropDownList)row.FindControl("ddl_WorkStatus");
        if (e.CommandArgument != null && e.CommandArgument.ToString() != "")
        {
            Button oList_Btn = (Button)row.FindControl("btnEndLog");
            TextBox pages = (TextBox)(row.Cells[5].Controls[1]);
            TextBox cmnts = (TextBox)(row.Cells[6].Controls[1]);
            if (pages.Text.Trim() == "")
                pages.Text = "0";
            HiddenField LENO = (HiddenField)row.FindControl("hf_logid");
            HiddenField PID = (HiddenField)row.FindControl("hf_Plogid");
            HiddenField hf_jobTypeid = (HiddenField)row.FindControl("hf_jobid");

            datasourceSQL lsql = new datasourceSQL();
            if (oList_WorkStatus.SelectedItem.Value.ToString() != "0")
            {
                if (oList_Btn.Text == "End")
                {
                    if (oList_WorkStatus.SelectedItem.Value.ToString() == "1")
                    {
                        if (PID.Value != "")
                        {
                            string[,] lparam = { { "@Pid", e.CommandArgument.ToString() }, { "@comments", cmnts.Text.Trim() } };
                            lsql.ExcSProcedure("spEnd_PloggedEvents", lparam, CommandType.StoredProcedure);
                        }
                        else
                        {
                            string[,] lparam = { { "@logid", e.CommandArgument.ToString() }, { "@pages", pages.Text.Trim() }, 
                                               { "@comments", cmnts.Text.Trim() },{ "@jobTypeid", hf_jobTypeid.Value.Trim() } };
                            lsql.ExcSProcedure("spEnd_loggedEvents", lparam, CommandType.StoredProcedure);
                        }
                    }
                    else if (oList_WorkStatus.SelectedItem.Value.ToString() == "2")
                    {
                        if (PID.Value != "")
                        {
                            string[,] lparam = { { "@pid", e.CommandArgument.ToString() }, { "@comments", cmnts.Text.Trim() } };
                            lsql.ExcSProcedure("spStart_PLoggedEvents_Pending", lparam, CommandType.StoredProcedure);
                        }
                        else
                        {
                            string[,] lparam = { { "@logid", e.CommandArgument.ToString() }, { "@pages", pages.Text.Trim() }, 
                                               { "@comments", cmnts.Text.Trim() },{ "@jobTypeid", hf_jobTypeid.Value.Trim() } };
                            lsql.ExcSProcedure("spStart_LoggedEvents_Pending", lparam, CommandType.StoredProcedure);
                        }
                    }
                }
                else if (oList_Btn.Text == "Start")
                {
                    if (oList_WorkStatus.SelectedItem.Value.ToString() == "2")
                    {
                        if (PID.Value != "")
                        {
                            string[,] lparam = { { "@pid", e.CommandArgument.ToString() }, { "@comments", cmnts.Text.Trim() } };
                            lsql.ExcSProcedure("spEnd_PLoggedEvents_Pending", lparam, CommandType.StoredProcedure);
                        }
                        else
                        {
                            string[,] lparam = { { "@logid", e.CommandArgument.ToString() }, { "@pages", pages.Text.Trim() }, 
                                               { "@comments", cmnts.Text.Trim() },{ "@jobTypeid", hf_jobTypeid.Value.Trim() } };
                            lsql.ExcSProcedure("spEnd_LoggedEvents_Pending", lparam, CommandType.StoredProcedure);
                        }
                    }
                }

                LoadGrid();
            }
            else
            {
                Alert("Please select status");
            }
        }
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    public void btnSearch_Click(object sender, EventArgs e)
    {
        Delphi_Articles oArt = new Delphi_Articles();
        DataSet ds = new DataSet();
        if (txtArtCode.Text.Trim() != "")
        {
            ds = oArt.getArticleSearch(txtArtCode.Text.Trim().ToUpper(), ddljobtype.SelectedValue);
            if (ds != null)
            {
                gvArticles.DataSource = ds;
                gvArticles.DataBind();
            }
        }
    }
    protected void gvArticles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceSQL tsql = new datasourceSQL();
        txtJobNumber.Text = e.CommandArgument.ToString();
        gvArticles.DataSource = null;
        gvArticles.DataBind();
        drpChapName.Items.Clear();
        DataSet ds = new DataSet();
        ds = tsql.GetChapDetBook(Convert.ToInt32(e.CommandArgument.ToString()));
        drpChapName.DataSource = ds;
        if (ds.Tables.Count > 0)
        {
            drpChapName.DataTextField = ds.Tables[0].Columns[1].ToString();
            drpChapName.DataValueField = ds.Tables[0].Columns[0].ToString();
            drpChapName.DataBind();
            drpChapName.Items.Insert(0, new ListItem("-- All --", "0"));
        }
        else
        {
            drpChapName.Items.Insert(0, new ListItem("-- All --", "0"));
        }
    }
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        datasourceSQL tsql = new datasourceSQL();
        DataSet tds = new DataSet();
        DataSet comds = new DataSet();
        try
        {
            if (Session["employeeid"] == null)
                throw new ArgumentException("Session Expired, Please relogin");

            string[,] TI_param = { { "@GroupBy", drpType.SelectedValue } };
            tds = tsql.GetTaskType(TI_param);
            ddlstyles.DataSource = tds;
            ddlstyles.DataBind();
        }
        catch (ArgumentException ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + ex.Message.ToString() + "');window.open('login.aspx','_top');</script>");

        }
        catch (Exception ex)
        { throw ex; }
        finally
        { tsql = null; tds = null; comds = null; }
    }
    protected void ddljobtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddljobtype.SelectedValue == "7")
        {
            lblchapName.Visible = true;
            drpChapName.Visible = true;
            lblJobCodeName.Text = "Cat ID :";
        }
        else if (ddljobtype.SelectedValue == "5")
        {
            lblchapName.Visible = false;
            drpChapName.Visible = false;
            lblJobCodeName.Text = "Article Code :";
        }
        else if (ddljobtype.SelectedValue == "6")
        {
            lblchapName.Visible = false;
            drpChapName.Visible = false;
            lblJobCodeName.Text = "Issue No.:";
        }
    }
}

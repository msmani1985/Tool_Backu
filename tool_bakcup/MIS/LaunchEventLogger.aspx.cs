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
using System.Net;
using System.Data.SqlClient;
using System.Diagnostics;
using Tools;
using System.Net.NetworkInformation;

public partial class LaunchEventLogger : System.Web.UI.Page
{
    string userName = "dpuser1";
    string domain = "sdsdomain";
    string password = "dpuser@1";
    Non_Launch NL = new Non_Launch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            divheader.InnerHtml = "Event Logger of " + Session["fname"].ToString() + " " + Session["sname"].ToString();
            LoadGrid();
        }
    }
    public void LoadGrid()
    {
        DataSet ds = new DataSet();
        ds = NL.getEmpAllocatedJobs(Session["employeeid"].ToString());
        agvLogEvents.DataSource = ds;
        agvLogEvents.DataBind();
    }
    protected void agvLogEvents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField FP_ID = e.Row.FindControl("hid_FP_ID") as HiddenField;
            HiddenField NL_ID = e.Row.FindControl("hf_NL_ID") as HiddenField;
            HiddenField ENO = e.Row.FindControl("hf_logid") as HiddenField;
            HiddenField JOB_HIS_ID = e.Row.FindControl("hf_Job_His_ID") as HiddenField;
            Label STime = e.Row.FindControl("txtSTime") as Label;
            Label ETime = e.Row.FindControl("txtETime") as Label;
            Button Btn = e.Row.FindControl("btnEndLog") as Button;
            DropDownList ddl_WorkStatus = e.Row.FindControl("ddl_WorkStatus") as DropDownList;
            DropDownList ddl_FileStatus = e.Row.FindControl("ddl_FileStatus") as DropDownList;
            HiddenField hf_lnkPath = e.Row.FindControl("hf_lnkPath") as HiddenField;
            TextBox PFrom = e.Row.FindControl("txtPFrom") as TextBox;
            TextBox PTo = e.Row.FindControl("txtPTo") as TextBox;
            DataSet ds = new DataSet();
            ds = NL.GetEventog(ENO.Value);
            if (ds.Tables[0].Rows[0]["EStartDate"].ToString() == "")
            {
                Btn.Text = "Start";
                ddl_WorkStatus.Enabled = false;
                ddl_FileStatus.Enabled = false;
                ddl_WorkStatus.SelectedValue = "0";
                PFrom.Enabled = false;
                PTo.Enabled = false;
            }
            else
            {
                if (ds.Tables[0].Rows[0]["Pending"].ToString() == "2" || ds.Tables[0].Rows[0]["Pending"].ToString() == "3" || ds.Tables[0].Rows[0]["Pending"].ToString() == "4")
                {
                    Btn.Text = "Start";
                    ddl_WorkStatus.Enabled = false;
                    ddl_FileStatus.Enabled = false;
                    ddl_WorkStatus.SelectedValue = ds.Tables[0].Rows[0]["Pending"].ToString();
                    PFrom.Enabled = false;
                    PTo.Enabled = false;
                }
                else
                {
                    Btn.Text = "End";
                    ddl_WorkStatus.Enabled = true;
                    ddl_FileStatus.Enabled = true;
                    ddl_WorkStatus.SelectedValue = "1";
                    PFrom.Enabled = true;
                }
            }

            if (ds.Tables[0].Rows[0]["FileStatus"].ToString() == "1")
            {
                ddl_FileStatus.Enabled = false;
            }
            else if (ds.Tables[0].Rows[0]["FileStatus"].ToString() == "3" ||
                ds.Tables[0].Rows[0]["FileStatus"].ToString() == "5")
            {
                ddl_WorkStatus.Enabled = false;
                ddl_FileStatus.Enabled = false;
            }
            else
            {
                ddl_FileStatus.Enabled = true;
            }
            if (ds.Tables[0].Rows[0]["FileStatus"].ToString() == "2")
            {
                ddl_FileStatus.Items.Clear();
                ddl_FileStatus.Items.Add(new ListItem("No Correction", "2"));
                ddl_FileStatus.Items.Add(new ListItem("Correction", "1"));
                ddl_FileStatus.Items.Add(new ListItem("Rejected for Re-Process", "3"));
                ddl_FileStatus.Items.Add(new ListItem("QC", "4"));
            }
            else
            {
                ddl_FileStatus.Items.Clear();
                ddl_FileStatus.Items.Add(new ListItem("No Correction", "2"));
                ddl_FileStatus.Items.Add(new ListItem("Correction", "1"));
                ddl_FileStatus.Items.Add(new ListItem("Rejected for Re-Process", "3"));
            }
            //if (ds.Tables[0].Rows[0]["Pending"].ToString() == "1")
            //{
            //    //ddl_WorkStatus.SelectedValue = "2";
            //}
            //else
            //{
            //    ddl_WorkStatus.SelectedValue = "0";
            //}
            ds = NL.getPathInfo(NL_ID.Value);
            if (ds.Tables[0].Rows[0]["File_path"].ToString()!="")
            {
                hf_lnkPath.Value = ds.Tables[0].Rows[0]["File_path"].ToString();
                hf_lnkPath.Visible = true;
            }
            else
            {
                hf_lnkPath.Value = "";
                hf_lnkPath.Visible = false;
            }
        }
    }
    protected void agvLogEvents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EndLogEvent")
        {
            GridViewRow row = (GridViewRow)((Button)e.CommandSource).Parent.Parent;
            DropDownList oList_WorkStatus = (DropDownList)row.FindControl("ddl_WorkStatus");
            DropDownList oList_FileStatus = (DropDownList)row.FindControl("ddl_FileStatus");
            TextBox txtPFrom = (TextBox)row.FindControl("txtPFrom");
            TextBox txtPTo = (TextBox)row.FindControl("txtPTo");
            int TotalPages = Convert.ToInt32(txtPTo.Text) - Convert.ToInt32(txtPFrom.Text) + 1;

            Button oList_Btn = (Button)row.FindControl("btnEndLog");
            //TextBox Ranges = (TextBox)(row.Cells[12].Controls[1]);
            TextBox cmnts = (TextBox)(row.Cells[13].Controls[1]);
            Launch_SQL lsql = new Launch_SQL();
            if (oList_Btn.Text == "Start")
            {
                if (oList_WorkStatus.SelectedItem.Value.ToString() != "1")
                {
                    if (oList_WorkStatus.SelectedItem.Value.ToString() == "2" || oList_WorkStatus.SelectedItem.Value.ToString() == "3" || oList_WorkStatus.SelectedItem.Value.ToString() == "4")
                    {
                        string[,] lparam = { { "@logid", e.CommandArgument.ToString() }, { "@comments", cmnts.Text.Trim() } };
                        lsql.ExcSProcedure("spLP_EPending_LoggedEvents", lparam, CommandType.StoredProcedure);
                    }
                    else
                    {
                        string[,] lparam = { { "@logid", e.CommandArgument.ToString() }, { "@comments", cmnts.Text.Trim() } };
                        lsql.ExcSProcedure("spLP_Start_loggedEvents", lparam, CommandType.StoredProcedure);
                    }
                }
                LoadGrid();
            }
            else
            {
                if (oList_WorkStatus.SelectedItem.Value.ToString() != "0")
                {
                    if (oList_WorkStatus.SelectedItem.Value.ToString() == "1")
                    {

                        string[,] lparam = { { "@logid", e.CommandArgument.ToString() }, { "@comments", cmnts.Text.Trim() }, 
                                             { "@PFrom", txtPFrom.Text }, { "@PTo", txtPTo.Text.Trim() },
                                             { "@TPages",TotalPages.ToString() },{ "@FileMove", oList_FileStatus.SelectedValue } };
                        lsql.ExcSProcedure("spLP_End_loggedEvents1", lparam, CommandType.StoredProcedure);
                    }
                    else if (oList_WorkStatus.SelectedItem.Value.ToString() == "2" || oList_WorkStatus.SelectedItem.Value.ToString() == "3" || oList_WorkStatus.SelectedItem.Value.ToString() == "4")
                    {
                        string[,] lparam = { { "@logid", e.CommandArgument.ToString() }, { "@comments", cmnts.Text.Trim() }, { "@pending", oList_WorkStatus.SelectedItem.Value.Trim() } };
                        lsql.ExcSProcedure("spLP_SPending_LoggedEvents", lparam, CommandType.StoredProcedure);
                    }

                    LoadGrid();
                }
                else
                {
                    Alert("Please select status");
                }
            }
        }
        else if (e.CommandName == "link")
        {
            using (new Impersonator(userName, domain, password))
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                HiddenField hf_lnkPath = (HiddenField)row.FindControl("hf_lnkPath");
                if (hf_lnkPath.Value != "")
                {
                    Process.Start(hf_lnkPath.Value);
                }
            }
        }
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void lnkPath_Click(object sender, EventArgs e)
    {
        string path = "";
    }
    protected void OnComments(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            HiddenField sJobID = (HiddenField)row.Cells[0].FindControl("hf_NL_ID");
            HiddenField sFPID = (HiddenField)row.Cells[0].FindControl("hf_FP_ID");
            Non_Launch nonLa=new Non_Launch();
            DataSet ds = new DataSet();
            ds = nonLa.GetFileLoggedEvents(sJobID.Value, sFPID.Value);
            gvLoggedEvents.DataSource = ds;
            gvLoggedEvents.DataBind();
            DelPopUp.Show();
        }
    }
    protected void imgbtnFinalQuoteSave_Click(object sender, EventArgs e)
    {
        DelPopUp.Hide();
    }
}
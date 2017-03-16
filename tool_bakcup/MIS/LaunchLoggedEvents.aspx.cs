using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Drawing;

public partial class LaunchLoggedEvents : System.Web.UI.Page
{
    Non_Launch nonLa = new Non_Launch();
    private static DataTable dtable = new DataTable();
    static string Filename_selectvalue = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            viewAmdDet.Visible = false;
            grdAmdDetails.Visible = false;
            gvLoggedEvents.Visible = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        grdJobDet.Visible = true;
        jobdetails.Visible = true;
        viewAmdDet.Visible = false;
        grdAmdDetails.Visible = false;
        gvLoggedEvents.Visible = false;

        DataSet ds = new DataSet();
        ds = nonLa.GetLPJobLogEvt(txtJobID.Text.Trim());
        if (ds != null && ds.Tables[0].Rows.Count>0)
        {
            lblError.Text = "";
            grdJobDet.DataSource = ds;
            grdJobDet.DataBind();
        }
        else
        {
            lblError.Text = "Project Name/JobID not Found...";
            grdJobDet.DataSource = null;
            grdJobDet.DataBind();
        }
    }
    protected void gvLoggedEvents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            DropDownList dd_Filename = (DropDownList)e.Row.FindControl("dd_FileName");
            DataSet dd_ds = new DataSet();
            DataTable dd_dt = null;
            DataView oV = null;
            if (Session["LaunchLogEvts"] != null)
            {
                dd_dt = (DataTable)Session["LaunchLogEvts"];
            }

            if (dd_Filename != null)
            {
                dd_Filename.DataTextField = "Files_Name";
                dd_Filename.DataValueField = "Files_Name";

                oV = new DataView(dd_dt);
                DataTable jstable = oV.ToTable(true, "Files_Name");
                DataRow selerow = jstable.NewRow();
                selerow["Files_Name"] = "All File Names";
                jstable.Rows.InsertAt(selerow, 0);

                dd_Filename.DataSource = jstable;
                dd_Filename.DataBind();
                if (Filename_selectvalue != "")
                    dd_Filename.SelectedValue = Filename_selectvalue;
                else
                    dd_Filename.SelectedValue = "All File Names";
            }
        }
    }
    protected void dd_FileName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dset = new DataTable();
        DataView dv = new DataView();
        dset = (DataTable)Session["LaunchLogEvts0"];
        dv = dset.DefaultView;
        if (((DropDownList)sender).SelectedIndex != 0)
            dv.RowFilter = "[Files_Name]='" + ((DropDownList)sender).SelectedItem.Text.ToString() + "'";
        else
            dv.RowFilter = "";
        DropDownList drpdn = (DropDownList)sender;
        Filename_selectvalue = ((DropDownList)sender).SelectedItem.Text.ToString();

        gvLoggedEvents.DataSource = dv;
        gvLoggedEvents.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        gvLoggedEvents.DataSource = null;
        gvLoggedEvents.DataBind();
    }
    protected void exportExl_Click(object sender, ImageClickEventArgs e)
    {
        int i = 1;
        if (dtable != null && dtable.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='12' align='center'><h4>Logged Events Report</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>File Name</b></td><td bgcolor='silver'><b>Pages</b></td><td bgcolor='silver'><b>Stage</b></td><td bgcolor='silver'><b>WorkFlow</b></td><td bgcolor='silver'><b>Start Time</b></td><td bgcolor='silver'><b>End Time</b></td><td bgcolor='silver'><b>Duration</b></td><td bgcolor='silver'><b>Remarks</b></td><td bgcolor='silver'><b>Employee Name</b></td></tr>");
            foreach (DataRow r in dtable.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["Jobid"] + "</td>");
                sbData.Append("<td>" + r["Projectname"] + "</td>");
                sbData.Append("<td>" + r["Files_Name"] + "</td>");
                sbData.Append("<td>" + r["Pages"] + "</td>");
                sbData.Append("<td>" + r["AmendName"] + "</td>");
                sbData.Append("<td>" + r["WorkFlow"] + "</td>");
                sbData.Append("<td>" + r["EStartDate"] + "</td>");
                sbData.Append("<td>" + r["EEndDate"] + "</td>");
                sbData.Append("<td>" + r["TimeDiff"] + "</td>");
                sbData.Append("<td>" + r["Remarks"] + "</td>");
                sbData.Append("<td>" + r["EMPNAME"] + "</td>");
                sbData.Append("</tr>");
                i = i + 1;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Logged_Events_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();
        }
    }
    protected void gvLoggedEvents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "FileDetails")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string JobID = ((LinkButton)row.FindControl("lblgvJobID")).Text.ToString();
            lnkEdit_Click(JobID, ((Label)row.FindControl("lblid")).Text.ToString());
        }
    }
    public void lnkEdit_Click(string JobID, string ID)
    {
        DataSet ds = new DataSet();
        ds = nonLa.GetFilewiseAmends(JobID.ToString());
        if (ds != null && ds.Tables.Count != 0)
        {
            grdFilewiseAmends.DataSource = ds.Tables[0];
            grdFilewiseAmends.DataBind();
        }
        else
        {
            grdFilewiseAmends.DataSource = null;
            grdFilewiseAmends.DataBind();
        }
        ds = nonLa.GetLPLoggedTotalTime(ID.ToString());
        if (ds != null)
        {
            lblLogTotalTime.Text = "Total Time : " + ds.Tables[0].Rows[0]["TotalTime"].ToString();
        }
        else
        {
            lblLogTotalTime.Text = "";
        }
        this.ModalPopupExtender1.Show();
    }
    protected void amd_Excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Amends_Details_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grdFilewiseAmends.AllowPaging = false;
            //this.BindGrid();

            grdFilewiseAmends.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdFilewiseAmends.HeaderRow.Cells)
            {
                cell.BackColor = grdFilewiseAmends.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdFilewiseAmends.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdFilewiseAmends.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdFilewiseAmends.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grdFilewiseAmends.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            this.ModalPopupExtender1.Show();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void grdJobDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();

        if (!e.CommandArgument.ToString().Contains("NL"))
        {
            ds = nonLa.GetLPLogEvt(e.CommandArgument.ToString());
            ds1 = nonLa.GetFilewiseAmends(e.CommandArgument.ToString());
        }
        else
        {
            ds = nonLa.GetNLLogEvt(e.CommandArgument.ToString());
            ds1 = nonLa.GetFilewiseAmendsnl(e.CommandArgument.ToString());
        }
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            Session["LaunchLogEvts"] = ds.Tables[1].Copy();
            Session["LaunchLogEvts0"] = ds.Tables[0].Copy();
            gvLoggedEvents.DataSource = ds;
            gvLoggedEvents.DataBind();
            lblError.Text = "";
            dtable = ds.Tables[0].Copy();
        }
        else
        {
            lblError.Text = "Logged Events not Found...";
            gvLoggedEvents.DataSource = null;
            gvLoggedEvents.DataBind();
        }

        grdJobDet.Visible = false;
        jobdetails.Visible = false;
        viewAmdDet.Visible = true;
        grdAmdDetails.Visible = true;
        gvLoggedEvents.Visible = true;

        GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        string id = ((Label)row.FindControl("lblid")).Text.ToString();
       
        if (ds1 != null && ds1.Tables.Count != 0)
        {
            grdAmdDetails.DataSource = ds1.Tables[0];
            grdAmdDetails.DataBind();
        }
        else
        {
            grdAmdDetails.DataSource = null;
            grdAmdDetails.DataBind();
        }
        ds = nonLa.GetLPLoggedTotalTime(id.ToString());
        if (ds != null)
        {
            lblLogTotalTime.Text = "Total Time : " + ds.Tables[0].Rows[0]["TotalTime"].ToString();
        }
        else
        {
            lblLogTotalTime.Text = "";
        }
    }
}
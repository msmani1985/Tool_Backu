using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LP_EMPLogEvts : System.Web.UI.Page
{
    Non_Launch nonLa = new Non_Launch();
    Launch_SQL lSql = new Launch_SQL();
    private static DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = lSql.FillDataSet_SP("spGetAllEmployee", null);
            drpEmpList.DataTextField = "EMPNAME";
            drpEmpList.DataValueField = "EMPLOYEE_ID";
            drpEmpList.DataSource = ds;
            drpEmpList.DataBind();
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string strStartDate = "";
        string strEndDate = "";
        try
        {
            if (Convert.ToString(txtStartDate.Text).Trim() == "")
            {
                lblError.Text = "Please select start date.";
                return;
            }
            else
            {
                strStartDate = Convert.ToString(txtStartDate.Text).Trim();
            }

            if (Convert.ToString(txtEndDate.Text).Trim() == "")
            {
                lblError.Text = "Please select end date.";
                return;
            }
            else
            {
                strEndDate = Convert.ToString(txtEndDate.Text).Trim();
            }
            if (rbLaunch.SelectedValue == "1")
            {
                if (rbEmpTeam.SelectedValue=="1")
                    ds = nonLa.GetLPEmpLogEvt(drpEmpList.SelectedValue, strStartDate, strEndDate);
                else
                    ds = nonLa.GetLPEmpLogEvtTeam(drpEmpList.SelectedValue, strStartDate, strEndDate);
            }
            else
            {
                if (rbEmpTeam.SelectedValue == "1")
                    ds = nonLa.GetNLEmpLogEvt(drpEmpList.SelectedValue, strStartDate, strEndDate);
                else
                    ds = nonLa.GetNLEmpLogEvtTeam(drpEmpList.SelectedValue, strStartDate, strEndDate);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
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
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
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
    protected void rbEmpTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpEmpList.Items.Clear();
        if(rbEmpTeam.SelectedValue=="1")
        {
            DataSet ds = new DataSet();
            ds = lSql.FillDataSet_SP("spGetAllEmployee", null);
            drpEmpList.DataTextField = "EMPNAME";
            drpEmpList.DataValueField = "EMPLOYEE_ID";
            drpEmpList.DataSource = ds;
            drpEmpList.DataBind();
            lblEmpName.Text = "Emp Name :";
        }
        else
        {
            drpEmpList.Items.Add(new ListItem("Indesign(CMB)", "100"));
            drpEmpList.Items.Add(new ListItem("QC(CMB)", "101"));
            drpEmpList.Items.Add(new ListItem("Word(CMB)", "102"));
            drpEmpList.Items.Add(new ListItem("Artwork(CMB)", "103"));
            lblEmpName.Text = "Team :";
        }
    }
}
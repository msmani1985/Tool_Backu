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
/*
/// <summary>
/// Created by: Royson
/// Creation Date: Tuesday, July 6, 2010
/// </summary>
 * */
public partial class job_costing_summary : System.Web.UI.Page
{
    private Costing oCost = new Costing();
    protected int id = 1;
    private int mins = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.popScreen();
    }
    private void popScreen()
    {
        mins = 0;
        if (Request.QueryString["jname"] != null && Request.QueryString["empid"] != null){
            this.Title = "Job Costing Break Down Sheet";
            divSummary.Visible = false;
            divSplitUp.Visible = true;
            gvJobCostingSplit.DataSource = oCost.getJobCostingByJobEmployee(Request.QueryString["jname"].ToString(), Request.QueryString["empid"].ToString());
            gvJobCostingSplit.DataBind();            
            lblEmpName.Text = Request.QueryString["empname"].ToString();
            lblJobName1.Text = Request.QueryString["jname"].ToString();
            lblLocation.Text = Request.QueryString["loc"].ToString();
        }
        else if (Request.QueryString["jname"] != null){
            divSummary.Visible = true;
            divSplitUp.Visible = false;
            gvJobCostingSummary.DataSource = oCost.getJobCostingByJobName(Request.QueryString["jname"].ToString());
            gvJobCostingSummary.DataBind();            
            lblCustomer.Text = Request.QueryString["cname"].ToString();
            lblJobName.Text = Request.QueryString["jname"].ToString();
            lblInvValue.Text = Request.QueryString["invval"].ToString();
        }
        else{
            divSummary.Visible = false;
            divSplitUp.Visible = false;
            Response.Write("Invalid Request!");
        }
    }
    protected void gvJobCostingSplit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow){
            mins =
                mins +
            Convert.ToInt32(((Label)e.Row.FindControl("lblgvSplitMins")).Text.Trim());
        }
        else if(e.Row.RowType == DataControlRowType.Footer){
            ((Label)e.Row.FindControl("lblgvSplitTotal")).Text = mins.ToString() + " (" + this.FormatTimeSpan(TimeSpan.FromMinutes(mins)) + ")";
        }
    }
    protected void gvJobCostingSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow){
            mins =
                mins +
            Convert.ToInt32(((Label)e.Row.FindControl("lblgvSummaryMins")).Text.Trim());
        }
        else if (e.Row.RowType == DataControlRowType.Footer){
            ((Label)e.Row.FindControl("lblgvSummaryTotal")).Text = mins.ToString() + " (" + this.FormatTimeSpan(TimeSpan.FromMinutes(mins)) + ")";
        }
    }
    private string FormatTimeSpan(TimeSpan span)
    {
        /*
        return span.Days.ToString("00") + "." +
               span.Hours.ToString("00") + ":" +
               span.Minutes.ToString("00") + ":" +
               span.Seconds.ToString("00");
               /*+ "." +
               span.Milliseconds.ToString("000");
                * */
        int days = span.Days; string sTime = "";
        int tothours = span.Hours + (24 * days);
        sTime = tothours.ToString() + " hrs " +
               span.Minutes.ToString("00") + " mins";
        if (sTime.Contains("-")) { sTime = sTime.Replace("-", ""); sTime = "-" + sTime; }
        return sTime;
        // + ":" + span.Seconds.ToString("00");
    }
}

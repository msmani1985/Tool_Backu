using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//Created By Naresh on 09092014 
public partial class JobReceivedReport : System.Web.UI.Page
{
    static string jobstage_selectvalue = "";
    static string custname_selectvalue = "";
    static string duedate_selectvalue = "";
    static string catsduedate_selectvalue = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //divTitle.InnerText = Request.QueryString["reporttitle"];
            DataSet Dst = new DataSet();
            datasourceIBSQL DSql = new datasourceIBSQL();
            Dst = DSql.GetJobReportDetails();
            CustomerName.DataTextField = "custname";
            CustomerName.DataValueField = "custno";
            CustomerName.DataSource = Dst;
            CustomerName.DataBind();
            ibtnExcel_Export.Visible = false;

            CustomerName.Items.Insert(0, new ListItem(" -- All Customer -- ", "0"));  //get details for all customers
            ToDate.Text = System.DateTime.Now.ToShortDateString();                    //display current  date

            DateTime today = DateTime.Today;
            DateTime DisplayMondayDate = today.AddDays(-(int)today.DayOfWeek).AddDays(1);
            FromDate.Text = DisplayMondayDate.Date.ToShortDateString();               //display monday date of current week
        }   
    }
    protected void ViewReport_Click(object sender, EventArgs e)
    {
        string[,] param = { { "@JobTypeId", JobType.SelectedValue.ToString() }, 
                            { "@customer_id", CustomerName.SelectedValue.ToString() },
                            { "@frmdate", FromDate.Text.Trim() +" " + "00:00:00.000" },
                            { "@todate", ToDate.Text.Trim() +" " + "23:59:59.000"},
                            { "@datetype","3" } };
        datasourceIBSQL obj = new datasourceIBSQL();
        DataSet rds = new DataSet();
        rds = obj.GetReprtDetailsCatstime(param, CommandType.StoredProcedure);
        Session["job_stage"] = rds;
        jobreceived.Caption = "<b>" + divTitle.InnerText + "  Between " + FromDate.Text + " and " + ToDate.Text + "  Customer Name : " + CustomerName.SelectedItem.ToString() + "</b>";

        if (rds == null)
        {
            div_Error.Visible = true;
            div_Error.InnerHtml = "No Records Found";
            ibtnExcel_Export.Visible = false;
        }
        else
        {
            div_Error.Visible = false;
            ibtnExcel_Export.Visible = true;
        }
        jobreceived.DataSource = rds;
        jobreceived.DataBind();

    }

    protected void ibtnExcel_Export_Click(object sender, ImageClickEventArgs e)
    {

        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Despatch_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
        this.EnableViewState = false;
        System.IO.StringWriter strwriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
        HtmlForm htmlfrm = new HtmlForm();
        jobreceived.Parent.Controls.Add(htmlfrm);
        htmlfrm.Attributes["runat"] = "server";
        //jobreceived.Controls.Remove((DropDownList)jobreceived.HeaderRow.FindControl("dd_jobstage"));
        jobreceived.HeaderRow.FindControl("dd_jobstage").Visible = false;
        jobreceived.HeaderRow.Cells[6].Text = "STAGES";
        htmlfrm.Controls.Add(jobreceived);
        htmlfrm.RenderControl(txtwriter);
        Response.Write(strwriter);
        Response.End();
    }

    protected void gv_jobreceived_datarowbound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            DropDownList dd_list = (DropDownList)e.Row.FindControl("dd_jobstage");
            DataSet dd_ds = null;
            if (Session["job_stage"] != null)
                dd_ds = (DataSet)Session["job_stage"];

            DataView oV = null;
            if (dd_list != null)
            {
                dd_list.DataTextField = "JOB STAGE";
                dd_list.DataValueField = "JOB STAGE";

                oV = new DataView(dd_ds.Tables[0]);
                DataTable jstable = oV.ToTable(true, "JOB STAGE");
                DataRow selerow = jstable.NewRow();
                selerow["JOB STAGE"] = "All Stages";
                jstable.Rows.InsertAt(selerow, 0);

                dd_list.DataSource = jstable;
                dd_list.DataBind();
                if (jobstage_selectvalue != "")
                    dd_list.SelectedValue = jobstage_selectvalue;

            }
            DropDownList cust_list = (DropDownList)e.Row.FindControl("dd_custname");
            if (cust_list != null)
            {
                cust_list.DataTextField = "CUSTOMER NAME";
                cust_list.DataValueField = "CUSTOMER NAME";
                oV = new DataView(dd_ds.Tables[0]);
                DataTable jstable = oV.ToTable(true, "CUSTOMER NAME");
                DataRow selerow = jstable.NewRow();
                selerow["CUSTOMER NAME"] = "All Customers";
                jstable.Rows.InsertAt(selerow, 0);
                cust_list.DataSource = jstable;
                cust_list.DataBind();
                if (custname_selectvalue != "")
                    cust_list.SelectedValue = custname_selectvalue;
            }
            DropDownList duedate_list = (DropDownList)e.Row.FindControl("dd_duedate");
            if (duedate_list != null)
            {
                duedate_list.DataTextField = "DUE DATE";
                duedate_list.DataValueField = "DUE DATE";
                oV = new DataView(dd_ds.Tables[0]);
                DataTable jstable = oV.ToTable(true, "DUE DATE");
                DataRow selerow = jstable.NewRow();
                selerow["DUE DATE"] = "All Due Dates";
                jstable.Rows.InsertAt(selerow, 0);
                duedate_list.DataSource = jstable;
                duedate_list.DataBind();
                if (duedate_selectvalue != "")
                    duedate_list.SelectedValue = duedate_selectvalue;
            }
            DropDownList catsduedate_list = (DropDownList)e.Row.FindControl("dd_catsduedate");
            if (catsduedate_list != null)
            {
                catsduedate_list.DataTextField = "CATS DUE DATE";
                catsduedate_list.DataValueField = "CATS DUE DATE";
                oV = new DataView(dd_ds.Tables[0]);
                DataTable jstable = oV.ToTable(true, "CATS DUE DATE");
                DataRow selerow = jstable.NewRow();
                selerow["CATS DUE DATE"] = "All Cats Due Dates";
                jstable.Rows.InsertAt(selerow, 0);
                catsduedate_list.DataSource = jstable;
                catsduedate_list.DataBind();
                if (catsduedate_selectvalue != "")
                    catsduedate_list.SelectedIndex = Convert.ToInt16(catsduedate_selectvalue);
            }
        }

    }
    protected void job_stage_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dset = new DataSet();
        DataView dv = new DataView();
        dset = (DataSet)Session["job_stage"];
        dv = dset.Tables[0].DefaultView;
        if (((DropDownList)sender).SelectedIndex != 0)
            dv.RowFilter = "[JOB STAGE]='" + ((DropDownList)sender).SelectedItem.Text.ToString() + "'";
        else
            dv.RowFilter = "";
        DropDownList drpdn = (DropDownList)sender;
        jobstage_selectvalue = ((DropDownList)sender).SelectedItem.Text.ToString();
        jobreceived.DataSource = dv;
        jobreceived.DataBind();
    }
    protected void custname_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dset = new DataSet();
        DataView dv = new DataView();
        dset = (DataSet)Session["job_stage"];
        dv = dset.Tables[0].DefaultView;
        if (((DropDownList)sender).SelectedIndex != 0)
            dv.RowFilter = "[CUSTOMER NAME]='" + ((DropDownList)sender).SelectedItem.Text.ToString() + "'";
        else
            dv.RowFilter = "";
        DropDownList drpdn = (DropDownList)sender;
        custname_selectvalue = ((DropDownList)sender).SelectedItem.Text.ToString();
        jobreceived.DataSource = dv;
        jobreceived.DataBind();
    }
    protected void duedate_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dset = new DataSet();
        DataView dv = new DataView();
        dset = (DataSet)Session["job_stage"];
        dv = dset.Tables[0].DefaultView;
        if (((DropDownList)sender).SelectedIndex != 0)
            dv.RowFilter = "[DUE DATE]='" + ((DropDownList)sender).SelectedItem.Text.ToString() + "'";
        else
            dv.RowFilter = "";
        DropDownList drpdn = (DropDownList)sender;
        duedate_selectvalue = ((DropDownList)sender).SelectedItem.Text.ToString();
        jobreceived.DataSource = dv;
        jobreceived.DataBind();
    }
    protected void catsduedate_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dset = new DataSet();
        DataView dv = new DataView();
        dset = (DataSet)Session["job_stage"];
        dv = dset.Tables[0].DefaultView;
        if (((DropDownList)sender).SelectedIndex != 0)
        {
            if (((DropDownList)sender).SelectedItem.Text.ToString() == "")
                dv.RowFilter = "[CATS DUE DATE] is null";
            else
                dv.RowFilter = "[CATS DUE DATE]='" + ((DropDownList)sender).SelectedItem.Text.ToString() + "'";
        }
        else
            dv.RowFilter = "";
        DropDownList drpdn = (DropDownList)sender;
        catsduedate_selectvalue = ((DropDownList)sender).SelectedIndex.ToString();
        jobreceived.DataSource = dv;
        jobreceived.DataBind();
    }
    protected void jobreceived_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
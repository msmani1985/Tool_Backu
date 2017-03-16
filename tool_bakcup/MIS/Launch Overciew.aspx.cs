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
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing;
using System.Text;
using System.Globalization;
using CrystalDecisions.Web.Design;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using Tools;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;
using SortDirection = System.Web.UI.WebControls.SortDirection;

public partial class Launch_Overciew : System.Web.UI.Page
{
    Launch la= new Launch();
    Non_Launch nonLa = new Non_Launch();
    LaunchSQL oLaunch = new LaunchSQL();
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;
    ReportDocument rep, subRep1, subRep2, subRep3;
    private static DataTable dtable = new DataTable();
    private static DataTable dtable1 = new DataTable();
    private static DataTable dtable2 = new DataTable();
    private static DataTable dtable3 = new DataTable();
    private static DataTable dtable4 = new DataTable();
    string userName = "dpisoft";
    string domain = "sdsserver2";
    string password = "India@123";
    private static string sSortExpression = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            int Year = Convert.ToInt32(DDYearList.SelectedValue);
            if (Year == 2015)
            {
                DDMonthList.Items.Clear();
                DDMonthList.Items.Insert(0, new ListItem("December", "12"));
                DDMonthList.Items.Insert(0, new ListItem("November", "11"));
                DDMonthList.Items.Insert(0, new ListItem("October", "10"));
                DDMonthList.Items.Insert(0, new ListItem("September", "9"));
                DDMonthList.Items.Insert(0, new ListItem("August", "8"));
                DDMonthList.Items.Insert(0, new ListItem("July", "7"));
                DDMonthList.Items.Insert(0, new ListItem("June", "6"));
                DDMonthList.Items.Insert(0, new ListItem("-- All --", "0"));
            }
            else
            {
                DDMonthList.Items.Clear();
                DDMonthList.Items.Insert(0, new ListItem("December", "12"));
                DDMonthList.Items.Insert(0, new ListItem("November", "11"));
                DDMonthList.Items.Insert(0, new ListItem("October", "10"));
                DDMonthList.Items.Insert(0, new ListItem("September", "9"));
                DDMonthList.Items.Insert(0, new ListItem("August", "8"));
                DDMonthList.Items.Insert(0, new ListItem("July", "7"));
                DDMonthList.Items.Insert(0, new ListItem("June", "6"));
                DDMonthList.Items.Insert(0, new ListItem("May", "5"));
                DDMonthList.Items.Insert(0, new ListItem("April", "4"));
                DDMonthList.Items.Insert(0, new ListItem("March", "3"));
                DDMonthList.Items.Insert(0, new ListItem("February", "2"));
                DDMonthList.Items.Insert(0, new ListItem("January", "1"));
                DDMonthList.Items.Insert(0, new ListItem("-- All --", "0"));
            }
            //int Month = Convert.ToInt16(DDMonthList.SelectedValue);
            //int Year = Convert.ToInt32(DDYearList.SelectedValue);
            //int Total = Month + Year * 12;
            DataSet dscust = new DataSet();
            //if (Total < 24186)
            //{
            //    dscust = la.getAllCustomers();
            //    drpCustomerSearch.DataSource = dscust;
            //    drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
            //    drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
            //    drpCustomerSearch.DataBind();
            //    drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
            //    DropLocation.Enabled = false;
            //    DropLocation.Items.Insert(0, new ListItem("-- All Location --", "0"));
            //}
            //else
            {
                dscust = nonLa.getAllCustomers();
                drpCustomerSearch.DataSource = dscust;
                drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
                drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
                drpCustomerSearch.DataBind();
                drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
                DropLocation.Enabled = false;
                DropLocation.Items.Insert(0, new ListItem("-- All Location --", "0"));
            }
            DDMonthList.SelectedValue = DateTime.Now.Month.ToString();
            DDYearList.SelectedValue = DateTime.Now.Year.ToString();
        }
    }
    private void opencon()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrSQLLaunch"].ConnectionString;
        oConn = new SqlConnection(sConStr);
        oConn.Open();
    }

    private void closecon()
    {
        if (oConn != null)
        {
            if (oConn.State != ConnectionState.Closed)
                oConn.Close();
            oConn.Dispose();
        }
    }
    private DataSet GetDataSet(string sProcedure, CommandType sCmdType)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = sCmdType;
            oCmd.CommandText = sProcedure;
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            oCmd = null;
            return ds;
        }
        catch (Exception oEx)
        {
            return null;
        }
        finally
        {
            closecon();
        }
    }
    protected void grdLaunch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int i = 0;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            DataSet ds1 = new DataSet();
            ds1.Tables.Add(dtable1.Copy());
            DropDownList drpPEName = (DropDownList)e.Row.FindControl("drpPEName");
            drpPEName.DataSource = ds1;
            drpPEName.DataValueField = ds1.Tables[0].Columns[0].ToString();
            drpPEName.DataTextField = ds1.Tables[0].Columns[0].ToString();
            drpPEName.DataBind();
            drpPEName.Items.Insert(0, new ListItem("--select--", "0"));
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string name = e.Row.Cells[2].Text.ToString();
            ImageButton report = e.Row.FindControl("imglaunchReport") as ImageButton;
            ImageButton quote = e.Row.FindControl("imglaunchQuote") as ImageButton;
            report.Visible = false;
            quote.Visible = false;
            if (e.Row.Cells[2].Text.ToString() == "&nbsp;" || e.Row.Cells[2].Text.ToString() == null)
            {
                report.Visible = false;
                quote.Visible = false;
            }
            else
            {
                report.Visible = true;
                quote.Visible = true;
            }
            Label pro_id = e.Row.FindControl("lblid") as Label;
            Label lblPROJECTNAME = e.Row.FindControl("lblPROJECTNAME") as Label;
            DataSet ds = new DataSet();
            //int Month = Convert.ToInt16(DDMonthList.SelectedValue);
            //int Year = Convert.ToInt32(DDYearList.SelectedValue);
            //int Total = Month + Year * 12;
            //if (Total < 24186)
            //    ds = la.GetDeliveryStatus(Convert.ToInt32(pro_id.Text));
            //else
            //    ds = nonLa.GetDeliveryStatus(Convert.ToInt32(pro_id.Text));

            string strProName = Regex.Replace(lblPROJECTNAME.Text.Trim().ToString(), "[^a-zA-Z0-9_]+", " ");
            string strPath = ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString();
            
            ImageButton mail = e.Row.FindControl("imgMail") as ImageButton;
            Label lblMailDetails = e.Row.FindControl("lblMailDetails") as Label;
            string m = lblMailDetails.Text.Trim().ToString();
            if (m != "")
                mail.Visible = true;
            else
                mail.Visible = false;

            Label status = e.Row.FindControl("lblStatus") as Label;
            Label Stage = e.Row.FindControl("lblStage") as Label;

            DataSet ds1 = new DataSet();
            //ds1 = nonLa.GetLOCostDetails(pro_id.Text);

            if (status.Text == "P")
                e.Row.Cells[11].CssClass = "gridP";
            else if (status.Text == "C")
                e.Row.Cells[11].CssClass = "gridC";
            else if (status.Text == "Del")
                e.Row.Cells[11].CssClass = "gridDel";
            else if (status.Text == "WIP")
                e.Row.Cells[11].CssClass = "gridWIP";

            Label lblcost = e.Row.FindControl("lblcost") as Label;
            Label lblRate = e.Row.FindControl("lblRate") as Label;
            LinkButton lblComments = e.Row.FindControl("lblComments") as LinkButton;
            Label lblJobNo = e.Row.FindControl("lblJobNo") as Label;

            //lblComments.Text = ds1.Tables[0].Rows[0]["Desc1"].ToString().Trim();
            //lblRate.Text = ds1.Tables[0].Rows[0]["CostTypeID"].ToString().Trim();
            //lblcost.Text = ds1.Tables[0].Rows[0]["Cost"].ToString().Trim();

            if (Stage.Text.Trim() == "Final Package" && lblJobNo.Text.Trim() == "")
                e.Row.BackColor = System.Drawing.Color.LightPink;

            i = i + 1;
        }
    }
    protected void grdLaunch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Launch Report")
        {
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            int Month = Convert.ToInt16(DDMonthList.SelectedValue);
            int Year = Convert.ToInt32(DDYearList.SelectedValue);
            int Total = Month + Year * 12;
            //if (Total < 24186)
            //{
            //    ViewReport(grdLaunchView.DataKeys[row.RowIndex].Values["Pro_name"].ToString());
            //}
            //else
            {
                string LP_ID = ((Label)row.FindControl("lblid")).Text.ToString();
                LPViewReport(LP_ID.ToString());
            }
        }
        if (e.CommandName == "Launch Quote")
        {
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            int Month = Convert.ToInt16(DDMonthList.SelectedValue);
            int Year = Convert.ToInt32(DDYearList.SelectedValue);
            int Total = Month + Year * 12;
            //if (Total < 24186)
            //{
            //    ViewQuote(grdLaunchView.DataKeys[row.RowIndex].Values["Pro_name"].ToString());
            //}
            //else
            {
                string LP_ID = ((Label)row.FindControl("lblid")).Text.ToString();
                LPViewQuote(LP_ID.ToString());
            }
        }
        else if (e.CommandName == "Mail Details")
        {
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            string Pro_name = Regex.Replace(grdLaunchView.DataKeys[row.RowIndex].Values["Pro_name"].ToString().Trim(), "[^a-zA-Z0-9_]+", " ").Trim();

            string year;
            if (DDYearList.SelectedValue == "0")
            {
                year = DateTime.Now.ToString("yyyy");
            }
            else
            {
                year = DDYearList.SelectedValue;
            }
            string fn = ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + DDMonthList.SelectedItem + " " + year.ToString() + "\\" + Pro_name.ToString().Trim();
            if (File.Exists(fn + ".doc"))
                openfile(fn + ".doc");
            else if (File.Exists(fn + ".docx"))
                openfile(fn + ".docx");
        }
        else if (e.CommandName == "FileDetails")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            //string JobID = ((LinkButton)row.FindControl("lbljobid")).Text.ToString();
            DataSet ds = new DataSet();
            ds = nonLa.GetLOCostDetails(((Label)row.FindControl("lblid")).Text.ToString());
            if (ds.Tables[1] != null)
            {
                grdFilewiseAmends.DataSource = ds.Tables[1];
                grdFilewiseAmends.DataBind();
            }
            else
            {
                grdFilewiseAmends.DataSource = null;
                grdFilewiseAmends.DataBind();
            }
            this.ModalPopupExtender1.Show();
            //lnkEdit_Click(JobID, ((Label)row.FindControl("lblid")).Text.ToString());
        }
        else if (e.CommandName == "InvCost")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            //string JobID = ((LinkButton)row.FindControl("lbljobid")).Text.ToString();
            DataSet ds = new DataSet();
            ds = nonLa.GetInvSum_LO(((Label)row.FindControl("lblid")).Text.ToString());
            if (ds.Tables[0] != null)
            {
                gvInvSummary.DataSource = ds.Tables[0];
                gvInvSummary.DataBind();
            }
            else
            {
                gvInvSummary.DataSource = null;
                gvInvSummary.DataBind();
            }
            this.ModalPopupExtender2.Show();
            //lnkEdit_Click(JobID, ((Label)row.FindControl("lblid")).Text.ToString());
        }
    }
    protected void grdLaunchView_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView();
        string di = "";
        try
        {
            SortDirection sortDirection = SortDirection.Ascending;
            if (ViewState[e.SortExpression] != null)
            {
                SortDirection currDirection = (SortDirection)ViewState[e.SortExpression];
                if (currDirection == SortDirection.Ascending) sortDirection = SortDirection.Descending;
            }
            ViewState[e.SortExpression] = sortDirection;
            if (sortDirection.Equals(SortDirection.Descending)) di = " desc"; dv.Table = dtable;
            dv.Sort = e.SortExpression + di;
            sSortExpression = e.SortExpression;
            grdLaunchView.DataSource = dv;
            grdLaunchView.DataBind();
        }
        catch (Exception ec)
        {

        }
        finally
        {
            dv.Dispose();
        }
    }
    private void openfile(string filename)
    {
        try
        {
            string[] fname = filename.Split('.');
            string sType = fname[4].Trim().ToString();
            if (sType == "xls")
                Response.ContentType = "application/vnd.xls";
            else if (sType == "pdf")
                Response.ContentType = "application/pdf";
            else if (sType == "doc")
                Response.ContentType = "application/msword";
            else if (sType == "docx")
                Response.ContentType = "application/vnd.ms-word.document.12";
            else if (sType == "rtf")
                Response.ContentType = "application/rtf";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            Response.Charset = "";
            Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(filename.Replace(' ', '_')));
            this.EnableViewState = false;
            Response.WriteFile(filename);
            Response.Flush();
            Response.Close();
        }
        catch (Exception Ex)
        {

        }
    }
    public void open(string filename)
    {
        try
        {
            string[] fname = filename.Split('.');
            string sType = fname[1].Trim().ToString();
            if (sType == "doc")
                Response.ContentType = "application/msword";
            else if (sType == "docx")
                Response.ContentType = "application/vnd.ms-word.document.12";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            Response.Charset = "";
            Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(filename.Replace(' ','_')));
            this.EnableViewState = false;
            Response.WriteFile(filename);
            Response.Flush();
            Response.Close();
        }
        catch (Exception Ex)
        {
           
        }
    }
    public void ViewQuote(string projectname)
    {
        LaunchDSnew Lads = new LaunchDSnew();
        LaunchQuoteDesc LaQd = new LaunchQuoteDesc();
        Launch la = new Launch();
        rep = new ReportDocument();
        try
        {
            string id;
            id = projectname.ToString();
            if (id != "")
            {
                Lads = la.ProductivityDetails("spGet_LaunchReport", new string[,] { { "@projectname", id } });
                LaQd = la.LaunchQuoteDesc("SpGetQuoteDesc", new string[,] { { "@projectname", id } });
                DataRow r = Lads.Tables[1].Rows[0];
                if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                {
                    rep = new ReportDocument();

                    rep.FileName = Server.MapPath("~/LaunchReport/LaunchQuote.rpt");
                    rep.SetDatabaseLogon("sa", "masterkey");
                    rep.SetDataSource(Lads.Tables[1]);
                    subRep1 = rep.Subreports[0];
                    subRep1.SetDataSource(LaQd.Tables[1]);
                    string filename = "Quote_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                    rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    
                }
            }
        }
        catch (Exception ex)
        { }
        finally
        {

        }

    }
    public void ViewReport(string projectname)
    {
        LaunchDSnew Lads = new LaunchDSnew();
        LaunchQuery Laq = new LaunchQuery();
        LaunchQuote LaQu = new LaunchQuote();
        LaunchLang LaLg = new LaunchLang();
        Launch la = new Launch();
        rep = new ReportDocument();
        try
        {
            string id;
            id = projectname.ToString();
            if (id != "")
            {
                Lads = la.ProductivityDetails("spGet_LaunchReport", new string[,] { { "@projectname", id } });
                DataRow r = Lads.Tables[1].Rows[0];
                if (r["TASK"].ToString() == "TE")
                {
                    Laq = la.LaunchQueryDetails("spGetQuery_Report", new string[,] { { "@projectname", id } });
                    LaQu = la.LaunchQuoteDetails("spGetTask_Report", new string[,] { { "@projectname", id } });
                    LaLg = la.LaunchLangDetails("spGetLang_Report", new string[,] { { "@projectname", id } });

                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LaunchFormTE.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
                else if (r["TASK"].ToString() == "File Conversion")
                {
                    Laq = la.LaunchQueryDetails("spGetQuery_Report", new string[,] { { "@projectname", id } });
                    LaQu = la.LaunchQuoteDetails("spGetTask_Report", new string[,] { { "@projectname", id } });

                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LaunchFormFileConv.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(Laq.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(LaQu.Tables[1]);
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));

                    }
                }
                else if (r["TASK"].ToString() == "DQA")
                {
                    Laq = la.LaunchQueryDetails("spGetQuery_Report", new string[,] { { "@projectname", id } });
                    LaQu = la.LaunchQuoteDetails("spGetTask_Report", new string[,] { { "@projectname", id } });
                    LaLg = la.LaunchLangDetails("spGetLang_Report", new string[,] { { "@projectname", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LaunchFormDQA.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
                else
                {
                    Laq = la.LaunchQueryDetails("spGetQuery_Report", new string[,] { { "@projectname", id } });
                    LaQu = la.LaunchQuoteDetails("spGetTask_Report", new string[,] { { "@projectname", id } });
                    LaLg = la.LaunchLangDetails("spGetLang_Report", new string[,] { { "@projectname", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LaunchForm.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ','_'));
                    }
                }
            }
        }
        catch (Exception ex)
        { }
        finally
        {

        }
    }
    public void LPViewQuote(string projectname)
    {
        LPQuoteValue Lads = new LPQuoteValue();
        LPQuoteDesc LaQd = new LPQuoteDesc();
        rep = new ReportDocument();
        try
        {
            string id;
            id = projectname;
            if (id != "")
            {
                Lads = nonLa.LP_QuoteValue("spGetLPQuoteValue", new string[,] { { "@LP_ID", id } });
                LaQd = nonLa.LP_QuoteDesc("spGetLPQuoteDesc", new string[,] { { "@LP_ID", id } });
                DataRow r = Lads.Tables[1].Rows[0];
                if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                {
                    rep = new ReportDocument();

                    rep.FileName = Server.MapPath("~/LaunchReport/LPQuote.rpt");
                    rep.SetDatabaseLogon("sa", "masterkey");
                    rep.SetDataSource(Lads.Tables[1]);
                    subRep1 = rep.Subreports[0];
                    subRep1.SetDataSource(LaQd.Tables[1]);
                    string filename = "Quote_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                    rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                }
            }
        }
        catch (Exception ex)
        { }
        finally
        {

        }
    }
    public void LPViewReport(string projectname)
    {
        LPForm Lads = new LPForm();
        LPQuery Laq = new LPQuery();
        LPQuote LaQu = new LPQuote();
        LPLang LaLg = new LPLang();
        rep = new ReportDocument();
        try
        {
            string id;
            id = projectname.ToString();
            if (id != "")
            {
                Lads = nonLa.LP_LaunchForm("spGetLaunchForm", new string[,] { { "@LP_ID", id } });
                DataRow r = Lads.Tables[1].Rows[0];
                if (r["TASK"].ToString().Trim() == "TE")
                {
                    Laq = nonLa.LP_LaunchQuires("spGetLPFormQueries", new string[,] { { "@LP_ID", id } });
                    LaQu = nonLa.LP_LaunchQuote("spGetLPFormQuote", new string[,] { { "@LP_ID", id } });
                    LaLg = nonLa.LP_LaunchLang("spGetLPFormLang", new string[,] { { "@LP_ID", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LPFormTE.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
                else if (r["TASK"].ToString().Trim() == "DQA")
                {
                    Laq = nonLa.LP_LaunchQuires("spGetLPFormQueries", new string[,] { { "@LP_ID", id } });
                    LaQu = nonLa.LP_LaunchQuote("spGetLPFormQuote", new string[,] { { "@LP_ID", id } });
                    LaLg = nonLa.LP_LaunchLang("spGetLPFormLang", new string[,] { { "@LP_ID", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LPFormDQA.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
                else if (r["TASK"].ToString().Trim() == "File Conversion")
                {
                    Laq = nonLa.LP_LaunchQuires("spGetLPFormQueries", new string[,] { { "@LP_ID", id } });
                    LaQu = nonLa.LP_LaunchQuote("spGetLPFormQuote", new string[,] { { "@LP_ID", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LPFormFC.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(Laq.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(LaQu.Tables[1]);
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
                else
                {
                    Laq = nonLa.LP_LaunchQuires("spGetLPFormQueries", new string[,] { { "@LP_ID", id } });
                    LaQu = nonLa.LP_LaunchQuote("spGetLPFormQuote", new string[,] { { "@LP_ID", id } });
                    LaLg = nonLa.LP_LaunchLang("spGetLPFormLang", new string[,] { { "@LP_ID", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LPForm.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
            }
        }
        catch (Exception ex)
        { }
        finally
        {

        }
    }
    protected void cmd_Save_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList al = new ArrayList();
        Hashtable val = null;
        try
        {
            foreach (GridViewRow grw in grdLaunchView.Rows)
            {
                val = new Hashtable();

                val.Add("ID", ((Label)grw.FindControl("lblid")).Text.ToString());
                val.Add("Status", ((DropDownList)grw.FindControl("DropStatus")).SelectedValue.ToString());
                val.Add("EMPID",  Session["employeeid"].ToString());
                val.Add("JobNo", ((TextBox)grw.FindControl("lblJobNo")).Text.ToString());
                al.Add(val);
            }
            int Month = Convert.ToInt16(DDMonthList.SelectedValue);
            int Year = Convert.ToInt32(DDYearList.SelectedValue);
            int Total = Month + Year * 12;
            //if (Total < 24186)
            //{
            //    if (!la.Update_DeliveryStatus(al))
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
            //    else
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
            //}
            //else
            {
                if (!nonLa.Update_DeliveryStatus(al))
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
            }
            btnSubmit_Click(sender, e);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { la = null; }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void cmd_Excel_Click(object sender, ImageClickEventArgs e)
    {
        int i = 1;
        if (dtable3 != null && dtable3.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='19' align='center'><h4>Launch Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Date</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>Project Editor</b></td><td bgcolor='silver'><b>No.of Pages</b></td><td bgcolor='silver'><b>Cost</b></td><td bgcolor='silver'><b>Invoiced Cost</b></td><td bgcolor='silver'><b>Invoiced POs</b></td><td bgcolor='silver'><b>Currency</b></td><td bgcolor='silver'><b>Page or Hour</b></td><td bgcolor='silver'><b>Status</b></td><td bgcolor='silver'><b>Stage</b></td><td bgcolor='silver'><b>Comments</b></td><td bgcolor='silver'><b>Mail Details</b></td><td bgcolor='silver'><b>WO/PO Number</b></td><td bgcolor='silver'><b>NonLaunch ID</b></td><td bgcolor='silver'><b>Software</b></td><td bgcolor='silver'><b>Quote Created</b></td></tr>");
            foreach (DataRow r in dtable3.Rows)
            {
                //DataSet ds1 = new DataSet();
                //ds1 = nonLa.GetLOCostDetails(r["pro_id"].ToString());
                string status = r["Status"].ToString();
                //if (status.ToString().Trim() == "P")
                //{
                //    status = "P";
                //}
                //else if (status.ToString().Trim() == "C")
                //{
                //    status = "C";
                //}
                //else if (r["AmendName"].ToString().Trim() == "Invoiced")
                //{
                //    status = "Del";
                //}
                //else if (r["AmendName"].ToString().Trim() != "")
                //{
                //    status = "WIP";
                //}

                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["Date"] + "</td>");
                sbData.Append("<td>" + r["Jobid"] + "</td>");
                sbData.Append("<td>" + r["Cust_name"] + "</td>");
                sbData.Append("<td>" + r["Projectname"] + "</td>");
                sbData.Append("<td>" + r["ProjectEditor"] + "</td>");
                sbData.Append("<td>" + r["pages"] + "</td>");
                sbData.Append("<td>" + r["Cost"] + "</td>");
                sbData.Append("<td>" + r["ICost"] + "</td>");
                sbData.Append("<td>" + r["Inv_Ponumber"] + "</td>");
                sbData.Append("<td>" + r["Cur"] + "</td>");
                sbData.Append("<td>" + r["CostTypeID"] + "</td>");

                if (status.ToString() == "P")
                    sbData.Append("<td  bgcolor='orange'>" + status + "</td>");
                else if (status.ToString() == "C")
                    sbData.Append("<td  bgcolor='Gray'>" + status + "</td>");
                else if (status.ToString() == "WIP")
                    sbData.Append("<td  bgcolor='LightGreen'>" + status + "</td>");
                else if (status.ToString() == "Del")
                    sbData.Append("<td  bgcolor='green'>" + status + "</td>");
                else
                    sbData.Append("<td>" + r["Status"] + "</td>");
                sbData.Append("<td>" + r["AmendName"] + "</td>");
                sbData.Append("<td>" + r["Desc1"] + "</td>");
                sbData.Append("<td>" + r["MailDetails"] + "</td>");
                sbData.Append("<td>" + r["Jobno"] + "</td>");
                sbData.Append("<td>" + r["LaunchID"] + "</td>");
                sbData.Append("<td>" + r["Soft"] + "</td>");
                sbData.Append("<td>" + r["CREATED_Quote"] + "</td>");
                sbData.Append("</tr>");
                i = i + 1;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Launch_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();                
            
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds =  new DataSet();
            DataSet ov = new DataSet();

            int Month = Convert.ToInt16(DDMonthList.SelectedValue);
            int Year = Convert.ToInt32(DDYearList.SelectedValue);
            int Total = Month + Year * 12;
            //if (Total < 24186)
            //{
            //    ds = la.overview(Convert.ToInt32(drpCustomerSearch.SelectedValue), Convert.ToInt32(DropLocation.SelectedValue), Convert.ToInt32(DDMonthList.SelectedValue), Convert.ToInt32(DDYearList.SelectedValue));
            //}
            //else
            {
                ds = nonLa.overview_new(Convert.ToInt32(drpCustomerSearch.SelectedValue), Convert.ToInt32(DropLocation.SelectedValue), Convert.ToInt32(DDMonthList.SelectedValue), Convert.ToInt32(DDYearList.SelectedValue),txtProjectName.Text);
            }
               
            dtable = new DataTable();
            dtable1 = new DataTable();
            dtable1 = ds.Tables[1].Copy();
            //dtable2 = new DataTable();
            //dtable2 = ds.Tables[2].Copy();
            grdLaunchView.DataSource = ds.Tables[0];
            grdLaunchView.DataBind();
            dtable = ds.Tables[0].Copy();
            dtable3 = ds.Tables[0].Copy();
        }
        catch (Exception Ex)
        {
        }
        finally
        {
           closecon();
        }
    }
    public void getloc(int custid)
    {
        if (Convert.ToInt16(custid) != 0)
        {
            DataSet ds8 = la.GetLocationCust(custid);
            DropLocation.DataSource = ds8;
            DropLocation.DataValueField = ds8.Tables[0].Columns[3].ToString();
            DropLocation.DataTextField = ds8.Tables[0].Columns[4].ToString();
            DropLocation.DataBind();
        }
        DropLocation.Items.Insert(0, new ListItem("-- All Location --", "0"));
    }
    public void getlocLP(string custid)
    {
        if (Convert.ToInt16(custid) != 0)
        {
            DataSet ds8 = nonLa.GetLocationCust(custid.ToString());
            DropLocation.DataSource = ds8;
            DropLocation.DataValueField = ds8.Tables[0].Columns[3].ToString();
            DropLocation.DataTextField = ds8.Tables[0].Columns[4].ToString();
            DropLocation.DataBind();
        }
        DropLocation.Items.Insert(0, new ListItem("-- All Location --", "0"));
    }
    protected void drpCustomerSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ( Convert.ToInt16(drpCustomerSearch.SelectedValue)==0)
            DropLocation.Enabled = false;
        else
            DropLocation.Enabled = true;

            int Month = Convert.ToInt16(DDMonthList.SelectedValue);
            int Year = Convert.ToInt32(DDYearList.SelectedValue);
            int Total = Month + Year * 12;
            DataSet dscust = new DataSet();
            if (Total < 24186)
            {
                getloc(Convert.ToInt16(drpCustomerSearch.SelectedValue));
            }
            else
            {
                getlocLP(drpCustomerSearch.SelectedValue);
            }
    }
    protected void drpPEName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = dtable.Copy();
        DropDownList drpPEName = (DropDownList)grdLaunchView.HeaderRow.FindControl("drpPEName");
        if (dt != null)
        {
            for (int R = 0; R <= dt.Rows.Count - 1; R++)
            {
                if ((dt.Rows[R]["projecteditor"].ToString() != drpPEName.SelectedValue))
                {
                    dt.Rows.RemoveAt(R);
                    R = -1;
                }
            }

            ds.Tables.Add(dt.Copy());
        }
        grdLaunchView.DataSource = ds.Tables[0];
        grdLaunchView.DataBind();
        dtable3 = ds.Tables[0].Copy();
    }
    protected void drpcost_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = dtable.Copy();
        DropDownList drpcost = (DropDownList)grdLaunchView.HeaderRow.FindControl("drpcost");
        if (dt != null)
        {
            for (int R = 0; R <= dt.Rows.Count - 1; R++)
            {
                if ((dt.Rows[R]["cost"].ToString() != drpcost.SelectedValue))
                {
                    dt.Rows.RemoveAt(R);
                    R = -1;
                }
            }

            ds.Tables.Add(dt.Copy());
        }
        grdLaunchView.DataSource = ds.Tables[0];
        grdLaunchView.DataBind();
        dtable3 = ds.Tables[0].Copy();
    }

    protected void DDMonthList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Year = Convert.ToInt32(DDYearList.SelectedValue);
        if (Year == 2015)
        {
            DDMonthList.Items.Clear();
            DDMonthList.Items.Insert(0, new ListItem("December", "12"));
            DDMonthList.Items.Insert(0, new ListItem("November", "11"));
            DDMonthList.Items.Insert(0, new ListItem("October", "10"));
            DDMonthList.Items.Insert(0, new ListItem("September", "9"));
            DDMonthList.Items.Insert(0, new ListItem("August", "8"));
            DDMonthList.Items.Insert(0, new ListItem("July", "7"));
            DDMonthList.Items.Insert(0, new ListItem("June", "6"));
            DDMonthList.Items.Insert(0, new ListItem("-- All --", "0"));
        }
        else
        {
            DDMonthList.Items.Clear();
            DDMonthList.Items.Insert(0, new ListItem("December", "12"));
            DDMonthList.Items.Insert(0, new ListItem("November", "11"));
            DDMonthList.Items.Insert(0, new ListItem("October", "10"));
            DDMonthList.Items.Insert(0, new ListItem("September", "9"));
            DDMonthList.Items.Insert(0, new ListItem("August", "8"));
            DDMonthList.Items.Insert(0, new ListItem("July", "7"));
            DDMonthList.Items.Insert(0, new ListItem("June", "6"));
            DDMonthList.Items.Insert(0, new ListItem("May", "5"));
            DDMonthList.Items.Insert(0, new ListItem("April", "4"));
            DDMonthList.Items.Insert(0, new ListItem("March", "3"));
            DDMonthList.Items.Insert(0, new ListItem("February", "2"));
            DDMonthList.Items.Insert(0, new ListItem("January", "1"));
            DDMonthList.Items.Insert(0, new ListItem("-- All --", "0"));
        }
        //int Month = Convert.ToInt16(DDMonthList.SelectedValue);
        //int Year = Convert.ToInt32(DDYearList.SelectedValue);
        //int Total = Month + Year * 12;
        //DataSet dscust = new DataSet();
        //if (Total < 24186)
        //{
        //    dscust = la.getAllCustomers();
        //    drpCustomerSearch.DataSource = dscust;
        //    drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
        //    drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
        //    drpCustomerSearch.DataBind();
        //    drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
        //    DropLocation.Enabled = false;
        //    DropLocation.Items.Insert(0, new ListItem("-- All Location --", "0"));
        //}
        //else
        //{
        //    dscust = nonLa.getAllCustomers();
        //    drpCustomerSearch.DataSource = dscust;
        //    drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
        //    drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
        //    drpCustomerSearch.DataBind();
        //    drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
        //    DropLocation.Enabled = false;
        //    DropLocation.Items.Insert(0, new ListItem("-- All Location --", "0"));
        //}
    }
    protected void DDYearList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Year = Convert.ToInt32(DDYearList.SelectedValue);
        if (Year == 2015)
        {
            DDMonthList.Items.Clear();
            DDMonthList.Items.Insert(0, new ListItem("December", "12"));
            DDMonthList.Items.Insert(0, new ListItem("November", "11"));
            DDMonthList.Items.Insert(0, new ListItem("October", "10"));
            DDMonthList.Items.Insert(0, new ListItem("September", "9"));
            DDMonthList.Items.Insert(0, new ListItem("August", "8"));
            DDMonthList.Items.Insert(0, new ListItem("July", "7"));
            DDMonthList.Items.Insert(0, new ListItem("June", "6"));
            DDMonthList.Items.Insert(0, new ListItem("-- All --", "0"));
        }
        else
        {
            DDMonthList.Items.Clear();
            DDMonthList.Items.Insert(0, new ListItem("December", "12"));
            DDMonthList.Items.Insert(0, new ListItem("November", "11"));
            DDMonthList.Items.Insert(0, new ListItem("October", "10"));
            DDMonthList.Items.Insert(0, new ListItem("September", "9"));
            DDMonthList.Items.Insert(0, new ListItem("August", "8"));
            DDMonthList.Items.Insert(0, new ListItem("July", "7"));
            DDMonthList.Items.Insert(0, new ListItem("June", "6"));
            DDMonthList.Items.Insert(0, new ListItem("May", "5"));
            DDMonthList.Items.Insert(0, new ListItem("April", "4"));
            DDMonthList.Items.Insert(0, new ListItem("March", "3"));
            DDMonthList.Items.Insert(0, new ListItem("February", "2"));
            DDMonthList.Items.Insert(0, new ListItem("January", "1"));
            DDMonthList.Items.Insert(0, new ListItem("-- All --", "0"));
        }
        //int Month = Convert.ToInt16(DDMonthList.SelectedValue);
        //int Year = Convert.ToInt32(DDYearList.SelectedValue);
        //int Total = Month + Year * 12;
        //DataSet dscust = new DataSet();
        //if (Total < 24186)
        //{
        //    dscust = la.getAllCustomers();
        //    drpCustomerSearch.DataSource = dscust;
        //    drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
        //    drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
        //    drpCustomerSearch.DataBind();
        //    drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
        //    DropLocation.Enabled = false;
        //    DropLocation.Items.Insert(0, new ListItem("-- All Location --", "0"));
        //}
        //else
        //{
        //    dscust = nonLa.getAllCustomers();
        //    drpCustomerSearch.DataSource = dscust;
        //    drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
        //    drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
        //    drpCustomerSearch.DataBind();
        //    drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
        //    DropLocation.Enabled = false;
        //    DropLocation.Items.Insert(0, new ListItem("-- All Location --", "0"));
        //}
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
        //if (ds != null)
        //{
        //    lblLogTotalTime.Text = "Total Time : " + ds.Tables[0].Rows[0]["TotalTime"].ToString();
        //}
        //else
        //{
        //    lblLogTotalTime.Text = "";
        //}
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


}

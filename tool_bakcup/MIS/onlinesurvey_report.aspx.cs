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
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;


public partial class onlinesurvey_report : System.Web.UI.Page
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        ibtnExcel_Export.Visible = false;  
        //gv_employeesurvey_report.Visible = false;
        if (!Page.IsPostBack)
        {
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            datasourceSQL sobj = new datasourceSQL();
            DataSet sds = new DataSet();
            try
            {
                sds = sobj.ExcProcedure("[spGet_Team]", null, CommandType.StoredProcedure);
                dd_employeeteam.DataSource = sds.Tables[0];
                dd_employeeteam.DataBind();
                dd_employeeteam.Items.Insert(0, new ListItem("-- Select a Value --", "0"));
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { sds = null; sobj = null; }
        }
    }
    
    protected void dd_employeeteam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dd_employeeteam.SelectedValue.ToString() == "0")
        {
            dd_teammember.Items.Clear();
            dd_teammember.Visible = false;
            lbl_emp.Visible = false;
            return;
        }
        datasourceSQL eobj = new datasourceSQL();
        DataSet eds = new DataSet();
        try
        {
            dd_teammember.Visible = true;
            lbl_emp.Visible = true;
            dd_teammember.Items.Clear();
            eds = eobj.ExcProcedure("[spGet_TeamMembers];2", new string[,] { { "@team_owner_id", dd_employeeteam.SelectedValue.ToString() } }, CommandType.StoredProcedure);
            dd_teammember.DataSource = eds; //.Tables[0];
            dd_teammember.DataBind();
            dd_teammember.Items.Insert(0, new ListItem("--Select a Value --", "0"));
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { eobj = null; eds = null; }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        lblerr.Text = "";
        datasourceSQL sobj = new datasourceSQL();
        DataSet onds = new DataSet();
        gv_employeesurvey_report.Caption = "<b>" + divTitle.InnerText + "  Employee Name : " + dd_teammember.SelectedItem.ToString() + "</b>";
        //divempdetails.Visible = false;
        onds = sobj.GetSurveyReport(dd_teammember.SelectedValue.ToString());
        if (onds != null)
        {
            gv_employeesurvey_report.Visible = true;
            ibtnExcel_Export.Visible = true;

        }
        else

            lblerr.Text = "No Records Found!";


        gv_employeesurvey_report.DataSource = onds;
        gv_employeesurvey_report.DataBind();
    }

    protected void dd_teammember_SelectedIndexChanged1(object sender, EventArgs e)
    {
        txt_employeenumber.Text = dd_teammember.SelectedValue.ToString();
    }

    protected void ibtnExcel_Export_Click(object sender, ImageClickEventArgs e)
    {
        string filename = "Test_Report";
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
        this.EnableViewState = true;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

        //adgdispatchedlist.RenderControl(oHtmlTextWriter);
        HtmlForm htmfrm = new HtmlForm();
        gv_employeesurvey_report.Parent.Controls.Add(htmfrm);
        htmfrm.Attributes["runat"] = "Server";
        htmfrm.Controls.Add(gv_employeesurvey_report);
        htmfrm.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
 
 } 
 
 
    }

   

 
 
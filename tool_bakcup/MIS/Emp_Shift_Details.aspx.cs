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
using System.IO;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.Data.OleDb;

public partial class Emp_Shift_Details : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    private static DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
        }
    }
    protected void grvShift_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Shift = (DropDownList)e.Row.FindControl("DropShift");
            Label empid = (Label)e.Row.FindControl("ID");
            DataSet Ds1 = new DataSet();
            Ds1 = SqlObj.GetEmpStage(Convert.ToInt16(empid.Text));
            Ds1 = SqlObj.GetShift(Ds1.Tables[0].Rows[0]["Location_ID"].ToString());
            Shift.DataSource = Ds1;
            Shift.DataTextField = "Shift_Name";
            Shift.DataValueField = "Shift_ID";
            Shift.DataBind();
            DataSet ds = new DataSet();
            ds = SqlObj.GetEmpShift(Convert.ToInt16(empid.Text));
            int i = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[i]["Shift_ID"].ToString() != "")
                {
                    Shift.Items[Shift.Items.IndexOf(Shift.Items.FindByValue(ds.Tables[0].Rows[i]["Shift_ID"].ToString()))].Selected = true;
                }
                else
                    Shift.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

    }
    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)grvShift.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in grvShift.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("Check");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
    protected void cmd_Save_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList al = new ArrayList();
        //Hashtable val = null;
        try
        {
            if (FromTxt.Text != "" && ToTxt.Text != "")
            {
                DateTime fdate = Convert.ToDateTime(FromTxt.Text);
                DateTime tdate = Convert.ToDateTime(ToTxt.Text);
                while (fdate <= tdate)
                {
                    foreach (GridViewRow grw in grvShift.Rows)
                    {
                        //val = new Hashtable();
                        Label ID = (Label)grw.FindControl("ID");
                        DropDownList Shift = (DropDownList)grw.FindControl("DropShift");
                        CheckBox txtdepends = (CheckBox)grw.FindControl("Check");
                        bool status = txtdepends.Checked;
                        int d;
                        if (status == true)
                            d = 1;
                        else
                            d = 0;
                        if (d == 1)
                        {
                            string[,] paramcol = { 
                                                { "@ID", ID.Text }, 
                                                { "@Shift_ID", Shift.SelectedValue }, 
                                                { "@SDate", fdate.ToShortDateString() } 
                                             };
                            int p = SqlObj.ExcSProcedure("spEmpShiftTrack", paramcol, CommandType.StoredProcedure);
                        }
                        //val.Add("ID", ((Label)grw.FindControl("ID")).Text.Trim().ToString());
                        //val.Add("Shift", ((DropDownList)grw.FindControl("DropShift")).SelectedValue.ToString());
                        //val.Add("Check", d);
                        //al.Add(val);
                    }
                    fdate = fdate.AddDays(1);
                }

                //if (!SqlObj.Update_ShiftDetails(al))
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
                //else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");

                ibtngridview_Click(sender, e);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Please fill the From and To Date Details');</script>");
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { SqlObj = null; }
    }
    protected void ibtngridview_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = SqlObj.GetEmpShiftTrack(Convert.ToInt16(Session["Employeeid"].ToString()), Convert.ToInt16(rbLocation.SelectedValue));
        grvShift.DataSource = ds;
        grvShift.DataBind();
        dtable = ds.Tables[0].Copy();
    }


    protected void cmd_Excel_Click(object sender, ImageClickEventArgs e)
    {
        if (dtable != null && dtable.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='6' align='center'><h4>Shift Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Employee ID</b></td><td bgcolor='silver'><b>Employee Name</b></td><td bgcolor='silver'><b>Designation</b></td><td bgcolor='silver'><b>Department</b></td><td bgcolor='silver'><b>Shift Detail</b></td></tr>");
            foreach (DataRow r in dtable.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + r["sl"] + "</td>");
                sbData.Append("<td>" + r["refno"] + "</td>");
                sbData.Append("<td>" + r["EMPNAME"] + "</td>");
                sbData.Append("<td>" + r["Designation_name"] + "</td>");
                sbData.Append("<td>" + r["Department"] + "</td>");
                //if (r["shift"].ToString() == "FS")
                //    sbData.Append("<td>" + "First Shift" + "</td>");
                //else if (r["shift"].ToString() == "SS")
                //    sbData.Append("<td>" + "Second Shift" + "</td>");
                //else if (r["shift"].ToString() == "GS")
                //    sbData.Append("<td >" + "General Shift" + "</td>");
                //else
                sbData.Append("<td>" + r["Shift_Name"] + "</td>");
                sbData.Append("</tr>");
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Shift_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();

        }
    }
    protected void cmd_Help_Click(object sender, ImageClickEventArgs e)
    {
        string strPopup = "<script language='javascript' ID='script1'>"
        // Passing intId to popup window.
        + "window.open('ShiftHelp.aspx?LocationID=" + HttpUtility.UrlEncode((rbLocation.SelectedValue))
        + "','new window', 'top=90, left=150, width=400, height=400, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
        + "</script>";

        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
    }
    public void Button1_Click(object sender, EventArgs e)
    {
        int count = 0;
        DataSet Ds = new DataSet();
        if (FileUpload1.HasFile)
        {
            if (FromTxt.Text != "" && ToTxt.Text != "")
            {
                string connectionString = "";
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string fileLocation = @"\\192.9.201.222\Mail\LaunchFiles\" + Path.GetFileNameWithoutExtension(fileName) + Convert.ToString(DateTime.Now).Replace("/", "_").Replace(":", "_") + "." + fileExtension;
                FileUpload1.SaveAs(fileLocation);
                if (fileExtension == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=NO\"";
                }
                else if (fileExtension == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }

                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmds = new OleDbCommand();
                cmds.CommandType = System.Data.CommandType.Text;
                cmds.Connection = con;
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmds);
                DataTable dtExcelRecords = new DataTable();
                con.Open();
                DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string getExcelSheetName1 = string.Empty;
                getExcelSheetName1 = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmds.CommandText = "SELECT * FROM [" + getExcelSheetName1 + "]";
                dAdapter.SelectCommand = cmds;
                dAdapter.Fill(dtExcelRecords);
                for (int i = 0; i < dtExcelRecords.Rows.Count; i++)
                {
                    datasourceSQL dsSql = new datasourceSQL();
                    DataSet ds = new DataSet();
                    int empid = Convert.ToInt32(dtExcelRecords.Rows[i][0].ToString().Replace("DDS", "").Replace("SDS", ""));
                    ds = dsSql.GetEmpDetails(empid, 3);
                    DateTime fdate = Convert.ToDateTime(FromTxt.Text);
                    DateTime tdate = Convert.ToDateTime(ToTxt.Text);
                    while (fdate <= tdate)
                    {
                        string[,] paramcol = { 
                                                { "@ID", ds.Tables[0].Rows[0]["employee_id"].ToString() }, 
                                                { "@Shift_ID", dtExcelRecords.Rows[i][1].ToString() }, 
                                                { "@SDate", fdate.ToShortDateString() } 
                                             };
                        int p = SqlObj.ExcSProcedure("spEmpShiftTrack", paramcol, CommandType.StoredProcedure);
                        fdate = fdate.AddDays(1);
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
                DataSet ds1 = new DataSet();
                ds1 = SqlObj.GetEmpShiftTrack(Convert.ToInt16(Session["Employeeid"].ToString()), Convert.ToInt16(rbLocation.SelectedValue));
                grvShift.DataSource = ds1;
                grvShift.DataBind();
                dtable = ds1.Tables[0].Copy();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Please fill the From and To Date Details');</script>");
            }
        }
    }
}

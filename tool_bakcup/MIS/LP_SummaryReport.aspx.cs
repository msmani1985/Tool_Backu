using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LP_SummaryReport : System.Web.UI.Page
{
    Non_Launch nonLa = new Non_Launch();
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    private static DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DataSet dscust = new DataSet();
            dscust = nonLa.getAllCustomers();
            drpCustomerSearch.DataSource = dscust;
            drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
            drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
            drpCustomerSearch.DataBind();
            drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
            DropLocation.Enabled = false;
            DropLocation.Items.Insert(0, new ListItem("-- All Location --", "0"));
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = nonLa.LP_Summary(Convert.ToInt32(drpCustomerSearch.SelectedValue), Convert.ToInt32(DropLocation.SelectedValue), 
            txtsdate.Text, txtedate.Text, txtProjectName.Text);
        dtable = ds.Tables[0].Copy();
        if (ds != null)
        {
            grdLaunchView.DataSource = ds.Tables[0];
            grdLaunchView.DataBind();
            //gv_Summary.DataSource = ds.Tables[1];
            //gv_Summary.DataBind();
        }
        else
        {
            grdLaunchView.DataSource = null;
            grdLaunchView.DataBind();
            //gv_Summary.DataSource = null;
            //gv_Summary.DataBind();
        }
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
        if (Convert.ToInt16(drpCustomerSearch.SelectedValue) == 0)
            DropLocation.Enabled = false;
        else
            DropLocation.Enabled = true;
        getlocLP(drpCustomerSearch.SelectedValue);
    }
    protected void cmd_Excel_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = nonLa.LP_Summary(Convert.ToInt32(drpCustomerSearch.SelectedValue), Convert.ToInt32(DropLocation.SelectedValue),
            txtsdate.Text, txtedate.Text, txtProjectName.Text);
        if (ds != null)
        {
            ds.Tables[0].TableName = "Information";
            //ds.Tables[1].TableName = "Summary";
            DataSet ds1 = new DataSet();
            ds1.Merge(ds.Tables[0]);
            //ds1.Merge(ds.Tables[1]);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds1);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= DSR" + 
                    DateTime.Now.ToString().Trim().Replace(" ", "").Replace(":", "").Replace(@"\", "").Replace(@"/", "") + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);

                    Response.Flush();
                    Response.End();
                }
            }
        }
         //int i = 1;
         //if (dtable != null && dtable.Rows.Count > 0)
         //{
         //    StringBuilder sbData = new StringBuilder();
         //    sbData.Append("<table border='1'>");
         //    sbData.Append("<tr valign='top'><td colspan='16' align='center'><h4>Launch Summary</h4></td><tr>");
         //    sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>JOB NAME</b></td><td bgcolor='silver'><b>No.of Pages</b></td><td bgcolor='silver'><b>PLATFORM</b></td><td bgcolor='silver'><b>PROCESS</b></td><td bgcolor='silver'><b>QUOTE REQUESTED</b></td><td bgcolor='silver'><b>QUOTE SUBMITTED</b></td><td bgcolor='silver'><b>QUOTE APPROVED</b></td><td bgcolor='silver'><b>PROJECT VALUE</b></td><td bgcolor='silver'><b>QUOTE STATUS</b></td><td bgcolor='silver'><b>PROJECT RECEIVED</b></td><td bgcolor='silver'><b>PROJECT DUE DATE</b></td><td bgcolor='silver'><b>PROJECT DELIVERED DATE</b></td><td bgcolor='silver'><b>INVOICE STATUS</b></td></tr>");
         //    foreach (DataRow r in dtable.Rows)
         //    {
         //        sbData.Append("<tr valign='top'>");
         //        sbData.Append("<td>" + i + "</td>");
         //        sbData.Append("<td>" + r["Jobid"] + "</td>");
         //        sbData.Append("<td>" + r["Cust_name"] + "</td>");
         //        sbData.Append("<td>" + r["Projectname"] + "</td>");
         //        sbData.Append("<td>" + r["Pages_Count"] + "</td>");
         //        sbData.Append("<td>" + r["Soft"] + "</td>");
         //        sbData.Append("<td>" + r["Task"] + "</td>");
         //        sbData.Append("<td>" + r["Created_Date"] + "</td>");
         //        sbData.Append("<td>" + r["Created_Quote"] + "</td>");
         //        sbData.Append("<td>" + r["QuoteApproved"] + "</td>");
         //        sbData.Append("<td>" + r["Desc1"] + "</td>");
         //        sbData.Append("<td>" + r["Delivery_Status"] + "</td>");
         //        sbData.Append("<td>" + r["Created_Date"] + "</td>");
         //        sbData.Append("<td>" + r["DueDate"] + "</td>");
         //        sbData.Append("<td>" + r["DesDate"] + "</td>");
         //        sbData.Append("<td>" + r["InvStatus"] + "</td>");
         //        sbData.Append("</tr>");
         //        i = i + 1;
         //    }
         //    sbData.Append("</table>");
         //    Response.Clear();
         //    Response.ContentType = "application/vnd.ms-excel";
         //    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Launch_OverAll_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
         //    Response.ContentEncoding = Encoding.Unicode;
         //    Response.BinaryWrite(Encoding.Unicode.GetPreamble());
         //    Response.Write(sbData.ToString());
         //    Response.Flush();
         //    Response.Close();
         //}
    }
}
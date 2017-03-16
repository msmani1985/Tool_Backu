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

public partial class LP_LaunchNonInvReport : System.Web.UI.Page
{
    Non_Launch nonLa = new Non_Launch();
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    private static DataTable dtable = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
        for (int i = 0; i < chkStatus.Items.Count; i++)
        {
            chkStatus.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
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
    protected void cmd_Excel_Click(object sender, ImageClickEventArgs e)
    {
        int i = 1;
        if (dtable != null && dtable.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='20' align='center'><h4>Launch Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>No.of Pages</b></td><td bgcolor='silver'><b>Platform</b></td><td bgcolor='silver'><b>Process</b></td><td bgcolor='silver'><b>QUOTE REQUESTED</b></td><td bgcolor='silver'><b>QUOTE SUBMITTED</b></td><td bgcolor='silver'><b>QUOTE APPROVED</b></td><td bgcolor='silver'><b>PROJECT VALUE</b></td><td bgcolor='silver'><b>QUOTE STATUS</b></td><td bgcolor='silver'><b>PROJECT RECEIVED</b></td><td bgcolor='silver'><b>PROJECT DUE DATE</b></td><td bgcolor='silver'><b>PROJECT DELIVERED DATE</b></td><td bgcolor='silver'><b>INVOICE STATUS</b></td><td bgcolor='silver'><b>Cost</b></td><td bgcolor='silver'><b>PO Number</b></td><td bgcolor='silver'><b>Invoiced Cost</b></td><td bgcolor='silver'><b>Invoiced POs</b></td></tr>");
            foreach (DataRow r in dtable.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["Jobid"] + "</td>");
                sbData.Append("<td>" + r["Cust_name"] + "</td>");
                sbData.Append("<td>" + r["Projectname"] + "</td>");
                sbData.Append("<td>" + r["pages"] + "</td>");
                sbData.Append("<td>" + r["Platform"] + "</td>");
                sbData.Append("<td>" + r["Process"] + "</td>");
                sbData.Append("<td>" + r["QUOTE_REQUESTED"] + "</td>");
                sbData.Append("<td>" + r["QUOTE_SUBMITTED"] + "</td>");
                sbData.Append("<td>" + r["Quote_Approved"] + "</td>");
                sbData.Append("<td>" + r["PROJECT_VALUE"] + "</td>");
                sbData.Append("<td>" + r["QUOTE_STATUS"] + "</td>");
                sbData.Append("<td>" + r["PROJECT_RECEIVED"] + "</td>");
                sbData.Append("<td>" + r["PROJECT_DueDate"] + "</td>");
                sbData.Append("<td>" + r["PROJECT_DesDate"] + "</td>");
                sbData.Append("<td>" + r["INVOICE_STATUS"] + "</td>");
                sbData.Append("<td>" + r["Cost"] + "</td>");
                sbData.Append("<td>" + r["Jobno"] + "</td>");
                sbData.Append("<td>" + r["Inv_Cost"] + "</td>");
                sbData.Append("<td>" + r["Inv_PonUmber"] + "</td>");
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
        DataSet ds = new DataSet();
        ds = nonLa.LP_NonInvReport(Convert.ToInt32(drpCustomerSearch.SelectedValue), Convert.ToInt32(DropLocation.SelectedValue),
            txtsdate.Text, txtedate.Text, txtProjectName.Text, chkStatus.SelectedValue);
        dtable = ds.Tables[0].Copy();
        if (ds != null)
        {
            grdLaunchView.DataSource = ds.Tables[0];
            grdLaunchView.DataBind();
        }
        else
        {
            grdLaunchView.DataSource = null;
            grdLaunchView.DataBind();
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
}
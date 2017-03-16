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
using System.Drawing;
using System.Text;


public partial class LeaveReport : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    private static DataTable dtable1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }
    }
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        DataSet ds1 = new DataSet();
        dtable1.Clear();
        //if (txtAppDate.Text != "")
        {
            string[,] pname1 ={ { "@empid", Session["employeeid"].ToString() }, { "@location", Session["locationid"].ToString() } 
                                , { "@from", FromTxt.Text }, { "@to", ToTxt.Text },{"@pempid",""}};
            ds1 = SqlObj.ExcProcedure("spGet_LeaveStatus_New", pname1, CommandType.StoredProcedure);
        }
        //else
        //{
        //    string[,] pname ={ { "@empid", Session["employeeid"].ToString() }, { "@location", Session["locationid"].ToString() } 
        //                        , { "@from", FromTxt.Text }, { "@to", ToTxt.Text },{"@pempid",""}};
        
        //    ds1 = SqlObj.ExcProcedure("spGet_LeaveStatus", pname, CommandType.StoredProcedure);
        //}

        if (ds1 != null)
        {
            LeaveApproveGridMgr.Visible = true;
            LeaveApproveGridMgr.DataSource = ds1;
            LeaveApproveGridMgr.DataBind();
            dtable1 = ds1.Tables[0].Copy();
        }
        else
            LeaveApproveGridMgr.Visible = false;
    }
    protected void exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        if (dtable1 != null && dtable1.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='13' align='center'><h4>Leave Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>EmpID</b></td><td bgcolor='silver'><b>Name</b></td><td bgcolor='silver'><b>Designation</b></td><td bgcolor='silver'><b>Department</b></td><td bgcolor='silver'><b>No.of Days</b></td><td bgcolor='silver'><b>Leave Type</b></td><td bgcolor='silver'><b>Leave On</b></td><td bgcolor='silver'><b>Reason</b></td><td bgcolor='silver'><b>Approver/Disapprover Name</b></td><td bgcolor='silver'><b>Mode of Communication</b></td><td bgcolor='silver'><b>Date and time of information</b></td><td bgcolor='silver'><b>Approved Date and Time</b></td></tr>");
            foreach (DataRow r in dtable1.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + r["sl"] + "</td>");
                sbData.Append("<td>" + r["refno"] + "</td>");
                sbData.Append("<td>" + r["EMPNAME"] + "</td>");
                sbData.Append("<td>" + r["DESIGNATION_NAME"] + "</td>");
                sbData.Append("<td>" + r["DEPARTMENT"] + "</td>");
                sbData.Append("<td>" + r["DAYS"] + "</td>");
                sbData.Append("<td>" + r["LEAVE_TYPE_NAME"] + "</td>");
                sbData.Append("<td>" + r["DATESDETAILS"] + "</td>");
                sbData.Append("<td>" + r["COMMENT"] + "</td>");
                sbData.Append("<td>" + r["Status"].ToString().Replace("Approved By:", "A :").Replace("Awaiting Approval :", "W :").Replace("Cancelled By: ", "C :") + "<b> / </b>" + r["Status1"].ToString().Replace("Approved By:", "A :").Replace("Awaiting Approval :", "W :").Replace("Cancelled By: ", "C :") + "</td>");
                sbData.Append("<td>" + r["ModeofInform"] + "</td>");
                sbData.Append("<td>" + r["AppliedDate"] + "</td>");
                sbData.Append("<td>" + r["RECORDED"] + "<b> / </b>" + r["RECORDED_L2"] + "</td>");
                sbData.Append("</tr>");
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Leave_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();

        }
    }
}
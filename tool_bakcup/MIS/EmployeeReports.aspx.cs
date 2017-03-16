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

public partial class EmployeeReports : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    private static DataTable dtable1 = new DataTable();
    private static DataTable dtable2 = new DataTable();
    private static DataTable dtable3 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet ds3 = new DataSet();
        
        if (!Page.IsPostBack)
        {
            //if (Convert.ToInt32(Session["employeeteamid"]) == 21) // for HR only
            //{
            string[,] pname ={ { "@empid", Session["employeeid"].ToString() }, { "@location", Session["locationid"].ToString() } 
                                , { "@from", FromTxt.Text }, { "@to", ToTxt.Text }};
            ds1 = SqlObj.ExcProcedure("spGet_LeaveStatus", pname, CommandType.StoredProcedure);
            
            //ds1 = SqlObj.ExcuteSelectProcedure("spGet_LeaveStatus", Session["locationid"].ToString(), Session["employeeid"].ToString(), "@location,@empid", "int,int", "Input,Input", CommandType.StoredProcedure);
                if (ds1 != null)
                {
                    LeaveApproveGridMgr.DataSource = ds1;
                    LeaveApproveGridMgr.DataBind();
                }
                //ds2 = SqlObj.ExcuteSelectProcedure("spGet_OTStatus", Session["locationid"].ToString(), Session["employeeid"].ToString(), "@location,@empid", "int,int", "Input,Input", CommandType.StoredProcedure);
                //if (ds2 != null)
                //{
                //    OTApproveL2.DataSource = ds2;
                //    OTApproveL2.DataBind();
                //}
                //ds3 = SqlObj.ExcuteSelectProcedure("spGet_ShiftStatus", Session["locationid"].ToString(), Session["employeeid"].ToString(), "@location,@empid", "int,int", "Input,Input", CommandType.StoredProcedure);
                //if (ds3 != null)
                //{
                //    ShiftApproveL2.DataSource = ds3;
                //    ShiftApproveL2.DataBind();
                //}
            //}
        }
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
                //sbData.Append("<td>" + r["LEAVE_IN"] + "</td>");
                //sbData.Append("<td>" + r["LEAVE_OUT"] + "</td>");
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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (dtable2 != null && dtable2.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='11' align='center'><h4>OT Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>EmpID</b></td><td bgcolor='silver'><b>Name</b></td><td bgcolor='silver'><b>Date</b></td><td bgcolor='silver'><b>Time In</b></td><td bgcolor='silver'><b>Time Out</b></td><td bgcolor='silver'><b>Overtime</b></td><td bgcolor='silver'><b>Break</b></td><td bgcolor='silver'><b>Rermarks</b></td><td bgcolor='silver'><b>Level 1</b></td><td bgcolor='silver'><b>Level 2</b></td></tr>");
            foreach (DataRow r in dtable2.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + r["sl"] + "</td>");
                sbData.Append("<td>" + r["refno"] + "</td>");
                sbData.Append("<td>" + r["EMPNAME"] + "</td>");
                sbData.Append("<td>" + r["Date"] + "</td>");
                sbData.Append("<td>" + r["TimeIn"] + "</td>");
                sbData.Append("<td>" + r["TimeOut"] + "</td>");
                sbData.Append("<td>" + r["OT"] + "</td>");
                sbData.Append("<td>" + r["OTBreak"] + "</td>");
                sbData.Append("<td>" + r["Remarks"] + "</td>");
                sbData.Append("<td>" + r["Status"] + "</td>");
                sbData.Append("<td>" + r["Status1"] + "</td>");
                sbData.Append("</tr>");
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "OT_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();

        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        if (dtable3 != null && dtable3.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='9' align='center'><h4>Shift Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>EmpID</b></td><td bgcolor='silver'><b>Name</b></td><td bgcolor='silver'><b>Date</b></td><td bgcolor='silver'><b>Shift From</b></td><td bgcolor='silver'><b>Shift To</b></td><td bgcolor='silver'><b>Rermarks</b></td><td bgcolor='silver'><b>Level 1</b></td><td bgcolor='silver'><b>Level 2</b></td></tr>");
            foreach (DataRow r in dtable3.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + r["sl"] + "</td>");
                sbData.Append("<td>" + r["refno"] + "</td>");
                sbData.Append("<td>" + r["EMPNAME"] + "</td>");
                sbData.Append("<td>" + r["FromDate"] + "</td>");
                sbData.Append("<td>" + r["PrvShift"] + "</td>");
                sbData.Append("<td>" + r["Shift"] + "</td>");
                sbData.Append("<td>" + r["Remarks"] + "</td>");
                sbData.Append("<td>" + r["Status"] + "</td>");
                sbData.Append("<td>" + r["Status1"] + "</td>");
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
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet ds3 = new DataSet();
        dtable1.Clear();
        dtable2.Clear();
        dtable3.Clear();
        string[,] pname ={ { "@empid", Session["employeeid"].ToString() }, { "@location", Session["locationid"].ToString() } 
                                , { "@from", FromTxt.Text }, { "@to", ToTxt.Text },{"@pempid",dropEmpName.SelectedValue}};
        
        ds2 = SqlObj.ExcProcedure("spGet_OTStatus", pname, CommandType.StoredProcedure);
        ds3 = SqlObj.ExcProcedure("spGet_ShiftStatus", pname, CommandType.StoredProcedure);
        if (txtAppDate.Text!="")
        {
            string[,] pname1 ={ { "@empid", Session["employeeid"].ToString() }, { "@location", Session["locationid"].ToString() } 
                                , { "@applyDate", txtAppDate.Text }, { "@from", FromTxt.Text }, { "@to", ToTxt.Text },{"@pempid",dropEmpName.SelectedValue}};
            ds1 = SqlObj.ExcProcedure("spGet_LeaveStatus_New", pname1, CommandType.StoredProcedure);
        }
        else
        {
            ds1 = SqlObj.ExcProcedure("spGet_LeaveStatus", pname, CommandType.StoredProcedure);
        }
       
        if (ds1 != null)
        {
            LeaveApproveGridMgr.Visible = true;
            LeaveApproveGridMgr.DataSource = ds1;
            LeaveApproveGridMgr.DataBind();
            dtable1 = ds1.Tables[0].Copy();
        }
        else
            LeaveApproveGridMgr.Visible = false;

        if (ds2 != null)
        {
            OTApproveL2.Visible = true;
            OTApproveL2.DataSource = ds2;
            OTApproveL2.DataBind();
            dtable2 = ds2.Tables[0].Copy();
        }
        else
            OTApproveL2.Visible = false;

        if (ds3 != null)
        {
            ShiftApproveL2.Visible = true;
            ShiftApproveL2.DataSource = ds3.Tables[1];
            ShiftApproveL2.DataBind();
            dtable3 = ds3.Tables[1].Copy();
        }
        else
            ShiftApproveL2.Visible = false;

        dropEmpName.Visible = true;
        lblempname.Visible = true;
        dropEmpName.DataSource = ds3.Tables[0];
        dropEmpName.DataTextField = ds3.Tables[0].Columns[1].ToString();
        dropEmpName.DataValueField = ds3.Tables[0].Columns[0].ToString();
        dropEmpName.DataBind();
        dropEmpName.Items.Insert(0, new ListItem("-- select --", "0"));
    }

    protected void dropEmpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds4 = new DataSet();
        DataSet ds5 = new DataSet();
        DataSet ds6 = new DataSet();
        string[,] pname1 ={ { "@empid", Session["employeeid"].ToString() }, { "@location", Session["locationid"].ToString() } 
                                , { "@from", FromTxt.Text }, { "@to", ToTxt.Text },{"@pempid",dropEmpName.SelectedValue}};
        ds5 = SqlObj.ExcProcedure("spGet_LeaveStatus1", pname1, CommandType.StoredProcedure);
        ds6 = SqlObj.ExcProcedure("spGet_OTStatus1", pname1, CommandType.StoredProcedure);
        ds4 = SqlObj.ExcProcedure("spGet_ShiftStatus1", pname1, CommandType.StoredProcedure);

        if (txtAppDate.Text != "")
        {
            string[,] pname2 ={ { "@empid", Session["employeeid"].ToString() }, { "@location", Session["locationid"].ToString() } 
                                , { "@applyDate", txtAppDate.Text }, { "@from", FromTxt.Text }, { "@to", ToTxt.Text },{"@pempid",dropEmpName.SelectedValue}};
            ds5 = SqlObj.ExcProcedure("spGet_LeaveStatus1_new", pname2, CommandType.StoredProcedure);
        }
        else
        {
            ds5 = SqlObj.ExcProcedure("spGet_LeaveStatus1", pname1, CommandType.StoredProcedure);
        }
        
        if (ds4 != null)
        {
            ShiftApproveL2.Visible = true;
            ShiftApproveL2.DataSource = ds4;
            ShiftApproveL2.DataBind();
            dtable3 = ds4.Tables[0].Copy();
        }
        else
            ShiftApproveL2.Visible = false;

        if (ds5 != null)
        {
            LeaveApproveGridMgr.Visible = true;
            LeaveApproveGridMgr.DataSource = ds5;
            LeaveApproveGridMgr.DataBind();
            dtable1 = ds5.Tables[0].Copy();
        }
        else
            LeaveApproveGridMgr.Visible = false;

        if (ds6 != null)
        {
            OTApproveL2.Visible = true;
            OTApproveL2.DataSource = ds6;
            OTApproveL2.DataBind();
            dtable2 = ds6.Tables[0].Copy();
        }
        else
            OTApproveL2.Visible = false;
    }
}

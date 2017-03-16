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
using System.Globalization;
using System.Text;
public partial class EmpAttDaily : System.Web.UI.Page
{
    private static DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        grvrpt.Visible = true;
        LoadEmpDetails(EmpNoNameTxt.Text, Txtsdate.Text, Txtedate.Text);
    }
    private void LoadEmpDetails(string empnameno, string sdate, string edate)
    {
        DataSet ds = new DataSet();
        Attendance sqlDS = new Attendance();
        ds = sqlDS.EmpAttDaily(empnameno, sdate, edate);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            grvrpt.DataSource = ds;
            grvrpt.DataBind();
            dtable = ds.Tables[0].Copy();
        }
        else
        {
            grvrpt.DataSource = null;
            grvrpt.DataBind();
            dtable = null;
        }
    }
    protected void grvrpt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label LateIn = e.Row.FindControl("LateIn") as Label;
            Label EarlyOut = e.Row.FindControl("EarlyOut") as Label;
            Label ShiftName = e.Row.FindControl("ShiftName") as Label;
            Label ShiftSta = e.Row.FindControl("ShiftSta") as Label;
            if (LateIn.Text != "0:00")
                LateIn.CssClass = "gridD";
            else
                LateIn.CssClass = "gridN";
            if (EarlyOut.Text != "0:00")
                EarlyOut.CssClass = "gridD";
            else
                EarlyOut.CssClass = "gridN";
            if (ShiftSta.Text != "0")
                ShiftName.CssClass = "gridE";
            else
                ShiftName.CssClass = "gridN";
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Txtsdate.Text = "";
        Txtedate.Text = "";
        EmpNoNameTxt.Text = "";
    }
    protected void imgbtnEventExport_Click(object sender, ImageClickEventArgs e)
    {
        if (dtable != null && dtable.Rows.Count > 0)
        {
            int i = 1;
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='30' align='center'><h4>Attendance Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>UserID</b></td><td bgcolor='silver'><b>Employee Name</b></td><td bgcolor='silver'><b>ProcessDate</b></td><td bgcolor='silver'><b>InTime</b></td><td bgcolor='silver'><b>OutTime</b></td><td bgcolor='silver'><b>TotalWrkTime</b></td><td bgcolor='silver'><b>Breaks</b></td><td bgcolor='silver'><b>ActWrkTime</b></td><td bgcolor='silver'><b>Status</b></td><td bgcolor='silver'><b>LateIn</b></td><td bgcolor='silver'><b>EarlyOut</b></td><td bgcolor='silver'><b>OverTime</b></td><td bgcolor='silver'><b>Designation</b></td><td bgcolor='silver'><b>ShiftName</b></td><td bgcolor='silver'><b>Punch1</b></td>            <td bgcolor='silver'><b>Punch2</b></td>            <td bgcolor='silver'><b>Punch3</b></td>            <td bgcolor='silver'><b>Punch4</b></td>            <td bgcolor='silver'><b>Punch5</b></td>            <td bgcolor='silver'><b>Punch6</b></td>            <td bgcolor='silver'><b>Punch7</b></td>            <td bgcolor='silver'><b>Punch8</b></td>            <td bgcolor='silver'><b>Punch9</b></td>            <td bgcolor='silver'><b>Punch10</b></td>            <td bgcolor='silver'><b>Punch11</b></td>            <td bgcolor='silver'><b>Punch12</b></td>            <td bgcolor='silver'><b>Punch13</b></td>            <td bgcolor='silver'><b>Punch14</b></td>            <td bgcolor='silver'><b>Punch15</b></td></tr>");
            foreach (DataRow r in dtable.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["UserID"] + "</td>");
                sbData.Append("<td>" + r["EmpName"] + "</td>");
                sbData.Append("<td>" + r["PDate"] + "</td>");
                sbData.Append("<td>" + r["InTime"] + "</td>");
                sbData.Append("<td>" + r["OutTime"] + "</td>");
                sbData.Append("<td>" + r["TotalMins"] + "</td>");
                sbData.Append("<td>" + r["BreakMins"] + "</td>");
                sbData.Append("<td>" + r["WrkMins"] + "</td>");
                sbData.Append("<td>" + r["Status"] + "</td>");
                sbData.Append("<td>" + r["LateIn"] + "</td>");
                sbData.Append("<td>" + r["EarlyOut"] + "</td>");
                sbData.Append("<td>" + r["OT"] + "</td>");
                sbData.Append("<td>" + r["Designation"] + "</td>");
                sbData.Append("<td>" + r["ShiftName"] + "</td>");
                sbData.Append("<td>" + r["Punch1"] + "</td>");
                sbData.Append("<td>" + r["Punch2"] + "</td>");
                sbData.Append("<td>" + r["Punch3"] + "</td>");
                sbData.Append("<td>" + r["Punch4"] + "</td>");
                sbData.Append("<td>" + r["Punch5"] + "</td>");
                sbData.Append("<td>" + r["Punch6"] + "</td>");
                sbData.Append("<td>" + r["Punch7"] + "</td>");
                sbData.Append("<td>" + r["Punch8"] + "</td>");
                sbData.Append("<td>" + r["Punch9"] + "</td>");
                sbData.Append("<td>" + r["Punch10"] + "</td>");
                sbData.Append("<td>" + r["Punch11"] + "</td>");
                sbData.Append("<td>" + r["Punch12"] + "</td>");
                sbData.Append("<td>" + r["Punch13"] + "</td>");
                sbData.Append("<td>" + r["Punch14"] + "</td>");
                sbData.Append("<td>" + r["Punch15"] + "</td>");
                sbData.Append("</tr>");
                i++;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Attendance_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();

        }
    }
}
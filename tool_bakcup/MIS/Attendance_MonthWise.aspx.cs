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
using System.Web.Services;

public partial class Attendance_MonthWise : System.Web.UI.Page
{
    datasourceSQL dsSql = new datasourceSQL();
    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    System.Globalization.CultureInfo newCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
    //    newCulture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
    //    newCulture.DateTimeFormat.DateSeparator = "/";
    //    System.Threading.Thread.CurrentThread.CurrentCulture = newCulture;
    //}
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void EmpnoRBtn_CheckedChanged(object sender, EventArgs e)
    {

        if (EmpnoRBtn.Checked == true)
            EmployeeLbl.Text = "Employee No.";
        else if (EmpNameRBtn2.Checked == true)
            EmployeeLbl.Text = "Employee Name";
        EmpNoNameTxt.Text = "";
    }
    [WebMethod]
    private void LoadEmpDetails(string empnameno, string sdate,string edate)
    {
        DataSet ds = new DataSet();
        Attendance emg = new Attendance();
        if (EmpnoRBtn.Checked == true)
        {
            ds = emg.GetEmployeeByName("spGet_AttDetailsMonthWise", new string[,] { { "@id", EmpNoNameTxt.Text }, { "@month", DropMonth.SelectedValue }, { "@year", DropYear.SelectedValue } } , CommandType.StoredProcedure);
            lblTotalHrs.Visible = false;
            txtTotalHrs.Visible = false;
            lblLateEarly.Visible = false;
            txtInOut.Visible = false;
            if (ds != null)
            {
                txtTotalHrs.Text = ds.Tables[1].Rows[0]["Time_HH:MM"].ToString();
                txtInOut.Text = ds.Tables[1].Rows[0]["LateIn_HH:MM"].ToString();
                lblTotalHrs.Visible = true;
                txtTotalHrs.Visible = true;
                lblLateEarly.Visible = true;
                txtInOut.Visible = true;
            }
        }
        else if (EmpNameRBtn2.Checked == true)
        {
            ds = emg.GetEmployeeByName("spGet_AttDetailsMonthWise", new string[,] { { "@name", EmpNoNameTxt.Text }, { "@month", DropMonth.SelectedValue }, { "@year", DropYear.SelectedValue } } , CommandType.StoredProcedure);
            lblTotalHrs.Visible = false;
            txtTotalHrs.Visible = false;
            lblLateEarly.Visible = false;
            txtInOut.Visible = false;
            if (ds != null)
            {
                txtTotalHrs.Text = ds.Tables[1].Rows[0]["Time_HH:MM"].ToString();
                txtInOut.Text = ds.Tables[1].Rows[0]["LateIn_HH:MM"].ToString();
                lblTotalHrs.Visible = true;
                txtTotalHrs.Visible = true;
                lblLateEarly.Visible = true;
                txtInOut.Visible = true;
            }
            
        }
        else
        {
            lblTotalHrs.Visible = false;
            txtTotalHrs.Visible = false;
            lblLateEarly.Visible = false;
            txtInOut.Visible = false;
            ds = emg.GetEmployeeByName("spGet_AttDetailsMonthWise", new string[,] { { "@id", EmpNoNameTxt.Text }, { "@month", DropMonth.SelectedValue }, { "@year", DropYear.SelectedValue } } , CommandType.StoredProcedure);
        }
        if (ds != null && ds.Tables[0].Rows.Count > 1)
        {
            
            grvrpt.DataSource = ds;
            grvrpt.DataBind();
            
        }
    }


    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        if (EmpnoRBtn.Checked == true || EmpNameRBtn2.Checked == true)
        {
            if (EmpNoNameTxt.Text != "")
            {
                grvrpt.Visible = true;
                LoadEmpDetails(EmpNoNameTxt.Text, DropMonth.SelectedValue, DropYear.SelectedValue);
            }
            else
            {
                msg = "Enter Employee Name/No.";
                ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
            }
        }
        else
        {
            grvrpt.Visible = true;
            LoadEmpDetails(EmpNoNameTxt.Text, DropMonth.SelectedValue, DropYear.SelectedValue);
            
        }
        //LoadEmpDetails(EmpNoNameTxt.Text, EmpnoRBtn.Checked, EmpNameRBtn2.Checked,DropMonth.SelectedValue,DropYear.SelectedValue);
    }
    protected void btnSync_Click(object sender, EventArgs e)
    {
        //LoadEmpDetails(EmpNoNameTxt.Text, DropMonth.SelectedValue, DropYear.SelectedValue);
        DataSet ds = new DataSet();
        Attendance emg = new Attendance();
        if (EmpnoRBtn.Checked == true)
        {
            ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@id", EmpNoNameTxt.Text }, { "@sdate", DropMonth.SelectedValue }, { "@edate", DropYear.SelectedValue } } , CommandType.StoredProcedure);
            lblTotalHrs.Visible = true;
            txtTotalHrs.Visible = true;
        }
        else if (EmpNameRBtn2.Checked == true)
        {
            lblTotalHrs.Visible = true;
            txtTotalHrs.Visible = true;
            ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@id", EmpNoNameTxt.Text }, { "@sdate", DropMonth.SelectedValue }, { "@edate", DropYear.SelectedValue } } , CommandType.StoredProcedure);
        }
        else
        {
            lblTotalHrs.Visible = false;
            txtTotalHrs.Visible = false;
            ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@id", EmpNoNameTxt.Text }, { "@sdate", DropMonth.SelectedValue }, { "@edate", DropYear.SelectedValue } } , CommandType.StoredProcedure);
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow row = ds.Tables[0].Rows[i];
            //DateTime dd = DateTime.ParseExact(row["ProcessDate"].ToString());
            DateTime d = Convert.ToDateTime(row["ProcessDate"].ToString());
            string inproc = "spUpdateAttDetails";
            string[,] pname ={
                    {"@Empid",row["UserID"].ToString()},{"@Name",row["UserName"].ToString()},
                    {"@Date",d.ToString() },{"@Location_ID",Session["Locationid"].ToString()},
                    {"@Intime",row["Punch1_time"].ToString()},{"@Outtime",row["Outpunch"].ToString()},
                    {"@TotalHrs",row["Total Time"].ToString()},{"@Status",row["Status"].ToString()}};
            int val3 = this.dsSql.ExcSP(inproc, pname, CommandType.StoredProcedure);
        }
    }
    protected void imgbtnEventExport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=Attendance.xls");
        this.EnableViewState = false;
        System.IO.StringWriter strwriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
        HtmlForm htmlfrm = new HtmlForm();
        grvrpt.Parent.Controls.Add(htmlfrm);
        htmlfrm.Attributes["runat"] = "server";
        htmlfrm.Controls.Add(grvrpt);
        htmlfrm.RenderControl(txtwriter);
        Response.Write(strwriter);
        Response.End();
    }
    protected void gvDistricts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "50px");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        EmpNoNameTxt.Text = "";
        EmpnoRBtn.Checked = false;
        EmpNameRBtn2.Checked = false;
        DropMonth.SelectedValue = "";
        DropYear.SelectedValue = "";
        grvrpt.Visible = false;
        lblTotalHrs.Visible = false;
        txtTotalHrs.Visible = false;
        lblLateEarly.Visible = false;
        txtInOut.Visible = false;
    }
}

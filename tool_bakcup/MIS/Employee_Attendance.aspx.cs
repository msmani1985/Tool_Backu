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

public partial class Employee_Attendance : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
        }
    }
    protected void EmpnoRBtn_CheckedChanged(object sender, EventArgs e)
    {

        if (EmpnoRBtn.Checked == true)
            EmployeeLbl.Text = "Employee No.";
        else if (EmpNameRBtn2.Checked == true)
            EmployeeLbl.Text = "Employee Name";
        EmpNoNameTxt.Text = "";
    }
    private void LoadEmpDetails(string empnameno, string sdate,string edate)
    {
        DataSet ds = new DataSet();
        Attendance emg = new Attendance();
        if (EmpnoRBtn.Checked == true)
        {
            ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@id", EmpNoNameTxt.Text }, { "@sdate", Txtsdate.Text }, { "@edate", Txtedate.Text } } , CommandType.StoredProcedure);
            lblTotalHrs.Visible = false;
            txtTotalHrs.Visible = false;
            if (ds != null)
            {
                txtTotalHrs.Text = ds.Tables[1].Rows[0]["Time_HH:MM"].ToString();
                lblTotalHrs.Visible = true;
                txtTotalHrs.Visible = true;
            }
        }
        else if (EmpNameRBtn2.Checked == true)
        {
            ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@name", EmpNoNameTxt.Text }, { "@sdate", Txtsdate.Text }, { "@edate", Txtedate.Text } } , CommandType.StoredProcedure);
            lblTotalHrs.Visible = false;
            txtTotalHrs.Visible = false;
            if (ds != null)
            {
                txtTotalHrs.Text = ds.Tables[1].Rows[0]["Time_HH:MM"].ToString();
                lblTotalHrs.Visible = true;
                txtTotalHrs.Visible = true;
            }
            
        }
        else
        {
            lblTotalHrs.Visible = false;
            txtTotalHrs.Visible = false;
            ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@id", EmpNoNameTxt.Text }, { "@sdate", Txtsdate.Text }, { "@edate", Txtedate.Text } } , CommandType.StoredProcedure);
        }
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            grvrpt.DataSource = ds;
            grvrpt.DataBind();
        }
        else
        {
            grvrpt.DataSource = null;
            grvrpt.DataBind();
        }


    }

    //private void LoadEmpDetails(string empnameno, bool EmpNoFlg, bool EmpNameFlg,string sdate,string edate)
    //{
    //    DataSet ds = new DataSet();
    //    Attendance emg = new Attendance();
    //    if (EmpNoFlg == true)
    //        ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@id", empnameno }, { "@sdate", sdate }, { "@edate", edate } } , CommandType.StoredProcedure);
    //    else if (EmpNameFlg == true)
    //        ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@name", empnameno },  { "@sdate", sdate }, { "@edate", edate } } , CommandType.StoredProcedure);
    //    if (ds != null && ds.Tables[0].Rows.Count > 1)
    //    {
    //        grvrpt.DataSource = ds;
    //        grvrpt.DataBind();
    //    }
        

    //}

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        if (EmpnoRBtn.Checked == true || EmpNameRBtn2.Checked == true)
        {
            if (EmpNoNameTxt.Text != "")
            {
                grvrpt.Visible = true;
                LoadEmpDetails(EmpNoNameTxt.Text, Txtsdate.Text, Txtedate.Text);
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
            LoadEmpDetails(EmpNoNameTxt.Text, Txtsdate.Text, Txtedate.Text);
            
        }
        //LoadEmpDetails(EmpNoNameTxt.Text, EmpnoRBtn.Checked, EmpNameRBtn2.Checked,Txtsdate.Text,Txtedate.Text);
    }
    protected void btnSync_Click(object sender, EventArgs e)
    {
        //LoadEmpDetails(EmpNoNameTxt.Text, Txtsdate.Text, Txtedate.Text);
        DataSet ds = new DataSet();
        Attendance emg = new Attendance();
        if (EmpnoRBtn.Checked == true)
        {
            ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@id", EmpNoNameTxt.Text }, { "@sdate", Txtsdate.Text }, { "@edate", Txtedate.Text } } , CommandType.StoredProcedure);
            lblTotalHrs.Visible = true;
            txtTotalHrs.Visible = true;
        }
        else if (EmpNameRBtn2.Checked == true)
        {
            lblTotalHrs.Visible = true;
            txtTotalHrs.Visible = true;
            ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@id", EmpNoNameTxt.Text }, { "@sdate", Txtsdate.Text }, { "@edate", Txtedate.Text } } , CommandType.StoredProcedure);
        }
        else
        {
            lblTotalHrs.Visible = false;
            txtTotalHrs.Visible = false;
            ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@id", EmpNoNameTxt.Text }, { "@sdate", Txtsdate.Text }, { "@edate", Txtedate.Text } } , CommandType.StoredProcedure);
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename=Attendance.xls"));
        Response.ContentType = "application/octet-stream";
        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Response.AddHeader("content-disposition", "attachment;filename=Attendance.xlsx");
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
        Txtsdate.Text = "";
        Txtedate.Text = "";
        grvrpt.Visible = false;
        lblTotalHrs.Visible = false;
        txtTotalHrs.Visible = false;
    }
    protected void AttSummary_Click(object sender, ImageClickEventArgs e)
    {
        //Attendances AttDS = new Attendances();
        //Attendance Att = new Attendance();
        //ReportDocument rep = new ReportDocument();
        //CrystalReportViewer1.Visible = true;
        ////if (id != "")
        ////{
        //if (EmpnoRBtn.Checked == true)
        //{
        //    AttDS = Att.AttendanceDetails("SpGetAttMonthWise", new string[,] { { "@id", EmpNoNameTxt.Text },  { "@sdate", Txtsdate.Text }, { "@edate", Txtedate.Text } });
        //}
        //else if (EmpnoRBtn.Checked == true)
        //{
        //    AttDS = Att.AttendanceDetails("SpGetAttMonthWise", new string[,] {  { "@name", EmpNoNameTxt.Text }, { "@sdate", Txtsdate.Text }, { "@edate", Txtedate.Text } });
        //}
        //    DataRow r = AttDS.Tables[1].Rows[0];
        //    if (AttDS != null && AttDS.Tables[1].Rows.Count > 0)
        //    {
        //        rep.FileName = Server.MapPath("~/AttendanceSummary.rpt");
        //        rep.SetDatabaseLogon("sa", "masterkey");
        //        rep.SetDataSource(AttDS.Tables[1]);
        //        CrystalReportViewer1.ReportSource = rep;
        //        CrystalReportViewer1.DataBind();
        //        string filename = "AttendanceSummary_" + r["Userid"].ToString();
        //        rep.ExportToHttpResponse(ExportFormatType.Excel, Response, true, filename.Replace(' ', '_'));
        //        //Response.ContentType = "Application/pdf";

        //    }
        //}
    }
}

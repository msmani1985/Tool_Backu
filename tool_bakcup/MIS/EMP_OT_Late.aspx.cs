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

public partial class EMP_OT_Late : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
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
    }
    protected void EmpnoRBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (EmpnoRBtn.Checked == true)
            EmployeeLbl.Text = "Employee No.";
        else if (EmpNameRBtn2.Checked == true)
            EmployeeLbl.Text = "Employee Name";
        EmpNoNameTxt.Text = "";
    }
    private void LoadEmpDetails(string empnameno, string sdate, string edate)
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
                lblTotalHrs.Visible = false;
                txtTotalHrs.Visible = false;
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
                lblTotalHrs.Visible = false;
                txtTotalHrs.Visible = false;
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
            grvrpt.DataSource = ds;
            grvrpt.DataBind();
        }
    }

    protected void imgbtnEventExport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=OT_and_Late.xls");
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
}

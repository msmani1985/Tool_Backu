using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class AttManualCorr : System.Web.UI.Page
{
    Attendance As = new Attendance();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            //BindData();
        }
    }
    protected void BindData()
    {
        string[] tempsplit = txtPdate.Text.Split('/');
        string joinstring = "/";
        string newdate = tempsplit[1] + joinstring + tempsplit[0] + joinstring + tempsplit[2];
        DateTime NextDate = Convert.ToDateTime(newdate);
        string NDate = NextDate.AddDays(1).ToString();
        DataSet ds = new DataSet();
        ds = As.GetAttPunch("select convert(varchar(30),Pdate,101) Pdate,* from Att_Punchwise where userid='" + EmpNoTxt.Text + "' and Pdate between '" + newdate + "' and '" + NDate + "' order by Punch", null, CommandType.Text);
        gvEmployeeDetails.DataSource = ds.Tables[0];
        gvEmployeeDetails.DataBind();
    }
    protected void gvEmployeeDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblEditID = (Label)gvEmployeeDetails.Rows[e.RowIndex].FindControl("lblEditID");
        Label lblEditEmpID = (Label)gvEmployeeDetails.Rows[e.RowIndex].FindControl("lblEditUserID");
        TextBox txtEditDate = (TextBox)gvEmployeeDetails.Rows[e.RowIndex].FindControl("txtEditPdate");
        string[] tempsplit = txtEditDate.Text.Split('/');
        string joinstring = "/";
        string newdate = tempsplit[1] + joinstring + tempsplit[0] + joinstring + tempsplit[2];
        DateTime NextDate = Convert.ToDateTime(newdate);
        string NDate = NextDate.AddDays(1).ToString();

        As.ExcSProcedure("spGetAttTimeBreaks1", new string[,] { { "@sdate", NextDate.ToString() }, { "@User_ID", lblEditEmpID.Text }, 
                                                                { "@ID", lblEditID.Text } }, CommandType.StoredProcedure);
        As.ExcSProcedure("spGetAttTimeBreaks3", new string[,] { { "@sdate", NDate }, { "@User_ID", lblEditEmpID.Text } }, 
                                                                    CommandType.StoredProcedure);
        
        gvEmployeeDetails.EditIndex = -1;
        BindData();
    }
    protected void gvEmployeeDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvEmployeeDetails.EditIndex = -1;
        BindData();
    }
    protected void gvEmployeeDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvEmployeeDetails.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void gvEmployeeDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblEmpID = (Label)gvEmployeeDetails.Rows[e.RowIndex].FindControl("lblEmpID");

        BindData();

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (EmpNoTxt.Text!="")
        {
            BindData();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        EmpNoTxt.Text = "";
        txtPdate.Text = "";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (EmpNoTxt.Text.ToUpper().Contains("DDS"))
        {
            string[] tempsplit = txtAddPdate.Text.Split('/');
            string joinstring = "/";
            string newdate = tempsplit[1] + joinstring + tempsplit[0] + joinstring + tempsplit[2];
            DateTime NextDate = Convert.ToDateTime(newdate);
            string NDate = NextDate.AddDays(1).ToString();
            As.ExcSProcedure("spInsertAttTime", new string[,] { { "@sdate", newdate.ToString() }, { "@User_ID", EmpNoTxt.Text.ToUpper() }, 
                                                                { "@Punch", txtAddPunch.Text } }, CommandType.StoredProcedure);
            As.ExcSProcedure("spGetAttTimeBreaks3", new string[,] { { "@sdate", NDate }, { "@User_ID", EmpNoTxt.Text.ToUpper() } },
                                                                    CommandType.StoredProcedure);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");

            BindData();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Enter Valid EmpID!..');</script>");
        }
    }
}
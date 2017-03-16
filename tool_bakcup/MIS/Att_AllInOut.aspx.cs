using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Att_AllInOut : System.Web.UI.Page
{
    public int id = 1;
    Attendance As = new Attendance();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //BindData();
        }
    }
    protected void BindData()
    {
        //string[] tempsplit = txtPdate.Text.Split('/');
        //string joinstring = "/";
        //string newdate = tempsplit[1] + joinstring + tempsplit[0] + joinstring + tempsplit[2];
        //DateTime NextDate = Convert.ToDateTime(newdate);
        DataSet ds = new DataSet();
        if (EmpNoTxt.Text!="")
            ds = As.GetAttPunch("select convert(varchar(30),Pdate,101) Pdate,convert(varchar(30),Edatetime,108) Edatetime,* from Att_AllInOutPunchs a where userid like '%" + EmpNoTxt.Text + "%' and Pdate = '" + txtPdate.Text + "' order by Userid, a.Edatetime", null, CommandType.Text);
        else
            ds = As.GetAttPunch("select convert(varchar(30),Pdate,101) Pdate,convert(varchar(30),Edatetime,108) Edatetime,* from Att_AllInOutPunchs a where Pdate = '" + txtPdate.Text + "' order by userid,a.Edatetime", null, CommandType.Text);
        if (ds != null)
        {
            grdInOut.DataSource = ds.Tables[0];
            grdInOut.DataBind();
        }
        else
        {
            grdInOut.DataSource = null;
            grdInOut.DataBind();
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if(txtPdate.Text!="")
        {
            BindData();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Enter the Date');</script>");
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        EmpNoTxt.Text = "";
        txtPdate.Text = "";
        grdInOut.DataSource = null;
        grdInOut.DataBind();
    }
    protected void grdInOut_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = 1;
            Label lblSRNO = e.Row.FindControl("lblSRNO") as Label;
            Label lblUserID = e.Row.FindControl("lblUserID") as Label;
            Label lblEmpName = e.Row.FindControl("lblEmpName") as Label;
            Label lblDate = e.Row.FindControl("lblDate") as Label;
            Label lblTime = e.Row.FindControl("lblTime") as Label;
            Label lblBreak = e.Row.FindControl("lblBreak") as Label;
            Label lblPunchs = e.Row.FindControl("lblPunchs") as Label;
            if(lblPunchs.Text.Trim()=="1")
            {
                lblUserID.Visible = true;
                lblEmpName.Visible = true;
                lblDate.Visible = true;
                lblBreak.Visible = true;
                lblSRNO.Text = id.ToString();
                lblSRNO.Visible = true;
                id++;
            }
            else
            {
                lblUserID.Visible = false;
                lblEmpName.Visible = false;
                lblDate.Visible = false;
                lblBreak.Visible = false;
                lblSRNO.Visible = false;
            }
        }
    }
}
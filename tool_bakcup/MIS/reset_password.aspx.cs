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

public partial class reset_password : System.Web.UI.Page
{
    protected int id = 1;
    private Common oCom = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.popScreen();
    }
    private void popScreen()
    {
        lblTeamOwner.Text = Session["fullname"].ToString().Trim();
        //gvEmployees.DataSource = oCom.getTeamListByOwner("1034"); //Tikoji
        gvEmployees.DataSource = oCom.getTeamListByOwner(Session["employeeid"].ToString().Trim());
        gvEmployees.DataBind();
    }
    protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "pwdreset"){
                GridViewRow row = (GridViewRow)((Control)((GridViewCommandEventArgs)e).CommandSource).Parent.Parent;
                string msg = oCom.ResetEmployeePassword(((HiddenField)row.FindControl("hfgvEmpID")).Value.Trim());
                if (!msg.ToLower().Contains("error")) { this.popScreen(); Alert("Reset Success."); }
                else Alert(msg);
            }
        }
        catch (Exception Ex)
        {
            Alert(Ex.Message);
        }
    }
    protected void gvEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((HiddenField)e.Row.FindControl("hfgvLastVisit")).Value.Trim() == "")
                e.Row.ForeColor = System.Drawing.Color.MediumBlue;
        }
    }
    public void Alert(string sMessage){
        sMessage = sMessage.Replace("'", "\'");
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + sMessage + "');</script>");
    }
}

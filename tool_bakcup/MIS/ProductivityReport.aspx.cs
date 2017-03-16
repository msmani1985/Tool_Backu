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
/// <summary>
/// Productive Report:
/// Created by: Royson
/// Creation Date: 18 Nov 08
/// </summary>
public partial class ProductivityReport : System.Web.UI.Page
{
    ProductivityBase oProdv = new ProductivityBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack){
            hideCtrls(0);
            popCtrls();
        }
    }
    private void popCtrls(){
        drpTeam.DataSource = oProdv.getTeams();
        drpTeam.DataTextField = "dname";
        drpTeam.DataValueField = "dno";
        drpTeam.DataBind();
        drpEmployee.DataSource = oProdv.getAllEmployees();
        drpEmployee.DataTextField = "column1";
        drpEmployee.DataValueField = "emp_id";
        drpEmployee.DataBind();
        txtStartdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        txtEnddate.Text = DateTime.Now.ToString("MM/dd/yyyy");
    }
    protected void rblstType_SelectedIndexChanged(object sender, EventArgs e){
        hideCtrls(int.Parse(rblstType.SelectedItem.Value));
    }
    private void hideCtrls(int t){
        if (t == 0){
            this.drpEmployee.Visible = true;
            this.drpTeam.Visible = false;
            this.drpDept.Visible = false;
            this.lblType.Text = "Employee Code:";
        }
        else if (t == 1){
            this.drpEmployee.Visible = false;
            this.drpTeam.Visible = true;
            this.drpDept.Visible = false;
            this.lblType.Text = "Team:";
        }
        else{
            this.drpEmployee.Visible = false;
            this.drpTeam.Visible = false;
            this.drpDept.Visible = true;
            this.lblType.Text = "Department:";
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e){
        if (validScreen()){
            if (rblstType.SelectedItem.Value == "0"){
                string[] aemp = oProdv.getEmployeeByID(drpEmployee.SelectedItem.Value.Trim());
                if (aemp[0] == null) { divError.InnerText = "Employee not found."; return; }
                Session["prodvempcode"] = aemp[2];
                Session["prodvempname"] = aemp[0];
                Session["prodvteamname"] = aemp[1];
                Session["prodvteamid"] = "";
            }
            else{
                Session["prodvempcode"] = "";
                Session["prodvempname"] = "";
                Session["prodvteamname"] = drpTeam.SelectedItem.Text.Trim();
                Session["prodvteamid"] = drpTeam.SelectedItem.Value.Trim();
            }
            Session["prodvsdate"] = txtStartdate.Text.Trim();
            Session["prodvedate"] = txtEnddate.Text.Trim();
            Page.RegisterStartupScript("Open", "<script language='javascript'>window.open('ProductivityReportView.aspx?repname=prodv','Preview','width=1000,height=700,left=5,top=0,toolbars=no,scrollbars=yes,status=no,resizable=yes');</script>");
        }
    }
    private bool validScreen(){
        divError.InnerText = "";
        if (rblstType.SelectedItem.Value == "0" && drpEmployee.SelectedItem.Value.Trim() == ""){
            divError.InnerText = "Enter a Employee code.";
            return false;
        }
        return true;
    }
}

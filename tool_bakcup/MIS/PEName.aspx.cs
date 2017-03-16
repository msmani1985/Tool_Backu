using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PEName : System.Web.UI.Page
{
    datasourceIBSQL ds = new datasourceIBSQL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btncreate1_Click(object sender, EventArgs e)
    {
        if (validateScreen())
        {
            ds.InsertPEName(txtFname.Text, txtSurname.Text, txtEmail.Text);
            lblresults.Text = "Inserted Successfully..!";
        }
    }
    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (txtFname.Text == "") sMessage += i++ + ". Enter First Name\\r\\n";
        if (txtSurname.Text.Trim() == "") sMessage += i++ + ". Enter Sur Name\\r\\n";
        if (txtEmail.Text == "") sMessage += i++ + ". Enter Email\\r\\n";
        
        if (i > 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + sMessage + "');</script>");
            return false;
        }
        return true;
    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        txtEmail.Text = "";
        txtFname.Text = "";
        txtSurname.Text = "";
    }
}
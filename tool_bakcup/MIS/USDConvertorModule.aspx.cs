using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class USDConvertorModule : System.Web.UI.Page
{
    datasourceIBSQL ds = new datasourceIBSQL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btncreate1_Click(object sender, EventArgs e)
    {
        if (txtInvNo.Text != "" && txtDollar.Text !="")
        {
            ds.UpdateDollarValues(txtInvNo.Text,txtDollar.Text);
            lblresults.Text = "Dollar Values Updated";
        }
        else
            lblresults.Text = "Please Fill Details Correctly..!";
    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        txtDollar.Text = "";
        txtInvNo.Text = "";
        lblresults.Text = "";
    }
}
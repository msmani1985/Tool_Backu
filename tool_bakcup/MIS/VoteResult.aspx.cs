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

public partial class VoteResult : System.Web.UI.Page
{
    datasourceSQL dsSQL = new datasourceSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!Page.IsPostBack)
            {
                fillResultDetails();
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void fillResultDetails()
    {
        DataSet dsEmp = new DataSet();
        DataSet dsVote = new DataSet();
        int intCount = 0;
        try
        {
            dsSQL = new datasourceSQL();
            dsEmp = dsSQL.GetResultDetails();
            if (dsEmp.Tables.Contains("EmployeeDetails"))
            {
                grdEmployeeMale.DataSource = dsEmp.Tables[0];
                grdEmployeeMale.DataBind();
            }
            if (dsEmp.Tables.Contains("EmployeeDetails1"))
            {
                grdEmployeeFemale.DataSource = dsEmp.Tables[1];
                grdEmployeeFemale.DataBind();
            }            
            grdEmployeeMale.Visible = true;
            grdEmployeeFemale.Visible = true;



        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

}

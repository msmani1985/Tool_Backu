using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lead_Followup : System.Web.UI.Page
{
    protected int id = 1;
    private Sales_Local oSales = new Sales_Local();
    private static DataTable dtPub;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.popScreen();
    }
    private void popScreen() {
        DataSet dsCountry = oSales.getCountryList();
        drpCountry.DataSource = dsCountry;
        drpCountry.DataValueField = dsCountry.Tables[0].Columns[2].ToString();
        drpCountry.DataTextField = dsCountry.Tables[0].Columns[1].ToString();
        drpCountry.DataBind();
        drpCountry.Items.Insert(0, new ListItem(" --- All --- ", "")); 
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        gv_Leadfollowup.DataSource = oSales.getPublishersSummary(drpCountry.SelectedItem.Value.Trim());
        gv_Leadfollowup.DataBind();
    }
    private DateTime GetNextBussinessDate(DateTime oStart, int iCounter)
    {

        while (oStart.DayOfWeek == DayOfWeek.Saturday || oStart.DayOfWeek == DayOfWeek.Sunday)
            oStart = oStart.AddDays(1);
        for (int i = 1; i <= iCounter; i++)
        {
            oStart = oStart.AddDays(1);
            while (oStart.DayOfWeek == DayOfWeek.Saturday || oStart.DayOfWeek == DayOfWeek.Sunday)
                oStart = oStart.AddDays(1);
        }
        return oStart;
    }
    protected void gv_Leadfollowup_rowdatabound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ImageButton report = e.Row.FindControl("imglaunchReport") as ImageButton;
            //ImageButton quote = e.Row.FindControl("imglaunchQuote") as ImageButton;
        }
    }
    private void alert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
    }
    protected void gv_Leadfollowup_rowcommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceIBSQL dobj = new datasourceIBSQL();
        try
        {
            if (e.CommandName == "Remove")
            {
                if (!dobj.updateSAMAuthormailsent("update Sales_Local_EmailFollowup set Obsolete='" + DateTime.Now.ToShortDateString() + "', removedby=" + Session["employeeid"] + " where Salesleadno in(" + e.CommandArgument.ToString() + ")", CommandType.Text))
                    alert("Remove failed");
            }
            btnSubmit_Click(sender, e);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { dobj = null; }
    }
}
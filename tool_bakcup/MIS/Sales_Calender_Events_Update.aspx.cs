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

public partial class Sales_Calender_Events_Update : System.Web.UI.Page
{
    private Sales oSales = new Sales();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        if (!IsPostBack) this.popScreen();
    }
    private void popScreen()
    {
        if (Request.QueryString["currentDate"] != null && 
            Request.QueryString["currentDate"].ToString() != ""){
                string sCDate = Request.QueryString["currentDate"].ToString().Trim();
                lblCurrentDate.Text = DateTime.Parse(sCDate).ToLongDateString();                
        }
        else
        {
            lblCurrentDate.Text = DateTime.Now.ToLongDateString();
        }
        loadEvent(getMDYDate(lblCurrentDate.Text, 0));
        lnkPrev.HRef = "Sales_Calender_Events_Update.aspx?currentDate=" + getMDYDate(lblCurrentDate.Text, -1);
        lnkNext.HRef = "Sales_Calender_Events_Update.aspx?currentDate=" + getMDYDate(lblCurrentDate.Text, 1);
        lnkBack.HRef = "Sales_Calender_Events.aspx?currentDate=" + getMDYDate(lblCurrentDate.Text, 0);

    }
    private void loadEvent(string sDate)
    {
        DataSet dsEvent = null;
        try
        {
            dsEvent = oSales.getEvent(sDate);
            if (dsEvent.Tables[0].Rows.Count > 0)
            {
                txtComment.Text = dsEvent.Tables[0].Rows[0]["e_desc"].ToString().Trim();
            }
            else txtComment.Text = "";
        }
        catch (Exception ee)
        {
            Alert(ee.Message);
        }
        finally
        {
            if (dsEvent != null) dsEvent.Dispose();
        }
    }
    private string getMDYDate(string sDate,int AddDays)
    {
        return DateTime.Parse(lblCurrentDate.Text).AddDays(AddDays).ToString("M/d/yyyy");
    }
    protected void btnUpdateCal_Click(object sender, EventArgs e)
    {
        if (lblCurrentDate.Text.Trim() != "")
        {
            string dt = oSales.InsertUpdateEvent(getMDYDate(lblCurrentDate.Text.Trim(), 0), txtComment.Text.Trim());
            Alert("Successfully Saved.");
        }
    }
    public void Alert(string sMessage)
    {
        sMessage = sMessage.Replace("\r", "\\r");
        sMessage = sMessage.Replace("\n", "\\n");
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
}

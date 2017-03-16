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

/*
/// <summary>
/// Created by: Royson
/// Creation Date: Friday, September 10, 2010
/// </summary>
 * */
public partial class payment_on_account : System.Web.UI.Page
{
	protected int rowid = 1;
	private biz_IB oIB = new biz_IB();
	private Sales oSal = new Sales();
	private static string CustID = "";
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack) this.popScreen();
	}

	private void popScreen() {
		oIB = new biz_IB();
		oSal = new Sales();
		if (Session["locationid"].ToString() == "1"//Dublin
            || Session["employeeteamid"].ToString() == "1"//Software
            || Session["employeeid"].ToString() == "1116" //Nisha
            || Session["employeeid"].ToString() == "986"
			|| Session["employeeid"].ToString() == "1336"){//Suresh
                form1.Visible = true;
        }
        else{
            form1.Visible = false;
            Response.Write("<h1>Access Denied!</h1>");
            return;
        }
        CustID = "";
        if (Request.QueryString["cid"] != null && Request.QueryString["cid"].ToString() != "")
        {
            lblCustName.Text = Request.QueryString["cname"].ToString() ?? "Unknown Customer!";
            CustID = Request.QueryString["cid"].ToString().Trim();
            DataSet dsp = new DataSet();
            if (Request.QueryString["save"] != null)
            {
                dsp = oSal.getCustomerPaymentOnAccount(CustID);
            }
            else
            {
                dsp = oSal.getCustomerPaymentOnAccount(CustID);
                //dsp = oIB.getCrediOnAccount(CustID);
            }
            gvPayment.DataSource = dsp;
            gvPayment.DataBind();
        }
        else Alert("Unknown Customer!");
    }
    public void Alert(string sMessage)
    {
        sMessage = sMessage.Replace("\r", "\\r");
        sMessage = sMessage.Replace("\n", "\\n");
        sMessage = sMessage.Replace("'", "\'");
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow){
            if (((HiddenField)e.Row.FindControl("hfgvCreditID")).Value.Trim() == ""){
                ((Label)e.Row.FindControl("lblgvCreditDate")).Visible = false;                
                ((Label)e.Row.FindControl("lblgvCreditValue")).Visible = false;
                ((TextBox)e.Row.FindControl("txtgvCreditDate")).Visible = true;
                ((ImageButton)e.Row.FindControl("imgCalendar")).Visible = true;                
                ((TextBox)e.Row.FindControl("txtgvCreditValue")).Visible = true;
            }
            else{
                ((Label)e.Row.FindControl("lblgvCreditDate")).Visible = true;                
                ((Label)e.Row.FindControl("lblgvCreditValue")).Visible = true;
                ((TextBox)e.Row.FindControl("txtgvCreditDate")).Visible = false;
                ((ImageButton)e.Row.FindControl("imgCalendar")).Visible = false;
                ((TextBox)e.Row.FindControl("txtgvCreditValue")).Visible = false;
            }
            ((LinkButton)e.Row.FindControl("lnkbtngvDelete")).OnClientClick = "javascript:return confirm('Confirm delete this item?');";
            ((ImageButton)e.Row.FindControl("imgCalendar")).OnClientClick =
                    "javascript:calendar_window=window.open('calendar.aspx?formname=" + ((TextBox)e.Row.FindControl("txtgvCreditDate")).ClientID + "','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus();return false;";
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dstemp = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("creditid");
            dt.Columns.Add("credited_date");
            dt.Columns.Add("credited_value");
            dstemp.Tables.Add(dt);
            foreach (GridViewRow r in gvPayment.Rows){
                DataRow ro = dstemp.Tables[0].NewRow();
                ro["creditid"] = ((HiddenField)r.FindControl("hfgvCreditID")).Value.Trim();
                ro["credited_date"] = ((TextBox)r.FindControl("txtgvCreditDate")).Text.Trim();
                ro["credited_value"] = ((TextBox)r.FindControl("txtgvCreditValue")).Text.Trim();
                dstemp.Tables[0].Rows.Add(ro);
            }
            for (int i = 0; i < int.Parse(drpRows.SelectedItem.Value); i++){
                DataRow ro = dstemp.Tables[0].NewRow();
                ro["creditid"] = "";
                ro["credited_date"] = DateTime.Now.ToString("MM/dd/yyyy");
                ro["credited_value"] = "";
                dstemp.Tables[0].Rows.InsertAt(ro, 0);
            }
            gvPayment.DataSource = dstemp;
            gvPayment.DataBind();
            gvPayment.Rows[0].FindControl("txtgvCreditDate").Focus();
        }
        catch (Exception ex)
        {
            Alert(ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try{
            ArrayList savelist = new ArrayList();
            foreach (GridViewRow r in gvPayment.Rows){
                if (((HiddenField)r.FindControl("hfgvCreditID")).Value.Trim() == ""
                    && ((TextBox)r.FindControl("txtgvCreditDate")).Text.Trim() != ""
                    && ((TextBox)r.FindControl("txtgvCreditValue")).Text.Trim() != ""){
                            if (!((TextBox)r.FindControl("txtgvCreditValue")).Text.Trim().StartsWith("-")) ((TextBox)r.FindControl("txtgvCreditValue")).Text = "-" + ((TextBox)r.FindControl("txtgvCreditValue")).Text.Trim();
                    savelist.Add(new ListItem("insert", "0|" + CustID + "|" + ((TextBox)r.FindControl("txtgvCreditValue")).Text.Trim() + "|" +
                    ((TextBox)r.FindControl("txtgvCreditDate")).Text.Trim()));
                }
                else if (((HiddenField)r.FindControl("hfgvCreditID")).Value.Trim() != ""
                    && ((TextBox)r.FindControl("txtgvCreditDate")).Visible == true
                    && ((TextBox)r.FindControl("txtgvCreditDate")).Text.Trim() != ""
                    && ((TextBox)r.FindControl("txtgvCreditValue")).Text.Trim() != ""){
                        if (!((TextBox)r.FindControl("txtgvCreditValue")).Text.Trim().StartsWith("-")) ((TextBox)r.FindControl("txtgvCreditValue")).Text = "-" + ((TextBox)r.FindControl("txtgvCreditValue")).Text.Trim();
                    savelist.Add(new ListItem("update", ((HiddenField)r.FindControl("hfgvCreditID")).Value.Trim()
                        + "|" + CustID + "|" + ((TextBox)r.FindControl("txtgvCreditValue")).Text.Trim() + "|" +
                        ((TextBox)r.FindControl("txtgvCreditDate")).Text.Trim()));
                }
                else { /* Do nothing */}
            }
            bool status = false;
            if (Request.QueryString["save"] != null)
            {
                status = oSal.AddUpdatePaymentOnAccount(savelist, Session["employeeid"].ToString());
            }
            else
            {
                status = oSal.AddUpdatePaymentOnAccount(savelist, Session["fullname"].ToString());
            }
            if (savelist.Count > 0 && status){            
                //this.popScreen();
                lbldummy.Text = "<script language='javascript'>window.close();if (window.opener && !window.opener.closed) {window.opener.form1.btnSubmit.click();} </script>";
            }
        }
        catch (Exception ex)
        {
            Alert("Error: please check whether you have entered the correct data and try again.");
            Response.Write("<!--" + ex.Message + "-->");
        }
    }
    protected void gvPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)((Control)((GridViewCommandEventArgs)e).CommandSource).Parent.Parent;
        if (e.CommandName == "editme"){
            ((Label)row.FindControl("lblgvCreditDate")).Visible = false;
            ((Label)row.FindControl("lblgvCreditValue")).Visible = false;
            ((TextBox)row.FindControl("txtgvCreditDate")).Visible = true;
            ((ImageButton)row.FindControl("imgCalendar")).Visible = true;
            ((TextBox)row.FindControl("txtgvCreditValue")).Visible = true;
            ((LinkButton)row.FindControl("lnkbtngvEdit")).Text = "Cancel";
            ((LinkButton)row.FindControl("lnkbtngvEdit")).CommandName = "cancelme";
        }
        else if (e.CommandName == "cancelme"){
            ((Label)row.FindControl("lblgvCreditDate")).Visible = true;
            ((Label)row.FindControl("lblgvCreditValue")).Visible = true;
            ((TextBox)row.FindControl("txtgvCreditDate")).Visible = false;
            ((ImageButton)row.FindControl("imgCalendar")).Visible = false;
            ((TextBox)row.FindControl("txtgvCreditValue")).Visible = false;
            ((LinkButton)row.FindControl("lnkbtngvEdit")).Text = "Edit";
            ((LinkButton)row.FindControl("lnkbtngvEdit")).CommandName = "editme";
        }
        else if (e.CommandName == "deleteme"){
            bool status = false;
            if (((HiddenField)row.FindControl("hfgvCreditID")).Value.Trim() != ""){
                if (Request.QueryString["save"] != null)
                    status = oSal.DeletePaymentOnAccount(((HiddenField)row.FindControl("hfgvCreditID")).Value.Trim(), Session["employeeid"].ToString());                
                else
                    status = oSal.DeletePaymentOnAccount(((HiddenField)row.FindControl("hfgvCreditID")).Value.Trim(), Session["fullname"].ToString());
            }
            else status = true;
            if (status){
                int curindex = 0, rindx = int.Parse(((Label)row.FindControl("lblSlno")).Text.Trim()) - 1;
                DataSet dstemp = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("creditid");
                dt.Columns.Add("credited_date");
                dt.Columns.Add("credited_value");
                dstemp.Tables.Add(dt);
                foreach (GridViewRow r in gvPayment.Rows){
                    if (curindex != rindx){
                        DataRow ro = dstemp.Tables[0].NewRow();
                        ro["creditid"] = ((HiddenField)r.FindControl("hfgvCreditID")).Value.Trim();
                        ro["credited_date"] = ((TextBox)r.FindControl("txtgvCreditDate")).Text.Trim();
                        ro["credited_value"] = ((TextBox)r.FindControl("txtgvCreditValue")).Text.Trim();
                        dstemp.Tables[0].Rows.Add(ro);
                    }
                    curindex++;
                }
                gvPayment.DataSource = dstemp;
                gvPayment.DataBind();
            }
        }
        else { /*do nothing*/ }
    }
    protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}

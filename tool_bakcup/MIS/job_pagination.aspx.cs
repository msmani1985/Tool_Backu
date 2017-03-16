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
using System.Drawing;

/* Creatin Date: Monday, March 15, 2010
 * Created by:  Royson 
 */
public partial class job_pagination : System.Web.UI.Page
{
    protected int id = 1;
    private Pagination oPgntn = new Pagination();
    private static DataSet dsNumSytm = new DataSet();
    protected void Page_Load(object sender, EventArgs e){
        if (Session["employeeid"] == null){
            throw new Exception("Session Expired!");
        }
        if (!IsPostBack) this.popScreen();        
    }
    private void popScreen(){        
        divPagination.Visible = false;
        lblIssue.Text = "";
        hfJourID.Value = "";
        hfJobIDs.Value = "";
        txtRowIndex.Attributes["readonly"] = "readonly";
        txtJobNumber.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) ||(event.keyCode == 13)){document.getElementById('" + btnSearch.ClientID + "').click();return false;}} else {return true}; ");
        dsNumSytm = oPgntn.getNumberSytems();
        if (Request.QueryString["jno"] != null && Request.QueryString["jno"].ToString().Trim() != ""){
            txtJobNumber.Text = Request.QueryString["jno"].ToString().Trim();
            btnPaginate_Click(null, null);
        }
    }
    public void Alert(string sMessage){
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected DataSet getStyleTypes() { if (dsNumSytm == null) dsNumSytm = oPgntn.getNumberSytems(); return dsNumSytm; }
    protected DataSet getdoctype() { DataSet docds = new DataSet(); docds = oPgntn.get_jobpagination_doctype("spget_document_type", null, CommandType.StoredProcedure); return docds; }
    protected DataSet getdocitemtype(string doctypeid) { DataSet docds = new DataSet(); docds = oPgntn.get_jobpagination_doctype("spGetDocItemTypes", new string[,] { { "@document_type_id", doctypeid } }, CommandType.StoredProcedure); return docds; }
    protected void btnSearch_Click(object sender, EventArgs e){
        txtRowIndex.Text = "";
        if (txtJobNumber.Text.Trim() != ""){
            this.popPaginationGrid();
        }
        else {            
            lblIssue.Text = "";
            hfJourID.Value = "";
            hfJobIDs.Value = "";
            divPagination.Visible = false;
            Alert("Enter a Job Number!");
        }
    }
    private void popPaginationGrid(){
        hfJobIDs.Value = "";
        txtRowIndex.Text = "";
        DataSet dsPagi = oPgntn.getJobPagination(txtJobNumber.Text.Trim());
        if (dsPagi.Tables[0].Rows.Count > 0)
        {
            divPagination.Visible = true;
            lblIssue.Text = "Issue: " + dsPagi.Tables[0].Rows[0]["journal_code"].ToString() + " " + dsPagi.Tables[0].Rows[0]["issueno"].ToString();
            hfJourID.Value = dsPagi.Tables[0].Rows[0]["journal_id"].ToString();
            gvPagination.DataSource = dsPagi.Tables[1];
            gvPagination.DataBind();
        }
        else
        {
            lblIssue.Text = "";
            hfJourID.Value = "";
            divPagination.Visible = false;
            Alert("Job number does not exists!");
        }
    }
    protected void gvPagination_RowDataBound(object sender, GridViewRowEventArgs e){
        if (e.Row.RowType == DataControlRowType.DataRow){
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] = "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] = "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] = "javascript:document.form1." + txtRowIndex.ClientID + ".value=" + (e.Row.RowIndex + 1);
            //    ((DropDownList)e.Row.FindControl("drpgvStyleType")).Items.Insert(0, new ListItem(" --select-- ", "0"));
            HiddenField hfdoctype = ((HiddenField)e.Row.FindControl("hfgvDocTypeID"));
            switch (hfdoctype.Value.Trim()){
                case "1": //front matter
                    e.Row.BackColor = Color.LightGreen;
                    break;
                case "2": //back matter
                    e.Row.BackColor = Color.LightGreen;
                    break;
                case "16": //Cover
                    e.Row.BackColor = Color.Wheat;
                    break;
                case "6": //blank
                    e.Row.BackColor = Color.LightYellow;
                    break;
                case "15": //manuscript
                    e.Row.BackColor = Color.SkyBlue;
                    break;
                case "17": //prelims
                    e.Row.BackColor = Color.LightGreen;
                    break;
                default:
                    break;
            }
            DropDownList dddoc = (DropDownList)e.Row.Cells[2].FindControl("dd_doctype");
            dddoc.DataTextField = "document_name";
            dddoc.DataValueField = "document_type_id";
            if (DataBinder.Eval(e.Row.DataItem, "document_type_id").ToString() != "")
            {
                dddoc.SelectedValue = DataBinder.Eval(e.Row.DataItem, "document_type_id").ToString();
            }            
            dddoc.DataSource = getdoctype();
            dddoc.DataBind();
            DropDownList ddsub = (DropDownList)e.Row.Cells[3].FindControl("dd_subtype");
            ddsub.DataSource = getdocitemtype(((DropDownList)e.Row.Cells[2].FindControl("dd_doctype")).SelectedValue.ToString());
            ddsub.DataBind();
            ddsub.SelectedValue = DataBinder.Eval(e.Row.DataItem, "document_item_type_id").ToString();
        }
    }
    protected void doctype_OnSelectIndexChanged(object sender, EventArgs e)
    {
        DropDownList dd_dtype = sender as DropDownList;
        GridViewRow docrow = (GridViewRow)dd_dtype.NamingContainer;
        DropDownList dd_docitem = (DropDownList)docrow.FindControl("dd_subtype");
        dd_docitem.DataSource = getdocitemtype(dd_dtype.SelectedValue.ToString());
        dd_docitem.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        hfJobIDs.Value = "";
        if (hfJourID.Value.Trim() != ""){
            Page.RegisterStartupScript("Open", "<script language = 'javascript'>window.open('job_unassigned.aspx?jid=" + hfJourID.Value.Trim() + "','Preview','width=800,height=600,left=100,top=50,toolbars=no,scrollbars=yes,status=yes,resizable=yes');</script>");
        }
        else Alert("Please enter a valid Job Number!");
    }
    protected void lnkSaveArticles_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtJobNumber.Text.Trim() != "" && hfJobIDs.Value.Trim() != ""){
                if (oPgntn.InsertPaginationJobs(txtJobNumber.Text.Trim(), hfJobIDs.Value.Trim(), "", Session["employeeid"].ToString(), "", "") != "true")
                    Alert("Error assigning articles!");
                else this.popPaginationGrid();
            }
            else Alert("Error assigning articles, please check whether you has entered a valid Job number \r\nand selected atleast one unassigned article!");
        }
        catch (Exception ee)
        {
            throw new Exception(ee.Message);
        }
    }
    protected void imgbtngvDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtJobNumber.Text != ""){
                string sJobIDs = "";
                foreach (GridViewRow row in gvPagination.Rows){
                    if (((CheckBox)row.FindControl("chkgvSelect")).Checked)
                        sJobIDs += ((HiddenField)row.FindControl("hfgvJobID")).Value.Trim() + ",";
                }
                sJobIDs = sJobIDs.TrimEnd(',');
                if (sJobIDs == "")
                    Alert("Error: Select atleast one articles for deletion!");
                else if (oPgntn.DeleteJobPagination(txtJobNumber.Text, sJobIDs) != "true")
                    Alert("Error deleting articles!");
                else this.popPaginationGrid();
            }
            else Alert("Error: Enter a valid Job Number!");
        }
        catch (Exception ec)
        {
            throw new Exception(ec.Message);
        }
    }
    protected void btnPaginate_Click(object sender, EventArgs e)
    {        
        ArrayList listPag = new ArrayList();
        string[] aPagination = null;
        int pgfrm = 0, pgto = 0, pgtot = 0, foundflg = 0, dummy = 0;
        foreach (GridViewRow row in gvPagination.Rows){
            aPagination  = new string[8];
            aPagination[0] = ((HiddenField)row.FindControl("hfgvJobID")).Value.Trim();
            //pgtot = int.Parse(((Label)row.FindControl("lblgvTotal")).Text.Trim());
            pgtot = int.Parse(((TextBox)row.FindControl("txt_gvtotal")).Text.Trim());
            if (foundflg == 0){
                if (((TextBox)row.FindControl("txtgvFrom")).Text.Trim() != ""
                    && ((TextBox)row.FindControl("txtgvFrom")).Text.Trim() != "0"
                    && int.TryParse(((TextBox)row.FindControl("txtgvFrom")).Text.Trim(), out dummy)){
                    foundflg = 1;
                    pgfrm = int.Parse(((TextBox)row.FindControl("txtgvFrom")).Text.Trim());
                    pgto = (pgfrm + (pgtot - 1));
                    aPagination[1] = pgfrm.ToString();
                    aPagination[2] = pgto.ToString();
                }
                else{
                    //if (((TextBox)row.FindControl("txtgvFrom")).Text.Trim() == "") aPagination[1] = "0";
                    //else 
                        aPagination[1] = ((TextBox)row.FindControl("txtgvFrom")).Text.Trim();
                    //if (((TextBox)row.FindControl("txtgvTo")).Text.Trim() == "") aPagination[2] = "0";
                    //else 
                        aPagination[2] = ((TextBox)row.FindControl("txtgvTo")).Text.Trim();
                }
            }
            else{
                pgfrm = pgto + 1;
                pgto = pgfrm + (pgtot - 1);
                aPagination[1] = pgfrm.ToString();
                aPagination[2] = pgto.ToString();
            }
            if (((DropDownList)row.FindControl("drpgvStyleType")).SelectedItem.Value.Trim() == "") aPagination[3] = "1";
            else aPagination[3] = ((DropDownList)row.FindControl("drpgvStyleType")).SelectedItem.Value.Trim();
            if (((TextBox)row.FindControl("txtgvOrderIndex")).Text.Trim() == "") aPagination[4] = "0";
            else aPagination[4] = ((TextBox)row.FindControl("txtgvOrderIndex")).Text.Trim();
            aPagination[5] = pgtot.ToString();
            aPagination[6] = ((DropDownList)row.FindControl("dd_doctype")).SelectedItem.Value.Trim();
            aPagination[7] = ((DropDownList)row.FindControl("dd_subtype")).SelectedItem.Value.Trim();
            listPag.Add(aPagination);
        }
        if (oPgntn.UpdatePaginationJobs(listPag, txtJobNumber.Text.Trim()) == "true"){
            this.popPaginationGrid();
        }
        else Alert("Pagination Failed!");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ArrayList listPag = new ArrayList();
        string[] aPagination = null;
        foreach (GridViewRow row in gvPagination.Rows){
            aPagination = new string[8];
            aPagination[0] = ((HiddenField)row.FindControl("hfgvJobID")).Value.Trim();
            aPagination[1] = ((TextBox)row.FindControl("txtgvFrom")).Text.Trim();
            aPagination[2] = ((TextBox)row.FindControl("txtgvTo")).Text.Trim(); 
            if (((DropDownList)row.FindControl("drpgvStyleType")).SelectedItem.Value.Trim() == "") aPagination[3] = "1";
            else aPagination[3] = ((DropDownList)row.FindControl("drpgvStyleType")).SelectedItem.Value.Trim();
            if (((TextBox)row.FindControl("txtgvOrderIndex")).Text.Trim() == "") aPagination[4] = "0";
            else aPagination[4] = ((TextBox)row.FindControl("txtgvOrderIndex")).Text.Trim();
            string txtTotal = ((TextBox)row.FindControl("txt_gvtotal")).Text.Trim();
            if (Convert.ToString(txtTotal) == "")
            {
                aPagination[5] = "0";
            }
            else
            {
                aPagination[5] = ((TextBox)row.FindControl("txt_gvtotal")).Text.Trim();
            }            
            aPagination[6] = ((DropDownList)row.FindControl("dd_doctype")).SelectedValue.ToString();
            aPagination[7] = ((DropDownList)row.FindControl("dd_subtype")).SelectedValue.ToString();
            listPag.Add(aPagination);
        }
        if (oPgntn.UpdatePaginationJobs(listPag, txtJobNumber.Text.Trim()) == "true"){
            this.popPaginationGrid();
        }
        else Alert("Save Failed!");
    }

    protected void lnkbtnInsertNonArticle_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtJobNumber.Text.Trim() != "" && txtRowIndex.Text != "")
            {
                int iindex = int.Parse(txtRowIndex.Text);
                GridViewRow ro = gvPagination.Rows[iindex - 1];
                int seq = int.Parse(((TextBox)ro.FindControl("txtgvOrderIndex")).Text.Trim());
                if (drpInsert.SelectedIndex == 0){
                    //if (iindex != 1) iindex = iindex - 1;
                           if (seq > 0) seq--;
                }
                else{
                    //iindex = iindex + 1;
                    seq++;
                }
                if (txtBlankCount.Text.Trim() != "" &&
                    int.Parse(txtBlankCount.Text.Trim()) > 0){
                    if (oPgntn.InsertPaginationJobs(txtJobNumber.Text.Trim(), "", "non-article", Session["employeeid"].ToString(), seq.ToString(), "", "Blank", txtBlankCount.Text.Trim()) != "true")
                        Alert("Error inserting blank article!");
                }
                if (txtPrelimCount.Text.Trim() != "" &&
                    int.Parse(txtPrelimCount.Text.Trim()) > 0){
                    if (oPgntn.InsertPaginationJobs(txtJobNumber.Text.Trim(), "", "non-article", Session["employeeid"].ToString(), seq.ToString(), "", "Prelim", txtPrelimCount.Text.Trim()) != "true")
                        Alert("Error inserting prelim article!");
                }
                if (txtFrontCovCount.Text.Trim() != "" &&
                    int.Parse(txtFrontCovCount.Text.Trim()) > 0){
                    if (oPgntn.InsertPaginationJobs(txtJobNumber.Text.Trim(), "", "non-article", Session["employeeid"].ToString(), seq.ToString(), "", "Front-Cover", txtFrontCovCount.Text.Trim()) != "true")
                        Alert("Error inserting front cover article!");
                }
                if (txtBackCovCount.Text.Trim() != "" &&
                    int.Parse(txtBackCovCount.Text.Trim()) > 0){
                    if (oPgntn.InsertPaginationJobs(txtJobNumber.Text.Trim(), "", "non-article", Session["employeeid"].ToString(), seq.ToString(), "", "Back-Cover", txtBackCovCount.Text.Trim()) != "true")
                        Alert("Error inserting back cover article!");
                }
                this.popPaginationGrid();
            }
            else Alert("Error: please check whether you have entered a valid Job number!");
        }
        catch (Exception ex){
            Alert(ex.Message);
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        if (txtJobNumber.Text.Trim() != ""){
            Page.RegisterStartupScript("Open", "<script language = 'javascript'>window.open('job_pagination_preview.aspx?jno=" + txtJobNumber.Text.Trim() + "','Preview','width=1000,height=650,left=10,top=10,toolbars=no,scrollbars=yes,status=yes,resizable=yes');</script>");
        }
        else Alert("Please enter a valid Job Number!");
    }
}

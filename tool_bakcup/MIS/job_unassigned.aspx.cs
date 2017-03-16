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

/* Creatin Date: Monday, March 15, 2010
 * Created by:  Royson 
 */
public partial class job_unassigned : System.Web.UI.Page
{
    protected int id = 1;
    private Pagination oPgntn = new Pagination();
    protected void Page_Load(object sender, EventArgs e){
        if (!IsPostBack) this.popScreen();
    }
    private void popScreen(){
        if (Request.QueryString["jid"] != null &&
            Request.QueryString["jid"].ToString() != ""){
            hfDuplicateJobID.Value = "";
            btnArticleDuplicate.Visible = false;
            btnBack.Visible = false;
            btnArticleAssigned.Visible = true;
            btnAssign.Visible = true;
            btnAssign1.Visible = true;
            string sJourID = Request.QueryString["jid"].ToString();
            DataSet dsArt = oPgntn.getJobsUnassigned(sJourID);
            gvArticlesList.Columns[5].Visible = true;
            gvArticlesList.DataSource = dsArt.Tables[0];
            gvArticlesList.DataBind();
        }
    }
    protected void btnAssign_Click(object sender, EventArgs e){
        string sJobIDs="";
        foreach (GridViewRow row in gvArticlesList.Rows){
            if (((CheckBox)row.FindControl("chkgvSelect")).Checked)
                sJobIDs += ((Label)row.FindControl("lblgvJobID")).Text + ",";
        }
        sJobIDs = sJobIDs.TrimEnd(',');
        if (sJobIDs != "")
            Page.RegisterStartupScript("onload", "<script language = 'javascript'>window.opener.document.form1.hfJobIDs.value='" + sJobIDs + "';window.opener.__doPostBack('lnkSaveArticles','');self.close();</script>");
        else Alert("Select atleast one article to assign!");
    }
    public void Alert(string sMessage){
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void btnArticleAssigned_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["jid"] != null &&
            Request.QueryString["jid"].ToString() != ""){
            hfDuplicateJobID.Value = "";
            btnArticleDuplicate.Visible = true;
            btnBack.Visible = true;
            btnArticleAssigned.Visible = false;
            btnAssign.Visible = false;
            btnAssign1.Visible = false;
            string sJourID = Request.QueryString["jid"].ToString();
            DataSet dsArt = oPgntn.getJobsAssigned(sJourID);
            gvArticlesList.Columns[5].Visible = false;
            gvArticlesList.DataSource = dsArt.Tables[0];
            gvArticlesList.DataBind();
        }
    }
    protected void btnArticleDuplicate_Click(object sender, EventArgs e)
    {
        //Alert("success!");
        if (oPgntn.InsertPaginationJobs(Request.QueryString["jid"].ToString().Trim(), "", "duplicate", Session["employeeid"].ToString(), "", hfDuplicateJobID.Value.Trim()) != "true")
            Alert("Error duplicating article!");
        else this.popScreen();        
    }
    protected void gvArticlesList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnBack.Visible && e.Row.RowType == DataControlRowType.DataRow){
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouserover"] = "javascript:" + e.Row.ClientID + ".style.textDecoration='underline'";
            e.Row.Attributes["onmouseout"] = "javascript:" + e.Row.ClientID + ".style.textDecoration='none'";
            e.Row.Attributes["onclick"] = "javascript:if(confirm('Do you wish to duplicate the article " + ((Label)e.Row.FindControl("lblgvJobName")).Text + "?')){document.form1." + hfDuplicateJobID.ClientID + ".value=" + ((Label)e.Row.FindControl("lblgvJobID")).Text.Trim() + ";document.form1." + btnArticleDuplicate.ClientID + ".click()}else{document.form1." + hfDuplicateJobID.ClientID + ".value='';}";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.popScreen();
    }
}

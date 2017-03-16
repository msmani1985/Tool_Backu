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
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

public partial class IssueInvoiceCorrection : System.Web.UI.Page
{
    string sSortExp = "";
    string sSortDir = "desc";
    static string strANO = "";
    static string strINO = "";
    static string strANO_ARTICLE = "";
    static string strJOURCODE = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            ViewState["sortOrder"] = "desc";
            sSortExp = "";
            sSortDir = "desc";
            DataSet oDS = new DataSet();
            if (Request.QueryString["title"] != null)
                invtitle.InnerHtml = Request.QueryString["title"].ToString();

            if (Session["CustomerName"] != null)
            {
                hfEmID.Value = Session["employeeteamid"].ToString().Trim();
                oDS = (DataSet)(Session["CustomerName"]);
                ddlcustomer.DataSource = oDS;
                string myvalue = "10066";
                ddlcustomer.SelectedValue = myvalue.ToString();
                ddlcustomer.DataBind();
            }
            //else
            //{

            //    string sHTML = "";
            //    Page page = HttpContext.Current.Handler as Page;

            //    sHTML += "<script language='javascript'>";
            //    sHTML += "window.open('Login.aspx','_top')";
            //    sHTML += "</script>";

            //    ClientScript.RegisterStartupScript(this.GetType(), "Script", sHTML);
            //}
            // btn_SavePrint.Visible = false;
            btn_SaveArticle.Visible = false;
            btn_SaveIssue.Visible = false;
            oDS = null;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        Invoiced_IBSQL bib = new Invoiced_IBSQL();

        Int64 iINO = 0;
        Datasource_IBSQL DSIB = new Datasource_IBSQL();
        gvIssueCorr.Visible = false;
        gvIssues.Visible = false;
        if (ddljobtype.SelectedItem.Value == "1")
        {
            string[] array = txtJobNumber.Text.Split(' ');

            DataSet ds_ISSUE = new DataSet();
            if (array.Length.ToString() == "3")
            {
                ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + ' ' + txtJobNumber.Text.ToString().Split(' ')[2] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");
            }
            else
                ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");

            if (ds_ISSUE != null && ds_ISSUE.Tables[0].Rows.Count > 0)
            {
                strJOURCODE = ds_ISSUE.Tables["ISSUE"].Rows[0]["JOURCODE"].ToString();
                strINO = ds_ISSUE.Tables["ISSUE"].Rows[0]["INO"].ToString();

                gvIssues.DataSource = ds_ISSUE;
                gvIssues.DataBind();
                gvIssues.Visible = true;

                DataSet ds_ARTICLE = new DataSet();
                ds_ARTICLE = DSIB.ExcuteProc("select isnull(adno,3) adno,* from article_dp  where ino = " + strINO + "order by AARTICLECODE ", "ARTICLE");
                if (ds_ARTICLE != null && ds_ARTICLE.Tables[0].Rows.Count > 0)
                {
                    //strANO_ARTICLE = ds_ARTICLE.Tables["ARTICLE"].Rows[0]["ANO"].ToString();

                    Session["ARTICLE"] = ds_ARTICLE;
                    gvIssueCorr.DataSource = ds_ARTICLE;
                    gvIssueCorr.DataBind();
                    gvIssueCorr.Visible = true;
                    div_Error.Visible = false;
                }

                btn_SaveIssue.Visible = true;
                btn_SaveArticle.Visible = true;
            }
            else
                div_Error.Visible = true;
            div_Error.InnerHtml = "No Records Found";
        }

        else if (ddljobtype.SelectedItem.Value == "2")
        {
            //ds = DSIB.ExcuteProc("", "");
        }

        else if (ddljobtype.SelectedItem.Value == "3")
        {

            //ds = DSIB.ExcuteProc("", "");

        }
        else
        {
            DataSet ds_ARTICLE_WIP = new DataSet();
            long iInte = 0;
            if (Int64.TryParse(txtJobNumber.Text.Trim(), out iInte))

                ds_ARTICLE_WIP = DSIB.ExcuteProc("select * from article_dp where invno = '" + txtJobNumber.Text.ToString().Replace(" ", "").TrimEnd() + "'" + "order by AARTICLECODE ", "ARTICLE");
            else
                ds_ARTICLE_WIP = DSIB.ExcuteProc("select * from article_dp where aarticlecode = '" + txtJobNumber.Text.ToString().Replace(" ", "").TrimEnd() + "'" + "order by AARTICLECODE ", "ARTICLE");
            if (ds_ARTICLE_WIP != null && ds_ARTICLE_WIP.Tables[0].Rows.Count > 0)
            {
                Session["ARTICLE"] = ds_ARTICLE_WIP;
                //strANO = ds_ARTICLE_WIP.Tables["ARTICLE"].Rows[0]["ANO"].ToString();

                gvIssueCorr.DataSource = ds_ARTICLE_WIP;
                gvIssueCorr.DataBind();
                gvIssueCorr.Visible = true;
                btn_SaveArticle.Visible = true;
                div_Error.Visible = false;
            }
            else
                div_Error.Visible = true;
            div_Error.InnerHtml = "No Records Found";

            //ds = DSIB.ExcuteProc("select * from article_dp where aarticlecode = '" + txtJobNumber.Text.ToString() + "'", "");
        }


    }

    protected void btn_Save_Article(object sender, ImageClickEventArgs e)
    {
        string strCE_PAGES = string.Empty;
        string strSAM_PAGES = string.Empty;
        string strWIP_AREALNOOFPAGES = string.Empty;
        string strAREALNOOFPAGES = string.Empty;
        string strCROSSREF = string.Empty;
        string strEPUB = string.Empty;
        string strHIGHLEVEL_COPYEDIT = string.Empty;
        string strEXTRA_COPYEDIT = string.Empty;
        string strADNO = string.Empty;
        string strPreedit = string.Empty;

        biz_IB bib = new biz_IB();
        DataSet ds = new DataSet();
        Datasource_IBSQL DSIB = new Datasource_IBSQL();
        if (gvIssueCorr.Visible == true)
        {
            btn_SaveArticle.Visible = true;

            foreach (GridViewRow row in gvIssueCorr.Rows)
            {
                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtCE_PAGES")).Text))
                    strCE_PAGES = ((TextBox)row.FindControl("txtCE_PAGES")).Text;

                if (Convert.ToString(strCE_PAGES).Trim() == "")
                {
                    strCE_PAGES = "0";
                }

                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtSAM_PAGES")).Text))
                    strSAM_PAGES = ((TextBox)row.FindControl("txtSAM_PAGES")).Text;

                if (Convert.ToString(strSAM_PAGES).Trim() == "")
                {
                    strSAM_PAGES = "0";
                }

                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtWIP_AREALNOOFPAGES")).Text))
                    strWIP_AREALNOOFPAGES = ((TextBox)row.FindControl("txtWIP_AREALNOOFPAGES")).Text;

                if (Convert.ToString(strWIP_AREALNOOFPAGES).Trim() == "")
                {
                    strWIP_AREALNOOFPAGES = "0";
                }

                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtAREALNOOFPAGES")).Text))
                    strAREALNOOFPAGES = ((TextBox)row.FindControl("txtAREALNOOFPAGES")).Text;

                if (Convert.ToString(strAREALNOOFPAGES).Trim() == "")
                {
                    strAREALNOOFPAGES = "0";
                }

                strEXTRA_COPYEDIT = ((DropDownList)row.FindControl("ddl_ExtraCopyEdit")).SelectedItem.Text.ToString();
                strCROSSREF = ((DropDownList)row.FindControl("ddl_crossref")).SelectedItem.Text.ToString();
                strEPUB = ((DropDownList)row.FindControl("ddl_epub")).SelectedItem.Text.ToString();
                strHIGHLEVEL_COPYEDIT = ((DropDownList)row.FindControl("ddl_highlevel_copyedit")).SelectedItem.Text.ToString();
                strADNO = ((DropDownList)row.FindControl("ddl_ADNO")).SelectedItem.Value.ToString();
                strPreedit = ((DropDownList)row.FindControl("ddl_Preedit")).SelectedItem.Value.ToString();

                strANO = row.Cells[1].Text.Trim();


                DSIB.ExcuteProc("update article_dp set  IsPreEdit='" + strPreedit + "',ADNO='" + strADNO + "', AEXTRA_COPY_EDIT=  '" + strEXTRA_COPYEDIT + "',  INV_CROSSREF='" + strCROSSREF + "', INV_EPUB='" + strEPUB + "', INV_HIGHLEVEL_COPYEDIT='" + strHIGHLEVEL_COPYEDIT + "', CE_PAGES= " + strCE_PAGES + ", SAM_PAGES=" + strSAM_PAGES + ", WIP_AREALNOOFPAGES=" + strWIP_AREALNOOFPAGES + ", AREALNOOFPAGES=" + strAREALNOOFPAGES + " where ANO=" + strANO);

            }

            btnSubmit_Click(null, null);
            Alert("Successfully Saved");

        }


    }

    protected void btn_Save_issue(object sender, ImageClickEventArgs e)
    {
        string strCE_SECTIONDISPLAY = string.Empty;
        string strSAM_SECTIONDISPLAY = string.Empty;
        string strCE_JOURNAL = string.Empty;
        string strSAM_JOURNAL = string.Empty;
        string strIsArticleBased = string.Empty;
        string strINV_SAM = string.Empty;
        string strIS_FPM = string.Empty;

        Invoiced_IBSQL bib = new Invoiced_IBSQL();
        DataSet ds = new DataSet();
        Datasource_IBSQL DSIB = new Datasource_IBSQL();
        if (gvIssues.Visible == true)
        {
            btn_SaveIssue.Visible = true;
            foreach (GridViewRow row in gvIssues.Rows)
            {
                strCE_SECTIONDISPLAY = ((DropDownList)row.FindControl("ddl_CEDISPLAY")).SelectedItem.Text.ToString();
                strSAM_SECTIONDISPLAY = ((DropDownList)row.FindControl("ddl_SAMDISPLAY")).SelectedItem.Text.ToString();
                strCE_JOURNAL = ((DropDownList)row.FindControl("ddl_COPYEDIT")).SelectedItem.Value.ToString();
                strSAM_JOURNAL = ((DropDownList)row.FindControl("ddl_SAM")).SelectedItem.Value.ToString();
                strIsArticleBased = ((DropDownList)row.FindControl("ddl_ArticleBased")).SelectedItem.Value.ToString();
                strINV_SAM = ((DropDownList)row.FindControl("ddl_epubSplit")).SelectedItem.Value.ToString();
                strIS_FPM = ((DropDownList)row.FindControl("ddl_FPM")).SelectedItem.Value.ToString();


                strANO = row.Cells[1].Text.Trim();
                strJOURCODE = row.Cells[3].Text.Trim();

                DSIB.ExcuteProc("update issue_dp set ce_sectiondisplay='" + strCE_SECTIONDISPLAY + "',  sam_sectiondisplay='" + strSAM_SECTIONDISPLAY + "',  INV_SAM ='" + strINV_SAM + "' where INO=" + strINO);
                //journal update
                //DSIB.ExcuteProc("update journal_dp set issam= " + strSAM_JOURNAL + ",  iscopyedit=" + strCE_JOURNAL + " where jourcode=" + strJOURCODE);
                DSIB.ExcuteProc("update journal_dp set issam= " + strSAM_JOURNAL + ",  iscopyedit=" + strCE_JOURNAL + ", isarticle_based=" + strIsArticleBased + ", isfpm=" + strIS_FPM + " where  jourcode= ( '" + strJOURCODE.ToString() + "')");

            }
            btnSubmit_Click(null, null);
            Alert("Successfully Saved");
        }

        //btnSubmit_Click(null, null);

    }

    protected void ddl_REMOVE_SelectedIndexChanged(object sender, EventArgs e)
    {
        Datasource_IBSQL DSIB = new Datasource_IBSQL();

        DropDownList ddl_Remove = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl_Remove.NamingContainer;
        DropDownList oList_Remove = (DropDownList)row.FindControl("ddl_REMOVE");

        if (oList_Remove.SelectedValue == "Y")
        {
            if (ddljobtype.SelectedItem.Value == "4")
            {
                if (oList_Remove.SelectedValue == "Y")
                {
                    DSIB.ExcuteProc("update article_dp set INVNO = null, AINVOICEDDATE = NULL where ANO=" + row.Cells[1].Text.Trim());
                    oList_Remove.Enabled = false;
                }
            }
            if (ddljobtype.SelectedItem.Value == "1")
            {
                if (oList_Remove.SelectedValue == "Y")
                {
                    DSIB.ExcuteProc("update article_dp set INO = null where ANO=" + row.Cells[1].Text.Trim());
                }

            }
            btnSubmit_Click(null, null);
            Alert("Successfully Saved");
        }
    }


    protected void gvIssueCorr_DataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList oList;
        DropDownList ddl_Remove;
        Datasource_IBSQL DSIB = new Datasource_IBSQL();
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                oList = (DropDownList)e.Row.Cells[6].FindControl("ddl_ExtraCopyEdit");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "AEXTRA_COPY_EDIT").ToString();
                oList = (DropDownList)e.Row.Cells[7].FindControl("ddl_crossref");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "INV_CROSSREF").ToString();
                oList = (DropDownList)e.Row.Cells[8].FindControl("ddl_epub");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "INV_EPUB").ToString();
                oList = (DropDownList)e.Row.Cells[9].FindControl("ddl_highlevel_copyedit");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "INV_HIGHLEVEL_COPYEDIT").ToString();
                ddl_Remove = (DropDownList)e.Row.Cells[14].FindControl("ddl_REMOVE");
                oList = (DropDownList)e.Row.Cells[15].FindControl("ddl_preedit");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ISPREEDIT").ToString();

                //if (ddljobtype.SelectedItem.Value == "4")
                //{
                //    if (DataBinder.Eval(e.Row.DataItem, "INVNO").ToString() == "")
                //    {
                //        ddl_Remove.Enabled = false;
                //    }
                //    else
                //        ddl_Remove.Enabled = true;
                //}

                DataSet ds_ADNO = new DataSet();
                ds_ADNO = DSIB.ExcuteProc("select * from articledoctype_dp", "ARTICLE_DOC_TYPE");
                oList = (DropDownList)e.Row.Cells[3].FindControl("ddl_ADNO");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ADNO").ToString();
                oList.DataTextField = "ADDESCRIPTION";
                oList.DataValueField = "ADNO";
                oList.DataSource = ds_ADNO;
                oList.DataBind();
            }

        }
        catch { }

    }

    protected void gvIssues_DataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList oList;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                oList = (DropDownList)e.Row.Cells[6].FindControl("ddl_COPYEDIT");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ISCOPYEDIT").ToString();
                oList = (DropDownList)e.Row.Cells[7].FindControl("ddl_SAM");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ISSAM").ToString();
                oList = (DropDownList)e.Row.Cells[8].FindControl("ddl_CEDISPLAY");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "CE_SECTIONDISPLAY").ToString();
                oList = (DropDownList)e.Row.Cells[9].FindControl("ddl_SAMDISPLAY");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "SAM_SECTIONDISPLAY").ToString();
                oList = (DropDownList)e.Row.Cells[10].FindControl("ddl_epubSplit");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "INV_SAM").ToString();
                oList = (DropDownList)e.Row.Cells[10].FindControl("ddl_ArticleBased");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ISARTICLE_BASED").ToString();
                oList = (DropDownList)e.Row.Cells[10].FindControl("ddl_FPM");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ISFPM").ToString();

            }
        }
        catch { }

    }

    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }

    protected void ddljobtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        sSortExp = "";
    }

    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }

}
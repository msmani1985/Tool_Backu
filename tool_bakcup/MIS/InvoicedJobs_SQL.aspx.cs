using System;
using System.Collections.Generic;
//using System.Linq;
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

public partial class InvoicedJobs_SQL : System.Web.UI.Page
{
    string sSortExp = "";
    string sSortDir = "desc";
    DataSet ds = new DataSet();

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
            if (Session["CustomerDS"] != null)
            {
                hfEmID.Value = Session["employeeteamid"].ToString().Trim();
                oDS = (DataSet)(Session["CustomerDS"]);
                ddlcustomer.DataSource = oDS;
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
            oDS = null;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        biz_IB bib = new biz_IB();
        ds = new DataSet();

        divmessage.Visible = false;
        if (invtitle.InnerHtml.ToUpper() == "Final Invoice".ToUpper())
        {
            ds = bib.GetDespatchedJobs2(Convert.ToInt16(ddlcustomer.SelectedValue), Convert.ToInt16(ddljobtype.SelectedValue));
        }
        else
        {
            ds = bib.GetDespatchedJobs1(Convert.ToInt16(ddlcustomer.SelectedValue), Convert.ToInt16(ddljobtype.SelectedValue));

        }

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt16(ddljobtype.SelectedValue) == 2)///////////// 2 For Books
                DataBind(BooksInvoiceList, ds.Tables[0]);
            else if (Convert.ToInt16(ddljobtype.SelectedValue) == 3)/////////// 3 For Projects
                DataBind(ProjectInvoiceList, ds.Tables[0]);
            else if (Convert.ToInt16(ddljobtype.SelectedValue) == 1)//////////// 1 For Journal
                DataBind(adgdispatchedlist, ds.Tables[0]);
            else if (Convert.ToInt32(ddljobtype.SelectedValue) == 4)///////////////// 4 For WIP
            {
                DataView dv;
                dv = ds.Tables[0].DefaultView;
                if (invtitle.InnerHtml.ToUpper() != "Final Invoice".ToUpper())
                    dv.RowFilter = "EVNO=10054";
                DataBind(WIPInvoiceList, dv.ToTable());
            }

            Session["invDS"] = ds;
        }
        else
        {
            BooksInvoiceList.Visible = false;
            ProjectInvoiceList.Visible = false;
            adgdispatchedlist.Visible = false;
            WIPInvoiceList.Visible = false;
            divmessage.Visible = true;
            divmessage.InnerHtml = "No records found";
        }
        bib = null;
    }

    private void DataBind(GridView oGrid, DataTable oDs)
    {
        DataView oView = new DataView();
        oView = oDs.DefaultView;
        if (sSortExp != "")
            oView.Sort = string.Format("{0} {1}", sSortExp, sSortDir);
        oGrid.DataSource = oView;
        oGrid.DataBind();
        //pnlContainer.Controls.Clear();
        //pnlContainer.Controls.Add(oGrid);  
        HideShowColumns(true, oGrid);
    }

    protected void Grid_Sorting(object sender, GridViewSortEventArgs e)
    {
        sSortExp = e.SortExpression;
        sSortDir = sortOrder;
        ds = new DataSet();
        if (Session["invDS"] != null)
            ds = (DataSet)Session["invDS"];
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt16(ddljobtype.SelectedValue) == 2)///////////// 2 For Books
                DataBind(BooksInvoiceList, ds.Tables[0]);
            else if (Convert.ToInt16(ddljobtype.SelectedValue) == 3)/////////// 3 For Projects
                DataBind(ProjectInvoiceList, ds.Tables[0]);
            else if (Convert.ToInt16(ddljobtype.SelectedValue) == 1)//////////// 1 For Journal
                DataBind(adgdispatchedlist, ds.Tables[0]);
        }
        else
        {
            btnSubmit_Click(null, null);
        }

    }

    protected void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (invtitle.InnerHtml.ToUpper() == "Final Invoice".ToUpper() && Convert.ToInt16(ddljobtype.SelectedValue) == 1)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "INV_EMAIL_SENT1").ToString().ToUpper() == "Y" && DataBinder.Eval(e.Row.DataItem, "INV_EMAIL_SENT2").ToString().ToUpper() == "N")
                {
                    e.Row.Cells[12].Controls.Add((HtmlImage)e.Row.Cells[12].FindControl("Img1"));
                    ((HtmlImage)e.Row.Cells[12].FindControl("Img1")).Src = "images/temail_green.jpg";
                }
                if (DataBinder.Eval(e.Row.DataItem, "INV_EMAIL_SENT1").ToString().ToUpper() == "N" && DataBinder.Eval(e.Row.DataItem, "INV_EMAIL_SENT2").ToString().ToUpper() == "Y")
                {
                    e.Row.Cells[13].Controls.Add((HtmlImage)e.Row.Cells[13].FindControl("SecondImg1"));
                    ((HtmlImage)e.Row.Cells[13].FindControl("SecondImg1")).Src = "images/temail_green.jpg";
                }
                if (DataBinder.Eval(e.Row.DataItem, "CNO").ToString() != "2556")//Second mail for TandF only
                    ((HtmlImage)e.Row.Cells[13].FindControl("SecondImg1")).Visible = false;
                if (DataBinder.Eval(e.Row.DataItem, "CNO").ToString() == "10040") //For Psychology Press Ltd 
                    ((HtmlImage)e.Row.Cells[13].FindControl("Img1")).Visible = false;

            }
        }
        if (invtitle.InnerHtml.ToUpper() == "Final Invoice".ToUpper() && Convert.ToInt16(ddljobtype.SelectedValue) == 4)//For WIP
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "INV_EMAIL_SENT1").ToString().ToUpper() == "Y" && DataBinder.Eval(e.Row.DataItem, "INV_EMAIL_SENT2").ToString().ToUpper() == "N")
                {
                    e.Row.Cells[4].Controls.Add((HtmlImage)e.Row.Cells[4].FindControl("Img1"));
                    ((HtmlImage)e.Row.Cells[4].FindControl("Img1")).Src = "images/temail_green.jpg";
                }
                if (DataBinder.Eval(e.Row.DataItem, "CNO").ToString() == "10040") //For Psychology Press Ltd 
                    ((HtmlImage)e.Row.Cells[4].FindControl("Img1")).Visible = false;

            }
        }
        if (invtitle.InnerHtml.ToUpper() == "Final Invoice".ToUpper() && Convert.ToInt16(ddljobtype.SelectedValue) == 3)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HtmlImage himg=e.Row.Cells[8].FindControl("imgEmailPDF") as HtmlImage;
                if (DataBinder.Eval(e.Row.DataItem, "CNO").ToString() == "2556")//TandF Projects invoice send with TandF Journal so hide mail option 27 May 2010
                    ((HtmlImage)e.Row.Cells[8].FindControl("imgEmailPDF")).Visible = false;
            }
        }
        if (invtitle.InnerHtml.ToUpper() == "India Invoice".ToUpper() && Convert.ToInt16(ddljobtype.SelectedValue) == 1)
        {
            if (DataBinder.Eval(e.Row.DataItem, "IINNO") != null && DataBinder.Eval(e.Row.DataItem, "IINNO").ToString() != "")
                ((ImageButton)e.Row.Cells[11].FindControl("btnsaveprint")).Enabled = false;
        }
        if (invtitle.InnerHtml.ToUpper() == "India Invoice".ToUpper() && Convert.ToInt16(ddljobtype.SelectedValue) == 2)
        {
            if (DataBinder.Eval(e.Row.DataItem, "BINVOICENO1") != null && DataBinder.Eval(e.Row.DataItem, "BINVOICENO1").ToString() != "")
                ((ImageButton)e.Row.Cells[7].FindControl("btnsaveprint")).Enabled = false;
        }
        if (invtitle.InnerHtml.ToUpper() == "India Invoice".ToUpper() && Convert.ToInt16(ddljobtype.SelectedValue) == 3)
        {
            if (DataBinder.Eval(e.Row.DataItem, "INVNO") != null && DataBinder.Eval(e.Row.DataItem, "INVNO").ToString() != "")
                ((ImageButton)e.Row.Cells[7].FindControl("btnsaveprint")).Enabled = false;
        }
        if (invtitle.InnerHtml.ToUpper() == "India Invoice".ToUpper() && Convert.ToInt16(ddljobtype.SelectedValue) == 4)
        {
            if (DataBinder.Eval(e.Row.DataItem, "INVNO") != null && DataBinder.Eval(e.Row.DataItem, "INVNO").ToString() != "")
                ((ImageButton)e.Row.Cells[3].FindControl("btnsaveprint")).Enabled = false;
        }


        //Color changed for previewed invoice 

        if (DataBinder.Eval(e.Row.DataItem, "INDIAINVPREVIEW") != null && DataBinder.Eval(e.Row.DataItem, "INDIAINVPREVIEW").ToString() != "")
        {
            if (ddljobtype.SelectedValue.ToString() == "1")//for journal
                ((HtmlImage)e.Row.Cells[7].FindControl("Journal_imgIndiaPDF")).Src = "images/te-proofgreen.gif";
            else if (ddljobtype.SelectedValue.ToString() == "2")//for Book
                ((HtmlImage)e.Row.Cells[4].FindControl("Book_imgIndiaPDF")).Src = "images/te-proofgreen.gif";
            else if (ddljobtype.SelectedValue.ToString() == "3")//for Projects
                ((HtmlImage)e.Row.Cells[4].FindControl("Project_imgIndiaPDF")).Src = "images/te-proofgreen.gif";
            else if (ddljobtype.SelectedValue.ToString() == "4")//for WIP
                ((HtmlImage)e.Row.Cells[1].FindControl("WIP_imgIndiaPDF")).Src = "images/te-proofgreen.gif";
        }
        if (DataBinder.Eval(e.Row.DataItem, "DUBLININVPREVIEW") != null && DataBinder.Eval(e.Row.DataItem, "DUBLININVPREVIEW").ToString() != "")
        {
            if (ddljobtype.SelectedValue.ToString() == "1")//for journal
                ((HtmlImage)e.Row.Cells[7].FindControl("Journal_imgDublinPDF")).Src = "images/te-proofgreen.gif";
            else if (ddljobtype.SelectedValue.ToString() == "2")//for Book
                ((HtmlImage)e.Row.Cells[5].FindControl("Book_imgDublinPDF")).Src = "images/te-proofgreen.gif";
            else if (ddljobtype.SelectedValue.ToString() == "3")//for Projects
                ((HtmlImage)e.Row.Cells[5].FindControl("Project_imgDublinPDF")).Src = "images/te-proofgreen.gif";
            else if (ddljobtype.SelectedValue.ToString() == "4")//for WIP
                ((HtmlImage)e.Row.Cells[2].FindControl("WIP_imgDublinPDF")).Src = "images/te-proofgreen.gif";
        }

        //If find any special character change row color in projects
        if (Convert.ToInt16(ddljobtype.SelectedValue) == 2 || Convert.ToInt16(ddljobtype.SelectedValue) == 3)
        {
            Regex reg = new Regex(@"(([/\\\?\*:\|\#\&])|(&lt;)|(&gt;)|(&quot;))");
            if (reg.IsMatch(e.Row.Cells[1].Text))
                e.Row.BackColor = System.Drawing.Color.LightPink;
        }



        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    HiddenField invno = (HiddenField)e.Row.FindControl("HFInvNo");
        //    if (invno.Value != null && invno.Value != "")
        //        e.Row.Enabled = false;
        //}
    }

    protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rindex;

            //GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;

            //GridViewRow row = (GridViewRow)(e.CommandSource);



            if (e.CommandArgument != null && e.CommandArgument.ToString() != "" && e.CommandName == "Approve")
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
                rindex = row.DataItemIndex;
                UpdateApprover(System.Convert.ToInt16(e.CommandArgument), rindex, Convert.ToInt16(ddljobtype.SelectedValue), e.CommandName);
            }
            else if (e.CommandName == "saveprint")
            {

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
                rindex = row.DataItemIndex;
                UpdateInvoiceNoDate(System.Convert.ToInt16(e.CommandArgument), Convert.ToInt16(ddljobtype.SelectedValue), e.CommandName);
                string fname = "";
                if (ddljobtype.SelectedValue == "1")
                {

                    fname = Session["PDFFilePathDub"] + ((Label)row.Cells[14].FindControl("Id_Jourcode")).Text.ToString().Trim() + ((Label)row.Cells[14].FindControl("Id_Issue")).Text.ToString().Trim().Replace("/", "_");
                }
                else if (ddljobtype.SelectedValue == "2")
                    fname = Session["PDFFilePathDub"] + ((Label)row.Cells[9].FindControl("Id_Bnumber")).Text.ToString().Trim();
                else if (ddljobtype.SelectedValue == "3")
                    fname = Session["PDFFilePathDub"] + ((Label)row.Cells[9].FindControl("Id_Pcode")).Text.ToString().Trim();

                if (File.Exists(fname + ".pdf"))
                    File.Delete(fname + ".pdf");
                if (File.Exists(fname + ".xls"))
                    File.Delete(fname + ".xls");

                row.Visible = false;

                if (ddljobtype.SelectedValue.ToString() != "4")//No need to update preview date, bcz we have insert in wiparticles_dp table after approve.
                {
                    //Update india and dublin preview date is null
                    datasourceIB preview_obj = new datasourceIB();
                    try
                    {
                        preview_obj.UpdatePreviewDate("SAVEPRINT", ddljobtype.SelectedValue.ToString(), e.CommandArgument.ToString(), "null");
                    }
                    catch (Exception ex)
                    { throw ex; }
                    finally
                    { preview_obj = null; }
                }

            }


        }
        catch (Exception oex)
        { throw oex; }
        finally
        { Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>cursor_clear();</script>"); }

    }



    private void UpdateInvoiceNoDate(int ComArg, int category, string commandname)
    {
        datasourceIB dib = new datasourceIB();
        try
        {
            if (Session["employeenumber"] != null)
                dib.UpdateApproveName(Convert.ToInt16(Session["employeenumber"].ToString()), ComArg, category, commandname);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { dib = null; }

    }

    private void UpdateApprover(int iInumber, int DGItemIndex, int category, string commandname)
    {
        //biz_IB bib = new biz_IB();
        bool bUpdate = false;
        datasourceIB dib = new datasourceIB();
        if (Session["employeenumber"] != null) //&& Session["employeenumber"].ToString() == "14")  
            bUpdate = dib.UpdateApproveName(Convert.ToInt16(Session["employeenumber"].ToString()), iInumber, category, commandname);

        if (bUpdate)
        {
            if (category == 2)
                BooksInvoiceList.Rows[DGItemIndex].Visible = false;
            else if (category == 3)
                ProjectInvoiceList.Rows[DGItemIndex].Visible = false;
            else
                adgdispatchedlist.Rows[DGItemIndex].Visible = false;
        }
        dib = null;
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

    private void HideShowColumns(bool bBoolean, GridView oGrid)
    {
        //adgdispatchedlist.Columns[7].Visible = false;
        adgdispatchedlist.Columns[8].Visible = false;
        adgdispatchedlist.Columns[9].Visible = false;
        //adgdispatchedlist.Columns[10].Visible = false;
        adgdispatchedlist.Columns[11].Visible = false;
        adgdispatchedlist.Columns[12].Visible = false;
        adgdispatchedlist.Columns[13].Visible = false;
        adgdispatchedlist.Columns[14].Visible = false;

        //BooksInvoiceList.Columns[4].Visible = false;
        //BooksInvoiceList.Columns[5].Visible = false;
        BooksInvoiceList.Columns[6].Visible = false;
        BooksInvoiceList.Columns[7].Visible = false;
        BooksInvoiceList.Columns[8].Visible = false;
        BooksInvoiceList.Columns[9].Visible = false;

        //ProjectInvoiceList.Columns[4].Visible = false;
        ProjectInvoiceList.Columns[6].Visible = false;
        //ProjectInvoiceList.Columns[5].Visible = false;
        ProjectInvoiceList.Columns[7].Visible = false;
        ProjectInvoiceList.Columns[8].Visible = false;
        ProjectInvoiceList.Columns[9].Visible = false;
        WIPInvoiceList.Columns[3].Visible = false;
        WIPInvoiceList.Columns[4].Visible = false;

        BooksInvoiceList.Visible = false;
        ProjectInvoiceList.Visible = false;
        adgdispatchedlist.Visible = false;
        WIPInvoiceList.Visible = false;
        // set values here
        if (invtitle.InnerHtml.ToUpper() == "Final Invoice".ToUpper())
        {
            //adgdispatchedlist.Columns[10].Visible = bBoolean;
            adgdispatchedlist.Columns[12].Visible = bBoolean;
            adgdispatchedlist.Columns[13].Visible = bBoolean;
            //BooksInvoiceList.Columns[5].Visible = bBoolean;
            BooksInvoiceList.Columns[8].Visible = bBoolean;
            //ProjectInvoiceList.Columns[4].Visible = bBoolean;
            //ProjectInvoiceList.Columns[5].Visible = bBoolean;
            ProjectInvoiceList.Columns[8].Visible = bBoolean;
            WIPInvoiceList.Columns[4].Visible = bBoolean;
        }
        else
        {
            //BooksInvoiceList.Columns[4].Visible = bBoolean;
            //BooksInvoiceList.Columns[5].Visible = bBoolean;
            BooksInvoiceList.Columns[7].Visible = bBoolean;
            //BooksInvoiceList.Columns[9].Visible = bBoolean;
            //adgdispatchedlist.Columns[10].Visible = bBoolean;
            adgdispatchedlist.Columns[11].Visible = bBoolean;
            //adgdispatchedlist.Columns[7].Visible = bBoolean;
            //ProjectInvoiceList.Columns[4].Visible = bBoolean;
            //ProjectInvoiceList.Columns[5].Visible = bBoolean;
            ProjectInvoiceList.Columns[7].Visible = bBoolean;
            //ProjectInvoiceList.Columns[9].Visible = bBoolean;
            WIPInvoiceList.Columns[3].Visible = bBoolean;
        }
        oGrid.Visible = true;
    }

    protected void exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        if (ddljobtype.SelectedValue == "1")///////Journal///////
        {
            adgdispatchedlist.Columns[7].Visible = false;
            adgdispatchedlist.Columns[8].Visible = false;
            adgdispatchedlist.Columns[9].Visible = false;
            adgdispatchedlist.Columns[10].Visible = false;
            adgdispatchedlist.Columns[11].Visible = false;
            adgdispatchedlist.Columns[12].Visible = false;
            adgdispatchedlist.Columns[13].Visible = false;
            Export_Excel(adgdispatchedlist);
        }
        else if (ddljobtype.SelectedValue == "2")//////Book//////
        {
            BooksInvoiceList.Columns[4].Visible = false;
            BooksInvoiceList.Columns[5].Visible = false;
            BooksInvoiceList.Columns[6].Visible = false;
            BooksInvoiceList.Columns[7].Visible = false;
            BooksInvoiceList.Columns[8].Visible = false;
            Export_Excel(BooksInvoiceList);
        }
        else if (ddljobtype.SelectedValue == "3")
        {
            ProjectInvoiceList.Columns[4].Visible = false;
            ProjectInvoiceList.Columns[5].Visible = false;
            ProjectInvoiceList.Columns[6].Visible = false;
            ProjectInvoiceList.Columns[7].Visible = false;
            ProjectInvoiceList.Columns[8].Visible = false;
            Export_Excel(ProjectInvoiceList);
        }
        else if (ddljobtype.SelectedValue == "4")
        {
            WIPInvoiceList.Columns[1].Visible = false;
            WIPInvoiceList.Columns[2].Visible = false;
            WIPInvoiceList.Columns[3].Visible = false;
            WIPInvoiceList.Columns[4].Visible = false;
            WIPInvoiceList.Columns[8].Visible = false;
            WIPInvoiceList.Columns[9].Visible = false;
            Export_Excel(WIPInvoiceList);
        }


    }

    private void Export_Excel(GridView Gview)
    {
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

        //adgdispatchedlist.RenderControl(oHtmlTextWriter);
        HtmlForm htmfrm = new HtmlForm();
        Gview.Parent.Controls.Add(htmfrm);
        htmfrm.Attributes["runat"] = "Server";
        htmfrm.Controls.Add(Gview);
        htmfrm.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }



}



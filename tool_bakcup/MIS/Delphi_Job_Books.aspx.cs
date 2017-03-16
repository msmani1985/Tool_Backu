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
using System.Text;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Delphi_Job_Books : System.Web.UI.Page
{
    public int id = 1;
    public int id1 = 1;
    public int l = 1;
    private Delphi_Books oBook = new Delphi_Books();
    private static DataSet dsGrapType = new DataSet();
    private static DataTable dtBook = new DataTable();
    private static string sSortExpression = "";
    datasourceSQL oSql = new datasourceSQL();
    datasourceIBSQL oIBSql = new datasourceIBSQL();
    static bool jobtrack = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["employeeid"] == null)
        //{
        //    throw new Exception("Session Expired!");
        //}
        Page.Header.DataBind(); 
        if (!IsPostBack) 
            this.popScreen();
        
    }
    private void popScreen()
    {
        DataSet dsemp = oBook.GetEmployeeName();
        DataSet dsdepart = oBook.GetDepartment();

        string myvalue = "10066";
        DataSet dscust = oBook.getCustomers();
        drpCustomerSearch.DataSource = dscust;
        drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
        drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
        drpCustomerSearch.SelectedValue = myvalue.ToString();
        drpCustomerSearch.DataBind();
        drpBookCustomer.DataSource = dscust;
        drpBookCustomer.DataTextField = dscust.Tables[0].Columns[1].ToString();
        drpBookCustomer.DataValueField = dscust.Tables[0].Columns[0].ToString();
        drpBookCustomer.SelectedValue = myvalue.ToString();
        drpBookCustomer.DataBind();
        drpBookcustfinsite.Items.Insert(0, new ListItem("-- select --", "0"));

        lstAdvancedCustomer.DataSource = dscust;
        lstAdvancedCustomer.DataTextField = dscust.Tables[0].Columns[1].ToString();
        lstAdvancedCustomer.DataValueField = dscust.Tables[0].Columns[0].ToString();
        lstAdvancedCustomer.DataBind();

        //pop format/styles
        DataSet dsFormat = oBook.getFormatOrStyles();
        drpBookTypeset.DataSource = dsFormat;
        drpBookTypeset.DataTextField = dsFormat.Tables[0].Columns[1].ToString();
        drpBookTypeset.DataValueField = dsFormat.Tables[0].Columns[0].ToString();
        drpBookTypeset.DataBind();
        drpBookTypeset.Items.Insert(0, new ListItem("-- select --", "0"));
        ////pop service types
        DataSet dsStatus = oBook.getCurrentStatus();

        DataSet dsSevice = oBook.getBookStage();

        DataSet dsSeviceHist = oBook.getBookStage();
        DropCurStageHist.DataSource = dsSeviceHist;
        DropCurStageHist.DataTextField = dsSeviceHist.Tables[0].Columns[1].ToString();
        DropCurStageHist.DataValueField = dsSeviceHist.Tables[0].Columns[0].ToString();
        DropCurStageHist.DataBind();

        DataSet dscustPE = oBook.getPEName();
        txtBookEditor.DataSource = dscustPE;
        txtBookEditor.DataTextField = dscustPE.Tables[0].Columns[1].ToString();
        txtBookEditor.DataValueField = dscustPE.Tables[0].Columns[0].ToString();
        txtBookEditor.DataBind();

        Session["hfB_ID"] = "";
       
        if (Request.QueryString["q"] != null &&
            Request.QueryString["q"].ToString().Trim() != "")
        {
            string pageQuery = Request.QueryString["q"].ToString().Trim();
            switch (pageQuery)
            {
                case "new_book":
                    drpBookCustomer.Enabled = true;
                    txtBookNo.Enabled = true;
                    lblBookHeader.Text = "New Book";
                    imgBookHeader.Src = "images/tools/new.png";
                    this.showPanel(tabBookDetails);
                    break;
            }
        }
        else this.showPanel(tabGeneral);
        txtSearch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) ||(event.keyCode == 13)){document.getElementById('" + btnSearch.ClientID + "').click();return false;}} else {return true}; ");
    }
    
   
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabGeneral":
                miGeneral.Attributes.Add("class", "current");
                miBookDetails.Attributes.Add("class", "");
                miChapDetails.Attributes.Add("class", "");
                miBookBarcode.Attributes.Add("class", "");
                miBooklogEvents.Attributes.Add("class", "");
                this.tabBookBarcode.Visible = false;
                this.tabGeneral.Visible = true;
                this.tabBookDetails.Visible = false;
                this.tabChapterdetails.Visible = false;
                this.tabLogEvents.Visible = false;
                break;
            case "tabBookDetails":
                if (this.hfB_Name.Value != "")
                    lblBookHeader.Text = "Edit Book : " + this.hfB_Name.Value;
                miGeneral.Attributes.Add("class", "");
                miBookDetails.Attributes.Add("class", "current");
                miChapDetails.Attributes.Add("class", "");
                miBookBarcode.Attributes.Add("class", "");
                miBooklogEvents.Attributes.Add("class", "");
                this.tabBookBarcode.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabBookDetails.Visible = true;
                this.tabChapterdetails.Visible = false;
                this.tabLogEvents.Visible = false;
                break;

            case "tabChapterdetails":
                if (this.hfB_Name.Value != "")
                    lblChapHeader.Text = "Chapter Details : " + this.hfB_Name.Value;
                miGeneral.Attributes.Add("class", "");
                miBookDetails.Attributes.Add("class", "");
                miChapDetails.Attributes.Add("class", "current");
                miBookBarcode.Attributes.Add("class", "");
                miBooklogEvents.Attributes.Add("class", "");
                this.tabBookBarcode.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabBookDetails.Visible = false;
                this.tabChapterdetails.Visible = true;
                this.tabLogEvents.Visible = false;
                break;
            case "tabBookBarcode":
                if (this.hfB_Name.Value != "")
                    lblBookBarcode.Text = "Barcode Scanner: " + this.hfB_Name.Value;
                miGeneral.Attributes.Add("class", "");
                miBookDetails.Attributes.Add("class", "");
                miChapDetails.Attributes.Add("class", "");
                miBookBarcode.Attributes.Add("class", "current");
                miBooklogEvents.Attributes.Add("class", "");
                this.tabBookBarcode.Visible = true;
                this.tabGeneral.Visible = false;
                this.tabBookDetails.Visible = false;
                this.tabChapterdetails.Visible = false;
                this.tabLogEvents.Visible = false;
                break;
            case "tabLogEvents":
                if (this.hfB_Name.Value != "")
                    lblBookBarcode.Text = "Logged Events: " + this.hfB_Name.Value;
                miGeneral.Attributes.Add("class", "");
                miBookDetails.Attributes.Add("class", "");
                miChapDetails.Attributes.Add("class", "");
                miBookBarcode.Attributes.Add("class", "");
                miBooklogEvents.Attributes.Add("class", "current");
                this.tabBookBarcode.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabBookDetails.Visible = false;
                this.tabChapterdetails.Visible = false;
                this.tabLogEvents.Visible = true;
                break;
        }
    }
   
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        char CompleteFlag = 'N';
        if (chkViewCompleted.Checked) CompleteFlag = 'Y';
        DataSet dsb = oBook.getBooks(txtSearch.Text.Trim().ToUpper(), CompleteFlag,
            drpCustomerSearch.SelectedItem.Value.Trim());
        dtBook = dsb.Tables[0].Copy();
        gvBooks.DataSource = dtBook;
        gvBooks.DataBind();
        this.hfB_ID.Value = "";
        this.hfB_Name.Value = "";
        this.hfC_ID.Value = "";
        this.showPanel(tabGeneral);
    }
    protected void gvBooks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,\"" + ((HiddenField)e.Row.FindControl("hfgvBookID")).Value.Trim() + "\",\"" + ((Label)e.Row.FindControl("lblgvBnumber")).Text.Trim() + "\");";
            
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                if (cell.HasControls())
                {
                    LinkButton btn = (LinkButton)cell.Controls[0];
                    if (btn != null)
                    {
                        Image img = new Image(); img.Attributes.Add("align", "absmiddle");
                        if (sSortExpression == btn.CommandArgument && ViewState[sSortExpression] != null)
                        {
                            if ((SortDirection)ViewState[sSortExpression] == SortDirection.Ascending)
                                img.ImageUrl = "~/images/tools/arrow_up.png";
                            else
                                img.ImageUrl = "~/images/tools/arrow_down.png";
                            cell.Controls.Add(img);
                        }
                    }
                }
            }
        }
    }
    protected void lnkGeneral_Click(object sender, EventArgs e)
    {
        this.showPanel(tabGeneral);
    }
   
    protected void lnkBookdetails_Click(object sender, EventArgs e)
    {
        if (hfB_ID.Value != "")
        {
            loadBookDetails(hfB_ID.Value.Trim());
        }
        //else
        //    txtBookRecived.Text = DateTime.Now.ToString("MM/dd/yyyy");
        hfC_ID.Value = "";
        this.showPanel(tabBookDetails);
    }

    protected void drpBookcustfinsite_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpBookcustfinsite.Items.Clear();
        if (drpBookCustomer.SelectedItem.Value.Trim() != "0")
        {
            DataSet dscustfin = oBook.getCusomerFinsite(drpBookCustomer.SelectedItem.Value.Trim());
            drpBookcustfinsite.DataSource = dscustfin;
            drpBookcustfinsite.DataTextField = dscustfin.Tables[0].Columns[1].ToString();
            drpBookcustfinsite.DataValueField = dscustfin.Tables[0].Columns[0].ToString();
            drpBookcustfinsite.DataBind();
        }
        drpBookcustfinsite.Items.Insert(0, new ListItem("-- select --", "0"));
    }
    protected void cmd_New_Book_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Delphi_Job_Books.aspx?q=new_book", true);
    }
    protected void cmd_Save_Book_Click(object sender, ImageClickEventArgs e)
    {
        string[] aBookDetails;
        try
        {
            if (validateScreen())
            {
            if (hfB_ID.Value.Trim() == "")
            {
                    //insert book
                    aBookDetails = new string[98];
                    aBookDetails[0] = txtBookNo.Text.Trim(); 
                    aBookDetails[1] = txtBookTitle.Text.Trim();
                    aBookDetails[2] = drpBookCustomer.SelectedItem.Value.Trim();
                    aBookDetails[3] = drpBookcustfinsite.SelectedItem.Value.Trim();
                    aBookDetails[4] = txtBookSize.Text.Trim();
                    aBookDetails[5] = txtBookComments.Text.Trim();
                    aBookDetails[6] = txtBookPISBN.Text.Trim();
                    aBookDetails[7] = "";
                    aBookDetails[8] = "";
                    aBookDetails[9] = drpBookTypeset.SelectedItem.Value;
                    ////////aBookDetails[10] = txtISBN2.Text;
                    ////////aBookDetails[11] = drpBookTypeset.SelectedValue;
                    aBookDetails[12] = "Y";
                    aBookDetails[13] = "N";
                    aBookDetails[14] = "N";
                    aBookDetails[15] = "11";
                    aBookDetails[16] = txtBookEditor.SelectedValue;
                    aBookDetails[17] = txtPriceNo.Text.Trim();
                    aBookDetails[18] = txtBookPONumber.Text.Trim();
                    
                    aBookDetails[20] = "L";
                    aBookDetails[21] = DropCurStageHist.SelectedValue;
                    aBookDetails[22] = "15";
                    aBookDetails[23] = "0";
                    aBookDetails[24] = txtcost1.Text;
                    aBookDetails[25] = txtQty1.Text;
                    aBookDetails[26] = txtPrice1.Text;
                    aBookDetails[27] = "";
                    aBookDetails[28] = txtPages.Text;
                    aBookDetails[29] = "";
                    aBookDetails[30] = "";
                    aBookDetails[31] = "";
                    aBookDetails[32] = txtcost2.Text;
                    aBookDetails[33] = txtQty2.Text;
                    aBookDetails[34] = txtPrice2.Text;
                    aBookDetails[35] = "";
                    aBookDetails[36] = txtcost3.Text;
                    aBookDetails[37] = txtQty3.Text;
                    aBookDetails[38] = txtPrice3.Text;
                    aBookDetails[39] = "";
                    aBookDetails[40] = txtCost4.Text;
                    aBookDetails[41] = txtQty4.Text;
                    aBookDetails[42] = txtPrice4.Text;
                    aBookDetails[43] = "";
                    aBookDetails[44] = txtCost5.Text;
                    aBookDetails[45] = txtQty5.Text;
                    aBookDetails[46] = txtPrice5.Text;
                    aBookDetails[47] = "";
                    aBookDetails[48] = "";
                    aBookDetails[49] = "";
                    aBookDetails[50] = "";
                    aBookDetails[51] = "";
                    aBookDetails[52] = "0";

                    aBookDetails[53] = "";
                    aBookDetails[54] = "";
                    aBookDetails[55] = "";
                    aBookDetails[56] = "";
                    aBookDetails[57] = "0";

                    aBookDetails[58] = "";
                    aBookDetails[59] = "";
                    aBookDetails[60] = "";
                    aBookDetails[61] = "";
                    aBookDetails[62] = "0";

                    aBookDetails[63] = "";
                    aBookDetails[64] = "";
                    aBookDetails[65] = "";
                    aBookDetails[66] = "";
                    aBookDetails[70] = "0";

                    aBookDetails[67] = rbWorkFrom.SelectedValue;

                    aBookDetails[68] = txtBookRecDate.Text;
                    aBookDetails[69] = "";

                    aBookDetails[71] = "";
                    aBookDetails[72] = "";
                    aBookDetails[73] = "";
                    aBookDetails[74] = "";
                    aBookDetails[75] = "0";

                    aBookDetails[76] = "";
                    aBookDetails[77] = "";
                    aBookDetails[78] = "";
                    aBookDetails[79] = "";
                    aBookDetails[80] = "0";

                    aBookDetails[81] = "";
                    aBookDetails[82] = "";
                    aBookDetails[10] = "";
                    aBookDetails[11] = "";
                    aBookDetails[19] = "0";
                    aBookDetails[83] = rbTypeCost.SelectedValue;
                    aBookDetails[84] = rbType.SelectedValue;
                    aBookDetails[85] = "0";
                    aBookDetails[86] = "";
                    aBookDetails[87] = "";
                    aBookDetails[88] = "";
                    aBookDetails[89] = "";
                    aBookDetails[90] = "";
                    /* S_TypeNo value removed  */
                    aBookDetails[91] = DropCurStageHist.SelectedValue;
                    aBookDetails[92] = txtBStartDt.Text;
                    aBookDetails[93] = "";
                    aBookDetails[94] = txtBDueDt.Text;
                    aBookDetails[95] = txtBDisDt.Text;
                    aBookDetails[96] = txtPagesH.Text;
                    if (txtPagesH.Text.Trim() == "")
                    {
                        aBookDetails[96] = "0";
                    }
                    else
                    {
                        aBookDetails[96] = txtPagesH.Text.Trim();
                    }
                    aBookDetails[97] = txtTemplate.Text.Trim();
                    clearTxt();
                    string msg = this.oBook.InsertBook(aBookDetails);
                    
                    if (msg.Contains("Book creation failed!") ||
                        msg.Contains("Book Already Exists!")) Alert(msg);
                    else
                    {
                        hfB_ID.Value = msg;
                        //this.loadBookDetails(msg);
                        Alert("Successfully Saved.");
                    }
                   
                }
                else
                {
                    //update book
                    aBookDetails = new string[99];
                    aBookDetails[0] = txtBookNo.Text.Trim();
                    aBookDetails[1] = txtBookTitle.Text.Trim();
                    aBookDetails[2] = drpBookCustomer.SelectedItem.Value.Trim();
                    aBookDetails[3] = drpBookcustfinsite.SelectedItem.Value.Trim();
                    aBookDetails[4] = txtBookSize.Text.Trim();
                    aBookDetails[5] = txtBookComments.Text.Trim();
                    aBookDetails[6] = txtBookPISBN.Text.Trim();
                    aBookDetails[7] = "";
                    aBookDetails[8] = "";
                    aBookDetails[9] = drpBookTypeset.SelectedItem.Value;
                    ////////aBookDetails[10] = txtISBN2.Text;
                    ////////aBookDetails[11] = drpBookTypeset.SelectedValue;
                    aBookDetails[12] = "Y";
                    aBookDetails[13] = "N";
                    aBookDetails[14] = "N";
                    aBookDetails[15] = "11";
                    aBookDetails[16] = txtBookEditor.SelectedValue;
                    aBookDetails[17] = txtPriceNo.Text.Trim();
                    aBookDetails[18] = txtBookPONumber.Text.Trim();
                    //////aBookDetails[19] = drpBookcustfinsite.SelectedItem.Value.Trim();
                    aBookDetails[20] = "L";
                    aBookDetails[21] = DropCurStageHist.SelectedValue;
                    aBookDetails[22] = "15";
                    aBookDetails[23] = "0";
                    aBookDetails[24] = txtcost1.Text;
                    aBookDetails[25] = txtQty1.Text;
                    aBookDetails[26] = txtPrice1.Text;
                    aBookDetails[27] = "";
                    aBookDetails[28] = txtPages.Text;
                    aBookDetails[29] = "";
                    aBookDetails[30] = "";
                    aBookDetails[31] = "";
                    aBookDetails[32] = txtcost2.Text;
                    aBookDetails[33] = txtQty2.Text;
                    aBookDetails[34] = txtPrice2.Text;
                    aBookDetails[35] = "";
                    aBookDetails[36] = txtcost3.Text;
                    aBookDetails[37] = txtQty3.Text;
                    aBookDetails[38] = txtPrice3.Text;
                    aBookDetails[39] = "";
                    aBookDetails[40] = txtCost4.Text;
                    aBookDetails[41] = txtQty4.Text;
                    aBookDetails[42] = txtPrice4.Text;
                    aBookDetails[43] = "";
                    aBookDetails[44] = txtCost5.Text;
                    aBookDetails[45] = txtQty5.Text;
                    aBookDetails[46] = txtPrice5.Text;
                    aBookDetails[47] = "";

                    aBookDetails[48] = "";
                    aBookDetails[49] = "";
                    aBookDetails[50] = "";
                    aBookDetails[51] = "";
                    aBookDetails[52] = "0";

                    aBookDetails[53] = "";
                    aBookDetails[54] = "";
                    aBookDetails[55] = "";
                    aBookDetails[56] = "";
                    aBookDetails[57] = "0";

                    aBookDetails[58] = "";
                    aBookDetails[59] = "";
                    aBookDetails[60] = "";
                    aBookDetails[61] = "";
                    aBookDetails[62] = "0";

                    aBookDetails[63] = "";
                    aBookDetails[64] = "";
                    aBookDetails[65] = "";
                    aBookDetails[66] = "";
                    aBookDetails[70] = "0";

                    aBookDetails[67] = rbWorkFrom.SelectedValue;
                    aBookDetails[68] = txtBookRecDate.Text;
                    aBookDetails[69] = "";

                    aBookDetails[71] = "";
                    aBookDetails[72] = "";
                    aBookDetails[73] = "";
                    aBookDetails[74] = "";
                    aBookDetails[75] = "0";

                    aBookDetails[76] = "";
                    aBookDetails[77] = "";
                    aBookDetails[78] = "";
                    aBookDetails[79] = "";
                    aBookDetails[80] = "0";

                    aBookDetails[81] = "";
                    aBookDetails[82] = "";
                    aBookDetails[10] = "";
                    aBookDetails[11] = "";
                    aBookDetails[19] = "0";
                    aBookDetails[83] = rbTypeCost.SelectedValue;
                    aBookDetails[84] = rbType.SelectedValue;
                    aBookDetails[85] = "0";
                    aBookDetails[86] = "";
                    aBookDetails[87] = "";
                    aBookDetails[88] = "";
                    aBookDetails[89] = "";
                    aBookDetails[90] = "";
                    /* S_TypeNo value removed  */
                    aBookDetails[91] = DropCurStageHist.SelectedValue;
                    aBookDetails[92] = txtBStartDt.Text;
                    aBookDetails[93] = "";
                    aBookDetails[94] = txtBDueDt.Text;
                    aBookDetails[95] = txtBDisDt.Text;
                    aBookDetails[96] = BNO_ID.Value.Trim();
                     if (txtPagesH.Text.Trim() == "")
                    {
                        aBookDetails[97] = "0";
                    }
                    else
                    {
                        aBookDetails[97] = txtPagesH.Text.Trim();
                    }
                     aBookDetails[98] = txtTemplate.Text.Trim();
                    string msg = this.oBook.UpdateBook(aBookDetails);
                    clearTxt();
                    if (msg.Contains("Error updating Book:")) Alert(msg);
                    else
                    {
                        //this.loadBookDetails(msg);
                        Alert("Successfully Saved.");
                    }
                }
            }
            loadBookDetails(hfB_ID.Value);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally { aBookDetails = null; }
    }
    public void clearTxt()
    {
        txtBDisDt.Text = "";
        txtBDueDt.Text = "";
        txtBStartDt.Text = "";
        DropCurStageHist.SelectedIndex = 0;
        txtPagesH.Text = "";
    }
    private void loadBookDetails(string sBookID)
    {
        sBookID = sBookID.Trim();
        BNO_ID.Value = sBookID.Trim();
        if (sBookID != "")
        {
            try
            {
                DataSet dsBook = oBook.getBookDetailsByID(sBookID);
                lblBookHeader.Text = "Edit Book";
                imgBookHeader.Src = "images/tools/edit.png";
                DataRow row = dsBook.Tables[0].Rows[0];
                drpBookCustomer.ClearSelection();
                if (drpBookCustomer.Items.FindByValue(row["custno"].ToString().Trim()) != null)
                    drpBookCustomer.Items.FindByValue(row["custno"].ToString().Trim()).Selected = true;
                drpBookcustfinsite.Items.Clear();
                DataSet dscustfin = oBook.getCusomerFinsite(row["custno"].ToString().Trim());
                drpBookcustfinsite.DataSource = dscustfin;
                drpBookcustfinsite.DataTextField = dscustfin.Tables[0].Columns[1].ToString();
                drpBookcustfinsite.DataValueField = dscustfin.Tables[0].Columns[0].ToString();
                drpBookcustfinsite.DataBind();
                if (dscustfin.Tables[0].Rows.Count > 0)
                {
                    if (drpBookcustfinsite.Items.FindByValue(row["finsiteno"].ToString().Trim()) != null)
                        drpBookcustfinsite.Items.FindByValue(row["finsiteno"].ToString().Trim()).Selected = true;
                }
                drpBookcustfinsite.Items.Insert(0, new ListItem("-- select --", "0"));
                txtBookNo.Text = row["BNUMBER"].ToString();
                txtBookTitle.Text = row["BTITLE"].ToString();
                txtBookSize.Text = row["BSIZE"].ToString();
                txtBookPISBN.Text = row["BISBN"].ToString();
                rbWorkFrom.SelectedValue = row["LocationID"].ToString();
                txtPriceNo.Text = row["BCNO_2009"].ToString();
                txtBookComments.Text = row["BComments"].ToString();
                if (row["STYPENO"].ToString() != "")
                {
                    string temp = row["STYPENO"].ToString();
                    DropCurStageHist.SelectedValue = row["STYPENO"].ToString();
                }
                else
                {
                    DropCurStageHist.SelectedValue = row["STYPENO"].ToString();
                }
                txtPages.Text = row["BNOOFPAGES"].ToString();
                txtcost1.Text = row["BACOSTDESC1"].ToString();
                txtQty1.Text = row["BAQTY1"].ToString();
                txtPrice1.Text = row["BACNO1"].ToString();
                txtcost2.Text = row["BACOSTDESC2"].ToString();
                txtQty2.Text = row["BAQTY2"].ToString();
                txtPrice2.Text = row["BACNO2"].ToString();
                txtcost3.Text = row["BACOSTDESC3"].ToString();
                txtQty3.Text = row["BAQTY3"].ToString();
                txtPrice3.Text = row["BACNO3"].ToString();
                txtCost4.Text = row["BACOSTDESC4"].ToString();
                txtQty4.Text = row["BAQTY4"].ToString();
                txtPrice4.Text = row["BACNO4"].ToString();
                txtCost5.Text = row["BACOSTDESC5"].ToString();
                txtQty5.Text = row["BAQTY5"].ToString();
                txtPrice5.Text = row["BACNO5"].ToString();
                drpBookTypeset.SelectedValue = row["BSNO"].ToString();
                txtBookEditor.SelectedValue = row["Conno"].ToString();
                txtBookPONumber.Text = row["PONumber"].ToString();
                rbTypeCost.SelectedValue = row["Bcost"].ToString();
                rbType.SelectedValue = row["BType"].ToString();
                txtBookRecDate.Text = row["India_Recd"].ToString();
                txtTemplate.Text = row["Template"].ToString();
                DataSet dsBookHist1 = oBook.getBook_Hist_DetailsByID(sBookID, "*");
                if (dsBookHist1 != null)
                {
                    gvBookHist.DataSource = dsBookHist1;
                    gvBookHist.DataBind();

                    gvBookHistUpdate.DataSource = dsBookHist1;
                    gvBookHistUpdate.DataBind();
                }
                else
                {
                    gvBookHist.DataSource = null;
                    gvBookHist.DataBind();

                    gvBookHistUpdate.DataSource = null;
                    gvBookHistUpdate.DataBind();
                }
                if (row["BEMAIL_FLAG"].ToString() == "Y")
                {
                    cmd_Save_Book.Visible = false;
                    cmd_Print_Book.Visible = false;
                    txtBookNo.Enabled = false;
                    txtBookTitle.Enabled = false;
                    txtBookSize.Enabled = false;
                    txtBookPISBN.Enabled = false;
                    txtPriceNo.Enabled = false;
                    txtPages.Enabled = false;
                    txtcost1.Enabled = false;
                    txtQty1.Enabled = false;
                    txtPrice1.Enabled = false;
                    txtcost2.Enabled = false;
                    txtQty2.Enabled = false;
                    txtPrice2.Enabled = false;
                    txtcost3.Enabled = false;
                    txtQty3.Enabled = false;
                    txtPrice3.Enabled = false;
                    txtCost4.Enabled = false;
                    txtQty4.Enabled = false;
                    txtPrice4.Enabled = false;
                    txtCost5.Enabled = false;
                    txtQty5.Enabled = false;
                    txtPrice5.Enabled = false;
                    drpBookTypeset.Enabled = false;
                    txtBookEditor.Enabled = false;
                    txtBookPONumber.Enabled = false;
                    rbTypeCost.Enabled = false;
                    rbType.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }
        }
    }

    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (drpBookCustomer.SelectedItem.Value == "0") sMessage += i++ + ". Select a Customer\\r\\n";
        if (txtBookNo.Text.Trim() == "") sMessage += i++ + ". Enter CAT# Name\\r\\n";
        if (txtBookTitle.Text.Trim() == "") sMessage += i++ + ". Enter Book Title\\r\\n";
        if (drpBookcustfinsite.SelectedItem.Value == "0") sMessage += i++ + ". Select Financial Site\\r\\n";
        if (txtBStartDt.Text.Trim() == "" && hfB_ID.Value=="") sMessage += i++ + ". Enter Start Date\\r\\n";
        if (i > 1)
        {
            Alert(sMessage);
            return false;
        }
        return true;
    }
    protected void cmd_Print_Book_Click(object sender, ImageClickEventArgs e)
    {
        if (hfB_ID.Value.Trim() != "") 
            Page.RegisterStartupScript("Open", "<script language='javascript'>window.open('Print_jobbag.aspx?jobid=" + hfB_ID.Value.Trim() + "&jobtypeid=2&print=1','Preview','width=1000,height=600,left=5,top=25,toolbars=no,scrollbars=yes,status=yes,resizable=yes');</script>");
        else Alert("Select a Book");
    }
    
    protected void cmd_Excel_Export_Click(object sender, ImageClickEventArgs e)
    {
        if (gvBooks.Visible)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='18' align='center'><h4>Books Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>CAT#</b></td><td bgcolor='silver'><b>Book Title</b></td><td bgcolor='silver'><b>Project Editor</b></td><td bgcolor='silver'><b>Pagination Platform</b></td><td bgcolor='silver'><b>Stage</b></td><td bgcolor='silver'><b>Status</b></td><td bgcolor='silver'><b>Rec. Date</b></td><td bgcolor='silver'><b>Due Date</b></td><td bgcolor='silver'><b>Disp. Date</b></td><td bgcolor='silver'><b>Proof Pages</b></td><td bgcolor='silver'><b>Templates</b></td><td bgcolor='silver'><b>Trim</b></td><td bgcolor='silver'><b>Project Leader</b></td><td bgcolor='silver'><b>Barcode</b></td><td bgcolor='silver'><b>Type</b></td><td bgcolor='silver'><b>ISBN</b></td></tr>");
            int i = 1;
            foreach (DataRow r in dtBook.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["CUSTNAME"] + "</td>");
                sbData.Append("<td>" + r["BNUMBER"] + "</td>");
                sbData.Append("<td>" + r["BTITLE"] + "</td>");
                sbData.Append("<td>" + r["CONFULLNAME"] + "</td>");
                sbData.Append("<td>" + r["BSTYLE"] + "</td>");
                sbData.Append("<td>" + r["STYPENAME"] + "</td>");
                sbData.Append("<td>" + r["STDESCRIPTION"] + "</td>");
                sbData.Append("<td>" + r["BFIRSTSTARTDATE"] + "</td>");
                sbData.Append("<td>" + r["BFINALDISPATCH"] + "</td>");
                sbData.Append("<td>" + r["BFINALDISPATCH"] + "</td>");
                sbData.Append("<td>" + r["Bnoofpages"] + "</td>");
                sbData.Append("<td>" + r["Template_Created"] + "</td>");
                sbData.Append("<td>" + r["bsize"] + "</td>");
                sbData.Append("<td>" + r["firstemployee"] + "</td>");
                sbData.Append("<td>" + r["BBARCODE"] + "</td>");
                sbData.Append("<td>" + r["Type"] + "</td>");
                sbData.Append("<td>" + r["BISBN"] + "</td>");
                sbData.Append("</tr>");
                i = i + 1;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Books_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            Response.Write(sbData.ToString());
            Response.End();
        }
    }
    protected void gvBooks_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView();
        string di = "";
        try
        {
            SortDirection sortDirection = SortDirection.Ascending;
            if (ViewState[e.SortExpression] != null)
            {
                SortDirection currDirection = (SortDirection)ViewState[e.SortExpression];
                if (currDirection == SortDirection.Ascending) sortDirection = SortDirection.Descending;
            }
            ViewState[e.SortExpression] = sortDirection;
            if (sortDirection.Equals(SortDirection.Descending)) di = " desc"; dv.Table = dtBook;
            dv.Sort = e.SortExpression + di;
            sSortExpression = e.SortExpression;
            gvBooks.DataSource = dv;
            gvBooks.DataBind();
        }
        catch (Exception ec)
        {
            Alert(ec.Message);
        }
        finally
        {
            dv.Dispose();
        }
    }

    protected void gvChapter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor1(this,\"" + ((HiddenField)e.Row.FindControl("hfgvCID")).Value.Trim() + "\",\"" + hfB_Name.Value + "\");";
        }
    }
    protected void gvHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
        }
    }
    protected void gvHistory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet dsBookHist = oBook.getBook_Hist_DetailsByID(hfB_ID.Value.Trim(), "*");
        gvBookHist.DataSource = dsBookHist;
        gvBookHist.DataBind();
    }
    protected void gvHistory_RowCommandUpdate(object sender, GridViewCommandEventArgs e)
    {
        DataSet dsBookHist = oBook.getBook_Hist_DetailsByID(hfB_ID.Value.Trim(), "*");
        gvBookHistUpdate.DataSource = dsBookHist;
        gvBookHistUpdate.DataBind();
    }
    protected void lnkBookBarcode_Click(object sender, EventArgs e)
    {
        if (hfB_ID.Value != "")
        {
            loadBarcodeDetails(hfB_ID.Value);

            for (int k = 0; k < gvBookHistUpdate.Rows.Count; k++)
            {
                Label lbl1 = (Label) gvBookHistUpdate.Rows[k].FindControl("lblgvCFigure2");
                CheckBox chk = (CheckBox)gvBookHistUpdate.Rows[k].FindControl("chkDispatch");
                if (lbl1.Text == "")
                {
                    chk.Enabled  = true;
                }
                else
                {
                    chk.Enabled = false;
                }
            }
        }
        this.showPanel(tabBookBarcode);
    }
    protected void btnProBarUpdate_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            string inproc = "SpUpdateBarcodeBOOKS";
            string[,] pname ={
                    {"@BNO",hfB_ID.Value },{"@Empno",Session["employeeid"].ToString()},{"@IsExist","OUTPUT"},
                    {"@Evno","10069"},
                    };
            int val = this.oIBSql.ExcSProcedure(inproc, pname, CommandType.StoredProcedure);
            if (val == 1)
                msg = "Updated Successfully";
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
        }
    }
    private void loadBarcodeDetails(string sBookID)
    {
        sBookID = sBookID.Trim();
        BNO_ID.Value = sBookID;
        if (sBookID != "")
        {
            DataSet dsBook = oBook.getBookDetailsByID(sBookID);
            DataRow row = dsBook.Tables[0].Rows[0];
            txtProBarcode.Text = row["BBARCODE"].ToString();
            txtProCat.Text = row["BNUMBER"].ToString();
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DataSet dscustPE = oBook.getPEName();
        txtBookEditor.DataSource = dscustPE;
        txtBookEditor.DataTextField = dscustPE.Tables[0].Columns[1].ToString();
        txtBookEditor.DataValueField = dscustPE.Tables[0].Columns[0].ToString();
        txtBookEditor.DataBind();
    }
    protected void lstAdvancedFormat_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnUpdtStg_Click(object sender, EventArgs e)
    {

        string msg = string.Empty;
        if (txtBDisDt.Text.Trim() != "")
        {
            for (int i = 0; i < gvBookHistUpdate.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvBookHistUpdate.Rows[i].FindControl("chkDispatch");
                HiddenField hdn = (HiddenField)gvBookHistUpdate.Rows[i].FindControl("hfgvHistID");
                if (chk.Checked == true)
                {
                    try
                    {
                        string inproc = "SpUpdateStageBOOKS";
                        string[,] pname = { { "@bno", BNO_ID.Value.Trim() }, { "@histID", hdn.Value.Trim() }, { "@dispDate", txtBDisDt.Text } };
                        int val = this.oIBSql.ExcSProcedure(inproc, pname, CommandType.StoredProcedure);
                        if (val == 1)
                            msg = "Updated Successfully";
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                    finally
                    {

                    }
                }
            }

            //ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('"+msg+"');</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please enter Dispatch date.');</script>");
        }
        DataSet dsBookHist1 = oBook.getBook_Hist_DetailsByID(BNO_ID.Value.Trim(), "*");
        gvBookHist.DataSource = dsBookHist1;
        gvBookHist.DataBind();
        gvBookHistUpdate.DataSource = dsBookHist1;
        gvBookHistUpdate.DataBind();
        txtBDisDt.Text = "";

        for (int k = 0; k < gvBookHistUpdate.Rows.Count; k++)
        {
            Label lbl1 = (Label)gvBookHistUpdate.Rows[k].FindControl("lblgvCFigure2");
            CheckBox chk = (CheckBox)gvBookHistUpdate.Rows[k].FindControl("chkDispatch");
            if (lbl1.Text == "")
            {
                chk.Enabled = true;
            }
            else
            {
                chk.Enabled = false;
            }
        }

        //oBook.getBook_Hist_DetailsByID(BNO_ID.Value.Trim(), "1",txtBDisDt.Text);
    }
    protected void btnCreate_chaplist_Click(object sender, EventArgs e)
    {
        if (hfB_ID.Value != "" && txtNoofChap.Text.Trim()!="")
        {
            try
            {
                int j=Convert.ToInt32(txtNoofChap.Text.Trim());
                for (int i = 1; i <= j; i++)
                {
                    string c=string.Empty;
                    if (i < 10)
                        c = "C000" + i.ToString();
                    else if(i<100)
                        c = "C00" + i.ToString();
                    else
                        c = "C0" + i.ToString();

                    string inproc = "spInsertChapList";
                    string[,] pname = { { "@BNO", hfB_ID.Value }, { "@CpTitle", c.ToString() } };
                    int val = this.oIBSql.ExcSProcedure(inproc, pname, CommandType.StoredProcedure);
                }
                txtNoofChap.Text = "";
                DataSet ds = new DataSet();
                ds = oBook.GetChapterList(hfB_ID.Value);
                gvChapList.DataSource = ds;
                gvChapList.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
               // ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
            }
        }
    }
    protected void gvChapList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList drpChpName = e.Row.FindControl("drpChpName") as DropDownList;
            HiddenField hfgvCpno = e.Row.FindControl("hfgvCpno") as HiddenField;
            DropDownList drpStypeno = e.Row.FindControl("drpStypeno") as DropDownList;
            DataSet dsSeviceHist = oBook.getBookStage();
            drpStypeno.DataSource = dsSeviceHist;
            drpStypeno.DataTextField = dsSeviceHist.Tables[0].Columns[1].ToString();
            drpStypeno.DataValueField = dsSeviceHist.Tables[0].Columns[0].ToString();
            drpStypeno.DataBind();
            DataSet ds = new DataSet();
            ds = oBook.GetChapterListDetails(hfgvCpno.Value);
            drpChpName.Items[drpChpName.Items.IndexOf(drpChpName.Items.FindByValue(ds.Tables[0].Rows[0]["CHPNAME"].ToString()))].Selected = true;
            drpStypeno.Items[drpStypeno.Items.IndexOf(drpStypeno.Items.FindByValue(ds.Tables[0].Rows[0]["Stypeno"].ToString()))].Selected = true;
        }
    }
    protected void cmd_Save_ChapterList_Click(object sender, ImageClickEventArgs e)
    {
        if (hfB_ID.Value != "")
        {
            for (int i = 0; i < gvChapList.Rows.Count; i++)
            {
                HiddenField hfgvCpno = (HiddenField)gvChapList.Rows[i].FindControl("hfgvCpno");
                TextBox chpTitle = (TextBox)gvChapList.Rows[i].FindControl("lblgvChpTitle");
                DropDownList drpChpName = (DropDownList)gvChapList.Rows[i].FindControl("drpChpName");
                TextBox RecDate = (TextBox)gvChapList.Rows[i].FindControl("lblgvRecDate");
                TextBox DueDate = (TextBox)gvChapList.Rows[i].FindControl("lblgvDueDate");
                TextBox DesDate = (TextBox)gvChapList.Rows[i].FindControl("lblgvDesDate");
                TextBox Pages = (TextBox)gvChapList.Rows[i].FindControl("lblgvPages");
                TextBox Figures = (TextBox)gvChapList.Rows[i].FindControl("lblgvFigures");
                TextBox Equations = (TextBox)gvChapList.Rows[i].FindControl("lblgvEquations");
                TextBox Tables = (TextBox)gvChapList.Rows[i].FindControl("lblgvTables");
                DropDownList drpStypeno = (DropDownList)gvChapList.Rows[i].FindControl("drpStypeno");
                try
                {
                    string joinstring = "/";
                    string rdate = ""; string dudate = ""; string dedate = "";
                    if (RecDate.Text != "")
                    {
                        string[] rec = RecDate.Text.Split('/');
                        rdate = rec[1] + joinstring + rec[0] + joinstring + rec[2];
                    }
                    else
                    {
                        rdate = "";
                    }
                    if (DueDate.Text != "")
                    {
                        string[] due = DueDate.Text.Split('/');
                        dudate = due[1] + joinstring + due[0] + joinstring + due[2];
                    }
                    else
                    {
                        dudate = "";
                    }
                    if (DesDate.Text != "")
                    {
                        string[] des = DesDate.Text.Split('/');
                        dedate = des[1] + joinstring + des[0] + joinstring + des[2];
                    }
                    else
                    {
                        dedate = "";
                    }

                    string inproc = "spUpdateChapList";
                    string[,] pname = { { "@cpno", hfgvCpno.Value.Trim() }, { "@CHPTITLE", chpTitle.Text.Trim() }, 
                                    { "@CHPNAME", drpChpName.SelectedValue.Trim() }, { "@RECDATE", rdate.ToString().Trim() },
                                    { "@DUEDATE", dudate.ToString().Trim() }, { "@PAGES", Pages.Text.Trim() },
                                    { "@FIGURES", Figures.Text.Trim() }, { "@EQUATIONS", Equations.Text.Trim() },
                                    { "@TABLES", Tables.Text.Trim() }, { "@STYPENO", drpStypeno.SelectedValue.Trim() },
                                    { "@Desdate", dedate.ToString().Trim() }
                                  };
                    int val = this.oIBSql.ExcSProcedure(inproc, pname, CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {

                }
            }
            DataSet ds = new DataSet();
            ds = oBook.GetChapterMatterDetails(hfB_ID.Value);
            gvChapMatter.DataSource = ds;
            gvChapMatter.DataBind();
        }
    }
    protected void lnkChapterdetails_Click(object sender, EventArgs e)
    {
        if(hfB_ID.Value!="")
        {
            DataSet ds = new DataSet();
            ds = oBook.GetChapterList(hfB_ID.Value);
            gvChapList.DataSource = ds;
            gvChapList.DataBind();
            txtNoofChap.Text = "";
            ds = oBook.GetChapterMatterDetails(hfB_ID.Value);
            gvChapMatter.DataSource = ds;
            gvChapMatter.DataBind();
        }
        this.showPanel(tabChapterdetails);
    }
    protected void lnkBooklogEvents_Click(object sender, EventArgs e)
    {
        if (hfB_ID.Value != "")
        {
            DataSet ds = new DataSet();
            ds = oBook.GetBookLogEvents(hfB_ID.Value);
            gvLogEvents.DataSource = ds;
            gvLogEvents.DataBind();
        }
        else
        {
            gvLogEvents.DataSource = null;
            gvLogEvents.DataBind();
        }
        this.showPanel(tabLogEvents);
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        if (hfB_ID.Value != "")
        {
            //DataSet ds = new DataSet();
            //ds = oBook.GetChapJobHis(lblId.Text);
            //gvLogEvents.DataSource = ds;
            //gvLogEvents.DataBind();
            //lblId.Text = lblId.Text + "hi";
        }
    }

    protected void Add(object sender, EventArgs e)
    {
        popup.Show();
    }
    protected void Edit(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            DataSet dsSeviceHist = oBook.getBookStage();
            drpNextStypeno.DataSource = dsSeviceHist;
            drpNextStypeno.DataTextField = dsSeviceHist.Tables[0].Columns[1].ToString();
            drpNextStypeno.DataValueField = dsSeviceHist.Tables[0].Columns[0].ToString();
            drpNextStypeno.DataBind();
            HiddenField cpno = (HiddenField)row.Cells[0].FindControl("hfgvCpno");
            HiddenField bno = (HiddenField)row.Cells[0].FindControl("hfgvCpno");
            lblNCpno.Text = cpno.Value;
            lblNBno.Text = bno.Value;
            DataSet ds = new DataSet();
            ds = oBook.GetChapJobHis(cpno.Value);
            if (ds != null)
            {
                gvChapStageDetails.DataSource = ds;
                gvChapStageDetails.DataBind();
            }
            txtNDesDate.Text = "";
            txtNDueDate.Text = "";
            txtNRecDate.Text = "";
            txtNPages.Text = "";
            popup.Show();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string joinstring = "/";
        string rdate = ""; string dudate = ""; string dedate = "";
        if (txtNRecDate.Text != "")
        {
            string[] rec = txtNRecDate.Text.Split('/');
            rdate = rec[1] + joinstring + rec[0] + joinstring + rec[2];
        }
        else
        {
            rdate = "";
        }
        if (txtNDueDate.Text != "")
        {
            string[] due = txtNDueDate.Text.Split('/');
            dudate = due[1] + joinstring + due[0] + joinstring + due[2];
        }
        else
        {
            dudate = "";
        }
        if (txtNDesDate.Text != "")
        {
            string[] des = txtNDesDate.Text.Split('/');
            dedate = des[1] + joinstring + des[0] + joinstring + des[2];
        }
        else
        {
            dedate = "";
        }
        string inproc = "spInsertNextStageChap";
        string[,] pname = { { "@cpno", lblNCpno.Text.Trim() },  { "@RECDATE", rdate.ToString().Trim() },
                            { "@DUEDATE", dudate.ToString().Trim() }, { "@PAGES", txtNPages.Text },
                            { "@STYPENO", drpNextStypeno.SelectedValue.Trim() },{ "@Desdate", dedate.ToString().Trim() }
                          };
        int val = this.oIBSql.ExcSProcedure(inproc, pname, CommandType.StoredProcedure);
        lnkChapterdetails_Click(sender, e);
        //popup.Hide();
        //ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", "window.opener.document.getElementById(\"imgSave_Chap\").click();", false);
        //ScriptManager.RegisterStartupScript(this.Page, GetType(), "ClosePopup", "window.opener.document.getElementById(\"imgSave_Chap\").click();", true);
    }
    protected void gvChapList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            oBook.DeleteChapList(e.CommandArgument.ToString());
        }
        if (hfB_ID.Value != "")
        {
            DataSet ds = new DataSet();
            ds = oBook.GetChapterList(hfB_ID.Value);
            gvChapList.DataSource = ds;
            gvChapList.DataBind();
            txtNoofChap.Text = "";
            ds = oBook.GetChapterMatterDetails(hfB_ID.Value);
            gvChapMatter.DataSource = ds;
            gvChapMatter.DataBind();
        }
        //lnkChapterdetails_Click(sender, e);
        //ScriptManager.RegisterStartupScript(this.Page, GetType(), "ClosePopup", "window.opener.document.getElementById(\"lnkChapterdetails\").click();", true);
    }
}

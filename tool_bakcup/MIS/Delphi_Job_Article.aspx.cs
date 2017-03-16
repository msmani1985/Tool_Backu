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
using System.Globalization;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Delphi_Job_Article : System.Web.UI.Page
{
    protected int id = 1;
    private static DataSet dsGrapType = new DataSet();
    private static DataSet dsFigType = new DataSet();
    private static DataTable dtArticle = new DataTable();
    private static string sSortExpression = "";
    private Delphi_Articles oArt = new Delphi_Articles();
   // private Articles oArt1 = new Articles();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
            //Session["employeeid"] = 2432;
        }
 

        if (!IsPostBack) this.popScreen();
        else
        {
            if (oArt.IsInvoiced(hfA_ID.Value.Trim())) this.toggleSaveOption(false);
            else this.toggleSaveOption(true);
        }
        if( Convert.ToInt32(Session["employeeteamid"]) ==1)
        {
            drpAutoprocess.Enabled = true;
        }
        else
        {
            drpAutoprocess.Enabled = false;
        }
    }

 
 



    private void popScreen()
    {
        //pop customers
        DataSet dscust = oArt.getCustomers();
        
        //drpCustomerSearch.DataSource = dscust;
        //drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[2].ToString();
        //drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
        //drpCustomerSearch.DataBind();
        //drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
        //
        drpArticleJournal.Items.Insert(0, new ListItem("-- select --", "0"));
        //--
        
        drpArticlecustomer.DataSource = dscust;
        drpArticlecustomer.DataTextField = dscust.Tables[0].Columns[1].ToString();
        drpArticlecustomer.DataValueField = dscust.Tables[0].Columns[0].ToString();
        drpArticlecustomer.DataBind();
        drpArticlecustomer.Items.Insert(0, new ListItem("-- select --", "0"));
        //--
        //////lstAdvancedCustomer.DataSource = dscust;
        //////lstAdvancedCustomer.DataTextField = dscust.Tables[0].Columns[1].ToString();
        //////lstAdvancedCustomer.DataValueField = dscust.Tables[0].Columns[0].ToString();
        //////lstAdvancedCustomer.DataBind();
        //pop stages
         DataSet dsStage = oArt.getArticleStages();
        drpArticleStage.DataSource = dsStage;
        drpArticleStage.DataTextField = dsStage.Tables[0].Columns[1].ToString();
        drpArticleStage.DataValueField = dsStage.Tables[0].Columns[0].ToString();
        drpArticleStage.DataBind();
        drpArticleStage.Items.Insert(0, new ListItem("-- select --", "0"));


        drpArticleCurrentStatge.DataSource = dsStage;
        drpArticleCurrentStatge.DataTextField = dsStage.Tables[0].Columns[1].ToString();
        drpArticleCurrentStatge.DataValueField = dsStage.Tables[0].Columns[0].ToString();
        drpArticleCurrentStatge.DataBind();
        drpArticleCurrentStatge.Items.Insert(0, new ListItem("-- select --", "0"));


        DataSet dspriority = oArt.getPriority();
        drpPriority.DataSource = dspriority;
        drpPriority.DataTextField = dspriority.Tables[0].Columns[2].ToString();
        drpPriority.DataValueField = dspriority.Tables[0].Columns[0].ToString();
        drpPriority.DataBind();
        drpPriority.Items.Insert(0, new ListItem("-- select --", "0"));



        DataSet dsCurrStatus = oArt.getCurrentStatus();
        drpCurrentstatus.DataSource = dsCurrStatus;
        drpCurrentstatus.DataTextField = dsCurrStatus.Tables[0].Columns[1].ToString();
        drpCurrentstatus.DataValueField = dsCurrStatus.Tables[0].Columns[0].ToString();
        drpCurrentstatus.DataBind();
        drpCurrentstatus.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dsCatDesc = oArt.getCatDescription();
        drpArticleCategory.DataSource = dsCatDesc;
        drpArticleCategory.DataTextField = dsCatDesc.Tables[0].Columns[2].ToString();
        drpArticleCategory.DataValueField = dsCatDesc.Tables[0].Columns[0].ToString();
        drpArticleCategory.DataBind();
        drpArticleCategory.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dsProdItemType = oArt.getProdItemType();
        drpProdItemType.DataSource = dsProdItemType;
        drpProdItemType.DataTextField = dsProdItemType.Tables[0].Columns[1].ToString();
        drpProdItemType.DataValueField = dsProdItemType.Tables[0].Columns[0].ToString();
        drpProdItemType.DataBind();
        drpProdItemType.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dsProdType = oArt.getProdType();
        drpProdType.DataSource = dsProdType;
        drpProdType.DataTextField = dsProdType.Tables[0].Columns[2].ToString();
        drpProdType.DataValueField = dsProdType.Tables[0].Columns[0].ToString();
        drpProdType.DataBind();
        drpProdType.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dsNumsys = oArt.getNumSys();
        drpNumSys.DataSource = dsNumsys;
        drpNumSys.DataTextField = dsNumsys.Tables[0].Columns[1].ToString();
        drpNumSys.DataValueField = dsNumsys.Tables[0].Columns[0].ToString();
        drpNumSys.DataBind();
        drpNumSys.Items.Insert(0, new ListItem("-- select --", "0"));


        DataSet dsArtFig = oArt.getArtFig();
        dropArtFigType.DataSource = dsArtFig;
        dropArtFigType.DataTextField = dsArtFig.Tables[0].Columns[1].ToString();
        dropArtFigType.DataValueField = dsArtFig.Tables[0].Columns[0].ToString();
        dropArtFigType.DataBind();
        dropArtFigType.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dsArtImg = oArt.getArtImg();
        dropImgType.DataSource = dsArtImg;
        dropImgType.DataTextField = dsArtImg.Tables[0].Columns[1].ToString();
        dropImgType.DataValueField = dsArtImg.Tables[0].Columns[0].ToString();
        dropImgType.DataBind();
        dropImgType.Items.Insert(0, new ListItem("-- select --", "0"));
        //pop advanc search stages
        /*   DataSet dsAdvStage = oArt.getStageTypes();
             lstAdvancedStage.DataSource = dsAdvStage;
             lstAdvancedStage.DataTextField = dsAdvStage.Tables[0].Columns[1].ToString();
             lstAdvancedStage.DataValueField = dsAdvStage.Tables[0].Columns[0].ToString();
             lstAdvancedStage.DataBind();*/
        //pop graphic type
                    dsGrapType = oArt.getGraphicTypes();
                 drpGraphicType.DataSource = dsGrapType;
                 drpGraphicType.DataTextField = dsGrapType.Tables[0].Columns[1].ToString();
                 drpGraphicType.DataValueField = dsGrapType.Tables[0].Columns[0].ToString();
                 drpGraphicType.DataBind();
                 drpGraphicType.Items.Insert(0, new ListItem("-- select --", "0"));
                 //pop figure type
                 dsFigType = oArt.getFigureTypes();
                 drpGFigureType.DataSource = dsFigType;
                 drpGFigureType.DataTextField = dsFigType.Tables[0].Columns[1].ToString();
                 drpGFigureType.DataValueField = dsFigType.Tables[0].Columns[0].ToString();
                 drpGFigureType.DataBind();
                 drpGFigureType.Items.Insert(0, new ListItem("-- select --", "0"));
                 //pop doc type
                 DataSet dsDoctype = oArt.getDocTypes();
                 drpArticleDoctype.DataSource = dsDoctype;
                 drpArticleDoctype.DataTextField = dsDoctype.Tables[0].Columns[1].ToString();
                 drpArticleDoctype.DataValueField = dsDoctype.Tables[0].Columns[0].ToString();
                 drpArticleDoctype.DataBind();
                 drpArticleDoctype.Items.Insert(0, new ListItem("-- select --", "0"));
                 drpArticleSubtype.Items.Insert(0, new ListItem("-- select --", "0"));

                 DataSet dsSubDoctype = oArt.getSubDocType();
                 drpArticleSubtype.DataSource = dsSubDoctype;
                 drpArticleSubtype.DataTextField = dsSubDoctype.Tables[0].Columns[1].ToString();
                 drpArticleSubtype.DataValueField = dsSubDoctype.Tables[0].Columns[0].ToString();
                 drpArticleSubtype.DataBind();
                 drpArticleSubtype.Items.Insert(0, new ListItem("-- select --", "0"));




                 DataSet dsSalesgroups = oArt.getSalesGroup();
                 drpArticleSalesGroup.DataSource = dsSalesgroups;
                 drpArticleSalesGroup.DataTextField = dsSalesgroups.Tables[0].Columns[1].ToString();
                 drpArticleSalesGroup.DataValueField = dsSalesgroups.Tables[0].Columns[0].ToString();
                 drpArticleSalesGroup.DataBind();
                 drpArticleSalesGroup.Items.Insert(0, new ListItem("-- select --", "0"));
                 
                 //pop categories
                 ////DataSet dsCattype = oArt.getCategoryTypes();
                 ////drpArticleCategory.DataSource = dsCattype;
                 ////drpArticleCategory.DataTextField = dsCattype.Tables[0].Columns[2].ToString();
                 ////drpArticleCategory.DataValueField = dsCattype.Tables[0].Columns[0].ToString();
                 ////drpArticleCategory.DataBind();
                 ////drpArticleCategory.Items.Insert(0, new ListItem("-- select --", "0"));
                 //pop OnHold type
                 DataSet dsHoldtyp = oArt.getOnHoldTypes();
                 drpArticleOnHoldType.DataSource = dsHoldtyp;
                 drpArticleOnHoldType.DataTextField = dsHoldtyp.Tables[0].Columns[1].ToString();
                 drpArticleOnHoldType.DataValueField = dsHoldtyp.Tables[0].Columns[0].ToString();
                 drpArticleOnHoldType.DataBind();
                 drpArticleOnHoldType.Items.Insert(0, new ListItem("-- select --", "0"));
                 // pop sales job group
              /*   DataSet dsSalesGrp = oArt.getSalesGroup();
                 drpArticleSalesGroup.DataSource = dsSalesGrp;
                 drpArticleSalesGroup.DataTextField = dsSalesGrp.Tables[0].Columns[1].ToString();
                 drpArticleSalesGroup.DataValueField = dsSalesGrp.Tables[0].Columns[0].ToString();
                 drpArticleSalesGroup.DataBind();
                 drpArticleSalesGroup.Items.Insert(0, new ListItem("-- select --", "0"));
                 txtArticleNoofImages.Attributes.Add("readonly", "readonly");
                 txtArticleSdate.Attributes.Add("readonly", "readonly");
                 txtArticleDdate.Attributes.Add("readonly", "readonly");
                 txtArticleActDdate.Attributes.Add("readonly", "readonly");
                 txtArticleCDdate.Attributes.Add("readonly", "readonly");
                 txtArticleIssueNo.Attributes.Add("readonly", "readonly");
                 txtArticleSequence.Attributes.Add("readonly", "readonly");
                 txtArticleNoofImages.Attributes.Add("readonly", "readonly");
                 txtAdvRecDate1.Attributes.Add("readonly", "readonly");
                 txtAdvRecDate2.Attributes.Add("readonly", "readonly");
                 txtAdvDueDate1.Attributes.Add("readonly", "readonly");
                 txtAdvDueDate2.Attributes.Add("readonly", "readonly");
                 txtAdvHlfDueDate1.Attributes.Add("readonly", "readonly");
                 txtAdvHlfDueDate2.Attributes.Add("readonly", "readonly");
                 txtAdvCatsDueDate1.Attributes.Add("readonly", "readonly");
                 txtAdvCatsDueDate2.Attributes.Add("readonly", "readonly");
                 if (txtArticleNoofImages.Text == "") txtArticleNoofImages.Text = "0";
                 if (Session["despatchgroup"] == null || Session["despatchgroup"].ToString() != "true")
                 {
                     chkArticleDespatch.Visible = false;
                 }
                */
        //show 1st panel
        if (Request.QueryString["q"] != null &&
            Request.QueryString["q"].ToString().Trim() != "")
        {
            string pageQuery = Request.QueryString["q"].ToString().Trim();
            switch (pageQuery)
            {
                case "new_article":
                    imgAD_stdate.Visible = false;
                    imgAD_dudate.Visible = false;
                   // imgAD_hdudate.Visible = false;
                    imgAD_cdudate.Visible = false;
                   ////////////////////////////////////////////////////// chkArticleDespatch.Enabled = false;
                    this.showPanel(tabArticleDetails);
                    break;
            }
        }
        else this.showPanel(tabGeneral);
        txtSearch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) ||(event.keyCode == 13)){document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ");
    }
    private void popDefaultArticleValues()
    {
        drpArticlecustomer.Enabled = true;
        drpArticleJournal.Enabled = true;
        txtArticleMnsID.Enabled = true;
        lblArticleHeader.Text = "New Article";
        imgArticleHeader.Src = "images/tools/new.png";
        drpArticleDoctype.ClearSelection();
        if (drpArticleDoctype.Items.FindByValue("15") != null)
        {
            drpArticleDoctype.Items.FindByValue("15").Selected = true;
            drpArticleDoctype_SelectedIndexChanged(null, null);
            drpArticleSubtype.ClearSelection();
            if (drpArticleSubtype.Items.FindByValue("15") != null)
                drpArticleSubtype.Items.FindByValue("15").Selected = true;
        }
        ////////////////////////////drpArticleCategory.ClearSelection();
        ////////////////////////////if (drpArticleCategory.Items.FindByValue("3") != null)
        //////////////////////////// drpArticleCategory.Items.FindByValue("3").Selected = true;
        drpArticleStage.ClearSelection();
        //if (drpArticleStage.Items.FindByValue("1") != null)
        //   drpArticleStage.Items.FindByValue("1").Selected = true;
        if (hfA_ID.Value.Trim() == "" && drpArticleStage.Items[1] != null) drpArticleStage.Items[1].Selected = true;
        drpArticleStage_SelectedIndexChanged(null, null);
    }
    protected void drpArticleDoctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpArticleSubtype.Items.Clear();
        if (drpArticleDoctype.SelectedItem.Value != "0")
        {
            DataSet dsDoc = oArt.getDocItemTypes(drpArticleDoctype.SelectedItem.Value.Trim());
            drpArticleSubtype.DataSource = dsDoc;
            drpArticleSubtype.DataTextField = dsDoc.Tables[0].Columns[1].ToString();
            drpArticleSubtype.DataValueField = dsDoc.Tables[0].Columns[0].ToString();
            drpArticleSubtype.DataBind();
        }
        drpArticleSubtype.Items.Insert(0, new ListItem("-- select --", "0"));
    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabGeneral":
                if (this.hfA_Name.Value != "")
                    lblArticleSummary.Text = "Article : " + this.hfA_Journal.Value.Trim() + this.hfA_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "current");
                miArticleDetails.Attributes.Add("class", "");
                miArticleEvents.Attributes.Add("class", "");
                miGraphics.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "");
                miHoldHistory.Attributes.Add("class", "");
                miInvoiceSetup.Attributes.Add("class", "");
                this.tabGeneral.Visible = true;
                this.tabArticleDetails.Visible = false;
                this.tabArticleEvents.Visible = false;
                this.tabGraphics.Visible = false;
                this.tabComments.Visible = false;
                this.tabHoldHistory.Visible = false;
                this.tabArtWork.Visible = false;
                this.tabInvoiceSetup.Visible = false;
                miArtWork.Attributes.Add("class", "");
                break;
            case "tabArticleDetails":
                if (this.hfA_Name.Value != "")
                    lblArticleHeader.Text = "Edit Article : " + this.hfA_Journal.Value.Trim() + this.hfA_Name.Value.Trim();
                else this.popDefaultArticleValues();
                miGeneral.Attributes.Add("class", "");
                miArticleDetails.Attributes.Add("class", "current");
                miArticleEvents.Attributes.Add("class", "");
                miGraphics.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "");
                miHoldHistory.Attributes.Add("class", "");
                miInvoiceSetup.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabArticleDetails.Visible = true;
                this.tabArticleEvents.Visible = false;
                this.tabGraphics.Visible = false;
                this.tabComments.Visible = false;
                this.tabHoldHistory.Visible = false;
                this.tabArtWork.Visible = false;
                this.tabInvoiceSetup.Visible = false;
                
                miArtWork.Attributes.Add("class", "");
                break;
            case "tabArticleEvents":
                if (this.hfA_Name.Value != "")
                    lblEventsHeader.Text = "Logged Events : " + this.hfA_Journal.Value.Trim() + this.hfA_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miArticleDetails.Attributes.Add("class", "");
                miArticleEvents.Attributes.Add("class", "current");
                miGraphics.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "");
                miHoldHistory.Attributes.Add("class", "");
                miInvoiceSetup.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabArticleDetails.Visible = false;
                this.tabArticleEvents.Visible = true;
                this.tabGraphics.Visible = false;
                this.tabComments.Visible = false;
                this.tabHoldHistory.Visible = false;
                this.tabArtWork.Visible = false;
                this.tabInvoiceSetup.Visible = false;
                miArtWork.Attributes.Add("class", "");
                break;
            case "tabGraphics":
                if (this.hfA_Name.Value != "")
                    lblGraphicHeader.Text = "Graphic Details : " + this.hfA_Journal.Value.Trim() + this.hfA_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miArticleDetails.Attributes.Add("class", "");
                miArticleEvents.Attributes.Add("class", "");
                miGraphics.Attributes.Add("class", "current");
                miComments.Attributes.Add("class", "");
                miHoldHistory.Attributes.Add("class", "");
                miInvoiceSetup.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabArticleDetails.Visible = false;
                this.tabArticleEvents.Visible = false;
                this.tabGraphics.Visible = true;
                this.tabComments.Visible = false;
                this.tabHoldHistory.Visible = false;
                this.tabArtWork.Visible = false;
                this.tabInvoiceSetup.Visible = false;
                miArtWork.Attributes.Add("class", "");
                break;
            case "tabComments":
                if (this.hfA_Name.Value != "")
                    lblCommentsHeader.Text = "Comments History : " + this.hfA_Journal.Value.Trim() + this.hfA_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miArticleDetails.Attributes.Add("class", "");
                miArticleEvents.Attributes.Add("class", "");
                miGraphics.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "current");
                miHoldHistory.Attributes.Add("class", "");
                miInvoiceSetup.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabArticleDetails.Visible = false;
                this.tabArticleEvents.Visible = false;
                this.tabGraphics.Visible = false;
                this.tabComments.Visible = true;
                this.tabHoldHistory.Visible = false;
                this.tabArtWork.Visible = false;
                this.tabInvoiceSetup.Visible = false;
                miArtWork.Attributes.Add("class", "");
                break;
            case "tabArtWork":
                if (this.hfA_Name.Value != "")
                    lblArtWorkHeader.Text = "Art Work : " + this.hfA_Journal.Value.Trim() + this.hfA_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miArticleDetails.Attributes.Add("class", "");
                miArticleEvents.Attributes.Add("class", "");
                miGraphics.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "");
                miHoldHistory.Attributes.Add("class", "");
                miArtWork.Attributes.Add("class", "current");
                miInvoiceSetup.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabArticleDetails.Visible = false;
                this.tabArticleEvents.Visible = false;
                this.tabGraphics.Visible = false;
                this.tabComments.Visible = false;
                this.tabHoldHistory.Visible = false;
                this.tabArtWork.Visible = true;
                this.tabInvoiceSetup.Visible = false;
                miArtWork.Attributes.Add("class", "current");
                break;

            case "tabInvoiceSetup":
                if (this.hfA_Name.Value != "")
                    lblArtWorkHeader.Text = "Art Work : " + this.hfA_Journal.Value.Trim() + this.hfA_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miArticleDetails.Attributes.Add("class", "");
                miArticleEvents.Attributes.Add("class", "");
                miGraphics.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "");
                miHoldHistory.Attributes.Add("class", "");
                miArtWork.Attributes.Add("class", "");
                miInvoiceSetup.Attributes.Add("class", "current");
                this.tabGeneral.Visible = false;
                this.tabArticleDetails.Visible = false;
                this.tabArticleEvents.Visible = false;
                this.tabGraphics.Visible = false;
                this.tabComments.Visible = false;
                this.tabHoldHistory.Visible = false;
                this.tabArtWork.Visible = false;
                this.tabInvoiceSetup.Visible = true;
                miInvoiceSetup.Attributes.Add("class", "current");
                break;
            default:
                if (this.hfA_Name.Value != "")
                    lblCommentsHeader.Text = "Hold History : " + this.hfA_Journal.Value.Trim() + this.hfA_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miArticleDetails.Attributes.Add("class", "");
                miArticleEvents.Attributes.Add("class", "");
                miGraphics.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "");
                miHoldHistory.Attributes.Add("class", "current");
                this.tabGeneral.Visible = false;
                this.tabArticleDetails.Visible = false;
                this.tabArticleEvents.Visible = false;
                this.tabGraphics.Visible = false;
                this.tabComments.Visible = false;
                this.tabHoldHistory.Visible = true;
                this.tabArtWork.Visible = false;
                miArtWork.Attributes.Add("class", "");
                break;
        }
    }
    protected void lnkGeneral_Click(object sender, EventArgs e)
    {
        this.showPanel(tabGeneral);
    }
    protected void lnkArticledetails_Click(object sender, EventArgs e)
    {
        if (hfA_ID.Value != "")
        {
            loadArticleDetails(hfA_ID.Value.Trim());
        }
        this.showPanel(tabArticleDetails);
    }
    protected void lnkInvoicesetup_Click(object sender, EventArgs e)
    {
        //if (hfA_ID.Value != "")
        //{
        //    loadArticleDetails(hfA_ID.Value.Trim());
        //}
        this.showPanel(tabInvoiceSetup);
    }

    private void loadArticleDetails(string sArticleID)
    {
        sArticleID = sArticleID.Trim();
        if (sArticleID != "")
        {
            drpArticlecustomer.Enabled = false;
            drpArticleJournal.Enabled = false;
            txtArticleMnsID.Enabled = false;
            txtArticleSdate.Text = "";
            txtArticleDdate.Text = "";
            txtArticleActDdate.Text = "";
            txtArticleCDdate.Text = "";
            imgAD_stdate.Visible = false;
            imgAD_dudate.Visible = false;
          //  imgAD_hdudate.Visible = false;
            imgAD_cdudate.Visible = false;
            //pop details
            DataSet dsArticle = oArt.getArticleDetailsByID(sArticleID);
            lblArticleHeader.Text = "Edit Article";
            imgArticleHeader.Src = "images/tools/edit.png";
            DataRow row = dsArticle.Tables[0].Rows[0];
            if (row["invno"].ToString().Trim() != "") this.toggleSaveOption(false);
            else this.toggleSaveOption(true);
            drpArticlecustomer.ClearSelection();
            if (drpArticlecustomer.Items.FindByValue(row["custno"].ToString().Trim()) != null)
                drpArticlecustomer.Items.FindByValue(row["custno"].ToString().Trim()).Selected = true;
            drpArticlecustomer_SelectedIndexChanged(null, null);
            if (drpArticleJournal.Items.FindByValue(row["journo"].ToString().Trim()) != null)
                drpArticleJournal.Items.FindByValue(row["journo"].ToString().Trim()).Selected = true;
            txtArticleMnsID.Text = row["AMANUSCRIPTID"].ToString().Trim();
            txtArticleTitle.Text = row["AMNSTITLE"].ToString().Trim();
            txtArticleAuthorCorr.Text = row["ACORRESPONDINGAUTHOR"].ToString().Trim();
            txtArticleAuthEmail.Text = row["AEMAIL"].ToString().Trim();
            txtArticleNoofAuth.Text = row["no_authors"].ToString().Trim();
            if (row["AEXTRA_COPY_EDIT"].ToString().Trim() == "True")
                chkArticleRegCopyedit.Checked = true;
            else chkArticleRegCopyedit.Checked = false;
            if (row["onhold_history_id"].ToString().Trim() != "")
                chkArticlOnHold.Checked = true;
            else chkArticlOnHold.Checked = false;
            drpArticleDoctype.ClearSelection();
            if (drpArticleDoctype.Items.FindByValue(row["document_type_id"].ToString().Trim()) != null)
                drpArticleDoctype.Items.FindByValue(row["document_type_id"].ToString().Trim()).Selected = true;
            drpArticleDoctype_SelectedIndexChanged(null, null);
            if (drpArticleSubtype.Items.FindByValue(row["document_item_type_id"].ToString().Trim()) != null)
                drpArticleSubtype.Items.FindByValue(row["document_item_type_id"].ToString().Trim()).Selected = true;
            drpArticleCategory.ClearSelection();
            if (drpArticleCategory.Items.FindByValue(row["catno"].ToString().Trim()) != null)
                drpArticleCategory.Items.FindByValue(row["catno"].ToString().Trim()).Selected = true;
            drpArticleSalesGroup.ClearSelection();
            if (drpArticleSalesGroup.Items.FindByValue(row["sales_job_group_id"].ToString().Trim()) != null)
                drpArticleSalesGroup.Items.FindByValue(row["sales_job_group_id"].ToString().Trim()).Selected = true;
            txtArticleIssueNo.Text = row["iissueno"].ToString().Trim();
            txtArticleSequence.Text = row["sequence"].ToString().Trim();
            txtArticleNoofImages.Text = row["no_figures"].ToString().Trim();
            txtArticleMspages.Text = row["ms_pages"].ToString().Trim();
            txtArticlePrintpages.Text = row["print_pages"].ToString().Trim();
            txtArtMsRecDate.Text = row["ms_received_date"].ToString().Trim().Equals("") ? "" : DateTime.Parse(row["ms_received_date"].ToString().Trim()).ToString("MM/dd/yyyy");
            txtArtMsRevDate.Text = row["ms_revised_date"].ToString().Trim().Equals("") ? "" : DateTime.Parse(row["ms_revised_date"].ToString().Trim()).ToString("MM/dd/yyyy");
            txtArtMsAcceptDate.Text = row["ms_accepted_date"].ToString().Trim().Equals("") ? "" : DateTime.Parse(row["ms_accepted_date"].ToString().Trim()).ToString("MM/dd/yyyy");
            txtDOINo.Text = row["DOINO"].ToString().Trim();
            txtArticleComments.Text = row["comments"].ToString().Trim();
            txt_SamAuthorQuery.Text = row["sam_author_query"].ToString().Trim();
            txt_FigureQuery.Text = row["figure_correction"].ToString().Trim();
            txtinterviewdate.Text = (row["sanlucas_interviewdate"] != null && row["sanlucas_interviewdate"].ToString() != "") ? Convert.ToDateTime(row["sanlucas_interviewdate"].ToString()).ToShortDateString() : "";
            txtphoneno.Text = row["sanlucas_phoneno"].ToString().Trim();
            txtfaxno.Text = row["sanlucas_faxno"].ToString().Trim();
            txtinterviewtime.Text = row["sanlucas_interviewtime"].ToString().Trim();
            txt_meetingplace.Text = row["sanlucas_meetingplace"].ToString().Trim();
            txt_meetingtime.Text = row["sanlucas_meetingtime"].ToString().Trim();
            txt_country.Text = row["sanlucas_country"].ToString().Trim();
            txt_appointmentdate1.Text = (row["sanlucas_appointmentdatestart"] != null && row["sanlucas_appointmentdatestart"].ToString() != "") ? Convert.ToDateTime(row["sanlucas_appointmentdatestart"].ToString().Trim()).ToShortDateString() : "";
            txt_appointmentdate2.Text = (row["sanlucas_appointmentdateend"] != null && row["sanlucas_appointmentdateend"].ToString() != "") ? Convert.ToDateTime(row["sanlucas_appointmentdateend"].ToString().Trim()).ToShortDateString() : "";
            txt_zone.Text = row["sanlucas_zone"].ToString().Trim();

            drpArticleStage.ClearSelection();
            //drpArticleStage.DataSource = dsArticle.Tables[1];
            //drpArticleStage.DataTextField = dsArticle.Tables[1].Columns["job_stage_name"].ToString();
            //drpArticleStage.DataValueField = dsArticle.Tables[1].Columns["job_stage_id"].ToString();
            //drpArticleStage.DataBind();
            //drpArticleStage.Items.Insert(0, new ListItem("-- select --", "0"));
            chkArticleDespatch.Checked = false;
            txtResearchPage.Text = row["ResearchPage"].ToString().Trim();
            txtArtReprocess.Text = row["ArtReprocess"].ToString().Trim();
            txtArtReprocess.Text = row["ArtReprocess"].ToString().Trim();

 

            if (drpAutoprocess.Items.FindByValue(row["Autoprocess"].ToString().Trim()) != null)
                drpAutoprocess.Items.FindByValue(row["Autoprocess"].ToString().Trim()).Selected = true;



            drpArticleStage.ClearSelection();

            if (drpArticleStage.Items.FindByValue(row["STYPENO"].ToString().Trim()) != null)
                drpArticleStage.Items.FindByValue(row["STYPENO"].ToString().Trim()).Selected = true;
            drpPriority.ClearSelection();
            if (drpPriority.Items.FindByValue(row["ARPNO"].ToString().Trim()) != null) //Priority_id
                drpPriority.Items.FindByValue(row["ARPNO"].ToString().Trim()).Selected = true;//Priority_id

            drpCurrentstatus.ClearSelection();
            if (drpCurrentstatus.Items.FindByValue(row["STNO"].ToString().Trim()) != null) //current status
                drpCurrentstatus.Items.FindByValue(row["STNO"].ToString().Trim()).Selected = true; //current status
            txtArtwork.Text = row["AARTWORKPIECES"].ToString().Trim();
            txtnoFolios.Text = row["ANOOFFOLIOS"].ToString().Trim();
            txtActNoPages.Text = row["AREALNOOFPAGES"].ToString().Trim();
            txtnoofproofs.Text = row["ANOPROOFS"].ToString().Trim();
            txtPageFrom.Text = row["APAGENOFROM"].ToString().Trim();
            txtPageto.Text = row["APAGENOTO"].ToString().Trim();
            drpWithdraw.SelectedValue = row["InvStatus"].ToString();
            if (drpAutoprocess.Items.FindByValue(row["Autoprocess"].ToString().Trim()) != null)
            {
                drpAutoprocess.SelectedValue = row["Autoprocess"].ToString();
            }else
            {
                drpAutoprocess.SelectedValue = "0";
            }
            if (dsArticle.Tables[1].Rows[0] != null)
            {
                if (dsArticle.Tables[1].Rows[0]["despatch_date"].ToString().Trim() == "")
                {
                    //txtArticleSdate.Text = dsArticle.Tables[2].Rows[0]["received_date"].ToString().Trim();
                    //txtArticleDdate.Text = dsArticle.Tables[2].Rows[0]["due_date"].ToString().Trim();
                    //txtArticleActDdate.Text = dsArticle.Tables[2].Rows[0]["half_due_date"].ToString().Trim();
                    //txtArticleCDdate.Text = dsArticle.Tables[2].Rows[0]["cats_due_date"].ToString().Trim();
                    chkArticleDespatch.Enabled = true;
                }
                else chkArticleDespatch.Enabled = false;
            }
            else
            {
                txtArticleSdate.Text = "";
                txtArticleDdate.Text = "";
                txtArticleActDdate.Text = "";
                txtArticleCDdate.Text = "";
                chkArticleDespatch.Enabled = false;
            }
            dgrdArticleStages.DataSource = dsArticle.Tables[1];
            dgrdArticleStages.DataBind();
        }
    }


    

    private void toggleSaveOption(bool IsVisible)
    {
        if (IsVisible)
        {
            cmd_Save_Article.Visible = true;
            cmdGD_Save.Visible = true;
            imgbtnGraphicsAdd.Visible = true;
            chkArticlOnHold.Enabled = true;
        }
        else
        {
            cmd_Save_Article.Visible = false;
            cmdGD_Save.Visible = false;
            imgbtnGraphicsAdd.Visible = false;
            chkArticlOnHold.Enabled = false;
        }

    }
    protected void drpArticleJournal_SelectedIndexChanged(object sender, EventArgs e)
    {
        //drpArticleStage.Items.Clear();
        //if (drpArticleJournal.SelectedItem.Value != "0")
        //{
        //    DataSet dsStag = oArt.getArticleStagesByJournal(drpArticleJournal.SelectedItem.Value.Trim());
        //    drpArticleStage.DataSource = dsStag.Tables[0];
        //    drpArticleStage.DataTextField = dsStag.Tables[0].Columns["job_stage_name"].ToString();
        //    drpArticleStage.DataValueField = dsStag.Tables[0].Columns["qualify_job_stage_id"].ToString();
        //    drpArticleStage.DataBind();
        //}
        //drpArticleStage.Items.Insert(0, new ListItem("-- select --", "0"));
        //if (hfA_ID.Value.Trim() == "" && drpArticleStage.Items.Count > 1) drpArticleStage.Items[1].Selected = true;
    }
    protected void lnkArticleEvents_Click(object sender, EventArgs e)
    {
        if (hfA_ID.Value != "")
        {
            DataSet dsEvents = oArt.getArticleEvents(hfA_ID.Value.Trim());
            gvEvents.DataSource = dsEvents.Tables[0];
            gvEvents.DataBind();
        }
        this.showPanel(tabArticleEvents);
    }
    protected void lnkGraphics_Click(object sender, EventArgs e)
    {
        if (hfA_ID.Value != "")
        {
            loadGraphicsDetails(hfA_ID.Value.Trim());
        }
        this.showPanel(tabGraphics);
    }
    protected void lnkComments_Click(object sender, EventArgs e)
    {
        if (hfA_ID.Value != "")
        {
            DataSet dsComment = oArt.getArticleComments(hfA_ID.Value.Trim());
            gvCommHistory.DataSource = dsComment.Tables[0];
            gvCommHistory.DataBind();
        }
        this.showPanel(tabComments);
    }
    protected void lnkHoldHistory_Click(object sender, EventArgs e)
    {
        if (hfA_ID.Value != "")
        {
            DataSet dsComment = oArt.getArticleHoldHistory(hfA_ID.Value.Trim());
            gvHoldHistory.DataSource = dsComment.Tables[0];
            gvHoldHistory.DataBind();
        }
        this.showPanel(tabHoldHistory);
    }

    private void loadGraphicsDetails(string sArticleID)
    {
        sArticleID = sArticleID.Trim();
        if (sArticleID != "")
        {
            gvGraphicsDetails.DataSource = oArt.getGraphicDetails(sArticleID);
            gvGraphicsDetails.DataBind();
            gvGraphicsDetails.Visible = true;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        sSortExpression = "";
        ////////////if (Request["__EVENTARGUMENT"] != null
        ////////////    && Request["__EVENTARGUMENT"].ToLower() == "advanced")
        ////////////{
        ////////////    char IsHold = 'N';
        ////////////    string[] aArtSearch = new string[26];
        ////////////    if (chkAdvOnHold.Checked) IsHold = 'Y';
        ////////////    txtAdvJourCode.Text = txtAdvJourCode.Text.ToUpper();
        ////////////    aArtSearch[0] = "";
        ////////////    aArtSearch[1] = "";
        ////////////    aArtSearch[2] = "";
        ////////////    aArtSearch[3] = "advanced";
        ////////////    aArtSearch[4] = rblstAdvCompleted.SelectedItem.Value;
        ////////////    aArtSearch[5] = txtAdvJourCode.Text.Trim();
        ////////////    aArtSearch[6] = drpAdvJourCodeExp.SelectedItem.Value;
        ////////////    aArtSearch[7] = txtAdvArtCode.Text.Trim();
        ////////////    aArtSearch[8] = drpAdvArtCodeExp.SelectedItem.Value;
        ////////////    aArtSearch[9] = txtAdvIssueNum.Text.Trim();
        ////////////    aArtSearch[10] = drpAdvIssueNumExp.SelectedItem.Value;
        ////////////    aArtSearch[11] = IsHold.ToString();
        ////////////    aArtSearch[12] = drpAdvRecExpr.SelectedItem.Value;
        ////////////    aArtSearch[13] = txtAdvRecDate1.Text.Trim();
        ////////////    aArtSearch[14] = txtAdvRecDate2.Text.Trim();
        ////////////    aArtSearch[15] = drpAdvDueExpr.SelectedItem.Value;
        ////////////    aArtSearch[16] = txtAdvDueDate1.Text.Trim();
        ////////////    aArtSearch[17] = txtAdvDueDate2.Text.Trim();
        ////////////    aArtSearch[18] = drpAdvHlfDueRecExpr.SelectedItem.Value;
        ////////////    aArtSearch[19] = txtAdvHlfDueDate1.Text.Trim();
        ////////////    aArtSearch[20] = txtAdvHlfDueDate2.Text.Trim();
        ////////////    aArtSearch[21] = drpAdvCatsDueExpr.SelectedItem.Value;
        ////////////    aArtSearch[22] = txtAdvCatsDueDate1.Text.Trim();
        ////////////    aArtSearch[23] = txtAdvCatsDueDate2.Text.Trim();
        ////////////    string sStageIDs = "";
        ////////////    for (int x = 0; x < lstAdvancedStage.Items.Count; x++)
        ////////////    {
        ////////////        if (lstAdvancedStage.Items[x].Selected)
        ////////////            sStageIDs += lstAdvancedStage.Items[x].Value + ",";
        ////////////    }
        ////////////    sStageIDs = sStageIDs.TrimEnd(',');
        ////////////    string sCustIDs = "";
        ////////////    for (int y = 0; y < lstAdvancedCustomer.Items.Count; y++)
        ////////////    {
        ////////////        if (lstAdvancedCustomer.Items[y].Selected)
        ////////////            sCustIDs += lstAdvancedCustomer.Items[y].Value + ",";
        ////////////    }
        ////////////    sCustIDs = sCustIDs.TrimEnd(',');
        ////////////    aArtSearch[24] = sStageIDs;
        ////////////    aArtSearch[25] = sCustIDs;
        ////////////    DataSet dsa = oArt.getArticles(aArtSearch);
        ////////////    dtArticle = dsa.Tables[0].Copy();
        ////////////    gvArticles.DataSource = dsa;
        ////////////    gvArticles.DataBind();
        ////////////    this.hfA_ID.Value = "";
        ////////////    this.hfA_Name.Value = "";
        ////////////    this.showPanel(tabGeneral);
        ////////////}
        ////////////else
        ////////////{
            txtSearch.Text = txtSearch.Text.Trim().ToUpper();
            //if (txtSearch.Text != "")
            //{
                char CompleteFlag = 'N';
                DataSet dsa = null;
                if (chkViewCompleted.Checked)
                {
                    if (txtSearch.Text.Trim() == "")
                    {
                        CompleteFlag = 'Y';
                        if (rblstArticleType.SelectedItem.Value == "0")
                            dsa = oArt.getArticles(txtSearch.Text.Trim().ToUpper(), "interior", CompleteFlag, DDMonthList.SelectedValue, DDYearList.SelectedValue);
                        else
                            dsa = oArt.getArticles(txtSearch.Text.Trim().ToUpper(), "", CompleteFlag, DDMonthList.SelectedValue, DDYearList.SelectedValue);
                    }
                    else
                    {
                        CompleteFlag = 'Y';
                        if (rblstArticleType.SelectedItem.Value == "0")
                            dsa = oArt.getArticles(txtSearch.Text.Trim().ToUpper(), "interior", CompleteFlag);
                        else
                            dsa = oArt.getArticles(txtSearch.Text.Trim().ToUpper(), "", CompleteFlag);
                    }
                }
                else
                {
                    if (rblstArticleType.SelectedItem.Value == "0")
                        dsa = oArt.getArticles(txtSearch.Text.Trim().ToUpper(), "interior", CompleteFlag);
                    else
                        dsa = oArt.getArticles(txtSearch.Text.Trim().ToUpper(), "", CompleteFlag);
                }
                dtArticle = dsa.Tables[0].Copy();
                Session["ArtcleDetails"] = dtArticle;
                gvArticles.DataSource = dsa;
                gvArticles.DataBind();
                this.hfA_ID.Value = "";
                this.hfA_Name.Value = "";
                this.showPanel(tabGeneral);
                lblArticleSummary.Text = "Search Summary";
            //}
            //else Alert("Please enter an Article No. or Manuscript ID or Journal Code!");
         
    }
    protected void gvArticles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,'" + ((HiddenField)e.Row.FindControl("hfgvArticleID")).Value.Trim() + "','" + ((HiddenField)e.Row.FindControl("hfgvArticleName")).Value.Trim() + "','" + ((HiddenField)e.Row.FindControl("hfgvJournal")).Value.Trim() + "');";
            e.Row.Attributes["ondblclick"] =
                "javascript:__doPostBack('lnkArticledetails','')";
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
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected DataSet loadGraphicType() { if (dsGrapType == null)oArt.getGraphicTypes(); return dsGrapType; }
    protected DataSet loadFigureType() { if (dsFigType == null)oArt.getFigureTypes(); return dsFigType; }
    protected void gvGraphicsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)e.Row.FindControl("lblGD_i_draphic_desc")).Text.Trim() == "")
            {
                ((Label)e.Row.FindControl("lblGD_i_figtype")).Visible = false;
                ((DropDownList)e.Row.FindControl("drpGD_i_figtype")).Visible = true;
                ((Label)e.Row.FindControl("lblGD_i_graphicname")).Visible = false;
                ((Label)e.Row.FindControl("lblGD_i_graphictype")).Visible = false;
                ((Label)e.Row.FindControl("lblGD_i_draphic_desc")).Visible = false;
                ((TextBox)e.Row.FindControl("txtGD_i_graphicname")).Visible = true;
                ((DropDownList)e.Row.FindControl("drpGD_i_graphictype")).Visible = true;
                ((TextBox)e.Row.FindControl("txtGD_i_Graphicdesc")).Visible = true;
                ((Label)e.Row.FindControl("lblGD_i_IsRedraw")).Visible = false;
                ((DropDownList)e.Row.FindControl("drpGD_i_Redraw")).Visible = true;
            }
            else
            {
                ((Label)e.Row.FindControl("lblGD_i_figtype")).Visible = true;
                ((DropDownList)e.Row.FindControl("drpGD_i_figtype")).Visible = false;
                ((Label)e.Row.FindControl("lblGD_i_graphicname")).Visible = true;
                ((Label)e.Row.FindControl("lblGD_i_graphictype")).Visible = true;
                ((Label)e.Row.FindControl("lblGD_i_draphic_desc")).Visible = true;
                ((TextBox)e.Row.FindControl("txtGD_i_graphicname")).Visible = false;
                ((DropDownList)e.Row.FindControl("drpGD_i_graphictype")).Visible = false;
                ((TextBox)e.Row.FindControl("txtGD_i_Graphicdesc")).Visible = false;
                ((Label)e.Row.FindControl("lblGD_i_IsRedraw")).Visible = true;
                ((DropDownList)e.Row.FindControl("drpGD_i_Redraw")).Visible = false;
            }
        }
    }
    protected void imgbtnGraphicsAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfA_ID.Value.Trim() != "" && drpGraphicType.SelectedItem.Value != "0" && int.Parse(txtGraphicCount.Text.Trim()) > 0)
            {
                this.oArt.InsertEmptyGraphics(hfA_ID.Value.Trim(), "5", drpGraphicType.SelectedItem.Value.Trim(), int.Parse(txtGraphicCount.Text.Trim()), drpGFigureType.SelectedItem.Value.Trim());
                this.loadGraphicsDetails(hfA_ID.Value);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void imbtnGraghicEdit_Click(object sender, ImageClickEventArgs e)
    {
        foreach (GridViewRow ro in gvGraphicsDetails.Rows)
        {
            if (((CheckBox)ro.FindControl("chkGD_i_editordelete")).Checked)
            {
                ((Label)ro.FindControl("lblGD_i_figtype")).Visible = false;
                ((DropDownList)ro.FindControl("drpGD_i_figtype")).Visible = true;
                ((Label)ro.FindControl("lblGD_i_graphicname")).Visible = false;
                ((Label)ro.FindControl("lblGD_i_graphictype")).Visible = false;
                ((Label)ro.FindControl("lblGD_i_draphic_desc")).Visible = false;
                ((TextBox)ro.FindControl("txtGD_i_graphicname")).Visible = true;
                ((DropDownList)ro.FindControl("drpGD_i_graphictype")).Visible = true;
                ((TextBox)ro.FindControl("txtGD_i_Graphicdesc")).Visible = true;
                ((Label)ro.FindControl("lblGD_i_IsRedraw")).Visible = false;
                ((DropDownList)ro.FindControl("drpGD_i_Redraw")).Visible = true;
            }
        }
    }
    protected void imgbtn_GD_delete_Click(object sender, ImageClickEventArgs e)
    {
        string sGraphic_ids = "";
        try
        {
            foreach (GridViewRow ro in gvGraphicsDetails.Rows)
            {
                if (((CheckBox)ro.FindControl("chkGD_i_editordelete")).Checked)
                {
                    sGraphic_ids += ((HiddenField)ro.FindControl("hfGD_GraphicId")).Value.Trim() + ",";
                }
            }
            sGraphic_ids = sGraphic_ids.TrimEnd(',');
            if (sGraphic_ids != "" && oArt.DeleteGraphics(sGraphic_ids) == "true")
            {
                this.loadGraphicsDetails(hfA_ID.Value);
                Alert("Successfully Deleted.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void cmd_New_Article_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Delphi_job_article.aspx?q=new_article", true);
    }
    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (drpArticlecustomer.SelectedItem.Value == "0") sMessage += i++ + ". Select a Customer\\r\\n";
        if (drpArticleJournal.SelectedItem.Value == "0") sMessage += i++ + ". Select a Journal\\r\\n";
        if (txtArticleMnsID.Text.Trim() == "") sMessage += i++ + ". Enter Mns. ID\\r\\n";
        if (drpArticleDoctype.SelectedItem.Value == "0") sMessage += i++ + ". Select a Doc Type\\r\\n";
        if (drpArticleSubtype.SelectedItem.Value == "0") sMessage += i++ + ". Select a Sub Type\\r\\n";
        if (hfA_ID.Value.Trim() == "")
        {
            if (drpArticleStage.SelectedItem.Value == "0")/* ||
            txtArticleSdate.Text == "" || txtArticleDdate.Text == "" || txtArticleActDdate.Text == "")*/
            {
                //sMessage += i++ + ". Select a Job Stage, Enter start date, \\rdue date and half due date\\r\\n";
                sMessage += i++ + ". Select a Article Stage\\r\\n";
            }
            else if (txtArticleSdate.Text == "" || txtArticleDdate.Text == "" || txtArticleActDdate.Text == "")
            {
                //sMessage += i++ + ". Enter start date, due date and half due date\\r\\n";
            }
            else
            {
                if (DateTime.Parse(txtArticleDdate.Text) < DateTime.Parse(txtArticleSdate.Text))
                {
                    sMessage += i++ + ". Invalid Due Date\\r\\n";
                }
                if ((DateTime.Parse(txtArticleSdate.Text) > DateTime.Parse(txtArticleActDdate.Text)) ||
                    (DateTime.Parse(txtArticleActDdate.Text) > DateTime.Parse(txtArticleDdate.Text)))
                {
                    sMessage += i++ + ". Invalid Half Due Date\\r\\n";
                }
            }
        }
        else
        {
            if (drpArticleStage.SelectedItem.Value != "0")
            {
                if (txtArticleSdate.Text == "" || txtArticleDdate.Text == "" || txtArticleActDdate.Text == "")
                {
                    //sMessage += i++ + ". Enter start date, due date and half due date\\r\\n";
                }
                else
                {
                    string strArtDDate = txtArticleDdate.Text;
                    DateTime dtArtDDate = DateTime.ParseExact(strArtDDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    string strArtSDate = txtArticleSdate.Text;
                    DateTime dtArtSDate = DateTime.ParseExact(strArtSDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    string strArtHDate = txtArticleActDdate.Text;
                    DateTime dtArtHDate = DateTime.ParseExact(strArtHDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);


                    // if (DateTime.Parse(txtArticleDdate.Text) < DateTime.Parse(txtArticleSdate.Text))
                    if ((dtArtDDate) < (dtArtSDate))
                    {
                        sMessage += i++ + ". Invalid Due Date\\r\\n";
                    }
                    //if ((DateTime.Parse(txtArticleSdate.Text) > DateTime.Parse(txtArticleActDdate.Text)) ||
                    //    (DateTime.Parse(txtArticleActDdate.Text) > DateTime.Parse(txtArticleActDdate.Text)))
                    if (((dtArtSDate) > (dtArtHDate)) ||
                        ((dtArtHDate) > (dtArtHDate)))
                    {
                        sMessage += i++ + ". Invalid Half Due Date\\r\\n";
                    }

                    /* - Deleted by Gnanasekaran; dt:27-Nov-2013; for data validation -- Start
                     if (DateTime.Parse(txtArticleDdate.Text) < DateTime.Parse(txtArticleSdate.Text))
                     {
                         sMessage += i++ + ". Invalid Due Date\\r\\n";
                     }
                     if ((DateTime.Parse(txtArticleSdate.Text) > DateTime.Parse(txtArticleActDdate.Text)) ||
                         (DateTime.Parse(txtArticleActDdate.Text) > DateTime.Parse(txtArticleActDdate.Text)))
                     {
                         sMessage += i++ + ". Invalid Half Due Date\\r\\n";
                     } 
                     --End
                     */
                }
            }
        }
        //if (drpArticleSalesGroup.SelectedItem.Value == "0") sMessage += i++ + ". Select a Sales Group\\r\\n";
        if (i > 1)
        {
            Alert(sMessage);
            return false;
        }
        return true;
    }

    protected void cmd_Save_Article_Click(object sender, ImageClickEventArgs e)
    {
        string[] aArticleDetails;
        try
        {
            if (validateScreen())
            {
                if (hfA_ID.Value.Trim() == "")
                {
                    //insert Article
                    aArticleDetails = new string[46];
                    aArticleDetails[0] = drpArticleJournal.SelectedItem.Value.Trim();
                    aArticleDetails[1] = txtArticleMnsID.Text.Trim();
                    aArticleDetails[2] = txtArticleTitle.Text.Trim();
                    aArticleDetails[3] = "5"; // jobtype_id for Article
                    aArticleDetails[4] = drpArticleDoctype.SelectedItem.Value.Trim();
                    aArticleDetails[5] = drpArticleSubtype.SelectedItem.Value.Trim();
                    aArticleDetails[6] = drpArticleCategory.SelectedItem.Value.Trim();
                    aArticleDetails[7] = txtDOINo.Text.Trim();
                    aArticleDetails[8] = txtArticleAuthorCorr.Text.Trim();
                    aArticleDetails[9] = txtArticleAuthEmail.Text.Trim();
                    if (txtArticleNoofAuth.Text.Trim() == "") txtArticleNoofAuth.Text = "0";
                    if (txtArticlePrintpages.Text.Trim() == "") txtArticlePrintpages.Text = "0";
                    if (txtArticleMspages.Text.Trim() == "") txtArticleMspages.Text = "0";
                    aArticleDetails[10] = txtArticleNoofAuth.Text.Trim();
                    aArticleDetails[11] = txtArticlePrintpages.Text.Trim();
                    aArticleDetails[12] = txtArticleMspages.Text.Trim();
                    aArticleDetails[13] = "";
                    aArticleDetails[14] = "";
                    aArticleDetails[15] = "";
                    if (chkArticleRegCopyedit.Checked == true) aArticleDetails[16] = "1";
                    else aArticleDetails[16] = "0";
                    aArticleDetails[17] = txtArticleComments.Text.Trim();
                    aArticleDetails[18] = txt_SamAuthorQuery.Text.Trim();
                    aArticleDetails[19] = txt_FigureQuery.Text.Trim();
                    //stage
                    aArticleDetails[20] = drpArticleStage.SelectedItem.Value.Trim();
                    if (txtArticleSdate.Text.Trim() != "") aArticleDetails[21] = DateTime.Parse(txtArticleSdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aArticleDetails[21] = "";
                    if (txtArticleDdate.Text.Trim() != "") aArticleDetails[22] = DateTime.Parse(txtArticleDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aArticleDetails[22] = "";
                    if (txtArticleActDdate.Text.Trim() != "") aArticleDetails[23] = DateTime.Parse(txtArticleActDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aArticleDetails[23] = "";
                    if (chkArticleDespatch.Checked) aArticleDetails[24] = DateTime.Now.ToString("MM/dd/yyyy");
                    else aArticleDetails[24] = "";
                    if (txtArticleCDdate.Text.Trim() != "") aArticleDetails[25] = DateTime.Parse(txtArticleCDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aArticleDetails[25] = "";
                    aArticleDetails[26] = Session["employeeid"].ToString().Trim();
                    aArticleDetails[27] = drpArticleSalesGroup.SelectedItem.Value.Trim();
                    aArticleDetails[28] = txtArtMsRecDate.Text.Trim();
                    aArticleDetails[29] = txtArtMsRevDate.Text.Trim();
                    aArticleDetails[30] = txtArtMsAcceptDate.Text.Trim();
                    aArticleDetails[31] = (txtinterviewdate.Text != "") ? Convert.ToDateTime(txtinterviewdate.Text).ToShortDateString() : "";
                    aArticleDetails[32] = txtphoneno.Text.Trim().ToString();
                    aArticleDetails[33] = txtfaxno.Text.Trim().ToString();
                    aArticleDetails[34] = txtinterviewtime.Text.Trim().ToString();
                    aArticleDetails[35] = txt_meetingplace.Text.Trim().ToString();
                    aArticleDetails[36] = txt_meetingtime.Text.Trim().ToString();
                    aArticleDetails[37] = txt_country.Text.Trim().ToString();
                    aArticleDetails[38] = (txt_appointmentdate1.Text != "") ? Convert.ToDateTime(txt_appointmentdate1.Text.Trim().ToString()).ToShortDateString() : "";
                    aArticleDetails[39] = (txt_appointmentdate2.Text != "") ? Convert.ToDateTime(txt_appointmentdate2.Text.Trim().ToString()).ToShortDateString() : "";
                    aArticleDetails[40] = txt_zone.Text.Trim().ToString();
                    aArticleDetails[41] = drpWithdraw.SelectedValue.ToString();
                    aArticleDetails[42] = drpOpenAccess.SelectedItem.Text.ToString();
                    aArticleDetails[43] = txtResearchPage.Text.Trim();
                    aArticleDetails[44] = txtArtReprocess.Text.ToString();
                    aArticleDetails[45] = drpAutoprocess.SelectedValue.ToString();

                    string art = "" + aArticleDetails[0] + "," + aArticleDetails[1] + "," + aArticleDetails[2] + "," + aArticleDetails[3] + "," + aArticleDetails[4] + "," + aArticleDetails[5] + "," + aArticleDetails[6] + "," + aArticleDetails[7] + "," + aArticleDetails[8] + "," + aArticleDetails[9] + "," + aArticleDetails[10] + "," + aArticleDetails[11] + "," + aArticleDetails[12] + "," + aArticleDetails[13] + "," + aArticleDetails[14] + "," + aArticleDetails[15] + "," + aArticleDetails[16] + "," + aArticleDetails[17] + "," + aArticleDetails[18] + "," + aArticleDetails[19] + "," + aArticleDetails[20] + "," + aArticleDetails[21] + "," + aArticleDetails[22] + "," + aArticleDetails[23] + "," + aArticleDetails[24] + "," + aArticleDetails[25] + "," + aArticleDetails[26] + "," + aArticleDetails[27] + "," + aArticleDetails[28] + "," + aArticleDetails[29] + "," + aArticleDetails[30] + "," + aArticleDetails[31] + "," + aArticleDetails[32] + "," + aArticleDetails[33] + "," + aArticleDetails[34] + "," + aArticleDetails[35] + "," + aArticleDetails[36] + "," + aArticleDetails[37] + "," + aArticleDetails[38] + "," + aArticleDetails[39] + "," + aArticleDetails[40] + " ";

                    string msg = this.oArt.InsertArticle(aArticleDetails);
                    if (msg.Contains("Article creation failed!") ||
                        msg.Contains("Article Already Exists!")) Alert(msg);
                    else
                    {
                        hfA_ID.Value = msg;
                        this.loadArticleDetails(hfA_ID.Value.Trim());
                        //if (drpArticlecustomer.SelectedItem.Text.Trim() == "Decker")
                        if (drpArticlecustomer.SelectedItem.Text.Trim() == "Addiction & Mental Health" || drpArticlecustomer.SelectedItem.Text.Trim() == "BMJ" || drpArticlecustomer.SelectedItem.Text.Trim() == "Clinics" || drpArticlecustomer.SelectedItem.Text.Trim() == "GERM" || drpArticlecustomer.SelectedItem.Text.Trim() == "Institute of Farm Management" || drpArticlecustomer.SelectedItem.Text.Trim() == "Johns Hopkins University" || drpArticlecustomer.SelectedItem.Text.Trim() == "MediaSphere Medical" || drpArticlecustomer.SelectedItem.Text.Trim() == "Purdue University Press" || drpArticlecustomer.SelectedItem.Text.Trim() == "Scientific Electronic Library Online" || drpArticlecustomer.SelectedItem.Text.Trim() == "UK Books" || drpArticlecustomer.SelectedItem.Text.Trim() == "Decker")                         
                        {
                            getMetaXML(hfA_ID.Value.Trim());
                        }
                        Alert("Successfully Saved.");
                    }
                }
                else
                {
                    //update Article

                    aArticleDetails = new string[44];
                    aArticleDetails[0] = hfA_ID.Value.Trim();
                    aArticleDetails[1] = drpArticleJournal.SelectedItem.Value.Trim();
                    aArticleDetails[2] = txtArticleMnsID.Text.Trim();
                    aArticleDetails[3] = txtArticleTitle.Text.Trim();
                    aArticleDetails[4] = "5"; // jobtype_id for Article
                    aArticleDetails[5] = drpArticleDoctype.SelectedItem.Value.Trim();
                    aArticleDetails[6] = drpArticleSubtype.SelectedItem.Value.Trim();
                    aArticleDetails[7] = drpArticleCategory.SelectedItem.Value.Trim();
                    aArticleDetails[8] = txtDOINo.Text.Trim();
                    aArticleDetails[9] = txtArticleAuthorCorr.Text.Trim();
                    aArticleDetails[10] = txtArticleAuthEmail.Text.Trim();
                    if (txtArticleNoofAuth.Text.Trim() == "") txtArticleNoofAuth.Text = "0";
                    if (txtArticlePrintpages.Text.Trim() == "") txtArticlePrintpages.Text = "0";
                    if (txtArticleMspages.Text.Trim() == "") txtArticleMspages.Text = "0";
                    aArticleDetails[11] = txtArticleNoofAuth.Text.Trim();
                    aArticleDetails[12] = txtArticlePrintpages.Text.Trim();
                    aArticleDetails[13] = txtArticleMspages.Text.Trim();
                    if (chkArticleRegCopyedit.Checked == true) aArticleDetails[14] = "Y";
                    else aArticleDetails[14] = "N";
                    aArticleDetails[15] = txtArticleComments.Text.Trim();
                    aArticleDetails[16] = txt_SamAuthorQuery.Text.Trim();
                    //stage
                    aArticleDetails[17] = drpArticleStage.SelectedItem.Value.Trim();
                    if (txtArticleSdate.Text.Trim() != "") aArticleDetails[18] = DateTime.Parse(txtArticleSdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aArticleDetails[18] = "";
                    if (txtArticleDdate.Text.Trim() != "") aArticleDetails[19] = DateTime.Parse(txtArticleDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aArticleDetails[19] = "";
                    if (txtArticleActDdate.Text.Trim() != "") aArticleDetails[20] = DateTime.Parse(txtArticleActDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aArticleDetails[20] = "";
                    if (chkArticleDespatch.Checked) aArticleDetails[21] = DateTime.Now.ToString("MM/dd/yyyy");
                    else aArticleDetails[21] = "";
                    if (txtArticleCDdate.Text.Trim() != "") aArticleDetails[22] = DateTime.Parse(txtArticleCDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aArticleDetails[22] = "";
                    aArticleDetails[23] = Session["employeeid"].ToString().Trim();
                    aArticleDetails[24] = drpArticleSalesGroup.SelectedItem.Value.Trim();
                    aArticleDetails[25] = txtArtMsRecDate.Text.Trim();
                    aArticleDetails[26] = txtArtMsRevDate.Text.Trim();
                    aArticleDetails[27] = txtArtMsAcceptDate.Text.Trim();
                    aArticleDetails[28] = (txtinterviewdate.Text != "") ? Convert.ToDateTime(txtinterviewdate.Text).ToShortDateString() : "";
                    aArticleDetails[29] = txtphoneno.Text.Trim().ToString();
                    aArticleDetails[30] = txtfaxno.Text.Trim().ToString();
                    aArticleDetails[31] = txtinterviewtime.Text.Trim().ToString();
                    aArticleDetails[32] = txt_meetingplace.Text.Trim().ToString();
                    aArticleDetails[33] = txt_meetingtime.Text.Trim().ToString();
                    aArticleDetails[34] = txt_country.Text.Trim().ToString();
                    aArticleDetails[35] = (txt_appointmentdate1.Text != "") ? Convert.ToDateTime(txt_appointmentdate1.Text.Trim().ToString()).ToShortDateString() : "";
                    aArticleDetails[36] = (txt_appointmentdate2.Text != "") ? Convert.ToDateTime(txt_appointmentdate2.Text.Trim().ToString()).ToShortDateString() : "";
                    aArticleDetails[37] = txt_zone.Text.Trim().ToString();
                    aArticleDetails[38] = txt_FigureQuery.Text.Trim();
                    aArticleDetails[39] = drpWithdraw.SelectedValue.ToString();
                    aArticleDetails[40] = drpOpenAccess.SelectedItem.Text.ToString();
                    aArticleDetails[41] = txtResearchPage.Text.Trim();
                    aArticleDetails[42] = txtArtReprocess.Text.ToString();
                    aArticleDetails[43] = drpAutoprocess.SelectedValue.ToString();



                    string msg = this.oArt.UpdateArticle(aArticleDetails);
                    if (msg.Contains("Error updating Article:")) Alert(msg);
                    else
                    {
                       // if (drpArticlecustomer.SelectedItem.Text.Trim() == "Decker")
                        if (drpArticlecustomer.SelectedItem.Text.Trim() == "Addiction & Mental Health" || drpArticlecustomer.SelectedItem.Text.Trim() == "BMJ1" || drpArticlecustomer.SelectedItem.Text.Trim() == "Clinics" || drpArticlecustomer.SelectedItem.Text.Trim() == "GERM" || drpArticlecustomer.SelectedItem.Text.Trim() == "Institute of Farm Management" || drpArticlecustomer.SelectedItem.Text.Trim() == "Johns Hopkins University" || drpArticlecustomer.SelectedItem.Text.Trim() == "MediaSphere Medical" || drpArticlecustomer.SelectedItem.Text.Trim() == "Purdue University Press" || drpArticlecustomer.SelectedItem.Text.Trim() == "Scientific Electronic Library Online" || drpArticlecustomer.SelectedItem.Text.Trim() == "UK Books" || drpArticlecustomer.SelectedItem.Text.Trim() == "Decker")                         
                        {
                            int iOldStage;
                            iOldStage = getStageId(drpArticleStage.SelectedItem.Value, hfA_ID.Value.Trim());
                            if (iOldStage != Convert.ToInt32(drpArticleStage.SelectedItem.Value))
                            {
                                getMetaXML(hfA_ID.Value.Trim());
                            }
                        }
                        this.loadArticleDetails(hfA_ID.Value.Trim());

                        Alert("Successfully Updated.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally { aArticleDetails = null; }
    }

    public int getStageId(string StageId, string Ano)
    {
        string connetionString = null;
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataSet ds = new DataSet();
        int istage = 0;
        string sql = null;
        connetionString = "server=192.9.200.222;database=dp_mis_live;uid=sa;pwd=masterkey";
        connection = new SqlConnection(connetionString);
        sql = "Select Stypeno from  Article_dp where Stypeno=" + StageId + " and Ano=" + Ano + " ";

        try
        {
            connection.Open();
            adapter = new SqlDataAdapter(sql, connection);
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                istage = Convert.ToInt32(ds.Tables[0].Rows[0]["Stypeno"].ToString());
                return istage;
            }

            connection.Close();
            return istage;
        }
        catch
        {
            return 0;
        }

    }


    //protected void cmd_Save_Article_Click(object sender, ImageClickEventArgs e)
    //{
    //    string[] aArticleDetails;
    //    try
    //    {

    //        if (validateScreen())
    //        {
    //           if (hfA_ID.Value.Trim() == "")
    //            {
    //                 //insert Article
                       
    //            string[] s11= drpArticleJournal.SelectedItem.Text.Split('(');
    //            string s22 = s11[0].ToString().TrimEnd();

                       
    //             DataSet sds = new DataSet();
    //             datasourceIBSQL objCom = new datasourceIBSQL();

    //             string[,] sArticleInset=   {{"@JOURNO" , drpArticleJournal.SelectedItem.Value.Trim()},{"@AMANUSCRIPTID", txtArticleMnsID.Text.Trim()},{"@AMNSTITLE", txtArticleTitle.Text.Trim()},{"@ACORRESPONDINGAUTHOR", txtArticleAuthorCorr.Text.Trim()},{"@AEMAIL", txtArticleAuthEmail.Text.Trim()},
    //                                        {"@ANOAUTHORS",txtArticleNoofAuth.Text.Trim()},{"@PILLNO", txtArticlePllNo.Text.Trim()},{"@ADNO",  drpArticleDoctype.SelectedItem.Value.Trim()},{"@ARPNO",  drpPriority.SelectedItem.Value.Trim()},{"@STNO", drpCurrentstatus.SelectedItem.Value.Trim()},{"@INO",drpArticleIssueNo.SelectedItem.Value.Trim()},{"@CSTYPENO",  drpArticleCurrentStatge.SelectedItem.Value.Trim()},
    //                                        {"@DOINO", txtDOINo.Text.Trim()},{"@AARTWORKPIECES", txtArtwork.Text.Trim()},{"@ARTNOCOLOUR",txtColoured.Text.Trim()},{"@AREALNOOFPAGES",txtActNoPages.Text.Trim()},{"@ANOOFFOLIOS ", txtnoFolios.Text.Trim()},{"@ANOPROOFS", txtnoofproofs.Text.Trim()},{"@ARTEFFECTTYPE", drpArticleEffectType.SelectedItem.Value.Trim()},{"@JOURDTD",txtjournalDTD.Text.Trim() },
    //                                        {"@STYPENO",drpArticleStage.SelectedItem.Value.Trim()},{"@RECEIVED_DATE", DateTime.Parse(txtArticleSdate.Text.Trim()).ToString("MM/dd/yyyy")},{"@AHALFDUEDATE", DateTime.Parse(txtArticleActDdate.Text.Trim()).ToString("MM/dd/yyyy")},{"@ADUEDATE",DateTime.Parse(txtArticleDdate.Text.Trim()).ToString("MM/dd/yyyy") },{"@CATS_DUE_DATE", DateTime.Parse(txtArticleCDdate.Text.Trim()).ToString("MM/dd/yyyy")},{"@ON_HOLD_FLAG","N"},
    //                                        {"@IPCTNO",drpArticleSubtype.SelectedItem.Value.Trim()},{"@ARTICLETYPE",  drpArticleArtType.SelectedItem.Value.Trim()},{"@CATNO",drpArticleCategory.SelectedItem.Value.Trim()},  
    //                                        {"@APINO", drpProdItemType.SelectedItem.Value.Trim()},{"@APTNO",drpProdType.SelectedItem.Value.Trim()},{"@APAGENOFROM",txtPageFrom.Text.Trim()},{"@APAGENOTO",txtPageto.Text.Trim()},{"@COPYRIGHT",drpArticleCopyright.SelectedItem.Text.Trim()},{"@NSNO", drpNumSys.SelectedItem.Value.Trim()},{"@EMPID",Session["employeeid"].ToString().Trim()},{"@CATSSAM",  "N"},
    //                                        {"@AINVOICED", "N"},{"@AARTICLECODE", s22 + txtArticleMnsID.Text.Trim()},{"@AARCHIVE",  "N"},{"@ABARCODE", "0"},{"@AWEEKNO","0"},{"@AQCPASSED",   "N"},{"@AEMAILED",  "N"}, {"@AEXTERNAL",  "N"},{"@CURRENT_DEPT_DUE",  "N"}, {"@NEXT_DEPT_DUE",   "N"},{"@CORRECT_DEPT_DUE",   "N"},{"@COMPLETED_FLAG",  "N"},{"@CONTENTS_ENTRY",   "N"},
    //                                        {"@REVISED_FLAG",  "N"},{"@INV_CROSSREF",   "N"},{"@INV_EPUB",  "N"},{"@INV_HIGHLEVEL_COPYEDIT",   "N"},{"@INV_REKEY",   "N"},{"@INV_ADDITIONAL_TOC",   "N"},{"@sales_job_group_id",drpArticleSalesGroup.SelectedItem.Value.Trim()},{"@ms_received_date",  txtArtMsRecDate.Text.Trim()},{"@ms_revised_date", txtArtMsRevDate.Text.Trim()},{"@ms_accepted_date",  txtArtMsAcceptDate.Text.Trim()},
    //                                        {"@comments",  txtArticleComments.Text.Trim()},{"@sam_author_query", txt_SamAuthorQuery.Text.Trim()},{"@figure_correction",  txt_FigureQuery.Text.Trim()},{"@print_pages",txtArticlePrintpages.Text.Trim()},
    //                                        {"@ms_pages",txtArticleMspages.Text.Trim()},{"@JOB_STAGE_ID",drpArticleStage.SelectedValue.ToString()}};
    //             sds = objCom.ExcProcedurePrdJL("spInsertArticle", sArticleInset, CommandType.StoredProcedure);
    //             lnkGeneral_Click(null, null);
    //            }
    //            else
    //            {
    //                //update Article



    //                string[,] sArticleupdate =   {{"@article_id",hfA_ID.Value.ToString()}, {"@journal_id" , drpArticleJournal.SelectedItem.Value.Trim()},{"@name", txtArticleMnsID.Text.Trim()},{"@title", txtArticleTitle.Text.Trim()},{"@job_type_id", "5"},{"@document_type_id", drpArticleDoctype.SelectedItem.Value},{"@document_item_type_id", drpArticleSubtype.SelectedItem.Value},{"@category_id", drpArticleCategory.SelectedItem.Value},{"@doi", txtDOINo.Text.Trim()},
    //                                             {"@AUTHOR", txtArticleAuthorCorr.Text.Trim()},{"@author_email ", txtArticleAuthEmail.Text.Trim()},{"@no_authors",txtArticleNoofAuth.Text.Trim()},{"@PILLNO", txtArticlePllNo.Text.Trim()},{"@print_pages",txtArticlePrintpages.Text.Trim()},{"@ms_pages",txtArticleMspages.Text.Trim()},{"@comments",txtArticleComments.Text.Trim()},{"@sam_author_query", txt_SamAuthorQuery.Text.Trim()},{"@figure_correction",  txt_FigureQuery.Text.Trim()},
    //                                             {"@ADNO",  drpArticleDoctype.SelectedItem.Value.Trim()},{"@ARPNO",  drpPriority.SelectedItem.Value.Trim()},{"@STNO", drpCurrentstatus.SelectedItem.Value.Trim()},{"@sales_job_group_id",drpArticleSalesGroup.SelectedItem.Value},{"@ms_received_date",DateTime.Parse(txtArtMsRecDate.Text.Trim()).ToString("MM/dd/yyyy")} , {"@ms_revised_date",DateTime.Parse(txtArtMsRevDate.Text.Trim()).ToString("MM/dd/yyyy")} ,{"@ms_accepted_date",DateTime.Parse(txtArtMsRecDate.Text.Trim()).ToString("MM/dd/yyyy")} ,
    //                                             {"@AARTWORKPIECES", txtArtwork.Text.Trim()},{"@ARTNOCOLOUR",txtColoured.Text.Trim()},{"@AREALNOOFPAGES",txtActNoPages.Text.Trim()},{"@ANOOFFOLIOS ", txtnoFolios.Text.Trim()},{"@ANOPROOFS", txtnoofproofs.Text.Trim()},{"@ARTEFFECTTYPE", drpArticleEffectType.SelectedItem.Value.Trim()},{"@JOURDTD",txtjournalDTD.Text.Trim()},{"@created_by",Session["employeeid"].ToString()},
    //                                             {"@job_stage_id",drpArticleStage.SelectedItem.Value.Trim()},{"@received_date", DateTime.Parse(txtArticleSdate.Text.Trim()).ToString("MM/dd/yyyy")},{"@half_due_date", DateTime.Parse(txtArticleActDdate.Text.Trim()).ToString("MM/dd/yyyy")},{"@due_date",DateTime.Parse(txtArticleDdate.Text.Trim()).ToString("MM/dd/yyyy") },{"@despatch_date",DateTime.Parse(txtArticleDdate.Text.Trim()).ToString("MM/dd/yyyy") },{"@CATS_DUE_DATE", DateTime.Parse(txtArticleCDdate.Text.Trim()).ToString("MM/dd/yyyy")},{"@ON_HOLD_FLAG","N"}};
            
    //                datasourceIBSQL objCom = new datasourceIBSQL();
    //                objCom.ExcProcedurePrdJL("spUpdateArticle_1", sArticleupdate, CommandType.StoredProcedure);
    //                lnkGeneral_Click(null, null);
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //Response.Write(ex.Message);
    //        Alert(ex.Message);
    //    }
    //    finally { aArticleDetails = null; }
    //}

    protected void lnkArticleHold_Click(object sender, EventArgs e)
    {
        try
        {
            int val = 0;
            if (hfA_ID.Value.Trim() != "")
            {
                if (chkArticlOnHold.Checked)
                {
                    string res = oArt.InsertJobOnHold(hfA_ID.Value.Trim(), "5", drpArticleOnHoldType.SelectedItem.Value.Trim(),
                        txtArticleOnHoldReason.Text.Trim(), Session["employeeid"].ToString());
                    if (int.TryParse(res, out val))
                    {
                        this.loadArticleDetails(hfA_ID.Value.Trim());
                        Alert("Successfully saved.");
                    }
                    else Alert("Onhold process failed!");
                }
                else
                {
                    string res = oArt.UpdateJobOnHold(hfA_ID.Value.Trim(), "5", drpArticleOnHoldType.SelectedItem.Value.Trim());
                    if (int.TryParse(res, out val))
                    {
                        this.loadArticleDetails(hfA_ID.Value.Trim());
                        Alert("Successfully saved.");
                    }
                    else Alert("Onhold process failed!");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void cmd_Print_Article_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void drpArticleStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////////////////////////////// chkArticleDespatch.Checked = false;
        if (drpArticleStage.SelectedItem.Value != "0")
        {

            txtArticleSdate.Text = "";
            txtArticleDdate.Text = "";
            txtArticleActDdate.Text = "";
            txtArticleCDdate.Text = "";
            imgAD_stdate.Visible = true;
            imgAD_dudate.Visible = true;
         //  imgAD_hdudate.Visible = true;
            imgAD_cdudate.Visible = true;
            
        }
        else
        {
            txtArticleSdate.Text = "";
            txtArticleDdate.Text = "";
            txtArticleActDdate.Text = "";
            txtArticleCDdate.Text = "";
            imgAD_stdate.Visible = false;
            imgAD_dudate.Visible = false;
           // imgAD_hdudate.Visible = false;
            imgAD_cdudate.Visible = false;
            ////////////////////////////// chkArticleDespatch.Enabled = false;
        }
    }
    protected void drpArticlecustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpArticleJournal.Items.Clear();
        if (drpArticlecustomer.SelectedItem.Value != "0")
        {
            DataSet dsJour = oArt.getJournalsByCustomer(drpArticlecustomer.SelectedItem.Value.Trim());
            drpArticleJournal.DataSource = dsJour;
            drpArticleJournal.DataTextField = dsJour.Tables[0].Columns["jourcode"].ToString();
            drpArticleJournal.DataValueField = dsJour.Tables[0].Columns["journal_id"].ToString();
            drpArticleJournal.DataBind();
        }
        drpArticleJournal.Items.Insert(0, new ListItem("-- select --", "0"));
    }
    protected void cmdGD_Save_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string[] sGraphic = null;
            ArrayList lstGraphic = new ArrayList();
            foreach (GridViewRow ro in gvGraphicsDetails.Rows)
            {
                if (((TextBox)ro.FindControl("txtGD_i_graphicname")).Visible)
                {
                    sGraphic = new string[6];
                    sGraphic[0] = ((HiddenField)ro.FindControl("hfGD_GraphicId")).Value.Trim();
                    sGraphic[1] = ((TextBox)ro.FindControl("txtGD_i_graphicname")).Text.Trim();
                    sGraphic[2] = ((DropDownList)ro.FindControl("drpGD_i_graphictype")).SelectedItem.Value.Trim();
                    sGraphic[3] = ((TextBox)ro.FindControl("txtGD_i_Graphicdesc")).Text.Trim();
                    sGraphic[4] = ((DropDownList)ro.FindControl("drpGD_i_figtype")).SelectedItem.Value.Trim();
                    sGraphic[5] = ((DropDownList)ro.FindControl("drpGD_i_Redraw")).SelectedItem.Value.Trim();
                    lstGraphic.Add(sGraphic);
                }
            }
            if (this.oArt.UpdateGraphics(lstGraphic) == "true")
            {
                this.loadGraphicsDetails(hfA_ID.Value);
                Alert("Successfully Saved.");
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToLower().Contains("cannot insert duplicate key"))
                Alert("Warning: duplicate records not allowed!");
        }
    }
    protected void cmd_Excel_Export_Click(object sender, ImageClickEventArgs e)
    {
        dtArticle = (DataTable)Session["ArtcleDetails"];

        if (dtArticle != null && dtArticle.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='10' align='center'><h4>Article Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Job Number</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Article Name</b></td><td bgcolor='silver'><b>Article Title</b></td><td bgcolor='silver'><b>Stage</b></td><td bgcolor='silver'><b>Received Date</b></td><td bgcolor='silver'><b>Half Due Date</b></td><td bgcolor='silver'><b>Due Date</b></td><td bgcolor='silver'><b>Issue</b></td><td bgcolor='silver'><b>Hold Details</b></td><td bgcolor='silver'><b>Cats Due Date</b></td><td bgcolor='silver'><b>Despatch Date</b></td></tr>");
            foreach (DataRow r in dtArticle.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + r["job_id"] + "</td>");
                sbData.Append("<td>" + r["aarticlecode"] + "</td>");
                sbData.Append("<td>" + r["title"] + "</td>");
                sbData.Append("<td>" + r["job_stage_name"] + "</td>");
                sbData.Append("<td>" + r["received_date"] + "</td>");
                sbData.Append("<td>" + r["Actual_due_date"] + "</td>");
                sbData.Append("<td>" + r["due_date"] + "</td>");
                sbData.Append("<td>" + r["Issue"] + "</td>");
                sbData.Append("<td>" + r["Hold_details"] + "</td>");
                sbData.Append("<td>" + r["cats_due_date"] + "</td>");
                sbData.Append("<td>" + r["despatch_date"] + "</td>");
                sbData.Append("</tr>");
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Article_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            //Response.ContentEncoding = Encoding.Unicode;
            //Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.End();
        }
    }
    protected void gvArticles_Sorting(object sender, GridViewSortEventArgs e)
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
            if (sortDirection.Equals(SortDirection.Descending)) di = " desc"; dv.Table = dtArticle;
            dv.Sort = e.SortExpression + di;
            sSortExpression = e.SortExpression;
            gvArticles.DataSource = dv;
            gvArticles.DataBind();
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
    protected void imgbtnEventExport_Click(object sender, ImageClickEventArgs e)
    {
        StringBuilder sbData = new StringBuilder();
        sbData.Append("<table border='1'>");
        sbData.Append("<tr valign='top'><td colspan='8'><h4>" + lblEventsHeader.Text.Trim() + "</h4></td></tr>");
        sbData.Append("<tr valign='top'><td colspan='8'><b>Article:</b></td></tr>");
        sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Job</b></td><td bgcolor='silver'><b>Job Stage</b></td><td bgcolor='silver'><b>Task</b></td><td bgcolor='silver'><b>Start Time</b></td><td bgcolor='silver'><b>End Time</b></td><td bgcolor='silver'><b>Duration</b></td><td bgcolor='silver'><b>Employee</b></td><td bgcolor='silver'><b>Comments</b></td></tr>");
        foreach (GridViewRow r in gvEvents.Rows)
        {
            sbData.Append("<tr valign='top'>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[0].Controls[0]).Text + "</td>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[1].Controls[0]).Text + "</td>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[2].Controls[0]).Text + "</td>");
            sbData.Append("<td align='left'>" + ((DataBoundLiteralControl)r.Cells[3].Controls[0]).Text + "</td>");
            sbData.Append("<td align='left'>" + ((DataBoundLiteralControl)r.Cells[4].Controls[0]).Text + "</td>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[5].Controls[0]).Text + "</td>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[6].Controls[0]).Text + "</td>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[7].Controls[0]).Text + "</td>");
            sbData.Append("</tr>");
        }
        sbData.Append("</table>");
        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Article_Events_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
        Response.ContentType = "application/ms-excel";
        Response.Write(sbData.ToString());
        Response.End();
    }
    protected void chkViewCompleted_CheckedChanged(object sender, EventArgs e)
    {
        if (chkViewCompleted.Checked)
        {
            if (txtSearch.Text.Trim() == "")
            {
                lblMonth.Visible = true;
                DDMonthList.Visible = true;
                lblYear.Visible = true;
                DDYearList.Visible = true;
            }
            else
            {
                lblMonth.Visible = false;
                DDMonthList.Visible = false;
                lblYear.Visible = false;
                DDYearList.Visible = false;
            }
        }
        else
        {
            lblMonth.Visible = false;
            DDMonthList.Visible = false;
            lblYear.Visible = false;
            DDYearList.Visible = false;
        }
    }
    protected void lnkArtWork_Click(object sender, EventArgs e)
    {
        if (hfA_ID.Value.Trim() != "")
        {
            DataSet ds = new DataSet();
            ds=oArt.getArtWork(hfA_ID.Value);
            if (ds != null)
            {
                gvArtWork.DataSource = ds;
                gvArtWork.DataBind();
            }
        }
        this.showPanel(tabArtWork);
    }
   
    protected void dropArtFigType_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropReceived.SelectedValue = "Y";
        txtDateReceived.Text = DateTime.Now.ToString();
    }
    protected void cmdArt_Save_Click1(object sender, ImageClickEventArgs e)
    {
        string[] aArtDetails;
        try
        {
                if (hfA_ID.Value.Trim() != "")
                {
                    aArtDetails = new string[6];
                    aArtDetails[0] = dropArtFigType.SelectedItem.Value.Trim();
                    aArtDetails[1] = dropImgType.SelectedItem.Value.Trim();
                    aArtDetails[2] = dropReceived.SelectedItem.Value.Trim();
                    aArtDetails[3] = txtDateReceived.Text.Trim();
                    aArtDetails[4] = dropRedraw.SelectedItem.Value.Trim();
                    aArtDetails[5] = hfA_ID.Value.Trim();

                    string msg = this.oArt.InsertArt(aArtDetails);
                    if (msg.Contains("Error Inserting Art Work")) 
                        Alert(msg);
                    else
                    {
                        Alert("Inserted Successfully");
                        artgrdload();
                    }
                }

            
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally { aArtDetails = null; }

        }
    protected void drpArticleSubtype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void artgrdload()
    {
        if (hfA_ID.Value.Trim() != "")
        {
            DataSet ds = new DataSet();
            ds = oArt.getArtWork(hfA_ID.Value);
            if (ds != null)
            {
                gvArtWork.DataSource = ds;
                gvArtWork.DataBind();
            }
        }
    }


    protected void cmd_XML__Click(object sender, ImageClickEventArgs e)
    {
        getMetaXML(hfA_ID.Value.Trim());
    }
    public void getMetaXML(string ano)
    {
        string connetionString = null;
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataSet ds = new DataSet();
        string sql = null;

        connetionString = "server=192.9.200.222;database=dp_mis_live;uid=sa;pwd=masterkey";
        connection = new SqlConnection(connetionString);
        sql = "Select LTRIM(RTRIM(C.CUSTNAME)) AS [CUSTOMERNAME],LTRIM(RTRIM(B.JOURNAME)) [JOURNALNAME],LTRIM(RTRIM(B.JOURCODE)) JOURNALCODE,  LTRIM(RTRIM(A.AARTICLECODE))ARTICLECODE,ISNULL(LTRIM(RTRIM(A.AMNSTITLE)),'') TITLE,LTRIM(RTRIM(A.ACORRESPONDINGAUTHOR)) AUTHOR, LTRIM(RTRIM(A.AEMAIL)) AUTHOREMAIL,LTRIM(RTRIM(D.ALTERNATE_NAME))STAGE,CONVERT(VARCHAR,JJ.ACTUAL_CATSDUEDATE,101) DUEDATE," +
              "   A.AARTWORKPIECES NOOFFIGURES,CASE WHEN A.MS_PAGES=0 THEN NULL ELSE  A.MS_PAGES END  MANUSCRIPTPAGE,case when A.AREALNOOFPAGES=0 then NULL Else A.AREALNOOFPAGES end NOOFPAGES " +
              "  ,J.COMMENTS,ltrim(rtrim(A.DOINO))DOINO " +
              "  FROM ARTICLE_DP A  WITH(NOLOCK) INNER JOIN JOB_HISTORY JJ WITH(NOLOCK) ON A.JOB_HISTORY_ID=JJ.JOB_HISTORY_ID" +
              "  INNER JOIN  JOURNAL_DP B WITH(NOLOCK) ON A.JOURNO=B.JOURNO  " +
              "  INNER JOIN  CUSTOMER_DP C WITH(NOLOCK) ON B.CUSTNO=C.CUSTNO " +
              "  INNER JOIN STYPE_DP D WITH(NOLOCK) ON A.STYPENO=D.STYPENO" +
              "  LEFT OUTER JOIN JOB_COMMENT J WITH(NOLOCK) ON A.COMMENT_ID=J.COMMENT_ID " +
              "  WHERE A.ANO=" + ano + " AND B.CUSTNO in(10216,10235,10231,10233,10226,10224,10218,10213,10211,10203,10222)";

        try
        {
            connection.Open();
            adapter = new SqlDataAdapter(sql, connection);
            adapter.Fill(ds);
            connection.Close();

            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt.AcceptChanges();
                dt.Namespace = "http://temporg.uri";
                dt.TableName = "Data";

                var desktopFolder = @"\\192.9.200.222\dp\NatureWeb\Decker";//Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var fullFileName = System.IO.Path.Combine(desktopFolder, ds.Tables[0].Rows[0]["ARTICLECODE"].ToString() + ".xml");
                //   var fs = new FileStream(fullFileName, FileMode.Create);

                using (System.IO.FileStream fs = new System.IO.FileStream(fullFileName, System.IO.FileMode.Create))
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                    sw.WriteLine("<?xml version=\"1.0\" standalone=\"yes\"?>");

                    foreach (DataRow item in dt.Rows)
                    {
                        sw.WriteLine("<Data>");
                        foreach (DataColumn col in dt.Columns)
                        {
                            string s = "<" + col.ColumnName + "/>";
                            if (item[col].ToString() != "0")
                            {
                                s = "<" + col.ColumnName + ">" + item[col].ToString() + "</" + col.ColumnName + ">";
                            }
                            sw.WriteLine(s);
                        }
                        sw.WriteLine("</Data>");
                    }
                    sw.Close();
                    Alert("Metadata XML generated Successfully");

                    // Console.WriteLine("File downloaded '"+ fullFileName + "' successfully");
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void cmd_Invoice_Setup_Click(object sender, ImageClickEventArgs e)
    {
        //if()
        Datasource_IBSQL dsObj = new Datasource_IBSQL();
        if (hfA_ID.Value.Trim() != "")
        {
            string id = hfA_ID.Value.Trim();

            string strPrePro = string.Empty;
            if(drpPreprocess.SelectedItem.Text=="N")
            {
                strPrePro = null;
            }
            else
            {
                strPrePro = "DP";
            }

            string strQuery = "update AP_Article_DP set PreProcess='" + strPrePro + "' where ano=" + id;
            dsObj.GetDataSet(strQuery,"Tem",CommandType.Text);

            if (drpFirstProof.SelectedItem.Text == "CW")
            {
                string strQuery1 = "update ARTICLE_DP  set adno=2 where ano=" + id;
                dsObj.GetDataSet(strQuery1, "Tem", CommandType.Text);
            }
            Alert("Updated Successfully");
         //  if()
             
           // dsObj.GetDataSet("select custno from issue_dp i, journal_dp j where i.journo=j.journo and ino=" + ino, "Projects", CommandType.Text);
        }
    }
}

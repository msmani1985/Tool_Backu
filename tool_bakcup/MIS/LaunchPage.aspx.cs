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
using System.Drawing;
using System.IO;
using System.Globalization;
using CrystalDecisions.Web.Design;
using System.Data.SqlClient;
using System.Xml;
//using CrystalDecisions.Enterprise;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Text.RegularExpressions;
using Tools;
using System.Net;
using System.Data.OleDb;

public partial class LaunchPage : System.Web.UI.Page
{
    public int id = 1;
    Non_Launch nonLa = new Non_Launch();
    LaunchSQL oLaunch = new LaunchSQL();
    Launch_SQL oNewLa = new Launch_SQL();
    Launch la = new Launch();
    datasourceSQL sql = new datasourceSQL();
    private static DataTable dtable = new DataTable();
    private static DataTable dtable4 = new DataTable();
    ReportDocument rep, subRep1, subRep2, subRep3;
    string userName = "dpisoft";
    string domain = "sdsdomain";
    string password = "Dp!@123";
    string t, s, x, y, soft1, ver1;
    SqlConnection scon;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.popScreen();
            this.popScreenNew();
            Session["txtlocation"] = null;
        }
        if (Session["employeeid"].ToString() == "2807" || Session["employeeid"].ToString() == "2461" || 
            Session["employeeid"].ToString() == "2499" || Session["employeeid"].ToString() == "2365" || 
            Session["employeeid"].ToString() == "2392" || Session["employeeid"].ToString() == "2997" ||
            Session["employeeid"].ToString() == "2603" || Session["employeeid"].ToString() == "2605" ||
            Session["employeeid"].ToString() == "2112" || Session["employeeid"].ToString() == "1914" ||
            Session["employeeid"].ToString() == "2721" || Session["employeeid"].ToString() == "1780" ||
            Session["employeeid"].ToString() == "1686" || Session["employeeid"].ToString() == "1594" ||
            Session["employeeid"].ToString() == "2852" || Session["employeeid"].ToString() == "2450" ||
            Session["employeeid"].ToString() == "3100")
        {
            btnQuoteSave.Visible = true;
            btnClear.Visible = true;
            btnNewQuoteSave.Visible = true;
            btnNewClear.Visible = true;
        }
        else
        {
            btnQuoteSave.Visible = false;
            btnClear.Visible = false;
            btnNewQuoteSave.Visible = false;
            btnNewClear.Visible = false;
        }

    }

    private void popScreen()
    {
        dtable = new DataTable();
        Launch DSql1 = new Launch();

        DataSet Dst2 = new DataSet();
        Dst2 = DSql1.searchlangname();
        lboxlang.DataTextField = "lang_name";
        lboxlang.DataValueField = "lang_name";
        lboxlang.DataSource = Dst2;
        lboxlang.DataBind();

        DataSet Dst3 = new DataSet();
        Dst3 = DSql1.GetTask();
        lboxtask.DataTextField = "taskname";
        lboxtask.DataValueField = "taskname";
        lboxtask.DataSource = Dst3;
        lboxtask.DataBind();

        DataSet Dst11 = new DataSet();
        Dst11 = DSql1.GetMissFonts();
        lboxMissFonts.DataTextField = "Fonts";
        lboxMissFonts.DataValueField = "Fonts";
        lboxMissFonts.DataSource = Dst11;
        lboxMissFonts.DataBind();
        lboxFonts.DataTextField = "Fonts";
        lboxFonts.DataValueField = "Fonts";
        lboxFonts.DataSource = Dst11;
        lboxFonts.DataBind();
        lboxusagefonts.DataTextField = "Fonts";
        lboxusagefonts.DataValueField = "Fonts";
        lboxusagefonts.DataSource = Dst11;
        lboxusagefonts.DataBind();

        DataSet dscust5 = la.getAllLocation();
        DropLocation.DataSource = dscust5;
        DropLocation.DataTextField = dscust5.Tables[0].Columns[1].ToString();
        DropLocation.DataValueField = dscust5.Tables[0].Columns[0].ToString();
        DropLocation.DataBind();
        DropLocation.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dscust = la.getAllCustomers();
        drpProjectcustomer.DataSource = dscust;
        drpProjectcustomer.DataTextField = dscust.Tables[0].Columns[1].ToString();
        drpProjectcustomer.DataValueField = dscust.Tables[0].Columns[0].ToString();
        drpProjectcustomer.DataBind();
        drpProjectcustomer.Items.Insert(0, new ListItem("-- select --", "0"));

        //DataSet dscust = la.getAllCustomers();
        //drpCustomerSearch.DataSource = dscust;
        //drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
        //drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
        //drpCustomerSearch.DataBind();
        //drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));

        Dropcomplex.Items.Clear();
        Dropcomplex.Items.Add(new ListItem("Simple", "S"));
        Dropcomplex.Items.Add(new ListItem("Medium", "M"));
        Dropcomplex.Items.Add(new ListItem("Complex", "C"));

        DataSet ds = new DataSet();
        ds = la.GetQueries(txtProjectTitle.Text);
        gv_Queries.DataSource = ds;
        gv_Queries.DataBind();
        this.showPanel(tabGeneral);
        gv_images.Visible = true;
        DDMonthList.SelectedValue = DateTime.Now.Month.ToString();
        DDYearList.SelectedValue = DateTime.Now.Year.ToString();

        DataSet sw = nonLa.getAllSoftware();
        lboxSW.DataSource = sw;
        lboxSW.DataTextField = sw.Tables[0].Columns[1].ToString();
        lboxSW.DataValueField = sw.Tables[0].Columns[0].ToString();
        lboxSW.DataBind();
    }
    public void load()
    {
        lblsource.Visible = true;
        DropSource.Visible = true;
        lblformat.Visible = true;
        lboxformat.Visible = true;
        lblimg.Visible = true;
        gv_images.Visible = true;
        lbltable.Visible = true;
        lblnotable.Visible = true;
        Dropnooftables.Visible = true;
        lblpdf.Visible = true;
        lblpage.Visible = true;
        txtpagesize.Visible = true;
        lblbleed.Visible = true;
        txtBleed.Visible = true;
        lblproof.Visible = true;
        txtproof.Visible = true;
        txtpress.Visible = true;
        lblpress.Visible = true;
        lblTarDate.Visible = true;
        txttarget.Visible = true;
        img2.Visible = true;
        CheckYTR.Visible = true;
        lblcomplex.Visible = true;
        lblcomplexlev.Visible = true;
        Dropcomplex.Visible = true;
        lblReason.Visible = true;
        ListComplexReason.Visible = true;
        lbltarLang.Visible = true;
        lboxlang.Visible = true;
        lboxlangused.Visible = true;
        btnlangdel.Visible = true;
        btnlangadd.Visible = true;
        lblNoTarLang.Visible = true;
        txtlangcount.Visible = true;
        gv_lang.Visible = true;
        txtfiglinks.Visible = true;
        lblmissfiglink.Visible = true;
        lboxFonts.Visible = true;
        lboxMissFonts.Visible = true;
        lblmissfonts.Visible = true;
        lblfonts.Visible = true;
        lbltarLang.Text = "Target Languages:";
        lblNoTarLang.Text = "No. of Target languages:";
        lblFilePage.Visible = true;
        lblfiles.Visible = false;
        txtFiles.Visible = false;
        lblfonts.Text = "Usage of fonts with respect to target languages:";
        lblmissfiglink.Text = "Missing Figure Links:";
        lblSourceDate.Text = "Source Received on:";
        lboxusagefonts.Visible = true;
        txtOtherUsageFonts.Visible = true;
        lblUsageFonts.Visible = true;
        btnUFontsadd.Visible = true;
        btnUFontsRemove.Visible = true;
        lboxUFonts.Visible = true;
        btnfontsadd.Visible = true;
        btnfontsdel.Visible = true;
        lboxTFonts.Visible = true;
        btnmissfontsadd.Visible = true;
        btnmissfontsdel.Visible = true;
        lboxMFonts.Visible = true;

    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabGeneral":
                miGeneral.Attributes.Add("class", "current");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class","");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = true;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = false;
                break;
            case "tabLaunchDetails":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "current");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class","");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = true;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = false;
                break;
            case "tabFileInfo":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "current");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class","");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = true;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = false;
                break;
            case "tabquotedetails":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "current");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class","");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = true;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = false;
                break;
            
            case "tabNewLaunch":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "current");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class","");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = true;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = false;
                break;
            case "tabNewFileDetails":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "current");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class","");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = true;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = false;
                break;
            case "tabNewQuote":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "current");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class","");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = true;
                this.tabAddQuote.Visible = false;
                break;
            case "tabJobTracking":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "current");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class","");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = true;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = false;
                break;
            case "tabNewreportdetails":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "current");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class","");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = true;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = false;
                break;
            case "tabreportdetails":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "current");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class","");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = true;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = false;
                break;
            case "tabLoggedEvents":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "current");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class","");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = true;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = false;
                break;
            case "tabFinalQuote":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "");
                miFinalQuote.Attributes.Add("class", "current");
                this.tabFinalQuote.Visible = true;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = false;
                break;
            case "tabAddQuote":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileInfo.Attributes.Add("class", "");
                miCostDetails.Attributes.Add("class", "");
                miReportDetails.Attributes.Add("class", "");
                miNewJobInfo.Attributes.Add("class", "");
                miNewFileInfo.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miNewReportDetails.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miAddQuote.Attributes.Add("class", "current");
                miFinalQuote.Attributes.Add("class", "");
                this.tabFinalQuote.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewreportdetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabNewLaunch.Visible = false;
                this.tabNewFileDetails.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabLaunchDetails.Visible = false;
                this.tabFileInfo.Visible = false;
                this.tabquotedetails.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabAddQuote.Visible = true;
                break;
        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,\"" + ((HiddenField)e.Row.FindControl("hfgvProjectID")).Value.Trim() + "\",\"" + ((Label)e.Row.FindControl("lblgvPtitle")).Text.Trim() + "\");";

            HiddenField pro_id = e.Row.FindControl("hfgvProjectID") as HiddenField;
            DropDownList status = e.Row.FindControl("DropStatus") as DropDownList;
            DataSet ds = new DataSet();
            ds = la.GetDeliveryStatus(Convert.ToInt32(pro_id.Value));
            if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() != "")
            {
                status.Items.Add(new ListItem("--Select--", "0"));
                status.Items.Add(new ListItem("P", "P"));
                status.Items.Add(new ListItem("C", "C"));
                status.Items.Add(new ListItem("WIP", "WIP"));
                status.Items.Add(new ListItem("Del", "Del"));
                status.Items[status.Items.IndexOf(status.Items.FindByValue(ds.Tables[0].Rows[0]["Delivery_Status"].ToString()))].Selected = true;
            }
            if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "P")
                e.Row.Cells[9].CssClass = "gridP";
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "C")
                e.Row.Cells[9].CssClass = "gridC";
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "Del")
                e.Row.Cells[9].CssClass = "gridDel";
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "WIP")
                e.Row.Cells[9].CssClass = "gridWIP";
        }
    }
    protected void GvNL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,\"" + ((HiddenField)e.Row.FindControl("hfgvNLID")).Value.Trim() + "\",\"" + ((Label)e.Row.FindControl("lblgvPtitle")).Text.Trim() + "\");";

            HiddenField pro_id = e.Row.FindControl("hfgvNLID") as HiddenField;
            DropDownList status = e.Row.FindControl("DropStatus") as DropDownList;
            LinkButton lnkFU = e.Row.FindControl("lnkFU") as LinkButton;
            lnkFU.Enabled = false;
            lnkFU.Text = "";
            DataSet ds = new DataSet();
            int Month = Convert.ToInt16(DDMonthList.SelectedValue);
            int Year = Convert.ToInt32(DDYearList.SelectedValue);
            int Total = Month + Year * 12;
            ds = nonLa.GetDeliveryStatus(Convert.ToInt32(pro_id.Value));
            if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() != "")
            {
                status.Items.Add(new ListItem("--Select--", "0"));
                status.Items.Add(new ListItem("P", "P"));
                status.Items.Add(new ListItem("C", "C"));
                status.Items.Add(new ListItem("WIP", "WIP"));
                status.Items.Add(new ListItem("Del", "Del"));
                status.Items[status.Items.IndexOf(status.Items.FindByValue(ds.Tables[0].Rows[0]["Delivery_Status"].ToString()))].Selected = true;
            }
            else
            {
                status.Items.Add(new ListItem("--Select--", "0"));
                status.Items.Add(new ListItem("P", "P"));
                status.Items.Add(new ListItem("C", "C"));
                status.Items.Add(new ListItem("WIP", "WIP"));
                status.Items.Add(new ListItem("Del", "Del"));
            }
            if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "P")
            {
                e.Row.Cells[11].CssClass = "gridP";
                lnkFU.Enabled = true;
                lnkFU.Text = "Click";
            }
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "C")
                e.Row.Cells[11].CssClass = "gridC";
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "Del")
                e.Row.Cells[11].CssClass = "gridDel";
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "WIP")
                e.Row.Cells[11].CssClass = "gridWIP";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int Month = Convert.ToInt16(DDMonthList.SelectedValue);
        int Year = Convert.ToInt32(DDYearList.SelectedValue);
        int Total = Month + Year * 12;
        DataSet ds = new DataSet();
        if (Total < 24186)
        {
            ds = la.GetJobDetails(Convert.ToInt32(drpCustomerSearch.SelectedValue), Convert.ToInt32(DDMonthList.SelectedValue), Convert.ToInt32(DDYearList.SelectedValue));
            GvProject.DataSource = ds;
            GvProject.DataBind();
            Session["projectdetails"] = ds;
            this.hfP_ID.Value = "";
            dtable = ds.Tables[0].Copy();
            dtable4 = ds.Tables[0].Copy();
            GvProject.Visible = true;
            GvNL.Visible = false;
        }
        else
        {
            ds = nonLa.GetJobDetailsLP(Convert.ToInt32(drpCustomerSearch.SelectedValue), Convert.ToInt32(DDMonthList.SelectedValue), Convert.ToInt32(DDYearList.SelectedValue));
            GvNL.DataSource = ds;
            GvNL.DataBind();
            this.hfP_ID.Value = "";
            Session["projectdetails"] = ds;
            if (ds != null)
            {
                dtable4 = ds.Tables[0].Copy();
            }
            GvProject.Visible = false;
            GvNL.Visible = true;
        }

        //DataSet ds = new DataSet();
        //DataSet ov = new DataSet();
        //ds = la.GetJobDetails(Convert.ToInt32(drpCustomerSearch.SelectedValue), Convert.ToInt32(DDMonthList.SelectedValue), Convert.ToInt32(DDYearList.SelectedValue));
        //ov = la.OverAllReport(Convert.ToInt32(drpCustomerSearch.SelectedValue),  Convert.ToInt32(DDMonthList.SelectedValue), Convert.ToInt32(DDYearList.SelectedValue));
        //GvProject.DataSource = ds;
        //GvProject.DataBind();
        //Session["projectdetails"] = ds;
        //this.hfP_ID.Value = "";
        //dtable = ds.Tables[0].Copy();
        //dtable4 = ov.Tables[0].Copy();
        this.showPanel(tabGeneral);

    }
    protected void lnkGeneral_Click(object sender, EventArgs e)
    {
        load();
        btnSearch_Click(sender, e);
        this.showPanel(tabGeneral);
    }
    protected void lnkLaunchDetails_Click(object sender, EventArgs e)
    {

        if (hfP_ID.Value != "")
        {
            load();
            loadJobDetails(hfP_ID.Value.Trim());
        }
        this.showPanel(tabLaunchDetails);

    }
    protected void lnkFileInfo_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            
            load();
            if (Convert.ToInt16(hfP_ID.Value) < 381)
            {
                DataSet dsCost12 = la.GetComplexReasonsold();
                ListComplexReason.DataSource = dsCost12;
                ListComplexReason.DataValueField = dsCost12.Tables[0].Columns[0].ToString();
                ListComplexReason.DataTextField = dsCost12.Tables[0].Columns[2].ToString();
                ListComplexReason.DataBind();
            }
            else
            {
                DataSet dsCost12 = la.GetComplexReason(Convert.ToInt16(hfP_ID.Value), Dropcomplex.SelectedValue);
                ListComplexReason.DataSource = dsCost12;
                ListComplexReason.DataValueField = dsCost12.Tables[0].Columns[0].ToString();
                ListComplexReason.DataTextField = dsCost12.Tables[0].Columns[2].ToString();
                ListComplexReason.DataBind();
            }
            loadFileDetails(hfP_ID.Value.Trim());
            
        }
        else
        {
            if (txtProjectTitle.Text != "")
            {
                DataSet dp = new DataSet();
                dp = la.GetProid(txtProjectTitle.Text);
                if (Convert.ToInt16(dp.Tables[0].Rows[0]["Pro_id"].ToString()) < 381)
                {
                    DataSet dsCost12 = la.GetComplexReasonsold();
                    ListComplexReason.DataSource = dsCost12;
                    ListComplexReason.DataValueField = dsCost12.Tables[0].Columns[0].ToString();
                    ListComplexReason.DataTextField = dsCost12.Tables[0].Columns[2].ToString();
                    ListComplexReason.DataBind();
                }
                else
                {
                    DataSet dsCost12 = la.GetComplexReason(Convert.ToInt16(dp.Tables[0].Rows[0]["Pro_id"].ToString()), Dropcomplex.SelectedValue);
                    ListComplexReason.DataSource = dsCost12;
                    ListComplexReason.DataValueField = dsCost12.Tables[0].Columns[0].ToString();
                    ListComplexReason.DataTextField = dsCost12.Tables[0].Columns[2].ToString();
                    ListComplexReason.DataBind();
                }
            }
        }
        string id;
        if (hfP_ID.Value != "")
            id = hfP_Name.Value;
        else if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else
            id = "";
        load();
        DataSet dst5 = new DataSet();
        dst5 = la.getDeliveryGroup(id.ToString());
        //dst5 = oLaunch.ExcuteSelectProcedure("spGet_deliverytypeGroups", id.ToString(), "@proname", "varChar", "input", CommandType.StoredProcedure);
        if (dst5.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dst5.Tables[0].Rows.Count; i++)
            {
                if (y == "" || y == null)
                    y = dst5.Tables[0].Rows[i]["Delivery_Type"].ToString();
                else
                    y = y + ',' + dst5.Tables[0].Rows[i]["Delivery_Type"].ToString();
            }
            if (y.ToString() == "1" || y.ToString() == "1,2" || y.ToString() == "1,3" || y.ToString() == "1,2,3")
            {
                lblSoft.Visible = true;
                gv_soft1.Visible = true;
            }
            else
            {
                lblSoft.Visible = false;
                gv_soft1.Visible = false;
            }
        }
        DataSet ds1 = new DataSet();
        ds1 = la.getTaskGroup(id.ToString());
        //ds1 = oLaunch.ExcuteSelectProcedure("spGet_taskGroups", id.ToString(), "@proname", "varChar", "input", CommandType.StoredProcedure);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            CheckBoxTask.Items.Clear();
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                if (x == "" || x == null)
                    x = ds1.Tables[0].Rows[i]["taskname"].ToString();
                else
                    x = x + ',' + ds1.Tables[0].Rows[i]["taskname"].ToString();

                ListItem it = new ListItem();
                it.Value = ds1.Tables[0].Rows[i]["taskname"].ToString();
                it.Text = ds1.Tables[0].Rows[i]["taskname"].ToString();
                CheckBoxTask.Items.Add(it);
            }
            if (x.ToString() == "TE")
            {
                lblsource.Visible = true;
                DropSource.Visible = true;
                lblTarDate.Visible = false;
                txttarget.Visible = false;
                img2.Visible = false;
                CheckYTR.Visible = false;
                lblmissfiglink.Visible = false;
                txtfiglinks.Visible = false;
                lblproof.Visible = false;
                txtproof.Visible = false;
                txtpress.Visible = false;
                lblpress.Visible = false;
                lbltarLang.Text = "Source Languages:";
                lblNoTarLang.Text = "No. of Source languages:";
                lblfiles.Visible = false;
                txtFiles.Visible = false;
                lblfonts.Text = "Usage of fonts with respect to Source languages:";
                lboxusagefonts.Visible = false;
                txtOtherUsageFonts.Visible = false;
                f1.Visible = false;
                lblUsageFonts.Visible = false;
                btnUFontsadd.Visible = false;
                btnUFontsRemove.Visible = false;
                lboxUFonts.Visible = false;
                lblSoft.Visible = false;
                gv_soft1.Visible = false;
                txtFonts.Visible = true;
                f2.Visible = true;
                txtMissFonts.Visible = true;
                f3.Visible = true;
            }
            else if (x.ToString() == "PrepDTP" || x.ToString() == "OVA")
            {
                lblsource.Visible = false;
                DropSource.Visible = false;
                lblformat.Visible = false;
                lboxformat.Visible = false;
                lblfiles.Visible = false;
                txtFiles.Visible = false;
            }
            else if (x.ToString() == "DQA")
            {
                lblsource.Visible = false;
                DropSource.Visible = false;
                lblformat.Visible = false;
                lboxformat.Visible = false;
                lblproof.Visible = false;
                txtproof.Visible = false;
                txtpress.Visible = false;
                lblpress.Visible = false;
                txtfiglinks.Visible = false;
                lblmissfiglink.Visible = false;
                lboxFonts.Visible = false;
                lboxMissFonts.Visible = false;
                lblmissfonts.Visible = false;
                lblfonts.Visible = false;
                lblimg.Visible = false;
                gv_images.Visible = false;
                lbltable.Visible = false;
                lblnotable.Visible = false;
                Dropnooftables.Visible = false;
                lblpdf.Visible = false;
                lblpage.Visible = false;
                txtpagesize.Visible = false;
                lblbleed.Visible = false;
                txtBleed.Visible = false;
                lblfiles.Visible = false;
                txtFiles.Visible = false;
                lboxusagefonts.Visible = false;
                txtOtherUsageFonts.Visible = false;
                f1.Visible = false;
                lblUsageFonts.Visible = false;
                btnUFontsadd.Visible = false;
                btnUFontsRemove.Visible = false;
                lboxUFonts.Visible = false;
                lblSoft.Visible = false;
                gv_soft1.Visible = false;
                btnfontsadd.Visible = false;
                btnfontsdel.Visible = false;
                lboxTFonts.Visible = false;
                btnmissfontsadd.Visible = false;
                btnmissfontsdel.Visible = false;
                lboxMFonts.Visible = false;
                txtFonts.Visible = false;
                f2.Visible = false;
                txtMissFonts.Visible = false;
                f3.Visible = false;
                f1.Visible = false;
            }
            else if (x.ToString() == "File Conversion")
            {
                lblsource.Visible = false;
                DropSource.Visible = false;
                lblformat.Visible = false;
                lboxformat.Visible = false;
                lblimg.Visible = false;
                gv_images.Visible = false;
                lbltable.Visible = false;
                lblnotable.Visible = false;
                Dropnooftables.Visible = false;
                lblpdf.Visible = false;
                lblpage.Visible = false;
                txtpagesize.Visible = false;
                lblbleed.Visible = false;
                txtBleed.Visible = false;
                lblproof.Visible = false;
                txtproof.Visible = false;
                txtpress.Visible = false;
                lblpress.Visible = false;
                lblTarDate.Visible = false;
                txttarget.Visible = false;
                img2.Visible = false;
                CheckYTR.Visible = false;
                lblcomplex.Visible = false;
                lblcomplexlev.Visible = false;
                Dropcomplex.Visible = false;
                lblReason.Visible = false;
                ListComplexReason.Visible = false;
                lbltarLang.Visible = false;
                lboxlang.Visible = false;
                btnlangadd.Visible = false;
                btnlangdel.Visible = false;
                lboxlangused.Visible = false;
                lblNoTarLang.Visible = false;
                txtlangcount.Visible = false;
                lblFilePage.Visible = false;
                gv_lang.Visible = false;
                lblfiles.Visible = true;
                txtFiles.Visible = true;
                lblmissfiglink.Text = "Missing Links:";
                lblSourceDate.Text = "File for Conversion Received on:";
                lboxusagefonts.Visible = false;
                txtOtherUsageFonts.Visible = false;
                f1.Visible = false;
                lblUsageFonts.Visible = false;
                btnUFontsadd.Visible = false;
                btnUFontsRemove.Visible = false;
                lboxUFonts.Visible = false;
                lblSoft.Visible = false;
                gv_soft1.Visible = false;
            }
            else
            {
                lboxusagefonts.Visible = true;
                lblUsageFonts.Visible = true;
                lblTarDate.Visible = true;
                txttarget.Visible = true;
                img2.Visible = true;
                CheckYTR.Visible = true;
                lblsource.Visible = false;
                DropSource.Visible = false;
                lblfiles.Visible = false;
                txtFiles.Visible = false;
                btnUFontsadd.Visible = true;
                btnUFontsRemove.Visible = true;
                lboxUFonts.Visible = true;
                lblSoft.Visible = true;
                gv_soft1.Visible = true;
                txtFonts.Visible = true;
                f2.Visible = true;
                txtMissFonts.Visible = true;
                f3.Visible = true;
                f1.Visible = true;
            }
        }
        this.showPanel(tabFileInfo);
    }

    protected void lnkReportInfo_Click(object sender, EventArgs e)
    {
        CrystalReportViewer1.Visible = false;
        CrystalReportViewer2.Visible = false;
        this.showPanel(tabreportdetails);
    }
    protected void lnkCostInfo_Click(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_Name.Value;
        else if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else
            id = "";
       
        if (id != "")
        {
            Launch la = new Launch();
            DataSet sds1 = new DataSet();
            sds1 = la.getQuoteValues(id);
            if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
            {
                gv_pmodule.DataSource = sds1.Tables[0];
                gv_pmodule.DataBind();
                if (sds1.Tables[0].Rows[0]["Jobid"].ToString() != "")
                {
                    
                    foreach (GridViewRow grw in gv_pmodule.Rows)
                    {
                        TextBox  Time= (TextBox)grw.FindControl("txt_time");
                        TextBox Hrate = (TextBox)grw.FindControl("txt_hrate");
                        TextBox Prate = (TextBox)grw.FindControl("txt_prate");
                        TextBox Hcost = (TextBox)grw.FindControl("txt_hourrate");
                        TextBox Pcost = (TextBox)grw.FindControl("txt_pagerate");
                        CheckBox HChk = (CheckBox)grw.FindControl("chk_hour");
                        CheckBox PChk = (CheckBox)grw.FindControl("chk_page");
                        TextBox Lang = (TextBox)grw.FindControl("txt_langcount");
                        TextBox Ptotal = (TextBox)grw.FindControl("txt_totalpage");
                        
                        if (Session["employeeid"].ToString() == "2461" || Session["employeeid"].ToString() == "1335")
                        {
                            Time.Enabled = true;
                            Hrate.Enabled = true;
                            Prate.Enabled = true;
                            Hcost.Enabled = true;
                            Pcost.Enabled = true;
                            HChk.Enabled = true;
                            PChk.Enabled = true;
                            Lang.Enabled = true;
                            Ptotal.Enabled = true;
                            txtquoteremark.Enabled = true;
                            btnQuoteSave.Enabled = true;
                            btnClear.Enabled = true;
                        }
                        else
                        {
                            Time.Enabled = false;
                            Hrate.Enabled = false;
                            Prate.Enabled = false;
                            Hcost.Enabled = false;
                            Pcost.Enabled = false;
                            HChk.Enabled = false;
                            PChk.Enabled = false;
                            Lang.Enabled = false;
                            Ptotal.Enabled = false;
                            txtquoteremark.Enabled = true;
                            btnQuoteSave.Enabled = true;
                            btnClear.Enabled = true;
                        }
                    }
                }
            }
            DataSet ds = new DataSet();
            ds = la.empname(id);
            txtquoteremark.Text = sds1.Tables[0].Rows[0]["QUOTEREMARKS"].ToString();
            txtProjectCo.Text = ds.Tables[0].Rows[0]["empname"].ToString();
            txtFinalCheck.Text = sds1.Tables[0].Rows[0]["FINALCHECK"].ToString();
        }
        CrystalReportViewer1.Visible = false;
        this.showPanel(tabquotedetails);
    }
    protected void TimeTaken_TextChanged(object sender, EventArgs e)
    {
        string id;
        if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else
            id = hfP_Name.Value;
        DataSet dp=new DataSet();
        dp = la.GetProid(id.ToString());
        int pro_id = Convert.ToInt16(dp.Tables[0].Rows[0]["Pro_id"].ToString());
        foreach (GridViewRow grw in gv_pmodule.Rows)
        {
            string time = ((TextBox)grw.FindControl("txt_time")).Text;
            string task = ((TextBox)grw.FindControl("txt_des")).Text;
            string name = ((TextBox)grw.FindControl("txt_name")).Text;
            string soft = ((TextBox)grw.FindControl("txt_soft")).Text;
            if(pro_id>511)
                la.UpdateTimeTaken(name, task, time,soft);
            else
                la.UpdateValues(name, task, time);
        }
        DataSet sds1 = new DataSet();
        sds1 = la.getValues(id);
        if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
        {
            gv_pmodule.DataSource = sds1.Tables[0];
            gv_pmodule.DataBind();
        }
        lnkCostInfo_Click(sender, e);
    }
    protected void Hrs_TextChanged(object sender, EventArgs e)
    {
        string id;
        if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else
            id = hfP_Name.Value;
        DataSet dp = new DataSet();
        dp = la.GetProid(id.ToString());
        int pro_id = Convert.ToInt16(dp.Tables[0].Rows[0]["Pro_id"].ToString());
        foreach (GridViewRow grw in gv_pmodule.Rows)
        {
            string hrs = ((TextBox)grw.FindControl("txt_hrate")).Text;
            string task = ((TextBox)grw.FindControl("txt_des")).Text;
            string name = ((TextBox)grw.FindControl("txt_name")).Text;
            string soft = ((TextBox)grw.FindControl("txt_soft")).Text;
            if (pro_id > 511)
                la.UpdateHrs(name, task, hrs, soft);
            else
                la.UpdateHrsValues(name, task, hrs);
        }
        DataSet sds1 = new DataSet();
        sds1 = la.getValues(id);
        if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
        {
            gv_pmodule.DataSource = sds1.Tables[0];
            gv_pmodule.DataBind();
        }
        lnkCostInfo_Click(sender, e);
    }
    protected void Page_TextChanged(object sender, EventArgs e)
    {
        string id;
        if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else
            id = hfP_Name.Value;
        DataSet dp = new DataSet();
        dp = la.GetProid(id.ToString());
        int pro_id = Convert.ToInt16(dp.Tables[0].Rows[0]["Pro_id"].ToString());
        foreach (GridViewRow grw in gv_pmodule.Rows)
        {
            string page = ((TextBox)grw.FindControl("txt_prate")).Text;
            string task = ((TextBox)grw.FindControl("txt_des")).Text;
            string name = ((TextBox)grw.FindControl("txt_name")).Text;
            string soft = ((TextBox)grw.FindControl("txt_soft")).Text;
            if (pro_id > 511)
                la.UpdatePage(name, task, page, soft);
            else
                la.UpdatePageValues(name, task, page);
        }
        DataSet sds1 = new DataSet();
        sds1 = la.getValues(id);
        if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
        {
            gv_pmodule.DataSource = sds1.Tables[0];
            gv_pmodule.DataBind();
        }
        lnkCostInfo_Click(sender, e);
    }
    private void ClearJobDetails()
    {
        txtsource.Text = "";
        txttarget.Text = "";
        txtlangcount.Text = "";
        txtfiglinks.Text = "";
        txtproof.Text = "";
        txtpress.Text = "";
        txtpagesize.Text = "";
        txtBleed.Text = "";
        txtMissFonts.Text = "";
        txtFonts.Text = "";
        Dropnooftables.Text="";
        Dropcomplex.ClearSelection();
        DropNameConv.ClearSelection();
        txtQuery.Text = "";
        txtQueryans.Text = "";
        txtSplIns.Text = "";
        txtProjectEditor.Text = "";
        lboxformat.ClearSelection();
        lboxlang.ClearSelection();
        lboxtask.ClearSelection();
        lboxdelivryType.ClearSelection();
        linputfile.ClearSelection();
        gv_lang.Visible = false;
        gv_Queries.Visible = false;
        gv_images.Visible = false;
    }
    private void loadJob()
    {
        txtJobID.Enabled = false;
        txtProjectEditor.Enabled = false;
        txtProjectTitle.Enabled = false;
        drpProjectcustomer.Enabled = false;
        DropLocation.Enabled = false;
        dropnoFTP.Enabled = false;
        txtSize.Enabled = false;
        DropSizeBytes.Enabled = false;
        dropSwPlat.Enabled = false;
        dropIpFTP.Enabled = false;
        txtDate.Enabled = false;
        TextBox2.Enabled = false;
        btnJobInfo.Enabled = false;
        btnJobClear.Enabled = false;
        txtdueFromdate.Enabled = false;
        txtdueTodate.Enabled = false;
        chkYTC.Enabled = false;
        lboxformat.Enabled = false;
        lboxtask.Enabled = false;
        linputfile.Enabled = false;

    }
    private void loadJobDetails(string sJobID)
    {
        sJobID = sJobID.Trim();
        if (sJobID != "")
        {
            lboxformat.ClearSelection();
            lboxtask.ClearSelection();
            lboxusagefonts.ClearSelection();
            lboxFonts.ClearSelection();
            lboxMissFonts.ClearSelection();
            lboxUFonts.ClearSelection();
            lboxTFonts.ClearSelection();
            lboxMFonts.ClearSelection();
            DataSet dsProject = la.getJobDetailsByID(sJobID);
            lblProjectHeader.Text = "Edit Project";
            imgProjectHeader.Src = "images/tools/edit.png";
            DataRow row = dsProject.Tables[0].Rows[0];
            txtJobID.Text = row["JOBID"].ToString();
            txtProjectEditor.Text = row["PROJECTEDITOR"].ToString();
            txtProjectTitle.Text = row["projectname"].ToString();
            txtProjectid.Text = row["projectid"].ToString();
            drpProjectcustomer.SelectedValue = row["cust_id"].ToString();
            getloc(Convert.ToInt16(row["cust_id"].ToString()));
            DropLocation.SelectedValue = row["LOCATION"].ToString();
            getloc_timezone(Convert.ToInt16(row["LOCATION"].ToString()));
            dropnoFTP.SelectedValue = row["noof_package"].ToString();
            txtSize.Text = row["pack_size"].ToString();
            DropSizeBytes.SelectedValue = row["PACK_SIZE_BYTES"].ToString();
            dropSwPlat.SelectedValue = row["platform"].ToString();
            dropIpFTP.SelectedValue = row["FTPYN"].ToString();
            txtDate.Text = row["CUR_DATE"].ToString();
            if (row["DUEDATEYN"].ToString() == "1")
            {
                lblDueFrom.Visible = true;
                txtdueFromdate.Visible = true;
                lblDueTo.Visible = true;
                imgBD_dueFromdate.Visible = true;
                txtdueTodate.Visible = true;
                imgBD_dueTodate.Visible = true;
                chkDueDate.Checked = true;
            }
            else
            {
                lblDueFrom.Visible = false;
                txtdueFromdate.Visible = true;
                lblDueTo.Visible = false;
                imgBD_dueFromdate.Visible = true;
                txtdueTodate.Visible = false;
                imgBD_dueTodate.Visible = false;
                chkDueDate.Checked = false;
            }
            if (row["DUETIMEYN"].ToString() == "1")
            {
                lblFrom.Visible = true;
                lblTo.Visible = true;
                chkDueTime.Checked = true;
                DropDueTimeTo.Visible = true;
                DropDueMinTo.Visible = true;
                DropDueTimeZoneTo.Visible = true;
                TextBox1.Visible = true;
                DropDueTimeFrom.SelectedValue = row["DUE_TIMEFROM"].ToString();
                DropDueMinFrom.SelectedValue = row["DUE_MINFROM"].ToString();
                DropDueTimeZoneFrom.SelectedValue = row["TIME_ZONEFROM"].ToString();
                DropDueTimeTo.SelectedValue = row["DUE_TIMETO"].ToString();
                DropDueMinTo.SelectedValue = row["DUE_MINTO"].ToString();
                DropDueTimeZoneTo.SelectedValue = row["TIME_ZONEFROM"].ToString();
                TextBox2.Text = row["duetimefrom_ist"].ToString();
                TextBox1.Text = row["duetimeto_ist"].ToString();
            }
            else
            {
                lblFrom.Visible = false;
                lblTo.Visible = false;
                chkDueTime.Checked = false;
                DropDueTimeFrom.SelectedValue = row["DUE_TIMEFROM"].ToString();
                DropDueMinFrom.SelectedValue = row["DUE_MINFROM"].ToString();
                DropDueTimeZoneFrom.SelectedValue = row["TIME_ZONEFROM"].ToString();
                DropDueTimeTo.SelectedValue = "00";
                DropDueMinTo.SelectedValue = "00";
                DropDueTimeZoneTo.SelectedValue = row["TIME_ZONEFROM"].ToString();
                TextBox2.Text = row["duetimefrom_ist"].ToString();
                TextBox1.Text = "";
            }
            txtFilepath.Text = row["File_Path"].ToString();
            if (row["ytcyn"].ToString() == "1") chkYTC.Checked = true;
            if (row["DUE_DATE"].ToString() == "")
                txtdueFromdate.Text = "";
            else
                txtdueFromdate.Text = DateTime.Parse(row["DUE_DATE"].ToString()).ToShortDateString();
            if (row["DUE_DATETo"].ToString() == "")
                txtdueTodate.Text = "";
            else
                txtdueTodate.Text = DateTime.Parse(row["DUE_DATETo"].ToString()).ToShortDateString();
            DataSet empds4 = new DataSet();
            empds4 = la.getTaskGroup(row["projectname"].ToString());
            //string pn = "'" + txtProjectTitle.Text + "'";
            //empds4 = oLaunch.ExcuteSelectProcedure("spGet_taskGroups", row["projectname"].ToString(), "@proname", "varChar", "input", CommandType.StoredProcedure);
            if (empds4.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < empds4.Tables[0].Rows.Count; i++)
                {
                    lboxtask.Items[lboxtask.Items.IndexOf(lboxtask.Items.FindByValue(empds4.Tables[0].Rows[i]["taskname"].ToString()))].Selected = true;

                    DataSet td = new DataSet();
                    td = la.GetTaskid(empds4.Tables[0].Rows[i]["taskname"].ToString());
                    DataRow rs = td.Tables[0].Rows[0];

                    if (t == "" || t == null)
                        t = rs["task_id"].ToString();
                    else
                        t = t + ',' + rs["task_id"].ToString();

                    if (s == "" || s == null)
                        s = empds4.Tables[0].Rows[i]["taskname"].ToString();
                    else
                        s = s + ',' + empds4.Tables[0].Rows[i]["taskname"].ToString();
                }
            }
            DataSet tn = new DataSet();
            tn = la.getTaskName(s.ToString());
            gv_Soft.DataSource = tn;
            gv_Soft.DataBind();
            DataSet Dst4 = new DataSet();
            for (int i = 0; i < empds4.Tables[0].Rows.Count; i++)
            {
                Dst4 = la.GetFormat(t.ToString());
                lboxformat.DataTextField = "format_name";
                lboxformat.DataValueField = "format_name";
                lboxformat.DataSource = Dst4;
                lboxformat.DataBind();
            }
            DataSet empds1 = new DataSet();
            empds1 = la.getFormatGroup(row["projectname"].ToString());
            //empds1 = oLaunch.ExcuteSelectProcedure("spGet_formatGroups", row["projectname"].ToString(), "@proname", "varChar", "input", CommandType.StoredProcedure);
            if (empds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < empds1.Tables[0].Rows.Count; i++)
                    lboxformat.Items[lboxformat.Items.IndexOf(lboxformat.Items.FindByValue(empds1.Tables[0].Rows[i]["format_id"].ToString()))].Selected = true;
            }
            DataSet empds2 = new DataSet();
            empds2 = la.getInputGroup(row["projectname"].ToString());
            //empds2 = oLaunch.ExcuteSelectProcedure("spGet_InputGroups", row["projectname"].ToString(), "@proname", "varChar", "input", CommandType.StoredProcedure);
            if (empds2.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < empds2.Tables[0].Rows.Count; i++)
                    linputfile.Items[linputfile.Items.IndexOf(linputfile.Items.FindByValue(empds2.Tables[0].Rows[i]["inputid"].ToString()))].Selected = true;
            }
            if (empds4.Tables[0].Rows.Count > 0)
            {
                if (empds4.Tables[0].Rows[0]["taskname"].ToString() == "TE" || empds4.Tables[0].Rows[0]["taskname"].ToString() == "" || empds4.Tables[0].Rows[0]["taskname"].ToString() == null)
                {
                    lblsource.Visible = true;
                    DropSource.Visible = true;
                    lboxformat.Visible = true;
                    lblformat.Visible = true;
                }
                else if (empds4.Tables[0].Rows[0]["taskname"].ToString() == "DTP" || empds4.Tables[0].Rows[0]["taskname"].ToString() == "File Conversion")
                {
                    lblsource.Visible = false;
                    DropSource.Visible = false;
                    lboxformat.Visible = true;
                    lblformat.Visible = true;
                }
                else
                {
                    lboxformat.Visible = false;
                    lblformat.Visible = false;
                    lblsource.Visible = false;
                    DropSource.Visible = false;
                }
            }

            DropSource.SelectedValue = row["SourceType"].ToString();
            btnJobInfo.Text = "Update";
        }
    }
    private void loadFileDetails(string sJobID)
    {
        sJobID = sJobID.Trim();
        if (sJobID != "")
        {

            DataSet dsProject1 = la.getJobDetailsByID(sJobID);
            DataRow row = dsProject1.Tables[0].Rows[0];
            Session["Task"] = row["Task"].ToString();
            if (row["TARGETDATE"].ToString() == "")
                txttarget.Text = "";
            else
                txttarget.Text = DateTime.Parse(row["TARGETDATE"].ToString()).ToShortDateString();
            if (row["SOURCEDATE"].ToString() == "")
                txtsource.Text = "";
            else
                txtsource.Text = DateTime.Parse(row["SOURCEDATE"].ToString()).ToShortDateString();
            if (row["ytryn"].ToString() == "1") CheckYTR.Checked = true;
            txtlangcount.Text = row["LANG_COUNT"].ToString();
            txtfiglinks.Text = row["MISS_FIG_LINK"].ToString();
            Dropnooftables.Text = row["TABLES"].ToString();
            txtproof.Text = row["PROOF"].ToString();
            txtpress.Text = row["PRESS_PRINT"].ToString();
            txtpagesize.Text = row["page_size"].ToString();
            lblMail.Text = row["MailDetails"].ToString();
            Dropcomplex.SelectedValue = row["COMPLEX_LEVEL"].ToString();
            if (Convert.ToInt16(row["Pro_id"].ToString())<381)
                getComplexReasonold( row["COMPLEX_LEVEL"].ToString());
            else
                getComplexReason(Convert.ToInt16(row["Pro_id"].ToString()), row["COMPLEX_LEVEL"].ToString());
        if (Convert.ToInt16(row["Pro_id"].ToString()) < 381)
        {
            if (row["Reason_id"].ToString() != "0")
            {
                ListComplexReason.Items[ListComplexReason.Items.IndexOf(ListComplexReason.Items.FindByValue(row["REASON_ID"].ToString()))].Selected = true;
            }
        }
        else
        {
            if (row["Reason_id"].ToString() != "0")
            {
                DataSet empds = new DataSet();
                empds = la.getComplexReason(Convert.ToInt16(row["Pro_id"].ToString()));
                if (empds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < empds.Tables[0].Rows.Count; i++)
                        ListComplexReason.Items[ListComplexReason.Items.IndexOf(ListComplexReason.Items.FindByValue(empds.Tables[0].Rows[i]["Reason_id"].ToString()))].Selected = true;
                }
            }
        }
            DropNameConv.SelectedValue = row["FILENAME_CONV"].ToString();
            txtSplIns.Text = row["REMARKS"].ToString();
            txtBleed.Text = row["bleed"].ToString();
            txtFiles.Text = row["FILE_CONV"].ToString();

            DataSet ufs = new DataSet();
            ufs = la.GetUsageFonts(row["projectname"].ToString());
            lboxUFonts.DataTextField = "Fonts";
            lboxUFonts.DataValueField = "Fonts";
            lboxUFonts.DataSource = ufs;
            lboxUFonts.DataBind();

            DataSet tf1 = new DataSet();
            tf1 = la.GetFonts(row["projectname"].ToString());
            lboxTFonts.DataTextField = "Fonts";
            lboxTFonts.DataValueField = "Fonts";
            lboxTFonts.DataSource = tf1;
            lboxTFonts.DataBind();

            DataSet mf = new DataSet();
            mf = la.GetMFonts(row["projectname"].ToString());
            lboxMFonts.DataTextField = "Fonts";
            lboxMFonts.DataValueField = "Fonts";
            lboxMFonts.DataSource = mf;
            lboxMFonts.DataBind();
            lboxlangused.Items.Clear();
            DataSet uf1 = new DataSet();
            uf1 = la.GetusedLang(row["projectname"].ToString());
            lboxlangused.DataTextField = "lang_name";
            lboxlangused.DataValueField = "lang_name";
            lboxlangused.DataSource = uf1;
            lboxlangused.DataBind();

            txtFonts.Text = row["OTHER_FONTS"].ToString();
            txtMissFonts.Text = row["OTHER_MISS_FONTS"].ToString();
            txtOtherUsageFonts.Text = row["OTHER_UASGE_FONTS"].ToString();
            
            DataSet sds1 = new DataSet();
            sds1 = la.GetLangFile(row["projectname"].ToString());
            //if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
            //{
                gv_lang.DataSource = sds1.Tables[0];
                gv_lang.DataBind();
                gv_images.DataSource = sds1.Tables[0];
                gv_images.DataBind();
            //}
            DataSet ds = new DataSet();
            ds = la.GetQueries(row["projectname"].ToString());
            gv_Queries.DataSource = ds;
            gv_Queries.DataBind();
            DataSet empd = new DataSet();
            empd = la.getLangGroup(row["projectname"].ToString());
            if (empd.Tables[0].Rows.Count > 0)
            {
                //for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                //    lboxlang.Items[lboxlang.Items.IndexOf(lboxlang.Items.FindByValue(empd.Tables[0].Rows[i]["lang_name"].ToString()))].Selected = true;
            }
            DataSet empds5 = new DataSet();
            empds5 = la.getMFontGroup(row["projectname"].ToString());
            if (empds5.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < empds5.Tables[0].Rows.Count; i++)
                    lboxMissFonts.Items[lboxMissFonts.Items.IndexOf(lboxMissFonts.Items.FindByValue(empds5.Tables[0].Rows[i]["Fonts"].ToString()))].Selected = true;
            }
            DataSet empds11 = new DataSet();
            empds11 = la.getUFontGroup(row["projectname"].ToString());
            if (empds11.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < empds11.Tables[0].Rows.Count; i++)
                    lboxusagefonts.Items[lboxusagefonts.Items.IndexOf(lboxusagefonts.Items.FindByValue(empds11.Tables[0].Rows[i]["Fonts"].ToString()))].Selected = true;
            }
            DataSet empds6 = new DataSet();
            empds6 = la.getLFontGroup(row["projectname"].ToString()); 
            if (empds6.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < empds6.Tables[0].Rows.Count; i++)
                    lboxFonts.Items[lboxFonts.Items.IndexOf(lboxFonts.Items.FindByValue(empds6.Tables[0].Rows[i]["Fonts"].ToString()))].Selected = true;
            }
            DataSet empds3 = new DataSet();
            empds3 = la.getDeliveryGroup(row["projectname"].ToString());
            if (empds3.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < empds3.Tables[0].Rows.Count; i++)
                    lboxdelivryType.Items[lboxdelivryType.Items.IndexOf(lboxdelivryType.Items.FindByValue(empds3.Tables[0].Rows[i]["delivery_type"].ToString()))].Selected = true;
            }
            DataSet empds2 = new DataSet();
            empds2 = la.getInputGroup(row["projectname"].ToString());
            if (empds2.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < empds2.Tables[0].Rows.Count; i++)
                    linputfile.Items[linputfile.Items.IndexOf(linputfile.Items.FindByValue(empds2.Tables[0].Rows[i]["inputid"].ToString()))].Selected = true;
            }
            DataSet empds4 = new DataSet();
            empds4 = la.getTaskGroup(row["projectname"].ToString());
            if (empds4.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < empds4.Tables[0].Rows.Count; i++)
                {
                   
                    if (s == "" || s == null)
                        s = empds4.Tables[0].Rows[i]["taskname"].ToString();
                    else
                        s = s + ',' + empds4.Tables[0].Rows[i]["taskname"].ToString();
                }
            }
            DataSet tn = new DataSet();
            tn = la.getTaskName(s.ToString());
            gv_soft1.DataSource = tn;
            gv_soft1.DataBind();
        }
    }
    public void getComplexReason(int id,string complex)
    {
        //if (id <= 10000)
        //{
        //    DataSet dsCost5 = la.GetComplexReason(id, complex);
        //    ListComplexReason.DataSource = dsCost5;
        //    ListComplexReason.DataValueField = dsCost5.Tables[0].Columns[0].ToString();
        //    ListComplexReason.DataTextField = dsCost5.Tables[0].Columns[2].ToString();
        //    ListComplexReason.DataBind();
        //}
        //else
        //{
        //    DataSet dsCost6 = nonLa.GetComplexReason(id.ToString(), complex);
        //    ListNewComplexReason.DataSource = dsCost6;
        //    ListNewComplexReason.DataValueField = dsCost6.Tables[0].Columns[0].ToString();
        //    ListNewComplexReason.DataTextField = dsCost6.Tables[0].Columns[2].ToString();
        //    ListNewComplexReason.DataBind();
        //}
        DataSet dsCost6 = nonLa.GetComplexReason(id.ToString(), complex);
        ListNewComplexReason.DataSource = dsCost6;
        ListNewComplexReason.DataValueField = dsCost6.Tables[0].Columns[0].ToString();
        ListNewComplexReason.DataTextField = dsCost6.Tables[0].Columns[2].ToString();
        ListNewComplexReason.DataBind();
    }
    
    public void getComplexReasonold(string complex)
    {
        DataSet dsCost5 = la.GetComplexReasonold(complex);
        ListComplexReason.DataSource = dsCost5;
        ListComplexReason.DataValueField = dsCost5.Tables[0].Columns[0].ToString();
        ListComplexReason.DataTextField = dsCost5.Tables[0].Columns[2].ToString();
        ListComplexReason.DataBind();
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void btnQuoteSave_Click(object sender, EventArgs e)
    {
        ArrayList al = new ArrayList();
        Hashtable val = null;
        Launch mobj = new Launch();
        try
        {
            bool chk;
            chk = true;
            foreach (GridViewRow grw in gv_pmodule.Rows)
            {
                val = new Hashtable();
                val.Add("ProjectName", ((TextBox)grw.FindControl("txt_name")).Text.Trim().ToString());
                val.Add("TASKNAME", ((TextBox)grw.FindControl("txt_des")).Text.Trim().ToString());
                val.Add("TIME_TAKEN", ((TextBox)grw.FindControl("txt_time")).Text.Trim().ToString());
                val.Add("LANG_COUNT", ((TextBox)grw.FindControl("txt_langcount")).Text.Trim().ToString());
                val.Add("PAGES_COUNT", ((TextBox)grw.FindControl("txt_totalpage")).Text.Trim().ToString());
                val.Add("TOTAL_PAGES", ((TextBox)grw.FindControl("txt_totalpage")).Text.Trim().ToString());
                val.Add("HOUR_RATE", ((TextBox)grw.FindControl("txt_hourrate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("PAGE_RATE", ((TextBox)grw.FindControl("txt_pagerate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("P_RATE", ((TextBox)grw.FindControl("txt_prate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("H_RATE", ((TextBox)grw.FindControl("txt_hrate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("Software_id", ((TextBox)grw.FindControl("txt_soft")).Text.Trim().ToString());
                CheckBox page = (CheckBox)grw.FindControl("chk_page"); //sender;
                bool status = page.Checked;
                int p;
                if (status == true)
                    p = 1;
                else
                    p = 0;
                val.Add("PAGEYN", p.ToString());
                CheckBox hour = (CheckBox)grw.FindControl("chk_hour"); //sender;
                bool status1 = hour.Checked;
                int h;
                if (status1 == true)
                    h = 1;
                else
                    h = 0;
                val.Add("HOURYN", h.ToString());
                al.Add(val);
                if (!page.Checked && !hour.Checked)
                {
                    chk = false;
                    break;
                }
            }
            if (chk == true)
            {
                string id;
                if (txtProjectTitle.Text != "")
                {
                    id = txtProjectTitle.Text;
                }
                else
                {
                    id = hfP_Name.Value;
                }
                string FinalCheck = "";
                //if (txtFinalCheck.Text == "")
                //    FinalCheck = Session["fname"].ToString() + ' ' + Session["sname"].ToString();
                //else
                    FinalCheck = Session["fname"].ToString() + ' ' + Session["sname"].ToString();

                DataSet dp = new DataSet();
                dp = la.GetProid(id.ToString());
                int pro_id = Convert.ToInt16(dp.Tables[0].Rows[0]["Pro_id"].ToString());

                string inproc = "spUpdateQuote_LP";
                string[,] pname ={
                {"@QUOTEREMARKS",txtquoteremark.Text},{"@FINALCHECK",FinalCheck.ToString()},
                {"@PROJECT_CO",txtProjectCo.Text},{"@LP_ID",pro_id.ToString()},
                {"@IsExist","Output"},{"@employee_id",Session["employeeid"].ToString()}};
                int val3 = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);

                if (pro_id > 511)
                {
                    if (!mobj.Update_LaunchQuote(al))
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
                }
                else
                {
                    if (!mobj.Update_ProjectModule(al))
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Please Check Page or Hour based...');</script>");
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mobj = null; }

    }

    private void LoadControl(DataSet pds)
    {
        btnQuoteSave.Text = "Update";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void clear()
    {
        btnQuoteSave.Text = "Save";
    }

    protected void cmd_New_Project_Click(object sender, EventArgs e)
    {
        Response.Redirect("LaunchPage.aspx?q=lnkFileInfo", true);
        this.showPanel(tabLaunchDetails);
    }

    protected void cmd_Save_Project_Click(object sender, EventArgs e)
    {
        string msg = "";
        ArrayList al = new ArrayList();
        Hashtable val1 = null;
        ArrayList al1 = new ArrayList();
        Hashtable val2 = null;
        try
        {
            string id;
            if (txtProjectTitle.Text != "")
                id = txtProjectTitle.Text;
            else if (hfP_ID.Value != "")
                id = hfP_Name.Value.Trim();
            else
                id = "";
            if (id != "")
            {
                int ytr;
                if (CheckYTR.Checked)
                    ytr = 1;
                else
                    ytr = 0;
                string tdate, sdate;

                if (txtsource.Text != "")
                    sdate = DateTime.Parse(txtsource.Text.Trim()).ToString("MM/dd/yyyy");
                else
                    sdate = "";
                if (txttarget.Text != "")
                    tdate = DateTime.Parse(txttarget.Text.Trim()).ToString("MM/dd/yyyy");
                else
                    tdate = "";
                int x = 0;
                for (int j = 0; j < lboxlangused.Items.Count; j++)
                {
                        x = x + 1;
                }
                string groupitem1 = "";
                for (int j = 0; j < lboxlangused.Items.Count; j++)
                {
                    //if (lboxlang.Items[j].Selected == true)
                    //{
                        if (groupitem1 == "")
                            groupitem1 = lboxlangused.Items[j].Value;
                        else
                            groupitem1 = groupitem1 + ',' + lboxlangused.Items[j].Value;
                    //}
                }
                string deliverytype1 = "";
                for (int j = 0; j < lboxdelivryType.Items.Count; j++)
                {
                    if (lboxdelivryType.Items[j].Selected == true)
                    {
                        if (deliverytype1 == "")
                            deliverytype1 = lboxdelivryType.Items[j].Value;
                        else
                            deliverytype1 = deliverytype1 + ',' + lboxdelivryType.Items[j].Value;
                    }
                }
                string ComplexReason = "";
                for (int j = 0; j < ListComplexReason.Items.Count; j++)
                {
                    if (ListComplexReason.Items[j].Selected == true)
                    {
                        if (ComplexReason == "")
                            ComplexReason = ListComplexReason.Items[j].Value;
                        else
                            ComplexReason =  ComplexReason  + ',' +  ListComplexReason.Items[j].Value ;
                    }
                }
                string path = string.Empty;
                //if (Session["employeeid"].ToString() == "2461")
                //{
                //    using (new Impersonator(userName, domain, password))
                //    {

                string filename = MailUpload.FileName;
                string ext = Path.GetExtension(MailUpload.PostedFile.FileName);
                if (filename == "" && lblMail.Text != "")
                    path = lblMail.Text;
                else if (filename == "" && lblMail.Text == "")
                    path = lblMail.Text;
                else
                {
                    filename = MailUpload.FileName;
                    if (!Directory.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + DateTime.Now.ToString("MMMM yyyy")))
                        Directory.CreateDirectory(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + DateTime.Now.ToString("MMMM yyyy"));
                    path = ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + DateTime.Now.ToString("MMMM yyyy") + "\\" + Regex.Replace(id.ToString().Trim(), "[^a-zA-Z0-9_]+", " ").Trim();
                    System.IO.File.Delete(path + ".doc");
                    System.IO.File.Delete(path + ".docx");
                    MailUpload.PostedFile.SaveAs(path + ext.ToString());
                    lblMail.Text = path + ext.ToString();
                }
                //    }
                //}
                //else
                //{
                    //path= "";
                //}
                string inproc = "SP_update_FileInfo";
                string[,] pname ={
                    {"@Projectname",id},{"@SOURCEDATE",sdate},{"@TARGETDATE",tdate},{"@YTRYN",ytr.ToString()},
                    {"@LANG_COUNT",x.ToString()},
                    {"@OTHER_FONTS",txtFonts.Text},{"@OTHER_MISS_FONTS",txtMissFonts.Text}
                    ,{"@OTHERUSAGE_FONTS",txtOtherUsageFonts.Text}
                    ,{"@MISS_FIG_LINK",txtfiglinks.Text},{"@TABLES",Dropnooftables.Text}
                    ,{"@PROOF",txtproof.Text},{"@PRESS_PRINT",txtpress.Text},{"@Page_Size",txtpagesize.Text}
                    ,{"@COMPLEX_LEVEL",Dropcomplex.SelectedValue},{"@REASON",ComplexReason},
                    {"@NEW_REASON",""},{"@MailDetails",path},
                    {"@FILENAME_CONV",DropNameConv.SelectedValue},{"@bleed",txtBleed.Text},
                    {"@REMARKS",txtSplIns.Text},{"@IsExist","Output"},{"@Lang",groupitem1.Replace(",",", ")},{"@DELIVERYTYPE",deliverytype1},{"@MISSFONTS",""}
                    ,{"@CREATEDBY_FILE",Session["employeeid"].ToString()},{"@NoFiles",txtFiles.Text} ,{"@DELIVERYSOFT",""}                  
                };
                int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                Launch inobj = new Launch();
                string deliverytype = "";
                for (int j = 0; j < lboxdelivryType.Items.Count; j++)
                {
                    if (lboxdelivryType.Items[j].Selected == true)
                    {
                        if (deliverytype == "")
                            deliverytype = lboxdelivryType.Items[j].Value;
                        else
                            deliverytype = deliverytype + '-' + lboxdelivryType.Items[j].Value;
                    }
                }
                string langproc = "spAdd_DeliveryType_group";
                string[,] langname ={
                    {"@Projectname",id},{"@Delivery_Type",deliverytype}};
                int lang12 = this.oLaunch.ExcSP(langproc, langname, CommandType.StoredProcedure);
                //oLaunch.ExcuteProcedure("spAdd_DeliveryType_group", id + "," + deliverytype, "@projectname,@Delivery_Type", "varchar,varchar", "input,input", CommandType.StoredProcedure);
                foreach (GridViewRow grw in gv_lang.Rows)
                {
                    val1 = new Hashtable();
                    val1.Add("Pro_ID", ((TextBox)grw.FindControl("txt_name")).Text.Trim().ToString());
                    val1.Add("LANG_NAME", ((TextBox)grw.FindControl("txt_ID")).Text.Trim().ToString());
                    val1.Add("FILE_COUNT", ((TextBox)grw.FindControl("txt_Files")).Text.Trim().ToString());
                    val1.Add("PAGES_COUNT", ((TextBox)grw.FindControl("txt_Pages")).Text.Trim().ToString());
                    val1.Add("Taskname", ((TextBox)grw.FindControl("txt_task")).Text.Trim().ToString());
                    val1.Add("Software_ID", ((TextBox)grw.FindControl("txt_softid")).Text.Trim().ToString());
                    al.Add(val1);
                }
                inobj.Update_LangFile(al);
                foreach (GridViewRow grw in gv_images.Rows)
                {
                    val2 = new Hashtable();
                    val2.Add("Pro_ID", ((TextBox)grw.FindControl("txt_name")).Text.Trim().ToString());
                    val2.Add("LANG_NAME", ((TextBox)grw.FindControl("txt_ID")).Text.Trim().ToString());
                    val2.Add("Editable", ((TextBox)grw.FindControl("txt_edit")).Text.Trim().ToString());
                    val2.Add("Scanned", ((TextBox)grw.FindControl("txt_scan")).Text.Trim().ToString());
                    val2.Add("Non_Local_Image", ((TextBox)grw.FindControl("txt_nonlocal")).Text.Trim().ToString());
                    val2.Add("Images", ((TextBox)grw.FindControl("txt_images")).Text.Trim().ToString());
                    val2.Add("Taskname", ((TextBox)grw.FindControl("txt_task")).Text.Trim().ToString());
                    val2.Add("Software_ID", ((TextBox)grw.FindControl("txt_softid")).Text.Trim().ToString());
                    al1.Add(val2);
                }
                inobj.Update_ImageFile(al1);
                inobj.UpdateLaunchQuote(id.ToString(),Convert.ToInt16(x.ToString()));
                inobj.getQuoteValue(id);
                inobj.UpdateFonts(id);
                if (txtFonts.Text != "")
                {
                    DataSet ft = new DataSet();
                    DataSet ck = new DataSet();
                    ft = la.getFonts(txtFonts.Text);
                    if (ft.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ft.Tables[0].Rows.Count; i++)
                        {
                            string fonts = ft.Tables[0].Rows[i]["Fonts"].ToString();
                            ck = la.Chkmissfonts(fonts);
                            if (ck == null)
                            {
                                inobj.insertmissfonts(fonts);
                            }
                        }
                    }
                }
                if (txtMissFonts.Text != "")
                {
                    DataSet mft = new DataSet();
                    DataSet ds1 = new DataSet();
                    mft = la.getFonts(txtMissFonts.Text);
                    if (mft.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < mft.Tables[0].Rows.Count; i++)
                        {
                            ds1 = la.Chkmissfonts(mft.Tables[0].Rows[i]["Fonts"].ToString());
                            if (ds1 == null)
                            {
                                inobj.insertmissfonts(mft.Tables[0].Rows[i]["Fonts"].ToString());
                            }
                        }
                    }
                }
                if (txtOtherUsageFonts.Text != "")
                {
                    DataSet uft = new DataSet();
                    DataSet ds2 = new DataSet();
                    uft = la.getFonts(txtMissFonts.Text);
                    if (uft.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < uft.Tables[0].Rows.Count; i++)
                        {
                            ds2 = la.Chkmissfonts(uft.Tables[0].Rows[i]["Fonts"].ToString());
                            if (ds2 == null)
                            {
                                inobj.insertmissfonts(uft.Tables[0].Rows[i]["Fonts"].ToString());
                            }
                        }
                    }
                }
                if (val == 1)
                {
                    msg = "Updated Successfully";

                }
                else if (val == 0)
                    msg = "Job ID Already Exists";
            }
            else
            {
                msg = "Select Project Details!..";
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            //throw ex;
        }
        finally
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
        }
    }

    protected void btnClearfile_Click(object sender, EventArgs e)
    {
        ClearJobDetails();
    }
    protected void btnCustQuote_Click(object sender, EventArgs e)
    {
        LaunchDSnew Lads = new LaunchDSnew();
        LaunchQuoteDesc LaQd = new LaunchQuoteDesc();
        Launch la = new Launch();
        rep = new ReportDocument();
        CrystalReportViewer1.Visible = true;
        try
        {
            string id;
            if (hfP_Name.Value != "")
                id = hfP_Name.Value;
            else
                id = txtProjectTitle.Text;
            if (id != "")
            {
                Lads = la.ProductivityDetails("spGet_LaunchReport", new string[,] { { "@projectname", id } });
                LaQd = la.LaunchQuoteDesc("SpGetQuoteDesc", new string[,] { { "@projectname", id } });
                DataRow r = Lads.Tables[1].Rows[0];
                if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                {
                    rep = new ReportDocument();

                    rep.FileName = Server.MapPath("~/LaunchReport/LaunchQuote.rpt");
                    rep.SetDatabaseLogon("sa", "masterkey");
                    rep.SetDataSource(Lads.Tables[1]);
                    subRep1 = rep.Subreports[0];
                    subRep1.SetDataSource(LaQd.Tables[1]);
                    CrystalReportViewer1.ReportSource = rep;
                    CrystalReportViewer1.DataBind();
                    string filename = "Quote_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                    rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ','_'));
                    //Response.ContentType = "Application/pdf";
                   
                }
            }
        }
        catch (Exception ex)
        { }
        finally
        {

        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        LaunchDSnew Lads = new LaunchDSnew();
        LaunchQuery Laq = new LaunchQuery();
        LaunchQuote LaQu = new LaunchQuote();
        LaunchLang LaLg = new LaunchLang();
        Launch la = new Launch();
        rep = new ReportDocument();
        CrystalReportViewer1.Visible = true;
        try
        {
            string id;
            if (hfP_Name.Value != "")
                id = hfP_Name.Value;
            else
                id = txtProjectTitle.Text;
            if (id != "")
            {
                Lads = la.ProductivityDetails("spGet_LaunchReport", new string[,] { { "@projectname", id } });
                DataRow r = Lads.Tables[1].Rows[0];
                if (r["TASK"].ToString() == "TE")
                {
                    Laq = la.LaunchQueryDetails("spGetQuery_Report", new string[,] { { "@projectname", id } });
                    LaQu = la.LaunchQuoteDetails("spGetTask_Report", new string[,] { { "@projectname", id } });
                    LaLg = la.LaunchLangDetails("spGetLang_Report", new string[,] { { "@projectname", id } });
                    
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LaunchFormTE.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        CrystalReportViewer1.ReportSource = rep;
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));

                    }
                }
                else if (r["TASK"].ToString() == "File Conversion")
                {
                    Laq = la.LaunchQueryDetails("spGetQuery_Report", new string[,] { { "@projectname", id } });
                    LaQu = la.LaunchQuoteDetails("spGetTask_Report", new string[,] { { "@projectname", id } });
                    
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LaunchFormFileConv.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(Laq.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(LaQu.Tables[1]);
                        CrystalReportViewer1.ReportSource = rep;
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));

                    }
                }
                else if (r["TASK"].ToString() == "DQA")
                {
                    Laq = la.LaunchQueryDetails("spGetQuery_Report", new string[,] { { "@projectname", id } });
                    LaQu = la.LaunchQuoteDetails("spGetTask_Report", new string[,] { { "@projectname", id } });
                    LaLg = la.LaunchLangDetails("spGetLang_Report", new string[,] { { "@projectname", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LaunchFormDQA.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        CrystalReportViewer1.ReportSource = rep;
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
                else 
                {
                    Laq = la.LaunchQueryDetails("spGetQuery_Report", new string[,] { { "@projectname", id } });
                    LaQu = la.LaunchQuoteDetails("spGetTask_Report", new string[,] { { "@projectname", id } });
                    LaLg = la.LaunchLangDetails("spGetLang_Report", new string[,] { { "@projectname", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LaunchForm.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        CrystalReportViewer1.ReportSource = rep;
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + r["PROJECTNAME"].ToString();
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
            }
        }
        catch (Exception ex)
        { }
        finally
        {

        }
    }

    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (drpProjectcustomer.SelectedItem.Value == "0") sMessage += i++ + ". Select a Customer\\r\\n";
        if (txtProjectTitle.Text.Trim() == "") sMessage += i++ + ". Enter Project Name\\r\\n";
        if (dropnoFTP.SelectedItem.Value == "0") sMessage += i++ + ". Select No. of Folders\\r\\n";
        if (lboxtask.GetSelectedIndices().Length == 0) sMessage += i++ + ". Select Project Task\\r\\n";
        if (!chkNewYTC.Checked)
        {
            if (txtNewdueFromdate.Text == "")
            {
                sMessage += i++ + ". Enter Due Date or (YTC)\\r\\n";
            }
        }
        if (i > 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + sMessage + "');</script>");
            return false;
        }
        return true;
    }
    private bool validateScreenLPFileinfo()
    {
        int i = 1;
        string sMessage = "";
        if (ListNewComplexReason.SelectedValue == "") sMessage += i++ + ". Select a Complex Reason\\r\\n";
        if (i > 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + sMessage + "');</script>");
            return false;
        }
        return true;
    }
    private bool validateScreenLP()
    {
        int i = 1;
        string sMessage = "";
        if (drpNewProjectcustomer.SelectedItem.Value == "0") sMessage += i++ + ". Select a Customer\\r\\n";
        if (txtNewProjectTitle.Text.Trim() == "") sMessage += i++ + ". Enter Project Name\\r\\n";
        if (dropNewNoofFolder.SelectedItem.Value == "0") sMessage += i++ + ". Select No. of Folders\\r\\n";
        if (lboxNewtask.GetSelectedIndices().Length == 0) sMessage += i++ + ". Select Project Task\\r\\n";
        if (i > 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + sMessage + "');</script>");
            return false;
        }
        return true;
    }

    protected void btnJobInfo_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
        {
            if (validateScreen())
            {
                int ytc;
                if (chkYTC.Checked)
                    ytc = 1;
                else
                    ytc = 0;
                int duetime;
                if (chkDueTime.Checked)
                    duetime = 1;
                else
                    duetime = 0;
                int dueDate;
                if (chkDueDate.Checked)
                    dueDate = 1;
                else
                    dueDate = 0;
                string ddate = "";
                if (txtdueFromdate.Text != "")
                    ddate = DateTime.Parse(txtdueFromdate.Text.Trim()).ToString("MM/dd/yyyy");
                else
                    ddate = "";
                string dTodate = "";
                if (txtdueTodate.Text != "")
                    dTodate = DateTime.Parse(txtdueTodate.Text.Trim()).ToString("MM/dd/yyyy");
                else
                    dTodate = "";
                string filepath = "";
                filepath = txtFilepath.Text;
                string taskitem1 = "";
                for (int j = 0; j < lboxtask.Items.Count; j++)
                {
                    if (lboxtask.Items[j].Selected == true)
                    {
                        if (taskitem1 == "")
                            taskitem1 = lboxtask.Items[j].Value;
                        else
                            taskitem1 = taskitem1 + ',' + lboxtask.Items[j].Value;
                    }
                }

                string inputitem1 = "";
                for (int j = 0; j < linputfile.Items.Count; j++)
                {
                    if (linputfile.Items[j].Selected == true)
                    {
                        if (inputitem1 == "")
                            inputitem1 = linputfile.Items[j].Value;
                        else
                            inputitem1 = inputitem1 + ',' + linputfile.Items[j].Value;
                    }
                }
                string formatitem1 = "";
                for (int j = 0; j < lboxformat.Items.Count; j++)
                {
                    if (lboxformat.Items[j].Selected == true)
                    {
                        if (formatitem1 == "")
                            formatitem1 = lboxformat.Items[j].Value;
                        else
                            formatitem1 = formatitem1 + ',' + lboxformat.Items[j].Value;
                    }
                }

                if (btnJobInfo.Text == "Save")
                {
                    string inproc = "SP_insert_JobInfo";

                    string[,] pname ={
                
                    {"@PROJECTNAME",txtProjectTitle.Text },{"@PROJECTID",txtProjectid.Text },{"@CUST_ID",drpProjectcustomer.SelectedValue},
                    {"@Location",DropLocation.SelectedValue},
                    {"@FILE_PATH",filepath},{"@NOOF_PACKAGE",dropnoFTP.SelectedValue},{"@PACK_SIZE",txtSize.Text},{"@PACK_SIZE_BYTES",DropSizeBytes.SelectedValue},
                    {"@FTPYN",dropIpFTP.SelectedValue},{"@PLATFORM",dropSwPlat.SelectedValue}
                    ,{"@YTCYN",ytc.ToString()},{"@DUE_DATE",ddate},{"@Due_Timefrom",DropDueTimeFrom.SelectedValue},{"@Due_MINFROM",DropDueMinFrom.SelectedValue},{"@TIME_ZoneFROM",DropDueTimeZoneFrom.SelectedValue},
                     {"@Due_TimeTO",DropDueTimeTo.SelectedValue},{"@Due_MINTO",DropDueMinTo.SelectedValue},{"@TIME_ZoneTO",DropDueTimeZoneTo.SelectedValue},
                    {"@PROJECTEDITOR",txtProjectEditor.Text},{"@IsExist","OUTPUT"},{"@INPUT",inputitem1.Replace(",",", ")},{"@task",taskitem1.Replace(",",", ")},
                    {"@FORMAT",formatitem1.Replace(",",", ")},{"@CREATEDBY_JOB",Session["employeeid"].ToString()},{"@DUETIMEFROM_IST",TextBox2.Text},{"@DUETIMETO_IST",TextBox1.Text},{"@SourceType",DropSource.SelectedValue}
                        ,{"@DUETIMEYN",duetime.ToString()},{"@DUEDateYN",dueDate.ToString()},{"@DUE_DATETo",dTodate}
                    };
                    int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                    Launch inobj = new Launch();
                    string pn = "'" + txtProjectTitle.Text + "'";
                    string taskitem = "";
                    for (int j = 0; j < lboxtask.Items.Count; j++)
                    {
                        if (lboxtask.Items[j].Selected == true)
                        {
                            if (taskitem == "")
                                taskitem = lboxtask.Items[j].Value;
                            else
                                taskitem = taskitem + '-' + lboxtask.Items[j].Value;
                        }
                    }
                     string taskproc = "spAdd_JobTask_group";
                     string[,] tname ={{"@PROJECTNAME",txtProjectTitle.Text },{"@taskname",taskitem }};
                     int task12 = this.oLaunch.ExcSP(taskproc, tname, CommandType.StoredProcedure);

                    string formatitem = "";
                    for (int j = 0; j < lboxformat.Items.Count; j++)
                    {
                        if (lboxformat.Items[j].Selected == true)
                        {
                            if (formatitem == "")
                                formatitem = lboxformat.Items[j].Value;
                            else
                                formatitem = formatitem + '-' + lboxformat.Items[j].Value;
                        }
                    }
                    string formatproc = "spAdd_JobFormat_group";
                    string[,] forname ={ { "@PROJECTNAME", txtProjectTitle.Text }, { "@format_id", formatitem } };
                    int format12 = this.oLaunch.ExcSP(formatproc, forname, CommandType.StoredProcedure);
                    string inputitem = "";
                    for (int j = 0; j < linputfile.Items.Count; j++)
                    {
                        if (linputfile.Items[j].Selected == true)
                        {
                            if (inputitem == "")
                                inputitem = linputfile.Items[j].Value;
                            else
                                inputitem = inputitem + '-' + linputfile.Items[j].Value;
                        }
                    }
                    string inputproc = "spAdd_JobInput_group";
                    string[,] inname ={ { "@PROJECTNAME", txtProjectTitle.Text }, { "@inputid", inputitem } };
                    int input12 = this.oLaunch.ExcSP(inputproc, inname, CommandType.StoredProcedure);
                    //oLaunch.ExcuteProcedure("spAdd_JobInput_group", pn.ToString() + "," + inputitem, "@projectname,@inputid", "varchar,varchar", "input,input", CommandType.StoredProcedure);
                    la.deleteQuote(txtProjectTitle.Text);
                    
                    ArrayList a = new ArrayList();
                    Hashtable s = new Hashtable();
                    foreach (GridViewRow grs in gv_Soft.Rows)
                    {
                        ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
                        ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
                        TextBox task = (TextBox)grs.FindControl("txt_task");
                        string soft;
                        int r = 0;
                        for (int j = 0; j < lboxSoft.Items.Count; j++)
                        {
                            if (lboxSoft.Items[j].Selected == true)
                            {
                                soft = lboxSoft.Items[j].Value;
                                s = new Hashtable();
                                s.Add("ProjectName", txtProjectTitle.Text);
                                s.Add("TaskName", ((TextBox)grs.FindControl("txt_task")).Text.Trim().ToString());
                                s.Add("Software_id", soft);
                                la.insertQuote(txtProjectTitle.Text, task.Text, Convert.ToInt16(soft.ToString()));
                                r = r + 1;

                                if (r == 1)
                                {
                                    for (int d = 0; d < lboxVer.Items.Count; d++)
                                    {
                                        if (lboxVer.Items[d].Selected == true)
                                        {
                                            ver1 = lboxVer.Items[d].Value;
                                            break;
                                        }
                                        else if (lboxVer.Items[d].Selected == true)
                                        {
                                            ver1 = lboxVer.Items[d].Value;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int d = 0; d < lboxVer.Items.Count; d++)
                                    {
                                        if (lboxVer.Items[d].Selected == true)
                                        {
                                            ver1 = lboxVer.Items[d].Value;
                                        }
                                    }
                                }
                                s.Add("Version_id", ver1);
                                a.Add(s);
                            }
                        }
                    }
                    la.deletetask(txtProjectTitle.Text);
                    inobj.Insert_Software(a);
                    inobj.UpdateSoft(txtProjectTitle.Text);
                    inobj.UpdateDelivery(txtProjectTitle.Text);
                    if (val == 1)
                    {
                        msg = "Inserted Successfully";
                        btnJobInfo.Text = "Update";
                    }
                    else if (val == 0)
                        msg = "Project Name  Already Exists";
                }
                else
                {
                    string inproc = "SP_update_JobInfo";

                    string[,] pname ={
                
                    {"@PROJECTNAME",txtProjectTitle.Text },{"@PROJECTID",txtProjectid.Text },{"@CUST_ID",drpProjectcustomer.SelectedValue},
                    {"@Location",DropLocation.SelectedValue},
                    {"@FILE_PATH",filepath},{"@NOOF_PACKAGE",dropnoFTP.SelectedValue},{"@PACK_SIZE",txtSize.Text},{"@PACK_SIZE_BYTES",DropSizeBytes.SelectedValue},
                    {"@FTPYN",dropIpFTP.SelectedValue},{"@PLATFORM",dropSwPlat.SelectedValue}
                    ,{"@YTCYN",ytc.ToString()},{"@DUE_DATE",ddate},{"@Due_Timefrom",DropDueTimeFrom.SelectedValue},{"@Due_MINFROM",DropDueMinFrom.SelectedValue},{"@TIME_ZoneFROM",DropDueTimeZoneFrom.SelectedValue},
                        {"@Due_TimeTO",DropDueTimeTo.SelectedValue},{"@Due_MINTO",DropDueMinTo.SelectedValue},{"@TIME_ZoneTO",DropDueTimeZoneTo.SelectedValue},
                    {"@PROJECTEDITOR",txtProjectEditor.Text},{"@IsExist","OUTPUT"},{"@INPUT",inputitem1.Replace(",",", ")},{"@task",taskitem1.Replace(",",", ")},
                    {"@FORMAT",formatitem1.Replace(",",", ")},{"@CREATEDBY_JOB",Session["employeeid"].ToString()},{"@DUETIMEFROM_IST",TextBox2.Text},{"@DUETIMETO_IST",TextBox1.Text},{"@SourceType",DropSource.SelectedValue}
                     ,{"@DUETIMEYN",duetime.ToString()},{"@DUEDateYN",dueDate.ToString()},{"@DUE_DATETo",dTodate}};
                    int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);

                    Launch inobj = new Launch();
                    string taskitem = "";
                    for (int j = 0; j < lboxtask.Items.Count; j++)
                    {
                        if (lboxtask.Items[j].Selected == true)
                        {
                            if (taskitem == "")
                                taskitem = lboxtask.Items[j].Value;
                            else
                                taskitem = taskitem + '-' + lboxtask.Items[j].Value;
                        }
                    }
                    string taskproc = "spAdd_JobTask_group";
                    string[,] tname ={ { "@PROJECTNAME", txtProjectTitle.Text }, { "@taskname", taskitem } };
                    int task12 = this.oLaunch.ExcSP(taskproc, tname, CommandType.StoredProcedure);
                    string formatitem = "";
                    for (int j = 0; j < lboxformat.Items.Count; j++)
                    {
                        if (lboxformat.Items[j].Selected == true)
                        {
                            if (formatitem == "")
                                formatitem = lboxformat.Items[j].Value;
                            else
                                formatitem = formatitem + '-' + lboxformat.Items[j].Value;
                        }
                    }
                    string formatproc = "spAdd_JobFormat_group";
                    string[,] forname ={ { "@PROJECTNAME", txtProjectTitle.Text }, { "@format_id", formatitem } };
                    int format12 = this.oLaunch.ExcSP(formatproc, forname, CommandType.StoredProcedure);

                    string inputitem = "";
                    for (int j = 0; j < linputfile.Items.Count; j++)
                    {
                        if (linputfile.Items[j].Selected == true)
                        {
                            if (inputitem == "")
                                inputitem = linputfile.Items[j].Value;
                            else
                                inputitem = inputitem + '-' + linputfile.Items[j].Value;
                        }
                    }
                    string inputproc = "spAdd_JobInput_group";
                    string[,] inname ={ { "@PROJECTNAME", txtProjectTitle.Text }, { "@inputid", inputitem } };
                    int input12 = this.oLaunch.ExcSP(inputproc, inname, CommandType.StoredProcedure);
                    //la.deleteQuote(txtProjectTitle.Text);
                    la.UpdateQuoteStatus(txtProjectTitle.Text, 0);
                    ArrayList a = new ArrayList();
                    Hashtable s = new Hashtable();
                    foreach (GridViewRow grs in gv_Soft.Rows)
                    {
                        ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
                        ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
                        TextBox task = (TextBox)grs.FindControl("txt_task");
                        string soft;
                        int r = 0;
                        for (int j = 0; j < lboxSoft.Items.Count; j++)
                        {
                            if (lboxSoft.Items[j].Selected == true)
                            {
                                soft = lboxSoft.Items[j].Value;
                                s = new Hashtable();
                                s.Add("ProjectName", txtProjectTitle.Text);
                                s.Add("TaskName", ((TextBox)grs.FindControl("txt_task")).Text.Trim().ToString());
                                s.Add("Software_id", soft);
                                DataSet ds2 = new DataSet();
                                ds2 = la.GetLaunchQuote(txtProjectTitle.Text, task.Text, Convert.ToInt16(soft.ToString()));
                                if (ds2 != null)
                                {
                                    la.UpdateQuoteStatus1(txtProjectTitle.Text, task.Text, Convert.ToInt16(soft.ToString()),1);
                                }
                                else
                                la.insertQuote(txtProjectTitle.Text, task.Text, Convert.ToInt16(soft.ToString()));
                                
                                        r = r + 1;
                                
                                if (r == 1)
                                {
                                    for (int d = 0; d < lboxVer.Items.Count; d++)
                                    {
                                        if (lboxVer.Items[d].Selected == true)
                                        {
                                            ver1 = lboxVer.Items[d].Value;
                                            break;
                                        }
                                        else if (lboxVer.Items[d].Selected == true)
                                        {
                                            ver1 = lboxVer.Items[d].Value;
                                        }
                                        
                                    }
                                }
                                else
                                {
                                    for (int d = 0; d < lboxVer.Items.Count; d++)
                                    {
                                        if (lboxVer.Items[d].Selected == true)
                                        {
                                            ver1 = lboxVer.Items[d].Value;
                                        }
                                    }
                                }
                                s.Add("Version_id", ver1);
                                a.Add(s);
                            }
                        }
                    }
                    la.deleteQuote(txtProjectTitle.Text);
                    la.deletetask(txtProjectTitle.Text);
                    //la.deletelang(txtProjectTitle.Text);
                    inobj.Insert_Software(a);
                    inobj.UpdateSoft(txtProjectTitle.Text);
                    inobj.UpdateDelivery(txtProjectTitle.Text);
                    if (val == 1)
                        msg = "Updated Successfully";
                    else
                        msg = "Project Name  Already Exists";
                }
                
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            //throw ex;
        }
        finally
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
        }
    }

    protected void Dropcomplex_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id;
        if (hfP_Name.Value != "")
            id = hfP_Name.Value;
        else
            id = txtProjectTitle.Text;
        if (id.ToString() != "")
        {
            DataSet dp = new DataSet();
            dp = la.GetProid(id.ToString());
            if (Convert.ToInt16(dp.Tables[0].Rows[0]["Pro_id"].ToString()) < 381)
                getComplexReasonold(Dropcomplex.SelectedValue);
            else
                getComplexReason(Convert.ToInt16(dp.Tables[0].Rows[0]["Pro_id"].ToString()), Dropcomplex.SelectedValue);
        }
    }
    protected void lboxlang_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void DropLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        getloc_timezone(Convert.ToInt16(DropLocation.SelectedValue));
    }
    public void getloc_timezone(int locid)
    {
        DataSet ds8 = la.GetTimeZone(locid);
        DropDueTimeZoneFrom.DataSource = ds8;
        DropDueTimeZoneFrom.DataValueField = ds8.Tables[0].Columns[2].ToString();
        DropDueTimeZoneFrom.DataTextField = ds8.Tables[0].Columns[2].ToString();
        DropDueTimeZoneFrom.DataBind();
        DropDueTimeZoneTo.DataSource = ds8;
        DropDueTimeZoneTo.DataValueField = ds8.Tables[0].Columns[2].ToString();
        DropDueTimeZoneTo.DataTextField = ds8.Tables[0].Columns[2].ToString();
        DropDueTimeZoneTo.DataBind();
        TimeFormat1();
        TimeFormat();
    }
    protected void lboxtask_SelectedIndexChanged(object sender, EventArgs e)
    {
        Launch DSql1 = new Launch();
        string format = "";
        for (int j = 0; j < lboxtask.Items.Count; j++)
        {
            if (lboxtask.Items[j].Selected == true)
            {
                if (format == "")
                    format = lboxtask.Items[j].Value;
                else
                    format = format + '-' + lboxtask.Items[j].Value;
            }
        }
        DataSet tn = new DataSet();
        tn = la.getTaskName(format.Replace("-", ","));
        gv_Soft.DataSource = tn;
        gv_Soft.DataBind();
        DataSet Dst4 = new DataSet();
        Dst4 = DSql1.GetFormats(format.Replace("-", "','"));
        lboxformat.DataTextField = "format_name";
        lboxformat.DataValueField = "format_name";
        lboxformat.DataSource = Dst4;
        lboxformat.DataBind();
        if (Dst4.Tables[0].Rows[0]["taskname"].ToString() == "TE")
        {
            lblsource.Visible = true;
            DropSource.Visible = true;
            lboxformat.Visible = true;
            lblformat.Visible = true;
        }
        else if (Dst4.Tables[0].Rows[0]["taskname"].ToString() == "DTP" || Dst4.Tables[0].Rows[0]["taskname"].ToString() == "File Conversion")
        {
            lblsource.Visible = false;
            DropSource.Visible = false;
            lboxformat.Visible = true;
            lblformat.Visible = true;
        }
        else
        {
            lboxformat.Visible = false;
            lblformat.Visible = false;
            lblsource.Visible = false;
            DropSource.Visible = false;
        }
    }
    protected void btnQueries_Click(object sender, EventArgs e)
    {
        Launch la = new Launch();
        string msg = "";
        try
        {
            if (txtProjectTitle.Text != "")
            {
                if (txtQuery.Text != "")
                {
                    la.insertQueries(txtProjectTitle.Text, txtQuery.Text, txtQueryans.Text);
                    DataSet ds = new DataSet();
                    ds = la.GetQueries(txtProjectTitle.Text);
                    gv_Queries.DataSource = ds;
                    gv_Queries.DataBind();
                    txtQuery.Text = "";
                    txtQueryans.Text = "";
                    msg = "Queries Added..";
                }
                else
                    msg = "Please enter Queries..";
            }
            else
            {
                msg = "Select Project Details!..";
            }
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
    protected void drpProjectcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        getloc(Convert.ToInt16(drpProjectcustomer.SelectedValue));
        getloc_timezone(Convert.ToInt16(DropLocation.SelectedValue));
        TimeFormat();
    }
    public void getloc(int custid)
    {
        DataSet ds8 = la.GetLocationCust(custid);
        DropLocation.DataSource = ds8;
        DropLocation.DataValueField = ds8.Tables[0].Columns[3].ToString();
        DropLocation.DataTextField = ds8.Tables[0].Columns[4].ToString();
        DropLocation.DataBind();
    }

    public void TimeFormat()
    {
        string hrs = DropDueTimeFrom.SelectedValue;
        string min = DropDueMinFrom.SelectedValue;
        DataSet time = la.getTimeDetails(hrs, min, DropLocation.SelectedValue, DropDueTimeZoneFrom.SelectedValue);
        DataRow row = time.Tables[0].Rows[0];
        TextBox2.Text = row["Mins"].ToString();
    }
    public void TimeFormat1()
    { 
        string hrs = DropDueTimeTo.SelectedValue;
        string min = DropDueMinTo.SelectedValue;
        DataSet time = la.getTimeDetails(hrs, min, DropLocation.SelectedValue, DropDueTimeZoneTo.SelectedValue);
        DataRow row = time.Tables[0].Rows[0];
        TextBox1.Text = row["Mins"].ToString();
    }

    protected void DropDueMin_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat();
    }
    protected void DropDueTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat();
    }

    protected void exportExl_Click(object sender, ImageClickEventArgs e)
    {
        int i = 1;
        if (dtable4 != null && dtable4.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='14' align='center'><h4>Launch OverAll Report</h4></td><tr>");
            //sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Date</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Location</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>Project Editor</b></td><td bgcolor='silver'><b>Task</b></td><td bgcolor='silver'><b>Platform</b></td><td bgcolor='silver'><b>No.of Pages</b></td><td bgcolor='silver'><b>Target Recived Date</b></td><td bgcolor='silver'><b>Committed Date From</b></td><td bgcolor='silver'><b>Committed Date To</b></td><td bgcolor='silver'><b>Committed Time From</b></td><td bgcolor='silver'><b>Committed Time To</b></td><td bgcolor='silver'><b>Committed Time_IST From</b></td><td bgcolor='silver'><b>Committed Time_IST To</b></td><td bgcolor='silver'><b>Status</b></td><td bgcolor='silver'><b>File_Path</b></td><td bgcolor='silver'><b>Remarks</b></td></tr>");
            //foreach (DataRow r in dtable4.Rows)
            //{
            //    sbData.Append("<tr valign='top'>");
            //    sbData.Append("<td>" + i + "</td>");
            //    sbData.Append("<td>" + r["Date"] + "</td>");
            //    sbData.Append("<td>" + r["Jobid"] + "</td>");
            //    sbData.Append("<td>" + r["Cust_name"] + "</td>");
            //    sbData.Append("<td>" + r["Location_name"] + "</td>");
            //    sbData.Append("<td>" + r["Projectname"] + "</td>");
            //    sbData.Append("<td>" + r["ProjectEditor"] + "</td>");
            //    sbData.Append("<td>" + r["Task"] + "</td>");
            //    sbData.Append("<td>" + r["Platform"] + "</td>");
            //    sbData.Append("<td>" + r["pages"] + "</td>");
            //    sbData.Append("<td>" + r["Recivdate"] + "</td>");
            //    sbData.Append("<td>" + r["DueDateFrom"] + "</td>");
            //    sbData.Append("<td>" + r["DueDateTo"] + "</td>");
            //    sbData.Append("<td>" + r["DueTimeFrom"] + "</td>");
            //    sbData.Append("<td>" + r["DueTimeTo"] + "</td>");
            //    sbData.Append("<td>" + r["DueTimeFrom_IST"] + "</td>");
            //    sbData.Append("<td>" + r["DueTimeTo_IST"] + "</td>");
            //    if (r["delivery"].ToString() == "P")
            //        sbData.Append("<td  bgcolor='orange'>" + r["delivery"] + "</td>");
            //    else if (r["delivery"].ToString() == "C")
            //        sbData.Append("<td  bgcolor='Gray'>" + r["delivery"] + "</td>");
            //    else if (r["delivery"].ToString() == "WIP")
            //        sbData.Append("<td  bgcolor='LightGreen'>" + r["delivery"] + "</td>");
            //    else if (r["delivery"].ToString() == "Del")
            //        sbData.Append("<td  bgcolor='green'>" + r["delivery"] + "</td>");
            //    else
            //        sbData.Append("<td>" + r["delivery"] + "</td>");
            //    sbData.Append("<td>" + r["File_Path"] + "</td>");
            //    sbData.Append("<td>" + r["QuoteRemarks"] + "</td>");
            //    sbData.Append("</tr>");
            //    i = i + 1;
            //}
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>Project Editor</b></td><td bgcolor='silver'><b>Platform</b></td><td bgcolor='silver'><b>Pages</b></td><td bgcolor='silver'><b>Files</b></td><td bgcolor='silver'><b>Target Recived Date</b></td><td bgcolor='silver'><b>Committed Date From</b></td><td bgcolor='silver'><b>Committed Date To</b></td><td bgcolor='silver'><b>Committed Time From</b></td><td bgcolor='silver'><b>Committed Time To</b></td><td bgcolor='silver'><b>Committed Time_IST From</b></td><td bgcolor='silver'><b>Committed Time_IST To</b></td></tr>");
            foreach (DataRow r in dtable4.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["Jobid"] + "</td>");
                sbData.Append("<td>" + r["Cust_name"] + "</td>");
                sbData.Append("<td>" + r["Projectname"] + "</td>");
                sbData.Append("<td>" + r["ProjectEditor"] + "</td>");
                sbData.Append("<td>" + r["Platform"] + "</td>");
                sbData.Append("<td>" + r["Pages_count"] + "</td>");
                sbData.Append("<td>" + r["File_Count"] + "</td>");
                sbData.Append("<td>" + r["CREATED_DATE"] + "</td>");
                sbData.Append("<td>" + r["DUE_DATEFROM"] + "</td>");
                sbData.Append("<td>" + r["DUE_DATETO"] + "</td>");
                sbData.Append("<td>" + r["DueTimeFrom"] + "</td>");
                sbData.Append("<td>" + r["DueTimeTo"] + "</td>");
                sbData.Append("<td>" + r["DUETIMEFROM_IST"] + "</td>");
                sbData.Append("<td>" + r["DUETIMETO_IST"] + "</td>");
                sbData.Append("</tr>");
                i = i + 1;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Launch_OverAll_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();

        }
        //if (dtable != null && dtable.Rows.Count > 0)
        //{
        //    StringBuilder sbData = new StringBuilder();
        //    sbData.Append("<table border='1'>");
        //    sbData.Append("<tr valign='top'><td colspan='11' align='center'><h4>Launch Summary</h4></td><tr>");
        //    sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Job Number</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>Recieved Date</b></td><td bgcolor='silver'><b>Due Date</b></td><td bgcolor='silver'><b>Due Time From</b></td><td bgcolor='silver'><b>Due Time TO</b></td><td bgcolor='silver'><b>PlatForm</b></td><td bgcolor='silver'><b>No. of Pages</b></td><td bgcolor='silver'><b>File Path Info.</b></td></tr>");
        //    foreach (DataRow r in dtable.Rows)
        //    {
        //        sbData.Append("<tr valign='top'>");
        //        sbData.Append("<td>" + r["sl"] + "</td>");
        //        sbData.Append("<td>" + r["jobid"] + "</td>");
        //        sbData.Append("<td>" + r["cust_name"] + "</td>");
        //        sbData.Append("<td>" + r["Projectname"] + "</td>");
        //        sbData.Append("<td>" + r["Cur_date"] + "</td>");
        //        sbData.Append("<td>" + r["Due_date"] + "</td>");
        //        sbData.Append("<td>" + r["Due_Timefrom"] + "</td>");
        //        sbData.Append("<td>" + r["Due_TimeTo"] + "</td>");
        //        sbData.Append("<td>" + r["Platform"] + "</td>");
        //        sbData.Append("<td>" + r["Pages_Count"] + "</td>");
        //        sbData.Append("<td>" + r["File_Path"] + "</td>");
        //        sbData.Append("</tr>");
        //    }
        //    sbData.Append("</table>");
        //    Response.Clear();
        //    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Launch_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
        //    Response.ContentType = "application/ms-excel";
        //    //Response.ContentEncoding = Encoding.Unicode;
        //    //Response.BinaryWrite(Encoding.Unicode.GetPreamble());
        //    Response.Write(sbData.ToString());
        //    Response.End();
        //}
    }

    protected void btnFileInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("LaunchPage.aspx?q=new_job", true);
    }

    protected void DropDueTimeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat1();
    }
    protected void DropDueMinTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat1();
    }
    protected void DropDueTimeZoneFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat();
    }
    protected void DropDueTimeZoneTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat1();
    }
    protected void lboxdelivryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_Name.Value.Trim();
        else if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else
            id = "";
        string type = "";
        for (int j = 0; j < lboxdelivryType.Items.Count; j++)
        {
            if (lboxdelivryType.Items[j].Selected == true)
            {
                if (type == "")
                    type = lboxdelivryType.Items[j].Value;
                else
                    type = type + ',' + lboxdelivryType.Items[j].Value;
            }
        }
        if (type == "1" || type == "1,2" || type == "1,3")
        {
            lblSoft.Visible = true;
            gv_soft1.Visible = true;
            DataSet empds4 = new DataSet();
            empds4 = la.getTaskGroup(id.ToString());
            //empds4 = oLaunch.ExcuteSelectProcedure("spGet_taskGroups", id.ToString(), "@proname", "varChar", "input", CommandType.StoredProcedure);
            if (empds4.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < empds4.Tables[0].Rows.Count; i++)
                {

                    if (s == "" || s == null)
                        s = empds4.Tables[0].Rows[i]["taskname"].ToString();
                    else
                        s = s + ',' + empds4.Tables[0].Rows[i]["taskname"].ToString();
                }
            }
            DataSet tn = new DataSet();
            tn = la.getTaskName(s.ToString());
            gv_soft1.DataSource = tn;
            gv_soft1.DataBind();
        }
        else
        {
            lblSoft.Visible = false;
            gv_soft1.Visible = false;
        }
    }
    protected void chkDueTime_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDueTime.Checked)
        {
            lblFrom.Visible = true;
            lblTo.Visible = true;
            DropDueTimeTo.Visible = true;
            DropDueMinTo.Visible = true;
            DropDueTimeZoneTo.Visible = true;
            TextBox1.Visible = true;
        }
        else
        {
            lblFrom.Visible = false;
            lblTo.Visible = false;
            DropDueTimeTo.Visible = false;
            DropDueMinTo.Visible = false;
            DropDueTimeZoneTo.Visible = false;
            TextBox1.Visible = false;
        }
    }

    protected void btnUFontsadd_Click(object sender, EventArgs e)
    {
        string id;
        if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else if (hfP_ID.Value != "")
            id = hfP_Name.Value.Trim();
        else
            id = "";
        string fontitem6 = "";
        for (int j = 0; j < lboxusagefonts.Items.Count; j++)
        {
            if (lboxusagefonts.Items[j].Selected == true)
            {
                if (fontitem6 == "")
                    fontitem6 = lboxusagefonts.Items[j].Value;
                else
                    fontitem6 = fontitem6 + ',' + lboxusagefonts.Items[j].Value;
            }
        }
        DataSet au1 = new DataSet();
        DataSet au2 = new DataSet();
        au1 = la.UsedFonts(id.ToString(), fontitem6);
        if (au1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < au1.Tables[0].Rows.Count; i++)
            {
                string fonts = au1.Tables[0].Rows[i]["Fonts"].ToString();

                if (txtOtherUsageFonts.Text != "" )
                {
                    DataSet uft = new DataSet();
                    DataSet ds2 = new DataSet();
                    uft = la.getFonts(txtOtherUsageFonts.Text);
                    if (uft.Tables[0].Rows.Count > 0)
                    {
                        for (int x = 0; x < uft.Tables[0].Rows.Count; x++)
                        {
                            ds2 = la.Chkmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (ds2 == null)
                            {
                                la.insertmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                            au2 = la.Chkusagefonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (au2 == null)
                            {
                                la.insertusagefonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                        }
                    }
                }
                else
                {
                    au2 = la.Chkusagefonts(id.ToString(), fonts);
                    if (au2 == null)
                    {
                        la.insertusagefonts(id.ToString(), fonts);
                    }
                }
            }
        }
        if (txtOtherUsageFonts.Text != "" && fontitem6.ToString()=="")
        {
            DataSet uft = new DataSet();
            DataSet ds2 = new DataSet();
            uft = la.getFonts(txtOtherUsageFonts.Text);
            if (uft.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < uft.Tables[0].Rows.Count; x++)
                {
                    ds2 = la.Chkmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (ds2 == null)
                    {
                        la.insertmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                    au2 = la.Chkusagefonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (au2 == null)
                    {
                        la.insertusagefonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                }
            }
        }
        DataSet uf = new DataSet();
        uf = la.GetUsageFonts(id);
        lboxUFonts.DataTextField = "Fonts";
        lboxUFonts.DataValueField = "Fonts";
        lboxUFonts.DataSource = uf;
        lboxUFonts.DataBind();
        DataSet Dst11 = new DataSet();
        Dst11 = la.GetMissFonts();
        lboxusagefonts.DataTextField = "Fonts";
        lboxusagefonts.DataValueField = "Fonts";
        lboxusagefonts.DataSource = Dst11;
        lboxusagefonts.DataBind();
    }
    protected void btnUFontsRemove_Click(object sender, EventArgs e)
    {
        string id;
        if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else if (hfP_ID.Value != "")
            id = hfP_Name.Value.Trim();
        else
            id = "";
        string usedFonts = "";
        for (int j = 0; j < lboxUFonts.Items.Count; j++)
        {
            if (lboxUFonts.Items[j].Selected == true)
            {
                if (usedFonts == "")
                    usedFonts = lboxUFonts.Items[j].Value;
                else
                    usedFonts = usedFonts + ',' + lboxUFonts.Items[j].Value;
            }
        }
        DataSet du1 = new DataSet();
        du1 = la.UsedFonts(id.ToString(), usedFonts);
        if (du1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < du1.Tables[0].Rows.Count; i++)
            {
                string fonts = du1.Tables[0].Rows[i]["Fonts"].ToString();
                la.deleteusagefonts(id.ToString(), fonts);
            }
        }
        lboxUFonts.Items.Clear();
        DataSet uf1 = new DataSet();
        uf1 = la.GetUsageFonts(id);
        lboxUFonts.DataTextField = "Fonts";
        lboxUFonts.DataValueField = "Fonts";
        lboxUFonts.DataSource = uf1;
        lboxUFonts.DataBind();
    }
    protected void btnfontsadd_Click(object sender, EventArgs e)
    {
        string id;
        if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else if (hfP_ID.Value != "")
            id = hfP_Name.Value.Trim();
        else
            id = "";
        string fontitem9 = "";
        for (int j = 0; j < lboxFonts.Items.Count; j++)
        {
            if (lboxFonts.Items[j].Selected == true)
            {
                if (fontitem9 == "")
                    fontitem9 = lboxFonts.Items[j].Value;
                else
                    fontitem9 = fontitem9 + ',' + lboxFonts.Items[j].Value;
            }
        }
        DataSet at1 = new DataSet();
        DataSet at2 = new DataSet();
        at1 = la.UsedFonts(id.ToString(), fontitem9);
        if (at1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < at1.Tables[0].Rows.Count; i++)
            {
                string Tfonts = at1.Tables[0].Rows[i]["Fonts"].ToString();

                if (txtFonts.Text != "" )
                {
                    DataSet tft = new DataSet();
                    DataSet dsf = new DataSet();
                    tft = la.getFonts(txtFonts.Text);
                    if (tft.Tables[0].Rows.Count > 0)
                    {
                        for (int x = 0; x < tft.Tables[0].Rows.Count; x++)
                        {
                            dsf = la.Chkmissfonts(tft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (dsf == null)
                            {
                                la.insertmissfonts(tft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                            at2 = la.Chkfonts(id.ToString(), tft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (at2 == null)
                            {
                                la.insertfonts(id.ToString(), tft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                        }
                    }
                }
                else
                {
                    at2 = la.Chkfonts(id.ToString(), Tfonts);
                    if (at2 == null)
                    {
                        la.insertfonts(id.ToString(), Tfonts);
                    }
                }
            }
        }
        if (txtFonts.Text != "" && fontitem9.ToString()=="")
        {
            DataSet tft = new DataSet();
            DataSet dsf = new DataSet();
            tft = la.getFonts(txtFonts.Text);
            if (tft.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < tft.Tables[0].Rows.Count; x++)
                {
                    dsf = la.Chkmissfonts(tft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (dsf == null)
                    {
                        la.insertmissfonts(tft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                    at2 = la.Chkfonts(id.ToString(), tft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (at2 == null)
                    {
                        la.insertfonts(id.ToString(), tft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                }
            }
        }
        DataSet tf = new DataSet();
        tf = la.GetFonts(id);
        lboxTFonts.DataTextField = "Fonts";
        lboxTFonts.DataValueField = "Fonts";
        lboxTFonts.DataSource = tf;
        lboxTFonts.DataBind();
        DataSet Dst12 = new DataSet();
        Dst12 = la.GetMissFonts();
        lboxFonts.DataTextField = "Fonts";
        lboxFonts.DataValueField = "Fonts";
        lboxFonts.DataSource = Dst12;
        lboxFonts.DataBind();
    }
    protected void btnfontsdel_Click(object sender, EventArgs e)
    {
        string id;
        if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else if (hfP_ID.Value != "")
            id = hfP_Name.Value.Trim();
        else
            id = "";
        string TFont = "";
        for (int j = 0; j < lboxTFonts.Items.Count; j++)
        {
            if (lboxTFonts.Items[j].Selected == true)
            {
                if (TFont == "")
                    TFont = lboxTFonts.Items[j].Value;
                else
                    TFont = TFont + ',' + lboxTFonts.Items[j].Value;
            }
        }
        DataSet dt1 = new DataSet();
        dt1 = la.UsedFonts(id.ToString(), TFont);
        if (dt1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Tables[0].Rows.Count; i++)
            {
                string f = dt1.Tables[0].Rows[i]["Fonts"].ToString();
                la.deletefonts(id.ToString(), f);
            }
        }
        lboxTFonts.Items.Clear();
        DataSet tf1 = new DataSet();
        tf1 = la.GetFonts(id);
        lboxTFonts.DataTextField = "Fonts";
        lboxTFonts.DataValueField = "Fonts";
        lboxTFonts.DataSource = tf1;
        lboxTFonts.DataBind();
    }
    protected void btnmissfontsadd_Click(object sender, EventArgs e)
    {
        string id;
        if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else if (hfP_ID.Value != "")
            id = hfP_Name.Value.Trim();
        else
            id = "";
        string fontitem11 = "";
        for (int j = 0; j < lboxMissFonts.Items.Count; j++)
        {
            if (lboxMissFonts.Items[j].Selected == true)
            {
                if (fontitem11 == "")
                    fontitem11 = lboxMissFonts.Items[j].Value;
                else
                    fontitem11 = fontitem11 + ',' + lboxMissFonts.Items[j].Value;
            }
        }
        DataSet am1 = new DataSet();
        DataSet am2 = new DataSet();
        am1 = la.UsedFonts(id.ToString(), fontitem11);
        if (am1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < am1.Tables[0].Rows.Count; i++)
            {
                string Mfonts = am1.Tables[0].Rows[i]["Fonts"].ToString();

                if (txtMissFonts.Text != "" )
                {
                    DataSet mft = new DataSet();
                    DataSet dsm = new DataSet();
                    mft = la.getFonts(txtMissFonts.Text);
                    if (mft.Tables[0].Rows.Count > 0)
                    {
                        for (int x = 0; x < mft.Tables[0].Rows.Count; x++)
                        {
                            dsm = la.Chkmissfonts(mft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (dsm == null)
                            {
                                la.insertmissfonts(mft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                            am2 = la.Chkmfonts(id.ToString(), mft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (am2 == null)
                            {
                                la.insertmfonts(id.ToString(), mft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                        }
                    }
                }
                else
                {
                    am2 = la.Chkmfonts(id.ToString(), Mfonts);
                    if (am2 == null)
                    {
                        la.insertmfonts(id.ToString(), Mfonts);
                    }
                }
            }
        }
        if (txtMissFonts.Text != "" && fontitem11.ToString()=="")
        {
            DataSet mft = new DataSet();
            DataSet dsm = new DataSet();
            mft = la.getFonts(txtMissFonts.Text);
            if (mft.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < mft.Tables[0].Rows.Count; x++)
                {
                    dsm = la.Chkmissfonts(mft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (dsm == null)
                    {
                        la.insertmissfonts(mft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                    am2 = la.Chkmfonts(id.ToString(), mft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (am2 == null)
                    {
                        la.insertmfonts(id.ToString(), mft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                }
            }
        }
        DataSet mf = new DataSet();
        mf = la.GetMFonts(id);
        lboxMFonts.DataTextField = "Fonts";
        lboxMFonts.DataValueField = "Fonts";
        lboxMFonts.DataSource = mf;
        lboxMFonts.DataBind();
        DataSet Dst13 = new DataSet();
        Dst13 = la.GetMissFonts();
        lboxMissFonts.DataTextField = "Fonts";
        lboxMissFonts.DataValueField = "Fonts";
        lboxMissFonts.DataSource = Dst13;
        lboxMissFonts.DataBind();
    }
    protected void btnmissfontsdel_Click(object sender, EventArgs e)
    {
        string id;
        if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else if (hfP_ID.Value != "")
            id = hfP_Name.Value.Trim();
        else
            id = "";
        string MFont = "";
        for (int j = 0; j < lboxMFonts.Items.Count; j++)
        {
            if (lboxMFonts.Items[j].Selected == true)
            {
                if (MFont == "")
                    MFont = lboxMFonts.Items[j].Value;
                else
                    MFont = MFont + ',' + lboxMFonts.Items[j].Value;
            }
        }
        DataSet dm1 = new DataSet();
        dm1 = la.UsedFonts(id.ToString(), MFont);
        if (dm1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dm1.Tables[0].Rows.Count; i++)
            {
                la.deletemfonts(id.ToString(), dm1.Tables[0].Rows[i]["Fonts"].ToString());
            }
        }
        DataSet Dsm = new DataSet();
        Dsm = la.GetMissFonts();
        lboxMissFonts.DataTextField = "Fonts";
        lboxMissFonts.DataValueField = "Fonts";
        lboxMissFonts.DataSource = Dsm;
        lboxMissFonts.DataBind();
        lboxMFonts.Items.Clear();
        DataSet mf1 = new DataSet();
        mf1 = la.GetMFonts(id.ToString());
        lboxMFonts.DataTextField = "Fonts";
        lboxMFonts.DataValueField = "Fonts";
        lboxMFonts.DataSource = mf1;
        lboxMFonts.DataBind();
    }
    protected void gv_Soft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id;
        if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else if (hfP_ID.Value != "")
            id = hfP_Name.Value.Trim();
        else
            id = "";
        string soft = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet dscust1 = la.getAllSoftware();
            ListBox lboxSoft = (ListBox)e.Row.FindControl("lboxSoft");
            TextBox task = (TextBox)e.Row.FindControl("txt_task");
            lboxSoft.DataSource = dscust1;
            lboxSoft.DataTextField = dscust1.Tables[0].Columns[2].ToString();
            lboxSoft.DataValueField = dscust1.Tables[0].Columns[1].ToString();
            lboxSoft.DataBind();
            //lboxSoft.SelectedIndex = e.Row.RowIndex;
            DataSet sv = new DataSet();
            sv = la.GetSoftname(id.ToString());
            if (sv != null)
            {
                DataSet empd = new DataSet();
                empd = la.Getsofttask(id.ToString(), task.Text);
                if (empd != null)
                {
                    if (empd.Tables[0].Rows.Count > 0 || empd != null)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxSoft.Items[lboxSoft.Items.IndexOf(lboxSoft.Items.FindByValue(empd.Tables[0].Rows[i]["Software_id"].ToString()))].Selected = true;

                            if (soft == "" || soft == null)
                                soft = empd.Tables[0].Rows[i]["Software_id"].ToString();
                            else
                                soft = soft + ',' + empd.Tables[0].Rows[i]["Software_id"].ToString();
                        }

                    }
                    ListBox lboxVer = (ListBox)e.Row.FindControl("lboxVer");
                    if (soft.ToString() != "")
                    {
                        DataSet dsSoft = la.GetSoftVers(soft.ToString());
                        lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                        lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                        lboxVer.DataSource = dsSoft;
                        lboxVer.DataBind();
                    }
                    if (empd.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxVer.Items[lboxVer.Items.IndexOf(lboxVer.Items.FindByValue(empd.Tables[0].Rows[i]["Version_id"].ToString()))].Selected = true;

                        }
                    }
                }
            }
        }
    }
    protected void lboxSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow grs in gv_Soft.Rows)
        {
            DataSet dscust1 = la.getAllSoftware();
            ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
            TextBox task = (TextBox)grs.FindControl("txt_task");
            ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
            string sv = "";
            for (int j = 0; j < lboxSoft.Items.Count; j++)
            {
                if (lboxSoft.Items[j].Selected == true)
                {
                    if (sv == "")
                        sv = lboxSoft.Items[j].Value;
                    else
                        sv = sv + ',' + lboxSoft.Items[j].Value;
                }
            }
            if (sv.ToString() != "")
            {
                DataSet dsSoft = la.GetSoftVers(sv.ToString());
                lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                lboxVer.DataSource = dsSoft;
                lboxVer.DataBind();
            }
        }
    }
    protected void chkDueDate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDueDate.Checked)
        {
            lblDueFrom.Visible = true;
            txtdueFromdate.Visible = true;
            lblDueTo.Visible = true;
            imgBD_dueFromdate.Visible = true;
            txtdueTodate.Visible = true;
            imgBD_dueTodate.Visible = true;
        }
        else
        {
            lblDueFrom.Visible = false;
            txtdueFromdate.Visible = true;
            lblDueTo.Visible = false;
            imgBD_dueFromdate.Visible = true;
            txtdueTodate.Visible = false;
            imgBD_dueTodate.Visible = false;
        }
    }
   
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            if (Convert.ToInt32(hfP_ID.Value) < 381)
                getComplexReasonold(Dropcomplex.SelectedValue);
            else
                getComplexReason(Convert.ToInt32(hfP_ID.Value), DropNewcomplex.SelectedValue);
        }
        else
        {
            if (txtProjectTitle.Text != "")
            {
                DataSet dp = new DataSet();
                dp = la.GetProid(txtProjectTitle.Text);
                if (Convert.ToInt16(dp.Tables[0].Rows[0]["Pro_id"].ToString()) < 381)
                    getComplexReasonold(Dropcomplex.SelectedValue);
                else
                    getComplexReason(Convert.ToInt16(dp.Tables[0].Rows[0]["Pro_id"].ToString()), DropNewcomplex.SelectedValue);
            }
        }
    }

    
    protected void btnlangadd_Click(object sender, EventArgs e)
    {
        Launch inobj = new Launch();
        string groupitem = "", msg = ""; ;
        try
        {
            string id;
            if (txtProjectTitle.Text != "")
                id = txtProjectTitle.Text;
            else if (hfP_ID.Value != "")
                id = hfP_Name.Value.Trim();
            else
                id = "";
            if (id != "")
            {
                for (int j = 0; j < lboxlang.Items.Count; j++)
                {
                    if (lboxlang.Items[j].Selected == true)
                    {
                        if (groupitem == "")
                            groupitem = lboxlang.Items[j].Value;
                        else
                            groupitem = groupitem + '-' + lboxlang.Items[j].Value;
                    }
                }
                
                DataSet du1 = new DataSet();
                //du1 = la.Getusedlangname(id.ToString(), groupitem);
                
                DataSet dss = new DataSet();
                dss = la.GetSoftname(id.ToString());
                for (int ii = 0; ii < dss.Tables[0].Rows.Count; ii++)
                {
                    for (int kk = 0; kk < CheckBoxTask.Items.Count; kk++)
                    {
                        if (CheckBoxTask.Items[kk].Value == dss.Tables[0].Rows[ii]["taskname"].ToString())
                        {
                            if (CheckBoxTask.Items[kk].Selected == true)
                            {
                                du1 = la.GetusedlangTaskWise(id.ToString(), groupitem, CheckBoxTask.Items[kk].Value, dss.Tables[0].Rows[ii]["software_id"].ToString());
                                if (du1 != null)
                                {
                                }
                                else
                                {
                                    //DataSet ss = new DataSet();
                                    //ss = la.GetSoftname(id.ToString());
                                    //if (ss != null && ss.Tables[0].Rows.Count > 0)
                                    //{
                                    //    for (int i = 0; i < ss.Tables[0].Rows.Count; i++)
                                    //    {
                                    //        for (int k = 0; k < CheckBoxTask.Items.Count; k++)
                                    //        {
                                    //            if (CheckBoxTask.Items[k].Value == ss.Tables[0].Rows[i]["taskname"].ToString())
                                    //            {
                                    //                if (CheckBoxTask.Items[k].Selected == true)
                                    //                {
                                                        string langproc = "spAdd_JobLang_group";
                                                        string[,] langname ={ { "@PROJECTNAME", id.ToString() }, { "@lang_name", groupitem },
                                                                             { "@taskname", dss.Tables[0].Rows[ii]["taskname"].ToString() }, { "@software_id", dss.Tables[0].Rows[ii]["software_id"].ToString() }};
                                                        int format12 = this.oLaunch.ExcSP(langproc, langname, CommandType.StoredProcedure);
                                        //            }
                                        //        }
                                        //    }
                                        //}
                                    //}
                                }
                            }
                        }
                    }
                }
                
                DataSet sds1 = new DataSet();
                sds1 = la.getLangGroup(id.ToString());
                //sds1 = oLaunch.ExcuteSelectProcedure("spGet_LangGroups", id, "@proname", "varChar", "input", CommandType.StoredProcedure);
                if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
                {
                    gv_lang.DataSource = sds1.Tables[0];
                    gv_lang.DataBind();
                    gv_images.DataSource = sds1.Tables[0];
                    gv_images.DataBind();
                }
                DataSet ul = new DataSet();
                ul = la.GetusedLang(id.ToString());
                lboxlangused.DataTextField = "lang_name";
                lboxlangused.DataValueField = "lang_name";
                lboxlangused.DataSource = ul;
                lboxlangused.DataBind();
                int x = 0;
                for (int j = 0; j < lboxlangused.Items.Count; j++)
                {
                        x = x + 1;
                }
                txtlangcount.Text = x.ToString();
            }
            else
            {
                msg = "Select Project Details!..";
                ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
        }

    }
    protected void btnlangdel_Click(object sender, EventArgs e)
    {
        string id;
        if (txtProjectTitle.Text != "")
            id = txtProjectTitle.Text;
        else if (hfP_ID.Value != "")
            id = hfP_Name.Value.Trim();
        else
            id = "";
        string langname = "";
        for (int j = 0; j < lboxlangused.Items.Count; j++)
        {
            if (lboxlangused.Items[j].Selected == true)
            {
                if (langname == "")
                    langname = lboxlangused.Items[j].Value;
                else
                    langname = langname + ',' + lboxlangused.Items[j].Value;
            }
        }
        DataSet du1 = new DataSet();
        du1 = la.Getusedlangname(id.ToString(), langname);
        if (du1.Tables[0].Rows.Count > 0 || du1 != null)
        {
            for (int i = 0; i < du1.Tables[0].Rows.Count; i++)
            {
                string lang = du1.Tables[0].Rows[i]["lang_name"].ToString();
                la.deleteusagelang(id.ToString(), lang);
                lboxlangused.ClearSelection();
            }
        }
        
        DataSet uf1 = new DataSet();
        uf1 = la.GetusedLang(id.ToString());
        lboxlangused.DataTextField = "lang_name";
        lboxlangused.DataValueField = "lang_name";
        lboxlangused.DataSource = uf1;
        lboxlangused.DataBind();
        int x = 0;
        for (int j = 0; j < lboxlangused.Items.Count; j++)
        {
            x = x + 1;
        }
        txtlangcount.Text = x.ToString();
        DataSet sds1 = new DataSet();
        sds1 = la.GetLangFile(id.ToString());
        //if (sds1 != null || sds1.Tables[0].Rows.Count > 0)
        //{
            gv_lang.DataSource = sds1;
            gv_lang.DataBind();
            gv_images.DataSource = sds1;
            gv_images.DataBind();
        //}
    }
    protected void ListComplexReason_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in ListComplexReason.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxusagefonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxusagefonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxFonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxFonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxMissFonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxMissFonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxUFonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxUFonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxTFonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxTFonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxMFonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxMFonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxlang_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxlang.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxlangused_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxlangused.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }

    //New Launch 04/24/2015

    private void popScreenNew()
    {
        DataSet Ds = new DataSet();
        Ds = nonLa.GetTask();
        lboxNewtask.DataTextField = Ds.Tables[0].Columns[1].ToString();
        lboxNewtask.DataValueField = Ds.Tables[0].Columns[0].ToString();
        lboxNewtask.DataSource = Ds;
        lboxNewtask.DataBind();

        //Ds = nonLa.getAllLocation();
        //DropLocation.DataSource = Ds;
        //DropLocation.DataTextField = Ds.Tables[0].Columns[1].ToString();
        //DropLocation.DataValueField = Ds.Tables[0].Columns[0].ToString();
        //DropLocation.DataBind();
        DropNewLocation.Items.Insert(0, new ListItem("-- select --", "0"));

        Ds = nonLa.getAllCustomers();
        drpNewProjectcustomer.DataSource = Ds;
        drpNewProjectcustomer.DataTextField = Ds.Tables[0].Columns[1].ToString();
        drpNewProjectcustomer.DataValueField = Ds.Tables[0].Columns[0].ToString();
        drpNewProjectcustomer.DataBind();
        drpNewProjectcustomer.Items.Insert(0, new ListItem("-- select --", "0"));

        drpCustomerSearch.DataSource = Ds;
        drpCustomerSearch.DataTextField = Ds.Tables[0].Columns[1].ToString();
        drpCustomerSearch.DataValueField = Ds.Tables[0].Columns[0].ToString();
        drpCustomerSearch.DataBind();
        drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));

        Ds = nonLa.GetLanguage();
        lboxNewlang.DataTextField = Ds.Tables[0].Columns[1].ToString();
        lboxNewlang.DataValueField = Ds.Tables[0].Columns[0].ToString();
        lboxNewlang.DataSource = Ds;
        lboxNewlang.DataBind();

        Ds = nonLa.GetLaunchInitial();
        if (Ds != null)
        {
        dropProjectName.DataTextField = Ds.Tables[0].Columns[1].ToString();
        dropProjectName.DataValueField = Ds.Tables[0].Columns[0].ToString();
        dropProjectName.DataSource = Ds;
        dropProjectName.DataBind();
        dropProjectName.Items.Insert(0, new ListItem("-- select --", "0"));
        }
        else
        {
            dropProjectName.Items.Insert(0, new ListItem("-- select --", "0"));
        }

        DataSet Dst11 = new DataSet();
        Dst11 = nonLa.GetMissFonts();
        lboxNewMissFonts.DataTextField = "Fonts";
        lboxNewMissFonts.DataValueField = "Fonts";
        lboxNewMissFonts.DataSource = Dst11;
        lboxNewMissFonts.DataBind();
        lboxNewFonts.DataTextField = "Fonts";
        lboxNewFonts.DataValueField = "Fonts";
        lboxNewFonts.DataSource = Dst11;
        lboxNewFonts.DataBind();
        lboxNewusagefonts.DataTextField = "Fonts";
        lboxNewusagefonts.DataValueField = "Fonts";
        lboxNewusagefonts.DataSource = Dst11;
        lboxNewusagefonts.DataBind();

        DropNewcomplex.Items.Clear();
        DropNewcomplex.Items.Add(new ListItem("Simple", "S"));
        DropNewcomplex.Items.Add(new ListItem("Medium", "M"));
        DropNewcomplex.Items.Add(new ListItem("Complex", "C"));
    }

    protected void gv_SoftNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet dscust1 = nonLa.getAllSoftware();
            ListBox lboxSoft = (ListBox)e.Row.FindControl("lboxSoft");
            TextBox txtTargetDate = (TextBox)e.Row.FindControl("txtTargetDate");
            HiddenField task = (HiddenField)e.Row.FindControl("hf_taskID");
            HiddenField lang = (HiddenField)e.Row.FindControl("hf_LangID");
            lboxSoft.DataSource = dscust1;
            lboxSoft.DataTextField = dscust1.Tables[0].Columns[1].ToString();
            lboxSoft.DataValueField = dscust1.Tables[0].Columns[0].ToString();
            lboxSoft.DataBind();
            string soft = "";
            DataSet sv = new DataSet();
            sv = nonLa.SoftSelectedLP(hfP_ID.Value);
            if (sv != null)
            {
                DataSet empd = new DataSet();
                empd = nonLa.GetSelectedSoftLP(hfP_ID.Value, task.Value, lang.Value);
                if (empd != null)
                {
                    if (empd.Tables[0].Rows.Count > 0 || empd != null)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxSoft.Items[lboxSoft.Items.IndexOf(lboxSoft.Items.FindByValue(empd.Tables[0].Rows[i]["Soft_id"].ToString()))].Selected = true;

                            if (soft == "" || soft == null)
                                soft = empd.Tables[0].Rows[i]["Soft_id"].ToString();
                            else
                                soft = soft + ',' + empd.Tables[0].Rows[i]["Soft_id"].ToString();
                        }

                    }
                    ListBox lboxVer = (ListBox)e.Row.FindControl("lboxVer");
                    if (soft.ToString() != "")
                    {
                        DataSet dsSoft = nonLa.GetSoftVers(soft.ToString());
                        lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                        lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                        lboxVer.DataSource = dsSoft;
                        lboxVer.DataBind();
                    }
                    if (empd.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxVer.Items[lboxVer.Items.IndexOf(lboxVer.Items.FindByValue(empd.Tables[0].Rows[i]["Version_id"].ToString()))].Selected = true;

                        }
                    }
                    txtTargetDate.Text = empd.Tables[0].Rows[0]["TARGET_DATE"].ToString();
                }
            }
        }
    }
    protected void lboxNewSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow grs in gv_SoftNew.Rows)
        {
            DataSet dscust1 = nonLa.getAllSoftware();
            ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
            Label task = (Label)grs.FindControl("txt_task");
            ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
            string sv = "";
            for (int j = 0; j < lboxSoft.Items.Count; j++)
            {
                if (lboxSoft.Items[j].Selected == true)
                {
                    if (sv == "")
                        sv = lboxSoft.Items[j].Value;
                    else
                        sv = sv + ',' + lboxSoft.Items[j].Value;
                }
            }
            if (sv.ToString() != "")
            {
                DataSet dsSoft = nonLa.GetSoftVers(sv.ToString());
                lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                lboxVer.DataSource = dsSoft;
                lboxVer.DataBind();
            }
        }
    }
    protected void btnNewJobInfo_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
        {
            if (validateScreenLP())
            {
                int ytc;
                if (chkNewYTC.Checked)
                    ytc = 1;
                else
                    ytc = 0;
                int duetime;
                if (chkNewDueTime.Checked)
                    duetime = 1;
                else
                    duetime = 0;
                int dueDate;
                if (chkNewDueDate.Checked)
                    dueDate = 1;
                else
                    dueDate = 0;
                string ddate = "";
                if (txtNewdueFromdate.Text != "")
                    ddate = DateTime.Parse(txtNewdueFromdate.Text.Trim()).ToString("MM/dd/yyyy");
                else
                    ddate = "";
                string dTodate = "";
                if (txtNewdueTodate.Text != "")
                    dTodate = DateTime.Parse(txtNewdueTodate.Text.Trim()).ToString("MM/dd/yyyy");
                else
                    dTodate = "";
                string recDate = "";
                if (txtNewRecDate.Text != "")
                    recDate = DateTime.Parse(txtNewRecDate.Text.Trim()).ToString("MM/dd/yyyy");
                else
                    recDate = "";
                if (btnNewJobInfo.Text == "Save")
                {
                    if(lblInitalID.Text!="")
                    {
                    string inproc = "spInsertLP";
                    string[,] pname ={
                                        {"@PROJECTNAME",txtNewProjectTitle.Text },
                                        {"@CUST_ID",drpNewProjectcustomer.SelectedValue},{"@Location",DropNewLocation.SelectedValue},
                                        {"@PLATFORM",dropNewSwPlat.SelectedValue},{"@YTCYN",ytc.ToString()},
                                        {"@DUE_DATEFROM",ddate},{"@DUE_DATETo",dTodate},
                                        {"@Due_Timefrom",DropNewDueTimeFrom.SelectedValue},{"@Due_MINFROM",DropNewDueMinFrom.SelectedValue},
                                        {"@TIME_ZoneFROM",DropNewDueTimeZoneFrom.SelectedValue},{"@PROJECTEDITOR",txtNewProjectEditor.Text},
                                        {"@Due_TimeTO",DropNewDueTimeTo.SelectedValue},{"@Due_MINTO",DropNewDueMinTo.SelectedValue},
                                        {"@TIME_ZoneTO",DropNewDueTimeZoneTo.SelectedValue},{"@CREATED_BY",Session["employeeid"].ToString()},
                                        {"@DUETIMEFROM_IST",txtNewIndFrom.Text},{"@DUETIMETO_IST",txtNewIndTo.Text},
                                        {"@SourceType",DropNewSource.SelectedValue},{"@DUETIMEYN",duetime.ToString()},
                                        {"@DUEDateYN",dueDate.ToString()},{"@IsExist","OUTPUT"},
                                        {"@File",""},{"@RecDate",recDate.ToString()},{"@LI_ID",lblInitalID.Text},
                                        {"@FILE_PATH",txtNewFileLocation.Text},{"@NOOF_PACKAGE",dropNewNoofFolder.SelectedValue},
                                        {"@PACK_SIZE",txtNewPackageSize.Text},{"@PACK_SIZE_BYTES",dropNewPackageSize.SelectedValue},
                                        {"@FTPYN",dropNewDownPackage.SelectedValue},{"@jobno",txtWOnumber.Text},{"@TATsDay", dropTATsDays.SelectedValue},{"@TATsHrs",dropTATsHrs.SelectedValue}
                                     };
                    int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
                    

                    if (val == 0)
                        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Inserted Failed!.');</script>");
                    else
                    {
                        btnNewJobInfo.Text = "Update";

                        DataSet ds = new DataSet();
                        ds = nonLa.GetLP_Launch("'" + txtNewProjectTitle.Text + "'");
                        string pn = ds.Tables[0].Rows[0]["LP_ID"].ToString();

                        string taskitem = "";
                        for (int j = 0; j < lboxNewtask.Items.Count; j++)
                        {
                            if (lboxNewtask.Items[j].Selected == true)
                            {
                                if (taskitem == "")
                                    taskitem = lboxNewtask.Items[j].Value;
                                else
                                    taskitem = taskitem + '-' + lboxNewtask.Items[j].Value;
                            }
                        }
                        string taskproc = "spInsert_LP_Task";
                        string[,] tname = { { "@LP_ID", pn }, { "@Task_ID", taskitem } };
                        int task12 = this.oNewLa.ExcSP(taskproc, tname, CommandType.StoredProcedure);

                        string formatitem = "";
                        for (int j = 0; j < lboxNewformat.Items.Count; j++)
                        {
                            if (lboxNewformat.Items[j].Selected == true)
                            {
                                if (formatitem == "")
                                    formatitem = lboxNewformat.Items[j].Value;
                                else
                                    formatitem = formatitem + '-' + lboxNewformat.Items[j].Value;
                            }
                        }
                        string formatproc = "spInsert_LP_Format";
                        string[,] forname = { { "@LP_ID", pn }, { "@format_id", formatitem } };

                        int format12 = this.oNewLa.ExcSP(formatproc, forname, CommandType.StoredProcedure);
                        string inputitem = "";
                        for (int j = 0; j < lboxNewInputFile.Items.Count; j++)
                        {
                            if (lboxNewInputFile.Items[j].Selected == true)
                            {
                                if (inputitem == "")
                                    inputitem = lboxNewInputFile.Items[j].Value;
                                else
                                    inputitem = inputitem + '-' + lboxNewInputFile.Items[j].Value;
                            }
                        }
                        string inputproc = "spInsert_LP_Input";
                        string[,] inname = { { "@LP_ID", pn }, { "@inputid", inputitem } };
                        int input12 = this.oNewLa.ExcSP(inputproc, inname, CommandType.StoredProcedure);

                        nonLa.UpdateUsedtLangLP(pn, tempID.Text);
                        tempID.Text = "";
                        hfP_ID.Value = pn;
                        hfP_Name.Value = txtNewProjectTitle.Text;
                        //ArrayList a = new ArrayList();
                        //Hashtable s = new Hashtable();
                        //string ver1 = "";
                        //foreach (GridViewRow grs in gv_SoftNew.Rows)
                        //{
                        //    ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
                        //    ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
                        //    HiddenField task = (HiddenField)grs.FindControl("hf_taskID");
                        //    HiddenField Lang = (HiddenField)grs.FindControl("hf_LangID");
                        //    TextBox TarDate = (TextBox)grs.FindControl("txtTargetDate");
                        //    if (recDate != "")
                        //    {
                        //        TarDate.Text = recDate;
                        //    }
                        //    else
                        //    {
                        //        TarDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        //    }
                        //    string soft;
                        //    int r = 0;
                        //    for (int j = 0; j < lboxSoft.Items.Count; j++)
                        //    {
                        //        if (lboxSoft.Items[j].Selected == true)
                        //        {
                        //            soft = lboxSoft.Items[j].Value;
                        //            r = r + 1;
                        //            if (r == 1)
                        //            {
                        //                for (int d = 0; d < lboxVer.Items.Count; d++)
                        //                {
                        //                    if (lboxVer.Items[d].Selected == true)
                        //                    {
                        //                        ver1 = lboxVer.Items[d].Value;
                        //                        break;
                        //                    }
                        //                    else if (lboxVer.Items[d].Selected == true)
                        //                    {
                        //                        ver1 = lboxVer.Items[d].Value;
                        //                    }

                        //                }
                        //            }
                        //            else
                        //            {
                        //                for (int d = 0; d < lboxVer.Items.Count; d++)
                        //                {
                        //                    if (lboxVer.Items[d].Selected == true)
                        //                    {
                        //                        ver1 = lboxVer.Items[d].Value;
                        //                    }
                        //                }
                        //            }
                        //            if (TarDate.Text == "")
                        //                TarDate.Text = null;
                        //            s = new Hashtable();
                        //            s.Add("LP_ID", pn);
                        //            s.Add("Task_ID", task.Value);
                        //            s.Add("Lang_ID", Lang.Value);
                        //            s.Add("TarDate", TarDate.Text);
                        //            s.Add("Software_id", soft);
                        //            s.Add("Version_id", ver1);
                        //            s.Add("Status", "1");
                        //            a.Add(s);
                        //        }
                        //    }
                        //}
                        //nonLa.Insert_SoftwareLP(a);
                        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Inserted Successfully!.');</script>");
                    }


                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Select ProjectName in the List!.');</script>");
                    }
                        
                }
                else
                {
                    string inproc = "spUpdateLP";
                    string[,] pname ={
                                        {"@PROJECTNAME",txtNewProjectTitle.Text },{"@LP_ID",hfP_ID.Value},
                                        {"@CUST_ID",drpNewProjectcustomer.SelectedValue},{"@Location",DropNewLocation.SelectedValue},
                                        {"@PLATFORM",dropNewSwPlat.SelectedValue},{"@YTCYN",ytc.ToString()},
                                        {"@DUE_DATEFROM",ddate},{"@DUE_DATETo",dTodate},
                                        {"@Due_Timefrom",DropNewDueTimeFrom.SelectedValue},{"@Due_MINFROM",DropNewDueMinFrom.SelectedValue},
                                        {"@TIME_ZoneFROM",DropNewDueTimeZoneFrom.SelectedValue},{"@PROJECTEDITOR",txtNewProjectEditor.Text},
                                        {"@Due_TimeTO",DropNewDueTimeTo.SelectedValue},{"@Due_MINTO",DropNewDueMinTo.SelectedValue},
                                        {"@TIME_ZoneTO",DropDueTimeZoneTo.SelectedValue},{"@MODIFIED_BY",Session["employeeid"].ToString()},
                                        {"@DUETIMEFROM_IST",txtNewIndFrom.Text},{"@DUETIMETO_IST",txtNewIndTo.Text},
                                        {"@SourceType",DropNewSource.SelectedValue},{"@DUETIMEYN",duetime.ToString()},
                                        {"@DUEDateYN",dueDate.ToString()},{"@IsExist","OUTPUT"},
                                        {"@File",""},{"@RecDate",recDate.ToString()},
                                        {"@FILE_PATH",txtNewFileLocation.Text},{"@NOOF_PACKAGE",dropNewNoofFolder.SelectedValue},
                                        {"@PACK_SIZE",txtNewPackageSize.Text},{"@PACK_SIZE_BYTES",dropNewPackageSize.SelectedValue},
                                        {"@FTPYN",dropNewDownPackage.SelectedValue},{"@TATsDay", dropTATsDays.SelectedValue},{"@TATsHrs",dropTATsHrs.SelectedValue}
                                     };
                    int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);

                    string taskitem = "";
                    for (int j = 0; j < lboxNewtask.Items.Count; j++)
                    {
                        if (lboxNewtask.Items[j].Selected == true)
                        {
                            if (taskitem == "")
                                taskitem = lboxNewtask.Items[j].Value;
                            else
                                taskitem = taskitem + '-' + lboxNewtask.Items[j].Value;
                        }
                    }
                    string taskproc = "spInsert_LP_Task";
                    string[,] tname = { { "@LP_ID", hfP_ID.Value }, { "@Task_ID", taskitem } };
                    int task12 = this.oNewLa.ExcSP(taskproc, tname, CommandType.StoredProcedure);

                    string formatitem = "";
                    for (int j = 0; j < lboxNewformat.Items.Count; j++)
                    {
                        if (lboxNewformat.Items[j].Selected == true)
                        {
                            if (formatitem == "")
                                formatitem = lboxNewformat.Items[j].Value;
                            else
                                formatitem = formatitem + '-' + lboxNewformat.Items[j].Value;
                        }
                    }
                    string formatproc = "spInsert_LP_Format";
                    string[,] forname = { { "@LP_ID", hfP_ID.Value }, { "@format_id", formatitem } };

                    int format12 = this.oNewLa.ExcSP(formatproc, forname, CommandType.StoredProcedure);
                    string inputitem = "";
                    for (int j = 0; j < lboxNewInputFile.Items.Count; j++)
                    {
                        if (lboxNewInputFile.Items[j].Selected == true)
                        {
                            if (inputitem == "")
                                inputitem = lboxNewInputFile.Items[j].Value;
                            else
                                inputitem = inputitem + '-' + lboxNewInputFile.Items[j].Value;
                        }
                    }
                    string inputproc = "spInsert_LP_Input";
                    string[,] inname = { { "@LP_ID", hfP_ID.Value }, { "@inputid", inputitem } };
                    int input12 = this.oNewLa.ExcSP(inputproc, inname, CommandType.StoredProcedure);

                    //nonLa.UpdateSoftStatusLP(hfP_ID.Value);
                    //ArrayList a = new ArrayList();
                    //Hashtable s = new Hashtable();
                    //string ver1 = "";
                    //foreach (GridViewRow grs in gv_SoftNew.Rows)
                    //{
                    //    ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
                    //    ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
                    //    HiddenField task = (HiddenField)grs.FindControl("hf_taskID");
                    //    HiddenField Lang = (HiddenField)grs.FindControl("hf_LangID");
                    //    TextBox TarDate = (TextBox)grs.FindControl("txtTargetDate");
                    //    if (recDate != "")
                    //    {
                    //        TarDate.Text = recDate;
                    //    }
                    //    else
                    //    {
                    //        TarDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    //    }
                    //    string soft;
                    //    int r = 0;
                    //    for (int j = 0; j < lboxSoft.Items.Count; j++)
                    //    {
                    //        if (lboxSoft.Items[j].Selected == true)
                    //        {
                    //            soft = lboxSoft.Items[j].Value;
                    //            r = r + 1;
                    //            if (r == 1)
                    //            {
                    //                for (int d = 0; d < lboxVer.Items.Count; d++)
                    //                {
                    //                    if (lboxVer.Items[d].Selected == true)
                    //                    {
                    //                        ver1 = lboxVer.Items[d].Value;
                    //                        break;
                    //                    }
                    //                    else if (lboxVer.Items[d].Selected == true)
                    //                    {
                    //                        ver1 = lboxVer.Items[d].Value;
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                for (int d = 0; d < lboxVer.Items.Count; d++)
                    //                {
                    //                    if (lboxVer.Items[d].Selected == true)
                    //                    {
                    //                        ver1 = lboxVer.Items[d].Value;
                    //                    }
                    //                }
                    //            }
                    //            if (TarDate.Text == "")
                    //                TarDate.Text = null;
                    //            s = new Hashtable();
                    //            s.Add("LP_ID", hfP_ID.Value);
                    //            s.Add("Task_ID", task.Value);
                    //            s.Add("Lang_ID", Lang.Value);
                    //            s.Add("TarDate", TarDate.Text);
                    //            s.Add("Software_id", soft);
                    //            s.Add("Version_id", ver1);
                    //            s.Add("Status", "1");
                    //            a.Add(s);
                    //        }
                    //    }
                    //}
                    //nonLa.Insert_SoftwareLP(a);
                    //nonLa.DeleteSoftLP(hfP_ID.Value);
                    if (val == 1)
                        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Updated Successfully!.');</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Updation Failed!.');</script>");
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void drpNewProjectcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = nonLa.GetLocationCust(drpNewProjectcustomer.SelectedValue);
        if (ds != null)
        {
            DropNewLocation.Enabled = true;
            DropNewLocation.DataSource = ds;
            DropNewLocation.DataValueField = ds.Tables[0].Columns[3].ToString();
            DropNewLocation.DataTextField = ds.Tables[0].Columns[4].ToString();
            DropNewLocation.DataBind();
        }
        else
        {
            DropNewLocation.Items.Insert(0, new ListItem("-- select --", "0"));
            DropNewLocation.SelectedValue = "0";
            DropNewLocation.Enabled = false;
        }
        getloc_timezoneNew(Convert.ToInt16(DropNewLocation.SelectedValue));
    }
    public void getloc_timezoneNew(int locid)
    {
        DataSet ds = nonLa.GetTimeZone(locid);
        if (ds != null)
        {
            DropNewDueTimeZoneFrom.DataSource = ds;
            DropNewDueTimeZoneFrom.DataValueField = ds.Tables[0].Columns[2].ToString();
            DropNewDueTimeZoneFrom.DataTextField = ds.Tables[0].Columns[2].ToString();
            DropNewDueTimeZoneFrom.DataBind();
            DropNewDueTimeZoneTo.DataSource = ds;
            DropNewDueTimeZoneTo.DataValueField = ds.Tables[0].Columns[2].ToString();
            DropNewDueTimeZoneTo.DataTextField = ds.Tables[0].Columns[2].ToString();
            DropNewDueTimeZoneTo.DataBind();

            DropDueTimeZoneFrom1.DataSource = ds;
            DropDueTimeZoneFrom1.DataValueField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneFrom1.DataTextField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneFrom1.DataBind();
            DropDelTimeZone.DataSource = ds;
            DropDelTimeZone.DataValueField = ds.Tables[0].Columns[2].ToString();
            DropDelTimeZone.DataTextField = ds.Tables[0].Columns[2].ToString();
            DropDelTimeZone.DataBind();
            DropDueTimeZoneTo1.DataSource = ds;
            DropDueTimeZoneTo1.DataValueField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneTo1.DataTextField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneTo1.DataBind();


            TimeFormatNew(locid.ToString(), DropNewDueTimeFrom.SelectedValue, DropNewDueMinFrom.SelectedValue, DropNewDueTimeZoneFrom.SelectedValue);
            TimeFormatNew(locid.ToString(), DropNewDueTimeTo.SelectedValue, DropNewDueMinTo.SelectedValue, DropNewDueTimeZoneTo.SelectedValue);
        }
        else
        {
            DropNewDueTimeZoneFrom.Items.Insert(0, new ListItem(" ", "0"));
            DropNewDueTimeZoneTo.Items.Insert(0, new ListItem(" ", "0"));
            DropNewDueTimeZoneFrom.SelectedValue = "0";
            DropNewDueTimeZoneTo.SelectedValue = "0";
            //DropNewDueTimeZoneFrom.Enabled = false;
            //DropNewDueTimeZoneTo.Enabled = false;
        }
    }

    protected void DropNewLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        getloc_timezoneNew(Convert.ToInt16(DropNewLocation.SelectedValue));
    }
    public void TimeFormatNew(string LocID, string Hrs, string Min, string Zone)
    {
        string hrs = Hrs;
        string min = Min;
        DataSet time = nonLa.getTimeDetails(hrs, min, LocID.ToString(), Zone);
        dtable = time.Tables[0].Copy();
    }
    protected void DropNewDueTimeFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(DropNewLocation.SelectedValue, DropNewDueTimeFrom.SelectedValue, DropNewDueMinFrom.SelectedValue, DropNewDueTimeZoneFrom.SelectedValue);
        txtNewIndFrom.Text = dtable.Rows[0]["Mins"].ToString();
    }
    protected void DropNewDueMinFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(DropNewLocation.SelectedValue, DropNewDueTimeFrom.SelectedValue, DropNewDueMinFrom.SelectedValue, DropNewDueTimeZoneFrom.SelectedValue);
        txtNewIndFrom.Text = dtable.Rows[0]["Mins"].ToString();
    }

    protected void DropNewDueTimeZoneFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(DropNewLocation.SelectedValue, DropNewDueTimeFrom.SelectedValue, DropNewDueMinFrom.SelectedValue, DropNewDueTimeZoneFrom.SelectedValue);
        txtNewIndFrom.Text = dtable.Rows[0]["Mins"].ToString();
    }
    protected void DropNewDueTimeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(DropNewLocation.SelectedValue, DropNewDueTimeTo.SelectedValue, DropNewDueMinTo.SelectedValue, DropNewDueTimeZoneTo.SelectedValue);
        txtNewIndTo.Text = dtable.Rows[0]["Mins"].ToString();
    }
    protected void DropNewDueMinTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(DropNewLocation.SelectedValue, DropNewDueTimeTo.SelectedValue, DropNewDueMinTo.SelectedValue, DropNewDueTimeZoneTo.SelectedValue);
        txtNewIndTo.Text = dtable.Rows[0]["Mins"].ToString();
    }
    protected void DropNewDueTimeZoneTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(DropNewLocation.SelectedValue, DropNewDueTimeTo.SelectedValue, DropNewDueMinTo.SelectedValue, DropNewDueTimeZoneTo.SelectedValue);
        txtNewIndTo.Text = dtable.Rows[0]["Mins"].ToString();
    }
    protected void lboxNewtask_SelectedIndexChanged(object sender, EventArgs e)
    {
        string TaskName = "";
        string TaskValue = "";
        Session["TaskValue"] = "";
        DataTable dTable = new DataTable();
        dTable.Columns.Add("Task");
        dTable.Columns.Add("Task_ID");
        CheckBoxNewTask.Items.Clear();
        chkFileType.Items.Clear();
        for (int j = 0; j < lboxNewtask.Items.Count; j++)
        {
            if (lboxNewtask.Items[j].Selected == true)
            {
                if (TaskName == "")
                {
                    TaskName = lboxNewtask.Items[j].Text;
                    TaskValue = lboxNewtask.Items[j].Value;
                }
                else
                {
                    TaskName = TaskName + ',' + lboxNewtask.Items[j].Text;
                    TaskValue = TaskValue + ',' + lboxNewtask.Items[j].Value;
                }
                Session["TaskValue"] = TaskValue.ToString();
                dTable.Rows.Add(lboxNewtask.Items[j].Text, lboxNewtask.Items[j].Value);

                ListItem it = new ListItem();
                it.Value = lboxNewtask.Items[j].Value;
                it.Text = lboxNewtask.Items[j].Text;
                CheckBoxNewTask.Items.Add(it);
            }
        }
        DataSet Ds = new DataSet();
        Ds.Tables.Add(dTable);
        //gv_Soft.DataSource = Ds;
        //gv_Soft.DataBind();
        

        Ds = nonLa.GetFormats(TaskName.Replace(",", "','"));
        lboxNewformat.DataTextField = Ds.Tables[0].Columns[1].ToString();
        lboxNewformat.DataValueField = Ds.Tables[0].Columns[0].ToString();
        lboxNewformat.DataSource = Ds;
        lboxNewformat.DataBind();
        if (TaskValue.ToString() == "1" || TaskValue.ToString() == "2" || TaskValue.ToString() == "6" || TaskValue.ToString() == "1,2" || TaskValue.ToString() == "1,6" || TaskValue.ToString() == "1,2,6" || TaskValue.ToString() == "2,6")
        {
            ListItem li1 = new ListItem("Source", "0", true);
            //ListItem li2 = new ListItem("Target", "1", true);
            li1.Selected = true;
            //li2.Selected = true;
            li1.Enabled = true;
            //li2.Enabled = false;
            chkFileType.Items.Add(li1);
            //chkFileType.Items.Add(li2);
        }
        else
        {
            ListItem li1 = new ListItem("Source", "0", true);
            ListItem li2 = new ListItem("Target", "1", true);
            //li1.Selected = true;
            //li2.Selected = true;
            li1.Enabled = true;
            li2.Enabled = true;
            chkFileType.Items.Add(li1);
            chkFileType.Items.Add(li2);
        }
        if (Ds.Tables[0].Rows[0]["taskname"].ToString() == "TE" )
        {
            lblNewsource.Visible = true;
            DropNewSource.Visible = true;
            lboxNewformat.Visible = true;
            lblNewformat.Visible = true;
            TarRecDate.Visible = false;
            txtNewRecDate.Visible = false;
            img9.Visible = false;
        }
        else if (Ds.Tables[0].Rows[0]["taskname"].ToString() == "DTP" || Ds.Tables[0].Rows[0]["taskname"].ToString() == "File Conversion")
        {
            lblNewsource.Visible = false;
            DropNewSource.Visible = false;
            lboxNewformat.Visible = true;
            lblNewformat.Visible = true;
            TarRecDate.Visible = false;
            txtNewRecDate.Visible = false;
            img9.Visible = false;
        }
        else
        {
            lboxNewformat.Visible = false;
            lblNewformat.Visible = false;
            lblNewsource.Visible = false;
            DropNewSource.Visible = false;
            TarRecDate.Visible = false;
            txtNewRecDate.Visible = false;
            img9.Visible = false;
        }
    }
    protected void btnNewlangadd_Click(object sender, EventArgs e)
    {
        string id = "";
        try
        {
            DataSet dss = new DataSet();
            if (hfP_ID.Value != "")
                id = hfP_ID.Value.Trim();
            else if (tempID.Text != "")
                id = tempID.Text;
            if (id != "")
            {
                int countLang = 0;
                foreach (ListItem li in lboxNewlang.Items)
                    if (li.Selected)
                        countLang++;

                int countTask = 0;
                foreach (ListItem li in lboxNewtask.Items)
                    if (li.Selected)
                        countTask++;

                int countChkTask = 0;
                foreach (ListItem li in CheckBoxNewTask.Items)
                    if (li.Selected)
                        countChkTask++;
                
                int countChkTarFiles = 0;
                foreach (ListItem li in chkFileType.Items)
                    if (li.Selected)
                        countChkTarFiles++;

                int countSW = 0;
                foreach (ListItem li in lboxSW.Items)
                    if (li.Selected)
                        countSW++;

                int countSWVer = 0;
                foreach (ListItem li in lboxSWVer.Items)
                    if (li.Selected)
                        countSWVer++;

                if (countTask > 0)
                {
                    if (countChkTask > 0 && countLang > 0 && countChkTarFiles > 0 && countSW > 0 && countSWVer > 0)
                    {
                        for (int kk = 0; kk < CheckBoxNewTask.Items.Count; kk++)
                        {
                            if (CheckBoxNewTask.Items[kk].Selected == true)
                            {
                                for (int k = 0; k < lboxNewlang.Items.Count; k++)
                                {
                                    if (lboxNewlang.Items[k].Selected == true)
                                    {
                                        for (int j = 0; j < lboxSW.Items.Count; j++)
                                        {
                                            if (lboxSW.Items[j].Selected == true)
                                            {
                                                for (int t = 0; t < lboxSWVer.Items.Count; t++)
                                                {
                                                    if (lboxSWVer.Items[t].Selected == true)
                                                    {
                                                        DataSet vs = new DataSet();
                                                        vs = nonLa.GetSoftVer(lboxSW.Items[j].Value, lboxSWVer.Items[t].Value);
                                                        if (vs != null)
                                                        {
                                                            for (int w = 0; w < chkFileType.Items.Count; w++)
                                                            {
                                                                if (chkFileType.Items[w].Selected == true)
                                                                {
                                                                    dss = nonLa.GetInsertedLangLP(CheckBoxNewTask.Items[kk].Value, lboxNewlang.Items[k].Value, id.ToString());
                                                                    if (dss != null)
                                                                    {
                                                                        if (CheckBoxNewTask.Items[kk].Value == "1" || CheckBoxNewTask.Items[kk].Value == "2" || CheckBoxNewTask.Items[kk].Value == "6")
                                                                        {
                                                                            DataSet dt = new DataSet();
                                                                            dt = nonLa.GetTarSoftLP(CheckBoxNewTask.Items[kk].Value, lboxNewlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value);
                                                                            if (dt == null)
                                                                            {
                                                                                nonLa.InsertTarSoftLP(CheckBoxNewTask.Items[kk].Value, lboxNewlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value, chkFileType.Items[w].Value);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            nonLa.InsertSoftLP(CheckBoxNewTask.Items[kk].Value, lboxNewlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value, chkFileType.Items[w].Value);
                                                                        }
                                                                        //ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Selected " + lboxlang.SelectedItem + " already inserted for " + CheckBoxTask.Items[kk].Text + " Task');</script>");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (CheckBoxNewTask.Items[kk].Value == "1" || CheckBoxNewTask.Items[kk].Value == "2" || CheckBoxNewTask.Items[kk].Value == "6")
                                                                        {
                                                                            nonLa.InsertLangLP(CheckBoxNewTask.Items[kk].Value, lboxNewlang.Items[k].Value, id.ToString());
                                                                            DataSet dt = new DataSet();
                                                                            dt = nonLa.GetTarSoftLP(CheckBoxNewTask.Items[kk].Value, lboxNewlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value);
                                                                            if (dt == null)
                                                                            {
                                                                                nonLa.InsertTarSoftLP(CheckBoxNewTask.Items[kk].Value, lboxNewlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value, chkFileType.Items[w].Value);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            nonLa.InsertLangLP(CheckBoxNewTask.Items[kk].Value, lboxNewlang.Items[k].Value, id.ToString());
                                                                            nonLa.InsertSoftLP(CheckBoxNewTask.Items[kk].Value, lboxNewlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value, chkFileType.Items[w].Value);

                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (countLang == 0)
                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected Language');</script>");
                        else if (countChkTask == 0)
                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected Task for this Language');</script>");
                        else if (countSW == 0)
                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected Software');</script>");
                        else if (countSWVer == 0)
                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected Software Version');</script>");
                        else if (countChkTarFiles == 0)
                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected File Type');</script>");
                    }
                    dss = nonLa.GetselectedLangLP(id.ToString());
                    if (dss != null)
                    {
                        lboxNewlangused.DataSource = dss;
                        lboxNewlangused.DataTextField = dss.Tables[0].Columns[1].ToString();
                        lboxNewlangused.DataValueField = dss.Tables[0].Columns[0].ToString();
                        lboxNewlangused.DataBind();
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Select Task');</script>");
                }
                lboxNewlang.ClearSelection();
                //lboxNewtask.ClearSelection();
                CheckBoxNewTask.ClearSelection();
                lboxSW.ClearSelection();
                lboxSWVer.ClearSelection();
                if (Session["TaskValue"].ToString() == "1" || Session["TaskValue"].ToString() == "2" ||
                    Session["TaskValue"].ToString() == "6" || Session["TaskValue"].ToString() == "1,2" ||
                    Session["TaskValue"].ToString() == "1,6" || Session["TaskValue"].ToString() == "1,2,6" ||
                    Session["TaskValue"].ToString() == "2,6")
                {
                    //chkFileType.ClearSelection();
                }
                else
                {
                    chkFileType.ClearSelection();
                }

                dss = nonLa.getTaskLangDetailsLP(id.ToString());
                gv_SoftNew.DataSource = dss;
                gv_SoftNew.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
        }
    }
    protected void btnNewlangdel_Click(object sender, EventArgs e)
    {
        string id = "";
        try
        {
            if (hfP_ID.Value != "")
                id = hfP_ID.Value.Trim();
            else if (tempID.Text != "")
                id = tempID.Text;
            if (id != "")
            {
                for (int j = 0; j < lboxNewlangused.Items.Count; j++)
                {
                    if (lboxNewlangused.Items[j].Selected == true)
                    {
                        nonLa.DeleteUsedtLangLP(lboxNewlangused.Items[j].Value, id.ToString());
                    }
                }
                DataSet dss = new DataSet();
                dss = nonLa.GetselectedLangLP(id.ToString());
                lboxNewlangused.Items.Clear();
                if (dss != null)
                {
                    lboxNewlangused.DataSource = dss;
                    lboxNewlangused.DataTextField = dss.Tables[0].Columns[1].ToString();
                    lboxNewlangused.DataValueField = dss.Tables[0].Columns[0].ToString();
                    lboxNewlangused.DataBind();
                }
                lboxSWVer.Items.Clear();
                dss = nonLa.getTaskLangDetailsLP(id.ToString());
                gv_SoftNew.DataSource = dss;
                gv_SoftNew.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
        }
    }
    protected void chkNewDueDate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNewDueDate.Checked)
        {
            lblNewDueFrom.Visible = true;
            txtNewdueFromdate.Visible = true;
            lblNewDueTo.Visible = true;
            txtNewdueTodate.Visible = true;
            calenderNewTo.Visible = true;
        }
        else
        {
            lblNewDueFrom.Visible = false;
            txtNewdueFromdate.Visible = true;
            lblNewDueTo.Visible = false;
            txtNewdueTodate.Visible = false;
            calenderNewTo.Visible = false;
        }
    }
    protected void chkNewDueTime_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNewDueTime.Checked)
        {
            lblNewFrom.Visible = true;
            lblNewTo.Visible = true;
            DropNewDueTimeTo.Visible = true;
            DropNewDueMinTo.Visible = true;
            DropNewDueTimeZoneTo.Visible = true;
            txtNewIndTo.Visible = true;
            lblNewIndFrom.Visible = true;
            lblNewIndTo.Visible = true;
        }
        else
        {
            lblNewFrom.Visible = false;
            lblNewTo.Visible = false;
            DropNewDueTimeTo.Visible = false;
            DropNewDueMinTo.Visible = false;
            DropNewDueTimeZoneTo.Visible = false;
            txtNewIndTo.Visible = false;
            lblNewIndFrom.Visible = false;
            lblNewIndTo.Visible = false;
        }
    }
    protected void lnkNewJobInfo_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            loadNewJobDetails(hfP_ID.Value.Trim());
            btnNewJobInfo.Text = "Update";
        }
        else
        {
            Random rand = new Random();
            int name = rand.Next(1, 999999999);
            tempID.Text = name.ToString();// DateTime.Now.ToString().GetHashCode().ToString("x"); 
        }
        TarRecDate.Visible = false;
        txtNewRecDate.Visible = false;
        img9.Visible = false;
        this.showPanel(tabNewLaunch);
    }
    public void loadNewJobDetails(string sJobID)
    {
        if (sJobID != "")
        {
            lboxNewformat.ClearSelection();
            lboxNewtask.ClearSelection();
            lboxNewInputFile.ClearSelection();
            lboxNewlang.ClearSelection();
            CheckBoxNewTask.Items.Clear();
            lboxNewlangused.ClearSelection();
            DataSet dsNL = nonLa.getJobDetailsByLPID(sJobID);
            lblNLHeader.Text = "Edit Project";
            imgNLHeader.Src = "images/tools/edit.png";
            DataRow row = dsNL.Tables[0].Rows[0];
            txtNewJobid.Text = row["JOBID"].ToString();
            txtNewProjectEditor.Text = row["PROJECTEDITOR"].ToString();
            txtNewProjectTitle.Text = row["projectname"].ToString();
            drpNewProjectcustomer.SelectedValue = row["custno"].ToString();
            DropNewSource.SelectedValue = row["SourceType"].ToString();
            //txtFile.Text = row["FILE_COUNT"].ToString();
            //getloc(Convert.ToInt16(row["cust_id"].ToString()));
            DataSet ds = nonLa.GetLocationCust(drpNewProjectcustomer.SelectedValue);
            DropNewLocation.DataSource = ds;
            DropNewLocation.DataValueField = ds.Tables[0].Columns[3].ToString();
            DropNewLocation.DataTextField = ds.Tables[0].Columns[4].ToString();
            DropNewLocation.DataBind();
            DropNewLocation.SelectedValue = row["LOCATION_id"].ToString();
            getloc_timezoneNew(Convert.ToInt16(row["LOCATION_id"].ToString()));
            dropNewSwPlat.SelectedValue = row["platform"].ToString();
            dropNewNoofFolder.SelectedValue = row["NOOF_PACKAGE"].ToString();
            txtNewPackageSize.Text = row["PACK_SIZE"].ToString();
            dropNewPackageSize.SelectedValue = row["PACK_SIZE_BYTES"].ToString();
            txtNewFileLocation.Text = row["FILE_PATH"].ToString();
            dropNewDownPackage.SelectedValue = row["FTPYN"].ToString();
            txtWOnumber.Text = row["jobno"].ToString();
            dropTATsDays.SelectedValue = row["TATsDay"].ToString();
            dropTATsHrs.SelectedValue = row["TATsHrs"].ToString();
            if (txtNewJobid.Text != "")
            {
                btnNewJobInfo.Visible = false;
            }
            else
            {
                btnNewJobInfo.Visible = true;
            }
            if (row["DUEDATEYN"].ToString() == "1")
            {
                lblNewDueFrom.Visible = true;
                txtNewdueFromdate.Visible = true;
                lblNewDueTo.Visible = true;
                txtNewdueTodate.Visible = true;
                chkNewDueDate.Checked = true;
            }
            else
            {
                lblNewDueFrom.Visible = false;
                txtNewdueFromdate.Visible = true;
                lblNewDueTo.Visible = false;
                txtNewdueTodate.Visible = false;
                chkNewDueDate.Checked = false;
            }
            if (row["DUETIMEYN"].ToString() == "1")
            {
                lblFrom.Visible = true;
                lblTo.Visible = true;
                chkNewDueTime.Checked = true;
                DropNewDueTimeTo.Visible = true;
                DropNewDueMinTo.Visible = true;
                DropNewDueTimeZoneTo.Visible = true;
                txtNewIndTo.Visible = true;
                DropNewDueTimeFrom.SelectedValue = row["DUE_TIMEFROM"].ToString();
                DropNewDueMinFrom.SelectedValue = row["DUE_MINFROM"].ToString();
                DropNewDueTimeZoneFrom.SelectedValue = row["TIME_ZONEFROM"].ToString();
                DropNewDueTimeTo.SelectedValue = row["DUE_TIMETO"].ToString();
                DropNewDueMinTo.SelectedValue = row["DUE_MINTO"].ToString();
                DropNewDueTimeZoneTo.SelectedValue = row["TIME_ZONEFROM"].ToString();
                txtNewIndFrom.Text = row["duetimefrom_ist"].ToString();
                txtNewIndTo.Text = row["duetimeto_ist"].ToString();
                lblNewIndFrom.Visible = true;
                lblNewIndTo.Visible = true;
            }
            else
            {
                lblNewFrom.Visible = false;
                lblNewTo.Visible = false;
                chkNewDueTime.Checked = false;
                DropNewDueTimeFrom.SelectedValue = row["DUE_TIMEFROM"].ToString();
                DropNewDueMinFrom.SelectedValue = row["DUE_MINFROM"].ToString();
                DropNewDueTimeZoneFrom.SelectedValue = row["TIME_ZONEFROM"].ToString();
                DropNewDueTimeTo.SelectedValue = "00";
                DropNewDueMinTo.SelectedValue = "00";
                DropNewDueTimeZoneTo.SelectedValue = row["TIME_ZONEFROM"].ToString();
                txtNewIndFrom.Text = row["duetimefrom_ist"].ToString();
                txtNewIndTo.Text = "";
                lblNewIndFrom.Visible = false;
                lblNewIndTo.Visible = false;
            }
            if (row["ytcyn"].ToString() == "1") 
                chkNewYTC.Checked = true;
            else
                chkNewYTC.Checked = false;
            //if (chkNewYTC.Checked)
            //{
            //    lblTATs.Visible = true;
            //    dropTATsDays.Visible = true;
            //    dropTATsHrs.Visible = true;
            //    Label28.Visible = true;
            //}
            //else
            //{
            //    lblTATs.Visible = false;
            //    dropTATsDays.Visible = false;
            //    dropTATsHrs.Visible = false;
            //    Label28.Visible = false;

            if (row["DUE_DATEFrom"].ToString() == "")
                txtNewdueFromdate.Text = "";
            else
                txtNewdueFromdate.Text = DateTime.Parse(row["DUE_DATEFrom"].ToString()).ToShortDateString();
            if (row["DUE_DATETo"].ToString() == "")
                txtNewdueTodate.Text = "";
            else
                txtNewdueTodate.Text = DateTime.Parse(row["DUE_DATETo"].ToString()).ToShortDateString();
            DataSet ds1 = new DataSet();
            ds1 = nonLa.TaskSelectedLP(sJobID);
            string Task = "";
            if (ds1 != null)
            {
                
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    Session["TaskValue"] = "";
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        lboxNewtask.Items[lboxNewtask.Items.IndexOf(lboxNewtask.Items.FindByValue(ds1.Tables[0].Rows[i]["task_id"].ToString()))].Selected = true;
                        if (Task == "")
                            Task = ds1.Tables[0].Rows[i]["task_id"].ToString();
                        else
                            Task = Task + ',' + ds1.Tables[0].Rows[i]["task_id"].ToString();
                    }
                    Session["TaskValue"] = Task.ToString();
                }
                ds1 = nonLa.GetSelectedFormats(Task);
                lboxNewformat.DataTextField = ds1.Tables[0].Columns[1].ToString();
                lboxNewformat.DataValueField = ds1.Tables[0].Columns[0].ToString();
                lboxNewformat.DataSource = ds1;
                lboxNewformat.DataBind();
                chkFileType.Items.Clear();
                if (Task.ToString() == "1" || Task.ToString() == "2" || Task.ToString() == "6" || Task.ToString() == "1,2" || Task.ToString() == "1,6" || Task.ToString() == "1,2,6" || Task.ToString() == "2,6")
                {
                    ListItem li1 = new ListItem("Source", "0", true);
                    li1.Selected = true;
                    li1.Enabled = true;
                    chkFileType.Items.Add(li1);
                }
                else
                {
                    ListItem li1 = new ListItem("Source", "0", true);
                    ListItem li2 = new ListItem("Target", "1", true);
                    li1.Enabled = true;
                    li2.Enabled = true;
                    chkFileType.Items.Add(li1);
                    chkFileType.Items.Add(li2);
                }
                if (Task == "1" || Task == "1,2" || Task == "1,3" || Task == "1,4" || Task == "1,5" || Task == "1,6" || Task == "1,2,3")
                {
                    lblNewsource.Visible = true;
                    DropNewSource.Visible = true;
                    lboxNewformat.Visible = true;
                    lblNewformat.Visible = true;
                    TarRecDate.Visible = false;
                    txtNewRecDate.Visible = false;
                    img9.Visible = false;
                }
                else if (Task.ToString() == "3" || Task.ToString() == "6")
                {
                    lblNewsource.Visible = false;
                    DropNewSource.Visible = false;
                    lboxNewformat.Visible = true;
                    lblNewformat.Visible = true;
                    TarRecDate.Visible = false;
                    txtNewRecDate.Visible = false;
                    img9.Visible = false;
                }
                else
                {
                    lboxNewformat.Visible = false;
                    lblNewformat.Visible = false;
                    lblNewsource.Visible = false;
                    DropNewSource.Visible = false;
                    TarRecDate.Visible = false;
                    txtNewRecDate.Visible = false;
                    img9.Visible = false;
                }
            }
            DataSet tn = new DataSet();
            tn = nonLa.getTaskLangDetailsLP(sJobID);
            gv_SoftNew.DataSource = tn;
            gv_SoftNew.DataBind();
            
            ds1 = nonLa.FormatSelectedLP(sJobID);
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        lboxNewformat.Items[lboxNewformat.Items.IndexOf(lboxNewformat.Items.FindByValue(ds1.Tables[0].Rows[i]["Format"].ToString()))].Selected = true;
                }
            }
            ds1 = nonLa.InputSelectedLP(sJobID);
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        lboxNewInputFile.Items[lboxNewInputFile.Items.IndexOf(lboxNewInputFile.Items.FindByValue(ds1.Tables[0].Rows[i]["input"].ToString()))].Selected = true;
                }
            }
            ds1 = nonLa.GetselectedLangLP(sJobID);
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        //lboxNewlang.Items[lboxNewlang.Items.IndexOf(lboxNewlang.Items.FindByValue(ds1.Tables[0].Rows[i]["Lang_ID"].ToString()))].Selected = true;
                }
                lboxNewlangused.DataSource = ds1;
                lboxNewlangused.DataTextField = ds1.Tables[0].Columns[1].ToString();
                lboxNewlangused.DataValueField = ds1.Tables[0].Columns[0].ToString();
                lboxNewlangused.DataBind();
            }
            
            for (int j = 0; j < lboxNewtask.Items.Count; j++)
            {
                if (lboxNewtask.Items[j].Selected == true)
                {
                    ListItem it = new ListItem();
                    it.Value = lboxNewtask.Items[j].Value;
                    it.Text = lboxNewtask.Items[j].Text;
                    CheckBoxNewTask.Items.Add(it);
                }
            }

        }
    }
    protected void lnkNewFileInfo_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            DataSet ds = new DataSet();
            ds = nonLa.getTaskLangDetailsByLPID(hfP_ID.Value);
            DataSet ds1 = new DataSet();
            ds1 = nonLa.getTarTaskLangDetailsByLPID(hfP_ID.Value);
            if (hfP_ID.Value != "")
            {
                gvFileInfo.DataSource = ds;
                gvFileInfo.DataBind();
                gvTarFileInfo.DataSource = ds1;
                gvTarFileInfo.DataBind();
            }
            NewFileInfo();
        }
        this.showPanel(tabNewFileDetails);
    }
    public void NewFileInfo()
    {
        Session["TaskValue"] = "";
        if (hfP_ID.Value != "")
        {
            DataSet ufs = new DataSet();
            ufs = nonLa.GetUsageFonts(hfP_ID.Value);
            lboxNewUFonts.DataTextField = "Fonts";
            lboxNewUFonts.DataValueField = "Fonts";
            lboxNewUFonts.DataSource = ufs;
            lboxNewUFonts.DataBind();

            DataSet tf1 = new DataSet();
            tf1 = nonLa.GetFonts(hfP_ID.Value);
            lboxNewTFonts.DataTextField = "Fonts";
            lboxNewTFonts.DataValueField = "Fonts";
            lboxNewTFonts.DataSource = tf1;
            lboxNewTFonts.DataBind();

            DataSet mf = new DataSet();
            mf = nonLa.GetMFonts(hfP_ID.Value);
            lboxNewMFonts.DataTextField = "Fonts";
            lboxNewMFonts.DataValueField = "Fonts";
            lboxNewMFonts.DataSource = mf;
            lboxNewMFonts.DataBind();

            DataSet dsNL = nonLa.getJobDetailsByLPID(hfP_ID.Value);
            DataRow row = dsNL.Tables[0].Rows[0];
            if (row["TARGETDATE"].ToString() == "")
                txtNewtarget.Text = "";
            else
                txtNewtarget.Text = DateTime.Parse(row["TARGETDATE"].ToString()).ToShortDateString();
            if (row["SOURCEDATE"].ToString() == "")
                txtNewsource.Text = "";
            else
                txtNewsource.Text = DateTime.Parse(row["SOURCEDATE"].ToString()).ToShortDateString();
            if (row["ytryn"].ToString() == "1")
            {
                CheckNewYTR.Checked = true;
                gvTarFileInfo.Visible = false;
            }
            else
            {
                CheckNewYTR.Checked = false;
                gvTarFileInfo.Visible = true;
            }
            txtNewfiglinks.Text = row["MISS_FIG_LINK"].ToString();
            DropNewnooftables.Text = row["TABLES"].ToString();
            txtNewproof.Text = row["PROOF"].ToString();
            txtNewpress.Text = row["PRESS_PRINT"].ToString();
            txtNewpagesize.Text = row["page_size"].ToString();
            lblNewMail.Text = row["MailDetails"].ToString();
            DropNewcomplex.SelectedValue = row["COMPLEX_LEVEL"].ToString();
            if (row["jobid"].ToString() != "")
            {
                btnNewSave.Visible = false;
                btnNewClearfile.Visible = false;
            }
            else
            {
                btnNewSave.Visible = true;
                btnNewClearfile.Visible = true;
            }
            if (row["FILENAME_CONV"].ToString() != "")
            {
                DropNewNameConv.SelectedValue = row["FILENAME_CONV"].ToString();
            }
            else
            {
                DropNewNameConv.SelectedValue = "As per source";
            }
            txtNewSplIns.Text = row["REMARKS"].ToString();
            txtNewBleed.Text = row["bleed"].ToString();
            txtNewFonts.Text = row["OTHER_FONTS"].ToString();
            txtNewMissFonts.Text = row["OTHER_MISS_FONTS"].ToString();
            txtNewOtherUsageFonts.Text = row["OTHER_UASGE_FONTS"].ToString();

            DataSet ds = new DataSet();
            ds = nonLa.GetQueries(row["LP_ID"].ToString());
            gv_QueriesNew.DataSource = ds;
            gv_QueriesNew.DataBind();

            DataSet dsCost5 = nonLa.GetComplexReason(hfP_ID.Value, row["COMPLEX_LEVEL"].ToString());
            ListNewComplexReason.DataSource = dsCost5;
            ListNewComplexReason.DataValueField = dsCost5.Tables[0].Columns[0].ToString();
            ListNewComplexReason.DataTextField = dsCost5.Tables[0].Columns[2].ToString();
            ListNewComplexReason.DataBind();
            if (row["Reason_id"].ToString() != "0")
            {
                DataSet empds = new DataSet();
                empds = nonLa.getComplexReason(row["LP_id"].ToString());
                if (empds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < empds.Tables[0].Rows.Count; i++)
                        ListNewComplexReason.Items[ListNewComplexReason.Items.IndexOf(ListNewComplexReason.Items.FindByValue(empds.Tables[0].Rows[i]["Reason_id"].ToString()))].Selected = true;
                }
            }
            DataSet empds3 = new DataSet();
            empds3 = nonLa.DeliveryType(row["LP_id"].ToString());
            if (empds3!=null)
            {
                for (int i = 0; i < empds3.Tables[0].Rows.Count; i++)
                    lboxNewdelivryType.Items[lboxNewdelivryType.Items.IndexOf(lboxNewdelivryType.Items.FindByValue(empds3.Tables[0].Rows[i]["delivery_type"].ToString()))].Selected = true;
            }
            for (int i = 0; i < dsNL.Tables[0].Rows.Count; i++)
            {
                if (y == "" || y == null)
                    y = row["DELIVERYTYPE"].ToString();
                else
                    y = y + ',' + row["DELIVERYTYPE"].ToString();
            }
            if (y.ToString() == "1" || y.ToString() == "1,2" || y.ToString() == "1,3" || y.ToString() == "1,2,3")
            {
                lblNewSoft.Visible = false;
                gv_soft1New.Visible = false;
            }
            else
            {
                lblNewSoft.Visible = false;
                gv_soft1New.Visible = false;
            }
            DataSet tn = new DataSet();
            tn = nonLa.getTaskLangDetailsLP(hfP_ID.Value);
            gv_soft1New.DataSource = tn;
            gv_soft1New.DataBind();

            tn = nonLa.getJobTrackingLP(hfP_ID.Value);
            if (tn != null)
            {
                gv_imagesNew.DataSource = tn;
                gv_imagesNew.DataBind();
            }

            DataSet ds1 = new DataSet();
            ds1 = nonLa.TaskSelectedLP(hfP_ID.Value);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    if (x == "" || x == null)
                        x = ds1.Tables[0].Rows[i]["task_id"].ToString();
                    else
                        x = x + ',' + ds1.Tables[0].Rows[i]["task_id"].ToString();
                }
                Session["TaskValue"] = x.ToString();
                if (x.ToString() == "1")
                {
                    lblNewsource.Visible = true;
                    DropNewSource.Visible = true;
                    lblNewTarDate.Visible = false;
                    txtNewtarget.Visible = false;
                    img11.Visible = false;
                    CheckNewYTR.Visible = false;
                    gvTarFileInfo.Visible = false;
                    lblTarName.Visible = false;
                    lblNewmissfiglink.Visible = false;
                    txtNewfiglinks.Visible = false;
                    lblNewproof.Visible = false;
                    txtNewproof.Visible = false;
                    txtNewpress.Visible = false;
                    lblNewpress.Visible = false;
                    lblNewtarLang.Text = "Source Languages:";
                    lblNewfonts.Text = "Usage of fonts with respect to Source languages:";
                    lboxNewusagefonts.Visible = false;
                    txtNewOtherUsageFonts.Visible = false;
                    f1New.Visible = false;
                    lblNewUsageFonts.Visible = false;
                    btnNewUFontsadd.Visible = false;
                    btnNewUFontsRemove.Visible = false;
                    lboxNewUFonts.Visible = false;
                    lblNewSoft.Visible = false;
                    gv_soft1New.Visible = false;
                    txtNewFonts.Visible = true;
                    f2New.Visible = true;
                    txtNewMissFonts.Visible = true;
                    f3New.Visible = true;
                    imgNew4.Visible = false;
                    ImageButton2.Visible = false;
                }
                else if (x.ToString() == "2" || x.ToString() == "5")
                {
                    lblNewsource.Visible = false;
                    DropNewSource.Visible = false;
                    lblNewformat.Visible = false;
                    lboxNewformat.Visible = false;
                    imgNew4.Visible = false;
                    ImageButton2.Visible = false;
                }
                else if (x.ToString() == "4")
                {
                    lblNewsource.Visible = false;
                    DropNewSource.Visible = false;
                    lblNewformat.Visible = false;
                    lboxNewformat.Visible = false;
                    lblNewproof.Visible = false;
                    txtNewproof.Visible = false;
                    txtNewpress.Visible = false;
                    lblNewpress.Visible = false;
                    txtNewfiglinks.Visible = false;
                    lblNewmissfiglink.Visible = false;
                    lboxNewFonts.Visible = false;
                    lboxNewMissFonts.Visible = false;
                    lblNewmissfonts.Visible = false;
                    lblNewfonts.Visible = false;
                    lblNewimg.Visible = false;
                    //imgRefresh.Visible = false;
                    gv_imagesNew.Visible = false;
                    lblNewtable.Visible = false;
                    lblNewnotable.Visible = false;
                    DropNewnooftables.Visible = false;
                    lblNewpdf.Visible = false;
                    lblNewpage.Visible = false;
                    txtNewpagesize.Visible = false;
                    lblNewbleed.Visible = false;
                    txtNewBleed.Visible = false;
                    lboxNewusagefonts.Visible = false;
                    txtNewOtherUsageFonts.Visible = false;
                    f1New.Visible = false;
                    lblNewUsageFonts.Visible = false;
                    btnNewUFontsadd.Visible = false;
                    btnNewUFontsRemove.Visible = false;
                    lboxNewUFonts.Visible = false;
                    lblNewSoft.Visible = false;
                    gv_soft1New.Visible = false;
                    btnNewfontsadd.Visible = false;
                    btnNewfontsdel.Visible = false;
                    lboxNewTFonts.Visible = false;
                    btnNewmissfontsadd.Visible = false;
                    btnNewmissfontsdel.Visible = false;
                    lboxNewMFonts.Visible = false;
                    txtNewFonts.Visible = false;
                    f2New.Visible = false;
                    txtNewMissFonts.Visible = false;
                    f3New.Visible = false;
                    f1New.Visible = false;
                    imgNew4.Visible = false;
                    ImageButton2.Visible = false;
                }
                else if (x.ToString() == "6")
                {
                    lblNewsource.Visible = false;
                    DropNewSource.Visible = false;
                    lblNewformat.Visible = false;
                    lboxNewformat.Visible = false;
                    lblNewimg.Visible = false;
                    //imgRefresh.Visible = false;
                    gv_imagesNew.Visible = false;
                    lblNewtable.Visible = false;
                    lblNewnotable.Visible = false;
                    DropNewnooftables.Visible = false;
                    lblNewpdf.Visible = false;
                    lblNewpage.Visible = false;
                    txtNewpagesize.Visible = false;
                    lblNewbleed.Visible = false;
                    txtNewBleed.Visible = false;
                    lblNewproof.Visible = false;
                    txtNewproof.Visible = false;
                    txtNewpress.Visible = false;
                    lblNewpress.Visible = false;
                    lblNewTarDate.Visible = false;
                    txtNewtarget.Visible = false;
                    img11.Visible = false;
                    CheckNewYTR.Visible = false;
                    gvTarFileInfo.Visible = false;
                    lblTarName.Visible = false;
                    lblNewcomplex.Visible = false;
                    //lblNewcomplexlev.Visible = false;
                    //DropNewcomplex.Visible = false;
                    //lblNewReason.Visible = false;
                    //ListNewComplexReason.Visible = false;
                    lblNewtarLang.Visible = false;
                    lboxNewlang.Visible = false;
                    btnNewlangadd.Visible = false;
                    btnNewlangdel.Visible = false;
                    lboxNewlangused.Visible = false;
                    lblNewmissfiglink.Text = "Missing Links:";
                    lblNewSourceDate.Text = "File for Conversion Received on:";
                    lboxNewusagefonts.Visible = false;
                    txtNewOtherUsageFonts.Visible = false;
                    f1New.Visible = false;
                    lblNewUsageFonts.Visible = false;
                    btnNewUFontsadd.Visible = false;
                    btnNewUFontsRemove.Visible = false;
                    lboxNewUFonts.Visible = false;
                    lblNewSoft.Visible = false;
                    gv_soft1New.Visible = false;
                    imgNew4.Visible = false;
                    ImageButton2.Visible = false;
                }
                else
                {
                    lboxNewusagefonts.Visible = true;
                    lblNewUsageFonts.Visible = true;
                    lblNewTarDate.Visible = true;
                    txtNewtarget.Visible = true;
                    img11.Visible = true;
                    CheckNewYTR.Visible = true;
                    gvTarFileInfo.Visible = true;
                    lblTarName.Visible = true;
                    lblNewsource.Visible = false;
                    DropNewSource.Visible = false;
                    btnNewUFontsadd.Visible = true;
                    btnNewUFontsRemove.Visible = true;
                    lboxNewUFonts.Visible = true;
                    lblNewSoft.Visible = false;
                    gv_soft1New.Visible = false;
                    txtNewFonts.Visible = true;
                    f2New.Visible = true;
                    txtNewMissFonts.Visible = true;
                    f3New.Visible = true;
                    f1New.Visible = true;
                    imgNew4.Visible = false;
                    ImageButton2.Visible = false;
                }
            }
            if (row["ytryn"].ToString() == "1")
            {
                CheckNewYTR.Checked = true;
                gvTarFileInfo.Visible = false;
                lblTarName.Visible = false;
            }
            else
            {
                CheckNewYTR.Checked = false;
                gvTarFileInfo.Visible = true;
                lblTarName.Visible = true;
            }
        }
    }
    protected void gv_Soft1New_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet dscust1 = nonLa.getAllSoftware();
            ListBox lboxSoft = (ListBox)e.Row.FindControl("lboxSoft");
            TextBox txtTargetDate = (TextBox)e.Row.FindControl("txtTargetDate");
            HiddenField task = (HiddenField)e.Row.FindControl("hf_taskID");
            HiddenField lang = (HiddenField)e.Row.FindControl("hf_LangID");
            lboxSoft.DataSource = dscust1;
            lboxSoft.DataTextField = dscust1.Tables[0].Columns[1].ToString();
            lboxSoft.DataValueField = dscust1.Tables[0].Columns[0].ToString();
            lboxSoft.DataBind();
            string soft = "";
            DataSet sv = new DataSet();
            sv = nonLa.SoftSelectedLP(hfP_ID.Value);
            if (sv != null)
            {
                DataSet empd = new DataSet();
                empd = nonLa.GetSelectedSoftLP(hfP_ID.Value, task.Value, lang.Value);
                if (empd != null)
                {
                    if (empd.Tables[0].Rows.Count > 0 || empd != null)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxSoft.Items[lboxSoft.Items.IndexOf(lboxSoft.Items.FindByValue(empd.Tables[0].Rows[i]["Soft_id"].ToString()))].Selected = true;

                            if (soft == "" || soft == null)
                                soft = empd.Tables[0].Rows[i]["Soft_id"].ToString();
                            else
                                soft = soft + ',' + empd.Tables[0].Rows[i]["Soft_id"].ToString();
                        }
                    }
                    ListBox lboxVer = (ListBox)e.Row.FindControl("lboxVer");
                    if (soft.ToString() != "")
                    {
                        DataSet dsSoft = nonLa.GetSoftVers(soft.ToString());
                        lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                        lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                        lboxVer.DataSource = dsSoft;
                        lboxVer.DataBind();
                    }
                    if (empd.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxVer.Items[lboxVer.Items.IndexOf(lboxVer.Items.FindByValue(empd.Tables[0].Rows[i]["Version_id"].ToString()))].Selected = true;
                        }
                    }
                    txtTargetDate.Text = empd.Tables[0].Rows[0]["TARGET_DATE"].ToString();
                }
            }
        }
    }
    protected void lboxNew1Soft_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow grs in gv_soft1New.Rows)
        {
            DataSet dscust1 = la.getAllSoftware();
            ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
            Label task = (Label)grs.FindControl("txt_task");
            ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
            string sv = "";
            for (int j = 0; j < lboxSoft.Items.Count; j++)
            {
                if (lboxSoft.Items[j].Selected == true)
                {
                    if (sv == "")
                        sv = lboxSoft.Items[j].Value;
                    else
                        sv = sv + ',' + lboxSoft.Items[j].Value;
                }
            }
            if (sv.ToString() != "")
            {
                DataSet dsSoft = la.GetSoftVers(sv.ToString());
                lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                lboxVer.DataSource = dsSoft;
                lboxVer.DataBind();
            }
        }
    }
    protected void Edit(object sender, EventArgs e)
    {
        Session["FilePages"] = "";
        lblResult.Text = "";
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            HiddenField sJobID = (HiddenField)row.Cells[0].FindControl("hid_LP_ID");
            HiddenField hid_NTLS_ID = (HiddenField)row.Cells[0].FindControl("hid_NTLS_ID");
            HiddenField hid_Task_ID = (HiddenField)row.Cells[1].FindControl("hid_Task_ID");
            HiddenField hid_Lang_ID = (HiddenField)row.Cells[2].FindControl("hid_Lang_ID");
            HiddenField hid_Soft_ID = (HiddenField)row.Cells[3].FindControl("hid_Soft_ID");
            TextBox Files = (TextBox)row.Cells[4].FindControl("lblgvFiles");
            NL_ID.Text = sJobID.Value;
            NTLS_ID.Text = hid_NTLS_ID.Value;
            Task_ID.Text = hid_Task_ID.Value;
            Soft_ID.Text = hid_Soft_ID.Value;
            Lang_ID.Text = hid_Lang_ID.Value;
            txtFiles.Text = Files.Text;
            int File = 0;
            if (Files.Text != "")
                File = Convert.ToInt16(Files.Text);
            DataSet dss = new DataSet();
            DataTable dTable = new DataTable();
            dTable.Columns.Add("Files_ID");
            dTable.Columns.Add("LP_ID");
            dTable.Columns.Add("NTLS_ID");
            dTable.Columns.Add("TaskName");
            dTable.Columns.Add("Task_ID");
            dTable.Columns.Add("Lang_name");
            dTable.Columns.Add("Lang_ID");
            dTable.Columns.Add("Soft_Name");
            dTable.Columns.Add("Soft_ID");
            dTable.Columns.Add("Files_Name");
            dTable.Columns.Add("Pages");

            dss = nonLa.GetFilePages(hid_NTLS_ID.Value);
            if (dss == null)
            {
                dss = nonLa.getLPNTLS(hid_NTLS_ID.Value);

                for (int j = 1; j <= File; j++)
                {
                    dTable.Rows.Add(j, dss.Tables[0].Rows[0]["LP_ID"].ToString(), dss.Tables[0].Rows[0]["NTLS_ID"].ToString(),
                        dss.Tables[0].Rows[0]["TaskName"].ToString(), dss.Tables[0].Rows[0]["Task_ID"].ToString(),
                        dss.Tables[0].Rows[0]["Lang_name"].ToString(), dss.Tables[0].Rows[0]["Lang_ID"].ToString(),
                        dss.Tables[0].Rows[0]["Soft_Name"].ToString(), dss.Tables[0].Rows[0]["Soft_ID"].ToString(), "", 0);
                }
            }
            else
            {
                dss = nonLa.getLPInsertedNTLS(hid_NTLS_ID.Value);
                int k = 1;
                for (int j = 0; j <= (File - 1); j++)
                {
                    if (k <= dss.Tables[0].Rows.Count)
                    {
                        dTable.Rows.Add(dss.Tables[0].Rows[j]["Files_ID"].ToString(), dss.Tables[0].Rows[j]["LP_ID"].ToString(),
                            dss.Tables[0].Rows[j]["NTLS_ID"].ToString(),
                            dss.Tables[0].Rows[j]["TaskName"].ToString(), dss.Tables[0].Rows[j]["Task_ID"].ToString(),
                            dss.Tables[0].Rows[j]["Lang_name"].ToString(), dss.Tables[0].Rows[j]["Lang_ID"].ToString(),
                            dss.Tables[0].Rows[j]["Soft_Name"].ToString(), dss.Tables[0].Rows[j]["Soft_ID"].ToString(),
                            dss.Tables[0].Rows[j]["Files_Name"].ToString(), dss.Tables[0].Rows[j]["Pages"].ToString());
                    }
                    else
                    {
                        dTable.Rows.Add(k, dss.Tables[0].Rows[0]["LP_ID"].ToString(),
                            dss.Tables[0].Rows[0]["NTLS_ID"].ToString(),
                            dss.Tables[0].Rows[0]["TaskName"].ToString(), dss.Tables[0].Rows[0]["Task_ID"].ToString(),
                            dss.Tables[0].Rows[0]["Lang_name"].ToString(), dss.Tables[0].Rows[0]["Lang_ID"].ToString(),
                            dss.Tables[0].Rows[0]["Soft_Name"].ToString(), dss.Tables[0].Rows[0]["Soft_ID"].ToString(),
                            "", 0);
                    }
                    k++;
                }
            }
            DataSet Ds = new DataSet();
            Ds.Tables.Add(dTable);
            gv_FilePages.DataSource = Ds;
            gv_FilePages.DataBind();
           // popup.Show();
            Session["FilePages"] = Ds;

            string strPopup = "<script language='javascript' ID='script1'>"
            + "window.open('LaunchFilePages.aspx?NL_ID=" + HttpUtility.UrlEncode(NL_ID.Text) + "&NTLS_ID=" + HttpUtility.UrlEncode(NTLS_ID.Text)
            + "&Task_ID=" + HttpUtility.UrlEncode(Task_ID.Text) + "&Soft_ID=" + HttpUtility.UrlEncode(Soft_ID.Text) + "&Lang_ID=" + HttpUtility.UrlEncode(Lang_ID.Text) + "&File=" + HttpUtility.UrlEncode(File.ToString()) + "&FileConv=" + HttpUtility.UrlEncode(DropNewNameConv.SelectedValue)
            + "','new window', 'top=90, left=200, width=950, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
            + "</script>";
            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
    }
    protected void Save(object sender, EventArgs e)
    {
        nonLa.UpdateFilePagesStatusLP(NL_ID.Text, NTLS_ID.Text);
        foreach (GridViewRow grs in gv_FilePages.Rows)
        {
            HiddenField sJobID = (HiddenField)grs.FindControl("hid_LP_ID");
            HiddenField hid_NTLS_ID = (HiddenField)grs.FindControl("hid_NTLS_ID");
            HiddenField hid_Task_ID = (HiddenField)grs.FindControl("hid_Task_ID");
            HiddenField hid_Lang_ID = (HiddenField)grs.FindControl("hid_Lang_ID");
            HiddenField hid_Soft_ID = (HiddenField)grs.FindControl("hid_Soft_ID");
            TextBox FileName = (TextBox)grs.FindControl("txt_Name");
            TextBox Pages = (TextBox)grs.FindControl("txt_Pages");
            Label FileID = (Label)grs.FindControl("lbl_File");
            string inproc = "spLPInsertFilePages";
            string[,] pname ={
                                {"@LP_ID",sJobID.Value },{"@NTLS_ID",hid_NTLS_ID.Value},
                                {"@Task_ID",hid_Task_ID.Value},{"@Lang_ID",hid_Lang_ID.Value},
                                {"@Soft_ID",hid_Soft_ID.Value},{"@Files_Name",FileName.Text},
                                {"@Pages",Pages.Text},{"@Files_ID",FileID.Text},{"@ISExists","Output"}
                             };
            int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
            if (val == 1)
                lblResult.Text = "Inserted Successfully";
            else
                lblResult.Text = "Inserted Failed!.";
        }
        nonLa.UpdateLPFile_Count(NTLS_ID.Text, NL_ID.Text, txtFiles.Text);
        nonLa.DeleteLPFilePagesStatus(NL_ID.Text, NTLS_ID.Text);
        DataSet tn = new DataSet();
        tn = nonLa.getJobTrackingLP(hfP_ID.Value);
        if (tn != null)
        {
            gv_imagesNew.DataSource = tn;
            gv_imagesNew.DataBind();
        }
        popup.Hide();
        
        //lnkNewFileInfo_Click(sender, e);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        int count = 0;
        if (fileBrowse.HasFile)
        {
            string connectionString = "";
            string fileName = Path.GetFileName(fileBrowse.PostedFile.FileName);
            string fileExtension = Path.GetExtension(fileBrowse.PostedFile.FileName);
            string fileLocation = @"\\192.9.201.222\Mail\LaunchFiles\" + Path.GetFileNameWithoutExtension(fileName) + Convert.ToString(DateTime.Now).Replace("/", "_").Replace(":", "_") + "." + fileExtension;
            fileBrowse.SaveAs(fileLocation);
            if (fileExtension == ".xls")
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=NO\"";
            }
            else if (fileExtension == ".xlsx")
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }

            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand cmds = new OleDbCommand();
            cmds.CommandType = System.Data.CommandType.Text;
            cmds.Connection = con;
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmds);
            DataTable dtExcelRecords = new DataTable();
            con.Open();
            DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string getExcelSheetName1 = string.Empty;
            getExcelSheetName1 = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
            cmds.CommandText = "SELECT * FROM [" + getExcelSheetName1 + "]";
            dAdapter.SelectCommand = cmds;
            dAdapter.Fill(dtExcelRecords);
            for (int i = 0; i < dtExcelRecords.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToInt32(txtFiles.Text) >= Convert.ToInt32(dtExcelRecords.Rows[i][0]))
                    {
                        scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString);
                        scon.Open();
                        SqlCommand cmd = new SqlCommand("spLPUploadFilePages", scon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Files_ID", SqlDbType.Int).Value = dtExcelRecords.Rows[i][0];
                        cmd.Parameters.Add("@Files_Name", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][1];
                        cmd.Parameters.Add("@Pages", SqlDbType.Int).Value = dtExcelRecords.Rows[i][2];
                        cmd.Parameters.Add("@LP_ID", SqlDbType.Int).Value = NL_ID.Text;
                        cmd.Parameters.Add("@NTLS_ID", SqlDbType.Int).Value = NTLS_ID.Text;
                        cmd.Parameters.Add("@Task_ID", SqlDbType.Int).Value = Task_ID.Text;
                        cmd.Parameters.Add("@Lang_ID", SqlDbType.Int).Value = Lang_ID.Text;
                        cmd.Parameters.Add("@Soft_ID", SqlDbType.Int).Value = Soft_ID.Text;
                        count += cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
                finally
                {
                    scon.Close();
                }
            }
        }
        DataSet ds = new DataSet();
        ds = nonLa.spGetUploadFilePages(Task_ID.Text,Lang_ID.Text,Soft_ID.Text,NL_ID.Text,NTLS_ID.Text);
        gv_FilePages.DataSource = ds;
        gv_FilePages.DataBind();
        popup.Show();
    }
    protected void gv_FilePages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = (DataSet)Session["FilePages"];
        
        SaveCheckedValues();
        gv_FilePages.PageIndex = e.NewPageIndex;
        gv_FilePages.DataSource = ds;
        gv_FilePages.DataBind();
        PopulateCheckedValues();
        popup.Show();
    }
    private void SaveCheckedValues()
    {
        DataTable userdetails = new DataTable();
        userdetails.Columns.Add("Files_ID");
        userdetails.Columns.Add("LP_ID");
        userdetails.Columns.Add("NTLS_ID");
        userdetails.Columns.Add("TaskName");
        userdetails.Columns.Add("Task_ID");
        userdetails.Columns.Add("Lang_name");
        userdetails.Columns.Add("Lang_ID");
        userdetails.Columns.Add("Soft_Name");
        userdetails.Columns.Add("Soft_ID");
        userdetails.Columns.Add("Files_name");
        userdetails.Columns.Add("Pages");

        foreach (GridViewRow gvrow in gv_FilePages.Rows)
        {
            string Files_ID = ((Label)gvrow.FindControl("lbl_File")).Text;
            string LP_ID = ((HiddenField)gvrow.FindControl("hid_LP_ID")).Value;
            string NTLS_ID = ((HiddenField)gvrow.FindControl("hid_NTLS_ID")).Value;
            string TaskName = ((Label)gvrow.FindControl("lblTaskName")).Text;
            string Task_ID = ((HiddenField)gvrow.FindControl("hid_Task_ID")).Value;
            string Lang_name = ((Label)gvrow.FindControl("lblLang_Name")).Text;
            string Lang_ID = ((HiddenField)gvrow.FindControl("hid_Lang_ID")).Value;
            string Soft_Name = ((Label)gvrow.FindControl("lblgvSoft_Name")).Text;
            string Soft_ID = ((HiddenField)gvrow.FindControl("hid_Soft_ID")).Value;
            string Files_name = ((TextBox)gvrow.FindControl("txt_Name")).Text;
            string Pages = ((TextBox)gvrow.FindControl("txt_Pages")).Text;

            userdetails.Rows.Add(Files_ID, LP_ID, NTLS_ID, TaskName, Task_ID, Lang_name, Lang_ID, Soft_Name, Soft_ID, Files_name, Pages);
        }
        if (userdetails != null && userdetails.Rows.Count > 0)
            Session["Files"] = userdetails;
    }
    private void PopulateCheckedValues()
    {
        DataTable userdetails = (DataTable)Session["Files"];
        if (userdetails != null && userdetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gv_FilePages.Rows)
            {
                string Files_ID = ((Label)gvrow.FindControl("lbl_File")).Text;
                DataRow[] matches = userdetails.Select("Files_ID like '%" + Files_ID + "%'");
                foreach (DataRow row in matches)
                {
                    TextBox Name = (TextBox)gvrow.FindControl("txt_Name");
                    Name.Text = row["Files_name"].ToString();
                    TextBox Pages = (TextBox)gvrow.FindControl("txt_Pages");
                    Pages.Text = row["Pages"].ToString();
                }
            }
        }
    }
    protected void ListNewComplexReason_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in ListNewComplexReason.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxNewusagefonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxNewusagefonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxNewFonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxNewFonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxNewMissFonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxNewMissFonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxNewUFonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxNewUFonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxNewTFonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxNewTFonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxNewMFonts_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxNewMFonts.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxNewlang_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxNewlang.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void lboxNewlangused_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in lboxNewlangused.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
    protected void DropNewcomplex_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value;
        else
            id = "";
        //if (id.ToString() != "")
        //{
        //    NewComplexReason(id.ToString(), DropNewcomplex.SelectedValue);
        //}
        ComplexReasonNew(DropNewcomplex.SelectedValue);
    }
    protected void lboxNewdelivryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value.Trim();
        else
            id = "";
        string type = "";
        for (int j = 0; j < lboxNewdelivryType.Items.Count; j++)
        {
            if (lboxNewdelivryType.Items[j].Selected == true)
            {
                if (type == "")
                    type = lboxNewdelivryType.Items[j].Value;
                else
                    type = type + ',' + lboxNewdelivryType.Items[j].Value;
            }
        }
        if (type == "1" || type == "1,2" || type == "1,3" || type == "1,2,3")
        {
            lblNewSoft.Visible = false;
            gv_soft1New.Visible = false;
            DataSet tn = new DataSet();
            tn = nonLa.getTaskLangDetailsLP(id.ToString());
            gv_soft1New.DataSource = tn;
            gv_soft1New.DataBind();
        }
        else
        {
            lblNewSoft.Visible = false;
            gv_soft1New.Visible = false;
        }
    }
    protected void lboxSoftNew_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnNewQueries_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
        {
            if (hfP_ID.Value != "")
            {
                if (txtNewQuery.Text != "")
                {
                    nonLa.insertQueries(hfP_ID.Value, txtNewQuery.Text, txtNewQueryans.Text);
                    DataSet ds = new DataSet();
                    ds = nonLa.GetQueries(hfP_ID.Value);
                    gv_QueriesNew.DataSource = ds;
                    gv_QueriesNew.DataBind();
                    txtNewQuery.Text = "";
                    txtNewQueryans.Text = "";
                    msg = "Queries Added..";
                }
                else
                    msg = "Please enter Queries..";
            }
            else
            {
                msg = "Select Project Details!..";
            }
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
    protected void btnNewUFontsadd_Click(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value.Trim();
        else
            id = "";
        string fontitem6 = "";
        for (int j = 0; j < lboxNewusagefonts.Items.Count; j++)
        {
            if (lboxNewusagefonts.Items[j].Selected == true)
            {
                if (fontitem6 == "")
                    fontitem6 = lboxNewusagefonts.Items[j].Value;
                else
                    fontitem6 = fontitem6 + ',' + lboxNewusagefonts.Items[j].Value;
            }
        }
        DataSet au1 = new DataSet();
        DataSet au2 = new DataSet();
        au1 = nonLa.FontsList(id.ToString(), fontitem6);
        if (au1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < au1.Tables[0].Rows.Count; i++)
            {
                string fonts = au1.Tables[0].Rows[i]["Fonts"].ToString();

                if (txtNewOtherUsageFonts.Text != "")
                {
                    DataSet uft = new DataSet();
                    DataSet ds2 = new DataSet();
                    uft = nonLa.FontsList(id.ToString(), txtNewOtherUsageFonts.Text);
                    if (uft.Tables[0].Rows.Count > 0)
                    {
                        for (int x = 0; x < uft.Tables[0].Rows.Count; x++)
                        {
                            ds2 = nonLa.Chkmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (ds2 == null)
                            {
                                nonLa.insertmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                            au2 = nonLa.Chkusagefonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (au2 == null)
                            {
                                nonLa.insertusagefonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                        }
                    }
                }
                else
                {
                    au2 = nonLa.Chkusagefonts(id.ToString(), fonts);
                    if (au2 == null)
                    {
                        nonLa.insertusagefonts(id.ToString(), fonts);
                    }
                }
            }
        }
        if (txtNewOtherUsageFonts.Text != "" && fontitem6.ToString() == "")
        {
            DataSet uft = new DataSet();
            DataSet ds2 = new DataSet();
            uft = nonLa.FontsList(id.ToString(), txtNewOtherUsageFonts.Text);
            if (uft.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < uft.Tables[0].Rows.Count; x++)
                {
                    ds2 = nonLa.Chkmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (ds2 == null)
                    {
                        nonLa.insertmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                    au2 = nonLa.Chkusagefonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (au2 == null)
                    {
                        nonLa.insertusagefonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                }
            }
        }
        DataSet uf = new DataSet();
        uf = nonLa.GetUsageFonts(id);
        lboxNewUFonts.DataTextField = "Fonts";
        lboxNewUFonts.DataValueField = "Fonts";
        lboxNewUFonts.DataSource = uf;
        lboxNewUFonts.DataBind();
        DataSet Dst11 = new DataSet();
        Dst11 = nonLa.GetMissFonts();
        lboxNewusagefonts.DataTextField = "Fonts";
        lboxNewusagefonts.DataValueField = "Fonts";
        lboxNewusagefonts.DataSource = Dst11;
        lboxNewusagefonts.DataBind();
    }
    protected void btnNewUFontsRemove_Click(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value.Trim();
        else
            id = "";
        string usedFonts = "";
        for (int j = 0; j < lboxNewUFonts.Items.Count; j++)
        {
            if (lboxNewUFonts.Items[j].Selected == true)
            {
                if (usedFonts == "")
                    usedFonts = lboxNewUFonts.Items[j].Value;
                else
                    usedFonts = usedFonts + ',' + lboxNewUFonts.Items[j].Value;
            }
        }
        DataSet du1 = new DataSet();
        du1 = nonLa.FontsList(id.ToString(), usedFonts);
        if (du1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < du1.Tables[0].Rows.Count; i++)
            {
                string fonts = du1.Tables[0].Rows[i]["Fonts"].ToString();
                nonLa.deleteusagefonts(id.ToString(), fonts);
            }
        }
        lboxNewUFonts.Items.Clear();
        DataSet uf1 = new DataSet();
        uf1 = nonLa.GetUsageFonts(id);
        lboxNewUFonts.DataTextField = "Fonts";
        lboxNewUFonts.DataValueField = "Fonts";
        lboxNewUFonts.DataSource = uf1;
        lboxNewUFonts.DataBind();
    }
    protected void btnNewfontsadd_Click(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value.Trim();
        else
            id = "";
        string fontitem6 = "";
        for (int j = 0; j < lboxNewFonts.Items.Count; j++)
        {
            if (lboxNewFonts.Items[j].Selected == true)
            {
                if (fontitem6 == "")
                    fontitem6 = lboxNewFonts.Items[j].Value;
                else
                    fontitem6 = fontitem6 + ',' + lboxNewFonts.Items[j].Value;
            }
        }
        DataSet au1 = new DataSet();
        DataSet au2 = new DataSet();
        au1 = nonLa.FontsList(id.ToString(), fontitem6);
        if (au1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < au1.Tables[0].Rows.Count; i++)
            {
                string fonts = au1.Tables[0].Rows[i]["Fonts"].ToString();

                if (txtNewFonts.Text != "")
                {
                    DataSet uft = new DataSet();
                    DataSet ds2 = new DataSet();
                    uft = nonLa.FontsList(id.ToString(), txtNewFonts.Text);
                    if (uft.Tables[0].Rows.Count > 0)
                    {
                        for (int x = 0; x < uft.Tables[0].Rows.Count; x++)
                        {
                            ds2 = nonLa.Chkmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (ds2 == null)
                            {
                                nonLa.insertmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                            au2 = nonLa.Chkfonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (au2 == null)
                            {
                                nonLa.insertfonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                        }
                    }
                }
                else
                {
                    au2 = nonLa.Chkfonts(id.ToString(), fonts);
                    if (au2 == null)
                    {
                        nonLa.insertfonts(id.ToString(), fonts);
                    }
                }
            }
        }
        if (txtNewFonts.Text != "" && fontitem6.ToString() == "")
        {
            DataSet uft = new DataSet();
            DataSet ds2 = new DataSet();
            uft = nonLa.FontsList(id.ToString(), txtNewFonts.Text);
            if (uft.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < uft.Tables[0].Rows.Count; x++)
                {
                    ds2 = nonLa.Chkmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (ds2 == null)
                    {
                        nonLa.insertmissfonts(uft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                    au2 = nonLa.Chkfonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (au2 == null)
                    {
                        nonLa.insertfonts(id.ToString(), uft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                }
            }
        }
        DataSet uf = new DataSet();
        uf = nonLa.GetFonts(id);
        lboxNewTFonts.DataTextField = "Fonts";
        lboxNewTFonts.DataValueField = "Fonts";
        lboxNewTFonts.DataSource = uf;
        lboxNewTFonts.DataBind();
        DataSet Dst11 = new DataSet();
        Dst11 = nonLa.GetMissFonts();
        lboxNewFonts.DataTextField = "Fonts";
        lboxNewFonts.DataValueField = "Fonts";
        lboxNewFonts.DataSource = Dst11;
        lboxNewFonts.DataBind();
    }
    protected void btnNewfontsdel_Click(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value.Trim();
        else
            id = "";
        string TFont = "";
        for (int j = 0; j < lboxNewTFonts.Items.Count; j++)
        {
            if (lboxNewTFonts.Items[j].Selected == true)
            {
                if (TFont == "")
                    TFont = lboxNewTFonts.Items[j].Value;
                else
                    TFont = TFont + ',' + lboxNewTFonts.Items[j].Value;
            }
        }
        DataSet dt1 = new DataSet();
        dt1 = nonLa.FontsList(id.ToString(), TFont);
        if (dt1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Tables[0].Rows.Count; i++)
            {
                string f = dt1.Tables[0].Rows[i]["Fonts"].ToString();
                nonLa.deletefonts(id.ToString(), f);
            }
        }
        lboxNewTFonts.Items.Clear();
        DataSet tf1 = new DataSet();
        tf1 = nonLa.GetFonts(id);
        lboxNewTFonts.DataTextField = "Fonts";
        lboxNewTFonts.DataValueField = "Fonts";
        lboxNewTFonts.DataSource = tf1;
        lboxNewTFonts.DataBind();
    }
    protected void btnNewmissfontsadd_Click(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value.Trim();
        else
            id = "";
        string fontitem11 = "";
        for (int j = 0; j < lboxNewMissFonts.Items.Count; j++)
        {
            if (lboxNewMissFonts.Items[j].Selected == true)
            {
                if (fontitem11 == "")
                    fontitem11 = lboxNewMissFonts.Items[j].Value;
                else
                    fontitem11 = fontitem11 + ',' + lboxNewMissFonts.Items[j].Value;
            }
        }
        DataSet am1 = new DataSet();
        DataSet am2 = new DataSet();
        am1 = nonLa.FontsList(id.ToString(), fontitem11);
        if (am1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < am1.Tables[0].Rows.Count; i++)
            {
                string Mfonts = am1.Tables[0].Rows[i]["Fonts"].ToString();

                if (txtNewMissFonts.Text != "")
                {
                    DataSet mft = new DataSet();
                    DataSet dsm = new DataSet();
                    mft = nonLa.FontsList(id.ToString(), txtNewMissFonts.Text);
                    if (mft.Tables[0].Rows.Count > 0)
                    {
                        for (int x = 0; x < mft.Tables[0].Rows.Count; x++)
                        {
                            dsm = nonLa.Chkmissfonts(mft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (dsm == null)
                            {
                                nonLa.insertmissfonts(mft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                            am2 = nonLa.Chkmfonts(id.ToString(), mft.Tables[0].Rows[x]["Fonts"].ToString());
                            if (am2 == null)
                            {
                                nonLa.insertmfonts(id.ToString(), mft.Tables[0].Rows[x]["Fonts"].ToString());
                            }
                        }
                    }
                }
                else
                {
                    am2 = nonLa.Chkmfonts(id.ToString(), Mfonts);
                    if (am2 == null)
                    {
                        nonLa.insertmfonts(id.ToString(), Mfonts);
                    }
                }
            }
        }
        if (txtNewMissFonts.Text != "" && fontitem11.ToString() == "")
        {
            DataSet mft = new DataSet();
            DataSet dsm = new DataSet();
            mft = nonLa.FontsList(id.ToString(), txtNewMissFonts.Text);
            if (mft.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < mft.Tables[0].Rows.Count; x++)
                {
                    dsm = nonLa.Chkmissfonts(mft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (dsm == null)
                    {
                        nonLa.insertmissfonts(mft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                    am2 = nonLa.Chkmfonts(id.ToString(), mft.Tables[0].Rows[x]["Fonts"].ToString());
                    if (am2 == null)
                    {
                        nonLa.insertmfonts(id.ToString(), mft.Tables[0].Rows[x]["Fonts"].ToString());
                    }
                }
            }
        }
        DataSet mf = new DataSet();
        mf = nonLa.GetMFonts(id);
        lboxNewMFonts.DataTextField = "Fonts";
        lboxNewMFonts.DataValueField = "Fonts";
        lboxNewMFonts.DataSource = mf;
        lboxNewMFonts.DataBind();
        DataSet Dst13 = new DataSet();
        Dst13 = nonLa.GetMissFonts();
        lboxNewMissFonts.DataTextField = "Fonts";
        lboxNewMissFonts.DataValueField = "Fonts";
        lboxNewMissFonts.DataSource = Dst13;
        lboxNewMissFonts.DataBind();
    }
    protected void btnNewmissfontsdel_Click(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value.Trim();
        else
            id = "";
        string MFont = "";
        for (int j = 0; j < lboxNewMFonts.Items.Count; j++)
        {
            if (lboxNewMFonts.Items[j].Selected == true)
            {
                if (MFont == "")
                    MFont = lboxNewMFonts.Items[j].Value;
                else
                    MFont = MFont + ',' + lboxNewMFonts.Items[j].Value;
            }
        }
        DataSet dm1 = new DataSet();
        dm1 = nonLa.FontsList(id.ToString(), MFont);
        if (dm1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dm1.Tables[0].Rows.Count; i++)
            {
                nonLa.deletemfonts(id.ToString(), dm1.Tables[0].Rows[i]["Fonts"].ToString());
            }
        }
        DataSet Dsm = new DataSet();
        Dsm = nonLa.GetMissFonts();
        lboxNewMissFonts.DataTextField = "Fonts";
        lboxNewMissFonts.DataValueField = "Fonts";
        lboxNewMissFonts.DataSource = Dsm;
        lboxNewMissFonts.DataBind();
        lboxNewMFonts.Items.Clear();
        DataSet mf1 = new DataSet();
        mf1 = nonLa.GetMFonts(id.ToString());
        lboxNewMFonts.DataTextField = "Fonts";
        lboxNewMFonts.DataValueField = "Fonts";
        lboxNewMFonts.DataSource = mf1;
        lboxNewMFonts.DataBind();
    }
    protected void btnNewSave_Click(object sender, EventArgs e)
    {
        string msg = "";
        ArrayList al = new ArrayList();
        Hashtable val1 = null;
        ArrayList al1 = new ArrayList();
        Hashtable val2 = null;
        try
        {
             
            string id;
            if (hfP_ID.Value != "")
                id = hfP_ID.Value.Trim();
            else
                id = "";
            if (id != "")
            {
                if (validateScreenLPFileinfo())
                {
                    int ytr;
                    if (CheckNewYTR.Checked)
                        ytr = 1;
                    else
                        ytr = 0;
                    string tdate, sdate;

                    if (txtNewsource.Text != "")
                        sdate = DateTime.Parse(txtNewsource.Text.Trim()).ToString("MM/dd/yyyy");
                    else
                        sdate = "";
                    if (txtNewtarget.Text != "")
                        tdate = DateTime.Parse(txtNewtarget.Text.Trim()).ToString("MM/dd/yyyy");
                    else
                        tdate = "";

                    string path = string.Empty;
                    string filename = MailNewUpload.FileName;
                    string ext = Path.GetExtension(MailNewUpload.PostedFile.FileName);
                    if (filename == "" && lblNewMail.Text != "")
                        path = lblNewMail.Text;
                    else if (filename == "" && lblNewMail.Text == "")
                        path = lblNewMail.Text;
                    else
                    {
                        filename = MailNewUpload.FileName;
                        if (!Directory.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + DateTime.Now.ToString("MMMM yyyy")))
                            Directory.CreateDirectory(ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + DateTime.Now.ToString("MMMM yyyy"));
                        path = ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + DateTime.Now.ToString("MMMM yyyy") + "\\" + Regex.Replace(hfP_Name.Value.ToString().Trim(), "[^a-zA-Z0-9_]+", " ").Trim();
                        System.IO.File.Delete(path + ".doc");
                        System.IO.File.Delete(path + ".docx");
                        MailNewUpload.PostedFile.SaveAs(path + ext.ToString());
                        lblNewMail.Text = path + ext.ToString();
                    }
                    string ComplexReason = "";
                    for (int j = 0; j < ListNewComplexReason.Items.Count; j++)
                    {
                        if (ListNewComplexReason.Items[j].Selected == true)
                        {
                            if (ComplexReason == "")
                                ComplexReason = ListNewComplexReason.Items[j].Value;
                            else
                                ComplexReason = ComplexReason + ',' + ListNewComplexReason.Items[j].Value;
                        }
                    }
                    string deliverytype = "";
                    for (int j = 0; j < lboxNewdelivryType.Items.Count; j++)
                    {
                        if (lboxNewdelivryType.Items[j].Selected == true)
                        {
                            if (deliverytype == "")
                                deliverytype = lboxNewdelivryType.Items[j].Value;
                            else
                                deliverytype = deliverytype + '-' + lboxNewdelivryType.Items[j].Value;
                        }
                    }

                    string inproc = "SPUpdateFileInfo";
                    string[,] pname =
                {
                    {"@LP_ID",id},{"@SOURCEDATE",sdate},{"@TARGETDATE",tdate},{"@YTRYN",ytr.ToString()},
                    {"@OTHER_FONTS",txtNewFonts.Text},{"@OTHER_MISS_FONTS",txtNewMissFonts.Text},{"@OTHERUSAGE_FONTS",txtNewOtherUsageFonts.Text},
                    {"@MISS_FIG_LINK",txtNewfiglinks.Text},{"@TABLES",DropNewnooftables.Text},{"@PROOF",txtNewproof.Text},
                    {"@PRESS_PRINT",txtNewpress.Text},{"@Page_Size",txtNewpagesize.Text},
                    {"@COMPLEX_LEVEL",DropNewcomplex.SelectedValue},{"@REASON",ComplexReason},{"@MailDetails",path},
                    {"@FILENAME_CONV",DropNewNameConv.SelectedValue},{"@bleed",txtNewBleed.Text},
                    {"@REMARKS",txtNewSplIns.Text},{"@IsExist","Output"},{"@CREATEDBY_FILE",Session["employeeid"].ToString()},
                    {"@DELIVERYTYPE",deliverytype.Replace("-",",")}
                };
                    int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);


                    string langproc = "spAdd_DeliveryType";
                    string[,] langname ={
                    {"@LP_ID",id},{"@Delivery_Type",deliverytype}};
                    int lang12 = this.oNewLa.ExcSP(langproc, langname, CommandType.StoredProcedure);

                    foreach (GridViewRow grw in gv_imagesNew.Rows)
                    {
                        val2 = new Hashtable();
                        val2.Add("FP_ID", ((HiddenField)grw.FindControl("hid_FP_ID")).Value.Trim().ToString());
                        val2.Add("Editable", ((TextBox)grw.FindControl("txt_edit")).Text.Trim().ToString());
                        val2.Add("Scanned", ((TextBox)grw.FindControl("txt_scan")).Text.Trim().ToString());
                        val2.Add("Non_Local", ((TextBox)grw.FindControl("txt_nonlocal")).Text.Trim().ToString());
                        val2.Add("Local", ((TextBox)grw.FindControl("txt_images")).Text.Trim().ToString());
                        al1.Add(val2);
                    }
                    nonLa.Update_ImageLP(al1);

                    //inobj.UpdateLaunchQuote(id.ToString(), Convert.ToInt16(x.ToString()));
                    //inobj.getQuoteValue(id);
                    //inobj.UpdateFonts(id);

                    if (txtNewFonts.Text != "")
                    {
                        DataSet ft = new DataSet();
                        DataSet ck = new DataSet();
                        ft = nonLa.FontsList(id.ToString(), txtNewFonts.Text);
                        if (ft.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ft.Tables[0].Rows.Count; i++)
                            {
                                string fonts = ft.Tables[0].Rows[i]["Fonts"].ToString();
                                ck = nonLa.Chkmissfonts(fonts);
                                if (ck == null)
                                {
                                    nonLa.insertmissfonts(fonts);
                                }
                            }
                        }
                    }
                    if (txtNewMissFonts.Text != "")
                    {
                        DataSet mft = new DataSet();
                        DataSet ds1 = new DataSet();
                        mft = nonLa.FontsList(id.ToString(), txtNewMissFonts.Text);
                        if (mft.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < mft.Tables[0].Rows.Count; i++)
                            {
                                ds1 = nonLa.Chkmissfonts(mft.Tables[0].Rows[i]["Fonts"].ToString());
                                if (ds1 == null)
                                {
                                    nonLa.insertmissfonts(mft.Tables[0].Rows[i]["Fonts"].ToString());
                                }
                            }
                        }
                    }
                    if (txtNewOtherUsageFonts.Text != "")
                    {
                        DataSet uft = new DataSet();
                        DataSet ds2 = new DataSet();
                        uft = nonLa.FontsList(id.ToString(), txtNewOtherUsageFonts.Text);
                        if (uft.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < uft.Tables[0].Rows.Count; i++)
                            {
                                ds2 = nonLa.Chkmissfonts(uft.Tables[0].Rows[i]["Fonts"].ToString());
                                if (ds2 == null)
                                {
                                    nonLa.insertmissfonts(uft.Tables[0].Rows[i]["Fonts"].ToString());
                                }
                            }
                        }
                    }
                    if (val == 1)
                    {
                        msg = "Updated Successfully";
                    }
                    else if (val == 0)
                        msg = "Job ID Already Exists";
                }
                else
                {
                    msg = "Select Project Details!..";
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            //throw ex;
        }
        finally
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
        }
    }
    protected void btnNewClearfile_Click(object sender, EventArgs e)
    {

    }
    public void ComplexReasonNew(string complex)
    {
        DataSet dsCost5 = nonLa.ComplexReasonNew(complex);
        ListNewComplexReason.DataSource = dsCost5;
        ListNewComplexReason.DataValueField = dsCost5.Tables[0].Columns[0].ToString();
        ListNewComplexReason.DataTextField = dsCost5.Tables[0].Columns[2].ToString();
        ListNewComplexReason.DataBind();
    }
    protected void lnkNewCostInfo_Click(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value;
        else
            id = "";

        if (id != "")
        {
            Launch la = new Launch();
            DataSet sds1 = new DataSet();
            sds1 = nonLa.getValues(id);
            if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
            {
                gv_pmoduleNew.DataSource = sds1.Tables[0];
                gv_pmoduleNew.DataBind();
                if (sds1.Tables[0].Rows[0]["Jobid"].ToString() != "")
                {
                    foreach (GridViewRow grw in gv_pmoduleNew.Rows)
                    {
                        TextBox Time = (TextBox)grw.FindControl("txt_time");
                        TextBox Hrate = (TextBox)grw.FindControl("txt_hrate");
                        TextBox Prate = (TextBox)grw.FindControl("txt_prate");
                        TextBox Hcost = (TextBox)grw.FindControl("txt_hourrate");
                        TextBox Pcost = (TextBox)grw.FindControl("txt_pagerate");
                        CheckBox HChk = (CheckBox)grw.FindControl("chk_hour");
                        CheckBox PChk = (CheckBox)grw.FindControl("chk_page");
                        TextBox Lang = (TextBox)grw.FindControl("txt_langcount");
                        TextBox Ptotal = (TextBox)grw.FindControl("txt_totalpage");

                        if (Session["employeeid"].ToString() == "2461" || Session["employeeid"].ToString() == "1335")
                        {
                            Time.Enabled = true;
                            Hrate.Enabled = true;
                            Prate.Enabled = true;
                            Hcost.Enabled = true;
                            Pcost.Enabled = true;
                            HChk.Enabled = true;
                            PChk.Enabled = true;
                            Lang.Enabled = true;
                            Ptotal.Enabled = true;
                            txtNewquoteremark.Enabled = true;
                            btnNewQuoteSave.Enabled = true;
                            btnNewClear.Enabled = true;
                        }
                        else
                        {
                            Time.Enabled = false;
                            Hrate.Enabled = false;
                            Prate.Enabled = false;
                            Hcost.Enabled = false;
                            Pcost.Enabled = false;
                            HChk.Enabled = false;
                            PChk.Enabled = false;
                            Lang.Enabled = false;
                            Ptotal.Enabled = false;
                            txtNewquoteremark.Enabled = true;
                            btnNewQuoteSave.Enabled = true;
                            btnNewClear.Enabled = true;
                        }
                    }
                }

                txtNewquoteremark.Text = sds1.Tables[0].Rows[0]["QUOTEREMARKS"].ToString();
                txtNewFinalCheck.Text = sds1.Tables[0].Rows[0]["FINALCHECK"].ToString();
            }
            DataSet ds = new DataSet();
            ds = nonLa.empname(id);
            txtNewProjectCo.Text = ds.Tables[0].Rows[0]["empname"].ToString();
        }
        this.showPanel(tabNewQuote);
    }
    protected void btnNewQuoteSave_Click(object sender, EventArgs e)
    {
        ArrayList al = new ArrayList();
        Hashtable val = null;
        
        try
        {
            bool chk;
            chk = true;
            foreach (GridViewRow grw in gv_pmoduleNew.Rows)
            {
                val = new Hashtable();
                val.Add("LP_ID", ((HiddenField)grw.FindControl("hid_LP_ID")).Value.Trim().ToString());
                val.Add("TASK_ID", ((HiddenField)grw.FindControl("hid_Task_ID")).Value.Trim().ToString());
                val.Add("TIME_TAKEN", ((TextBox)grw.FindControl("txt_time")).Text.Trim().ToString());
                val.Add("LANG_COUNT", ((TextBox)grw.FindControl("txt_langcount")).Text.Trim().ToString());
                val.Add("PAGES_COUNT", ((TextBox)grw.FindControl("txt_totalpage")).Text.Trim().ToString());
                val.Add("TOTAL_PAGES", ((TextBox)grw.FindControl("txt_totalpage")).Text.Trim().ToString());
                val.Add("HOUR_RATE", ((TextBox)grw.FindControl("txt_hourrate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("PAGE_RATE", ((TextBox)grw.FindControl("txt_pagerate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("P_RATE", ((TextBox)grw.FindControl("txt_prate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("H_RATE", ((TextBox)grw.FindControl("txt_hrate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("Soft_ID", ((HiddenField)grw.FindControl("hid_Soft_ID")).Value.Trim().ToString());
                CheckBox page = (CheckBox)grw.FindControl("chk_page"); //sender;
                bool status = page.Checked;
                int p;
                if (status == true)
                    p = 1;
                else
                    p = 0;
                val.Add("PAGEYN", p.ToString());
                CheckBox hour = (CheckBox)grw.FindControl("chk_hour"); //sender;
                bool status1 = hour.Checked;
                int h;
                if (status1 == true)
                    h = 1;
                else
                    h = 0;
                val.Add("HOURYN", h.ToString());
                al.Add(val);
                if (!page.Checked && !hour.Checked)
                {
                    chk = false;
                    break;
                }
            }
            if (chk == true)
            {
                string id;
                if (hfP_ID.Value != "")
                {
                    id = hfP_ID.Value;
                }
                else
                {
                    id = "";
                }
                string FinalCheck = "";
                //if (txtFinalCheck.Text == "")
                //    FinalCheck = Session["fname"].ToString() + ' ' + Session["sname"].ToString();
                //else
                FinalCheck = Session["fname"].ToString() + ' ' + Session["sname"].ToString();

                string inproc = "spUpdateQuote_LP";
                string[,] pname ={
                    {"@QUOTEREMARKS",txtNewquoteremark.Text},{"@FINALCHECK",FinalCheck.ToString()},
                    {"@PROJECT_CO",txtNewProjectCo.Text},{"@LP_ID",id},
                    {"@IsExist","Output"},{"@employee_id",Session["employeeid"].ToString()}};
                int val3 = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
                
                if (!nonLa.Update_LaunchQuote(al))
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Please Check Page or Hour based...');</script>");
            }
            lnkNewCostInfo_Click(sender,e);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {  }
    }
    protected void btnNewClear_Click(object sender, EventArgs e)
    {

    }
    protected void TimeTakenNew_TextChanged(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value;
        else
            id = "";
        foreach (GridViewRow grw in gv_pmoduleNew.Rows)
        {
            string time = ((TextBox)grw.FindControl("txt_time")).Text;
            string task = ((HiddenField)grw.FindControl("hid_Task_ID")).Value;
            string name = ((HiddenField)grw.FindControl("hid_LP_ID")).Value;
            string soft = ((HiddenField)grw.FindControl("hid_Soft_ID")).Value;
            nonLa.UpdateTimeTaken(name, task, time, soft);
        }
        DataSet sds1 = new DataSet();
        sds1 = nonLa.getValues(id);
        if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
        {
            gv_pmoduleNew.DataSource = sds1.Tables[0];
            gv_pmoduleNew.DataBind();
        }
        lnkNewCostInfo_Click(sender, e);
    }
    protected void HrsNew_TextChanged(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value;
        else
            id = "";
        foreach (GridViewRow grw in gv_pmoduleNew.Rows)
        {
            string hrs = ((TextBox)grw.FindControl("txt_hrate")).Text;
            string task = ((HiddenField)grw.FindControl("hid_Task_ID")).Value;
            string name = ((HiddenField)grw.FindControl("hid_LP_ID")).Value;
            string soft = ((HiddenField)grw.FindControl("hid_Soft_ID")).Value;
            nonLa.UpdateHrs(name, task, hrs, soft);
        }
        DataSet sds1 = new DataSet();
        sds1 = nonLa.getValues(id);
        if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
        {
            gv_pmoduleNew.DataSource = sds1.Tables[0];
            gv_pmoduleNew.DataBind();
        }
        lnkNewCostInfo_Click(sender, e);
    }
    protected void PageNew_TextChanged(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value;
        else
            id = "";
        foreach (GridViewRow grw in gv_pmoduleNew.Rows)
        {
            string page = ((TextBox)grw.FindControl("txt_prate")).Text;
            string task = ((HiddenField)grw.FindControl("hid_Task_ID")).Value;
            string name = ((HiddenField)grw.FindControl("hid_LP_ID")).Value;
            string soft = ((HiddenField)grw.FindControl("hid_Soft_ID")).Value;
            nonLa.UpdatePage(name, task, page, soft);
        }
        DataSet sds1 = new DataSet();
        sds1 = nonLa.getValues(id);
        if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
        {
            gv_pmoduleNew.DataSource = sds1.Tables[0];
            gv_pmoduleNew.DataBind();
        }
        lnkNewCostInfo_Click(sender, e);
    }
    protected void lnkJobTracking_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            DataSet ds = new DataSet();
            ds = nonLa.getJobTrackingLP(hfP_ID.Value);
            if (ds != null)
            {
                gvJobTrack.DataSource = ds;
                gvJobTrack.DataBind();
            }
        }
        this.showPanel(tabJobTracking);
    }
    protected void gvJobTrack_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField FP_ID = e.Row.FindControl("hid_FP_ID") as HiddenField;
            DataSet ds = new DataSet();
            ds = nonLa.GetTarFPStatus(FP_ID.Value);
            DropDownList status = e.Row.FindControl("dropDelStatus") as DropDownList;
            status.Items.Add(new ListItem("--Select--", "0"));
            status.Items.Add(new ListItem("P", "P"));
            status.Items.Add(new ListItem("C", "C"));
            status.Items.Add(new ListItem("WIP", "WIP"));
            status.Items.Add(new ListItem("Del", "Del"));
            status.Items[status.Items.IndexOf(status.Items.FindByValue(ds.Tables[0].Rows[0]["DelStatus"].ToString()))].Selected = true;

            if (ds.Tables[0].Rows[0]["DelStatus"].ToString() == "P")
                status.CssClass = "gridP";
            else if (ds.Tables[0].Rows[0]["DelStatus"].ToString() == "C")
                status.CssClass = "gridC";
            else if (ds.Tables[0].Rows[0]["DelStatus"].ToString() == "Del")
                status.CssClass = "gridDel";
            else if (ds.Tables[0].Rows[0]["DelStatus"].ToString() == "WIP")
                status.CssClass = "gridWIP";
        }
    }
    protected void OnDelivery(object sender, EventArgs e)
    {
        DataSet dsNL = new DataSet();
        dsNL = nonLa.GetAmends();
        //lboxDelSatge.DataSource = dsNL;
        //lboxDelSatge.DataTextField = dsNL.Tables[0].Columns[1].ToString();
        //lboxDelSatge.DataValueField = dsNL.Tables[0].Columns[0].ToString();
        //lboxDelSatge.DataBind();
        dsNL = nonLa.getJobDetailsByLPID(hfP_ID.Value);
        DataRow r = dsNL.Tables[0].Rows[0];
        DelJobID.Text = r["JOBID"].ToString();
        DelProName.Text = r["projectname"].ToString();
        DelNL_ID.Text = r["LP_ID"].ToString();
        dropDelZoneAll.SelectedValue = r["TIME_ZONEFROM"].ToString();
        DelLoc_ID.Text = r["LOCATION_ID"].ToString();
        txtdeldateAll.Text = "";
        txtDel_ISTAll.Text = "";
        lblResult2.Text = "";
        dropCurStage.SelectedValue = "0";
        txtdeldateAll.Enabled = true;
        img14.Visible = true;
        dropDelHrsAll.Enabled = true;
        dropDelMinsAll.Enabled = true;
        dropDelZoneAll.Enabled = true;
        DelPopUp.Show();
    }
    public void ClearNew()
    {
        NL_ID1.Text = "";
        txtJobid1.Text = "";
        txtProjectEditor1.Text = "";
        txtdueFromdate1.Text = "";
        chkYTC1.Checked = false;
        txtdueTodate1.Text = "";
        txtRecDate1.Text = "";
        txtdueFromdate1.Text = "";
        txtdueTodate1.Text = "";
        chkDueDate1.Checked = false;
        chkDueTime1.Checked = false;
        txtIndTo1.Text = "";
        txtIndFrom1.Text = "";
        lboxStage.ClearSelection();
        lboxtask.ClearSelection();
        lblResult1.Text = "";
        txtDelDate.Text = "";
        lblResult1.Text = "";
        ddlStatus.Items.Clear();
    }
    protected void Click(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            ClearNew();
            DataSet dsNL = new DataSet();
            dsNL = nonLa.GetAmends();
            lboxStage.DataSource = dsNL;
            lboxStage.DataTextField = dsNL.Tables[0].Columns[1].ToString();
            lboxStage.DataValueField = dsNL.Tables[0].Columns[0].ToString();
            lboxStage.DataBind();
            HiddenField sJobID = (HiddenField)row.Cells[0].FindControl("hid_LP_ID");
            HiddenField sFPID = (HiddenField)row.Cells[0].FindControl("hid_FP_ID");
            dsNL = nonLa.getJobTrackingByIDLP(sJobID.Value.ToString(), sFPID.Value.ToString());
            DataRow r = dsNL.Tables[0].Rows[0];
            txtFP_ID.Text = sFPID.Value.ToString();
            NL_ID1.Text = r["LP_ID"].ToString();
            txtJobid1.Text = r["JOBID"].ToString();
            txtProjectEditor1.Text = r["PROJECTEDITOR"].ToString();
            txtProjectTitle1.Text = r["projectname"].ToString();
            drpProjectcustomer1.Text = r["CUSTNAME"].ToString().Trim();
            DropLocation1.Text = r["LOCATION_NAME"].ToString();
            hid_Loc_ID.Value = r["LOCATION_ID"].ToString();
            getloc_timezoneNew(Convert.ToInt16(r["LOCATION_ID"].ToString()));
            if (r["YTCYN"].ToString() == "1") chkYTC1.Checked = true;
            if (r["DUE_DATEFROM"].ToString() == "")
                txtdueFromdate1.Text = "";
            else
                txtdueFromdate1.Text = DateTime.Parse(r["DUE_DATEFROM"].ToString()).ToShortDateString();
            if (r["DUE_DATETO"].ToString() == "")
                txtdueTodate1.Text = "";
            else
                txtdueTodate1.Text = DateTime.Parse(r["DUE_DATETO"].ToString()).ToShortDateString();
            if (r["Rec_Date"].ToString() == "")
                txtRecDate1.Text = "";
            else
                txtRecDate1.Text = DateTime.Parse(r["Rec_Date"].ToString()).ToShortDateString();

            if (r["DUEDATEYN"].ToString() == "1")
            {
                lblDueFrom1.Visible = true;
                txtdueFromdate1.Visible = true;
                lblDueTo1.Visible = true;
                txtdueTodate1.Visible = true;
                chkDueDate1.Checked = true;
                imgBD_dueFromdate1.Visible = true;
                imgBD_dueTodate1.Visible = true;
            }
            else
            {
                lblDueFrom1.Visible = false;
                txtdueFromdate1.Visible = true;
                lblDueTo1.Visible = false;
                txtdueTodate1.Visible = false;
                chkDueDate1.Checked = false;
                imgBD_dueTodate1.Visible = false;
            }
            if (r["DUETIMEYN"].ToString() == "1")
            {
                lblFrom1.Visible = true;
                lblTo1.Visible = true;
                chkDueTime1.Checked = true;
                DropDueTimeTo1.Visible = true;
                DropDueMinTo1.Visible = true;
                DropDueTimeZoneTo1.Visible = true;
                txtIndTo1.Visible = true;
                DropDueTimeFrom1.SelectedValue = r["DUE_HrsFROM"].ToString();
                DropDueMinFrom1.SelectedValue = r["DUE_MINFROM"].ToString();
                DropDueTimeZoneFrom1.SelectedValue = r["TIME_ZONEFROM"].ToString();
                DropDueTimeTo1.SelectedValue = r["DUE_HrsTO"].ToString();
                DropDueMinTo1.SelectedValue = r["DUE_MINTO"].ToString();
                DropDueTimeZoneTo1.SelectedValue = r["TIME_ZONETO"].ToString();
                DropDelTimeZone.SelectedValue = r["TIME_ZONETO"].ToString();
                txtIndFrom1.Text = r["DUETIMEFROM_IST"].ToString();
                txtIndTo1.Text = r["DUETIMETO_IST"].ToString();
                lblIndFrom1.Visible = true;
                lblIndTo1.Visible = true;
            }
            else
            {
                lblFrom1.Visible = false;
                lblTo1.Visible = false;
                chkDueTime1.Checked = false;
                DropDueTimeFrom1.SelectedValue = r["DUE_HrsFROM"].ToString();
                DropDueMinFrom1.SelectedValue = r["DUE_MINFROM"].ToString();
                DropDueTimeZoneFrom1.SelectedValue = r["TIME_ZONEFROM"].ToString();
                DropDueTimeTo1.SelectedValue = "00";
                DropDueMinTo1.SelectedValue = "00";
                DropDueTimeZoneTo1.SelectedValue = r["TIME_ZONEFROM"].ToString();
                DropDelTimeZone.SelectedValue = r["TIME_ZONEFROM"].ToString();
                txtIndFrom1.Text = r["DUETIMEFROM_IST"].ToString();
                txtIndTo1.Text = "";
                lblIndFrom1.Visible = false;
                lblIndTo1.Visible = false;
                txtIndTo1.Visible = false;
                DropDueTimeTo1.Visible = false;
                DropDueMinTo1.Visible = false;
                DropDueTimeZoneTo1.Visible = false;
            }
            lboxtask1.Text = r["TASKNAME"].ToString();
            string[] s = r["Amend_ID"].ToString().Split(',');
            foreach (string stage in s)
            {
                lboxStage.Items[lboxStage.Items.IndexOf(lboxStage.Items.FindByValue(stage))].Selected = true;
            }
            if (r["Despatch_Date"].ToString() == "")
            {
                txtDelDate.Text = "";
                txtDelIST.Text = "";
                DropDelHrs.SelectedValue = "00";
                DropDelMins.SelectedValue = "00";
            }
            else
            {
                txtDelDate.Text = DateTime.Parse(r["Despatch_Date"].ToString()).ToShortDateString();
                DropDelHrs.SelectedValue = r["DEL_HRS"].ToString();
                DropDelMins.SelectedValue = r["DEL_MINS"].ToString();
                DropDelTimeZone.SelectedValue = r["DEL_ZONE"].ToString();
                txtDelIST.Text = r["DEL_IST"].ToString();
            }
            if (r["LastAmend_ID"].ToString() == "52" && r["Despatch_Date"].ToString() != "")
            {
                btnSave1.Visible = false;
            }
            else
            {
                btnSave1.Visible = true;
            }

            ddlStatus.Items.Add(new ListItem("P", "P"));
            ddlStatus.Items.Add(new ListItem("C", "C"));
            ddlStatus.Items.Add(new ListItem("WIP", "WIP"));
            ddlStatus.Items.Add(new ListItem("Del", "Del"));
            ddlStatus.Items[ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(r["DelStatus"].ToString()))].Selected = true;

            if (r["DelStatus"].ToString() == "P")
                ddlStatus.CssClass = "gridP";
            else if (r["DelStatus"].ToString() == "C")
                ddlStatus.CssClass = "gridC";
            else if (r["DelStatus"].ToString() == "Del")
                ddlStatus.CssClass = "gridDel";
            else if (r["DelStatus"].ToString() == "WIP")
                ddlStatus.CssClass = "gridWIP";
            popup1.Show();
        }
    }
    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvJobTrack.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in gvJobTrack.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkDelStatus");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
    protected void dropCurStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropCurStage.SelectedValue == "1")
        {
            txtdeldateAll.Text = "";
            lblResult2.Text = "";
            txtdeldateAll.Enabled = false;
            img14.Visible = false;
            dropDelHrsAll.Enabled = false;
            dropDelMinsAll.Enabled = false;
            dropDelZoneAll.Enabled = false;
        }
        else if (dropCurStage.SelectedValue == "2")
        {
            txtdeldateAll.Text = "";
            lblResult2.Text = "";
            txtdeldateAll.Enabled = false;
            img14.Visible = false;
            dropDelHrsAll.Enabled = false;
            dropDelMinsAll.Enabled = false;
            dropDelZoneAll.Enabled = false;
        }
        else
        {
            txtdeldateAll.Text = "";
            lblResult2.Text = "";
            txtdeldateAll.Enabled = true;
            img14.Visible = true;
            dropDelHrsAll.Enabled = true;
            dropDelMinsAll.Enabled = true;
            dropDelZoneAll.Enabled = true;
        }
        DelPopUp.Show();
    }
    protected void dropDelHrsAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(DelLoc_ID.Text, dropDelHrsAll.SelectedValue, dropDelMinsAll.SelectedValue, dropDelZoneAll.SelectedValue);
        txtDel_ISTAll.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
    }
    protected void dropDelMinsAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(DelLoc_ID.Text, dropDelHrsAll.SelectedValue, dropDelMinsAll.SelectedValue, dropDelZoneAll.SelectedValue);
        txtDel_ISTAll.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
    }
    protected void dropDelZoneAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(DelLoc_ID.Text, dropDelHrsAll.SelectedValue, dropDelMinsAll.SelectedValue, dropDelZoneAll.SelectedValue);
        txtDel_ISTAll.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
    }
    protected void onDelSave(object sender, EventArgs e)
    {
        foreach (GridViewRow grs in gvJobTrack.Rows)
        {
            HiddenField sJobID = (HiddenField)grs.FindControl("hid_LP_ID");
            HiddenField hid_FP_ID = (HiddenField)grs.FindControl("hid_FP_ID");
            DropDownList DelStatus = (DropDownList)grs.FindControl("dropDelStatus");
            CheckBox chkDelStatus = (CheckBox)grs.FindControl("chkDelStatus");

            string delDate = "";
            if (txtdeldateAll.Text != "")
                delDate = DateTime.Parse(txtdeldateAll.Text.Trim()).ToString("MM/dd/yyyy");
            else
                delDate = "";

            int val = 0;
            if (chkDelStatus.Checked)
            {
                if (dropCurStage.SelectedValue == "1")
                {
                    string inproc = "spLPInsertNextJobHis";
                    string[,] pname ={
                                        {"@LP_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},
                                        {"@DelStatus",DelStatus.SelectedValue},{"@IsExist","Output"}
                                     };
                    val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
                }
                else if (dropCurStage.SelectedValue == "0")
                {
                    string inproc = "spLPUpdateDelJobHis";
                    string[,] pname ={
                                {"@LP_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},
                                {"@DEL_DATE",delDate.ToString()},{"@DEL_HRS",dropDelHrsAll.SelectedValue},
                                {"@DEL_MINS",dropDelMinsAll.SelectedValue},{"@DEL_ZONE",dropDelZoneAll.SelectedValue},
                                {"@DEL_IST",txtDel_ISTAll.Text},{"@IsExist","Output"},
                                {"@DelStatus",DelStatus.SelectedValue}
                             };
                    val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
                }
                else if (dropCurStage.SelectedValue == "2")
                {
                    string inproc = "spLPInsertNextFinalJobHis";
                    string[,] pname ={
                                        {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},
                                        {"@DelStatus",DelStatus.SelectedValue},{"@IsExist","Output"}
                                     };
                    val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
                }
                if (val == 1)
                    lblResult2.Text = "Inserted Successfully";
                else
                    lblResult2.Text = "Inserted Failed!.";
            }
        }
        if (hfP_ID.Value != "")
        {
            DataSet ds = new DataSet();
            ds = nonLa.getJobTrackingLP(hfP_ID.Value);
            if (ds != null)
            {
                gvJobTrack.DataSource = ds;
                gvJobTrack.DataBind();
            }
        }
        DelPopUp.Show();
    }
    protected void DropDueMin1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(hid_Loc_ID.Value, DropDueTimeFrom1.SelectedValue, DropDueMinFrom1.SelectedValue, DropDueTimeZoneFrom1.SelectedValue);
        txtIndFrom1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDueTime1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(hid_Loc_ID.Value, DropDueTimeFrom1.SelectedValue, DropDueMinFrom1.SelectedValue, DropDueTimeZoneFrom1.SelectedValue);
        txtIndFrom1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDueTimeZoneFrom1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(hid_Loc_ID.Value, DropDueTimeFrom1.SelectedValue, DropDueMinFrom1.SelectedValue, DropDueTimeZoneFrom1.SelectedValue);
        txtIndFrom1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDueTimeTo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(hid_Loc_ID.Value, DropDueTimeTo1.SelectedValue, DropDueMinTo1.SelectedValue, DropDueTimeZoneTo1.SelectedValue);
        txtIndTo1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDueMinTo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(hid_Loc_ID.Value, DropDueTimeTo1.SelectedValue, DropDueMinTo1.SelectedValue, DropDueTimeZoneTo1.SelectedValue);
        txtIndTo1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDueTimeZoneTo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(hid_Loc_ID.Value, DropDueTimeTo1.SelectedValue, DropDueMinTo1.SelectedValue, DropDueTimeZoneTo1.SelectedValue);
        txtIndTo1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDelHrs_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(hid_Loc_ID.Value, DropDelHrs.SelectedValue, DropDelMins.SelectedValue, DropDelTimeZone.SelectedValue);
        txtDelIST.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDelMins_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(hid_Loc_ID.Value, DropDelHrs.SelectedValue, DropDelMins.SelectedValue, DropDelTimeZone.SelectedValue);
        txtDelIST.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDelTimeZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormatNew(hid_Loc_ID.Value, DropDelHrs.SelectedValue, DropDelMins.SelectedValue, DropDelTimeZone.SelectedValue);
        txtDelIST.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void chkDueDate1_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDueDate1.Checked)
        {
            lblDueFrom1.Visible = true;
            txtdueFromdate1.Visible = true;
            lblDueTo1.Visible = true;
            txtdueTodate1.Visible = true;
            imgBD_dueTodate1.Visible = true;
        }
        else
        {
            lblDueFrom1.Visible = false;
            txtdueFromdate1.Visible = true;
            lblDueTo1.Visible = false;
            txtdueTodate1.Visible = false;
            imgBD_dueTodate1.Visible = false;
        }
        popup1.Show();
    }
    protected void Save1(object sender, EventArgs e)
    {
        int ytc;
        if (chkYTC1.Checked)
            ytc = 1;
        else
            ytc = 0;
        int duetime;
        if (chkDueTime1.Checked)
            duetime = 1;
        else
            duetime = 0;
        int dueDate;
        if (chkDueDate1.Checked)
            dueDate = 1;
        else
            dueDate = 0;
        string ddate = "";
        if (txtdueFromdate1.Text != "")
            ddate = DateTime.Parse(txtdueFromdate1.Text.Trim()).ToString("MM/dd/yyyy");
        else
            ddate = "";
        string dTodate = "";
        if (txtdueTodate1.Text != "")
            dTodate = DateTime.Parse(txtdueTodate1.Text.Trim()).ToString("MM/dd/yyyy");
        else
            dTodate = "";
        string recDate = "";
        if (txtRecDate1.Text != "")
            recDate = DateTime.Parse(txtRecDate1.Text.Trim()).ToString("MM/dd/yyyy");
        else
            recDate = "";
        string delDate = "";
        if (txtDelDate.Text != "")
            delDate = DateTime.Parse(txtDelDate.Text.Trim()).ToString("MM/dd/yyyy");
        else
            delDate = "";

        string Amends = "", AmendName = "", LastAmends = "";
        for (int j = 0; j < lboxStage.Items.Count; j++)
        {
            if (lboxStage.Items[j].Selected == true)
            {
                if (Amends == "")
                {
                    Amends = lboxStage.Items[j].Value;
                    AmendName = lboxStage.Items[j].Text;
                }
                else
                {
                    Amends = Amends + ',' + lboxStage.Items[j].Value;
                    AmendName = AmendName + ',' + lboxStage.Items[j].Text;
                }
                LastAmends = lboxStage.Items[j].Value;
            }
        }
        if (ddlStatus.SelectedValue == "Del")
        {
            if (delDate.ToString() != "")
            {
                string inproc = "spLPUpdateJobHis";
                string[,] pname ={
                            {"@YTCYN",ytc.ToString()},{"@DUETIMEYN",duetime.ToString()},
                            {"@DUE_DATEFROM",ddate},{"@DUE_DATETo",dTodate},
                            {"@Due_Timefrom",DropDueTimeFrom1.SelectedValue},{"@Due_MINFROM",DropDueMinFrom1.SelectedValue},
                            {"@TIME_ZoneFROM",DropDueTimeZoneFrom1.SelectedValue},
                            {"@Due_TimeTO",DropDueTimeTo1.SelectedValue},{"@Due_MINTO",DropDueMinTo1.SelectedValue},
                            {"@TIME_ZoneTO",DropDueTimeZoneTo1.SelectedValue},{"@MODIFIED_BY",""},
                            {"@DUETIMEFROM_IST",txtIndFrom1.Text},{"@DUETIMETO_IST",txtIndTo1.Text},
                            {"@DUEDateYN",dueDate.ToString()},{"@IsExist","OUTPUT"},
                            {"@RecDate",recDate.ToString()},{"@LP_ID",NL_ID1.Text},
                            {"@DEL_DATE",delDate.ToString()},{"@DEL_HRS",DropDelHrs.SelectedValue},
                            {"@DEL_MINS",DropDelMins.SelectedValue},{"@DEL_ZONE",DropDelTimeZone.SelectedValue},
                            {"@DEL_IST",txtDelIST.Text},{"@Amends_ID",Amends.ToString()},{"@Status",ddlStatus.SelectedValue},
                            {"@AmendName",AmendName.ToString()},{"@FP_ID",txtFP_ID.Text},{"@LastAmend",LastAmends.ToString()}
                         };
                int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);

                if (val == 1)
                    lblResult1.Text = "Inserted Successfully";
                else
                    lblResult1.Text = "Inserted Failed!.";
            }
            else
            {
                lblResult1.Text = "Please check Delivery Date!..";
            }
        }
        else
        {
            string inproc = "spLPUpdateJobHis";
            string[,] pname ={
                            {"@YTCYN",ytc.ToString()},{"@DUETIMEYN",duetime.ToString()},
                            {"@DUE_DATEFROM",ddate},{"@DUE_DATETo",dTodate},
                            {"@Due_Timefrom",DropDueTimeFrom1.SelectedValue},{"@Due_MINFROM",DropDueMinFrom1.SelectedValue},
                            {"@TIME_ZoneFROM",DropDueTimeZoneFrom1.SelectedValue},
                            {"@Due_TimeTO",DropDueTimeTo1.SelectedValue},{"@Due_MINTO",DropDueMinTo1.SelectedValue},
                            {"@TIME_ZoneTO",DropDueTimeZoneTo1.SelectedValue},{"@MODIFIED_BY",""},
                            {"@DUETIMEFROM_IST",txtIndFrom1.Text},{"@DUETIMETO_IST",txtIndTo1.Text},
                            {"@DUEDateYN",dueDate.ToString()},{"@IsExist","OUTPUT"},
                            {"@RecDate",recDate.ToString()},{"@LP_ID",NL_ID1.Text},
                            {"@DEL_DATE",delDate.ToString()},{"@DEL_HRS",DropDelHrs.SelectedValue},
                            {"@DEL_MINS",DropDelMins.SelectedValue},{"@DEL_ZONE",DropDelTimeZone.SelectedValue},
                            {"@DEL_IST",txtDelIST.Text},{"@Amends_ID",Amends.ToString()},{"@Status",ddlStatus.SelectedValue},
                            {"@AmendName",AmendName.ToString()},{"@FP_ID",txtFP_ID.Text},{"@LastAmend",LastAmends.ToString()}
                         };
            int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);

            if (val == 1)
                lblResult1.Text = "Inserted Successfully";
            else
                lblResult1.Text = "Inserted Failed!.";
        }
        if (hfP_ID.Value != "")
        {
            DataSet ds = new DataSet();
            ds = nonLa.getJobTrackingLP(hfP_ID.Value);
            if (ds != null)
            {
                gvJobTrack.DataSource = ds;
                gvJobTrack.DataBind();
            }
        }
        popup1.Hide();
    }
    protected void chkDueTime1_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDueTime1.Checked)
        {
            lblFrom1.Visible = true;
            lblTo1.Visible = true;
            DropDueTimeTo1.Visible = true;
            DropDueMinTo1.Visible = true;
            DropDueTimeZoneTo1.Visible = true;
            txtIndTo1.Visible = true;
            lblIndFrom1.Visible = true;
            lblIndTo1.Visible = true;
        }
        else
        {
            lblFrom1.Visible = false;
            lblTo1.Visible = false;
            DropDueTimeTo1.Visible = false;
            DropDueMinTo1.Visible = false;
            DropDueTimeZoneTo1.Visible = false;
            txtIndTo1.Visible = false;
            lblIndFrom1.Visible = false;
            lblIndTo1.Visible = false;
        }
        popup1.Show();
    }
    protected void btnLPFrom_Click(object sender, EventArgs e)
    {
        LPForm Lads = new LPForm();
        LPQuery Laq = new LPQuery();
        LPQuote LaQu = new LPQuote();
        LPLang LaLg = new LPLang();
        rep = new ReportDocument();
        CrystalReportViewer2.Visible = true;
        try
        {
            string id;
            if (hfP_ID.Value != "")
                id = hfP_ID.Value;
            else
                id = txtProjectTitle.Text;
            if (id != "")
            {
                Lads = nonLa.LP_LaunchForm("spGetLaunchForm", new string[,] { { "@LP_ID", id } });
                DataRow r = Lads.Tables[1].Rows[0];
                string PNAME = r["PROJECTNAME"].ToString();
                string PROJECTNAME = Regex.Replace(PNAME, @"[^0-9a-zA-Z]+", "_");
                if (r["TASK"].ToString().Trim() == "TE")
                {
                    Laq = nonLa.LP_LaunchQuires("spGetLPFormQueries", new string[,] { { "@LP_ID", id } });
                    LaQu = nonLa.LP_LaunchQuote("spGetLPFormQuote", new string[,] { { "@LP_ID", id } });
                    LaLg = nonLa.LP_LaunchLang("spGetLPFormLang", new string[,] { { "@LP_ID", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LPFormTE.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        CrystalReportViewer2.ReportSource = rep;
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + PROJECTNAME;
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
                else if (r["TASK"].ToString().Trim() == "DQA")
                {
                    Laq = nonLa.LP_LaunchQuires("spGetLPFormQueries", new string[,] { { "@LP_ID", id } });
                    LaQu = nonLa.LP_LaunchQuote("spGetLPFormQuote", new string[,] { { "@LP_ID", id } });
                    LaLg = nonLa.LP_LaunchLang("spGetLPFormLang", new string[,] { { "@LP_ID", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LPFormDQA.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        CrystalReportViewer2.ReportSource = rep;
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + PROJECTNAME;
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
                else if (r["TASK"].ToString().Trim() == "File Conversion")
                {
                    Laq = nonLa.LP_LaunchQuires("spGetLPFormQueries", new string[,] { { "@LP_ID", id } });
                    LaQu = nonLa.LP_LaunchQuote("spGetLPFormQuote", new string[,] { { "@LP_ID", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LPFormFC.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(Laq.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(LaQu.Tables[1]);
                        CrystalReportViewer2.ReportSource = rep;
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + PROJECTNAME;
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
                else
                {
                    Laq = nonLa.LP_LaunchQuires("spGetLPFormQueries", new string[,] { { "@LP_ID", id } });
                    LaQu = nonLa.LP_LaunchQuote("spGetLPFormQuote", new string[,] { { "@LP_ID", id } });
                    LaLg = nonLa.LP_LaunchLang("spGetLPFormLang", new string[,] { { "@LP_ID", id } });
                    if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                    {
                        rep = new ReportDocument();
                        rep.FileName = Server.MapPath("~/LaunchReport/LPForm.rpt");
                        rep.SetDatabaseLogon("sa", "masterkey");
                        rep.SetDataSource(Lads.Tables[1]);
                        subRep1 = rep.Subreports[0];
                        subRep1.SetDataSource(LaLg.Tables[1]);
                        subRep2 = rep.Subreports[1];
                        subRep2.SetDataSource(Laq.Tables[1]);
                        subRep3 = rep.Subreports[2];
                        subRep3.SetDataSource(LaQu.Tables[1]);
                        CrystalReportViewer2.ReportSource = rep;
                        string filename = "Launch_Meeting_Form_" + r["Jobid"].ToString() + '_' + PROJECTNAME;
                        rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    }
                }
            }
        }
        catch (Exception ex)
        { }
        finally
        {

        }
    }
    protected void btnLPQuote_Click(object sender, EventArgs e)
    {
        LPQuoteValue Lads = new LPQuoteValue();
        LPQuoteDesc LaQd = new LPQuoteDesc();
        rep = new ReportDocument();
        CrystalReportViewer1.Visible = true;
        try
        {
            string id;
            if (hfP_ID.Value != "")
                id = hfP_ID.Value;
            else
                id = "";
            if (id != "")
            {
                Lads = nonLa.LP_QuoteValue("spGetLPQuoteValue", new string[,] { { "@LP_ID", id } });
                LaQd = nonLa.LP_QuoteDesc("spGetLPQuoteDesc", new string[,] { { "@LP_ID", id } });
                DataRow r = Lads.Tables[1].Rows[0];
                string PNAME = r["PROJECTNAME"].ToString();
                string PROJECTNAME = Regex.Replace(PNAME, @"[^0-9a-zA-Z]+", "_");
                if (Lads != null && Lads.Tables[1].Rows.Count > 0)
                {
                    rep = new ReportDocument();

                    rep.FileName = Server.MapPath("~/LaunchReport/LPQuote.rpt");
                    rep.SetDatabaseLogon("sa", "masterkey");
                    rep.SetDataSource(Lads.Tables[1]);
                    subRep1 = rep.Subreports[0];
                    subRep1.SetDataSource(LaQd.Tables[1]);
                    CrystalReportViewer1.ReportSource = rep;
                    CrystalReportViewer1.DataBind();
                    string filename = "Quote_" + r["Jobid"].ToString() + '_' + PROJECTNAME;
                    rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
                    //Response.ContentType = "Application/pdf";

                }
            }
        }
        catch (Exception ex)
        { }
        finally
        {

        }
    }
    protected void DDMonthList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Month = Convert.ToInt16(DDMonthList.SelectedValue);
        int Year = Convert.ToInt32(DDYearList.SelectedValue);
        int Total=Month+Year*12;
        if(Total<24186)
        {
            DataSet dscust = la.getAllCustomers();
            drpCustomerSearch.DataSource = dscust;
            drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
            drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
            drpCustomerSearch.DataBind();
            drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
            miNewJobInfo.Visible = false;
            miNewFileInfo.Visible = false;
            miJobTracking.Visible = false;
            miNewCostDetails.Visible = false;
            miNewReportDetails.Visible = false;
            miLaunchDetails.Visible = true;
            miFileInfo.Visible = true;
            miCostDetails.Visible = true;
            miReportDetails.Visible = true;
            Button1.Visible = true;
            btnCustQuote.Visible = true;
            btnLPFrom.Visible = false;
            btnLPQuote.Visible = false;
            miLoggedEvent.Visible = false;
        }
        else
        {
            DataSet Ds = nonLa.getAllCustomers();
            drpCustomerSearch.DataSource = Ds;
            drpCustomerSearch.DataTextField = Ds.Tables[0].Columns[1].ToString();
            drpCustomerSearch.DataValueField = Ds.Tables[0].Columns[0].ToString();
            drpCustomerSearch.DataBind();
            drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
            miNewJobInfo.Visible = true;
            miNewFileInfo.Visible = true;
            miJobTracking.Visible = true;
            miNewCostDetails.Visible = true;
            miNewReportDetails.Visible = true;
            miLaunchDetails.Visible = false;
            miFileInfo.Visible = false;
            miCostDetails.Visible = false;
            miReportDetails.Visible = false;
            Button1.Visible = false;
            btnCustQuote.Visible = false;
            btnLPFrom.Visible = true;
            btnLPQuote.Visible = true;
            miLoggedEvent.Visible = true;
        }
        this.showPanel(tabGeneral);
    }
    protected void DDYearList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Month = Convert.ToInt16(DDMonthList.SelectedValue);
        int Year = Convert.ToInt32(DDYearList.SelectedValue);
        int Total = Month + Year * 12;
        if (Total < 24186)
        {
            DataSet dscust = la.getAllCustomers();
            drpCustomerSearch.DataSource = dscust;
            drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
            drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
            drpCustomerSearch.DataBind();
            drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
            miNewJobInfo.Visible = false;
            miNewFileInfo.Visible = false;
            miJobTracking.Visible = false;
            miNewCostDetails.Visible = false;
            miNewReportDetails.Visible = false;
            miLaunchDetails.Visible = true;
            miFileInfo.Visible = true;
            miCostDetails.Visible = true;
            miReportDetails.Visible = true;
            Button1.Visible = true;
            btnCustQuote.Visible = true;
            btnLPFrom.Visible = false;
            btnLPQuote.Visible = false;
        }
        else
        {
            DataSet Ds = nonLa.getAllCustomers();
            drpCustomerSearch.DataSource = Ds;
            drpCustomerSearch.DataTextField = Ds.Tables[0].Columns[1].ToString();
            drpCustomerSearch.DataValueField = Ds.Tables[0].Columns[0].ToString();
            drpCustomerSearch.DataBind();
            drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));
            miNewJobInfo.Visible = true;
            miNewFileInfo.Visible = true;
            miJobTracking.Visible = true;
            miNewCostDetails.Visible = true;
            miNewReportDetails.Visible = true;
            miLaunchDetails.Visible = false;
            miFileInfo.Visible = false;
            miCostDetails.Visible = false;
            miReportDetails.Visible = false;
            Button1.Visible = false;
            btnCustQuote.Visible = false;
            btnLPFrom.Visible = true;
            btnLPQuote.Visible = true;
        }
        this.showPanel(tabGeneral);
    }
    protected void lnkNewReportInfo_Click(object sender, EventArgs e)
    {
        CrystalReportViewer1.Visible = false;
        CrystalReportViewer2.Visible = false;
        this.showPanel(tabNewreportdetails);
    }
    protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        DataSet tn = new DataSet();
        tn = nonLa.getJobTrackingLP(hfP_ID.Value);
        if (tn != null)
        {
            gv_imagesNew.DataSource = tn;
            gv_imagesNew.DataBind();
        }
    }
    protected void lnkLoggedEvent_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            DataSet ds = new DataSet();
            ds = nonLa.GetLPLoggedEvents(hfP_ID.Value);
            gvLoggedEvents.DataSource = ds;
            gvLoggedEvents.DataBind();
            ds = nonLa.GetLPLoggedTotalTime(hfP_ID.Value);
            if (ds != null)
            {
                lblLogTotalTime.Text = "Total Time : " + ds.Tables[0].Rows[0]["TotalTime"].ToString();
            }
            else
            {
                lblLogTotalTime.Text = "";
            }
        }
        this.showPanel(tabLoggedEvents);
    }
    protected void lboxSW_SelectedIndexChanged(object sender, EventArgs e)
    {
        string soft = "";
        for (int j = 0; j < lboxSW.Items.Count; j++)
        {
            if (lboxSW.Items[j].Selected == true)
            {
                if(soft.ToString() == "")
                {
                    soft = lboxSW.Items[j].Value;
                }
                else
                {
                    soft = soft + ',' + lboxSW.Items[j].Value;
                }
            }
        }
        if (soft.ToString() != "")
        {
            DataSet dsSoft = nonLa.GetSoftVers(soft.ToString());
            lboxSWVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
            lboxSWVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
            lboxSWVer.DataSource = dsSoft;
            lboxSWVer.DataBind();
        }
    }
    protected void lnkSWDetails_Click1(object sender, EventArgs e)
    {
        string id = "";
        if (hfP_ID.Value != "")
            id = hfP_ID.Value.Trim();
        else if (tempID.Text != "")
            id = tempID.Text;

        string strPopup = "<script language='javascript' ID='script1'>"
            + "window.open('LaunchSoftLang.aspx?LP_ID=" + HttpUtility.UrlEncode(id.ToString()) + "&TaskValue=" + HttpUtility.UrlEncode(Session["TaskValue"].ToString())
            + "','new window', 'top=90, left=200, width=950, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
            + "</script>";
        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
    }
    protected void TarEdit(object sender, EventArgs e)
    {
        Session["FilePages"] = "";
        lblResult.Text = "";
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            HiddenField sJobID = (HiddenField)row.Cells[0].FindControl("hid_LP_ID");
            HiddenField hid_NTLS_ID = (HiddenField)row.Cells[0].FindControl("hid_NTLS_ID");
            HiddenField hid_Task_ID = (HiddenField)row.Cells[1].FindControl("hid_Task_ID");
            HiddenField hid_Lang_ID = (HiddenField)row.Cells[2].FindControl("hid_Lang_ID");
            HiddenField hid_Soft_ID = (HiddenField)row.Cells[3].FindControl("hid_Soft_ID");
            TextBox Files = (TextBox)row.Cells[4].FindControl("lblgvFiles");
            //NL_ID.Text = sJobID.Value;
            //NTLS_ID.Text = hid_NTLS_ID.Value;
            //Task_ID.Text = hid_Task_ID.Value;
            //Soft_ID.Text = hid_Soft_ID.Value;
            //Lang_ID.Text = hid_Lang_ID.Value;
            //txtFiles.Text = Files.Text;
            int File = 0;
            if (Files.Text != "")
                File = Convert.ToInt16(Files.Text);
            DataSet dss = new DataSet();
            DataTable dTable = new DataTable();
            dTable.Columns.Add("Files_ID");
            dTable.Columns.Add("LP_ID");
            dTable.Columns.Add("NTLS_ID");
            dTable.Columns.Add("TaskName");
            dTable.Columns.Add("Task_ID");
            dTable.Columns.Add("Lang_name");
            dTable.Columns.Add("Lang_ID");
            dTable.Columns.Add("Soft_Name");
            dTable.Columns.Add("Soft_ID");
            dTable.Columns.Add("Files_Name");
            dTable.Columns.Add("Pages");

            dss = nonLa.GetTarFilePages(hid_NTLS_ID.Value);
            if (dss == null)
            {
                dss = nonLa.getLPNTLS(hid_NTLS_ID.Value);

                for (int j = 1; j <= File; j++)
                {
                    dTable.Rows.Add(j, dss.Tables[0].Rows[0]["LP_ID"].ToString(), dss.Tables[0].Rows[0]["NTLS_ID"].ToString(),
                        dss.Tables[0].Rows[0]["TaskName"].ToString(), dss.Tables[0].Rows[0]["Task_ID"].ToString(),
                        dss.Tables[0].Rows[0]["Lang_name"].ToString(), dss.Tables[0].Rows[0]["Lang_ID"].ToString(),
                        dss.Tables[0].Rows[0]["Soft_Name"].ToString(), dss.Tables[0].Rows[0]["Soft_ID"].ToString(), "", 0);
                }
            }
            else
            {
                dss = nonLa.getTarLPInsertedNTLS(hid_NTLS_ID.Value);
                int k = 1;
                for (int j = 0; j <= (File - 1); j++)
                {
                    if (k <= dss.Tables[0].Rows.Count)
                    {
                        dTable.Rows.Add(dss.Tables[0].Rows[j]["Files_ID"].ToString(), dss.Tables[0].Rows[j]["LP_ID"].ToString(),
                            dss.Tables[0].Rows[j]["NTLS_ID"].ToString(),
                            dss.Tables[0].Rows[j]["TaskName"].ToString(), dss.Tables[0].Rows[j]["Task_ID"].ToString(),
                            dss.Tables[0].Rows[j]["Lang_name"].ToString(), dss.Tables[0].Rows[j]["Lang_ID"].ToString(),
                            dss.Tables[0].Rows[j]["Soft_Name"].ToString(), dss.Tables[0].Rows[j]["Soft_ID"].ToString(),
                            dss.Tables[0].Rows[j]["Files_Name"].ToString(), dss.Tables[0].Rows[j]["Pages"].ToString());
                    }
                    else
                    {
                        dTable.Rows.Add(k, dss.Tables[0].Rows[0]["LP_ID"].ToString(),
                            dss.Tables[0].Rows[0]["NTLS_ID"].ToString(),
                            dss.Tables[0].Rows[0]["TaskName"].ToString(), dss.Tables[0].Rows[0]["Task_ID"].ToString(),
                            dss.Tables[0].Rows[0]["Lang_name"].ToString(), dss.Tables[0].Rows[0]["Lang_ID"].ToString(),
                            dss.Tables[0].Rows[0]["Soft_Name"].ToString(), dss.Tables[0].Rows[0]["Soft_ID"].ToString(),
                            "", 0);
                    }
                    k++;
                }
            }
            DataSet Ds = new DataSet();
            Ds.Tables.Add(dTable);
            gv_FilePages.DataSource = Ds;
            gv_FilePages.DataBind();
            // popup.Show();
            Session["TarFilePages"] = Ds;

            string strPopup = "<script language='javascript' ID='script1'>"
            + "window.open('LaunchTarFilesPages.aspx?NL_ID=" + HttpUtility.UrlEncode(sJobID.Value) + "&NTLS_ID=" + HttpUtility.UrlEncode(hid_NTLS_ID.Value)
            + "&Task_ID=" + HttpUtility.UrlEncode(hid_Task_ID.Value) + "&Soft_ID=" + HttpUtility.UrlEncode(hid_Soft_ID.Value) + "&Lang_ID=" + HttpUtility.UrlEncode(hid_Lang_ID.Value) + "&File=" + HttpUtility.UrlEncode(File.ToString()) + "&FileConv=" + HttpUtility.UrlEncode(DropNewNameConv.SelectedValue)
            + "','new window', 'top=90, left=200, width=950, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
            + "</script>";
            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
    }
    protected void xxxx_Click(object sender, EventArgs e)
    {
        DataSet ds1 = new DataSet();
        ds1 = nonLa.getTarTaskLangDetailsByLPID(hfP_ID.Value);
        if (hfP_ID.Value != "")
        {
            gvTarFileInfo.DataSource = ds1;
            gvTarFileInfo.DataBind();
        }
        DataSet tn = new DataSet();
        tn = nonLa.getJobTrackingLP(hfP_ID.Value);
        if (tn != null)
        {
            gv_imagesNew.DataSource = tn;
            gv_imagesNew.DataBind();
        }
    }
    protected void CheckNewYTR_CheckedChanged1(object sender, EventArgs e)
    {
        if (CheckNewYTR.Checked)
        {
            gvTarFileInfo.Visible = false;
            lblTarName.Visible = false;
        }
        else
        {
            gvTarFileInfo.Visible = true;
            lblTarName.Visible = true;
        }
    }
    protected void xlang_Click(object sender, EventArgs e)
    {
        string id = "";
        if (hfP_ID.Value != "")
            id = hfP_ID.Value.Trim();
        else if (tempID.Text != "")
            id = tempID.Text;

        DataSet dss = new DataSet();
        dss = nonLa.GetselectedLangLP(id.ToString());
        lboxNewlangused.Items.Clear();
        if (dss != null)
        {
            lboxNewlangused.DataSource = dss;
            lboxNewlangused.DataTextField = dss.Tables[0].Columns[1].ToString();
            lboxNewlangused.DataValueField = dss.Tables[0].Columns[0].ToString();
            lboxNewlangused.DataBind();
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList al = new ArrayList();
        Hashtable val = null;
        bool cks = false;
        try
        {
            int Month = Convert.ToInt16(DDMonthList.SelectedValue);
            int Year = Convert.ToInt32(DDYearList.SelectedValue);
            int Total = Month + Year * 12;
            if (Total < 24186)
            {
                foreach (GridViewRow grw in GvProject.Rows)
                {
                    CheckBox ck = ((CheckBox)grw.FindControl("chkBoxStatus"));
                    if (ck.Checked)
                    {
                        val = new Hashtable();
                        val.Add("ID", ((HiddenField)grw.FindControl("hfgvProjectID")).Value.ToString());
                        val.Add("Status", ((DropDownList)grw.FindControl("DropStatus")).SelectedValue.ToString());
                        val.Add("EMPID", Session["employeeid"].ToString());
                        al.Add(val);
                        cks = true;
                    }
                }
                if (cks == true)
                {
                    if (!la.Update_DeliveryStatus(al))
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Please select CheckBox to the Project..');</script>");
                }
            }
            else
            {
                foreach (GridViewRow grw in GvNL.Rows)
                {
                    CheckBox ck = ((CheckBox)grw.FindControl("chkBoxStatus"));
                    if (ck.Checked)
                    {
                        val = new Hashtable();
                        val.Add("ID", ((HiddenField)grw.FindControl("hfgvNLID")).Value.ToString());
                        val.Add("Status", ((DropDownList)grw.FindControl("DropStatus")).SelectedValue.ToString());
                        val.Add("EMPID", Session["employeeid"].ToString());
                        val.Add("Jobno", ((TextBox)grw.FindControl("lblJobNo")).Text.ToString());
                        al.Add(val);
                        cks = true;
                    }
                }
                if (cks == true)
                {
                    if (!nonLa.Update_DeliveryStatus1(al))
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Please select CheckBox to the Project..');</script>");
                }
            }
            btnSearch_Click(sender, e);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { la = null; }
    }
    protected void lnkFinalQuote_Click(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
        {
            DataSet ds2 = new DataSet();
            DataSet ds1 = new DataSet();
            ds2 = nonLa.GetFQLP(hfP_ID.Value);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                gv_Lmodule1.DataSource = ds2;
                gv_Lmodule1.DataBind();
                gv_Lmodule1.Visible = true;
                gv_Lmodule.Visible = false;
                imgFQ.Visible = false;
            }
            else
            {
                ds1 = nonLa.getFinalQuoteVal(hfP_ID.Value);
                gv_Lmodule.DataSource = ds1;
                gv_Lmodule.DataBind();
                gv_Lmodule.Visible = true;
                gv_Lmodule1.Visible = false;
                imgFQ.Visible = true;
            }
        }
        this.showPanel(tabFinalQuote);
    }
    protected void imgbtnFinalQuoteSave_Click(object sender, ImageClickEventArgs e)
    {
        string[] aProjectDetails;
        if (hfP_ID.Value != "")
        {
            foreach (GridViewRow grs in gv_Lmodule.Rows)
            {
                TextBox Desc = (TextBox)grs.FindControl("txt_des");
                TextBox Qty = (TextBox)grs.FindControl("txt_qty");
                TextBox PC = (TextBox)grs.FindControl("txt_pricecode");
                TextBox po = (TextBox)grs.FindControl("txt_mponumber");
                DropDownList costtype = (DropDownList)grs.FindControl("ddl_costtype");
                TextBox Rate = (TextBox)grs.FindControl("txt_Rate");

                string proc = "spInsertLP_FinalQuote";
                string[,] para = { {"@LP_ID", hfP_ID.Value}, {"@Rate", Rate.Text },
                                   {"@PONumber", po.Text }, {"@COSTTYPEID",costtype.SelectedValue},
                                   {"@PRICECODE",PC.Text}, {"@CreatedBY",Session["employeeid"].ToString()},
                                   {"@Qty",Qty.Text},{"@Desc",Desc.Text}};
                int val = this.oNewLa.ExcSP(proc, para, CommandType.StoredProcedure);
            }
            Alert("Successfully Saved.");
            lnkFinalQuote_Click(sender, e);
        }
    }
    protected void gv_Lmodule1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Add"))
        {
            TextBox txtAddDesc = (TextBox)gv_Lmodule1.FooterRow.FindControl("txtAddDesc");
            TextBox txtAddQty = (TextBox)gv_Lmodule1.FooterRow.FindControl("txtAddQty");
            TextBox txtAddPC = (TextBox)gv_Lmodule1.FooterRow.FindControl("txtAddPC");
            TextBox txtAddPO = (TextBox)gv_Lmodule1.FooterRow.FindControl("txtAddPO");
            DropDownList Addddl_costtype = (DropDownList)gv_Lmodule1.FooterRow.FindControl("Addddl_costtype");
            TextBox Addddl_Rate = (TextBox)gv_Lmodule1.FooterRow.FindControl("txtAddRate");

            string proc = "spInsertLP_FinalQuote";
            string[,] para = { {"@LP_ID", hfP_ID.Value},{"@Rate", Addddl_Rate.Text }, { "@ponumber", txtAddPO.Text }, 
                                {"@COSTTYPEID",Addddl_costtype.SelectedValue},{"@PRICECODE",txtAddPC.Text},
                                {"@CreatedBY",Session["employeeid"].ToString()},{"@Qty",txtAddQty.Text},
                                {"@Desc",txtAddDesc.Text}};
            int val = this.oNewLa.ExcSP(proc, para, CommandType.StoredProcedure);

            lnkFinalQuote_Click(sender, e);
        }
    }
    protected void ibtn_Delete_click(object sender, ImageClickEventArgs e)
    {
        string uval = string.Empty;
        foreach (GridViewRow gr in gv_Lmodule1.Rows)
        {
            if (((CheckBox)gr.FindControl("cb_delete")).Checked)
            {
                if (uval == "")
                {
                    uval = ((HiddenField)gr.FindControl("hf_FQ_ID")).Value;
                }
                else
                {
                    uval = ((HiddenField)gr.FindControl("hf_FQ_ID")).Value + "," + uval;
                }
            }
        }
        if (!string.IsNullOrEmpty(uval))
        {
            uval = uval.TrimEnd(',');
            datasourceIBSQL mobj = new datasourceIBSQL();
            if (nonLa.DeleteFQLPModules(uval))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Deleted');</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Deletion Failed');</script>");
           
            lnkFinalQuote_Click(sender, e);
        }

    }
    protected void gv_Lmodule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string s = string.Empty;
            TextBox txt_des = e.Row.FindControl("txt_des") as TextBox;
            string[] des = txt_des.Text.Split(',');
            int i = des.Length - 1;
            for (int j = 0; j < des.Length; j++)
            {
                if (i > j)
                {
                    if (s == "")
                    {
                        s = des[j];
                    }
                    else
                    {
                        s = s + "," + des[j];
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        s = des[j];
                    }
                    else
                    {
                        s = s + " and" + des[j];
                    }
                }
            }
            txt_des.Text = s.ToString().Trim();
        }
    }
    protected void imgASave_Click(object sender, ImageClickEventArgs e)
    {
        if (hfP_ID.Value != "" && txtADesc.Text.Trim() != "")
        {
            string proc = "spInsertLP_AddQuote";
            string[,] para = { {"@LP_ID", hfP_ID.Value},{"@Rate", txtARate.Text }, 
                            {"@COSTTYPEID",ddlAcosttype.SelectedValue},{"@Remarks",txtARemarks.Text},
                            {"@CreatedBY",Session["employeeid"].ToString()},{"@Qty",txtAQty.Text},
                            {"@Desc",txtADesc.Text}, { "@ponumber", txtAPONum.Text }
                         };
            int val = this.oNewLa.ExcSP(proc, para, CommandType.StoredProcedure);
            DataSet ds = new DataSet();
            ds = nonLa.GetAQLP(hfP_ID.Value);
            gvAQuote.DataSource = ds;
            gvAQuote.DataBind();
            txtARate.Text = "";
            txtARemarks.Text = "";
            txtADesc.Text = "";
            txtAPONum.Text = "";
            txtAQty.Text = "";
        }
        this.showPanel(tabAddQuote);
    }
    protected void lnkAddQuote_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            txtARate.Text = "";
            txtARemarks.Text = "";
            txtAQty.Text = "";
            txtADesc.Text = "";
            txtAPONum.Text = "";

            DataSet ds = new DataSet();
            ds = nonLa.GetAQLP(hfP_ID.Value);
            gvAQuote.DataSource = ds;
            gvAQuote.DataBind();
        }
        this.showPanel(tabAddQuote);
    }
    protected void OnClickFU(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            txtFUDate.Text = "";
            txtFURemarks.Text = "";
            DataSet ds = new DataSet();
            ds = nonLa.GetFULP(hfP_ID.Value);
            gvFU.DataSource = ds;
            gvFU.DataBind();
        }
        FUPopUp.Show();
    }
    protected void imgbtnFUclose_Click(object sender, EventArgs e)
    {
        FUPopUp.Hide();
    }
    protected void btnFUSave_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            string ddate = "";
            if (txtFUDate.Text != "")
                ddate = DateTime.Parse(txtFUDate.Text.Trim()).ToString("MM/dd/yyyy");
            else
                ddate = "";
            if (txtFUDate.Text != "")
                nonLa.InsertFU(hfP_ID.Value, ddate.ToString(), txtFURemarks.Text, Session["employeeid"].ToString());

            DataSet ds = new DataSet();
            ds = nonLa.GetFULP(hfP_ID.Value);
            gvFU.DataSource = ds;
            gvFU.DataBind();
        }
        FUPopUp.Show();
    }
    protected void chkNewYTC_CheckedChanged(object sender, EventArgs e)
    {
        //if(chkNewYTC.Checked)
        //{
        //    lblTATs.Visible = true;
        //    dropTATsHrs.Visible = true;
        //    dropTATsDays.Visible = true;
        //    Label28.Visible = true;
        //}
        //else
        //{
        //    lblTATs.Visible = false;
        //    dropTATsHrs.Visible = false;
        //    dropTATsDays.Visible = false;
        //    Label28.Visible = false;
        //}
    }
    protected void dropProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNewProjectTitle.Text = "";
        lblInitalID.Text = "";
        txtNewProjectEditor.Text = "";
        DataSet ds = new DataSet();
        ds = nonLa.getLIinitialbyID(dropProjectName.SelectedValue);
        txtNewProjectTitle.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
        drpNewProjectcustomer.SelectedValue = ds.Tables[0].Rows[0]["Custno"].ToString();
        DataSet ds1 = nonLa.GetLocationCust(drpNewProjectcustomer.SelectedValue);
        if (ds1 != null)
        {
            DropNewLocation.Enabled = true;
            DropNewLocation.DataSource = ds1;
            DropNewLocation.DataValueField = ds1.Tables[0].Columns[3].ToString();
            DropNewLocation.DataTextField = ds1.Tables[0].Columns[4].ToString();
            DropNewLocation.DataBind();
        }
        else
        {
            DropNewLocation.Items.Insert(0, new ListItem("-- select --", "0"));
            DropNewLocation.SelectedValue = "0";
            DropNewLocation.Enabled = false;
        }
        DropNewLocation.SelectedValue = ds.Tables[0].Rows[0]["Location_ID"].ToString();
        getloc_timezoneNew(Convert.ToInt16(DropNewLocation.SelectedValue));
        txtNewProjectEditor.Text = ds.Tables[0].Rows[0]["PEname"].ToString();
        lblInitalID.Text = ds.Tables[0].Rows[0]["LI_ID"].ToString();
    }
}


   


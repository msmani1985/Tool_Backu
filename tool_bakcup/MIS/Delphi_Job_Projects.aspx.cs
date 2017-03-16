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
using System.IO;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;
using System.Data.OleDb;

public partial class Delphi_Job_Projects : System.Web.UI.Page
{
    public int id = 1;
    private Delphi_Projects oPro = new Delphi_Projects();
    private static DataSet dsGrapType = new DataSet();
    private static DataTable dtProject = new DataTable();
    private static string sSortExpression = "";
    datasourceSQL oSql = new datasourceSQL();
    datasourceIBSQL oIBSql = new datasourceIBSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            this.popScreen();
    }
    private void popScreen()
    {
        DataSet dsemp = oPro.GetEmployeeName();
        DropEmpnameMain.DataSource = dsemp;
        DropEmpnameMain.DataTextField = dsemp.Tables[0].Columns[1].ToString();
        DropEmpnameMain.DataValueField = dsemp.Tables[0].Columns[0].ToString();
        DropEmpnameMain.DataBind();
        DropEmpnameMain.Items.Insert(0, new ListItem("-- select --", "0"));

        dropEmpPProof.DataSource = dsemp;
        dropEmpPProof.DataTextField = dsemp.Tables[0].Columns[1].ToString();
        dropEmpPProof.DataValueField = dsemp.Tables[0].Columns[0].ToString();
        dropEmpPProof.DataBind();
        dropEmpPProof.Items.Insert(0, new ListItem("-- select --", "0"));

        dropEmpFirst.DataSource = dsemp;
        dropEmpFirst.DataTextField = dsemp.Tables[0].Columns[1].ToString();
        dropEmpFirst.DataValueField = dsemp.Tables[0].Columns[0].ToString();
        dropEmpFirst.DataBind();
        dropEmpFirst.Items.Insert(0, new ListItem("-- select --", "0"));

        dropEmpSecond.DataSource = dsemp;
        dropEmpSecond.DataTextField = dsemp.Tables[0].Columns[1].ToString();
        dropEmpSecond.DataValueField = dsemp.Tables[0].Columns[0].ToString();
        dropEmpSecond.DataBind();
        dropEmpSecond.Items.Insert(0, new ListItem("-- select --", "0"));

        dropEmpThird.DataSource = dsemp;
        dropEmpThird.DataTextField = dsemp.Tables[0].Columns[1].ToString();
        dropEmpThird.DataValueField = dsemp.Tables[0].Columns[0].ToString();
        dropEmpThird.DataBind();
        dropEmpThird.Items.Insert(0, new ListItem("-- select --", "0"));

        dropEmpFinal.DataSource = dsemp;
        dropEmpFinal.DataTextField = dsemp.Tables[0].Columns[1].ToString();
        dropEmpFinal.DataValueField = dsemp.Tables[0].Columns[0].ToString();
        dropEmpFinal.DataBind();
        dropEmpFinal.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dsdepart = oPro.GetDepartment();
        DropDepartment.DataSource = dsdepart;
        DropDepartment.DataTextField = dsdepart.Tables[0].Columns[1].ToString();
        DropDepartment.DataValueField = dsdepart.Tables[0].Columns[0].ToString();
        DropDepartment.DataBind();

        dropProStage.DataSource = dsdepart;
        dropProStage.DataTextField = dsdepart.Tables[0].Columns[1].ToString();
        dropProStage.DataValueField = dsdepart.Tables[0].Columns[0].ToString();
        dropProStage.DataBind();

        string myvalue = "10066";
        DataSet dscust = oPro.getCustomers();
        drpCustomerSearch.DataSource = dscust;
        drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
        drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
        drpCustomerSearch.SelectedValue = myvalue.ToString();
        drpCustomerSearch.DataBind();
        drpProjectCustomer.DataSource = dscust;
        drpProjectCustomer.DataTextField = dscust.Tables[0].Columns[1].ToString();
        drpProjectCustomer.DataValueField = dscust.Tables[0].Columns[0].ToString();
        drpProjectCustomer.SelectedValue = myvalue.ToString();
        drpProjectCustomer.DataBind();
        drpProjectcustfinsite.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dsStatus = oPro.getCurrentStatus();
        dropCurStatus.DataSource = dsStatus;
        dropCurStatus.DataTextField = dsStatus.Tables[0].Columns[1].ToString();
        dropCurStatus.DataValueField = dsStatus.Tables[0].Columns[0].ToString();
        dropCurStatus.DataBind();
        dropCurStatus.Items.Insert(0, new ListItem("-- select --", "0"));
        dropCurStatus.SelectedValue = "15";

        DataSet dscustPE = oPro.getPEName();
        txtProjectEditor.DataSource = dscustPE;
        txtProjectEditor.DataTextField = dscustPE.Tables[0].Columns[1].ToString();
        txtProjectEditor.DataValueField = dscustPE.Tables[0].Columns[0].ToString();
        txtProjectEditor.DataBind();

        DataSet dsSevice = oPro.getProjectStage();
        DropCurStage.DataSource = dsSevice;
        DropCurStage.DataTextField = dsSevice.Tables[0].Columns[1].ToString();
        DropCurStage.DataValueField = dsSevice.Tables[0].Columns[0].ToString();
        DropCurStage.DataBind();
        DropCurStage.Items.Insert(0, new ListItem("-- select --", "0"));
        DropCurStage.SelectedValue = "10077";

        DataSet dsFormat = oPro.getTypeset();
        DropTypeset.DataSource = dsFormat;
        DropTypeset.DataTextField = dsFormat.Tables[0].Columns[1].ToString();
        DropTypeset.DataValueField = dsFormat.Tables[0].Columns[0].ToString();
        DropTypeset.DataBind();
        DropTypeset.Items.Insert(0, new ListItem("-- select --", "0"));

        
        dropInput.DataSource = dsFormat;
        dropInput.DataTextField = dsFormat.Tables[0].Columns[1].ToString();
        dropInput.DataValueField = dsFormat.Tables[0].Columns[0].ToString();
        dropInput.DataBind();
        dropInput.Items.Insert(0, new ListItem("-- select --", "0"));

        
        DropOutPut.DataSource = dsFormat;
        DropOutPut.DataTextField = dsFormat.Tables[0].Columns[1].ToString();
        DropOutPut.DataValueField = dsFormat.Tables[0].Columns[0].ToString();
        DropOutPut.DataBind();
        DropOutPut.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dsProd = oPro.GetProductCode();
        dropProdCode.DataSource = dsProd;
        dropProdCode.DataTextField = dsProd.Tables[0].Columns[1].ToString();
        dropProdCode.DataValueField = dsProd.Tables[0].Columns[0].ToString();
        dropProdCode.DataBind();
        dropProdCode.Items.Insert(0, new ListItem("-- select --", "0"));

        this.showPanel(tabGeneral);
    }

    protected void drpProjectCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpProjectcustfinsite.Items.Clear();
        if (drpProjectCustomer.SelectedItem.Value.Trim() != "0")
        {
            DataSet dscustfin = oPro.getCusomerFinsite(drpProjectCustomer.SelectedItem.Value.Trim());
            drpProjectcustfinsite.DataSource = dscustfin;
            drpProjectcustfinsite.DataTextField = dscustfin.Tables[0].Columns[1].ToString();
            drpProjectcustfinsite.DataValueField = dscustfin.Tables[0].Columns[0].ToString();
            drpProjectcustfinsite.DataBind();
        }
        drpProjectcustfinsite.Items.Insert(0, new ListItem("-- select --", "0"));
    }

    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabGeneral":
                miGeneral.Attributes.Add("class", "current");
                miProjectDetails.Attributes.Add("class", "");
                miProjectEvents.Attributes.Add("class", "");
                miProjectBarcode.Attributes.Add("class", "");
                miProjectListUpload.Attributes.Add("class", "");
                this.tabProjectBarcode.Visible = false;
                this.tabGeneral.Visible = true;
                this.tabProjectDetails.Visible = false;
                this.tabProjectEvents.Visible = false;
                this.tabProjectListUpload.Visible = false;
                break;
            case "tabProjectDetails":
                if (this.hfP_Name.Value != "")
                    //lblBookHeader.Text = "Edit Book : " + this.hfB_Name.Value;
                miGeneral.Attributes.Add("class", "");
                miProjectDetails.Attributes.Add("class", "current");
                miProjectEvents.Attributes.Add("class", "");
                miProjectBarcode.Attributes.Add("class", "");
                miProjectListUpload.Attributes.Add("class", "");
                this.tabProjectBarcode.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabProjectDetails.Visible = true;
                this.tabProjectEvents.Visible = false;
                this.tabProjectListUpload.Visible = false;
                break;

            case "tabProjectEvents":
                if (this.hfP_Name.Value != "")
                    //lblChapHeader.Text = "Chapter Details : " + this.hfB_Name.Value;
                miGeneral.Attributes.Add("class", "");
                miProjectDetails.Attributes.Add("class", "");
                miProjectEvents.Attributes.Add("class", "current");
                miProjectBarcode.Attributes.Add("class", "");
                miProjectListUpload.Attributes.Add("class", "");
                this.tabProjectBarcode.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabProjectDetails.Visible = false;
                this.tabProjectEvents.Visible = true;
                this.tabProjectListUpload.Visible = false;
                break;
            case "tabProjectBarcode":
                if (this.hfP_Name.Value != "")
                    miGeneral.Attributes.Add("class", "");
                miProjectDetails.Attributes.Add("class", "");
                miProjectEvents.Attributes.Add("class", "");
                miProjectBarcode.Attributes.Add("class", "current");
                miProjectListUpload.Attributes.Add("class", "");
                this.tabProjectBarcode.Visible = true;
                this.tabGeneral.Visible = false;
                this.tabProjectDetails.Visible = false;
                this.tabProjectEvents.Visible = false;
                this.tabProjectListUpload.Visible = false;
                break;
            case "miProjectListUpload":
                if (this.hfP_Name.Value != "")
                    miGeneral.Attributes.Add("class", "");
                miProjectDetails.Attributes.Add("class", "");
                miProjectEvents.Attributes.Add("class", "");
                miProjectBarcode.Attributes.Add("class", "");
                miProjectListUpload.Attributes.Add("class", "current");
                this.tabProjectBarcode.Visible = false;
                this.tabGeneral.Visible = false;
                this.tabProjectDetails.Visible = false;
                this.tabProjectEvents.Visible = false;
                this.tabProjectListUpload.Visible = true;
                break;

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        char CompleteFlag = 'N';
        if (chkViewCompleted.Checked) CompleteFlag = 'Y';
        DataSet dsb = oPro.getProject(txtSearch.Text.Trim().ToUpper(), CompleteFlag,
            drpCustomerSearch.SelectedItem.Value.Trim());
        dtProject = dsb.Tables[0].Copy();
        gvProjects.DataSource = dtProject;
        gvProjects.DataBind();
        this.showPanel(tabGeneral);
        hfP_ID.Value = "";
    }
    protected void lnkGeneral_Click(object sender, EventArgs e)
    {
        this.showPanel(tabGeneral);
    }
    protected void lnkProjectdetails_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            loadProjectDetails(hfP_ID.Value.Trim());
        }
        this.showPanel(tabProjectDetails);
    }
    protected void lnkProjectEvents_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            DataSet dsProject = oPro.getProjectEvents(hfP_ID.Value.Trim());
            gvEvents.DataSource = dsProject.Tables[0];
            gvEvents.DataBind();
        }
        this.showPanel(tabProjectEvents);
    }
    protected void gvProjects_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,\"" + ((HiddenField)e.Row.FindControl("hfgvProjectID")).Value.Trim() + "\",\"" + ((Label)e.Row.FindControl("lblgvpcode")).Text.Trim() + "\");";
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
    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (drpProjectCustomer.SelectedItem.Value == "0") sMessage += i++ + ". Select a Customer\\r\\n";
        if (txtProjectNo.Text.Trim() == "") sMessage += i++ + ". Enter CAT# Name\\r\\n";
        if (txtPojectTitle.Text.Trim() == "") sMessage += i++ + ". Enter Project Title\\r\\n";
        if (drpProjectcustfinsite.SelectedItem.Value == "0") sMessage += i++ + ". Select Financial Site\\r\\n";
        if (hfP_ID.Value == "")
        {
            if (txtPONumber.Text != "")
            {
                string[] ponumbers = txtPONumber.Text.Trim().Split(',');
                string sMsg = "";
                foreach (string ponumber in ponumbers)
                {
                    DataSet dsPro = oPro.chkPoNumber(ponumber.Trim());
                    if (dsPro != null)
                    {
                        if (dsPro.Tables[0].Rows.Count > 0)
                        {
                            if (sMsg == "")
                            {
                                sMsg = ponumber.Trim();
                            }
                            else
                            {
                                sMsg = sMsg + ", " + ponumber.Trim();
                            }
                        }
                    }
                }
                if (sMsg != "")
                {
                    sMessage += i++ + ". " + sMsg.Trim() + " PO Number Already Exists.";
                }
            }
        }
        if (i > 1)
        {
            Alert(sMessage);
            return false;
        }
        return true;
    }
    private void loadProjectDetails(string sProjectID)
    {
        sProjectID = sProjectID.Trim();
        if (sProjectID != "")
        {
            DataSet dsPro = oPro.getProjectDetailsByID(sProjectID);
            //lblBookHeader.Text = "Edit Book";
            //imgBookHeader.Src = "images/tools/edit.png";
            DataRow row = dsPro.Tables[0].Rows[0];
            drpProjectCustomer.ClearSelection();
            if (drpProjectCustomer.Items.FindByValue(row["custno"].ToString().Trim()) != null)
                drpProjectCustomer.Items.FindByValue(row["custno"].ToString().Trim()).Selected = true;
            drpProjectcustfinsite.Items.Clear();
            DataSet dscustfin = oPro.getCusomerFinsite(row["custno"].ToString().Trim());
            drpProjectcustfinsite.DataSource = dscustfin;
            drpProjectcustfinsite.DataTextField = dscustfin.Tables[0].Columns[1].ToString();
            drpProjectcustfinsite.DataValueField = dscustfin.Tables[0].Columns[0].ToString();
            drpProjectcustfinsite.DataBind();
            if (dscustfin.Tables[0].Rows.Count > 0)
            {
                if (drpProjectcustfinsite.Items.FindByValue(row["finsiteno"].ToString().Trim()) != null)
                    drpProjectcustfinsite.Items.FindByValue(row["finsiteno"].ToString().Trim()).Selected = true;
            }
            drpProjectcustfinsite.Items.Insert(0, new ListItem("-- select --", "0"));
            txtProjectNo.Text = row["Pcode"].ToString();
            txtPojectTitle.Text = row["PTITLE"].ToString();
            txtPRecvDate.Text = row["PRECEIVEDDATE"].ToString();
            txtPDueDate.Text = row["PDUEDATE"].ToString();
            txtPComptDate.Text = row["PCOMPLETEDDATE"].ToString();
            txtProjectadditem.Text = row["PADDITEMS"].ToString();
            txtProjectEditor.SelectedValue = row["CONNO"].ToString();
            txtStartPProof.Text = row["PFIRSTSTARTDATE"].ToString();
            txtDuePProof.Text = row["PFIRSTDUEDATE"].ToString();
            txtHalfPProof.Text = row["PFIRSTHALFDUEDATE"].ToString();
            dropEmpPProof.SelectedValue = row["PFIRSTEMPLOYEE"].ToString();
            txtDispPProof.Text = row["PFIRSTDISPATCH"].ToString();
            txtStartFirst.Text = row["PSECONDSTARTDATE"].ToString();
            txtDueFirst.Text = row["PSECONDDUEDATE"].ToString();
            txtHalfFirst.Text = row["PSECONDHALFDUEDATE"].ToString();
            dropEmpFirst.SelectedValue = row["PSECONDEMPLOYEE"].ToString();
            txtDispFirst.Text = row["PSECONDDISPATCH"].ToString();
            txtStartSecond.Text = row["PTHIRDSTARTDATE"].ToString();
            txtDueSecond.Text = row["PTHIRDDUEDATE"].ToString();
            txtHalfSecond.Text = row["PTHIRDHALFDUEDATE"].ToString();
            dropEmpSecond.SelectedValue = row["PTHIRDEMPLOYEE"].ToString();
            txtDispSecond.Text = row["PTHIRDDISPATCH"].ToString();
            txtStartThird.Text = row["PFOURTHSTARTDATE"].ToString();
            txtDueThird.Text = row["PFOURTHDUEDATE"].ToString();
            txtHalfThird.Text = row["PFOURTHHALFDUEDATE"].ToString();
            dropEmpThird.SelectedValue = row["PFOURTHEMPLOYEE"].ToString();
            txtDispThird.Text = row["PFOURTHDISPATCH"].ToString();
            txtStartFProof.Text = row["PFINALSTARTDATE"].ToString();
            txtDueFProof.Text = row["PFINALDUEDATE"].ToString();
            txtHalfFProof.Text = row["PFINALHALFDUEDATE"].ToString();
            txtDispFProof.Text = row["PFINALDISPATCH"].ToString();
            //dropEmpFinal.SelectedValue = row["PFINALDISPATCH"].ToString();
            txtInvoiceDate.Text = row["PINVOICEDDATE"].ToString();
            txtInvoiceNo.Text = row["INVNO"].ToString();
            dropProdCode.SelectedValue = row["DIGITALPRODNO"].ToString();
            DropTypeset.SelectedValue = row["TPLATNO"].ToString();
            dropCurStatus.SelectedValue = row["stno"].ToString();
            DropCurStage.SelectedValue = row["STYPENO"].ToString();
            DropEmpnameMain.SelectedValue = row["EMPNO"].ToString();
            DropDepartment.SelectedValue = row["DNO"].ToString();
            txtAddCharge.Text = row["PADDCHARGES"].ToString();
            txtPONumber.Text = row["PONUMBER"].ToString();
            txtCrditCost.Text = "";//
            txtProjectCost.Text = row["PCNO_2010"].ToString();//
            //dropInput.SelectedValue = row["PINPUTS1"].ToString();
            //DropOutPut.SelectedValue = row["POUTPUTS1"].ToString();
            txtNoCharater.Text = row["PNOOFCHARACTERS"].ToString();
            txtISBN.Text = row["PISBN"].ToString();
            DropCrdInd.SelectedValue = row["PCREDITED_IND"].ToString();
            DropCrd.SelectedValue = row["PCREDITED"].ToString();
            txtChargedPages.Text = row["PNOOFCHARGEDPAGES"].ToString();
            txtChargedArticle.Text = row["PNOOFCHARGEDARTICLES"].ToString();
            DropDigitalProd.SelectedValue = row["PDIGITAL"].ToString();//
            txtProjectNumber.Text = row["PROJECTNUMBER"].ToString();
            txtPages.Text = row["PNOOFPAGES"].ToString();
            txtcost1.Text = row["PACOSTDESC1"].ToString();
            txtQty1.Text = row["PAQTY1"].ToString();
            txtPrice1.Text = row["PACNO1"].ToString();
            txtcost2.Text = row["PACOSTDESC2"].ToString();
            txtQty2.Text = row["PAQTY2"].ToString();
            txtPrice2.Text = row["PACNO2"].ToString();
            txtcost3.Text = row["PACOSTDESC3"].ToString();
            txtQty3.Text = row["PAQTY3"].ToString();
            txtPrice3.Text = row["PACNO3"].ToString();
            txtCost4.Text = row["PACOSTDESC4"].ToString();
            txtQty4.Text = row["PAQTY4"].ToString();
            txtPrice4.Text = row["PACNO4"].ToString();
            txtCost5.Text = row["PACOSTDESC5"].ToString();
            txtQty5.Text = row["PAQTY5"].ToString();
            txtPrice5.Text = row["PACNO5"].ToString();
            rbWorkFrom.SelectedValue = row["LocationID"].ToString();
            rbTypeCost.SelectedValue = row["PCOST"].ToString();
            txtProjectDesc.Text = row["PDESCRIPTION"].ToString();
            txtProjectComments.Text = row["PCOMMENTS"].ToString();
            drpCategory.SelectedValue = row["CATEGORY"].ToString();
            if(row["PINVOICED"].ToString()=="Y")
            {
                cmd_Save_Project.Visible = false;
            }

        }
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void cmd_Save_Project_Click(object sender, ImageClickEventArgs e)
    {
        string[] aProjectDetails;
        try
        {
            if (validateScreen())
            {
                if (hfP_ID.Value == "")
                {
                    //insert Chapter
                    aProjectDetails = new string[76];
                    aProjectDetails[0] = drpProjectCustomer.SelectedValue;
                    aProjectDetails[1] = drpProjectcustfinsite.SelectedValue;
                    aProjectDetails[2] = txtProjectNo.Text;
                    aProjectDetails[3] = txtPojectTitle.Text;
                    aProjectDetails[4] = txtPRecvDate.Text;
                    aProjectDetails[5] = txtPDueDate.Text;
                    aProjectDetails[6] = txtPComptDate.Text;
                    aProjectDetails[7] = txtProjectadditem.Text;
                    aProjectDetails[8] = txtProjectEditor.SelectedValue;

                    if (txtStartPProof.Text.Trim() != "") aProjectDetails[9] = DateTime.Parse(txtStartPProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[9] = "";
                    if (txtDuePProof.Text.Trim() != "") aProjectDetails[10] = DateTime.Parse(txtDuePProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[10] = "";
                    if (txtHalfPProof.Text.Trim() != "") aProjectDetails[11] = DateTime.Parse(txtHalfPProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[11] = "";
                    if (txtDispPProof.Text.Trim() != "") aProjectDetails[13] = DateTime.Parse(txtDispPProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[13] = "";
                    aProjectDetails[12] = dropEmpPProof.SelectedValue;

                    if (txtStartFirst.Text.Trim() != "") aProjectDetails[14] = DateTime.Parse(txtStartFirst.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[14] = "";
                    if (txtDueFirst.Text.Trim() != "") aProjectDetails[15] = DateTime.Parse(txtDueFirst.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[15] = "";
                    if (txtHalfFirst.Text.Trim() != "") aProjectDetails[16] = DateTime.Parse(txtHalfFirst.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[16] = "";
                    if (txtDispFirst.Text.Trim() != "") aProjectDetails[18] = DateTime.Parse(txtDispFirst.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[18] = "";
                    aProjectDetails[17] = dropEmpFirst.SelectedValue;

                    if (txtStartSecond.Text.Trim() != "") aProjectDetails[19] = DateTime.Parse(txtStartSecond.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[19] = "";
                    if (txtDueSecond.Text.Trim() != "") aProjectDetails[20] = DateTime.Parse(txtDueSecond.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[20] = "";
                    if (txtHalfSecond.Text.Trim() != "") aProjectDetails[21] = DateTime.Parse(txtHalfSecond.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[21] = "";
                    if (txtDispSecond.Text.Trim() != "") aProjectDetails[23] = DateTime.Parse(txtDispSecond.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[23] = "";
                    aProjectDetails[22] = dropEmpSecond.SelectedValue;

                    if (txtStartThird.Text.Trim() != "") aProjectDetails[24] = DateTime.Parse(txtStartThird.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[24] = "";
                    if (txtDueThird.Text.Trim() != "") aProjectDetails[25] = DateTime.Parse(txtDueThird.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[25] = "";
                    if (txtHalfThird.Text.Trim() != "") aProjectDetails[26] = DateTime.Parse(txtHalfThird.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[26] = "";
                    if (txtDispThird.Text.Trim() != "") aProjectDetails[28] = DateTime.Parse(txtDispThird.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[28] = "";
                    aProjectDetails[27] = dropEmpThird.SelectedValue;

                    if (txtStartFProof.Text.Trim() != "") aProjectDetails[29] = DateTime.Parse(txtStartFProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[29] = "";
                    if (txtDueFProof.Text.Trim() != "") aProjectDetails[30] = DateTime.Parse(txtDueFProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[30] = "";
                    if (txtHalfFProof.Text.Trim() != "") aProjectDetails[31] = DateTime.Parse(txtHalfFProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[31] = "";
                    if (txtDispFProof.Text.Trim() != "") aProjectDetails[32] = DateTime.Parse(txtDispFProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[32] = "";

                    //aProjectDetails[32] = dropEmpFinal.SelectedValue;

                    aProjectDetails[33] = txtInvoiceDate.Text;
                    aProjectDetails[34] = txtInvoiceNo.Text;
                    aProjectDetails[35] = dropProdCode.SelectedValue;
                    aProjectDetails[36] = DropTypeset.SelectedValue;
                    aProjectDetails[37] = dropCurStatus.SelectedValue;
                    aProjectDetails[38] = DropCurStage.SelectedValue;
                    aProjectDetails[39] = DropEmpnameMain.SelectedValue;
                    aProjectDetails[40] = DropDepartment.SelectedValue;
                    aProjectDetails[41] = txtAddCharge.Text;
                    aProjectDetails[42] = txtPONumber.Text;
                    aProjectDetails[43] = txtCrditCost.Text;//
                    aProjectDetails[44] = txtProjectCost.Text;//
                    aProjectDetails[45] = dropInput.SelectedValue;
                    aProjectDetails[46] = DropOutPut.SelectedValue;
                    aProjectDetails[47] = txtNoCharater.Text;
                    aProjectDetails[48] = txtISBN.Text;
                    aProjectDetails[49] = DropCrdInd.SelectedValue;
                    aProjectDetails[50] = DropCrd.SelectedValue;
                    aProjectDetails[51] = txtChargedPages.Text;
                    aProjectDetails[52] = txtChargedArticle.Text;
                    aProjectDetails[53] = DropDigitalProd.SelectedValue;//
                    aProjectDetails[54] = txtProjectNumber.Text;
                    aProjectDetails[55] = txtPages.Text;
                    aProjectDetails[56] = txtcost1.Text;
                    aProjectDetails[57] = txtQty1.Text;
                    aProjectDetails[58] = txtPrice1.Text;
                    aProjectDetails[59] = txtcost2.Text;
                    aProjectDetails[60] = txtQty2.Text;
                    aProjectDetails[61] = txtPrice2.Text;
                    aProjectDetails[62] = txtcost3.Text;
                    aProjectDetails[63] = txtQty3.Text;
                    aProjectDetails[64] = txtPrice3.Text;
                    aProjectDetails[65] = txtCost4.Text;
                    aProjectDetails[66] = txtQty4.Text;
                    aProjectDetails[67] = txtPrice4.Text;
                    aProjectDetails[68] = txtCost5.Text;
                    aProjectDetails[69] = txtQty5.Text;
                    aProjectDetails[70] = txtPrice5.Text;
                    aProjectDetails[71] = rbWorkFrom.SelectedValue;
                    aProjectDetails[72] = rbTypeCost.SelectedValue;
                    aProjectDetails[73] = txtProjectDesc.Text;
                    aProjectDetails[74] = txtProjectComments.Text;
                    aProjectDetails[75] = drpCategory.SelectedValue.ToString();

                    string msg = this.oPro.InsertProject(aProjectDetails);
                    if (msg.Contains("Project creation failed!") ||
                        msg.Contains("PO or WO Already Exists!") ||
                        msg.Contains("Project Name Already Exists!")) Alert(msg);
                    else
                    {
                        Alert("Successfully Saved.");
                    }

                }
                else
                {
                    aProjectDetails = new string[76];
                    aProjectDetails[0] = drpProjectCustomer.SelectedValue;
                    aProjectDetails[1] = drpProjectcustfinsite.SelectedValue;
                    aProjectDetails[2] = txtProjectNo.Text;
                    aProjectDetails[3] = txtPojectTitle.Text;
                    aProjectDetails[4] = txtPRecvDate.Text;
                    aProjectDetails[5] = txtPDueDate.Text;
                    aProjectDetails[6] = txtPComptDate.Text;
                    aProjectDetails[7] = txtProjectadditem.Text;
                    aProjectDetails[8] = txtProjectEditor.SelectedValue;

                    if (txtStartPProof.Text.Trim() != "") aProjectDetails[9] = DateTime.Parse(txtStartPProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[9] = "";
                    if (txtDuePProof.Text.Trim() != "") aProjectDetails[10] = DateTime.Parse(txtDuePProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[10] = "";
                    if (txtHalfPProof.Text.Trim() != "") aProjectDetails[11] = DateTime.Parse(txtHalfPProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[11] = "";
                    if (txtDispPProof.Text.Trim() != "") aProjectDetails[13] = DateTime.Parse(txtDispPProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[13] = "";
                    aProjectDetails[12] = dropEmpPProof.SelectedValue;

                    if (txtStartFirst.Text.Trim() != "") aProjectDetails[14] = DateTime.Parse(txtStartFirst.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[14] = "";
                    if (txtDueFirst.Text.Trim() != "") aProjectDetails[15] = DateTime.Parse(txtDueFirst.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[15] = "";
                    if (txtHalfFirst.Text.Trim() != "") aProjectDetails[16] = DateTime.Parse(txtHalfFirst.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[16] = "";
                    if (txtDispFirst.Text.Trim() != "") aProjectDetails[18] = DateTime.Parse(txtDispFirst.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[18] = "";
                    aProjectDetails[17] = dropEmpFirst.SelectedValue;

                    if (txtStartSecond.Text.Trim() != "") aProjectDetails[19] = DateTime.Parse(txtStartSecond.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[19] = "";
                    if (txtDueSecond.Text.Trim() != "") aProjectDetails[20] = DateTime.Parse(txtDueSecond.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[20] = "";
                    if (txtHalfSecond.Text.Trim() != "") aProjectDetails[21] = DateTime.Parse(txtHalfSecond.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[21] = "";
                    if (txtDispSecond.Text.Trim() != "") aProjectDetails[23] = DateTime.Parse(txtDispSecond.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[23] = "";
                    aProjectDetails[22] = dropEmpSecond.SelectedValue;

                    if (txtStartThird.Text.Trim() != "") aProjectDetails[24] = DateTime.Parse(txtStartThird.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[24] = "";
                    if (txtDueThird.Text.Trim() != "") aProjectDetails[25] = DateTime.Parse(txtDueThird.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[25] = "";
                    if (txtHalfThird.Text.Trim() != "") aProjectDetails[26] = DateTime.Parse(txtHalfThird.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[26] = "";
                    if (txtDispThird.Text.Trim() != "") aProjectDetails[28] = DateTime.Parse(txtDispThird.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[28] = "";
                    aProjectDetails[27] = dropEmpThird.SelectedValue;

                    if (txtStartFProof.Text.Trim() != "") aProjectDetails[29] = DateTime.Parse(txtStartFProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[29] = "";
                    if (txtDueFProof.Text.Trim() != "") aProjectDetails[30] = DateTime.Parse(txtDueFProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[30] = "";
                    if (txtHalfFProof.Text.Trim() != "") aProjectDetails[31] = DateTime.Parse(txtHalfFProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[31] = "";
                    if (txtDispFProof.Text.Trim() != "") aProjectDetails[32] = DateTime.Parse(txtDispFProof.Text.Trim()).ToString("MM/dd/yyyy");
                    else aProjectDetails[32] = "";

                    //aProjectDetails[32] = dropEmpFinal.SelectedValue;

                    aProjectDetails[33] = txtInvoiceDate.Text;
                    aProjectDetails[34] = txtInvoiceNo.Text;
                    aProjectDetails[35] = dropProdCode.SelectedValue;
                    aProjectDetails[36] = DropTypeset.SelectedValue;
                    aProjectDetails[37] = dropCurStatus.SelectedValue;
                    aProjectDetails[38] = DropCurStage.SelectedValue;
                    aProjectDetails[39] = DropEmpnameMain.SelectedValue;
                    aProjectDetails[40] = DropDepartment.SelectedValue;
                    aProjectDetails[41] = txtAddCharge.Text;
                    aProjectDetails[42] = txtPONumber.Text;
                    aProjectDetails[43] = txtCrditCost.Text;//
                    aProjectDetails[44] = txtProjectCost.Text;//
                    aProjectDetails[45] = dropInput.SelectedValue;
                    aProjectDetails[46] = DropOutPut.SelectedValue;
                    aProjectDetails[47] = txtNoCharater.Text;
                    aProjectDetails[48] = txtISBN.Text;
                    aProjectDetails[49] = DropCrdInd.SelectedValue;
                    aProjectDetails[50] = DropCrd.SelectedValue;
                    aProjectDetails[51] = txtChargedPages.Text;
                    aProjectDetails[52] = txtChargedArticle.Text;
                    aProjectDetails[53] = DropDigitalProd.SelectedValue;//
                    aProjectDetails[54] = txtProjectNumber.Text;
                    aProjectDetails[55] = txtPages.Text;
                    aProjectDetails[56] = txtcost1.Text;
                    aProjectDetails[57] = txtQty1.Text;
                    aProjectDetails[58] = txtPrice1.Text;
                    aProjectDetails[59] = txtcost2.Text;
                    aProjectDetails[60] = txtQty2.Text;
                    aProjectDetails[61] = txtPrice2.Text;
                    aProjectDetails[62] = txtcost3.Text;
                    aProjectDetails[63] = txtQty3.Text;
                    aProjectDetails[64] = txtPrice3.Text;
                    aProjectDetails[65] = txtCost4.Text;
                    aProjectDetails[66] = txtQty4.Text;
                    aProjectDetails[67] = txtPrice4.Text;
                    aProjectDetails[68] = txtCost5.Text;
                    aProjectDetails[69] = txtQty5.Text;
                    aProjectDetails[70] = txtPrice5.Text;
                    aProjectDetails[71] = rbWorkFrom.SelectedValue;
                    aProjectDetails[72] = rbTypeCost.SelectedValue;
                    aProjectDetails[73] = txtProjectDesc.Text;
                    aProjectDetails[74] = txtProjectComments.Text;
                    aProjectDetails[75] = drpCategory.SelectedValue.ToString();

                    string msg = this.oPro.UpdateProject(aProjectDetails);
                    if (msg.Contains("Project creation failed!") ||
                        msg.Contains("Project Already Exists!")) Alert(msg);
                    else
                    {

                        Alert("Successfully Saved.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToLower().Contains("cannot insert duplicate key"))
                Alert("Warning: duplicate records not allowed!");
            else Alert(ex.Message);
        }
        finally { aProjectDetails = null; }
    }
    protected void lnkProjectBarcode_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            loadBarcodeDetails(hfP_ID.Value);
        }
        this.showPanel(tabProjectBarcode);
    }
    protected void lnkProjectLinkUpload_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            loadArticleDetails(hfP_ID.Value);
        }
        this.showPanel(miProjectListUpload);
    }
    protected void btnProBarUpdate_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            string inproc = "SpupdateBarcodeStatus";
            string[,] pname ={
                    {"@PROJECTNO",hfP_ID.Value },{"@Stage",dropProStage.SelectedValue},{"@IsExist","OUTPUT"}};
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
    private void loadBarcodeDetails(string sProjectID)
    {
        sProjectID = sProjectID.Trim();
        if (sProjectID != "")
        {
            DataSet dsPro = oPro.getProjectDetailsByID(sProjectID);
            DataRow row = dsPro.Tables[0].Rows[0];
            txtProBarcode.Text = row["PBarcode"].ToString();
            txtProCat.Text = row["Pcode"].ToString();
        }
    }
    private void loadArticleDetails(string sProjectID)
    {
        sProjectID = sProjectID.Trim();
        if (sProjectID != "")
        {
            DataSet dsPro = oPro.getProjectArticleDetailsByID(sProjectID);
            gvExcelFile.DataSource = dsPro.Tables[0];
            //binding the gridview  
            gvExcelFile.DataBind();
        
        }
    }
    
    protected void cmd_New_Project_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Delphi_Job_Projects.aspx?q=new_Project", true);
    }
    protected void cmd_Excel_Export_Click(object sender, ImageClickEventArgs e)
    {
        if (gvProjects.Visible)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='12' align='center'><h4>Projects Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>CAT#</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>Recd. Date</b></td><td bgcolor='silver'><b>Due Date</b></td><td bgcolor='silver'><b>Despatch Datee</b></td><td bgcolor='silver'><b>Stage</b></td><td bgcolor='silver'><b>Digital Product</b></td><td bgcolor='silver'><b>Prod Code</b></td><td bgcolor='silver'><b>Typesetting Platform</b></td><td bgcolor='silver'><b>Barcode</b></td></tr>");
            int i = 1;
            foreach (DataRow r in dtProject.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["CUSTNAME"] + "</td>");
                sbData.Append("<td>" + r["pcode"] + "</td>");
                sbData.Append("<td>" + r["PTITLE"] + "</td>");
                sbData.Append("<td>" + r["PRECEIVEDDATE"] + "</td>");
                sbData.Append("<td>" + r["PDUEDATE"] + "</td>");
                sbData.Append("<td>" + r["PCOMPLETEDDATE"] + "</td>");
                sbData.Append("<td>" + r["STYPENAME"] + "</td>");
                sbData.Append("<td>" + r["PDIGITAL"] + "</td>");
                sbData.Append("<td>" + r["PRODCODE"] + "</td>");
                sbData.Append("<td>" + r["TPLATCODE"] + "</td>");
                sbData.Append("<td>" + r["PBARCODE"] + "</td>");
                sbData.Append("</tr>");
                i = i + 1;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Projects_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            Response.Write(sbData.ToString());
            Response.End();
        }
    }
    protected void gvProjects_Sorting(object sender, GridViewSortEventArgs e)
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
            if (sortDirection.Equals(SortDirection.Descending)) di = " desc"; dv.Table = dtProject;
            dv.Sort = e.SortExpression + di;
            sSortExpression = e.SortExpression;
            gvProjects.DataSource = dv;
            gvProjects.DataBind();
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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DataSet dscustPE = oPro.getPEName();
        txtProjectEditor.DataSource = dscustPE;
        txtProjectEditor.DataTextField = dscustPE.Tables[0].Columns[1].ToString();
        txtProjectEditor.DataValueField = dscustPE.Tables[0].Columns[0].ToString();
        txtProjectEditor.DataBind();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
		try
        {
        string ConStr = "";
        string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
        string path = @"\\192.9.201.222\Mail\LaunchFiles\" + FileUpload1.FileName;
        if (ext.Trim() == ".xls")
        {
            ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

        }
        else if (ext.Trim() == ".xlsx")
        {
            ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        }

        FileUpload1.SaveAs(path);
        Label7.Text = FileUpload1.FileName + "\'s Data showing into the GridView";
        string query = "SELECT * FROM [Sheet1$]";

        OleDbConnection conn = new OleDbConnection(ConStr);

        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        OleDbCommand cmd = new OleDbCommand(query, conn);
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        DataSet ds = new DataSet();

        da.Fill(ds);

            DataSet dss = new DataSet();
            DataTable dTable = new DataTable();
            dTable.Columns.Add("SLNo");
            dTable.Columns.Add("ARTICLEID");
            dTable.Columns.Add("PAGES");
            dTable.Columns.Add("RECEIVED");
            dTable.Columns.Add("DUE");
            dTable.Columns.Add("TASK");
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                dTable.Rows.Add(ds.Tables[0].Rows[j]["SLNo"].ToString(), ds.Tables[0].Rows[j]["ARTICLEID"].ToString(),
                ds.Tables[0].Rows[j]["PAGES"].ToString(), "", "", 0);
            }
            dss.Tables.Add(dTable);
            //set data source of the grid view  
            gvExcelFile.DataSource = dss.Tables[0];
            //binding the gridview  
            gvExcelFile.DataBind();
            //close the connection  
            cmd.Dispose();
            ds.Dispose();
            da.Dispose();
            conn.Close();
            conn.Dispose();
            File.Delete(path);
        }
        catch(Exception ex)
        {

        }
    }

    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        string ts = gvExcelFile.ClientID.ToString();
        string str = "gridFillValues.aspx?recv=" + ts;
        Type type = base.GetType();
        this.Page.ClientScript.RegisterStartupScript(type, "dateSrpt", "<script language=\"javascript\">window.open('" + str + "','SportZip',toolbar='0,scrollbars=-1,resizable=1,left=250,top=200,width=300, height=150,fullscreen=no');void(0);</script>");
        string s = "temp";
    }

    protected void SelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvExcelFile.HeaderRow.FindControl("selectALL");
        foreach (GridViewRow row in gvExcelFile.Rows)
        {
            HtmlInputCheckBox ChkBoxRows = (HtmlInputCheckBox)row.FindControl("chkFillAll");
            TextBox txtRev = (TextBox)row.FindControl("txtRecDate");
            TextBox txtDue = (TextBox)row.FindControl("txtDueDate");
            DropDownList drpTsk = (DropDownList)row.FindControl("drpTask");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
                txtDue.Text = "";
                txtRev.Text = "";
                drpTsk.SelectedIndex = 0;

            }
        }
    }
    protected void saveArticle_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            int test = 0;
            string sConStr = ConfigurationManager.ConnectionStrings["conStrSQLEmp"].ConnectionString;
            SqlConnection oConn = new SqlConnection(sConStr);
            SqlCommand cmd = new SqlCommand();
            oConn.Open();
            string statement = "";
            foreach (GridViewRow grw in gvExcelFile.Rows)
            {
                string jourId = ((Label)grw.FindControl("lblJourID")).Text.Trim().ToString();
                string pages = ((TextBox)grw.FindControl("lblPages")).Text.Trim().ToString();
                string recDt = ((TextBox)grw.FindControl("txtRecDate")).Text.Trim().ToString();
                string dueDt = ((TextBox)grw.FindControl("txtDueDate")).Text.Trim().ToString();
                string tsk = ((DropDownList)grw.FindControl("drpTask")).SelectedValue.ToString();
                HtmlInputCheckBox chkFill = (HtmlInputCheckBox)grw.FindControl("chkFillAll");
                cmd.CommandText = "select JOURNAL_ID from EP_PRO_MODULE where JOURNAL_ID='" + jourId + "' and PROJECTNO='"+hfP_ID.Value+"'";
                cmd.Connection = oConn;
                cmd.CommandType = CommandType.Text;
                string validate = (string)cmd.ExecuteScalar();
                if (validate == null)
                {
                    if (chkFill.Checked == true)
                    {
                        statement = string.Format("insert into EP_PRO_MODULE ( JOURNAL_ID, PAGES, RECEIVED,DUE,TASK,PROJECTNO) values ('{0}','{1}','{2}','{3}','{4}','{5}')", jourId, pages, recDt, dueDt, tsk, hfP_ID.Value);
                        cmd.CommandText = statement;
                        cmd.Connection = oConn;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        chkFill.Checked = false;
                        test++;
                    }
                }
                else
                {
                    if (chkFill.Checked == true)
                    {
                    cmd.CommandText = "update EP_PRO_MODULE set PAGES='" + pages + "', RECEIVED='" + recDt + "',DUE='" + dueDt + "',TASK='"
                        + tsk + "' where JOURNAL_ID='" + jourId + "' and PROJECTNO='" + hfP_ID.Value + "'";
                    cmd.Connection = oConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    chkFill.Checked = false;
                    test++;
                    }
                }
            }
            oConn.Close();
            cmd.Dispose();
            if (test != 0)
            {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Inserted successfully.');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Select atleast one Row');</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please select project');</script>");
        }
    }

    protected void gvExcelFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            if (e.CommandName == "imgdelete")
            {
                try
                {
                    string sConStr = ConfigurationManager.ConnectionStrings["conStrSQLEmp"].ConnectionString;
                    SqlConnection oConn = new SqlConnection(sConStr);
                    SqlCommand cmd = new SqlCommand();
                    oConn.Open();
                    GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
                    string jourId = ((Label)row.FindControl("lblJourID")).Text.Trim().ToString();
                    cmd.CommandText = "select JOURNAL_ID from EP_PRO_MODULE where JOURNAL_ID='" + jourId + "' and PROJECTNO='" + hfP_ID.Value + "'";
                    cmd.Connection = oConn;
                    cmd.CommandType = CommandType.Text;
                    string validate = (string)cmd.ExecuteScalar();
                    if (validate == null)
                    {
                        gvExcelFile.Rows[row.DataItemIndex].Visible = false;
                    }
                    else
                    {
                        cmd.CommandText = "delete EP_PRO_MODULE where JOURNAL_ID='" + jourId + "' and PROJECTNO='" + hfP_ID.Value + "'";
                        cmd.Connection = oConn;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        oConn.Close();
                        //gvExcelFile.Rows[row.DataItemIndex].Visible = false;
                    }
                    loadArticleDetails(hfP_ID.Value);
                }
                catch (Exception ex)
                {
                    string s = ex.ToString();
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please select project');</script>");
        }
        

    }
}
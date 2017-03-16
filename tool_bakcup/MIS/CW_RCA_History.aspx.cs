using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class CW_RCA_History : System.Web.UI.Page
{
    QSMSAMProfile cw = new QSMSAMProfile();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadGrid();
        }
    }
    public void loadGrid()
    {
        DataSet ds = new DataSet();
        ds = cw.getJournalsByCW(txtSearch.Text);
        if (Session["employeeid"].ToString() == "978" || Session["employeeid"].ToString() == "2638" || Session["employeeid"].ToString() == "2258" || Session["employeeid"].ToString() == "2461" || Session["employeeid"].ToString() == "1847" || Session["employeeid"].ToString() == "1074" || Session["employeeid"].ToString() == "2056")
        {
            gv_CWDetails.DataSource = ds;
            gv_CWDetails.DataBind();
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
    protected void gv_CWDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_CWDetails.EditIndex = -1;
        loadGrid();
    }
    protected void gv_CWDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Add"))
        {
            TextBox JournalName = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddName");
            TextBox NoofIssueYear = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddNoIssue");
            TextBox JournalTitle = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddTitle");
            TextBox ISSNPrint = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddPrint");
            TextBox ISSNOnline = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddOnline");
            TextBox PubMonth = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddPubMonth");
            TextBox NoofPages = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddNoofPages");
            TextBox TrimSize = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddTrimSize");
            TextBox WorkFlow = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddWorkFlow");
            TextBox Online_Printed = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddOnlinePrinted");
            TextBox Color = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddColor");
            TextBox Template = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddTemplate");
            TextBox SplMaterial = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddSplMaterial");
            TextBox TempLead = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddTempLead");
            TextBox Copyright = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddCopyright");
            TextBox Copyedit = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddCopyedit");
            TextBox Printed = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddPrinted");
            TextBox DTD = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddDTD");
            TextBox DTDUsed = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddDTDUsed");
            TextBox ProofStage = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddProofStage");
            TextBox Atypon = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddAtypon");
            TextBox Software = (TextBox)gv_CWDetails.FooterRow.FindControl("txtAddSoftware");


            gv_CWDetails.EditIndex = -1;
            string inproc = "";
            inproc = "SpInsertCW_QMS_Excel";

            string[,] param ={
                    {"JournalName",JournalName.Text},{"NoofIssueYear",NoofIssueYear.Text },{"JournalTitle",JournalTitle.Text},
                    {"ISSNPrint",ISSNPrint.Text},{"ISSNOnline",ISSNOnline.Text },{"PubMonth",PubMonth.Text},
                    {"NoofPages",NoofPages.Text},{"TrimSize",TrimSize.Text },{"WorkFlow",WorkFlow.Text},
                    {"Online_Printed",Online_Printed.Text},{"Color",Color.Text },{"Template",Template.Text},
                    {"SplMaterial",SplMaterial.Text},{"TempLead",TempLead.Text },{"Copyright",Copyright.Text},
                    {"Copyedit",Copyedit.Text},{"Printed",Printed.Text },{"DTD",DTD.Text},
                    {"DTDUsed",DTDUsed.Text},{"ProofStage",ProofStage.Text },{"Atypon",Atypon.Text},
                    {"Software",Software.Text},{"Empid",""}
                    };
            int val;
                val = cw.ExcSProcedure(inproc, param, CommandType.StoredProcedure);
            loadGrid();
        }
    }

    protected void gv_CWDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_CWDetails.EditIndex = e.NewEditIndex;
        loadGrid();
    }

    protected void gv_CWDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox JournalName = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtName");
        TextBox NoofIssueYear = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtNoIssue");
        TextBox JournalTitle = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtTitle");
        TextBox ISSNPrint = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtPrint");
        TextBox ISSNOnline = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtOnline");
        TextBox PubMonth = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtPubMonth");
        TextBox NoofPages = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtNoofPages");
        TextBox TrimSize = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtTrimSize");
        TextBox WorkFlow = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtWorkFlow");
        TextBox Online_Printed = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtOnlinePrinted");
        TextBox Color = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtColor");
        TextBox Template = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtTemplate");
        TextBox SplMaterial = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtSplMaterial");
        TextBox TempLead = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtTempLead");
        TextBox Copyright = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtCopyright");
        TextBox Copyedit = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtCopyedit");
        TextBox Printed = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtPrinted");
        TextBox DTD = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtDTD");
        TextBox DTDUsed = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtDTDUsed");
        TextBox ProofStage = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtProofStage");
        TextBox Atypon = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtAtypon");
        TextBox Software = (TextBox)gv_CWDetails.Rows[e.RowIndex].FindControl("txtSoftware");
        
        string inproc = "";
        inproc = "SpUpdateCW_QMS_Excel";

        string[,] param ={
                    {"JournalName",JournalName.Text},{"NoofIssueYear",NoofIssueYear.Text },{"JournalTitle",JournalTitle.Text},
                    {"ISSNPrint",ISSNPrint.Text},{"ISSNOnline",ISSNOnline.Text },{"PubMonth",PubMonth.Text},
                    {"NoofPages",NoofPages.Text},{"TrimSize",TrimSize.Text },{"WorkFlow",WorkFlow.Text},
                    {"Online_Printed",Online_Printed.Text},{"Color",Color.Text },{"Template",Template.Text},
                    {"SplMaterial",SplMaterial.Text},{"TempLead",TempLead.Text },{"Copyright",Copyright.Text},
                    {"Copyedit",Copyedit.Text},{"Printed",Printed.Text },{"DTD",DTD.Text},
                    {"DTDUsed",DTDUsed.Text},{"ProofStage",ProofStage.Text },{"Atypon",Atypon.Text},
                    {"Software",Software.Text},{"Empid",""}
                    };
        int val;
        val = cw.ExcSProcedure(inproc, param, CommandType.StoredProcedure);
        loadGrid();
        gv_CWDetails.EditIndex = -1;
        loadGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadGrid();
    }
}
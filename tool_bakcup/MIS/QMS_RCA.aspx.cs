using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

public partial class QMS_RCA : System.Web.UI.Page
{
    SqlConnection scon =new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString);
    int iEmpId = 0;
    private datasourceSQL oSql;
    private string sSql = "";
    string aEmployeeID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            divPanel.Visible = false;
            bindGrid();
            divHome.Visible = true;
        }
       
        //DataSet dsa = Access_Validation(Convert.ToInt32(Session["employeeid"]));



        if ((Convert.ToInt32(Session["employeeid"]) == 2461 ||  Convert.ToInt32(Session["employeeid"]) == 2432 || Convert.ToInt32(Session["employeeid"]) == 2416 || Convert.ToInt32(Session["employeeid"]) == 2056 || Convert.ToInt32(Session["employeeid"]) == 1732 || Convert.ToInt32(Session["employeeid"]) == 2016 || Convert.ToInt32(Session["employeeid"]) == 978 || Convert.ToInt32(Session["employeeid"]) == 1074 || Convert.ToInt32(Session["employeeid"]) == 1847 || Convert.ToInt32(Session["employeeid"]) == 2258))
        {
           
            lnkUpload.Visible = true;
            lnkReport.Visible = true;
            lnkResponse.Visible = true;
        }
        else
        {
            lnkUpload.Visible = false;
            lnkReport.Visible = false;
            lnkResponse.Visible = false;
        }
        
    }

    public DataSet Access_Validation(int empid)
    {
        scon.Open();
        SqlCommand gcmd = new SqlCommand("Select Employee_Id from  tbQMS_Access_Det Where Active_Status=1 and Employee_Id="+empid, scon);
        SqlDataAdapter da = new SqlDataAdapter(gcmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public void Upload()
    {
        string connectionString = "";
        int count = 0;

        if (fileBrowse.HasFile)
        {
            //string fileName = Path.GetFileName(fileBrowse.PostedFile.FileName);
            //string fileExtension = Path.GetExtension(fileBrowse.PostedFile.FileName);
            //string fileLocation = Server.MapPath(fileName);
            string fileName = Path.GetFileName(fileBrowse.PostedFile.FileName);
            string fileExtension = Path.GetExtension(fileBrowse.PostedFile.FileName);
            string fileLocation = @"\\192.9.201.222\db\QMS\" + Path.GetFileNameWithoutExtension(fileName) + Convert.ToString(DateTime.Now).Replace("/", "_").Replace(":", "_") + "." + fileExtension;
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = con;
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
            DataTable dtExcelRecords = new DataTable();
            con.Open();
            string sheetname = "Feedback response form";
            DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string getExcelSheetName = string.Empty;
            // if(dtExcelSheetName.Rows[0]["Table_Name"].ToString()=="Sheet1")
            getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
            //else
            // getExcelSheetName = dtExcelSheetName.Rows[2]["Table_Name"].ToString();

            cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
            dAdapter.SelectCommand = cmd;
            dAdapter.Fill(dtExcelRecords);


            SqlCommand cm = new SqlCommand();
            for (int i = 0; i < dtExcelRecords.Rows.Count; i++)
            {
                try
                {
                    scon.Close();
                    scon.Open();
                    string desc = dtExcelRecords.Rows[i][11].ToString();
                    desc = desc.Replace('.', '~');

                    string root = dtExcelRecords.Rows[i][16].ToString();
                    root = root.Replace('.', '~');


                    string Supplier_actions = dtExcelRecords.Rows[i][19].ToString();
                    Supplier_actions = Supplier_actions.Replace('.', '~');

                    SqlCommand cmds = new SqlCommand("Sp_Insert_QMS", scon);
                    cmds.CommandType = CommandType.StoredProcedure;
                    cmds.Parameters.Add("@Issue_ID", SqlDbType.Int).Value = dtExcelRecords.Rows[i][0];
                    cmds.Parameters.Add("@Prod_Staff", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][1];
                    cmds.Parameters.Add("@Prod_Manager", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][2];
                    cmds.Parameters.Add("@Journal", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][3];
                    cmds.Parameters.Add("@Manuscript_Issues", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][4];
                    cmds.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][5];

                    cmds.Parameters.Add("@Freelance_CE", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][6];
                    cmds.Parameters.Add("@Date_Feedback_Log", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][7];
                    cmds.Parameters.Add("@Type_Problem", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][8];
                    cmds.Parameters.Add("@SOP", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][9];
                    cmds.Parameters.Add("@WorkFlow", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][10];
                    cmds.Parameters.Add("@Description", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][11];
                    cmds.Parameters.Add("@Shortdetails", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][12];

                    cmds.Parameters.Add("@Assign_category", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][13];
                    cmds.Parameters.Add("@Multiple_category", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][14];
                    cmds.Parameters.Add("@Problem_occurred", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][15];
                    cmds.Parameters.Add("@Root_causes", SqlDbType.VarChar).Value = root;
                    cmds.Parameters.Add("@Nature_problem_Single", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][17];

                    cmds.Parameters.Add("@Nature_problem_Multiple", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][18];
                    cmds.Parameters.Add("@Supplier_actions", SqlDbType.VarChar).Value = Supplier_actions;
                    cmds.Parameters.Add("@Completion_date", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][20];
                    cmds.Parameters.Add("@Name_role", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][21];
                    cmds.Parameters.Add("@Is_issue", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][22];
                    cmds.Parameters.Add("@interim", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][23];
                    cmds.Parameters.Add("@Projected_final", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][24];
                    cmds.Parameters.Add("@FollowUp", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][25];

                    cmds.Parameters.Add("@Date_RCA", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][26];
                    cmds.Parameters.Add("@TandF1", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][27];
                    cmds.Parameters.Add("@TandF2", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][28];
                    cmds.Parameters.Add("@TandF3", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][29];
                    cmds.Parameters.Add("@TandF4", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][30];
                    cmds.Parameters.Add("@TandF5", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][31];

                    cmds.Parameters.Add("@TandF_Comments1", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][32];
                    cmds.Parameters.Add("@TandF_Comments2", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][33];
                    cmds.Parameters.Add("@TandF_Comments3", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][34];
                    cmds.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = 1525;
                    cmds.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = DateTime.Now;


                    count += cmds.ExecuteNonQuery();
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
            if (count > 1)
            {
                SqlCommand gcmd = new SqlCommand("Select Upload_Id Upload_Id,Issue_ID [Issue ID],Prod_Staff[Production Staff]," +
                                               " Prod_Manager[Production Manager], Journal, Manuscript_Issues[Manuscript / Issue], Supplier[Supplier]," +
                                                "Freelance_CE[(If Freelance CE, please give name)  N/A],Convert(varchar,Date_Feedback_Log,101)[Date feedback logged]," +
                                                "Type_Problem[Type of problem],SOP[Severity of problem]," +
                                                "WorkFlow[Workflow],Replace(Description,'~','.')[Description (also identify TS copy-editor, typesetter, collator, other contacts)],Root_causes,FolloWUp from " +
                                                " tbQMS_Excel_History order by CreatedOn Desc", scon);
                SqlDataAdapter da = new SqlDataAdapter(gcmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                gvCustomerList.DataSource = ds;
                gvCustomerList.DataBind();

            }
            else
            {

            }

        }

        //String strConnection = @"Data Source=DPI154\SQLEXPRESS;Initial Catalog=Old_11042013;Integrated Security=True";
        //string path = fileuploadExcel.PostedFile.FileName;
        //string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
        //OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
        //OleDbCommand cmd = new OleDbCommand("Select [Issue ID],[Production Staff],[Journal],[Manuscript / Issue],[Type of problem],[Severity of problem],[Workflow],[Description of Feedback] from [Sheet1]", excelConnection);
        //excelConnection.Open();
        //OleDbDataReader dReader;
        //dReader = cmd.ExecuteReader();
        //SqlBulkCopy sqlBulk = new SqlBulkCopy(strConnection);
        //sqlBulk.DestinationTableName = "tbQMS_Excel_History";
        //sqlBulk.WriteToServer(dReader);
        //excelConnection.Close();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Upload();
    }
    protected void gvCustomerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (Session["SearchActive"] != null)
        {
            gvCustomerList.PageIndex = e.NewPageIndex;
            gvCustomerList.DataSource = Session["SearchActive"];
            gvCustomerList.DataBind();
            divPanel.Visible = false;
        }
        else
        {
            gvCustomerList.PageIndex = e.NewPageIndex;
            bindGrid();
            divPanel.Visible = false;
        }
    }
    public void bindGrid()
    {
        try
        {
            scon.Close();
            scon.Open();
            SqlCommand gcmd = new SqlCommand("Select Upload_Id ,Issue_ID [Issue ID],Prod_Staff[Production Staff]," +
                                                  " Prod_Manager[Production Manager], Journal, Manuscript_Issues[Manuscript / Issue], Supplier[Supplier]," +
                                                   "Freelance_CE,Convert(varchar,Date_Feedback_Log,101)[Date feedback logged]," +
                                                   "Type_Problem [Type of problem],SOP[Severity of problem]" +
                                                   ", WorkFlow[Workflow],Replace(Description,'~','.')Description,Root_causes from " +
                                                   " tbQMS_Excel_History order by Date_Feedback_Log Desc", scon);
            SqlDataAdapter da = new SqlDataAdapter(gcmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            gvCustomerList.DataSource = ds;
            gvCustomerList.DataBind();
        }
        catch (Exception Ex)
        {
            // continue;
        }
        finally
        {
            scon.Close();
        }
    }
    protected void gvCustomerList_RowDataBound(object sender, GridViewRowEventArgs e)
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


    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {

            case "divTypeset":

                miTypeSet.Attributes.Add("class", "current");
                miClientFeedback.Attributes.Add("class", "");
                miFeedbackUpload.Attributes.Add("class", "");
                miReports.Attributes.Add("class", "");
                this.divInternal.Visible = false;
                this.divTypeset.Visible = true;
                this.divClientFeedback.Visible = false;
                this.divUpload.Visible = false;
                this.divReports.Visible = false;
                break;
            case "divClientFeedback":

                miTypeSet.Attributes.Add("class", "");
                miClientFeedback.Attributes.Add("class", "current");
                miFeedbackUpload.Attributes.Add("class", "");
                miReports.Attributes.Add("class", "");
                this.divInternal.Visible = false;
                this.divTypeset.Visible = false;
                this.divClientFeedback.Visible = true;
                this.divUpload.Visible = false;
                this.divReports.Visible = false;
                break;
            case "divUpload":

                miTypeSet.Attributes.Add("class", "");
                miClientFeedback.Attributes.Add("class", "");
                miFeedbackUpload.Attributes.Add("class", "current");
                miReports.Attributes.Add("class", "");
                this.divInternal.Visible = false;
                this.divTypeset.Visible = false;
                this.divClientFeedback.Visible = false;
                this.divUpload.Visible = true;
                this.divReports.Visible = false;
                break;
            case "divReports":

                miTypeSet.Attributes.Add("class", "");
                miClientFeedback.Attributes.Add("class", "");
                miFeedbackUpload.Attributes.Add("class", "");
                miReports.Attributes.Add("class", "current");
                this.divInternal.Visible = false;
                this.divTypeset.Visible = false;
                this.divClientFeedback.Visible = false;
                this.divUpload.Visible = false;
                this.divReports.Visible = true;
                break;
            default:

                miTypeSet.Attributes.Add("class", "current");
                miClientFeedback.Attributes.Add("class", "");
                miFeedbackUpload.Attributes.Add("class", "");
                miReports.Attributes.Add("class", "");
                this.divInternal.Visible = false;
                this.divTypeset.Visible = true;
                this.divClientFeedback.Visible = false;
                this.divUpload.Visible = false;
                this.divReports.Visible = false;
                break;
        }
    }


    protected void lnkFeedback_Click(object sender, EventArgs e)
    {
        showPanel(divTypeset);
    }
    protected void lnkResponse_Click(object sender, EventArgs e)
    {
        showPanel(divClientFeedback);
        DataSet ds1 = (DataSet)Session["Sorting"];
        grvSort.DataSource = ds1;
        grvSort.DataBind();
        grvSort.Visible = true;
        Session["Uploadid"] = ds1.Tables[0].Rows[0]["Upload_Id"].ToString();

    }
    protected void lnkFeedbackUpload_Click(object sender, EventArgs e)
    {
        showPanel(divUpload);
    }
    protected void lnkReports_Click(object sender, EventArgs e)
    {
        showPanel(divReports);
    }
    protected void gvCustomerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View" && Page.IsValid)
        {
            if (Session["PreviousRowIndex"] != null)
            {
                int previousRowIndex = (int)Session["PreviousRowIndex"];
                if (gvCustomerList.Rows.Count > 4)
                {
                    GridViewRow PreviousRow = gvCustomerList.Rows[previousRowIndex];
                    PreviousRow.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    if (previousRowIndex == 1 || previousRowIndex == 2 || previousRowIndex == 0)
                    {
                        GridViewRow PreviousRow = gvCustomerList.Rows[previousRowIndex];
                        PreviousRow.BackColor = System.Drawing.Color.White;
                    }
                }
            }

            GridViewRow row1 = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            row1.BackColor = System.Drawing.Color.MediumPurple;
            Session["PreviousRowIndex"] = row1.RowIndex;

            divPanel.Visible = true;
            showPanel(divInternal);

            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string IssueId = gvCustomerList.DataKeys[row.RowIndex].Values["Upload_Id"].ToString();
            DataSet ds = GetLiveFeedBackDetails(IssueId);
            Session["Sorting"] = ds;
            if (ds.Tables[0].Rows.Count >= 0)
            {
                ddlSupplier_point.SelectedItem.Text = ds.Tables[0].Rows[0]["Problem_type"].ToString();
                ddlAssign_category.SelectedItem.Text = ds.Tables[0].Rows[0]["Assign_category"].ToString();
                txtmultiplecategories.Text = ds.Tables[0].Rows[0]["Multiple_category"].ToString();
                ddlproblem_occurred.SelectedItem.Text = ds.Tables[0].Rows[0]["Problem_occurred"].ToString();
                txtroot_causes1.Text = ds.Tables[0].Rows[0]["Root_causes"].ToString();
                ddlNature_problem_Single.SelectedItem.Text = ds.Tables[0].Rows[0]["Nature_problem_Single"].ToString();
                txtmultiplecategories.Text = ds.Tables[0].Rows[0]["Nature_problem_Multiple"].ToString();
                txtSupplier_actions1.Text = ds.Tables[0].Rows[0]["Supplier_actions"].ToString();
                txtCompletionDate.Text = (ds.Tables[0].Rows[0]["Completion_date"]).ToString();
                ddlName_role_title.SelectedItem.Text = (ds.Tables[0].Rows[0]["Name_role"]).ToString();
                ddlIs_issue.SelectedItem.Text = ds.Tables[0].Rows[0]["Is_issue"].ToString();
                txtinterim.Text = ds.Tables[0].Rows[0]["interim"].ToString();
                txtFinalResolution.Text = ds.Tables[0].Rows[0]["Projected_final"].ToString();
                txtDescription1.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                lblDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                lblRoot_causes.Text = ds.Tables[0].Rows[0]["Root_causes"].ToString();
                lblSupplier_actions.Text = ds.Tables[0].Rows[0]["Supplier_actions"].ToString();
                dropFlowUp.SelectedItem.Text = ds.Tables[0].Rows[0]["Followup"].ToString();

                int uploadid = Convert.ToInt16(ds.Tables[0].Rows[0]["Upload_id"].ToString());
                if (uploadid > 290)
                {
                    lblFol.Visible = true;
                    dropFlowUp.Visible = true;
                    lblpro.Visible = false;
                    ddlSupplier_point.Visible = false;
                    lblcat.Text = "Re-categorized problem type, if not correct?";
                }
                else
                {
                    lblFol.Visible = false;
                    dropFlowUp.Visible = false;
                    lblpro.Visible = true;
                    ddlSupplier_point.Visible = true;
                    lblcat.Text = "If not, please assign a category from the list provided If single category, select here.If multiple categories, type categories from this list in the next cell.If 'Other', specify further details in the next cell.";
                }
            }
            
            EnableFalse();
        }

    }
    public DataSet GetLiveFeedBackDetails(string IssueId)
    {
        return GetDataSet("sp_QMS_Bind", IssueId, CommandType.StoredProcedure);
    }
    public DataSet GetLiveFeedBackDetailsAll()
    {
        return GetDataSet("sp_QMS_Bind", null, CommandType.StoredProcedure);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnEdit.Visible = false;
        btnUpdate.Visible = true;
        btnUpdate.Enabled = true;
        btnCancel.Enabled = true;
        ddlSupplier_point.Enabled = true;
        ddlAssign_category.Enabled = true;
        txtmultiplecategories.Enabled = true;
        ddlproblem_occurred.Enabled = true;
        txtroot_causes1.Enabled = true;
        ddlNature_problem_Single.Enabled = true;
        txtmultiplecategories.Enabled = true;
        txtSupplier_actions1.Enabled = true;
        txtCompletionDate.Enabled = true;
        ddlName_role_title.Enabled = true;
        txtDescription1.Enabled = true;
        ddlIs_issue.Enabled = true;
        txtinterim.Enabled = true;
        txtSingleCategory.Enabled = true;
        txtFinalResolution.Enabled = true;
        imgcal.Visible = true;
        dropFlowUp.Enabled = true;
    }
    public void EnableFalse()
    {
        ddlSupplier_point.Enabled = false;
        ddlAssign_category.Enabled = false;
        txtmultiplecategories.Enabled = false;
        ddlproblem_occurred.Enabled = false;
        txtroot_causes1.Enabled = false;
        ddlNature_problem_Single.Enabled = false;
        txtmultiplecategories.Enabled = false;
        txtSupplier_actions1.Enabled = false;
        txtCompletionDate.Enabled = false;
        ddlName_role_title.Enabled = false;
        ddlIs_issue.Enabled = false;
        txtinterim.Enabled = false;
        txtFinalResolution.Enabled = false;
        txtDescription1.Enabled = false;
        txtSingleCategory.Enabled = false;
        btnEdit.Visible = true;
        btnEdit.Enabled = true;
        btnUpdate.Enabled = false;
        btnUpdate.Visible = false;
        btnCancel.Enabled = false;
        imgcal.Visible = false;
        dropFlowUp.Enabled = false;
    }
    public void EnableTrue()
    {
        txtIssueId.Enabled = true;
        txtProdId.Enabled = true;
        txtProdMgr.Enabled = true;
        txtJournal.Enabled = true;
        txtManuscript.Enabled = true;
        txtSupplier.Enabled = true;
        txtReference.Enabled = true;
        txtFeedBack.Enabled = true;
        txtProblem.Enabled = true;
        txtServerity.Enabled = true;
        txtWorkFlow.Enabled = true;
        txtDescription1.Enabled = true;
        //txtDesc.Enabled = true;
        //txtRoot_causes.Enabled = true;
        txtSupplier.Enabled = true;
        txtWorkFlow.Enabled = true;

        txtSupplier.Enabled = true;

    }
    protected void imgbtnEventExport_Click(object sender, ImageClickEventArgs e)
    {
        DateTime fdate = Convert.ToDateTime(Txtsdate.Text);
        DateTime tdate = Convert.ToDateTime(Txtedate.Text);
        if (fdate > Convert.ToDateTime("10/31/2014"))
        {
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            lblEventsHeader.Text = "Feedback response form";
            Response.AddHeader("content-disposition", "attachment;filename=" + lblEventsHeader.Text + ".xls");
            this.EnableViewState = false;
            System.IO.StringWriter strwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
            HtmlForm htmlfrm = new HtmlForm();
            grdview2.Parent.Controls.Add(htmlfrm);
            htmlfrm.Attributes["runat"] = "server";
            htmlfrm.Controls.Add(grdview2);
            htmlfrm.RenderControl(txtwriter);
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(strwriter);
            Response.End();
        }
        else
        {
            if (tdate < Convert.ToDateTime("10/31/2014"))
            {
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.xls";
                lblEventsHeader.Text = "Feedback response form";
                Response.AddHeader("content-disposition", "attachment;filename=" + lblEventsHeader.Text + ".xls");
                this.EnableViewState = false;
                System.IO.StringWriter strwriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
                HtmlForm htmlfrm = new HtmlForm();
                grvrpt.Parent.Controls.Add(htmlfrm);
                htmlfrm.Attributes["runat"] = "server";
                htmlfrm.Controls.Add(grvrpt);
                htmlfrm.RenderControl(txtwriter);
                Response.ContentEncoding = Encoding.Unicode;
                Response.BinaryWrite(Encoding.Unicode.GetPreamble());
                Response.Write(strwriter);
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition","attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                Table tb = new Table();
                TableRow tr1 = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.Controls.Add(grvrpt);
                tr1.Cells.Add(cell1);
                TableCell cell3 = new TableCell();
                cell3.Controls.Add(grdview2);
                TableCell cell2 = new TableCell();
                cell2.Text = "&nbsp;";
                TableRow tr2 = new TableRow();
                tr2.Cells.Add(cell2);
                TableRow tr3 = new TableRow();
                tr3.Cells.Add(cell3);
                tb.Rows.Add(tr1);
                tb.Rows.Add(tr2);
                tb.Rows.Add(tr3);
                tb.RenderControl(hw);
                Response.ContentEncoding = Encoding.Unicode;
                Response.BinaryWrite(Encoding.Unicode.GetPreamble());
                Response.Write(sw);
                Response.Flush();
                Response.End();
            }
        }

    }


    protected void PrepareForExport(GridView Gridview)
    {
        Gridview.AllowPaging = Convert.ToBoolean(true);
        Gridview.DataBind();
        Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF");
        for (int k = 0; k < Gridview.HeaderRow.Cells.Count; k++)
        {
            Gridview.HeaderRow.Cells[k].Style.Add("background-color", "green");
        }
        for (int i = 0; i < Gridview.Rows.Count; i++)
        {
            GridViewRow row = Gridview.Rows[i];
            row.BackColor = System.Drawing.Color.White;
            row.Attributes.Add("class", "textmode");
            if (i % 2 != 0)
            {
                for (int j = 0; j < Gridview.Rows[i].Cells.Count; j++)
                {
                    row.Cells[j].Style.Add("background-color", "#C2D69B");
                }
            }
        }
    }

    private DataSet GetDataSet(string sProcedure, string sTitle, CommandType sCmdType)
    {
        try
        {
            scon.Close();
            scon.Open();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = scon;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "sp_QMS_Bind";
            oCmd.Parameters.Clear();
            addParamsLog(oCmd, "@Issueid", sTitle, SqlDbType.VarChar, 250, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "UserInfo");
            oCmd = null;
            return ds;

        }
        catch (Exception oEx)
        {
            return null;
        }
        finally
        {
            scon.Close();
        }
    }
    private void addParamsLog(SqlCommand oCmmd, string sName, string sValue, SqlDbType sDBType, int sLen, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType, sLen));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }
    protected void gvCustomerList_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvCustomerList.Rows)
        {
            if (row.RowIndex == gvCustomerList.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
        }
    }
    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        bindGrid();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string[] aFeedbacDet;
        aFeedbacDet = new string[16];
        aFeedbacDet[0] = ddlSupplier_point.SelectedItem.Text.Trim();
        aFeedbacDet[1] = ddlAssign_category.SelectedItem.Text.Trim();
        aFeedbacDet[2] = txtmultiplecategories.Text.Trim();
        aFeedbacDet[3] = ddlproblem_occurred.SelectedItem.Text.Trim();
        aFeedbacDet[4] = txtroot_causes1.Text.Trim();
        aFeedbacDet[5] = ddlNature_problem_Single.SelectedItem.Text.Trim();
        aFeedbacDet[6] = txtmultiplecategories.Text.Trim();
        aFeedbacDet[7] = txtSupplier_actions1.Text.Trim();
        aFeedbacDet[8] = txtCompletionDate.Text.Trim();
        aFeedbacDet[9] = ddlName_role_title.SelectedItem.Text.Trim();
        aFeedbacDet[10] = ddlIs_issue.SelectedItem.Value.Trim();
        aFeedbacDet[11] = txtinterim.Text.Trim();
        aFeedbacDet[12] = txtFinalResolution.Text.Trim();
        aFeedbacDet[13] = Session["Uploadid"].ToString();
        aFeedbacDet[14] = txtDescription1.Text.Trim();
        aFeedbacDet[15] = dropFlowUp.SelectedItem.Text.Trim();


        int i = UpdateArticle(aFeedbacDet);
        if (i > 0)
        {
            Alert("Successfully Updated");
            EnableFalse();
        }
        else
        {
            Alert("Updated Failed.");
        }

    }
    public int UpdateArticle(string[] aFeedbacDet)
    {
        int Status = 0;
        try
        {

            SqlCommand cmdsu = new SqlCommand("Sp_Update_QMS", scon);
            cmdsu.CommandType = CommandType.StoredProcedure;
            cmdsu.Parameters.Add("@Problem_type", SqlDbType.VarChar).Value = aFeedbacDet[0];
            cmdsu.Parameters.Add("@Assign_category", SqlDbType.VarChar).Value = aFeedbacDet[1];
            cmdsu.Parameters.Add("@Multiple_category", SqlDbType.VarChar).Value = aFeedbacDet[2];
            cmdsu.Parameters.Add("@Problem_occurred", SqlDbType.VarChar).Value = aFeedbacDet[3];
            cmdsu.Parameters.Add("@Root_causes", SqlDbType.VarChar).Value = aFeedbacDet[4];
            cmdsu.Parameters.Add("@Nature_problem_Single", SqlDbType.VarChar).Value = aFeedbacDet[5];
            cmdsu.Parameters.Add("@Nature_problem_Multiple", SqlDbType.VarChar).Value = aFeedbacDet[6];
            cmdsu.Parameters.Add("@Supplier_actions", SqlDbType.VarChar).Value = aFeedbacDet[7];
            cmdsu.Parameters.Add("@Completion_date", SqlDbType.VarChar).Value = aFeedbacDet[8];
            cmdsu.Parameters.Add("@Name_role", SqlDbType.VarChar).Value = aFeedbacDet[9];
            cmdsu.Parameters.Add("@Is_issue", SqlDbType.VarChar).Value = aFeedbacDet[10];
            cmdsu.Parameters.Add("@interim", SqlDbType.VarChar).Value = aFeedbacDet[11];
            cmdsu.Parameters.Add("@Projected_final", SqlDbType.VarChar).Value = aFeedbacDet[12];
            cmdsu.Parameters.Add("@UploadId", SqlDbType.Int).Value = aFeedbacDet[13];
            cmdsu.Parameters.Add("@Description", SqlDbType.VarChar).Value = aFeedbacDet[14];
            cmdsu.Parameters.Add("@FollowUp", SqlDbType.VarChar).Value = aFeedbacDet[15];
            scon.Open();
            Status = cmdsu.ExecuteNonQuery();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
        finally
        {
            scon.Close();
        }

        return Status;


    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //DataSet ds1 = (DataSet)Session["Sorting"];
            //if (ds1.Tables[0].Rows.Count >= 0)
            //{
            //    txtDescription1.Text = ds1.Tables[0].Rows[0]["Description"].ToString();
            //    ddlSupplier_point.Text = ds1.Tables[0].Rows[0]["Problem_type"].ToString();
            //    ddlAssign_category.SelectedItem.Text = ds1.Tables[0].Rows[0]["Assign_category"].ToString();
            //    txtmultiplecategories.Text = ds1.Tables[0].Rows[0]["Multiple_category"].ToString();
            //    ddlproblem_occurred.Text = ds1.Tables[0].Rows[0]["Problem_occurred"].ToString();
            //    txtroot_causes1.Text = ds1.Tables[0].Rows[0]["Root_causes"].ToString();
            //    ddlNature_problem_Single.SelectedItem.Text = ds1.Tables[0].Rows[0]["Nature_problem_Single"].ToString();
            //    txtmultiplecategories.Text = ds1.Tables[0].Rows[0]["Nature_problem_Multiple"].ToString();
            //    txtSupplier_actions1.Text = ds1.Tables[0].Rows[0]["Supplier_actions"].ToString();
            //    txtCompletionDate.Text = ds1.Tables[0].Rows[0]["Completion_date"].ToString();
            //    ddlName_role_title.SelectedItem.Text = ds1.Tables[0].Rows[0]["Name_role"].ToString();
            //    ddlIs_issue.SelectedItem.Text = ds1.Tables[0].Rows[0]["Is_issue"].ToString();
            //    txtinterim.Text = ds1.Tables[0].Rows[0]["interim"].ToString();
            //    txtFinalResolution.Text = ds1.Tables[0].Rows[0]["Projected_final"].ToString();

            //}
            EnableFalse();
            divPanel.Visible = false;
            
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime fdate = Convert.ToDateTime(Txtsdate.Text);
            DateTime tdate = Convert.ToDateTime(Txtedate.Text);

            scon.Close();
            DataSet rptDs = new DataSet();
            DataSet rptDs1 = new DataSet();
            scon.Open();
            

            if (fdate > Convert.ToDateTime("10/31/2014"))
            {
                SqlCommand cmdsu = new SqlCommand("sp_QMS_FeedbackReports", scon);
                cmdsu.CommandType = CommandType.StoredProcedure;
                cmdsu.Parameters.Add("@sdate", SqlDbType.VarChar).Value = Txtsdate.Text + " 00:00:00";
                cmdsu.Parameters.Add("@edate", SqlDbType.VarChar).Value = Txtedate.Text + " 23:59:59";
                cmdsu.ExecuteNonQuery();
                SqlDataAdapter daRpt = new SqlDataAdapter(cmdsu);
                daRpt.Fill(rptDs);
                grdview2.DataSource = rptDs;
                grdview2.DataBind();
            }
            else
            {
                if (tdate < Convert.ToDateTime("10/31/2014"))
                {
                    SqlCommand cmdsu = new SqlCommand("sp_QMS_FeedbackReports", scon);
                    cmdsu.CommandType = CommandType.StoredProcedure;
                    cmdsu.Parameters.Add("@sdate", SqlDbType.VarChar).Value = Txtsdate.Text + " 00:00:00";
                    cmdsu.Parameters.Add("@edate", SqlDbType.VarChar).Value = Txtedate.Text + " 23:59:59";
                    cmdsu.ExecuteNonQuery();
                    SqlDataAdapter daRpt = new SqlDataAdapter(cmdsu);
                    daRpt.Fill(rptDs);
                    grvrpt.DataSource = rptDs;
                    grvrpt.DataBind();
                }
                else
                {
                    SqlCommand cmdsu = new SqlCommand("sp_QMS_FeedbackReports", scon);
                    cmdsu.CommandType = CommandType.StoredProcedure;
                    cmdsu.Parameters.Add("@sdate", SqlDbType.VarChar).Value = Txtsdate.Text + " 00:00:00";
                    cmdsu.Parameters.Add("@edate", SqlDbType.VarChar).Value = Convert.ToDateTime("10/31/2014");
                    cmdsu.ExecuteNonQuery();
                    SqlDataAdapter daRpt = new SqlDataAdapter(cmdsu);
                    daRpt.Fill(rptDs);
                    grvrpt.DataSource = rptDs;
                    grvrpt.DataBind();

                    SqlCommand cmdsu1 = new SqlCommand("sp_QMS_FeedbackReports", scon);
                    cmdsu1.CommandType = CommandType.StoredProcedure;
                    cmdsu1.Parameters.Add("@sdate", SqlDbType.VarChar).Value = Convert.ToDateTime("10/31/2014");
                    cmdsu1.Parameters.Add("@edate", SqlDbType.VarChar).Value = Txtedate.Text + " 23:59:59";
                    cmdsu1.ExecuteNonQuery();
                    SqlDataAdapter daRpt1 = new SqlDataAdapter(cmdsu1);
                    daRpt1.Fill(rptDs1);
                    grdview2.DataSource = rptDs1;
                    grdview2.DataBind();
                }
            }
        }
        catch (Exception Ex)
        {
        }
        finally
        {
            scon.Close();
        }

    }
    protected void lnkUpload_Click(object sender, EventArgs e)
    {
        divUpload.Visible = true;
        divReports.Visible = false;
        divHome.Visible = false;
        divSubpanel.Visible = false;
    }
    protected void lnkReport_Click(object sender, EventArgs e)
    {
        grvrpt.DataSource = null;
        grvrpt.DataBind();
        divUpload.Visible = false;
        divReports.Visible = true;
        divHome.Visible = false;
        divSubpanel.Visible = false;
    }
    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Session["SearchActive"] = null;
        txtSearch.Text = " ";
        divSubpanel.Visible = false;
        divHome.Visible = true;
        divUpload.Visible = false;
        divReports.Visible = false;
        divPanel.Visible = false;
        bindGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            scon.Close();
            scon.Open();
            string str = "Select Upload_Id ,Issue_ID [Issue ID],Prod_Staff[Production Staff]," +
                                                  " Prod_Manager[Production Manager], Journal, Manuscript_Issues[Manuscript / Issue], Supplier[Supplier]," +
                                                   "Freelance_CE,Convert(varchar,Date_Feedback_Log,101)[Date feedback logged]," +
                                                   "Type_Problem[Type of problem],SOP[Severity of problem]" +
                                                   ", WorkFlow[Workflow],Replace(Description,'~','.')Description,Root_causes from " +
                                                   " tbQMS_Excel_History where Journal like '%" + txtSearch.Text + "%' order by Date_Feedback_Log Desc";
            SqlCommand gcmd = new SqlCommand("Select Upload_Id ,Issue_ID [Issue ID],Prod_Staff[Production Staff]," +
                                                  " Prod_Manager[Production Manager], Journal, Manuscript_Issues[Manuscript / Issue], Supplier[Supplier]," +
                                                   "Freelance_CE,Convert(varchar,Date_Feedback_Log,101)[Date feedback logged]," +
                                                   "Type_Problem[Type of problem],SOP[Severity of problem]" +
                                                   ", WorkFlow[Workflow],Replace(Description,'~','.')Description,Root_causes from " +
                                                   " tbQMS_Excel_History where Journal like '%" + txtSearch.Text + "%' order by Date_Feedback_Log Desc", scon);
            SqlDataAdapter da = new SqlDataAdapter(gcmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            gvCustomerList.DataSource = ds;
            gvCustomerList.DataBind();
            divPanel.Visible = false;
            Session["SearchActive"] = ds;
        }
        catch (Exception Ex)
        {
            // continue;
        }
        finally
        {
            scon.Close();
        }
    }
    public void AutoComplete()
    {
        try
        {
            //  scon.Open();

            //SqlCommand cmd = new SqlCommand("Select  Journal tbQMS_Excel_History order by Date_Feedback_Log Desc", scon);
            //DataSet ds = new DataSet();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(ds, "List"); AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            //int i = 0;
            //for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            //{
            //    col.Add(ds.Tables[0].Rows[i]["Name"].ToString());

            //}
            //txtJobName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txtJobName.AutoCompleteCustomSource = col;
            //txtJobName.AutoCompleteMode = AutoCompleteMode.Suggest;
        }
        catch (Exception Ex)
        {
            // continue;
        }
        finally
        {
            scon.Close();
        }
    }
    protected void grdSublist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSublist.PageIndex = e.NewPageIndex;
        bindHomeGrid();
    }
    protected void grdSublist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View" && Page.IsValid)
        {
            divHome.Visible = true;
            divSubpanel.Visible = false;

            if (e.CommandName == "View" && Page.IsValid)
            {
                GridViewRow row1 = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                row1.BackColor = System.Drawing.Color.MediumPurple;
                divPanel.Visible = true;
                showPanel(divInternal);
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string IssueIds = grdSublist.DataKeys[row.RowIndex].Values["Upload_Id"].ToString();
                DataSet ds = GetLiveFeedBackDetails(IssueIds);
                Session["Sorting"] = ds;
                if (ds.Tables[0].Rows.Count >= 0)
                {
                    ddlSupplier_point.SelectedItem.Text = ds.Tables[0].Rows[0]["Problem_type"].ToString();
                    ddlAssign_category.SelectedItem.Text = ds.Tables[0].Rows[0]["Assign_category"].ToString();
                    txtmultiplecategories.Text = ds.Tables[0].Rows[0]["Multiple_category"].ToString();
                    ddlproblem_occurred.SelectedItem.Text = ds.Tables[0].Rows[0]["Problem_occurred"].ToString();
                    txtroot_causes1.Text = ds.Tables[0].Rows[0]["Root_causes"].ToString();
                    ddlNature_problem_Single.SelectedItem.Text = ds.Tables[0].Rows[0]["Nature_problem_Single"].ToString();
                    txtmultiplecategories.Text = ds.Tables[0].Rows[0]["Nature_problem_Multiple"].ToString();
                    txtSupplier_actions1.Text = ds.Tables[0].Rows[0]["Supplier_actions"].ToString();
                    txtCompletionDate.Text = (ds.Tables[0].Rows[0]["Completion_date"]).ToString();
                    ddlName_role_title.SelectedItem.Text = (ds.Tables[0].Rows[0]["Name_role"]).ToString();
                    ddlIs_issue.SelectedItem.Text = ds.Tables[0].Rows[0]["Is_issue"].ToString();
                    txtinterim.Text = ds.Tables[0].Rows[0]["interim"].ToString();
                    txtFinalResolution.Text = ds.Tables[0].Rows[0]["Projected_final"].ToString();
                    txtDescription1.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    lblDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    lblRoot_causes.Text = ds.Tables[0].Rows[0]["Root_causes"].ToString();
                    lblSupplier_actions.Text = ds.Tables[0].Rows[0]["Supplier_actions"].ToString();
                }



                EnableFalse();


            }

        }
    }


    public void bindHomeGrid()
    {
        try
        {
            scon.Open();
            SqlCommand gcmd = new SqlCommand("Select Upload_Id ,Issue_ID [Issue ID],Prod_Staff[Production Staff]," +
                                                  " Prod_Manager[Production Manager], Journal, Manuscript_Issues[Manuscript / Issue], Supplier[Supplier]," +
                                                   "Freelance_CE,Convert(varchar,Date_Feedback_Log,101)[Date feedback logged]," +
                                                   "Type_Problem [Type of problem],SOP[Severity of problem]" +
                                                   ", WorkFlow[Workflow],Replace(Description,'~','.')Description,Root_causes from " +
                                                   " tbQMS_Excel_History order by Date_Feedback_Log Desc", scon);
            SqlDataAdapter da = new SqlDataAdapter(gcmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            grdSublist.DataSource = ds;
            grdSublist.DataBind();
        }
        catch (Exception Ex)
        {
            // continue;
        }
        finally
        {
            scon.Close();
        }
    }
    protected void grdSublist_RowDataBound(object sender, GridViewRowEventArgs e)
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
}


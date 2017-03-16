using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Configuration;

public partial class LaunchFilePages : System.Web.UI.Page
{
    SqlConnection scon;
    Non_Launch nonLa = new Non_Launch();
    Launch_SQL oNewLa = new Launch_SQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DataSet Ds = (DataSet)Session["FilePages"];
            gv_FilePages.DataSource = Ds;
            gv_FilePages.DataBind();
            NL_ID.Text = Request.QueryString["NL_ID"];
            NTLS_ID.Text = Request.QueryString["NTLS_ID"];
            Task_ID.Text = Request.QueryString["Task_ID"];
            Soft_ID.Text = Request.QueryString["Soft_ID"];
            Lang_ID.Text = Request.QueryString["Lang_ID"];
            txtFiles.Text = Request.QueryString["File"];
            DataSet ds1 = new DataSet();
            ds1 = nonLa.GetDeliveryStatus(Convert.ToInt32(NL_ID.Text));
            if (ds1.Tables[0].Rows[0]["Jobid"].ToString() != "")
            {
                if (Session["employeeid"].ToString() == "2461")
                {
                    btnFPSave.Visible = true;
                    btnCancel.Visible = true;
                    btnUpload.Visible = true; 
                }
                else
                {
                    btnFPSave.Visible = false;
                    btnCancel.Visible = false;
                    btnUpload.Visible = false;
                }
            }
            else
            {
                btnFPSave.Visible = true;
                btnCancel.Visible = true;
                btnUpload.Visible = true; 
            }
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        int count = 0;
        DataSet Ds = new DataSet();
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
            Session["NTLS_ID"] = "";
            nonLa.UpdateFilePagesStatusLP(NL_ID.Text, NTLS_ID.Text);
            int xx = 0;
            DataSet ds1 = new DataSet();
            for (int i = 0; i < dtExcelRecords.Rows.Count; i++)
            {
                if (Convert.ToInt16(Request.QueryString["File"].ToString()) > xx)
                {
                    string inproc = "spLPInsertFilePages";
                    string[,] pname ={
                                {"@LP_ID",NL_ID.Text },{"@NTLS_ID",NTLS_ID.Text},
                                {"@Task_ID",Task_ID.Text},{"@Lang_ID",Lang_ID.Text},
                                {"@Soft_ID",Soft_ID.Text},{"@Files_Name",dtExcelRecords.Rows[i][1].ToString()},
                                {"@Pages",dtExcelRecords.Rows[i][2].ToString()},{"@Files_ID",dtExcelRecords.Rows[i][0].ToString()},
                                {"@ISExists","Output"},{"@FileConv",Request.QueryString["FileConv"].ToString()}
                             };
                    int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
                    if (val == 1)
                        lblResult.Text = "Inserted Successfully";
                    else
                        lblResult.Text = "Inserted Failed!.";

                    ds1 = nonLa.GetTargetFileDetails(Task_ID.Text, Lang_ID.Text, Soft_ID.Text, NL_ID.Text);

                    if (ds1 != null)
                    {
                        Session["NTLS_ID"] = ds1.Tables[0].Rows[0]["NTLS_ID"].ToString();
                    }
                }
                xx++;
            }
            nonLa.UpdateLPFile_Count(NTLS_ID.Text, NL_ID.Text, txtFiles.Text);
            nonLa.DeleteLPFilePagesStatus(NL_ID.Text, NTLS_ID.Text);

                //Target Files
                if (Request.QueryString["FileConv"].ToString() != "As per target")
                {
                    if (Session["NTLS_ID"].ToString() != "")
                    {
                        DataSet tf = new DataSet();
                        nonLa.UpdateTarFilePagesStatusLP(NL_ID.Text, Session["NTLS_ID"].ToString());
                        int yy = 0;
                        for (int i = 0; i < dtExcelRecords.Rows.Count; i++)
                        {
                            if (Convert.ToInt16(Request.QueryString["File"].ToString()) > yy)
                            {
                                TextBox FileName = new TextBox();
                                FileName.Text = dtExcelRecords.Rows[i][1].ToString();
                                Session["FileName"] = dtExcelRecords.Rows[i][1].ToString();
                                tf = nonLa.GetLangDetails(Lang_ID.Text);
                                string langDesc = tf.Tables[0].Rows[0]["Lang_Desc"].ToString().Trim();
                                if (Request.QueryString["Task_ID"].ToString() != "1")
                                {
                                    if (Request.QueryString["Task_ID"].ToString() != "2")
                                    {
                                        if (Request.QueryString["Task_ID"].ToString() != "6")
                                        {
                                            if (Request.QueryString["FileConv"].ToString() == "As per source_language code")
                                            {
                                                FileName.Text = FileName.Text.Trim() + "_" + langDesc.ToString().Replace("(", "").Replace(")", "");
                                            }
                                            else if (Request.QueryString["FileConv"].ToString() == "File Name_Languge Code_YYYY_MM_DD_Version")
                                            {
                                                FileName.Text = FileName.Text.Trim() + "_" + langDesc.ToString().Replace("(", "").Replace(")", "") + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
                                            }
                                            else if (Request.QueryString["FileConv"].ToString() == "File Name_YYYY_MM_DD_Version")
                                            {
                                                FileName.Text = FileName.Text.Trim() + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
                                            }
                                            else
                                            {
                                                FileName.Text = FileName.Text.Trim();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    FileName.Text = FileName.Text.Trim();
                                }

                                string inproc = "spTarLPInsertFilePages";
                                string[,] pname ={
                                                    {"@LP_ID",NL_ID.Text },{"@NTLS_ID",Session["NTLS_ID"].ToString()},
                                                    {"@Task_ID",Task_ID.Text},{"@Lang_ID",Lang_ID.Text},
                                                    {"@Soft_ID",Soft_ID.Text},{"@Files_Name",dtExcelRecords.Rows[i][1].ToString()},
                                                    {"@Pages",dtExcelRecords.Rows[i][2].ToString()},{"@Files_ID",dtExcelRecords.Rows[i][0].ToString()},
                                                    {"@ISExists","Output"},{"@FileConv",Request.QueryString["FileConv"].ToString()}
                                                 };
                                int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
                                if (val == 1)
                                    lblResult.Text = "Inserted Successfully";
                                else
                                    lblResult.Text = "Inserted Failed!.";
                                FileName.Text = Session["FileName"].ToString();
                                yy++;
                            }
                        }
                        nonLa.UpdateTarLPFile_Count(Session["NTLS_ID"].ToString(), NL_ID.Text, txtFiles.Text);
                        nonLa.DeleteTarLPFilePagesStatus(NL_ID.Text, Session["NTLS_ID"].ToString());
                    }
                    else
                    {
                        DataSet ss = new DataSet();
                        ss = nonLa.GetTarTaskLP(NL_ID.Text, Request.QueryString["Task_ID"].ToString(), Request.QueryString["Soft_ID"].ToString());
                        if (ss != null)
                        {
                            for (int i = 0; i < ss.Tables[0].Rows.Count; i++)
                            {
                                DataSet tf = new DataSet();
                                nonLa.UpdateTarFilePagesStatusLP(NL_ID.Text, ss.Tables[0].Rows[i]["NTLS_ID"].ToString());
                                int zz = 0;
                                for (int j = 0; j < dtExcelRecords.Rows.Count; j++)
                                {
                                    if (Convert.ToInt16(Request.QueryString["File"].ToString()) > zz)
                                    {
                                        TextBox FileName = new TextBox();
                                        TextBox Pages = new TextBox();
                                        Label FileID = new Label();
                                        FileName.Text = dtExcelRecords.Rows[j][1].ToString();
                                        Pages.Text = dtExcelRecords.Rows[j][2].ToString();
                                        FileID.Text = dtExcelRecords.Rows[j][0].ToString();
                                        tf = nonLa.GetLangDetails(ss.Tables[0].Rows[i]["Lang_ID"].ToString());
                                        string langDesc = tf.Tables[0].Rows[0]["Lang_Desc"].ToString().Trim();
                                        Session["FileName"] = FileName.Text;
                                        if (Request.QueryString["Task_ID"].ToString() != "1")
                                        {
                                            if (Request.QueryString["Task_ID"].ToString() != "2")
                                            {
                                                if (Request.QueryString["Task_ID"].ToString() != "6")
                                                {
                                                    if (Request.QueryString["FileConv"].ToString() == "As per source_language code")
                                                    {
                                                        FileName.Text = FileName.Text.Trim() + "_" + langDesc.ToString().Replace("(", "").Replace(")", "");
                                                    }
                                                    else if (Request.QueryString["FileConv"].ToString() == "File Name_Languge Code_YYYY_MM_DD_Version")
                                                    {
                                                        FileName.Text = FileName.Text.Trim() + "_" + langDesc.ToString().Replace("(", "").Replace(")", "") + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
                                                    }
                                                    else if (Request.QueryString["FileConv"].ToString() == "File Name_YYYY_MM_DD_Version")
                                                    {
                                                        FileName.Text = FileName.Text.Trim() + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
                                                    }
                                                    else
                                                    {
                                                        FileName.Text = FileName.Text.Trim();
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            FileName.Text = FileName.Text.Trim();
                                        }

                                        string inproc = "spTarLPInsertFilePages";
                                        string[,] pname ={
                                                            {"@LP_ID",NL_ID.Text },{"@NTLS_ID",ss.Tables[0].Rows[i]["NTLS_ID"].ToString()},
                                                            {"@Task_ID",ss.Tables[0].Rows[i]["Task_ID"].ToString()},{"@Lang_ID",ss.Tables[0].Rows[i]["Lang_ID"].ToString()},
                                                            {"@Soft_ID",ss.Tables[0].Rows[i]["Soft_ID"].ToString()},{"@Files_Name",FileName.Text},
                                                            {"@Pages",Pages.Text},{"@Files_ID",FileID.Text},{"@ISExists","Output"},
                                                            {"@FileConv",Request.QueryString["FileConv"].ToString()}
                                                         };
                                        int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
                                        if (val == 1)
                                            lblResult.Text = "Inserted Successfully";
                                        else
                                            lblResult.Text = "Inserted Failed!.";
                                        FileName.Text = Session["FileName"].ToString();
                                    }
                                    zz++;
                                }
                                nonLa.UpdateTarLPFile_Count(ss.Tables[0].Rows[i]["NTLS_ID"].ToString(), NL_ID.Text, txtFiles.Text);
                                nonLa.DeleteTarLPFilePagesStatus(NL_ID.Text, ss.Tables[0].Rows[i]["NTLS_ID"].ToString());
                            }
                        }
                    }
                }
                //Ds.Tables.Add(dtExcelRecords);
            }
        //DataSet ds = new DataSet();
        //ds = nonLa.spGetUploadFilePages(Task_ID.Text, Lang_ID.Text, Soft_ID.Text, NL_ID.Text, NTLS_ID.Text);
        //gv_FilePages.DataSource = ds;
        //gv_FilePages.DataBind();

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

        dss = nonLa.GetFilePages(NTLS_ID.Text);
        if (dss == null)
        {
            dss = nonLa.getLPNTLS(NTLS_ID.Text);

            for (int j = 1; j <= Convert.ToInt32(txtFiles.Text); j++)
            {
                dTable.Rows.Add(j, dss.Tables[0].Rows[0]["LP_ID"].ToString(), dss.Tables[0].Rows[0]["NTLS_ID"].ToString(),
                    dss.Tables[0].Rows[0]["TaskName"].ToString(), dss.Tables[0].Rows[0]["Task_ID"].ToString(),
                    dss.Tables[0].Rows[0]["Lang_name"].ToString(), dss.Tables[0].Rows[0]["Lang_ID"].ToString(),
                    dss.Tables[0].Rows[0]["Soft_Name"].ToString(), dss.Tables[0].Rows[0]["Soft_ID"].ToString(), "", 0);
            }
        }
        else
        {
            dss = nonLa.getLPInsertedNTLS(NTLS_ID.Text);
            int k = 1;
            for (int j = 0; j <= (Convert.ToInt32(txtFiles.Text) - 1); j++)
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
        //DataSet Ds = new DataSet();
        Ds.Tables.Add(dTable);
        gv_FilePages.DataSource = Ds;
        gv_FilePages.DataBind();
        Session["FilePages"] = Ds;
        ScriptManager.RegisterStartupScript(this.Page, GetType(), "ClosePopup", "window.opener.document.getElementById(\"xxxx\").click();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Window", "self.close();", true);
    }
    protected void Save(object sender, EventArgs e)
    {
        DataSet ds1 = new DataSet();
        Session["NTLS_ID"] = "";
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
                                {"@Pages",Pages.Text},{"@Files_ID",FileID.Text},{"@ISExists","Output"},
                                {"@FileConv",Request.QueryString["FileConv"].ToString()}
                             };
            int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
            if (val == 1)
                lblResult.Text = "Inserted Successfully";
            else
                lblResult.Text = "Inserted Failed!.";

            ds1 = nonLa.GetTargetFileDetails(hid_Task_ID.Value, hid_Lang_ID.Value, hid_Soft_ID.Value, sJobID.Value);
            if (ds1 != null)
            {
                Session["NTLS_ID"] = ds1.Tables[0].Rows[0]["NTLS_ID"].ToString();
            }
        }
        nonLa.UpdateLPFile_Count(NTLS_ID.Text, NL_ID.Text, txtFiles.Text);
        nonLa.DeleteLPFilePagesStatus(NL_ID.Text, NTLS_ID.Text);

        //Target Files
        if (Request.QueryString["FileConv"].ToString() != "As per target")
        {
            if (Session["NTLS_ID"].ToString() != "")
            {
                DataSet tf = new DataSet();
                nonLa.UpdateTarFilePagesStatusLP(NL_ID.Text, Session["NTLS_ID"].ToString());
                foreach (GridViewRow grs in gv_FilePages.Rows)
                {
                    HiddenField sJobID = (HiddenField)grs.FindControl("hid_LP_ID");
                    //HiddenField hid_NTLS_ID = (HiddenField)Session["NTLS_ID"];
                    HiddenField hid_Task_ID = (HiddenField)grs.FindControl("hid_Task_ID");
                    HiddenField hid_Lang_ID = (HiddenField)grs.FindControl("hid_Lang_ID");
                    HiddenField hid_Soft_ID = (HiddenField)grs.FindControl("hid_Soft_ID");
                    TextBox FileName = (TextBox)grs.FindControl("txt_Name");
                    TextBox Pages = (TextBox)grs.FindControl("txt_Pages");
                    Label FileID = (Label)grs.FindControl("lbl_File");
                    Session["FileName"] = FileName.Text;
                    tf = nonLa.GetLangDetails(hid_Lang_ID.Value);
                    string langDesc = tf.Tables[0].Rows[0]["Lang_Desc"].ToString().Trim();
                    if (Request.QueryString["Task_ID"].ToString() != "1")
                    {
                        if (Request.QueryString["Task_ID"].ToString() != "2")
                        {
                            if (Request.QueryString["Task_ID"].ToString() != "6")
                            {
                                if (Request.QueryString["FileConv"].ToString() == "As per source_language code")
                                {
                                    FileName.Text = FileName.Text.Trim() + "_" + langDesc.ToString().Replace("(", "").Replace(")", "");
                                }
                                else if (Request.QueryString["FileConv"].ToString() == "File Name_Languge Code_YYYY_MM_DD_Version")
                                {
                                    FileName.Text = FileName.Text.Trim() + "_" + langDesc.ToString().Replace("(", "").Replace(")", "") + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
                                }
                                else if (Request.QueryString["FileConv"].ToString() == "File Name_YYYY_MM_DD_Version")
                                {
                                    FileName.Text = FileName.Text.Trim() + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
                                }
                                else
                                {
                                    FileName.Text = FileName.Text.Trim();
                                }
                            }
                        }
                    }
                    else
                    {
                        FileName.Text = FileName.Text.Trim();
                    }

                    string inproc = "spTarLPInsertFilePages";
                    string[,] pname ={
                                {"@LP_ID",sJobID.Value },{"@NTLS_ID",Session["NTLS_ID"].ToString()},
                                {"@Task_ID",hid_Task_ID.Value},{"@Lang_ID",hid_Lang_ID.Value},
                                {"@Soft_ID",hid_Soft_ID.Value},{"@Files_Name",FileName.Text},
                                {"@Pages",Pages.Text},{"@Files_ID",FileID.Text},{"@ISExists","Output"},
                                {"@FileConv",Request.QueryString["FileConv"].ToString()}
                             };
                    int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
                    if (val == 1)
                        lblResult.Text = "Inserted Successfully";
                    else
                        lblResult.Text = "Inserted Failed!.";
                    FileName.Text = Session["FileName"].ToString();
                }
                nonLa.UpdateTarLPFile_Count(Session["NTLS_ID"].ToString(), NL_ID.Text, txtFiles.Text);
                nonLa.DeleteTarLPFilePagesStatus(NL_ID.Text, Session["NTLS_ID"].ToString());
            }
            else
            {
                DataSet ss = new DataSet();
                ss = nonLa.GetTarTaskLP(NL_ID.Text, Request.QueryString["Task_ID"].ToString(), Request.QueryString["Soft_ID"].ToString());
                if (ss != null)
                {
                    for (int i = 0; i < ss.Tables[0].Rows.Count; i++)
                    {
                        DataSet tf = new DataSet();
                        nonLa.UpdateTarFilePagesStatusLP(NL_ID.Text, ss.Tables[0].Rows[i]["NTLS_ID"].ToString());
                        foreach (GridViewRow grs in gv_FilePages.Rows)
                        {
                            TextBox FileName = (TextBox)grs.FindControl("txt_Name");
                            TextBox Pages = (TextBox)grs.FindControl("txt_Pages");
                            Label FileID = (Label)grs.FindControl("lbl_File");

                            tf = nonLa.GetLangDetails(ss.Tables[0].Rows[i]["Lang_ID"].ToString());
                            string langDesc = tf.Tables[0].Rows[0]["Lang_Desc"].ToString().Trim();
                            Session["FileName"] = FileName.Text;
                            if (Request.QueryString["Task_ID"].ToString() != "1")
                            {
                                if (Request.QueryString["Task_ID"].ToString() != "2")
                                {
                                    if (Request.QueryString["Task_ID"].ToString() != "6")
                                    {
                                        if (Request.QueryString["FileConv"].ToString() == "As per source_language code")
                                        {
                                            FileName.Text = FileName.Text.Trim() + "_" + langDesc.ToString().Replace("(", "").Replace(")", "");
                                        }
                                        else if (Request.QueryString["FileConv"].ToString() == "File Name_Languge Code_YYYY_MM_DD_Version")
                                        {
                                            FileName.Text = FileName.Text.Trim() + "_" + langDesc.ToString().Replace("(", "").Replace(")", "") + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
                                        }
                                        else if (Request.QueryString["FileConv"].ToString() == "File Name_YYYY_MM_DD_Version")
                                        {
                                            FileName.Text = FileName.Text.Trim() + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
                                        }
                                        else
                                        {
                                            FileName.Text = FileName.Text.Trim();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                FileName.Text = FileName.Text.Trim();
                            }

                            string inproc = "spTarLPInsertFilePages";
                            string[,] pname ={
                                {"@LP_ID",NL_ID.Text },{"@NTLS_ID",ss.Tables[0].Rows[i]["NTLS_ID"].ToString()},
                                {"@Task_ID",ss.Tables[0].Rows[i]["Task_ID"].ToString()},{"@Lang_ID",ss.Tables[0].Rows[i]["Lang_ID"].ToString()},
                                {"@Soft_ID",ss.Tables[0].Rows[i]["Soft_ID"].ToString()},{"@Files_Name",FileName.Text},
                                {"@Pages",Pages.Text},{"@Files_ID",FileID.Text},{"@ISExists","Output"},
                                {"@FileConv",Request.QueryString["FileConv"].ToString()}
                             };
                            int val = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
                            if (val == 1)
                                lblResult.Text = "Inserted Successfully";
                            else
                                lblResult.Text = "Inserted Failed!.";
                            FileName.Text = Session["FileName"].ToString();
                        }
                        nonLa.UpdateTarLPFile_Count(ss.Tables[0].Rows[i]["NTLS_ID"].ToString(), NL_ID.Text, txtFiles.Text);
                        nonLa.DeleteTarLPFilePagesStatus(NL_ID.Text, ss.Tables[0].Rows[i]["NTLS_ID"].ToString());
                    }
                }
            }
        }

        ScriptManager.RegisterStartupScript(this.Page, GetType(), "ClosePopup", "window.opener.document.getElementById(\"xxxx\").click();", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Window", "self.close();", true);
    }
}
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

public partial class LaunchNLTarFilesPages : System.Web.UI.Page
{
    SqlConnection scon;
    Non_Launch nonLa = new Non_Launch();
    Launch_SQL oNewLa = new Launch_SQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet Ds = (DataSet)Session["NLTarFilePages"];
            gv_FilePages.DataSource = Ds;
            gv_FilePages.DataBind();
            NL_ID.Text = Request.QueryString["NL_ID"];
            NTLS_ID.Text = Request.QueryString["NTLS_ID"];
            Task_ID.Text = Request.QueryString["Task_ID"];
            Soft_ID.Text = Request.QueryString["Soft_ID"];
            Lang_ID.Text = Request.QueryString["Lang_ID"];
            txtFiles.Text = Request.QueryString["File"];

        }
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
                        SqlCommand cmd = new SqlCommand("spTarNLUploadFilePages", scon);
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

        dss = nonLa.GetTarFilePages(NTLS_ID.Text);
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
            dss = nonLa.getTarLPInsertedNTLS(NTLS_ID.Text);
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
        DataSet Ds = new DataSet();
        Ds.Tables.Add(dTable);
        gv_FilePages.DataSource = Ds;
        gv_FilePages.DataBind();
        // popup.Show();
        Session["NLTarFilePages"] = Ds;
    }
    protected void Save(object sender, EventArgs e)
    {
        nonLa.UpdateTarFilePagesStatusLP(NL_ID.Text, NTLS_ID.Text);
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
            string inproc = "spTarLPInsertFilePages";
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
        }
        nonLa.UpdateTarLPFile_Count(NTLS_ID.Text, NL_ID.Text, txtFiles.Text);
        nonLa.DeleteTarLPFilePagesStatus(NL_ID.Text, NTLS_ID.Text);
        //nonLa.InsertUnassignJobs(NL_ID.Text);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Window", "self.close();", true);
    }
}
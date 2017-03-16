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
using System.Data.SqlClient;

public partial class EditJournal : System.Web.UI.Page
{
    datasourceSQL jobj = new datasourceSQL();
    DataSet jds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Btn_journalupdate.Enabled = true;
        if (!Page.IsPostBack)
        {
            loadDetails();

            string jourcode = Request.QueryString["jourid"];
            jds = jobj.ExcProcedure("spGet_JournalInfo", new string[,] { { "@jourcode", jourcode } }, CommandType.StoredProcedure);
            journal_id.Text = jds.Tables[0].Rows[0]["Journal_Code"].ToString();
            journal_name.Text = jds.Tables[0].Rows[0]["Journal_Title"].ToString();
            ddCE.Text = jds.Tables[0].Rows[0]["Is_Copyedit"].ToString();
            ddlSAM.Text = jds.Tables[0].Rows[0]["Is_SAM"].ToString();
            ddlSensitive.Text = jds.Tables[0].Rows[0]["Is_Sensitive"].ToString();
            ddlFPM.Text = jds.Tables[0].Rows[0]["FPM_Journal"].ToString();
            ddlDoi.Text = jds.Tables[0].Rows[0]["DOI"].ToString();
            ddlAQSheetNo.Text = jds.Tables[0].Rows[0]["AQ_Cover_Sheet_No"].ToString();
            pagetrim_id.Text = jds.Tables[0].Rows[0]["Trim_Size"].ToString();
            prodeditor_id.Text = jds.Tables[0].Rows[0]["Production_Editor"].ToString();
        }
    }

    private void loadDetails()
    {
        datasourceSQL pobj = new datasourceSQL();
        DataSet pds = new DataSet();
        DataSet dsDup = new System.Data.DataSet();
      
        DataTable dtDistinct;
        try
        {
            pds = pobj.ExcProcedure("spGet_allJournal",null, CommandType.StoredProcedure);

            DataTable dt = pds.Tables[0];


            journal_id.DataSource = pds;
            journal_id.DataTextField = pds.Tables[0].Columns["Journal_Code"].ToString();
            journal_id.DataBind();
            journal_id.Items.Insert(0, new ListItem("-- select --", "0"));


            string[] TobeDistinct = { "Is_Copyedit" };
              dtDistinct = GetDistinctRecords(pds.Tables[0], TobeDistinct);
            dsDup.Tables.Add(dtDistinct);

            ddCE.DataSource = dsDup;
            ddCE.DataTextField = dsDup.Tables[0].Columns["Is_Copyedit"].ToString();
            ddCE.DataBind();
            ddCE.Items.Insert(0, new ListItem("-- select --", "0"));

            dtDistinct.Reset();
            dsDup.Reset();
            string[]  TobeDistinct2 = { "Is_SAM" };
             dtDistinct = GetDistinctRecords(pds.Tables[0], TobeDistinct2);
            dsDup.Tables.Add(dtDistinct);

            ddlSAM.DataSource = dsDup;
            ddlSAM.DataTextField = dsDup.Tables[0].Columns["Is_SAM"].ToString();
            ddlSAM.DataBind();
            ddlSAM.Items.Insert(0, new ListItem("-- select --", "0"));

            dtDistinct.Reset();
            dsDup.Reset();
           string[]  TobeDistinct3 = { "FPM_Journal" };
             dtDistinct = GetDistinctRecords(pds.Tables[0], TobeDistinct3);
            dsDup.Tables.Add(dtDistinct);

            ddlFPM.DataSource = dsDup;
            ddlFPM.DataTextField = dsDup.Tables[0].Columns["FPM_Journal"].ToString();
            ddlFPM.DataBind();
            ddlFPM.Items.Insert(0, new ListItem("-- select --", "0"));

            dtDistinct.Reset();
            dsDup.Reset();
           string[]   TobeDistinct4 = { "Trim_Size" };
             dtDistinct = GetDistinctRecords(pds.Tables[0], TobeDistinct4);
            dsDup.Tables.Add(dtDistinct);

            pagetrim_id.DataSource = dsDup;
            pagetrim_id.DataTextField = dsDup.Tables[0].Columns["Trim_Size"].ToString();
            pagetrim_id.DataBind();
            pagetrim_id.Items.Insert(0, new ListItem("-- select --", "0"));

            dtDistinct.Reset();
            dsDup.Reset();
           string[]   TobeDistinct5 = { "DOI" };
             dtDistinct = GetDistinctRecords(pds.Tables[0], TobeDistinct5);
            dsDup.Tables.Add(dtDistinct);

            ddlDoi.DataSource = dsDup;
            ddlDoi.DataTextField = dsDup.Tables[0].Columns["DOI"].ToString();
            ddlDoi.DataBind();
            ddlDoi.Items.Insert(0, new ListItem("-- select --", "0"));

            dtDistinct.Reset();
            dsDup.Reset();
            string[] TobeDistinct6 = { "AQ_Cover_Sheet_No" };
             dtDistinct = GetDistinctRecords(pds.Tables[0], TobeDistinct6);
            dsDup.Tables.Add(dtDistinct);

            ddlAQSheetNo.DataSource = dsDup;
            ddlAQSheetNo.DataTextField = dsDup.Tables[0].Columns["AQ_Cover_Sheet_No"].ToString();
            ddlAQSheetNo.DataBind();
            ddlAQSheetNo.Items.Insert(0, new ListItem("-- select --", "0"));

            dtDistinct.Reset();
            dsDup.Reset();
            string[] TobeDistinct7 = { "Production_Editor" };
            dtDistinct = GetDistinctRecords(pds.Tables[0], TobeDistinct7);
            dsDup.Tables.Add(dtDistinct);
            prodeditor_id.DataSource = dsDup;
            prodeditor_id.DataTextField = dsDup.Tables[0].Columns["Production_Editor"].ToString();
            prodeditor_id.DataBind();
            prodeditor_id.Items.Insert(0, new ListItem("-- select --", "0"));

            dtDistinct.Reset();
            dsDup.Reset();
            string[] TobeDistinct8 = { "Is_Sensitive" };
            dtDistinct = GetDistinctRecords(pds.Tables[0], TobeDistinct8);
            dsDup.Tables.Add(dtDistinct);
            ddlSensitive.DataSource = dsDup;
            ddlSensitive.DataTextField = dsDup.Tables[0].Columns["Is_Sensitive"].ToString();
            ddlSensitive.DataBind();
            ddlSensitive.Items.Insert(0, new ListItem("-- select --", "0"));
            
        }
        catch (Exception ex)
        { throw ex; }
        finally { pobj = null; pds = null; }
    }

    public static DataTable GetDistinctRecords(DataTable dt, string[] Columns)
    {
        DataTable dtUniqRecords = new DataTable();
        dtUniqRecords = dt.DefaultView.ToTable(true, Columns);
        return dtUniqRecords;
    }

    private void LoadddlControl(DropDownList ddlcntrl, DataTable lds, string selectedval, string firstval)
    {
        try
        {
            //ddlcntrl.Items.Clear();
            if (lds == null)
                ddlcntrl.Items.Clear();
            else
            {
                ddlcntrl.Items.Clear();
                ddlcntrl.ClearSelection();
                ddlcntrl.DataSource = lds;
                ddlcntrl.DataBind();
            }
            if (firstval != null)
                ddlcntrl.Items.Insert(0, new ListItem(firstval, "0"));
            if (selectedval != null)
                ddlcntrl.SelectedValue = selectedval;
        }
        catch (Exception ex)
        { throw ex; }


    }



    protected void Btn_journalupdate_Click(object sender, EventArgs e)
    {
        SqlConnection oConn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString);
       
        SqlCommand ocmd;
        try
        {
            oConn.Open();
            string strUpdate = "Update tbQMS_Journal_Det set Journal_Title='" + journal_name.Text + "',Is_Copyedit='" + ddCE.Text + "'" +
                             " ,Is_SAM='" + ddlSAM.Text + "',Is_Sensitive='" + ddlSensitive.Text + "',FPM_Journal='" + ddlFPM.Text + "',DOI='" + ddlDoi.Text + "',AQ_Cover_Sheet_No='" + ddlAQSheetNo.Text + "' " +
                             " ,Trim_Size='" + pagetrim_id.Text + "',Production_Editor='" + prodeditor_id.Text + "' where Journal_Code='" + journal_id.Text + "' ";
            ocmd = new SqlCommand(strUpdate, oConn);
            ocmd.ExecuteNonQuery();

            Btn_journalupdate.Enabled = false;

        }
        catch (Exception Ex)
        {

        }
        finally
        {
            oConn.Close();
            Response.Redirect("QMS_StyleSheet.aspx", true);
          
        }
    }
}

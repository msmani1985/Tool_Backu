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

public partial class AddJournal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SqlConnection oConn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString);
        SqlCommand ocmd;
        try
        {
            oConn.Open();
            string strInsert = "Insert Into tbQMS_Journal_Det (Journal_Code,Journal_Title,Production_Editor,Trim_Size,Is_Copyedit,Is_Sensitive,Is_SAM,FPM_Journal,AQ_Cover_Sheet_No,DOI)" +
                                " Values ('" + txtJourCode.Text.Trim() + "','" + journal_name.Text.Trim() + "','" + txtPE_name.Text.Trim() + "','" + txtTrim.Text.Trim() + "','" + ddCE.Text.Trim() + "'," +
                                " '" + ddlSensitive.Text.Trim() + "','" + ddlSAM.Text.Trim() + "','" + ddlFPM.Text.Trim() + "','" + txtAQ_Cover.Text.Trim() + "','" + txtDOI.Text.Trim() + "')";

            ocmd = new SqlCommand(strInsert, oConn);
            ocmd.ExecuteNonQuery();
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

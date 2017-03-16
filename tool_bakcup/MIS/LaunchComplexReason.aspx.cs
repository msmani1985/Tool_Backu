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

public partial class LaunchComplexReason : System.Web.UI.Page
{
    Launch la = new Launch();
    Non_Launch oNL = new Non_Launch();
    Launch_SQL oLaunch = new Launch_SQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet Dst3 = new DataSet();
            Dst3 = la.GetTask();
            lboxtaskReason.DataTextField = "taskname";
            lboxtaskReason.DataValueField = "task_id";
            lboxtaskReason.DataSource = Dst3;
            lboxtaskReason.DataBind();
            DropComReason.Items.Clear();
            DropComReason.Items.Add(new ListItem("Simple", "S"));
            DropComReason.Items.Add(new ListItem("Medium", "M"));
            DropComReason.Items.Add(new ListItem("Complex", "C"));
        }
    }
    protected void btnSaveReason_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
        {
            string inproc = "SPInsert_ComPlexReason";
            string[,] pname ={
                    {"@Com_Level",DropComReason.SelectedValue},{"@Task",lboxtaskReason.SelectedValue},
                    {"@Format",lboxformatreason.SelectedValue},{"@SourceType",DropSourceTypeReason.SelectedValue},
                    {"@Com_Reason",txtNewReason.Text},{"@Isexists","OUTPUT"}
                };
            int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
            if (val == 1)
                msg = "Inserted Successfully";
            else
                msg = "Reason Already Exists";
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
    protected void lboxtaskReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lboxtaskReason.SelectedValue == "1" )
        {
            lboxformatreason.Visible = true;
            lblFormatreason.Visible = true;
            lblSourcetypereason.Visible = true;
            DropSourceTypeReason.Visible = true;
            DataSet Dst4 = new DataSet();
            Dst4 = oNL.GetSelectedFormats(lboxtaskReason.SelectedValue);
            lboxformatreason.DataTextField = "format_name";
            lboxformatreason.DataValueField = "format_ID";
            lboxformatreason.DataSource = Dst4;
            lboxformatreason.DataBind();
        }
        else if (lboxtaskReason.SelectedValue == "3")
        {
            lboxformatreason.Visible = true;
            lblFormatreason.Visible = true;
            lblSourcetypereason.Visible = false;
            DropSourceTypeReason.Visible = false;
            DataSet Dst4 = new DataSet();
            Dst4 = oNL.GetSelectedFormats(lboxtaskReason.SelectedValue);
            lboxformatreason.DataTextField = "format_name";
            lboxformatreason.DataValueField = "format_ID";
            lboxformatreason.DataSource = Dst4;
            lboxformatreason.DataBind();
        }
        else
        {
            lboxformatreason.Visible = false;
            lblFormatreason.Visible = false;
            lblSourcetypereason.Visible = false;
            DropSourceTypeReason.Visible = false;
            DataSet Dst4 = new DataSet();
            Dst4 = oNL.GetSelectedFormats(lboxtaskReason.SelectedValue);
            lboxformatreason.DataTextField = "format_name";
            lboxformatreason.DataValueField = "format_ID";
            lboxformatreason.DataSource = Dst4;
            lboxformatreason.DataBind();
        }
    }
}

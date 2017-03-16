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

public partial class tempactions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["title"] != null)
        {

            if (Request.QueryString["title"].ToString() == "movepdf")
            {

                divMovePDF.Style.Add("display", "''");

            }

        }


    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        int job_id = 0;

        int.TryParse(txtjobid.Text, out job_id);

        if (txtjobid.Text != "" && job_id != 0)
        {

            divMessage.InnerHtml = ResetJobObjects();

        }

        else
        {

            divMessage.InnerHtml = "Enter Jobnumber (Numerics)!!";

        }

    }

    private string ResetJobObjects()
    {

        string sResult = "";

        datasourceSQL oDB = new datasourceSQL();

        string[,] oParams = new string[,] {{"@jobid", txtjobid.Text.ToString()}, 
                {"@jobtypeid", ddljobtypeid.SelectedValue.ToString()}};

        try
        {

            sResult = oDB.RevertJobObjects(oParams,true);

            if (sResult == "")

                return "Reset of PDF Move is completed, try now to Move PDF from 'PS BATCH' folder";

            else

                return sResult;

        }

        catch (Exception oex)
        {

            return "Error in Processing PDF Move, contact IT team\n" + oex.Message;

        }

        finally
        {

            oDB = null;

        }


    }


}

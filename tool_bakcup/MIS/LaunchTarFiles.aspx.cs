using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class LaunchTarFiles : System.Web.UI.Page
{
    Non_Launch nonLa = new Non_Launch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            DataSet ds1 = new DataSet();
            ds1 = nonLa.getTarTaskLangDetailsByLPID(Request.QueryString["NL_ID"]);
            gvTarFileInfo.DataSource = ds1;
            gvTarFileInfo.DataBind();
            DataSet ds = new DataSet();
            ds = nonLa.GetDeliveryStatus(Convert.ToInt32(Request.QueryString["NL_ID"]));
            txtNewtarget.Text = ds.Tables[0].Rows[0]["TargetDate"].ToString();
            if (ds.Tables[0].Rows[0]["TargetDate"].ToString() == "0")
                CheckNewYTR.Checked = false;
            else
                CheckNewYTR.Checked = true;
        }
    }
    protected void TarEdit(object sender, EventArgs e)
    {
        Session["FilePages"] = "";
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
            //gvTarFileInfo.DataSource = Ds;
            //gvTarFileInfo.DataBind();
            // popup.Show();
            Session["TarFilePages"] = Ds;

            string strPopup = "<script language='javascript' ID='script1'>"
            + "window.open('LaunchTarFilesPages.aspx?NL_ID=" + HttpUtility.UrlEncode(sJobID.Value) + "&NTLS_ID=" + HttpUtility.UrlEncode(hid_NTLS_ID.Value)
            + "&Task_ID=" + HttpUtility.UrlEncode(hid_Task_ID.Value) + "&Soft_ID=" + HttpUtility.UrlEncode(hid_Soft_ID.Value) + "&Lang_ID=" + HttpUtility.UrlEncode(hid_Lang_ID.Value) + "&File=" + HttpUtility.UrlEncode(File.ToString())
            + "','new window', 'top=90, left=200, width=950, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
            + "</script>";
            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
    }
    protected void Update_Click(object sender, EventArgs e)
    {
        int ytr=0;
        if(CheckNewYTR.Checked)
            ytr=1;
        else
            ytr=0;

        nonLa.UpdateTarDate(txtNewtarget.Text, ytr, Request.QueryString["NL_ID"]);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Window", "self.close();", true);
    }
}
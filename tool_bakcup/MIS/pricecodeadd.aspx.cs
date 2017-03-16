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
using System.Xml;

public partial class pricecodeadd : System.Web.UI.Page
{
    private int lastVarValue;
    string sFileName = @"\\dpserver2\db\dublin_journal_prices_2009.xml";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            try
            {
                if (Convert.ToBoolean(Session["webuser"].ToString()) == true)
                {
                    /*
                    if (Session["location"].ToString() != "i")
                    {
                        lblFileName.Text = Session["dublinPCfile"].ToString();   
                        lblServer.Text = "Dublin Price Code";
                    }
                    else
                    {
                        lblFileName.Text = Session["indiaPCfile"].ToString(); 
                        lblServer.Text = "Indian Price Code";
                    }
                    sFileName = lblFileName.Text;
                    sFileName = @"\\dpserver2\db\dublin_journal_prices_2009.xml";
                    */
                    //LoadXML(sFileName);
                }
                else
                    ShowError("");
            }
            catch (Exception oex)
            {
                ShowError(oex.Message);
            }
        }
        else
        {
            string sLocate = "<script language=Javascript>location.href='#" + Request.Form["__EVENTTARGET"] + "';</script>";
            this.RegisterStartupScript(this.UniqueID + "Startup", sLocate);
            

        }


    }
    private void ShowError(string sErrorMsg)
    {
        error1.Text = sErrorMsg + ".., you are not authorized to edit this page..., You  will be re-direct to welcome page in 5 seconds.";
        error1.Style.Add("text-align", "left");   
        error1.Visible = true;
        error1.Width = 600;

        if (!Page.ClientScript.IsClientScriptBlockRegistered(GetType(), "myMethod"))
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("intValue=window.setTimeout('myMethod()', 5000)");

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "key", sb.ToString(), true);

        }
    }

    private void LoadXML(string sFileName)
    {
        try
        {
            DataSet oDs = new DataSet();
            oDs.ReadXml(sFileName);
            dgPC.DataSource = oDs.Tables["ROW"];
            dgPC.DataBind();
        }
        catch
        {
            CreateXML();
        }
    }

    public void CreateXML()
    {
        try
        {
            DataSet objdata = new DataSet("DATAPACKET");
            DataTable dt = new DataTable("ROWDATA");
            DataRow dr = default(DataRow);

            dt.Columns.Add(new DataColumn("JCPNO"));
            dt.Columns.Add(new DataColumn("JCPNAME"));
            dt.Columns.Add(new DataColumn("JCPPRICE"));
            dt.Columns.Add(new DataColumn("JCPPRICE2"));
            dt.Columns.Add(new DataColumn("JCPPRICE3"));
            dt.Columns.Add(new DataColumn("CURRENCY"));
            dt.Columns.Add(new DataColumn("INDIAPRICE"));

            dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "No NAME";
            dr[2] = "0";
            dr[3] = "0";
            dr[4] = "0";
            dr[5] = "0";
            dr[6] = "0";

            dt.Rows.Add(dr);

            objdata.Tables.Add(dt);

            dgPC.DataSource = objdata;
            dgPC.DataBind();

            objdata.WriteXml(sFileName);

            LoadXML(sFileName);
        }
        catch (Exception oex)
        {
            error1.Visible = true;
            error1.Text = oex.Message;
        }

    }

    public void setEditMode(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            DataSet objdata = new DataSet();
            string x1 = null;

            objdata.ReadXml(sFileName);

            x1 = dgPC.DataKeys[e.Item.ItemIndex].ToString();

            objdata.Tables["ROW"].DefaultView.RowFilter = "JCPNO='" + x1 + "'";

            if (objdata.Tables["ROW"].DefaultView.Count > 0)
            {
                error4.Visible = false;
                dgPC.EditItemIndex = e.Item.ItemIndex;
                dgPC.ShowFooter = false;
                LoadXML(sFileName);
            }
            else
            {
                error4.Visible = true;
            }
        }
        catch (Exception oex)
        {
            error1.Visible = true;
            error1.Text = oex.Message;
        }
    }

    public void cancelEdit(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            dgPC.EditItemIndex = -1;
            dgPC.ShowFooter = true;
            error1.Visible = false;
            error2.Visible = false;
            error3.Visible = false;
            error4.Visible = false;
            error5.Visible = false;

            LoadXML(sFileName);
        }
        catch (Exception oex)
        {
            error1.Visible = true;
            error1.Text = oex.Message;
        }

    }

    public void delXML(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                if (dgPC.EditItemIndex == -1)
                {
                    error5.Visible = false;
                    string x1 = null;

                    x1 = dgPC.DataKeys[e.Item.ItemIndex].ToString();

                    DataSet objdata = new DataSet();
                    try
                    {
                        objdata.ReadXml(sFileName);
                        objdata.Tables["ROW"].DefaultView.RowFilter = "JCPNO='" + x1 + "'";
                        if (objdata.Tables["ROW"].DefaultView.Count > 0)
                            objdata.Tables["ROW"].DefaultView.Delete(0);

                        objdata.Tables["ROW"].DefaultView.RowFilter = "";

                        objdata.WriteXml(sFileName);
                    }
                    catch
                    {
                        CreateXML();
                    }

                    LoadXML(sFileName);
                }
                else
                {
                    error5.Visible = true;
                }
            }
        }
        catch (Exception oex)
        {
            error1.Visible = true;
            error1.Text = oex.Message;
        }
    }

    public void UpdateXML(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                int v1 = 0;
                DataSet objdata = new DataSet();
                string x1 = null;

                objdata.ReadXml(sFileName);

                v1 = e.Item.ItemIndex;

                x1 = dgPC.DataKeys[e.Item.ItemIndex].ToString();

                objdata.Tables["ROW"].DefaultView.RowFilter = "JCPNO='" + x1 + "'";

                if (objdata.Tables["ROW"].DefaultView.Count > 0)
                {
                    error3.Visible = false;

                    TextBox tadd1 = (TextBox)e.Item.Controls[0].FindControl("JCPNO_edit");
                    TextBox tadd2 = (TextBox)e.Item.Controls[0].FindControl("JCPNAME_edit");
                    TextBox tadd3 = (TextBox)e.Item.Controls[0].FindControl("JCPPRICE_edit");
                    TextBox tadd4 = (TextBox)e.Item.Controls[0].FindControl("JCPPRICE2_edit");
                    TextBox tadd5 = (TextBox)e.Item.Controls[0].FindControl("JCPPRICE3_edit");
                    TextBox tadd6 = (TextBox)e.Item.Controls[0].FindControl("CURRENCY_edit");
                    TextBox tadd7 = (TextBox)e.Item.Controls[0].FindControl("INDIAPRICE_edit");

                    objdata.Tables["ROW"].Rows[v1]["JCPNO"] = tadd1.Text;
                    objdata.Tables["ROW"].Rows[v1]["JCPNAME"] = tadd2.Text;
                    objdata.Tables["ROW"].Rows[v1]["JCPPRICE"] = tadd3.Text;
                    objdata.Tables["ROW"].Rows[v1]["JCPPRICE2"] = tadd4.Text;
                    objdata.Tables["ROW"].Rows[v1]["JCPPRICE3"] = tadd5.Text;
                    objdata.Tables["ROW"].Rows[v1]["CURRENCY"] = tadd6.Text;
                    objdata.Tables["ROW"].Rows[v1]["INDIAPRICE"]= tadd7.Text;

                    dgPC.DataSource = objdata.Tables["ROW"] ;
                    dgPC.DataBind();

                    objdata.WriteXml(sFileName);

                    dgPC.EditItemIndex = -1;
                    LoadXML(sFileName);
                    dgPC.ShowFooter = true;
                }
                else
                {
                    error3.Visible = true;
                }
            }
        }
        catch (Exception oex)
        {
            error1.Visible = true;
            error1.Text = oex.Message; 
        }
    }

    public void doInsert(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "doAdd")
            {

                DataSet objdata = new DataSet();
                DataRow dr = default(DataRow);

                objdata.ReadXml(sFileName);

                TextBox tadd1 = (TextBox)e.Item.Controls[0].FindControl("JCPNO_edit");
                TextBox tadd2 = (TextBox)e.Item.Controls[0].FindControl("JCPNNAME_edit");
                TextBox tadd3 = (TextBox)e.Item.Controls[0].FindControl("JCPPRICE_edit");
                TextBox tadd4 = (TextBox)e.Item.Controls[0].FindControl("JCPPRICE2_edit");
                TextBox tadd5 = (TextBox)e.Item.Controls[0].FindControl("JCPPRICE3_edit");
                TextBox tadd6 = (TextBox)e.Item.Controls[0].FindControl("CURRENCY_edit");
                TextBox tadd7 = (TextBox)e.Item.Controls[0].FindControl("INDIAPRICE_edit");

                try
                {
                    //tadd1.Text = Convert.ToInt32(tadd1.Text);
                    error2.Visible = false;

                    if (Convert.ToInt32(tadd1.Text.Trim()) < 1)
                    {
                        tadd1.Text = "";
                        error2.Visible = true;
                    }
                }
                catch
                {
                    tadd1.Text = "";
                    error2.Visible = true;
                }

                if (!string.IsNullOrEmpty(tadd1.Text) & !string.IsNullOrEmpty(tadd2.Text) & !string.IsNullOrEmpty(tadd3.Text) & !string.IsNullOrEmpty(tadd4.Text) & !string.IsNullOrEmpty(tadd5.Text) & !string.IsNullOrEmpty(tadd6.Text) & !string.IsNullOrEmpty(tadd7.Text))
                {
                    objdata.Tables["ROW"].DefaultView.RowFilter = "JCPNO='" + tadd1.Text + "'";
                    if (objdata.Tables["ROW"].DefaultView.Count <= 0)
                    {
                        error1.Visible = false;
                        objdata.Tables["ROW"].DefaultView.RowFilter = "";
                        dr = objdata.Tables["ROW"].NewRow();

                        dr[0] = tadd1.Text;
                        dr[1] = tadd2.Text;
                        dr[2] = tadd3.Text;
                        dr[3] = tadd4.Text;
                        dr[4] = tadd5.Text;
                        dr[5] = tadd6.Text;
                        dr[6] = tadd7.Text;

                        objdata.Tables["ROW"].Rows.Add(dr);

                        objdata.WriteXml(sFileName);
                        LoadXML(sFileName);
                    }
                    else
                    {
                        error1.Visible = true;
                        error1.Text = "Id must be unique";
                    }
                }
            }
        }
        catch (Exception oex)
        {
            error1.Visible = true;
            error1.Text = oex.Message;
        }
    }

    public object showval(int a)
    {
        lastVarValue = a;
        return a;
    }

    protected void Btn_showpricecode_Click(object sender, EventArgs e)
    {
        string pcFileName = string.Empty;
        if (RB_pricecode.SelectedValue.ToString().ToUpper() == "D")
            pcFileName = ConfigurationManager.ConnectionStrings["dublinPCXML"].ToString();
        else if (RB_pricecode.SelectedValue.ToString().ToUpper() == "I")
            pcFileName = ConfigurationManager.ConnectionStrings["indiaPCXML"].ToString();
        lblFileName.Text = pcFileName.ToString();
        LoadXML(pcFileName);
        div_pricecode.Visible = true;
        div_pcfile.Visible = true;
    }
}

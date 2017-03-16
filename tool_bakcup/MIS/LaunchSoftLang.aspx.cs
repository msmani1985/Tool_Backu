using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class LaunchSoftLang : System.Web.UI.Page
{
    Non_Launch nonLa = new Non_Launch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            loadGrid();
        }
    }
    public void loadGrid()
    {
        DataSet tn = new DataSet();
        tn = nonLa.getSourceDetailsLP(Request.QueryString["LP_ID"].ToString());
        gv_Source.DataSource = tn;
        gv_Source.DataBind();

        tn = nonLa.getTargetDetailsLP(Request.QueryString["LP_ID"].ToString());
        gv_Target.DataSource = tn;
        gv_Target.DataBind();

        if (Request.QueryString["TaskValue"].ToString() == "1" || Request.QueryString["TaskValue"].ToString() == "2" || 
            Request.QueryString["TaskValue"].ToString() == "6" || Request.QueryString["TaskValue"].ToString() == "1,2" ||
            Request.QueryString["TaskValue"].ToString() == "1,6" || Request.QueryString["TaskValue"].ToString() == "1,2,6" || 
            Request.QueryString["TaskValue"].ToString() == "2,6")
        {
            gv_Target.Visible = false;
            gv_Source.Visible = true;
            lblSource.Visible = true;
            lblTarget.Visible = false;
        }
        else
        {
            gv_Target.Visible = true;
            gv_Source.Visible = true;
            lblSource.Visible = true;
            lblTarget.Visible = true;
        }
    }
    protected void gv_Source_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet dscust1 = nonLa.getAllSoftware();
            ListBox lboxSoft = (ListBox)e.Row.FindControl("lboxSoft");
            TextBox txtTargetDate = (TextBox)e.Row.FindControl("txtTargetDate");
            HiddenField task = (HiddenField)e.Row.FindControl("hf_taskID");
            HiddenField lang = (HiddenField)e.Row.FindControl("hf_LangID");
            lboxSoft.DataSource = dscust1;
            lboxSoft.DataTextField = dscust1.Tables[0].Columns[1].ToString();
            lboxSoft.DataValueField = dscust1.Tables[0].Columns[0].ToString();
            lboxSoft.DataBind();
            string soft = "";
            DataSet sv = new DataSet();
            sv = nonLa.SoftSourceLP(Request.QueryString["LP_ID"].ToString(),"0");
            if (sv != null)
            {
                DataSet empd = new DataSet();
                empd = nonLa.GetSourceSoftLP(Request.QueryString["LP_ID"].ToString(), task.Value, lang.Value, "0");
                if (empd != null)
                {
                    if (empd.Tables[0].Rows.Count > 0 || empd != null)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxSoft.Items[lboxSoft.Items.IndexOf(lboxSoft.Items.FindByValue(empd.Tables[0].Rows[i]["Soft_id"].ToString()))].Selected = true;

                            if (soft == "" || soft == null)
                                soft = empd.Tables[0].Rows[i]["Soft_id"].ToString();
                            else
                                soft = soft + ',' + empd.Tables[0].Rows[i]["Soft_id"].ToString();
                        }

                    }
                    ListBox lboxVer = (ListBox)e.Row.FindControl("lboxVer");
                    if (soft.ToString() != "")
                    {
                        DataSet dsSoft = nonLa.GetSoftVers(soft.ToString());
                        lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                        lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                        lboxVer.DataSource = dsSoft;
                        lboxVer.DataBind();
                    }
                    if (empd.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxVer.Items[lboxVer.Items.IndexOf(lboxVer.Items.FindByValue(empd.Tables[0].Rows[i]["Version_id"].ToString()))].Selected = true;

                        }
                    }
                    if (empd.Tables[0].Rows[0]["TARGET_DATE"].ToString() != "")
                        txtTargetDate.Text = empd.Tables[0].Rows[0]["TARGET_DATE"].ToString();
                    else
                        txtTargetDate.Text = "";
                }
            }
        }
    }
    protected void gv_Target_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet dscust1 = nonLa.getAllSoftware();
            ListBox lboxSoft = (ListBox)e.Row.FindControl("lboxSoft");
            TextBox txtTargetDate = (TextBox)e.Row.FindControl("txtTargetDate");
            HiddenField task = (HiddenField)e.Row.FindControl("hf_taskID");
            HiddenField lang = (HiddenField)e.Row.FindControl("hf_LangID");
            lboxSoft.DataSource = dscust1;
            lboxSoft.DataTextField = dscust1.Tables[0].Columns[1].ToString();
            lboxSoft.DataValueField = dscust1.Tables[0].Columns[0].ToString();
            lboxSoft.DataBind();
            string soft = "";
            DataSet sv = new DataSet();
            sv = nonLa.SoftSourceLP(Request.QueryString["LP_ID"].ToString(), "0");
            //sv = nonLa.SoftTargetLP(Request.QueryString["LP_ID"].ToString());
            if (sv != null)
            {
                DataSet empd = new DataSet();
                empd = nonLa.GetSourceSoftLP(Request.QueryString["LP_ID"].ToString(), task.Value, lang.Value, "1");
                //empd = nonLa.GetTargetSoftLP(Request.QueryString["LP_ID"].ToString(), task.Value, lang.Value);
                if (empd != null)
                {
                    if (empd.Tables[0].Rows.Count > 0 || empd != null)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxSoft.Items[lboxSoft.Items.IndexOf(lboxSoft.Items.FindByValue(empd.Tables[0].Rows[i]["Soft_id"].ToString()))].Selected = true;

                            if (soft == "" || soft == null)
                                soft = empd.Tables[0].Rows[i]["Soft_id"].ToString();
                            else
                                soft = soft + ',' + empd.Tables[0].Rows[i]["Soft_id"].ToString();
                        }

                    }
                    ListBox lboxVer = (ListBox)e.Row.FindControl("lboxVer");
                    if (soft.ToString() != "")
                    {
                        DataSet dsSoft = nonLa.GetSoftVers(soft.ToString());
                        lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                        lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                        lboxVer.DataSource = dsSoft;
                        lboxVer.DataBind();
                    }
                    if (empd.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxVer.Items[lboxVer.Items.IndexOf(lboxVer.Items.FindByValue(empd.Tables[0].Rows[i]["Version_id"].ToString()))].Selected = true;

                        }
                    }
                    if (empd.Tables[0].Rows[0]["TARGET_DATE"].ToString() != "")
                        txtTargetDate.Text = empd.Tables[0].Rows[0]["TARGET_DATE"].ToString();
                    else
                        txtTargetDate.Text = "";
                }
            }
        }
    }
    protected void lboxSourceSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow grs in gv_Source.Rows)
        {
            DataSet dscust1 = nonLa.getAllSoftware();
            ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
            Label task = (Label)grs.FindControl("txt_task");
            ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
            string sv = "";
            for (int j = 0; j < lboxSoft.Items.Count; j++)
            {
                if (lboxSoft.Items[j].Selected == true)
                {
                    if (sv == "")
                        sv = lboxSoft.Items[j].Value;
                    else
                        sv = sv + ',' + lboxSoft.Items[j].Value;
                }
            }
            if (sv.ToString() != "")
            {
                DataSet dsSoft = nonLa.GetSoftVers(sv.ToString());
                lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                lboxVer.DataSource = dsSoft;
                lboxVer.DataBind();
            }
        }
    }
    protected void lboxTargetSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow grs in gv_Target.Rows)
        {
            DataSet dscust1 = nonLa.getAllSoftware();
            ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
            Label task = (Label)grs.FindControl("txt_task");
            ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
            string sv = "";
            for (int j = 0; j < lboxSoft.Items.Count; j++)
            {
                if (lboxSoft.Items[j].Selected == true)
                {
                    if (sv == "")
                        sv = lboxSoft.Items[j].Value;
                    else
                        sv = sv + ',' + lboxSoft.Items[j].Value;
                }
            }
            if (sv.ToString() != "")
            {
                DataSet dsSoft = nonLa.GetSoftVers(sv.ToString());
                lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                lboxVer.DataSource = dsSoft;
                lboxVer.DataBind();
            }
        }
    }
    protected void gv_Source_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Launch_SQL SqlObj = new Launch_SQL();
        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
        if (e.CommandName == "Update1")
        {
            GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            paramCol = e.CommandArgument.ToString() + ",Update," + ((ListBox)row.FindControl("lboxVer")).Text.ToString() + "," + ((ListBox)row.FindControl("lboxSoft")).Text.ToString() + "," + Request.QueryString["TaskValue"].ToString();
            paramname = "@NTLS_ID,@Status,@SWVer,@SW,@TaskValue";
            paramtype = "Int,varchar,Int,Int,varchar";
            paramdir = "Input,Input,Input,Input,Input";
        }
        if (e.CommandName == "Delete1")
        {
            GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            paramCol = e.CommandArgument.ToString() + ",Delete," + ((ListBox)row.FindControl("lboxVer")).Text.ToString() + "," + ((ListBox)row.FindControl("lboxSoft")).Text.ToString() + "," + Request.QueryString["TaskValue"].ToString();
            paramname = "@NTLS_ID,@Status,@SWVer,@SW,@TaskValue";
            paramtype = "Int,varchar,Int,Int,varchar";
            paramdir = "Input,Input,Input,Input,Input";
        }
        SqlObj.ExcuteProcedure("spUpdateSourceTarSoft", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
        SqlObj = null;
        loadGrid();
        ScriptManager.RegisterStartupScript(this.Page, GetType(), "ClosePopup", "window.opener.document.getElementById(\"xlang\").click();", true);
    }
    protected void gv_Target_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Launch_SQL SqlObj = new Launch_SQL();
        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
        if (e.CommandName == "Update1")
        {
            GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            paramCol = e.CommandArgument.ToString() + ",Update," + ((ListBox)row.FindControl("lboxVer")).Text.ToString() + "," + ((ListBox)row.FindControl("lboxSoft")).Text.ToString() + "," + Request.QueryString["TaskValue"].ToString();
            paramname = "@NTLS_ID,@Status,@SWVer,@SW,@TaskValue";
            paramtype = "Int,varchar,Int,Int,varchar";
            paramdir = "Input,Input,Input,Input,Input";
        }
        if (e.CommandName == "Delete1")
        {
            GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            paramCol = e.CommandArgument.ToString() + ",Delete," + ((ListBox)row.FindControl("lboxVer")).Text.ToString() + "," + ((ListBox)row.FindControl("lboxSoft")).Text.ToString() + "," + Request.QueryString["TaskValue"].ToString();
            paramname = "@NTLS_ID,@Status,@SWVer,@SW,@TaskValue";
            paramtype = "Int,varchar,Int,Int,varchar";
            paramdir = "Input,Input,Input,Input,Input";
        }
        SqlObj.ExcuteProcedure("spUpdateSourceTarSoft", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
        SqlObj = null;
        loadGrid();
        ScriptManager.RegisterStartupScript(this.Page, GetType(), "ClosePopup", "window.opener.document.getElementById(\"xlang\").click();", true);
    }
}
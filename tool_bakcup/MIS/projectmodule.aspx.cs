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

public partial class projectmodule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            datasourceIBSQL mobj = new datasourceIBSQL();
            DataSet mds = new DataSet();
            mds = mobj.Getproject_details();
            ddl_projectlist.DataSource = mds;
            ddl_projectlist.DataBind();
            ddl_custlist.DataSource = mds;
            ddl_custlist.DataBind();
            ddl_ponumber.DataSource = mds;
            ddl_ponumber.DataBind();
            ddl_projectlist.Items.Insert(0,new ListItem("-- Select a Project --","0"));
            ddl_custlist.Items.Insert(0,new ListItem("-- Select a Customer --","0"));
            ddl_ponumber.Items.Insert(0,new ListItem("-- Select a PONumber --","0"));
        }
    }
    protected void btn_createmodule_Click(object sender, EventArgs e)
    {
        ddl_custlist.SelectedIndex=ddl_projectlist.SelectedIndex;
        ddl_ponumber.SelectedIndex = ddl_projectlist.SelectedIndex;
        string[] aModuleDetails;
        datasourceIBSQL inobj = new datasourceIBSQL();
        Delphi_Module dmobj = new Delphi_Module();
        int intNoOfTimes = 0;
        try
        {
            if (txt_nofoitems.Text != "0" && txt_nofoitems.Text != "")
            {
                aModuleDetails = new string[6];
                aModuleDetails[0] = ddl_projectlist.SelectedValue.ToString();
                aModuleDetails[1] = ddl_custlist.SelectedValue.ToString();
                aModuleDetails[2] = ddl_ponumber.SelectedItem.Text.Trim().ToString();
                aModuleDetails[3] = "11";
                aModuleDetails[4] = "15";
                aModuleDetails[5] = "10077";
                if (Convert.ToString(txt_nofoitems.Text).Trim() == "")
                {
                    Alert("Please enter the no of items.");
                    return;
                }
                intNoOfTimes = Convert.ToInt32(txt_nofoitems.Text.Trim());
                string msg = dmobj.InsertModule(aModuleDetails, intNoOfTimes);
                if (msg.Contains("A"))
                {
                    LoadGrid();
                    Alert("Module Already Created!");
                }
                else
                {
                    LoadGrid();
                    //Alert("Successfully Saved.");
                }

                //if (!inobj.insertprojectmodule(ddl_projectlist.SelectedValue.ToString(), ddl_custlist.SelectedValue.ToString(), txt_nofoitems.Text.ToString(),ddl_ponumber.SelectedItem.Text.Trim().ToString()))
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insertion failed');</script>");
                ////else
                ////    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Created');</script>");
            }
            else
            {
                LoadGrid();
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { inobj = null; }
    }
   
    protected DataSet loadpename()
    {
        datasourceIBSQL peobj = new datasourceIBSQL();
        DataSet peds = new DataSet();
        try
        {
            peds = peobj.GetPEContact();
            DataRow dr = peds.Tables[0].NewRow();
            dr["conno"] = "0";
            dr["invdisplayname"] = "--Select--";
            peds.Tables[0].Rows.InsertAt(dr,0);
            return peds;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { peobj = null; }
    }
    protected void ibtn_save_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList al = new ArrayList();
        Hashtable  val = null;
        datasourceIBSQL mobj=new datasourceIBSQL();
        try
        {
            foreach (GridViewRow grw in gv_pmodule.Rows)
            {
                string numpages;
                if (((TextBox)grw.FindControl("txt_qty")).Text.Trim().ToString() == "")
                    numpages = "0";
                else
                    numpages = ((TextBox)grw.FindControl("txt_qty")).Text.Trim().ToString();

                val = new Hashtable();
                val.Add("MPTITLE", ((TextBox)grw.FindControl("txt_des")).Text.Trim().ToString());
                val.Add("NUMPAGES", numpages);
                val.Add("PRICECODE", ((TextBox)grw.FindControl("txt_pricecode")).Text.Trim().ToString());
                val.Add("COSTTYPEID", ((DropDownList)grw.FindControl("ddl_costtype")).SelectedValue.ToString());
                val.Add("PAGEDESCRIPTIONID", ((DropDownList)grw.FindControl("ddl_description")).SelectedValue.ToString());
                val.Add("CONNO", ((DropDownList)grw.FindControl("ddl_pename")).SelectedValue.ToString());
                val.Add("MPROJNO", ((HiddenField)grw.FindControl("hf_rowmoduleno")).Value);
                val.Add("MOPONUMBER", ((TextBox)grw.FindControl("txt_mponumber")).Text.Trim().ToString());
                al.Add(val);
            }
            if (!mobj.Update_ProjectModule(al))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Saved');</script>");
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mobj = null; }
        
        
    }
    protected void ibtn_Delete_click(object sender, ImageClickEventArgs e)
    {
        string uval = string.Empty;
        foreach (GridViewRow gr in gv_pmodule.Rows)
        {
            if (((CheckBox)gr.FindControl("cb_delete")).Checked)
            { uval = ((HiddenField)gr.FindControl("hf_rowmoduleno")).Value + "," ; }
        }
        if (!string.IsNullOrEmpty(uval))
        {
            uval = uval.TrimEnd(',');
            datasourceIBSQL mobj = new datasourceIBSQL();
            if (mobj.DeleteModules(uval))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Deleted');</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Deletion Failed');</script>");
            LoadGrid();
        }

    }
    private void LoadGrid()
    {
        datasourceIBSQL inobj = new datasourceIBSQL();
        DataSet inds = new DataSet();
        try
        {
            gv_pmodule.Visible = true;
            gv_pmodule.DataSource = inobj.GetProject_Modules(ddl_projectlist.SelectedValue.ToString());
            gv_pmodule.DataBind();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { inds = null; inobj = null; }
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void gv_pmodule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         datasourceIBSQL peobj = new datasourceIBSQL();
        DataSet peds = new DataSet();
        try
        {
            DropDownList drpConn = (DropDownList)e.Row.FindControl("ddl_pename");
            if (drpConn != null)
            {
                peds = peobj.GetPEContact();
                DataRow dr = peds.Tables[0].NewRow();
                dr["conno"] = 0;
                dr["invdisplayname"] = "--Select--";
                peds.Tables[0].Rows.InsertAt(dr, 0);
                drpConn.DataSource = peds;
                drpConn.DataTextField = "invdisplayname";
                drpConn.DataValueField = "CONNO";
                drpConn.DataBind();
                drpConn.SelectedValue = DataBinder.Eval(e.Row.DataItem, "CONNO").ToString();
            }
        }
        catch (Exception Ex)
        {
            Alert(Ex.Message.ToString());
        }
    }
    protected void btnLangWork_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", "USDConvertorModule.aspx"));
    }
}

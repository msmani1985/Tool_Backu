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


public partial class menuheading : System.Web.UI.Page
{
    string paramcol = "";
    string paramname = "";
    string paramtype = "";
    string paramdir = "";
    string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!Page.IsPostBack)
        {
            LoadControl();
        }
    }

    private void BuildListItems(ListControl oListBox, DataSet oDs)
    {
        int i = 0;
        if (oDs.Tables[0].Rows.Count > 0)
        {
            oListBox.Items.Clear();
            for (i = 0; i < oDs.Tables[0].Rows.Count; i++)
                oListBox.Items.Add(new ListItem(oDs.Tables[0].Rows[i][1].ToString(), oDs.Tables[0].Rows[i][0].ToString()));
        }
    }
    private void BuildDDLItems(DropDownList oDDListBox, DataSet oDs)
    {
        int i = 0;
        if (oDs.Tables[0].Rows.Count > 0)
        {
            oDDListBox.Items.Clear();
            for (i = 0; i < oDs.Tables[0].Rows.Count; i++)
                oDDListBox.Items.Add(new ListItem(oDs.Tables[0].Rows[i][1].ToString(), oDs.Tables[0].Rows[i][0].ToString()));
        }
    }
    protected void btnCreatMenuItem_Click(object sender, EventArgs e)
    {
        string mgrp="";
        for(int k=0;k<lstmenugroup.Items.Count;k++)
        {
            if (lstmenugroup.Items[k].Selected == true && mgrp=="")
                mgrp =lstmenugroup.Items[k].Value;
            else if (lstmenugroup.Items[k].Selected == true && mgrp != "")
                mgrp = mgrp + "-" + lstmenugroup.Items[k].Value;
        }

        paramcol = txtItemName.Text + "," + lstMenuHeading1.SelectedValue + "," + txtItemFile.Text + "," + txtOrderIndex.Text + "," + mgrp + ",0";
        paramname = "@menuitemname,@menuheadingid,@menuitemfile,@orderindex,@menugroupid,@IsExists";
        paramtype = "varchar,int,varchar,int,VARCHAR,int";
        paramdir = "Input,Input,Input,Input,Input,Output";
        msg = "";
        int isexists;
        datasourceSQL sobj = new datasourceSQL();
        try
        {
            isexists = sobj.ExcuteProcedure("spAdd_MenuItem", paramcol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            if (isexists == 1)
                msg = "Successfully Inserted";
            else
                msg = "Already Exists";
            LoadControl();
        }
        catch (Exception ex)
        {
            sobj = null;
            throw ex;
        }
        finally
        {
            sobj = null;
            txtItemName.Text = "";
            lstMenuHeading1.ClearSelection();
            txtItemFile.Text = "";
            txtOrderIndex.Text = "";
            lstmenugroup.ClearSelection();
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
        }
        //return null;
    }
    protected void btnCreateMenu_Click(object sender, EventArgs e)
    {
         paramcol=newmenuheading.Text + ","+ MHorderindexTxt.Text + ",0";
         paramname = "@menuhname,@orderindex,@IsExist";
         paramtype = "varchar,int,int";
         paramdir = "Input,Input,Output";
         msg="";
        int isexist;
        datasourceSQL sobj=new datasourceSQL();
        try
        {
            isexist = sobj.ExcuteProcedure("spAdd_menuHeading", paramcol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            if (isexist == 1)
                msg = "Successfully Inserted";
            else
                msg = "Already Exists";
            LoadControl();
        }
        catch (Exception ex)
        {
            sobj = null;
            throw ex;

        }
        finally
        {
            sobj = null;
            newmenuheading.Text = "";
            MHorderindexTxt.Text = "";
            ClientScript.RegisterStartupScript(this.GetType(),"Open", "<script language='javascript'>alert('"+ msg +"');</script>");
        }

        //return null;
    }
    private void LoadControl()
    {
        biz_emp_menu_mgmt biz = new biz_emp_menu_mgmt();
        string sHTMLTable = "";
        sHTMLTable = biz.GetMenuListHTML();
        if (sHTMLTable != "")
            itemandheadings.InnerHtml = sHTMLTable;
        DataSet ds = new DataSet();
        ds = biz.GetMenuHeadings();

        //BuildListItems(lstMenuHeading, ds);
        lstMenuHeading.DataSource = ds;
        lstMenuHeading.DataBind();
        //BuildListItems(lstMenuHeading1, ds);
        lstMenuHeading1.DataSource = ds;
        lstMenuHeading1.DataBind();
        ds = new DataSet();
        ds = biz.GetMenuGroups();
        //BuildListItems(lstmenugroup, ds);
        lstmenugroup.DataSource = ds;
        lstmenugroup.DataBind();
        ds = new DataSet();
        ds = biz.GetMenuItems();
        //BuildListItems(lstMenuItem, ds);
         //LoadMenuItem(lstMenuItem,ds);
         lstMenuItem.DataSource = ds;
         lstMenuItem.DataBind();
         
        ds = null;
        biz = null;
    }
    protected void lstMenuItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        //char[] sep=new char[] {'_'};
        //char[] sep2 = new char[] { ',' };
        datasourceSQL menusql = new datasourceSQL();
        DataSet mds = new DataSet();

        //id = lstMenuItem.SelectedValue.ToString().Split(sep);
        //lstMenuHeading1.SelectedValue = id[1].ToString();

        string[,] mparam ={ { "@menuitem_id", lstMenuItem.SelectedValue.ToString() } };
        try
        {

            mds = menusql.ExcProcedure("spGetParticular_MenuItem", mparam, CommandType.StoredProcedure);

            txtItemName.Text = mds.Tables[0].Rows[0]["menu_item_name"].ToString();
            txtItemFile.Text = mds.Tables[0].Rows[0]["menu_item_file"].ToString();
            txtOrderIndex.Text = mds.Tables[0].Rows[0]["order_index"].ToString();

            lstMenuHeading1.SelectedValue = mds.Tables[0].Rows[0]["menu_heading_id"].ToString();
            for (int i = 0; i < lstmenugroup.Items.Count; i++)
                lstmenugroup.Items[i].Selected = false;
            for (int j = 0; j < mds.Tables[0].Rows.Count; j++)
                lstmenugroup.Items[lstmenugroup.Items.IndexOf(lstmenugroup.Items.FindByValue(mds.Tables[0].Rows[j]["menu_group_id"].ToString()))].Selected = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { mds = null; menusql = null; }


       
    }
    protected void BtnUpdateMenuitem_Click(object sender, EventArgs e)
    {
        datasourceSQL menuobj = new datasourceSQL();
        string menu_item = "";
        string[] menu_itemid=lstMenuItem.SelectedValue.ToString().Split('_');
        try
        {
            for (int g = 0; g < lstmenugroup.Items.Count; g++)
            {
                if (lstmenugroup.Items[g].Selected == true)
                {
                    if (menu_item == "")
                        menu_item = lstmenugroup.Items[g].Value;
                    else
                        menu_item = menu_item + '-' + lstmenugroup.Items[g].Value;
                }
            }
            string[,] itemparm ={ { "@menu_heading_id", lstMenuHeading1.SelectedValue.ToString() }, { "@menu_item_id", menu_itemid[0] }, { "@menu_groups", menu_item }, { "@menu_item", txtItemName.Text }, { "@menu_item_file", txtItemFile.Text }, { "@orderindex", txtOrderIndex.Text } };
            //menuobj.ExcuteProcedure("SPADD_MENU_GROUP_ITEM", menu_itemid[0] + "," + menu_item, "@menu_item_id,@menu_groups", "int,varchar", "input,input", CommandType.StoredProcedure);
            menuobj.ExcSProcedure("SPADD_MENU_GROUP_ITEM", itemparm, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Successfully Updated');</script>");
        }
    }
   /* private void LoadMenuItem(DropDownList oDDListBox, DataSet oDs)
    {
        int i = 0;
        datasourceSQL sqlobj = new datasourceSQL();
        DataSet sqlds = new DataSet();
        if (oDs.Tables[0].Rows.Count > 0)
        {
            oDDListBox.Items.Clear();
            try
            {
                for (i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {


                    string valuefield = "";
                    string[,] param={   {"@menuitemid",oDs.Tables[0].Rows[i]["menu_item_id"].ToString()}  };
                    //sqlds = sqlobj.ExcuteSelectProcedure("spGet_MenuGroup", oDs.Tables[0].Rows[i]["menu_item_id"].ToString(), "@menuitemid", "int", "Input", CommandType.StoredProcedure);
                    sqlds = sqlobj.ExcProcedure("spGet_MenuGroup",param,CommandType.StoredProcedure);
                    if (sqlds.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < sqlds.Tables[0].Rows.Count; j++)
                        {
                            if (valuefield == "")
                                valuefield = oDs.Tables[0].Rows[i]["menu_item_id"].ToString() + "_" + oDs.Tables[0].Rows[i]["menu_heading_id"].ToString() + "_" + sqlds.Tables[0].Rows[j]["menu_group_id"].ToString();
                            else
                                valuefield = valuefield + "," + sqlds.Tables[0].Rows[j]["menu_group_id"].ToString();
                        }
                        oDDListBox.Items.Add(new ListItem(oDs.Tables[0].Rows[i]["menu_item_name"].ToString(), valuefield));

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlds = null;
                sqlobj = null;
                oDs = null;
            }

                //oDDListBox.Items.Add(new ListItem(oDs.Tables[0].Rows[i][1].ToString(), oDs.Tables[0].Rows[i][0].ToString()));
           
           
        }
    }*/

    
}

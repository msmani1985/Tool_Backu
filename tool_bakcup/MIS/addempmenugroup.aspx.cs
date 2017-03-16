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

public partial class addempmenugroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadListControl(EmployeeList, "spGet_AllEmployee");
            LoadListControl(groupsList, "spGet_MenuGroups");
        }
    }
    
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        datasourceSQL inobj = new datasourceSQL();
        string groupitem = "";
        try
        {
            for (int j = 0; j < groupsList.Items.Count; j++)
            {
                if (groupsList.Items[j].Selected == true)
                {
                    if (groupitem == "")
                        groupitem = groupsList.Items[j].Value;
                    else
                        groupitem = groupitem + '-' + groupsList.Items[j].Value;
                }
            }
            inobj.ExcuteProcedure("spAdd_Employeemenu_group", EmployeeList.SelectedValue + "," + groupitem, "@emp_id,@menu_group_id", "int,varchar", "input,input", CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            inobj = null;
            throw ex;
        }
        finally
        {
            inobj = null;
            ClientScript.RegisterStartupScript(this.GetType(),"Open","<script language='javascript'>alert('Successfully Added');</script>");
        }
    }
    private void LoadListControl(ListBox lstbox, string procName)
    {
        datasourceSQL lstobj = new datasourceSQL();
        DataSet lstds = new DataSet();
        try
        {
            lstds = lstobj.ExcuteSelectProcedure(procName, "", "", "", "", CommandType.StoredProcedure);
            lstbox.DataSource = lstds.Tables[0];
            lstbox.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            lstobj = null;
            lstds = null;
        }
    }
    protected void EmployeeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int j = 0; j < groupsList.Items.Count; j++)
            groupsList.Items[j].Selected = false;
        datasourceSQL empobj = new datasourceSQL();
        DataSet empds = new DataSet();
        try
        {
            empds = empobj.ExcuteSelectProcedure("spGet_empGroups", EmployeeList.SelectedValue, "@emp_id", "int", "input", CommandType.StoredProcedure);
            if (empds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < empds.Tables[0].Rows.Count; i++)
                    groupsList.Items[groupsList.Items.IndexOf(groupsList.Items.FindByValue(empds.Tables[0].Rows[i]["menu_group_id"].ToString()))].Selected = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            empds = null;
            empobj = null;
        }
    }
}

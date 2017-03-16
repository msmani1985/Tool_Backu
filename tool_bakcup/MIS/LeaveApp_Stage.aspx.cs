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

public partial class LeaveApp_Stage : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    protected void grvLvl_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList lvl1 = (DropDownList)e.Row.FindControl("level1");
            Label empid = (Label)e.Row.FindControl("ID");
            DataSet Ds1 = new DataSet();
            Ds1 = SqlObj.GetTeamHeads(Convert.ToInt16(Session["locationid"].ToString()));
            lvl1.DataSource = Ds1;
            lvl1.DataTextField = "EMP_FULLNAME";
            lvl1.DataValueField = "EMPLOYEE_ID";
            lvl1.DataBind();
            DataSet ds = new DataSet();
            ds = SqlObj.GetEmpStage(Convert.ToInt16(empid.Text));
            int i = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[i]["Level1"].ToString()!="")
                {
                    lvl1.Items[lvl1.Items.IndexOf(lvl1.Items.FindByValue(ds.Tables[0].Rows[i]["Level1"].ToString()))].Selected = true;
                }
                else
                    lvl1.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            //lvl1.Items.Insert(0, new ListItem("--Select--", "0"));
            DropDownList lvl2 = (DropDownList)e.Row.FindControl("level2");
            DataSet Ds2 = new DataSet();
            Ds2 = SqlObj.GetTeamHeads(Convert.ToInt16(Session["locationid"].ToString()));
            lvl2.DataSource = Ds2;
            lvl2.DataTextField = "EMP_FULLNAME";
            lvl2.DataValueField = "EMPLOYEE_ID";
            lvl2.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[i]["Level2"].ToString() != "")
                {
                    lvl2.Items[lvl2.Items.IndexOf(lvl2.Items.FindByValue(ds.Tables[0].Rows[i]["Level2"].ToString()))].Selected = true;
                }
                else
                    lvl2.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            //lvl2.Items.Insert(0, new ListItem("--Select--", "0"));
           
        }
        
    }
    protected void cmd_Save_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList al = new ArrayList();
        Hashtable val = null;
        try
        {
            foreach (GridViewRow grw in grvLvl.Rows)
            {
                val = new Hashtable();

                val.Add("ID", ((Label)grw.FindControl("ID")).Text.Trim().ToString());
                //val.Add("EmpID", ((Label)grw.FindControl("EmpID")).Text.Trim().ToString());
                val.Add("lev1", ((DropDownList)grw.FindControl("level1")).SelectedValue.ToString());
                val.Add("lev2", ((DropDownList)grw.FindControl("level2")).SelectedValue.ToString());
                al.Add(val);
            }
            if (!SqlObj.Update_StageDetails(al))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { SqlObj = null; }
    }
    protected void ibtngridview_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = SqlObj.GetAllEmployee(Convert.ToInt16(Session["employeeid"].ToString()), Convert.ToInt16(Session["locationid"].ToString()));
        grvLvl.DataSource = ds;
        grvLvl.DataBind();
    }
   
}

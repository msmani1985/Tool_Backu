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
using System.Text;

public partial class TeamBuilder : System.Web.UI.Page
{
    datasourceSQL mtbsql = new datasourceSQL();

    protected void Page_Load(object sender,EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            datasourceSQL sqltb = new datasourceSQL();

            DataSet tb = new DataSet();
             
            tb = mtbsql.selectTeam();
            ddl_teamname.DataTextField = "employee_team_name";
            ddl_teamname.DataValueField = "employee_team_id";
            ddl_teamname.DataSource = tb;
            ddl_teamname.DataBind();
            ListItem lst = new ListItem("---Select---", "0");
            ddl_teamname.Items.Insert(0, lst);
            ddl_teamname.SelectedIndex = 0;
            tb = mtbsql.Getempname(ddl_teamname.SelectedValue.ToString());
            List_employee.DataTextField = "fname";
            List_employee.DataValueField = "employee_id";
            List_employee.DataSource = tb;
            List_employee.DataBind();
           
            
            for (int i = 0; i < tb.Tables[0].Rows.Count - 1; i++)
            {
                List_employee.Items.Add(new ListItem(tb.Tables[0].Rows[i]["fname"].ToString(), tb.Tables[0].Rows[i]["employee_id"].ToString()));
            }
        }
          
      }
            
    //protected void imgbtnadd_Click(object sender, ImageClickEventArgs e)
    //{
         
    //    lb_teammembers.Items.Add(List_employee.SelectedItem.ToString());
    //   // List_employee.Items.Remove(lb_teammembers.SelectedItem.ToString());
    //}
    //protected void imgbtnremove_Click(object sender, ImageClickEventArgs e)
    //{
    //    lb_teammembers.Items.Remove(lb_teammembers.SelectedItem.ToString());
    //}
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ddl_teamname.SelectedIndex = 0;
        lb_teammembers.Items.Clear();
        DataSet tb = new DataSet();
        tb = mtbsql.Getempname(ddl_teamname.SelectedValue.ToString());
        List_employee.DataTextField = "fname";
        List_employee.DataValueField = "employee_id";
        List_employee.DataSource = tb;
        List_employee.DataBind();
        lblresult.Text = "";
             
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        datasourceSQL lbsql = new datasourceSQL();
        DataSet lds = new DataSet();

        //lds = lbsql.checkname(ddl_teamname.SelectedValue.ToString(), lb_teammembers.SelectedValue.ToString());


        //if (lds != null && lds.Tables[0].Rows.Count > 0)
        //{
            //lblresult.Text = "This Record was already Exist!";
        //}
        //else
        //{
            for (int j = 0; j < lb_teammembers.Items.Count; j++)
            {
                lds = lbsql.checkname(ddl_teamname.SelectedValue.ToString(), lb_teammembers.Items[j].Value.ToString());
                if (lds == null || lds.Tables[0].Rows.Count == 0)
                {
                    lbsql.insertnew(ddl_teamname.SelectedValue.ToString(), lb_teammembers.Items[j].Value.ToString());
                    //lds = (lds.Tables[0].Rows[0]["team_owner_id"]);
                    lbsql.updatenew(lb_teammembers.Items[j].Value.ToString(),ddl_teamname.SelectedValue.ToString());
                    lblresult.Text = "The Record was Saved Successfully!";

                }

            }                
        //}

    }

     protected void imgbtnadd_Click(object sender, ImageClickEventArgs e)

     {

    for (int i = List_employee.Items.Count - 1; i >= 0; i--) 
                        {
                            if (List_employee.Items[i].Selected == true) 
                                {
                                    lb_teammembers.Items.Add(List_employee.Items[i]);
                                    ListItem li = List_employee.Items[i];
                                    List_employee.Items.Remove(li); 
                                } 

                        } 

      }

    protected void imgbtnremove_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = lb_teammembers.Items.Count - 1; i >= 0; i--)
        {
            if (lb_teammembers.Items[i].Selected == true)
            {
                List_employee.Items.Add(lb_teammembers.Items[i]);
                ListItem li = lb_teammembers.Items[i];
                lb_teammembers.Items.Remove(li);
            }
        }
    }
    
    protected void ddl_teamname_SelectedIndexChanged(object sender, EventArgs e)
    {
        datasourceSQL mtbsql1 = new datasourceSQL();
        DataSet tb1 = new DataSet();
                
        tb1 = mtbsql1.fillteam(ddl_teamname.SelectedValue.ToString());
        lb_teammembers.DataTextField = "fname";
        lb_teammembers.DataValueField = "employee_id";
        if(tb1==null)
            lb_teammembers.Items.Clear();       

        lb_teammembers.DataSource = tb1;
        lb_teammembers.DataBind();
        tb1 = mtbsql.Getempname(ddl_teamname.SelectedValue.ToString());
        List_employee.DataTextField = "fname";
        List_employee.DataValueField = "employee_id";
        List_employee.DataSource = tb1;
        List_employee.DataBind();


     }
}  
   
                
  
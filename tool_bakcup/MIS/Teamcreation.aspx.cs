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


public partial class Teamcreation : System.Web.UI.Page
{
    
    datasourceSQL sqlobj = new datasourceSQL();
    // SqlConnection cnn = new  conSqlConnection("conStrSQL");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            DataSet ds = new DataSet();
            ds = sqlobj.GetDepart();
            dropdepart.DataSource = ds;
            dropdepart.DataBind();

            //sqlobj.GetLead();
            //ds = sqlobj.GetLead();
            //Droplead.DataSource = ds;
            //Droplead.DataBind();

            //sqlobj.GetGroupby();
            ds = sqlobj.GetGroupby();
            //Dropgroupby.DataSource = ds;
            //Dropgroupby.DataBind();

            lb_groupby_team.DataSource = ds;
            lb_groupby_team.DataBind();

            DataSet Dst = new DataSet();
            datasourceSQL DSql = new datasourceSQL();
            Dst = DSql.searchdrop();
            Dropsearch.DataTextField = "employee_team_name";
            Dropsearch.DataValueField = "employee_team_id";
            Dropsearch.DataSource = Dst;
            Dropsearch.DataBind();
           

            dropdepart.Items.Insert(0, "----select----");
            dropdepart.SelectedIndex = 0;

            //Droplead.Items.Insert(0, new ListItem("----select----","null"));
            //Droplead.SelectedIndex = 0;

            dd_location.DataSource = DSql.getlocation();
            dd_location.DataBind();
            dd_location.Items.Insert(0, new ListItem("---select---","null"));
            dd_location.SelectedIndex = 0;

            //Dropgroupby.Items.Insert(0, "----select----");
            //Dropgroupby.SelectedIndex = 0;

            lb_groupby_team.Items.Insert(0, new ListItem("----select----","null"));
            lb_groupby_team.SelectedIndex = 0;

            Dropsearch.Items.Insert(0, "----select---");
            Dropsearch.SelectedIndex = 0;

            lb_groupby_team.SelectionMode = ListSelectionMode.Single;
            Label5.Visible = false;
            lb_groupby_team.Visible = false;

        }
    }


    protected void btncreate_Click(object sender, EventArgs e)
    {
         
        datasourceSQL sqlob4 = new datasourceSQL();
        DataSet tds = new DataSet();
        tds = sqlob4.chkname(txttname.Text,"");

        if (dd_location.SelectedValue.ToString() == "null")
        { lblresults.Text = "Fill in all mandatory fields"; return; }
        
        if (tds != null && tds.Tables[0].Rows.Count > 0)
        {
            lblresults.Text = "This TeamName was already exist!";

        }
        else
        {
            string groupby = string.Empty;
            for (int gcnt = 0; gcnt < lb_groupby_team.Items.Count; gcnt++)
            {
                if (lb_groupby_team.Items[gcnt].Selected == true)
                    groupby += lb_groupby_team.Items[gcnt].Value.ToString();
            }
            //sqlobj.insertdet(txttname.Text, dropdepart.SelectedValue.ToString(), Droplead.SelectedValue.ToString(), groupby.ToString(), Convert.ToInt16(txtindexorder.Text));
            sqlobj.insertdet(txttname.Text, dropdepart.SelectedValue.ToString(), "null", groupby.ToString(), Convert.ToInt16(txtindexorder.Text),dd_location.SelectedValue.ToString());
            lblresults.Text = "The Record was successfully inserted!";
            
        }


    }

    private void createcontrol()
    {

       
        btnupdate.Enabled = false;
        Dropsearch.SelectedIndex = 0;
        txttname.Text = "";
        dropdepart.SelectedIndex = 0;
        //Droplead.SelectedIndex = 0;
        lb_groupby_team.SelectedIndex = 0;
        txtindexorder.Text = "";
        lblresults.Text = "";
        lb_groupby_team.SelectionMode = ListSelectionMode.Single;
        Label5.Visible = false;
        lb_groupby_team.Visible = false;
        dd_location.SelectedIndex = 0;
        
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        
       
        createcontrol();
         
    }
    
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        lb_groupby_team.SelectionMode = ListSelectionMode.Multiple;

        DataSet sds = new DataSet();
        sds = sqlobj.chkname("", Dropsearch.SelectedValue.ToString());
        if (sds != null && sds.Tables[0].Rows.Count > 0)
        {
            lblresults.Text = "";
            btnupdate.Enabled = true;
            Dropsearch.SelectedValue = sds.Tables[0].Rows[0]["employee_team_id"].ToString();
            txttname.Text = sds.Tables[0].Rows[0]["employee_team_name"].ToString();
            dropdepart.SelectedValue = sds.Tables[0].Rows[0]["department_id"].ToString();
            dd_location.SelectedValue = (sds.Tables[0].Rows[0]["location_id"].ToString() != "") ? sds.Tables[0].Rows[0]["location_id"].ToString() : "null";
            //Droplead.SelectedValue = (sds.Tables[0].Rows[0]["team_owner_id"].ToString() != "" ? sds.Tables[0].Rows[0]["team_owner_id"].ToString() : "null");
            
            if (sds.Tables[0].Rows[0]["grouped_by"].ToString() == "")
            {
                lb_groupby_team.SelectedIndex = 0;
            }
            else
            {
                lb_groupby_team.ClearSelection();
                for (int gnt = 0; gnt < lb_groupby_team.Items.Count; gnt++)
                {
                    if (lb_groupby_team.Items[gnt].Value == sds.Tables[0].Rows[0]["grouped_by"].ToString())
                        lb_groupby_team.Items[gnt].Selected = true;
                }
            }
                txtindexorder.Text = sds.Tables[0].Rows[0]["order_index"].ToString();
                lb_groupby_team.Items[0].Selected = false;
                for (int rowcnt = 0; rowcnt < sds.Tables[1].Rows.Count; rowcnt++)
                {
                    foreach (ListItem it in lb_groupby_team.Items)
                    {
                        if (it.Value == sds.Tables[1].Rows[rowcnt]["team_group_owner_id"].ToString())
                            it.Selected = true;
                        //else
                        //    it.Selected = false;
                    }
                    //lb_groupby_team.SelectedIndex  = lb_groupby_team.Items.IndexOf(lb_groupby_team.Items.FindByValue(sds.Tables[1].Rows[rowcnt]["team_group_owner_id"].ToString()));
                        //((ListItem)lb_groupby_team.Items.FindByValue(sds.Tables[1].Rows[rowcnt]["team_group_owner_id"].ToString())).Value.ToString();
                }

                Label5.Visible = true;
                lb_groupby_team.Visible = true;
        }
        else
        {
            lblresults.Text = "No Records Found";
            createcontrol();
            // btnupdate.Enabled = false;
            Label5.Visible = false;
            lb_groupby_team.Visible = false;
        }

    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        
        datasourceSQL sqlob = new datasourceSQL();
        string groupby = string.Empty;
        for (int gcnt = 0; gcnt < lb_groupby_team.Items.Count; gcnt++)
        {
            if (lb_groupby_team.Items[gcnt].Selected == true)
            {
                groupby += lb_groupby_team.Items[gcnt].Value.ToString() + ",";
            }
        }
        //sqlob.updateteam(txttname.Text, dropdepart.SelectedValue.ToString(), Droplead.SelectedValue.ToString(), groupby.TrimEnd(','), Convert.ToInt16(txtindexorder.Text), Dropsearch.SelectedValue.ToString());
        sqlob.updateteam(txttname.Text, dropdepart.SelectedValue.ToString(), null, groupby.TrimEnd(','), Convert.ToInt16(txtindexorder.Text), Dropsearch.SelectedValue.ToString(),dd_location.SelectedValue.ToString());
        
        lblresults.Text = "The Record was successfully Updated!";
    }
    
}
   
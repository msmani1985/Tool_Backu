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
using System.Data.Sql;
using System.Text;
public partial class EmployeeDesignation : System.Web.UI.Page
{
    datasourceSQL sqlobj1 = new datasourceSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //btnupdate1.Enabled = false;
            
            //DropTimeSheet.Items.Insert(0, "----select----");
            //DropTimeSheet.SelectedIndex = 0;
            datasourceSQL sqlss = new datasourceSQL();

            DataSet Dst1 = new DataSet();
            datasourceSQL DSql1 = new datasourceSQL();
            Dst1 = DSql1.searchdesigname();
            Dropdesigname.DataTextField = "designation_name";
            Dropdesigname.DataValueField = "designation_id";
            Dropdesigname.DataSource = Dst1;
            Dropdesigname.DataBind();
                                  
            Dropdesigname.Items.Insert(0, "---select---");
            Dropdesigname.SelectedIndex = 0;
    }
    }
    private void cleardesignation()
    {
        Dropdesigname.SelectedIndex = 0;
        txtdesigname.Text = "";
        dd_timesheet.SelectedIndex = 0;
        lblresults.Text = "";
    }


    protected void btnsearch1_Click(object sender, EventArgs e)
    {
        datasourceSQL sqlob3 = new datasourceSQL();
        DataSet sds = new DataSet();
        sds = sqlob3.chkdesigname("", Dropdesigname.SelectedValue.ToString());
        if (sds != null && sds.Tables[0].Rows.Count > 0)
        {
            lblresults.Text = "";
            btnupdate1.Enabled = true;
            if (sds.Tables[0].Rows[0]["Time_Sheet"].ToString() == "True")
                dd_timesheet.SelectedValue = "0";
            else if(sds.Tables[0].Rows[0]["Time_Sheet"].ToString() == "False")
                dd_timesheet.SelectedValue = "1";
            txtdesigname.Text = sds.Tables[0].Rows[0]["designation_name"].ToString();
                      
        }
        else
        {
            lblresults.Text = "No Records Found";
            cleardesignation();
            // btnupdate.Enabled = false;
        }
    }

    protected void btncreate1_Click(object sender, EventArgs e)
    {
       
        datasourceSQL sqlob2 = new datasourceSQL();
        DataSet ids = new DataSet();
        ids = sqlob2.chkdesigname(txtdesigname.Text, "");


        if (ids != null && ids.Tables[0].Rows.Count > 0)
        {
            lblresults.Text = "This Designation Name was already exist!";

        }
        else
        {
            sqlob2.insertdesig(txtdesigname.Text, Convert.ToInt32(dd_timesheet.SelectedValue));
            lblresults.Text = "The Record was successfully inserted!";
            btnupdate1.Enabled = true;
        }


    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        cleardesignation();
    }
    protected void btnupdate1_Click(object sender, EventArgs e)
    {
        datasourceSQL sqlob = new datasourceSQL();
        sqlob.updatedesignation(txtdesigname.Text, dd_timesheet.SelectedValue.ToString(), Dropdesigname.SelectedValue.ToString());
        btnupdate1.Enabled = true;
        lblresults.Text = "The Record was successfully Updated!";

              
         
    }
   
}

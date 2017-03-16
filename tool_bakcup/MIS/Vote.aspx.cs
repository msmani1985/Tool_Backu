using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vote : System.Web.UI.Page
{
    datasourceSQL dsSQL = new datasourceSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["employeeid"] == null)
            {
                throw new Exception("Session Expired!");
            }
            else if (Convert.ToString(Session["locationid"]) != "3" && Convert.ToString(Session["employeeid"])!="2461")
            {
                throw new Exception("Invalid Location!");
            }
            else
            {
                if (!Page.IsPostBack)
                {

                    FillEmployeeDetails();
                }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
        finally 
        { 
        
        }
      
    }

    private void FillEmployeeDetails()
    {
        DataSet dsEmp = new DataSet();
        DataSet dsVote = new DataSet();
        int intCount = 0;
        try
        {
            dsSQL = new datasourceSQL();
            intCount = dsSQL.GetEmployeeVote(Convert.ToInt32(Session["employeeid"]));
            if (intCount == 0)
            {
                dsEmp = dsSQL.GetEmployeeDetails();
                grdEmployeeMale.DataSource = dsEmp.Tables[0];
                grdEmployeeMale.DataBind();
                grdEmployeeFemale.DataSource = dsEmp.Tables[1];
                grdEmployeeFemale.DataBind();
                grdEmployeeMale.Visible = true;
                grdEmployeeFemale.Visible = true;
                btnVote.Visible = true;
                btnClear.Visible = true;
                lblError.Text = "";
            }
            else
            {
                grdEmployeeMale.Visible = false;
                grdEmployeeFemale.Visible = false;
                btnVote.Visible = false;
                btnClear.Visible = false;
                lblError.Text = "You have voted successfully.";
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void btnVote_Click(object sender, EventArgs e)
    {
        int ref_no=0;
        int male_emp_id=0;
        int female_emp_id=0;
        Boolean blnChecked = false;
        try
        {

            foreach (GridViewRow itm in grdEmployeeMale.Rows)
            {
                CheckBox chx = (CheckBox)itm.Cells[7].FindControl("chkMaleVote");
                if (chx.Checked)
                {
                    if (blnChecked == false)
                    {
                        blnChecked = true;
                    }
                    else
                    { 
                        lblError.Text="Multiple selection not allowed.";
                        return;
                    }
                    
                }
            }

            if (blnChecked == false)
            {
                lblError.Text = "Please select Mr Datapage.";
                return;
            }

            blnChecked = false;

            foreach (GridViewRow itm in grdEmployeeFemale.Rows)
            {
                CheckBox chx = (CheckBox)itm.Cells[7].FindControl("chkFemaleVote");
                if (chx.Checked)
                {
                    if (blnChecked == false)
                    {
                        blnChecked = true;
                    }
                    else
                    {
                        lblError.Text = "Multiple selection not allowed.";
                        return;
                    }

                }

            }

            if (blnChecked == false)
            {
                lblError.Text = "Please select Ms Datapage.";
                return;
            }

            if (Session["employeeid"] == null)
            {
                throw new Exception("Session Expired!");
            }
            else
            {
                ref_no = Convert.ToInt32(Session["employeeid"]);
            }

            foreach (GridViewRow itm in grdEmployeeMale.Rows)
            {
                CheckBox chx = (CheckBox)itm.Cells[7].FindControl("chkMaleVote");
                Label lblEmpId = (Label)itm.Cells[1].FindControl("lblMaleId");
                if (chx.Checked)
                {
                    male_emp_id = Convert.ToInt32(lblEmpId.Text);
                }

            }

            foreach (GridViewRow itm in grdEmployeeFemale.Rows)
            {
                CheckBox chx = (CheckBox)itm.Cells[7].FindControl("chkFemaleVote");
                Label lblEmpId = (Label)itm.Cells[1].FindControl("lblFemaleId");
                if (chx.Checked)
                {
                    female_emp_id = Convert.ToInt32(lblEmpId.Text);
                }

            }

            dsSQL.SetVoteToEmp(Convert.ToString(ref_no), Convert.ToString(male_emp_id), Convert.ToString(female_emp_id));

            FillEmployeeDetails();

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
      
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            FillEmployeeDetails();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
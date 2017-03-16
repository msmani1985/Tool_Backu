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
public partial class EmployeeDetails : System.Web.UI.Page
{
    HRMS_CMB cmb = new HRMS_CMB();
    HRMS_CH ch = new HRMS_CH();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!Page.IsPostBack)
        {
            DataSet ds = new DataSet();
            
            int id = Convert.ToInt16(Session["locationid"].ToString());
            if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
            {
                loadDetails_CH();
            }
            else
            {
                loadDetails_CMB();
            }

            this.showPanel(div_employee_details);

        }
    }
    private void loadDetails_CH()
    {
        DataSet SqlDs = new DataSet();
        HRMS_CH hrCh = new HRMS_CH();
        SqlDs = hrCh.GetEmployeeDetail(Convert.ToInt16(Session["employeenumber"].ToString()));
        DataRow row = SqlDs.Tables[0].Rows[0];
        txtEmpid.Text = row["Refno"].ToString();
        txtEmpName.Text = row["empname"].ToString();
        txtDOB.Text = row["dob"].ToString();
        txtGender.Text = row["sex"].ToString();
        txtFname.Text = row["fname"].ToString();
        txtMname.Text = row["mname"].ToString();
        DropMaritalStatus.SelectedValue = row["MARITALSTATUS"].ToString();
        if (row["MARITALSTATUS"].ToString() == "Single")
            txtSpouse.Enabled = false;
        else
            txtSpouse.Enabled = true;
        txtSpouse.Text = row["SPOUSENAME"].ToString();
        txtDOJ.Text = row["doj"].ToString();
        txtBranch.Text = row["branch"].ToString();
        txtDepart.Text = row["Department"].ToString();
        txtDesign.Text = row["Designation"].ToString();
        //txtBank.Text = row["bankname"].ToString();
        //txtBankAcc.Text = row["bankacno"].ToString();
        txtPF.Text = row["pfno"].ToString();
        txtESI.Text = row["esino"].ToString();
        txtPAN.Text = row["panno"].ToString();
        txtPreNo.Text = row["ad1"].ToString();
        txtPreName.Text = row["ad2"].ToString();
        txtPreName1.Text = row["ad3"].ToString();
        txtPrePlace.Text = row["ad4"].ToString();
        txtPreCity.Text = row["ad5"].ToString();
        DropPreState.Text = row["State"].ToString();
        txtPrePin.Text = row["adpin"].ToString();
        txtPerNo.Text = row["ad1p"].ToString();
        txtPerName.Text = row["ad2p"].ToString();
        txtPerName1.Text = row["ad3p"].ToString();
        txtPerPlace.Text = row["ad4p"].ToString();
        txtPerCity.Text = row["ad5p"].ToString();
        DropPerState.Text = row["Statep"].ToString();
        txtPerPin.Text = row["adpinp"].ToString();
        txtConDate.Text = row["CONFIRMATIONDATE"].ToString();
        if ("~/Photos/Chennai/" + row["Refno"].ToString() + ".jpg" == null)
        {
            imgPhoto.ImageUrl = "~/Photos/Chennai/index.jpg";
        }
        else
        {
            imgPhoto.ImageUrl = "~/Photos/Chennai/" + row["Refno"].ToString() + ".jpg";
        }
        txtEmailid.Text = row["email"].ToString();
        txtPhone.Text = row["phone"].ToString();
        txtMobile.Text = row["mobile"].ToString();
    }
    private void loadDetails_CMB()
    {
        DataSet SqlDs = new DataSet();
        HRMS_CMB hrCh = new HRMS_CMB();
        SqlDs = hrCh.GetEmployeeDetail(Convert.ToInt16(Session["employeenumber"].ToString()));
        DataRow row = SqlDs.Tables[0].Rows[0];
        txtEmpid.Text = row["Refno"].ToString();
        txtEmpName.Text = row["empname"].ToString();
        txtDOB.Text = row["dob"].ToString();
        txtGender.Text = row["sex"].ToString();
        txtFname.Text = row["fname"].ToString();
        txtMname.Text = row["mname"].ToString();
        DropMaritalStatus.SelectedValue = row["MARITALSTATUS"].ToString();
        if (row["MARITALSTATUS"].ToString() == "Single")
            txtSpouse.Enabled = false;
        else
            txtSpouse.Enabled = true;
        txtSpouse.Text = row["SPOUSENAME"].ToString();
        txtDOJ.Text = row["doj"].ToString();
        txtBranch.Text = row["branch"].ToString();
        txtDepart.Text = row["department"].ToString();
        txtDesign.Text = row["designation"].ToString();
        //txtBank.Text = row["bankname"].ToString();
        //txtBankAcc.Text = row["bankacno"].ToString();
        txtPF.Text = row["pfno"].ToString();
        txtESI.Text = row["esino"].ToString();
        txtPAN.Text = row["panno"].ToString();
        txtPreNo.Text = row["ad1"].ToString();
        txtPreName.Text = row["ad2"].ToString();
        txtPreName1.Text = row["ad3"].ToString();
        txtPrePlace.Text = row["ad4"].ToString();
        txtPreCity.Text = row["ad5"].ToString();
        DropPreState.Text = row["State"].ToString();
        txtPrePin.Text = row["adpin"].ToString();
        txtPerNo.Text = row["ad1p"].ToString();
        txtPerName.Text = row["ad2p"].ToString();
        txtPerName1.Text = row["ad3p"].ToString();
        txtPerPlace.Text = row["ad4p"].ToString();
        txtPerCity.Text = row["ad5p"].ToString();
        DropPerState.Text = row["Statep"].ToString();
        txtPerPin.Text = row["adpinp"].ToString();
        if ("~/Photos/CMB/" + row["Refno"].ToString() + ".jpg" == null)
        {
            imgPhoto.ImageUrl = "~/Photos/CMB/index.jpg";
        }
        else
        {
            imgPhoto.ImageUrl = "~/Photos/CMB/" + row["Refno"].ToString() + ".jpg";
        }
        txtEmailid.Text = row["email"].ToString();
        txtPhone.Text = row["phone"].ToString();
        txtMobile.Text = row["mobile"].ToString();
        txtConDate.Text = row["CONFIRMATIONDATE"].ToString();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
        {
            if (Session["employeenumber"].ToString() != "")
            {
                string inproc = "";

                inproc = "spUpdate_Emp_Details";

                string[,] pname ={{"@REFNO", txtEmpid.Text},{"@FNAME", txtFname.Text},{"@MNAME", txtMname.Text},{"@MaritalStatus",DropMaritalStatus.SelectedValue},{"@SPOUSENAME", txtSpouse.Text},
                    {"@ad1", txtPreNo.Text},{"@ad1p",txtPerNo.Text },{"@ad2",txtPreName.Text},
                    {"@ad2p",txtPerName.Text},{"@ad3",txtPreName1.Text},
                    {"@ad3p",txtPerName1.Text},{"@ad4",txtPrePlace.Text},{"@ad4p",txtPerPlace.Text},
                    {"@ad5",txtPreCity.Text},{"@ad5p",txtPerCity.Text},
                    {"@STATE",DropPreState.Text},{"@STATEp",DropPerState.Text},
                    {"@adpin",txtPrePin.Text},{"@adpinp",txtPerPin.Text},
                    {"@phone",txtPhone.Text},{"@mobile",txtMobile.Text},{"@Email",txtEmailid.Text},
                    {"@IsExist","Output"}};

                int val;
                if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
                    val = ch.ExcSProcedure(inproc, pname, CommandType.StoredProcedure);
                else
                    val = cmb.ExcSProcedure(inproc, pname, CommandType.StoredProcedure);
                if (val == 1)
                {
                    msg = "Inserted Successfully";
                }
                else if (val == 0)
                    msg = "User Name Already Exists";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmb = null;
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
        }
    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            
            case "div_employee_details":
                miEmpDetails.Attributes.Add("class", "current");
                miPersonalDetails.Attributes.Add("class", "");
                this.div_employee_details.Visible = true;
                this.Personal_Details.Visible = false;
                break;
            case "Personal_Details":
                miEmpDetails.Attributes.Add("class", "");
                miPersonalDetails.Attributes.Add("class", "current");
                this.div_employee_details.Visible = false;
                this.Personal_Details.Visible = true;
                break;
            case "div_Performance":
                miEmpDetails.Attributes.Add("class", "");
                miPersonalDetails.Attributes.Add("class", "");
                this.div_employee_details.Visible = false;
                this.Personal_Details.Visible = false;
                break;
        }
    }
    protected void lnkEmpDetails_Click(object sender, EventArgs e)
    {
        this.showPanel(div_employee_details);
    }
    protected void lnkPersonalDetails_Click(object sender, EventArgs e)
    {
        this.showPanel(Personal_Details);
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
        {
            HRMS_CH ch = new HRMS_CH();
            ds = ch.GetFamilyDetails(txtEmpid.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
               ch.InsertFamilyDetails(txtEmpid.Text);
               ds = ch.GetFamilyDetails(txtEmpid.Text);
            }
            ds1 = ch.GetEduDetails(txtEmpid.Text);
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                ch.InsertEduDetails(txtEmpid.Text);
                ds1 = ch.GetEduDetails(txtEmpid.Text);
            }
        }
        else
        {
            HRMS_CMB cmb = new HRMS_CMB();
            ds = cmb.GetFamilyDetails(txtEmpid.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                cmb.InsertFamilyDetails(txtEmpid.Text);
                ds = cmb.GetFamilyDetails(txtEmpid.Text);
            }
            ds1 = cmb.GetEduDetails(txtEmpid.Text);
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                cmb.InsertEduDetails(txtEmpid.Text);
                ds1 = cmb.GetEduDetails(txtEmpid.Text);
            }
        }

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            gv_EduDetails.DataSource = ds1;
            gv_EduDetails.DataBind();
        }
        
    }
   
    protected void DropMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropMaritalStatus.SelectedValue == "Single")
            txtSpouse.Enabled = false;
        else
            txtSpouse.Enabled = true;
    }
    
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Add"))
            {
                TextBox txtname = (TextBox)GridView1.FooterRow.FindControl("txtAddname");
                TextBox txtrelationship = (TextBox)GridView1.FooterRow.FindControl("txtAddaddress");
                TextBox txtremarks = (TextBox)GridView1.FooterRow.FindControl("txtAdddesignation");
                TextBox txtage = (TextBox)GridView1.FooterRow.FindControl("txtAddage");
                CheckBox txtdepends = (CheckBox)GridView1.FooterRow.FindControl("chkAdddepends");
                bool status = txtdepends.Checked;
                int d;
                if (status == true)
                    d = 1;
                else
                    d = 0;
                CheckBox txtnominee = (CheckBox)GridView1.FooterRow.FindControl("chkAddnominee");
                bool status1 = txtnominee.Checked;
                int N;
                if (status1 == true)
                    N = 1;
                else
                    N = 0;
                string year = Convert.ToString(txtage.Text);
                GridView1.EditIndex = -1;
                string inproc = "";

                inproc = "spInsert_Personal_Details";

                string[,] param ={
                    {"@Desc01",txtname.Text},{"@Desc02",txtrelationship.Text },{"@desc04",txtremarks.Text},{"@empid",txtEmpid.Text},
                    {"@year",year},{"@hrtype","5"},{"@OPTIONYN",d.ToString()},{"@NOMINEEYN",N.ToString()}
                    };
                int val;
                if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
                    val = ch.ExcSProc(inproc, param, CommandType.StoredProcedure);
                else
                    val = cmb.ExcSProc(inproc, param, CommandType.StoredProcedure);
                lnkPersonalDetails_Click(sender, e);
            }
        }
       
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label Counter = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");
            if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
                ch.deleteEduDetails(txtEmpid.Text, 5, Convert.ToInt32(Counter.Text));
            else
                cmb.deleteEduDetails(txtEmpid.Text, 5, Convert.ToInt32(Counter.Text));
            GridView1.EditIndex = -1;
            lnkPersonalDetails_Click(sender, e);
        }
        
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            lnkPersonalDetails_Click(sender, e);
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label Counter = (Label)GridView1.Rows[e.RowIndex].FindControl("lblid");
            TextBox txtname = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtname");
            TextBox txtrelationship = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtaddress");
            TextBox txtremarks = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtdesignation");
            TextBox txtage = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtage");
            //DateTime dob = DateTime.Parse(txtage.Text);
            string year = Convert.ToString(txtage.Text);
            CheckBox txtdepends = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("txtdepends"); //sender;
            bool status = txtdepends.Checked;
            int d;
            if (status == true)
                d = 1;
            else
                d = 0;
            CheckBox txtnominee = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("txtnominee");
            bool status1 = txtnominee.Checked;
            int N;
            if (status1 == true)
                N = 1;
            else
                N = 0;
            string inproc = "";

            inproc = "spUpdate_Personal_Details";

            string[,] param ={
                    {"@Desc01",txtname.Text},{"@Desc02",txtrelationship.Text },{"@desc04",txtremarks.Text},{"@empid",txtEmpid.Text},
                    {"@year",year},{"@hrtype","5"},{"@count",Counter.Text},{"@OPTIONYN",d.ToString()},{"@NOMINEEYN",N.ToString()}
                    };
            int val;
            if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
                val = ch.ExcSProc(inproc, param, CommandType.StoredProcedure);
            else
                val = cmb.ExcSProc(inproc, param, CommandType.StoredProcedure);
            lnkPersonalDetails_Click(sender, e);
            GridView1.EditIndex = -1;
            lnkPersonalDetails_Click(sender, e);
        }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        lnkPersonalDetails_Click(sender, e);
    }
    //Eductional Details
    protected void gv_EduDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_EduDetails.EditIndex = -1;
        lnkPersonalDetails_Click(sender, e);
    }
        protected void gv_EduDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Add"))
            {
                TextBox Qualification = (TextBox)gv_EduDetails.FooterRow.FindControl("txtAddQualif");
                TextBox Board = (TextBox)gv_EduDetails.FooterRow.FindControl("txtAddboard");
                TextBox Percentage = (TextBox)gv_EduDetails.FooterRow.FindControl("txtAddper");
                TextBox Passout = (TextBox)gv_EduDetails.FooterRow.FindControl("txtAddpass");
                //string Per = Percentage.Text;
                //string pass = Passout.Text;
                gv_EduDetails.EditIndex = -1;
                string inproc = "";

                inproc = "spInsert_Personal_Details";

                string[,] param ={
                    {"@Desc01",Qualification.Text},{"@Desc02",Board.Text },{"@Num01",Percentage.Text},{"@year",Passout.Text},{"@empid",txtEmpid.Text},
                    {"@hrtype","2"}
                    };
                int val;
                if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
                    val = ch.ExcSProcedure(inproc, param, CommandType.StoredProcedure);
                else
                    val = cmb.ExcSProcedure(inproc, param, CommandType.StoredProcedure);
                lnkPersonalDetails_Click(sender, e);
            }
        }

        protected void gv_EduDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label Counter = (Label)gv_EduDetails.Rows[e.RowIndex].FindControl("lblid");
            if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
                ch.deleteEduDetails(txtEmpid.Text, 2, Convert.ToInt32(Counter.Text));
            else
                cmb.deleteEduDetails(txtEmpid.Text, 2, Convert.ToInt32(Counter.Text));
            
            gv_EduDetails.EditIndex = -1;
            lnkPersonalDetails_Click(sender, e);
        }

        protected void gv_EduDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_EduDetails.EditIndex = e.NewEditIndex;
            lnkPersonalDetails_Click(sender, e);
        }

        protected void gv_EduDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label Counter = (Label)gv_EduDetails.Rows[e.RowIndex].FindControl("lblid");
            TextBox Qualification = (TextBox)gv_EduDetails.Rows[e.RowIndex].FindControl("txtQualif");
            TextBox Board = (TextBox)gv_EduDetails.Rows[e.RowIndex].FindControl("txtboard");
            TextBox Percentage = (TextBox)gv_EduDetails.Rows[e.RowIndex].FindControl("txtper");
            TextBox Passout = (TextBox)gv_EduDetails.Rows[e.RowIndex].FindControl("txtpass");
            string inproc = "";

            inproc = "spUpdate_Personal_Details";

            string[,] param ={
                    {"@Desc01",Qualification.Text},{"@Desc02",Board.Text },{"@num01",Percentage.Text},{"@year",Passout.Text},{"@empid",txtEmpid.Text},
                    {"@hrtype","2"},{"@count",Counter.Text}
                    };
            int val;
            if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
                val = ch.ExcSProcedure(inproc, param, CommandType.StoredProcedure);
            else
                val = cmb.ExcSProcedure(inproc, param, CommandType.StoredProcedure);
            lnkPersonalDetails_Click(sender, e);
            gv_EduDetails.EditIndex = -1;
            lnkPersonalDetails_Click(sender, e);
        }


    protected void CheckAddress_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckAddress.Checked == true)
        {
            txtPerNo.Text = txtPreNo.Text;
            txtPerName.Text = txtPreName.Text;
            txtPerName1.Text = txtPreName1.Text;
            txtPerPlace.Text = txtPrePlace.Text;
            txtPerCity.Text = txtPreCity.Text;
            DropPerState.Text = DropPreState.Text;
            txtPrePin.Text = txtPrePin.Text;
        }
        else
        {
            txtPerNo.Text = "";
            txtPerName.Text = "";
            txtPerName1.Text = "";
            txtPerPlace.Text = "";
            txtPerCity.Text = "";
            DropPerState.Text = "";
            txtPrePin.Text = "";
        }
    }
}

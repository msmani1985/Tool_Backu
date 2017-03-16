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

public partial class createemployee : System.Web.UI.Page
{
    SqlConnection oConnVote;
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;
    //string empno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        nextappraisaldate.Visible = false;
        nextappdatelbl.Visible = false;
        txtEmpId.Value = "";
        id_transfer_resigned.Visible = false;
        
        if (!Page.IsPostBack)
        {
            ErrorDiv.Visible = false;
            GridDiv.Visible = false;
            EmpnoRBtn.Checked = true;
            //LoadEmployeeDetails();
            DataSet ds = new DataSet();
            biz_emp_menu_mgmt emg = new biz_emp_menu_mgmt();
            ds = emg.GetDesignation();
            if (ds != null)
                BuildDDLItems(designation, ds, "---Select Designation---");

           // ds = emg.GetDepartment();
            //if (ds != null)
            //    BuildDDLItems(department, ds);
            ds = emg.GetLocation();
            if (ds != null)
            {
                BuildDDLItems(location, ds, "---Select Location---");
                BuildDDLItems(Transfer_Location, ds,"---Select Location---");
            }

            ds = emg.GetEmpTeam();
            if (ds != null)
            {
                BuildDDLItems(ddlempteam, ds, "---Select Employee Team---");
                Session["employee_team"] = ds;
            }

            ds = emg.GetTeamOwner();
            if (ds != null)
                BuildDDLItems(ddlreportto, ds, "---Select Report To---");

            gender.Items.Clear();
            gender.Items.Add (new ListItem("M","M"));  
            gender.Items.Add (new ListItem("F","F"));

            ds = null;
            emg = null;
        }


       
        if (btnSubmit.Text == "Add")
            //Find EmployeeNumber
            employee_number.Text = GetEmpNo(Convert.ToInt32(location.SelectedValue));
        DateTime dtd;
        if (DateTime.TryParse(dateofjoin.Text,out dtd) && btnSubmit.Text=="Add")
        {
            nextappraisaldate.Text = Convert.ToDateTime(dateofjoin.Text).AddMonths(6).ToShortDateString();
            nextappraisaldate.Visible = true;
            nextappdatelbl.Visible = true;
        }
    }


    private String GetNullString(string sString)
    {
        if (sString == null)
            sString = "";
        return sString;
    }
    private int GetNullInteger(string sString)
    {
        if (sString == null)
            sString = "0";
        return Convert.ToInt16(sString);   
    }

    private void BuildDDLItems(DropDownList oDDListBox, DataSet oDs,string defaultval)
    {
        //int i = 0;
        //if (oDs.Tables[0].Rows.Count > 0)
        //{
        //    oDDListBox.Items.Clear();
        //    for (i = 0; i < oDs.Tables[0].Rows.Count; i++)
        //        oDDListBox.Items.Add(new ListItem(oDs.Tables[0].Rows[i][1].ToString(), oDs.Tables[0].Rows[i][0].ToString()));
        //}
        oDDListBox.DataSource = oDs;
        oDDListBox.DataBind();
        if (!string.IsNullOrEmpty(defaultval))
            oDDListBox.Items.Insert(0, new ListItem(defaultval, "0"));
        oDs = null;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ResetCntrl();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string msg="";
        DateTime date1;DateTime date2;DateTime date3;
        if ((!DateTime.TryParse(dateofbirth.Text, out date1) && dateofbirth.Text.Trim()!="") || (!DateTime.TryParse(dateofjoin.Text,out date2) && dateofjoin.Text.Trim()!="") || (!DateTime.TryParse(dateofresigned.Text,out date3) && dateofresigned.Text.Trim()!=""))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Msg", "<script language='javascript'>alert('Please give Date Foramt (MM/DD/YYYY) in Date text');</script>");
            return;
        }
        datasourceSQL DIBObj = new datasourceSQL();
        try
        {
            if (btnSubmit.Text == "Add")
            {
                string inproc = "spADD_EMPLOYEE";

                string[,] pname ={
                    {"@fname", fname.Text},{"@surname",lastname.Text },{"@designation_id",designation.SelectedValue},
                    {"@location_id",location.SelectedValue},{"@email_address",Txtmailid.Text},
                    {"@date_of_birth",dateofbirth.Text},{"@username",username.Text},{"@password",username.Text},
                    {"@employee_number",employee_number.Text},{"@gender",gender.SelectedItem.ToString()},
                    {"@blood_group",""},{"@skillset",skillset.Text},{"@address",address.Text},
                    {"@join_date",dateofjoin.Text},{"@comment",""},{"@app_date",nextappraisaldate.Text},
                    {"@app_mode_id","2"},{"@barcode","0"},{"@empno","0"},{"@employee_id","0"},{"@IsExist","Output"},
                    {"@paddress",paddress.Text},{"@telephoneno",phoneno.Text },{"@mobile",mobileno.Text },
                    {"@team_id",ddlempteam.SelectedValue.ToString()},{"@report_to",ddlreportto.SelectedValue.ToString()}
                };
                
                int val=DIBObj.ExcSProcedure(inproc, pname, CommandType.StoredProcedure);
                int val1 = ExcSProcedure(inproc, pname, CommandType.StoredProcedure);
                if (val == 1)
                {
                    msg = "Inserted Successfully";
                    btnClear_Click(sender, e);
                }
                else if (val == 0)
                    msg = "User Name Already Exists";
                //LoadEmployeeDetails();
                
            }
            if (btnSubmit.Text == "Update" && obsolete.Checked == false)
            {
                string inproc = "spUpdate_Employee";
                //nextappraisaldate.Text = Convert.ToDateTime(dateofjoin.Text).AddMonths(6).ToShortDateString();
                string[,] param ={
                    {"@fname",fname.Text},{"@surname",lastname.Text },{"@designation_id",designation.SelectedValue},{"@location_id",location.SelectedValue},{"@email_address",Txtmailid.Text},
                    {"@date_of_birth",dateofbirth.Text},{"@username",username.Text},{"@employee_number",employee_number.Text},{"@gender",gender.SelectedItem.ToString()},
                    {"@blood_group",""},{"@skillset",skillset.Text},{"@address", address.Text},{"@join_date",dateofjoin.Text},{"@comment",""},{"@app_date",nextappraisaldate.Text},{"@app_mode_id","2"},
                    {"@app_id1",HFApp_id.Value },{"@IsExist","Output"},{"@employee_id",EmpId_HF.Value},{"@paddress",paddress.Text},{"@telephoneno",phoneno.Text },{"@mobile",mobileno.Text},
                    {"@plleave",PLTxt.Text},{"@slleave",SLTxt.Text},{"@team_id",ddlempteam.SelectedValue.ToString()},{"@report_to",ddlreportto.SelectedValue.ToString()}
                };
                DIBObj.ExcSProcedure(inproc, param, CommandType.StoredProcedure);
				ExcSProcedure(inproc, param, CommandType.StoredProcedure);
                btnClear_Click(sender, e);
                //LoadEmployeeDetails();
                msg="Updated Successfully";
            }
            if (btnSubmit.Text == "Update" && obsolete.Checked == true)
            {
                if (Transfer_Location.SelectedValue.ToString() == "0" && Transfer.Checked==true)
                {
                    msg="If you want to tranfer, Please click transfer icon and select location";
                    return;
                }
                
                string inproc = "spDelete_Employee";
                string[,] param ={ { "@emp_id", EmpId_HF.Value }, { "@resigndate", dateofresigned.Text.ToString().Trim() }, { "@comment", Comment.Text.ToString().Trim() }, { "@transfer_location", Transfer_Location.SelectedValue.ToString() } };
                //string emid = EmpId_HF.Value + "," + dateofresigned.Text;
                //string paramname = "@emp_id,@resigndate";
                //string paramtype = "Int,DATE";
                //string paramdir = "Input,Input";
                DIBObj.ExcSProcedure(inproc, param, CommandType.StoredProcedure);
				ExcSProcedure(inproc, param, CommandType.StoredProcedure);
                //DIBObj.ExcuteProcedure(inproc, emid, paramname, paramtype, paramdir, CommandType.StoredProcedure);
                btnClear_Click(sender, e);


                //LoadEmployeeDetails();
                msg="Deleted Successfully";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            DIBObj = null;
            ClientScript.RegisterStartupScript(this.GetType(),"Open","<script language='javascript'>alert('"+ msg +"');</script>");
        }
    }
    private string GetEmpNo(int locationid)
    {
        int empno;
        DataSet EmpDs = new DataSet();
        datasourceSQL SQLObj = new datasourceSQL();
        EmpDs = SQLObj.GetEmployeeNo(locationid);
        if (EmpDs.Tables[0].Rows.Count > 0)
        {
            if (EmpDs.Tables[0].Rows[0]["empno"] != null && EmpDs.Tables[0].Rows[0]["empno"].ToString()!="")
                empno = Convert.ToInt32(EmpDs.Tables[0].Rows[0]["empno"]);
            else
                empno = 0;
            EmpDs = null;
            SQLObj = null;
            return Convert.ToString(empno +1);
        }
        else
        {
            EmpDs = null;
            SQLObj = null;
            return "";
        }
        
    }

   /* private void LoadEmployeeDetails()
    {
        DataSet ds = new DataSet();
        biz_emp_menu_mgmt emg = new biz_emp_menu_mgmt();
        ds = emg.GetLiveEmployees();
        if (ds != null)
        {
            adgliveemp.DataSource = ds.Tables[0];
            adgliveemp.DataBind();
            EMPLOYEEGRID1.DataSource = ds.Tables[0];
            EMPLOYEEGRID1.DataBind();
        }
        ds = emg.GetResignedEmployees();
        if (ds != null)
        {
            adgresignedemp.DataSource = ds.Tables[0];
            adgresignedemp.DataBind();
        }
        ds = null;
        emg = null;
    }*/
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        ResetCntrl();
        GridDiv.Visible = false;
        ErrorDiv.Visible = false;
        EMPLOYEEGRID.Visible = false;
        LoadEmpDetails(EmpNoNameTxt.Text,EmpnoRBtn.Checked,EmpNameRBtn2.Checked);
    }


    protected void EmpnoRBtn_CheckedChanged(object sender, EventArgs e)
    {

        if (EmpnoRBtn.Checked == true)
            EmployeeLbl.Text = "Employee No.";
        else if (EmpNameRBtn2.Checked == true)
            EmployeeLbl.Text = "Employee Name";
        ResetCntrl();
        EmpNoNameTxt.Text = "";
        ErrorDiv.Visible = false;
        GridDiv.Visible = false;
    }
    protected void Employee_ItemCommand(object sender, DataGridCommandEventArgs e)
    {
       // empno = e.CommandArgument.ToString();
        //LoadEmpDetails(e.CommandArgument.ToString(),true,false);
        biz_emp_menu_mgmt obj = new biz_emp_menu_mgmt();
        DataSet ids=new DataSet();
        ids = obj.GetEmployeeByName("spGet_EmployeeDetailsByName", new string[,] { { "@empname", "" }, { "@empid", e.CommandArgument.ToString() } }, CommandType.StoredProcedure);
        if (ids != null && ids.Tables[0].Rows.Count == 1)
            LoadControl(ids);
        EMPLOYEEGRID.Visible = false;
        GridDiv.Visible = false;
        obj = null;
        ids = null;
        
    }
    private void LoadEmpDetails(string empnameno ,bool EmpNoFlg,bool EmpNameFlg)
    {
        DataSet ds = new DataSet();
        biz_emp_menu_mgmt emg = new biz_emp_menu_mgmt();
        if (EmpNoFlg == true)
            ds = emg.GetEmployeeDetail(Convert.ToInt16(empnameno));
        else if (EmpNameFlg == true)
            ds = emg.GetEmployeeByName("spGet_EmployeeDetailsByName", new string[,] { { "@empname", empnameno }, {"@empid","" } } , CommandType.StoredProcedure);
        if (ds!=null && ds.Tables[0].Rows.Count > 1)
        {
            GridDiv.Visible = true;
            EMPLOYEEGRID.Visible = true;
            EMPLOYEEGRID.DataSource = ds.Tables[0];
            EMPLOYEEGRID.DataBind();
        }
        else if (ds != null && ds.Tables[0].Rows.Count == 1)
        {
            ErrorDiv.Visible = false;
            ErrorDiv.InnerHtml = "";
            LoadControl(ds);
           
        }
        else
        {
            ErrorDiv.Visible = true;
            //ErrorDiv.InnerHtml = "Record Missing in Appraisal History";
            ErrorDiv.InnerHtml = "No Records";
            ResetCntrl();
        }


        ds = null;
        emg = null;
        nextappraisaldate.Visible = true;
        nextappdatelbl.Visible = true;
        
    }
    private void LoadControl(DataSet pds)
    {
        fname.Text = GetNullString(pds.Tables[0].Rows[0]["fname"].ToString());
        lastname.Text = GetNullString(pds.Tables[0].Rows[0]["surname"].ToString());
        gender.SelectedValue = pds.Tables[0].Rows[0]["gender"].ToString();
        designation.SelectedValue = pds.Tables[0].Rows[0]["designation_id"].ToString();
        location.SelectedValue = pds.Tables[0].Rows[0]["location_id"].ToString();
        address.Text = GetNullString(pds.Tables[0].Rows[0]["Contact_Address"].ToString());
        paddress.Text = GetNullString(pds.Tables[0].Rows[0]["permanent_address"].ToString());
        phoneno.Text = pds.Tables[0].Rows[0]["Telephone"].ToString();
        mobileno.Text = pds.Tables[0].Rows[0]["mobile"].ToString();
        employee_number.Text = pds.Tables[0].Rows[0]["employee_number"].ToString();
        username.Text = pds.Tables[0].Rows[0]["username"].ToString();
        //barcode.Text = pds.Tables[0].Rows[0]["barcode"].ToString();
        dateofbirth.Text = Convert.ToDateTime(pds.Tables[0].Rows[0]["date_of_birth"]).ToShortDateString();
        dateofjoin.Text = Convert.ToDateTime(pds.Tables[0].Rows[0]["joined_date"]).ToShortDateString();
        if (pds.Tables[0].Rows[0]["obsolete"].ToString() != "")
            dateofresigned.Text = Convert.ToDateTime(pds.Tables[0].Rows[0]["obsolete"]).ToShortDateString();
        //nextappraisaldate.Text = pds.Tables[0].Rows[0]["next_appraisal_history_id"].ToString();
        nextappraisaldate.Text = Convert.ToDateTime(pds.Tables[0].Rows[0]["ACTUAL_APPRAISAL_DATE"]).ToShortDateString();
        skillset.Text = pds.Tables[0].Rows[0]["skillset"].ToString();
        txtEmpId.Value = pds.Tables[0].Rows[0]["employee_id"].ToString();
        EmpId_HF.Value = pds.Tables[0].Rows[0]["employee_id"].ToString();
        HFApp_id.Value = pds.Tables[0].Rows[0]["next_appraisal_history_id"].ToString();
        Txtmailid.Text = pds.Tables[0].Rows[0]["email_address"].ToString();
        
        try
        {
            if (pds.Tables[0].Rows[0]["employee_team_id"] != null && pds.Tables[0].Rows[0]["employee_team_id"].ToString() != "")
                ddlempteam.SelectedValue = pds.Tables[0].Rows[0]["employee_team_id"].ToString();
            if (pds.Tables[0].Rows[0]["Report_To"] != null && pds.Tables[0].Rows[0]["Report_To"].ToString() != "")
                ddlreportto.SelectedValue = pds.Tables[0].Rows[0]["Report_To"].ToString();
        }
        catch (Exception ex)
        { }
        Comment.Text = pds.Tables[0].Rows[0]["comment"].ToString();
        if (dateofresigned.Text != "")
        {
            id_transfer_resigned.Visible = true;
            obsolete.Checked = true;
            obsolete.Enabled = false;
        }
        if (pds.Tables[0].Rows[0]["transfer_location"] != null && pds.Tables[0].Rows[0]["transfer_location"].ToString() != "")
        {
            id_transfer_resigned.Visible = true;
            /*Transfer option disabled*/
            //Transfer.Checked = true;
            //Transfer.Enabled = false;
            Transfer_Location.SelectedValue = pds.Tables[0].Rows[0]["transfer_location"].ToString();
            Transfer_Location.Enabled = false;
        }
        else
        {
            /*Transfer option disabled*/
            //Transfer.Checked = false;
            //Transfer.Enabled = true;
            Transfer_Location.SelectedValue = "0";
            Transfer_Location.Enabled = true;
        }

        btnSubmit.Text = "Update";
        location.Enabled = false;
    }
    
    private void ResetCntrl()
    {
        fname.Text = "";
        lastname.Text = "";
        gender.SelectedIndex = 0;
        designation.SelectedIndex = 0;
        location.SelectedIndex = 0;
        location.Enabled = true;
        ddlempteam.SelectedIndex = 0;
        address.Text = "";
        employee_number.Text = GetEmpNo(Convert.ToInt32(location.SelectedValue));
        username.Text = "";
        // barcode.Text = "";
        dateofbirth.Text = "";
        dateofjoin.Text = "";
        dateofresigned.Text = "";
        nextappraisaldate.Text = "";
        skillset.Text = "";
        txtEmpId.Value = "";
        Txtmailid.Text = "";
        paddress.Text = "";
        phoneno.Text = "";
        mobileno.Text = "";
        SLTxt.Text = "";
        PLTxt.Text = "";
        obsolete.Checked = false;
        obsolete.Enabled = true;
        /*Transfer option disabled*/
        //Transfer.Checked = false;
        //Transfer.Enabled = true;
        Transfer_Location.Enabled = true;
        designation.SelectedValue = "0";
        ddlempteam.SelectedValue = "0";
        location.SelectedValue = "0";
        ddlreportto.SelectedValue = "0";
        btnSubmit.Text = "Add";
    }
    protected void UserValidBtn_Click(object sender, ImageClickEventArgs e)
    {
        datasourceSQL DbObj = new datasourceSQL();
        try
        {
            string[,] paramcol ={ { "@username", username.Text }, { "@isexist", "OutPut" } };
            int valid = DbObj.ExcSProcedure("spUserValidation", paramcol, CommandType.StoredProcedure);
            if (valid == 1)
                ValidationLbl.Text = "User Name is Already Exists";
            else
                ValidationLbl.Text = "User Name is Valid ";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { DbObj = null; }


    }
    protected void location_SelectedIndexChanged(object sender, EventArgs e)
    {
        employee_number.Text = GetEmpNo(Convert.ToInt32(location.SelectedValue));
    }
    protected void Transfer_CheckedChanged(object sender, EventArgs e)
    {
        if (Transfer.Checked == true)
        {
            id_transfer_resigned.Visible = true;
            Transfer_Location.Enabled = true;
        }
        if(obsolete.Checked==false)
            obsolete.Checked=true;
        if (Transfer.Checked == true)
            Comment.Text = "Transfered";
        else
        {
            if (obsolete.Checked == true)
            {
                id_transfer_resigned.Visible = true;
                Comment.Text = "Resigned";
            }
            else
                Comment.Text = "";
        }

    }
    protected void obsolete_CheckedChanged(object sender, EventArgs e)
    {
      
        if (Transfer.Checked == true)
            obsolete.Checked = true;
        if (obsolete.Checked == true)
            id_transfer_resigned.Visible = true;
        Transfer_Location.Enabled = true;
        if (obsolete.Checked == true && Transfer.Checked == false)
        {
            Comment.Text = "Resigned";
            Transfer_Location.Enabled = false;
        }
        else if (obsolete.Checked == false && Transfer.Checked == false)
            Comment.Text = "";
    }
    protected void btn_transfer_ok_Click(object sender, EventArgs e)
    {

    }
    private void opencon()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrSQLEmp"].ConnectionString;
        oConn = new SqlConnection(sConStr);
        oConn.Open();
    }
    private void closecon()
    {
        if (oConn != null)
        {
            if (oConn.State != ConnectionState.Closed)
                oConn.Close();
            oConn.Dispose();
        }
    }
    public int ExcSProcedure(string sProcName, string[,] sparameter, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        SqlTransaction sqltrans = null;
        char[] separator = new char[] { ',' };
        bool flg = false;
        string OutparamName = "";
        try
        {
            opencon();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
            sqltrans = oConn.BeginTransaction();
            ocmd.Transaction = sqltrans;
            ocmd.Parameters.Clear();
            int i;
            if (sparameter != null)
            {
                for (i = 0; i < sparameter.GetLength(0); i++)
                {
                    if (sparameter[i, 1].ToString().ToUpper() == "OUTPUT")
                    {

                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), "").Direction = ParameterDirection.Output;
                        //ocmd.Parameters[sparameter[i, 0]].Direction = ParameterDirection.Output;
                        flg = true;
                        OutparamName = sparameter[i, 0].ToString();
                    }
                    else if (sparameter[i, 1] == null || sparameter[i, 1].ToString() == "" || sparameter[i, 1].ToString() == "0")
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), DBNull.Value);
                    else
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), sparameter[i, 1]);
                }
            }
            ocmd.ExecuteNonQuery();
            sqltrans.Commit();
            if (flg == true)
                return Convert.ToInt32(ocmd.Parameters[OutparamName].Value);
            else
                return 0;
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            throw ex;
        }
        finally
        {
            ocmd = null;
            closecon();
        }

    }
   /* protected void ddlempteam_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet empteamds = (DataSet)Session["employee_team"];
        DataRow[] etrow = empteamds.Tables[0].Select("employee_team_id=" + ddlempteam.SelectedValue.ToString());
        ddlreportto.SelectedValue = etrow[0]["team_owner_id"].ToString();
    }
    protected void ddlreportto_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet emprpttods = (DataSet)Session["employee_team"];
        DataRow[] etrow = emprpttods.Tables[0].Select("team_owner_id=" + ddlreportto.SelectedValue.ToString());
        ddlempteam.SelectedValue = etrow[0]["employee_team_id"].ToString();

    }
    */
}

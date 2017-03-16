using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using SortDirection=System.Web.UI.WebControls.SortDirection;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Drawing;
//using CrystalDecisions.Enterprise;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Text.RegularExpressions;
using System.Net;

public partial class Employee_Upload : System.Web.UI.Page
{

    SqlConnection scon;
    HRMS_CMB cmb = new HRMS_CMB();
    HRMS_CH ch = new HRMS_CH();
    private static string sSortExpression = "";
    private static DataTable dtEmp = new DataTable();
    public double cl;
    public double sl;
    datasourceSQL SqlObj = new datasourceSQL();
    decimal p, s, c, pm, LOP, ULOP;
    int Pmins;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            rbLocation.Visible = false;
            if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
            {
                scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCH"].ConnectionString);
            }
            else if (Convert.ToInt16(Session["locationid"].ToString()) == 3)
            {
                scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCMB"].ConnectionString);
            }
            else
            {
                rbLocation.Visible = true;
                //rdBtnLive.Visible = false;
            }
            this.showPanel(div_Summary_details);
        }
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Upload();
    }
    public void Upload()
    {
        string connectionString = "";
        int count = 0;
        if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
        {
            scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCH"].ConnectionString);
        }
        else if (Convert.ToInt16(Session["locationid"].ToString()) == 3)
        {
            scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCMB"].ConnectionString);
        }
        if (fileBrowse.HasFile)
        {
            string fileName = Path.GetFileName(fileBrowse.PostedFile.FileName);
            string fileExtension = Path.GetExtension(fileBrowse.PostedFile.FileName);
            string fileLocation = @"\\192.9.201.222\db\QMS\" + Path.GetFileNameWithoutExtension(fileName) + Convert.ToString(DateTime.Now).Replace("/", "_").Replace(":", "_") + "." + fileExtension;
            fileBrowse.SaveAs(fileLocation);
            if (fileExtension == ".xls")
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=NO\"";
            }
            else if (fileExtension == ".xlsx")
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }

            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand cmds = new OleDbCommand();
            cmds.CommandType = System.Data.CommandType.Text;
            cmds.Connection = con;
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmds);
            OleDbDataAdapter dAdapter1 = new OleDbDataAdapter(cmds);
            DataTable dtExcelRecords = new DataTable();
            DataTable dtExcelRecords2 = new DataTable();
            DataTable dtExcelRecords4 = new DataTable();
            con.Open();
            DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string getExcelSheetName1 = string.Empty;
            getExcelSheetName1 = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
            string getExcelSheetName2 = string.Empty;
            string getExcelSheetName3 = string.Empty;
            
            if (fileName.Substring(0, 8) == "Employee")
            {
                getExcelSheetName1 = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmds.CommandText = "SELECT * FROM [" + getExcelSheetName1 + "]";
                dAdapter.SelectCommand = cmds;
                dAdapter.Fill(dtExcelRecords);
                for (int i = 2; i < dtExcelRecords.Rows.Count; i++)
                {
                        SqlCommand gcmd = new SqlCommand("Select empid from master_employee where empid='"+dtExcelRecords.Rows[i][0]+"'", scon);
                        SqlDataAdapter da = new SqlDataAdapter(gcmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds == null || ds.Tables[0].Rows.Count == 0)
                        {
                            try
                            {
                                string dob;
                                if (dtExcelRecords.Rows[i][7].ToString() == "")
                                    dob = null;
                                else
                                    dob = dtExcelRecords.Rows[i][7].ToString();
                                
                                string doj;
                                if (dtExcelRecords.Rows[i][39].ToString() == "")
                                    doj = null;
                                else
                                    doj = dtExcelRecords.Rows[i][39].ToString();

                                string dol;
                                if (dtExcelRecords.Rows[i][41].ToString() == "")
                                    dol = null;
                                else
                                    dol = dtExcelRecords.Rows[i][41].ToString();

                                scon.Open();
                                SqlCommand cmd = new SqlCommand("SP_Insert_EMPDetails", scon);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@EMPID", SqlDbType.Int).Value = dtExcelRecords.Rows[i][0];
                                cmd.Parameters.Add("@REFNO", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][1];
                                cmd.Parameters.Add("@EMPTITLE", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][2];
                                cmd.Parameters.Add("@EMPNAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][3];
                                cmd.Parameters.Add("@SHORTNAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][4];
                                cmd.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][5];
                                cmd.Parameters.Add("@MNAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][6];
                                cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = dob;
                                cmd.Parameters.Add("@SEX", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][8];
                                cmd.Parameters.Add("@MARITALSTATUS", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][9];
                                cmd.Parameters.Add("@SPOUSENAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][10];
                                cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][11];
                                cmd.Parameters.Add("@OCCUPATION", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][12];
                                cmd.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][13];
                                cmd.Parameters.Add("@GRADE", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][14];
                                cmd.Parameters.Add("@BRANCH", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][15];
                                cmd.Parameters.Add("@Division", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][16];
                                cmd.Parameters.Add("@BANKACNO", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][17];
                                cmd.Parameters.Add("@BANKNAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][18];
                                cmd.Parameters.Add("@AD1", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][21];
                                cmd.Parameters.Add("@AD2", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][22];
                                cmd.Parameters.Add("@AD3", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][23];
                                cmd.Parameters.Add("@AD4", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][24];
                                cmd.Parameters.Add("@AD5", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][25];
                                cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][26];
                                cmd.Parameters.Add("@ADPIN", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][27];
                                cmd.Parameters.Add("@AD1P", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][28];
                                cmd.Parameters.Add("@AD2P", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][29];
                                cmd.Parameters.Add("@AD3P", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][30];
                                cmd.Parameters.Add("@AD4P", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][31];
                                cmd.Parameters.Add("@AD5P", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][32];
                                cmd.Parameters.Add("@STATEP", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][33];
                                cmd.Parameters.Add("@ADPINP", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][34];
                                cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][35];
                                cmd.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][37];
                                cmd.Parameters.Add("@MOBILE", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][38];
                                cmd.Parameters.Add("@DOJ", SqlDbType.DateTime).Value = doj;
                                cmd.Parameters.Add("@PFNO", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][47];
                                cmd.Parameters.Add("@ESINO", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][44];
                                cmd.Parameters.Add("@PANNO", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][52];
                                cmd.Parameters.Add("@DOL", SqlDbType.DateTime).Value = dol;
                                count += cmd.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                            finally
                            {
                                scon.Close();
                            }
                        }
                        else
                        {
                            try
                            {

                                string dob;
                                if (dtExcelRecords.Rows[i][7].ToString() == "")
                                    dob = null;
                                else
                                    dob = dtExcelRecords.Rows[i][7].ToString();

                                string doj;
                                if (dtExcelRecords.Rows[i][39].ToString() == "")
                                    doj = null;
                                else
                                    doj = dtExcelRecords.Rows[i][39].ToString();

                                string dol;
                                if (dtExcelRecords.Rows[i][41].ToString() == "")
                                    dol = null;
                                else
                                    dol = dtExcelRecords.Rows[i][41].ToString();
                                scon.Open();
                                SqlCommand cmd = new SqlCommand("SP_Insert_EMPDetails", scon);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@EMPID", SqlDbType.Int).Value = dtExcelRecords.Rows[i][0];
                                cmd.Parameters.Add("@REFNO", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][1];
                                cmd.Parameters.Add("@EMPTITLE", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][2];
                                cmd.Parameters.Add("@EMPNAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][3];
                                cmd.Parameters.Add("@SHORTNAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][4];
                                cmd.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][5];
                                cmd.Parameters.Add("@MNAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][6];
                                cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = dob;
                                cmd.Parameters.Add("@SEX", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][8];
                                cmd.Parameters.Add("@MARITALSTATUS", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][9];
                                cmd.Parameters.Add("@SPOUSENAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][10];
                                cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][11];
                                cmd.Parameters.Add("@OCCUPATION", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][12];
                                cmd.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][13];
                                cmd.Parameters.Add("@GRADE", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][14];
                                cmd.Parameters.Add("@BRANCH", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][15];
                                cmd.Parameters.Add("@Division", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][16];
                                cmd.Parameters.Add("@BANKACNO", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][17];
                                cmd.Parameters.Add("@BANKNAME", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][18];
                                cmd.Parameters.Add("@AD1", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][21];
                                cmd.Parameters.Add("@AD2", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][22];
                                cmd.Parameters.Add("@AD3", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][23];
                                cmd.Parameters.Add("@AD4", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][24];
                                cmd.Parameters.Add("@AD5", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][25];
                                cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][26];
                                cmd.Parameters.Add("@ADPIN", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][27];
                                cmd.Parameters.Add("@AD1P", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][28];
                                cmd.Parameters.Add("@AD2P", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][29];
                                cmd.Parameters.Add("@AD3P", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][30];
                                cmd.Parameters.Add("@AD4P", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][31];
                                cmd.Parameters.Add("@AD5P", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][32];
                                cmd.Parameters.Add("@STATEP", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][33];
                                cmd.Parameters.Add("@ADPINP", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][34];
                                cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][35];
                                cmd.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][37];
                                cmd.Parameters.Add("@MOBILE", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][38];
                                cmd.Parameters.Add("@DOJ", SqlDbType.DateTime).Value =doj;
                                cmd.Parameters.Add("@PFNO", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][47];
                                cmd.Parameters.Add("@ESINO", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][44];
                                cmd.Parameters.Add("@PANNO", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][52];
                                cmd.Parameters.Add("@DOL", SqlDbType.DateTime).Value = dol;
                                
                                count += cmd.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                            finally
                            {
                                scon.Close();
                            }
                        }
                }
            }
            else if ( fileName.Substring(0,9) == "HRDetails")
            {
                getExcelSheetName3 = dtExcelSheetName.Rows[5]["Table_Name"].ToString();
                cmds.CommandText = "SELECT * FROM [" + getExcelSheetName3 + "]";
                dAdapter.SelectCommand = cmds;
                dAdapter.Fill(dtExcelRecords2);
                for (int i = 1; i < dtExcelRecords2.Rows.Count; i++)
                {
                    SqlCommand gcmd = new SqlCommand("Select empid from mas_hrdetails where empid=(select empid from master_employee where refno='" + dtExcelRecords2.Rows[i][0] + "') and hrtype='"+5+"' and desc01='"+"No Records"+"'", scon);
                    SqlDataAdapter da = new SqlDataAdapter(gcmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null || ds.Tables[0].Rows.Count == 1)
                    {
                        SqlCommand gcmd1 = new SqlCommand("delete from mas_hrdetails where empid=(select empid from master_employee where refno='" + dtExcelRecords2.Rows[i][0] + "') and hrtype='" + 5 + "' and desc01='" + "No Records" + "'", scon);
                        SqlDataAdapter da1 = new SqlDataAdapter(gcmd1);
                        da1.Fill(ds);
                    }
                    try
                    {
                         int age = 40;
                        int option;
                        if (dtExcelRecords2.Rows[i][39].ToString() == "Yes")
                            option = 1;
                        else
                            option = 0;
                        scon.Open();
                        SqlCommand cmd = new SqlCommand("spInsert_Personal_Details", scon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = dtExcelRecords2.Rows[i][0];
                        cmd.Parameters.Add("@HRType", SqlDbType.Int).Value = 5;
                        cmd.Parameters.Add("@Desc01", SqlDbType.VarChar).Value = dtExcelRecords2.Rows[i][2];
                        cmd.Parameters.Add("@Desc02", SqlDbType.VarChar).Value = dtExcelRecords2.Rows[i][3];
                        cmd.Parameters.Add("@desc04", SqlDbType.VarChar).Value = dtExcelRecords2.Rows[i][4];
                        cmd.Parameters.Add("@age", SqlDbType.Int).Value = age;
                        cmd.Parameters.Add("@OPTIONYN", SqlDbType.Int).Value = option;

                        count += cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    finally
                    {
                        scon.Close();
                    }
                }
            
                getExcelSheetName2 = dtExcelSheetName.Rows[2]["Table_Name"].ToString();
                cmds.CommandText = "SELECT * FROM [" + getExcelSheetName2 + "]";
                dAdapter1.SelectCommand = cmds;
                dAdapter1.Fill(dtExcelRecords4);

                for (int i = 1; i < dtExcelRecords4.Rows.Count; i++)
                {
                    SqlCommand gcmd = new SqlCommand("Select empid from mas_hrdetails where empid=(select empid from master_employee where refno='" + dtExcelRecords4.Rows[i][0] + "') and hrtype='" + 2 + "' and desc01='" + "No Records" + "'", scon);
                    SqlDataAdapter da = new SqlDataAdapter(gcmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null || ds.Tables[0].Rows.Count == 1)
                    {
                        SqlCommand gcmd1 = new SqlCommand("delete from mas_hrdetails where empid=(select empid from master_employee where refno='" + dtExcelRecords4.Rows[i][0] + "') and hrtype='" + 2 + "' and desc01='" + "No Records" + "'", scon);
                        SqlDataAdapter da1 = new SqlDataAdapter(gcmd1);
                        da1.Fill(ds);
                    }
                    try
                    {
                        string pass = dtExcelRecords4.Rows[i][6].ToString();
                        scon.Open();
                        SqlCommand cmd = new SqlCommand("spInsert_Personal_Details", scon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = dtExcelRecords4.Rows[i][0];
                        cmd.Parameters.Add("@HRType", SqlDbType.Int).Value = 2;
                        cmd.Parameters.Add("@Desc01", SqlDbType.VarChar).Value = dtExcelRecords4.Rows[i][2];
                        cmd.Parameters.Add("@Desc02", SqlDbType.VarChar).Value = dtExcelRecords4.Rows[i][3];
                        cmd.Parameters.Add("@desc04", SqlDbType.VarChar).Value = dtExcelRecords4.Rows[i][4];
                        cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = pass;
                        cmd.Parameters.Add("@NUM01", SqlDbType.VarChar).Value = dtExcelRecords4.Rows[i][5];
                        count += cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    finally
                    {
                        scon.Close();
                    }
                }
            }
        }
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,\"" + ((HiddenField)e.Row.FindControl("hfgvempid")).Value.Trim() + "\",\"" + ((Label)e.Row.FindControl("lblgvempName")).Text.Trim() + "\");";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //scon.Close();
        
        DataSet rptDs = new DataSet();
        if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
            scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCH"].ConnectionString);
        else if (Convert.ToInt16(Session["locationid"].ToString()) == 3)
            scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCMB"].ConnectionString);
        else
        {
            if(rbLocation.SelectedValue=="2")
                scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCH"].ConnectionString);
            else
                scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCMB"].ConnectionString);
        }
            
        scon.Open();
        SqlCommand cmdsu = new SqlCommand("sp_GetEmpDetails1", scon);
        cmdsu.CommandType = CommandType.StoredProcedure;
        cmdsu.Parameters.Add("@empid", SqlDbType.VarChar).Value = txtEmp.Text;
        cmdsu.Parameters.Add("@teamid", SqlDbType.VarChar).Value = Session["employeeid"].ToString();
        cmdsu.ExecuteNonQuery();
        SqlDataAdapter daRpt = new SqlDataAdapter(cmdsu);
        daRpt.Fill(rptDs);
        dtEmp = rptDs.Tables[0].Copy();
        GvEmp.DataSource = rptDs;
        GvEmp.DataBind();
        this.showPanel(div_Summary_details);
    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "div_employee_details":
                miEmpDetails.Attributes.Add("class", "current");
                miPersonalDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "");
                miManualLeave.Attributes.Add("class", "");
                miFeedbackDetails.Attributes.Add("class", "");
                miDocuments.Attributes.Add("class", "");
                this.div_Preview.Visible = false;
                this.div_employee_details.Visible = true;
                this.Personal_Details.Visible = false;
                this.div_Summary_details.Visible = false;
                this.div_LeaveCorrection.Visible = false;
                this.div_FeedBack_details.Visible = false;
                this.div_documents.Visible = false;
                break;
            case "Personal_Details":
                miEmpDetails.Attributes.Add("class", "");
                miPersonalDetails.Attributes.Add("class", "current");
                miSummaryDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "");
                miManualLeave.Attributes.Add("class", "");
                miFeedbackDetails.Attributes.Add("class", "");
                miDocuments.Attributes.Add("class", "");
                this.div_Preview.Visible = false;
                this.div_employee_details.Visible = false;
                this.Personal_Details.Visible = true;
                this.div_Summary_details.Visible = false;
                this.div_LeaveCorrection.Visible = false;
                this.div_FeedBack_details.Visible = false;
                this.div_documents.Visible = false;
                break;
            case "div_Summary_details":
                miEmpDetails.Attributes.Add("class", "");
                miPersonalDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "current");
                miSummaryDetails.Attributes.Add("class", "");
                miManualLeave.Attributes.Add("class", "");
                miFeedbackDetails.Attributes.Add("class", "");
                miDocuments.Attributes.Add("class", "");
                this.div_Preview.Visible = false;
                this.div_employee_details.Visible = false;
                this.Personal_Details.Visible = false;
                this.div_Summary_details.Visible = true;
                this.div_LeaveCorrection.Visible = false;
                this.div_FeedBack_details.Visible = false;
                this.div_documents.Visible = false;
                break;
            case "div_Preview":
                miEmpDetails.Attributes.Add("class", "");
                miPersonalDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "current");
                miManualLeave.Attributes.Add("class", "");
                miFeedbackDetails.Attributes.Add("class", "");
                miDocuments.Attributes.Add("class", "");
                this.div_Preview.Visible = true;
                this.div_employee_details.Visible = false;
                this.Personal_Details.Visible = false;
                this.div_Summary_details.Visible = false;
                this.div_LeaveCorrection.Visible = false;
                this.div_FeedBack_details.Visible = false;
                this.div_documents.Visible = false;
                break;
            case "div_LeaveCorrection":
                miEmpDetails.Attributes.Add("class", "");
                miPersonalDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "");
                miManualLeave.Attributes.Add("class", "current");
                miFeedbackDetails.Attributes.Add("class", "");
                miDocuments.Attributes.Add("class", "");
                this.div_Preview.Visible = false;
                this.div_employee_details.Visible = false;
                this.Personal_Details.Visible = false;
                this.div_Summary_details.Visible = false;
                this.div_LeaveCorrection.Visible = true;
                this.div_FeedBack_details.Visible = false;
                this.div_documents.Visible = false;
                break;
            case "div_FeedBack_details":
                miEmpDetails.Attributes.Add("class", "");
                miPersonalDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "");
                miManualLeave.Attributes.Add("class", "");
                miFeedbackDetails.Attributes.Add("class", "current");
                miDocuments.Attributes.Add("class", "");
                this.div_Preview.Visible = false;
                this.div_employee_details.Visible = false;
                this.Personal_Details.Visible = false;
                this.div_Summary_details.Visible = false;
                this.div_LeaveCorrection.Visible = false;
                this.div_FeedBack_details.Visible = true;
                this.div_documents.Visible = false;
                break;
            case "div_documents":
                miEmpDetails.Attributes.Add("class", "");
                miPersonalDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "");
                miSummaryDetails.Attributes.Add("class", "");
                miManualLeave.Attributes.Add("class", "");
                miFeedbackDetails.Attributes.Add("class", "");
                miDocuments.Attributes.Add("class", "current");
                this.div_Preview.Visible = false;
                this.div_employee_details.Visible = false;
                this.Personal_Details.Visible = false;
                this.div_Summary_details.Visible = false;
                this.div_LeaveCorrection.Visible = false;
                this.div_FeedBack_details.Visible = false;
                this.div_documents.Visible = true;
                break;
        }
    }
    protected void lnkEmpDetails_Click(object sender, EventArgs e)
    {
        if (Session["employeeid"] != null)
        {
            int empid = Convert.ToInt32(Session["employeeid"]);
            if (empid != 2439)
            {
                this.showPanel(div_employee_details);
                loadDetails();
            }
        }
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
    protected void lnkSummaryDetails_Click(object sender, EventArgs e)
    {
        this.showPanel(div_Summary_details);
        
    }
    private void loadDetails()
    {
        DataSet SqlDs = new DataSet();
        if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
        {
            HRMS_CH hrCh = new HRMS_CH();
            SqlDs = hrCh.GetEmpDetail(hfP_ID.Value);
        }
        else if (Convert.ToInt16(Session["locationid"].ToString()) == 3)
        {
            HRMS_CMB hrCh = new HRMS_CMB();
            SqlDs = hrCh.GetEmpDetail(hfP_ID.Value);
        }
        else
        {
            if (rbLocation.SelectedValue == "2")
            {
                HRMS_CH hrCh = new HRMS_CH();
                SqlDs = hrCh.GetEmpDetail(hfP_ID.Value);
            }
            else 
            {
                HRMS_CMB hrCh = new HRMS_CMB();
                SqlDs = hrCh.GetEmpDetail(hfP_ID.Value);
            }
        }

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
        txtBank.Text = row["bankname"].ToString();
        txtBankAcc.Text = row["bankacno"].ToString();
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
        txtProbConDate.Text = row["Prob_CONF_DATE"].ToString();
        txtConDate.Text = row["CONFIRMATIONDATE"].ToString();
        if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
        {
            if ("~/Photos/Chennai/" + row["Refno"].ToString() + ".jpg" == null)
                imgPhoto.ImageUrl = "~/Photos/Chennai/index.jpg";
            else
                imgPhoto.ImageUrl = "~/Photos/Chennai/" + row["Refno"].ToString() + ".jpg";
        }
        else
        {
            if ("~/Photos/CMB/" + row["Refno"].ToString() + ".jpg" == null)
                imgPhoto.ImageUrl = "~/Photos/CMB/index.jpg";
            else
                imgPhoto.ImageUrl = "~/Photos/CMB/" + row["Refno"].ToString() + ".jpg";
        }
        if ("~/Photos/CMB/" + row["Refno"].ToString() + ".jpg" == null)
            imgPhoto.ImageUrl = "~/Photos/CMB/index.jpg";
        else
            imgPhoto.ImageUrl = "~/Photos/CMB/" + row["Refno"].ToString() + ".jpg";

        string path = string.Empty;
        path = @"\\192.9.201.222\dp\MIS\TermCond" + "\\" + Regex.Replace(txtEmpid.Text.ToString(), "[^a-zA-Z0-9_]+", " ");
        btnImg.Visible = true;
        if (File.Exists(path + ".jpg"))
        {
            btnImg.ImageUrl = "~/images/QMS/img.png";
            btnImg.Width = 50;
            btnImg.Height = 50;
        }
        else if (File.Exists(path + ".png"))
        {
            btnImg.ImageUrl = "~/images/QMS/img.png";
            btnImg.Width = 50;
            btnImg.Height = 50;
        }
        else if (File.Exists(path + ".xls"))
        {
            btnImg.ImageUrl = "~/images/QMS/excel.png";
            btnImg.Width = 50;
            btnImg.Height = 50;
        }
        else if (File.Exists(path + ".xlsx"))
        {
            btnImg.ImageUrl = "~/images/QMS/excel.png";
            btnImg.Width = 50;
            btnImg.Height = 50;
        }
        else if (File.Exists(path + ".pdf"))
        {
            btnImg.ImageUrl = "~/images/QMS/pdf13.png";
            btnImg.Width = 50;
            btnImg.Height = 50;
        }
        else if (File.Exists(path + ".doc"))
        {
            btnImg.ImageUrl = "~/images/QMS/Word13.png";
            btnImg.Width = 50;
            btnImg.Height = 50;
        }
        else if (File.Exists(path + ".docx"))
        {
            btnImg.ImageUrl = "~/images/QMS/Word13.png";
            btnImg.Width = 50;
            btnImg.Height = 50;
        }
        else
        {
            btnImg.Visible = false;
        }
        txtEmailid.Text = row["email"].ToString();
        txtPhone.Text = row["phone"].ToString();
        txtMobile.Text = row["mobile"].ToString();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
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
                    {"@phone",txtPhone.Text},{"@mobile",txtMobile.Text},{"@Email",txtEmailid.Text},{"@ConfirmationDate",txtConDate.Text},
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

                this.showPanel(div_employee_details);
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
    protected void DropMaritalStatus_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (DropMaritalStatus.SelectedValue == "SINGLE")
            txtSpouse.Enabled = false;
        else
            txtSpouse.Enabled = true;
        this.showPanel(div_employee_details);
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
    protected void rdBtnLive_CheckedChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
            scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCH"].ConnectionString);
        else if (Convert.ToInt16(Session["locationid"].ToString()) == 3)
            scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCMB"].ConnectionString);
        else
        {
            if (rbLocation.SelectedValue == "2")
                scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCH"].ConnectionString);
            else
                scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCMB"].ConnectionString);
        }
        scon.Open();
        SqlCommand gcmd;
        if(rdBtnLive.Checked)
            gcmd = new SqlCommand("select Row_Number() OVER (ORDER BY (refno)) as sl,convert(varchar(20),doj,106) doj,sex,convert(varchar(20),dol,106) dol,convert(varchar(20),confirmationdate,106) confirmationdate,case when confirmationdate is null then convert(varchar(20),Prob_CONF_DATE,106) else null end Prob_CONF_DATE,* from master_employee where dol is null order by refno", scon);
        else
            gcmd = new SqlCommand("select Row_Number() OVER (ORDER BY (refno)) as sl,convert(varchar(20),doj,106) doj,sex,convert(varchar(20),dol,106) dol,convert(varchar(20),confirmationdate,106) confirmationdate,case when confirmationdate is null then convert(varchar(20),Prob_CONF_DATE,106) else null end Prob_CONF_DATE,* from master_employee order by refno", scon);
        SqlDataAdapter da = new SqlDataAdapter(gcmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        dtEmp = ds.Tables[0].Copy();
        GvEmp.DataSource = ds;
        GvEmp.DataBind();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        string month = txtMonth.Text;
        string year = txtYear.Text;
        if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
            scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCH"].ConnectionString);
        else if (Convert.ToInt16(Session["locationid"].ToString()) == 3)
            scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCMB"].ConnectionString);
        else
        {
            if (rbLocation.SelectedValue == "2")
                scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCH"].ConnectionString);
            else
                scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCMB"].ConnectionString);
        }
        scon.Open();
        SqlCommand gcmd = new SqlCommand("select Row_Number() OVER (ORDER BY (refno)) as sl,convert(varchar(20),doj,106) doj,sex,convert(varchar(20),dol,106) dol,convert(varchar(20),confirmationdate,106) confirmationdate, " +
                                    "case when confirmationdate is null then convert(varchar(20),Prob_CONF_DATE,106) else null end Prob_CONF_DATE,* from master_employee where month(Prob_CONF_DATE)='" + month + "' and year(Prob_CONF_DATE)='" + year + "'", scon);
        SqlDataAdapter da = new SqlDataAdapter(gcmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        dtEmp = ds.Tables[0].Copy();
        GvEmp.DataSource = ds;
        GvEmp.DataBind();
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
        this.showPanel(div_employee_details);
    }
    protected void GvEmp_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView();
        string di = "";
        try
        {
            SortDirection sortDirection = SortDirection.Ascending;
            if (ViewState[e.SortExpression] != null)
            {
                SortDirection currDirection = (SortDirection)ViewState[e.SortExpression];
                if (currDirection == SortDirection.Ascending) sortDirection = SortDirection.Descending;
            }
            ViewState[e.SortExpression] = sortDirection;
            if (sortDirection.Equals(SortDirection.Descending)) di = " desc"; dv.Table = dtEmp;
            dv.Sort = e.SortExpression + di;
            sSortExpression = e.SortExpression;
            GvEmp.DataSource = dv;
            GvEmp.DataBind();
        }
        catch (Exception ec)
        {
            
        }
        finally
        {
            dv.Dispose();
        }
    }
    protected void exportExl_Click(object sender, ImageClickEventArgs e)
    {
            if (dtEmp != null && dtEmp.Rows.Count > 0)
            {
                StringBuilder sbData = new StringBuilder();
                sbData.Append("<table border='1'>");
                sbData.Append("<tr valign='top'><td colspan='15' align='center'><h4>Employee Summary</h4></td><tr>");
                sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>EmplD</b></td><td bgcolor='silver'><b>Emp Name</b></td><td bgcolor='silver'><b>Designation</b></td><td bgcolor='silver'><b>Department</b></td>" +
                    "<td bgcolor='silver'><b>DOJ</b></td><td bgcolor='silver'><b>Prob. Date</b></td><td bgcolor='silver'><b>Conf. Date</b></td><td bgcolor='silver'><b>DOL</b></td>"+
                    "<td bgcolor='silver'><b>Gender</b></td></tr>");
                foreach (DataRow r in dtEmp.Rows)
                {
                    sbData.Append("<tr valign='top'>");
                    sbData.Append("<td>" + r["sl"] + "</td>");
                    sbData.Append("<td>" + r["Refno"] + "</td>");
                    sbData.Append("<td>" + r["empname"] + "</td>");
                    sbData.Append("<td>" + r["designation"] + "</td>");
                    sbData.Append("<td>" + r["department"] + "</td>");
                    sbData.Append("<td>" + r["doj"] + "</td>");
                    sbData.Append("<td>" + r["Prob_CONF_DATE"] + "</td>");
                    sbData.Append("<td>" + r["confirmationdate"] + "</td>");
                    sbData.Append("<td>" + r["DOL"] + "</td>");
                    sbData.Append("<td>" + r["Sex"] + "</td>");
                    sbData.Append("</tr>");
                }
                sbData.Append("</table>");
                Response.Clear();
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Employee_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
                Response.ContentType = "application/ms-excel";
                //Response.ContentEncoding = Encoding.Unicode;
                //Response.BinaryWrite(Encoding.Unicode.GetPreamble());
                Response.Write(sbData.ToString());
                Response.End();
            }
     }
    protected void lnkManualLeave_Click(object sender, EventArgs e)
    {
        this.showPanel(div_LeaveCorrection);
        if (hfP_ID.Value != "")
        {
            loadLeaveCorrection(hfP_ID.Value.Replace("DDS", "").Replace("DP", "").Replace("SDS", ""));
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Select Employee Details..!');</script>");
            this.showPanel(div_Summary_details);
        }
    }
    public void loadLeaveCorrection(string empid)
    {
        SaveBtn.Enabled = true;
        DataSet SqlDs = new DataSet();
        datasourceSQL SqlObj = new datasourceSQL();
        int loc = 0;
        cl = 0;
        sl = 0;
        try
        {
            int id = Convert.ToInt16(Session["locationid"].ToString());
            if(Session["locationid"].ToString()=="1")
            {
                id =  Convert.ToInt16(rbLocation.SelectedValue);
            }
            else
            {
                id = Convert.ToInt16(Session["locationid"].ToString());
            }
            DataSet ds = new DataSet();
            DataSet SqlD = new DataSet();
            
            if (id == 2)
            {
                HRMS_CH hrCh = new HRMS_CH();
                SqlD = hrCh.GetEmployeeDetail(Convert.ToInt16(empid));
                DataRow row = SqlD.Tables[0].Rows[0];
                DepartmentLbl.Text = row["Department"].ToString();
                DesignationLbl.Text = row["Designation"].ToString();
                ds = SqlObj.GetEmpDetails(Convert.ToInt16(empid), id);
            }
            else
            {
                HRMS_CMB hrCh = new HRMS_CMB();
                SqlD = hrCh.GetEmployeeDetail(Convert.ToInt16(empid));
                DataRow row = SqlD.Tables[0].Rows[0];
                DepartmentLbl.Text = row["Department"].ToString();
                DesignationLbl.Text = row["Designation"].ToString();
                ds = SqlObj.GetEmpDetails(Convert.ToInt16(empid), id);                
            }
            EmpcodeLbl.Text = ds.Tables[0].Rows[0]["employee_number"].ToString();
            NameLbl.Text = ds.Tables[0].Rows[0]["fname"].ToString() + ' ' + ds.Tables[0].Rows[0]["surname"].ToString();
            DateLbl.Text = DateTime.Now.ToShortDateString();
            SqlDs = SqlObj.ExcuteSelectProcedure("spGet_LeaveType", "", "", "", "", CommandType.StoredProcedure);
            if (SqlDs != null)
            {
                //LeavetypeDDList.DataSource = SqlDs.Tables[0];
                //LeavetypeDDList.DataBind();

            }
            Session["HR_Empid"] = ds.Tables[0].Rows[0]["employee_id"].ToString();
            GetAvailableDates(Session["HR_Empid"].ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            SqlDs = null;
            SqlObj = null;
        }
    }
    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (LeaveReasonTxt.Text == "") sMessage += i++ + ". Enter the Reason Details!.\\r\\n";
        if (grvLeave.Rows.Count == 0) sMessage += i++ + ". Select Date and click Submit button!.\\r\\n";
        if (i > 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + sMessage + " ');</script>");
            return false;
        }
        return true;
    }
    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        string msg = "", msg1 = "", datedetails = "";
        bool intimate = false;
        ArrayList al = new ArrayList();
        Hashtable val = null;
        try
        {
            if (validateScreen())
            {

                DataTable table2 = new DataTable("Leave");
                table2.Columns.Add("Date");
                table2.Columns.Add("Days");
                table2.Columns.Add("Type");
                table2.Columns.Add("DateDetails");
                table2.Columns.Add("PerMins");
                foreach (GridViewRow grw in grvLeave.Rows)
                {
                    datedetails = "";
                    val = new Hashtable();
                    Label Date = (Label)grw.FindControl("lblDate");
                    DropDownList ddFirst = (DropDownList)grw.FindControl("LeavetypeFirst");
                    CheckBox ckFirst = (CheckBox)grw.FindControl("ckFirstHalf");
                    DropDownList ddSecond = (DropDownList)grw.FindControl("LeavetypeSecond");
                    CheckBox ckSecond = (CheckBox)grw.FindControl("ckSecondHalf");
                    DropDownList PermissionMins = (DropDownList)grw.FindControl("PermissionMins");
                    bool f = ckFirst.Checked;
                    bool s = ckSecond.Checked;

                    if (f == true && s == true)
                    {
                        if (datedetails == "")
                        {
                            string ss = Convert.ToDateTime(Date.Text).Day.ToString();
                            datedetails = ss + '-' + Convert.ToDateTime(Date.Text).ToString("MMM");
                        }
                        else
                            datedetails = datedetails + ";" + Convert.ToDateTime(Date.Text).Day + '-' + Convert.ToDateTime(Date.Text).ToString("MMMMM");

                        if (ddFirst.SelectedValue == ddSecond.SelectedValue)
                        {
                            table2.Rows.Add(Date.Text, 1, ddFirst.SelectedValue, datedetails,0);
                        }
                        else
                        {
                            if (ddFirst.SelectedValue.ToString() == "3")
                                table2.Rows.Add(Date.Text, 1.0, ddFirst.SelectedValue, datedetails + " First Half", PermissionMins.SelectedValue);
                            else
                                table2.Rows.Add(Date.Text, 0.5, ddFirst.SelectedValue, datedetails + " First Half", 0);

                            if (ddSecond.SelectedValue.ToString() == "3")
                                table2.Rows.Add(Date.Text, 1.0, ddSecond.SelectedValue, datedetails + " Second Half", PermissionMins.SelectedValue);
                            else
                                table2.Rows.Add(Date.Text, 0.5, ddSecond.SelectedValue, datedetails + " Second Half", 0);
                        }
                    }
                    else if (f == true && s == false)
                    {
                        if (datedetails == "")
                        {
                            string ss = Convert.ToDateTime(Date.Text).Day.ToString();
                            datedetails = ss + '-' + Convert.ToDateTime(Date.Text).ToString("MMM") + " First Half";
                        }
                        else
                            datedetails = datedetails + ";" + Convert.ToDateTime(Date.Text).Day + '-' + Convert.ToDateTime(Date.Text).ToString("MMM") + " First Half";
                        if (ddFirst.SelectedValue.ToString() == "3")
                            table2.Rows.Add(Date.Text, 1.0, ddFirst.SelectedValue, datedetails, PermissionMins.SelectedValue);
                        else
                            table2.Rows.Add(Date.Text, 0.5, ddFirst.SelectedValue, datedetails, 0);
                    }
                    else if (f == false && s == true)
                    {
                        if (datedetails == "")
                        {
                            string ss = Convert.ToDateTime(Date.Text).Day.ToString();
                            datedetails = ss + '-' + Convert.ToDateTime(Date.Text).ToString("MMM") + " Second Half";
                        }
                        else
                            datedetails = datedetails + ";" + Convert.ToDateTime(Date.Text).Day + '-' + Convert.ToDateTime(Date.Text).ToString("MMMMM") + " Second Half";

                        if (ddSecond.SelectedValue.ToString() == "3")
                            table2.Rows.Add(Date.Text, 1.0, ddSecond.SelectedValue, datedetails, PermissionMins.SelectedValue);
                        else
                            table2.Rows.Add(Date.Text, 0.5, ddSecond.SelectedValue, datedetails, 0);
                    }
                }
                DataSet lv = new DataSet("Leave");
                lv.Tables.Add(table2);

                DataRow[] Presult = table2.Select("Type = 1");
                if (Presult.Length > 0)
                {
                    foreach (DataRow row in Presult)
                    {
                        if (p == 0)
                            p = Convert.ToDecimal(row["Days"].ToString());
                        else
                            p = p + Convert.ToDecimal(row["Days"].ToString());
                    }
                }
                DataRow[] Sresult = table2.Select("Type = 2");
                if (Sresult.Length > 0)
                {
                    foreach (DataRow row in Sresult)
                    {
                        if (s == 0)
                            s = Convert.ToDecimal(row["Days"].ToString());
                        else
                            s = s + Convert.ToDecimal(row["Days"].ToString());
                    }
                }
                DataRow[] Cresult = table2.Select("Type = 7");
                if (Cresult.Length > 0)
                {
                    foreach (DataRow row in Cresult)
                    {
                        if (c == 0)
                            c = Convert.ToDecimal(row["Days"].ToString());
                        else
                            c = c + Convert.ToDecimal(row["Days"].ToString());
                    }
                }
                DataRow[] PMresult = table2.Select("Type = 3");
                if (PMresult.Length > 0)
                {
                    foreach (DataRow row in PMresult)
                    {
                        if (pm == 0)
                            pm = Convert.ToDecimal(row["Days"].ToString());
                        else
                            pm = pm + Convert.ToDecimal(row["Days"].ToString());
                    }
                }
                DataRow[] LOPresult = table2.Select("Type = 5");
                if (LOPresult.Length > 0)
                {
                    foreach (DataRow row in LOPresult)
                    {
                        if (LOP == 0)
                            LOP = Convert.ToDecimal(row["Days"].ToString());
                        else
                            LOP = LOP + Convert.ToDecimal(row["Days"].ToString());
                    }
                }
                DataRow[] ULOPresult = table2.Select("Type = 9");
                if (ULOPresult.Length > 0)
                {
                    foreach (DataRow row in ULOPresult)
                    {
                        if (ULOP == 0)
                            ULOP = Convert.ToDecimal(row["Days"].ToString());
                        else
                            ULOP = ULOP + Convert.ToDecimal(row["Days"].ToString());
                    }
                }
                
                //Leave Policy Validation
                if ((p + c + s) != 0)
                {
                    string pp, cc, ssl;
                    if (PLLeaveHF.Value == "")
                        pp = "0.00";
                    else
                        pp = PLLeaveHF.Value;
                    if (CLLeaveHF.Value == "")
                        cc = "0.00";
                    else
                        cc = CLLeaveHF.Value;
                    if (SLLeaveHF.Value == "")
                        ssl = "0.00";
                    else
                        ssl = SLLeaveHF.Value;

                    //decimal val1;
                    //decimal val2 = Convert.ToDecimal(pp) + Convert.ToDecimal(cc) + Convert.ToDecimal(ssl);
                    //string[,] paramcol1 ={
                    //    {"@empid",Session["employeeid"].ToString()},{"@Bal",val2.ToString()},{"@flg","Output"}
                    //            };
                    //val1 = SqlObj.ExcSProcedure("spGetLeavePolicy", paramcol1, CommandType.StoredProcedure);

                    //if ((val1 - p - c - s) < 0)
                    //{
                    //    msg = "You are only Eligible for " + val1.ToString() + " days";
                    //    ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + msg + "');</script>");
                    //    return;
                    //}
                }

                if ((p + c + s + pm + LOP + ULOP) == 0)
                {
                    msg = "Please select Leave details (First/Second Half or Full Days)";
                    ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + msg + "');</script>");
                    return;
                }

                if (p > 0)
                {
                    if ((PLLeaveHF.Value == "" || System.Convert.IsDBNull(PLLeaveHF.Value) || Convert.ToDouble(p) > Convert.ToDouble(PLLeaveHF.Value)))//For preffered Leave
                    {
                        msg = "PL";
                        intimate = true;
                    }
                }
                if (s > 0)
                {
                    if ((SLLeaveHF.Value == "" || System.Convert.IsDBNull(SLLeaveHF.Value) || Convert.ToDouble(s) > Convert.ToDouble(SLLeaveHF.Value)))//For Sick Leave
                    {
                        if (msg == "")
                            msg = "SL";
                        else
                            msg = msg + "/SL";
                        intimate = true;
                    }
                }
                if (c > 0)
                {
                    if ((CLLeaveHF.Value == "" || System.Convert.IsDBNull(CLLeaveHF.Value) || Convert.ToDouble(c) > Convert.ToDouble(CLLeaveHF.Value)))//For CL 
                    {
                        if (msg == "")
                            msg = "CL";
                        else
                            msg = msg + "/CL";
                        intimate = true;
                    }
                }
                if (pm > 0)
                {
                    int v;
                    string[,] paramcol ={
                                         {"@empid",Session["HR_Empid"].ToString()},{"@val","Output"}
                                        };
                    v = SqlObj.ExcSProcedure("sp_PerHistory", paramcol, CommandType.StoredProcedure);
                    if (v == 1)
                    {
                        msg1 = "Already you have taken Permission .. Plz contact HR ";
                        intimate = true;
                    }
                    else if (v == 2)
                    {
                        if (Pmins == 60)
                        {
                            msg1 = "Already you have taken 30 mins Permission .. So you have 30 mins remaining";
                            intimate = true;
                        }
                        else
                        {
                            intimate = false;
                        }
                    }
                }

                if (intimate == false)
                {
                    string Level1 = "", Level2 = "";
                    string rec_l1="", rec_l2="";
                    DataSet dss = new DataSet();
                    dss = SqlObj.GetTeamHead(Convert.ToInt16(Session["employeeid"].ToString()));
                    if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                    {
                        Level1 = Session["employeeid"].ToString();
                        Level2 = "Null";
                        rec_l1 = DateTime.Now.ToShortDateString();
                        rec_l2 = "Null";
                    }
                    else if (dss.Tables[0].Rows[0]["Position"].ToString() == "HR")
                    {
                        Level1 = "Null";
                        Level2 = "Null";
                        rec_l1 = "Null";
                        rec_l2 = "Null";
                    }
                    else
                    {
                        Level1 = Session["employeeid"].ToString();
                        Level2 = Session["employeeid"].ToString();
                        rec_l1 = DateTime.Now.ToShortDateString();
                        rec_l2 = DateTime.Now.ToShortDateString();
                    }
                    if (lv.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < lv.Tables[0].Rows.Count; i++)
                        {
                            val = new Hashtable();
                            val.Add("ID", Session["HR_Empid"].ToString());
                            val.Add("LeaveIn", lv.Tables[0].Rows[i]["Date"].ToString());
                            val.Add("LeaveOut", lv.Tables[0].Rows[i]["Date"].ToString());
                            val.Add("Date", DateTime.Now.ToShortDateString());
                            val.Add("LeaveType", lv.Tables[0].Rows[i]["Type"].ToString());
                            val.Add("Days", lv.Tables[0].Rows[i]["Days"].ToString());
                            val.Add("DateDetails", lv.Tables[0].Rows[i]["DateDetails"].ToString());
                            val.Add("Remarks", LeaveReasonTxt.Text);
                            val.Add("Approved", Level1);
                            val.Add("Recorded", rec_l1);
                            val.Add("Approved_L2", Level2);
                            val.Add("Recorded_L2", rec_l2);
                            val.Add("ModeofInform", ModeofInform.SelectedValue);
                            val.Add("AppliedDate", txtAppliedDate.Text + " " + txtTime.Text);
                            val.Add("PerMins", lv.Tables[0].Rows[i]["PerMins"].ToString());
                            al.Add(val);
                        }
                    }
                    if (!SqlObj.Insert_LeaveDetailsTeamHead(al))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
                    }
                    else
                    {
                        GetAvailableDates(Session["HR_Empid"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Requested " + msg + " leave days is greater than available leave days ');</script>");
                }
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(" + msg + " );</script>");
            SqlObj = null;
        }
    }
    private void ClearFunction()
    {
        FromTxt.Text = "";
        ToTxt.Text = "";
        LeaveReasonTxt.Text = "";
        ListItem item = new ListItem();
        item.Value = "0";
        item.Text = "None";
        SaveBtn.Enabled = true;
    }

    private void GetAvailableDates(string empid)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        DataSet SqlDs = new DataSet();
        try
        {
            SqlDs = SqlObj.ExcuteSelectProcedure("SPGET_MY_LEAVE_STATUS",empid.ToString(), "@employee_id", "int", "Input", CommandType.StoredProcedure);
            if (SqlDs != null)
            {
                string LeaveDet = "<table class='bordertable' cellpadding='2' cellspacing='5' width='300px'><tr><td colspan='3' style='font-weight:bold;color:Red;' align='center'>Leave Status</td></tr>";
                LeaveDet = LeaveDet + "<tr><th>Description</th><th>PL</th><th>SL</th><th>CL</th><th>Total</th></tr>";
                for (int i = 0; i < SqlDs.Tables[0].Rows.Count; i++)
                {
                    LeaveDet += "<tr><td>Available Leave</td>";
                    LeaveDet += "<td>" + SqlDs.Tables[0].Rows[i]["PL_AVAILABLE_LEAVE"].ToString() + "</td>";
                    LeaveDet += "<td>" + SqlDs.Tables[0].Rows[i]["SL_AVAILABLE_LEAVE"].ToString() + "</td>";
                    LeaveDet += "<td>" + SqlDs.Tables[0].Rows[i]["CL_AVAILABLE_LEAVE"].ToString() + "</td></tr>";
                    if (SqlDs.Tables[0].Rows[i]["PL_AVAILABLE_LEAVE"].ToString() != "" && SqlDs.Tables[0].Rows[i]["PL_AVAILABLE_LEAVE"].ToString() != null)
                        PLLeaveHF.Value = SqlDs.Tables[0].Rows[i]["PL_AVAILABLE_LEAVE"].ToString();
                    if (SqlDs.Tables[0].Rows[i]["SL_AVAILABLE_LEAVE"].ToString() != "" && SqlDs.Tables[0].Rows[i]["SL_AVAILABLE_LEAVE"].ToString() != null)
                        SLLeaveHF.Value = SqlDs.Tables[0].Rows[i]["SL_AVAILABLE_LEAVE"].ToString();
                    if (SqlDs.Tables[0].Rows[i]["CL_AVAILABLE_LEAVE"].ToString() != "" && SqlDs.Tables[0].Rows[i]["CL_AVAILABLE_LEAVE"].ToString() != null)
                        CLLeaveHF.Value = SqlDs.Tables[0].Rows[i]["CL_AVAILABLE_LEAVE"].ToString();
                }
                LeaveDet += "</table>";
                LeaveDetaildiv.InnerHtml = LeaveDet;
            }
            FromTxt.Text = "";
            ToTxt.Text = "";
            LeaveReasonTxt.Text = "";
            grvLeave.DataSourceID = null;
            grvLeave.DataBind();
            txtAppliedDate.Text = "";
            txtTime.Text = "00:00";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            SqlDs = null;
            SqlObj = null;
        }
    }
    protected void DateListBtn_Click(object sender, EventArgs e)
    {
        string test = "", msg = ""; ;
        DateTime fdate = Convert.ToDateTime(FromTxt.Text);
        DateTime tdate = Convert.ToDateTime(ToTxt.Text);
        if (FromTxt.Text == ToTxt.Text)
        {
            if (Convert.ToDateTime(FromTxt.Text).ToString("dddd").ToUpper() == "SUNDAY")
            {
                msg = "Your Apllied Leave date is Sunday";
                ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + msg + "');</script>");
                return;
            }
        }
        int flg;
        string[,] paramcol ={
                    {"@employeeid",Session["HR_Empid"].ToString()},{"@leavein",FromTxt.Text},{"@leaveout",ToTxt.Text},{"@flg","Output"}
                            };
        flg = SqlObj.ExcSProcedure("SpGet_LEAVEHISTORY1", paramcol, CommandType.StoredProcedure);
        if (flg == 1)
        {
            msg = "Already Leave applied in this date!.";
            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + msg + "');</script>");
            return;
        }

        DataTable table1 = new DataTable("Leave");
        table1.Columns.Add("Date");
        while (fdate <= tdate)
        {
            table1.Rows.Add(fdate.ToShortDateString());
            fdate = fdate.AddDays(1);
        }
        DataSet lv = new DataSet("Leave");
        lv.Tables.Add(table1);
        grvLeave.DataSource = lv;
        grvLeave.DataBind();
    }
    private int CheckholidayDate(DateTime TestDate)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        int lflg;
        try
        {
            string[,] paramcol ={ { "@leavedate", TestDate.ToShortDateString() }, { "@lflg", "output" } };
            lflg = SqlObj.ExcSProcedure("spGet_BankLeave", paramcol, CommandType.StoredProcedure);
            return lflg;
        }
        catch (Exception ex)
        {
            return 2;
        }
        finally
        {
            SqlObj = null;
        }
    }
    protected void grvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Fisrt = e.Row.FindControl("LeavetypeFirst") as DropDownList;
            DropDownList second = e.Row.FindControl("LeavetypeSecond") as DropDownList;
            DropDownList PermissionMins = e.Row.FindControl("PermissionMins") as DropDownList;
            Label Date = e.Row.FindControl("lblDate") as Label;
            CheckBox FC = e.Row.FindControl("ckFirstHalf") as CheckBox;
            CheckBox SC = e.Row.FindControl("ckSecondHalf") as CheckBox;
            FC.Checked = true;
            SC.Checked = true;
            Label Days = e.Row.FindControl("lblDays") as Label;
            Days.Text = "1";
            DataSet SqlDs = new DataSet();
            SqlDs = SqlObj.ExcuteSelectProcedure("spGet_LeaveType", "", "", "", "", CommandType.StoredProcedure);
            if (SqlDs != null)
            {
                if (Convert.ToDateTime(Date.Text).ToString("dddd").ToUpper() == "SUNDAY")
                {
                    Fisrt.DataSource = SqlDs.Tables[0];
                    Fisrt.DataTextField = SqlDs.Tables[0].Columns[1].ToString();
                    Fisrt.DataValueField = SqlDs.Tables[0].Columns[0].ToString();
                    Fisrt.DataBind();
                    second.DataSource = SqlDs.Tables[0];
                    second.DataTextField = SqlDs.Tables[0].Columns[1].ToString();
                    second.DataValueField = SqlDs.Tables[0].Columns[0].ToString();
                    second.DataBind();
                    Fisrt.SelectedValue = "5";
                    second.SelectedValue = "5";
                    FC.Checked = true;
                    SC.Checked = true;
                    FC.Enabled = false;
                    SC.Enabled = false;
                    Fisrt.Enabled = false;
                    second.Enabled = false;
                }
                else
                {
                    Fisrt.DataSource = SqlDs.Tables[0];
                    Fisrt.DataTextField = SqlDs.Tables[0].Columns[1].ToString();
                    Fisrt.DataValueField = SqlDs.Tables[0].Columns[0].ToString();
                    Fisrt.DataBind();
                    second.DataSource = SqlDs.Tables[0];
                    second.DataTextField = SqlDs.Tables[0].Columns[1].ToString();
                    second.DataValueField = SqlDs.Tables[0].Columns[0].ToString();
                    second.DataBind();
                }
                if (Session["locationid"].ToString() == "2")
                {
                    e.Row.Cells[5].Visible = false;
                }
                else
                {
                    e.Row.Cells[5].Visible = true;
                }
            }
        }
    }
    protected void LeavetypeFirst_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        foreach (GridViewRow row in grvLeave.Rows)
        {
            Control First = row.FindControl("LeavetypeFirst") as DropDownList;
            Control Second = row.FindControl("LeavetypeSecond") as DropDownList;
            DropDownList F = (DropDownList)First;
            DropDownList S = (DropDownList)Second;

            Control PermissionMins = row.FindControl("PermissionMins") as DropDownList;
            DropDownList P = (DropDownList)PermissionMins;
            Control FirstChk = row.FindControl("ckFirstHalf") as CheckBox;
            Control SecondChk = row.FindControl("ckSecondHalf") as CheckBox;
            Label Days = row.FindControl("lblDays") as Label;
            CheckBox FC = (CheckBox)FirstChk;
            CheckBox SC = (CheckBox)SecondChk;
            if (FC.Checked)
            {
                if (F.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                    if (SC.Checked)
                    {
                        if (S.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                            Days.Text = "1";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "0.5";
                        }
                    }
                    else
                        Days.Text = "0.5";
                }
                else
                {
                    P.Visible = true;
                    if (SC.Checked)
                    {
                        if (S.SelectedValue.ToString() != "3")
                        {
                            if (F.SelectedValue.ToString() != "3")
                            {
                                P.Visible = false;
                            }
                            else
                            {
                                P.Visible = true;
                            }
                            Days.Text = "0.5";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "";
                        }
                    }
                    else
                    {
                        Days.Text = "";
                    }
                }
            }
            else
            {
                if (F.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                }
                else
                {
                    P.Visible = true;
                }
                if (SC.Checked)
                {
                    if (S.SelectedValue.ToString() != "3")
                    {
                        if (F.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                        }
                        else
                        {
                            if (FC.Checked)
                                P.Visible = true;
                            else
                                P.Visible = false;
                        }
                        Days.Text = "0.5";
                    }
                    else
                    {
                        P.Visible = true;
                        Days.Text = "";
                    }
                }
                else
                    Days.Text = "";
            }
        }
    }
    protected void LeavetypeSecond_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        foreach (GridViewRow row in grvLeave.Rows)
        {
            Control First = row.FindControl("LeavetypeFirst") as DropDownList;
            Control Second = row.FindControl("LeavetypeSecond") as DropDownList;
            DropDownList F = (DropDownList)First;
            DropDownList S = (DropDownList)Second;
            Control PermissionMins = row.FindControl("PermissionMins") as DropDownList;
            DropDownList P = (DropDownList)PermissionMins;
            Control FirstChk = row.FindControl("ckFirstHalf") as CheckBox;
            Control SecondChk = row.FindControl("ckSecondHalf") as CheckBox;
            Label Days = row.FindControl("lblDays") as Label;
            CheckBox FC = (CheckBox)FirstChk;
            CheckBox SC = (CheckBox)SecondChk;
            //if (S.SelectedValue.ToString() == "3" || F.SelectedValue.ToString() == "3")
            //{
            //    P.Visible = true;
            //}
            //else
            //{
            //    P.Visible = false;
            //}
            if (FC.Checked)
            {
                if (F.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                    if (SC.Checked)
                    {
                        if (S.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                            Days.Text = "1";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "0.5";
                        }
                    }
                    else
                        Days.Text = "0.5";
                }
                else
                {
                    P.Visible = true;
                    if (SC.Checked)
                    {
                        if (S.SelectedValue.ToString() != "3")
                        {
                            if (F.SelectedValue.ToString() != "3")
                            {
                                P.Visible = false;
                            }
                            else
                            {
                                P.Visible = true;
                            }
                            Days.Text = "0.5";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "";
                        }
                    }
                }
            }
            else
            {
                if (F.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                }
                else
                {
                    P.Visible = true;
                }
                if (SC.Checked)
                {
                    if (S.SelectedValue.ToString() != "3")
                    {
                        if (F.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                        }
                        else
                        {
                            P.Visible = true;
                        }
                        Days.Text = "0.5";
                    }
                    else
                    {
                        P.Visible = true;
                        Days.Text = "";
                    }
                }
                else
                    Days.Text = "";
            }
        }
    }

    protected void ckFirstHalf_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grvLeave.Rows)
        {
            Control First = row.FindControl("ckFirstHalf") as CheckBox;
            Control Second = row.FindControl("ckSecondHalf") as CheckBox;
            Label Days = row.FindControl("lblDays") as Label;
            CheckBox F = (CheckBox)First;
            CheckBox S = (CheckBox)Second;
            Control FirstDrop = row.FindControl("LeavetypeFirst") as DropDownList;
            Control SecondDrop = row.FindControl("LeavetypeSecond") as DropDownList;
            DropDownList FD = (DropDownList)FirstDrop;
            DropDownList SD = (DropDownList)SecondDrop;
            Control PermissionMins = row.FindControl("PermissionMins") as DropDownList;
            DropDownList P = (DropDownList)PermissionMins;
            if (F.Checked)
            {
                if (FD.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                    if (S.Checked)
                    {
                        if (SD.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                            Days.Text = "1";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "0.5";
                        }
                    }
                    else
                        Days.Text = "0.5";
                }
                else
                {
                    P.Visible = true;
                    if (S.Checked)
                    {
                        if (SD.SelectedValue.ToString() != "3")
                        {
                            if (FD.SelectedValue.ToString() != "3")
                            {
                                P.Visible = false;
                            }
                            else
                            {
                                P.Visible = true;
                            }
                            Days.Text = "0.5";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "";
                        }
                    }
                }
            }
            else
            {
                if (FD.SelectedValue.ToString() != "3")
                {
                    P.Visible = true;
                }
                else
                {
                    P.Visible = false;
                }
                if (S.Checked)
                {
                    if (SD.SelectedValue.ToString() != "3")
                    {
                        if (FD.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                        }
                        else
                        {
                            P.Visible = true;
                        }
                        Days.Text = "0.5";
                    }
                    else
                    {
                        P.Visible = true;
                        Days.Text = "";
                    }
                }
                else
                    Days.Text = "";
            }
        }
    }
    protected void ckSecondHalf_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grvLeave.Rows)
        {
            Control First = row.FindControl("ckFirstHalf") as CheckBox;
            Control Second = row.FindControl("ckSecondHalf") as CheckBox;
            Label Days = row.FindControl("lblDays") as Label;
            CheckBox F = (CheckBox)First;
            CheckBox S = (CheckBox)Second;
            Control FirstDrop = row.FindControl("LeavetypeFirst") as DropDownList;
            Control SecondDrop = row.FindControl("LeavetypeSecond") as DropDownList;
            DropDownList FD = (DropDownList)FirstDrop;
            DropDownList SD = (DropDownList)SecondDrop;
            Control PermissionMins = row.FindControl("PermissionMins") as DropDownList;
            DropDownList P = (DropDownList)PermissionMins;
            if (F.Checked)
            {
                if (FD.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                    if (S.Checked)
                    {
                        if (SD.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                            Days.Text = "1";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "0.5";
                        }
                    }
                    else
                        Days.Text = "0.5";
                }
                else
                {
                    P.Visible = true;
                    if (S.Checked)
                    {
                        if (SD.SelectedValue.ToString() != "3")
                        {
                            if (FD.SelectedValue.ToString() != "3")
                            {
                                P.Visible = false;
                            }
                            else
                            {
                                P.Visible = true;
                            }
                            Days.Text = "0.5";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "";
                        }
                    }
                    else
                    {
                        if (FD.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "";
                        }
                    }
                }
            }
            else
            {
                if (FD.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                }
                else
                {
                    P.Visible = true;
                }
                if (S.Checked)
                {
                    if (SD.SelectedValue.ToString() != "3")
                    {
                        if (FD.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                        }
                        else
                        {
                            P.Visible = true;
                        }
                        Days.Text = "0.5";
                    }
                    else
                    {
                        P.Visible = true;
                        Days.Text = "";
                    }
                }
                else
                    Days.Text = "";
            }
        }
    }
    protected void lnkFeedbackDetails_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        Datasource_IBSQL dsSQL = new Datasource_IBSQL();
        DataSet SqlDs = new DataSet();
        if (hfP_ID.Value != "")
        {
            if (Session["locationid"].ToString() == "2" || Session["locationid"].ToString()=="3")
            {
                rbLocation.SelectedValue = Session["locationid"].ToString();
            }
            else
            {
                rbLocation.SelectedValue = rbLocation.SelectedValue;
            }
            ds = dsSQL.spGetEmployee(Convert.ToInt32(hfP_ID.Value.Replace("DP", "").Replace("SDS", "").Replace("DDS", "")), Convert.ToInt32(rbLocation.SelectedValue));
            hfE_ID.Value = ds.Tables[0].Rows[0]["Employee_id"].ToString();

            ds = dsSQL.spGetFB(hfE_ID.Value);
            if (ds != null)
            {
                gvFeedback.DataSource = ds;
                gvFeedback.DataBind();
                txtPositive.Text = "";
                txtNegative.Text = "";
            }
            else
            {
                gvFeedback.DataSource = null;
                gvFeedback.DataBind();
                txtPositive.Text = "";
                txtNegative.Text = "";
            }

            if (rbLocation.SelectedValue == "2")
            {
                HRMS_CH hrCh = new HRMS_CH();
                SqlDs = hrCh.GetEmpDetail(hfP_ID.Value);
            }
            else
            {
                HRMS_CMB hrCh = new HRMS_CMB();
                SqlDs = hrCh.GetEmpDetail(hfP_ID.Value);
            }
            DataRow row = SqlDs.Tables[0].Rows[0];
            lblFEmpId.Text = row["Refno"].ToString();
            lblFEmpName.Text = row["empname"].ToString();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Select Employee Details!..');</script>");
        }
        this.showPanel(div_FeedBack_details);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Datasource_IBSQL dsSQL = new Datasource_IBSQL();
        if (hfE_ID.Value != "")
        {
            dsSQL.InsertFB(hfE_ID.Value, txtPositive.Text.Replace("'", "''"), txtNegative.Text.Replace("'", "''"));
            DataSet ds = new DataSet();
            ds = dsSQL.spGetFB(hfE_ID.Value);
            if (ds != null)
            {
                gvFeedback.DataSource = ds;
                gvFeedback.DataBind();
                txtPositive.Text = "";
                txtNegative.Text = "";
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Review details Not Saved!..');</script>");
        }
        this.showPanel(div_FeedBack_details);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        btnImg.Visible = true;
        if (txtEmpid.Text != "")
        {
            string path = string.Empty;
            string filename = FileUpload1.FileName;
            string ext = Path.GetExtension(FileUpload1.PostedFile.FileName);
            path = @"\\192.9.201.222\dp\MIS\TermCond" + "\\" + Regex.Replace(txtEmpid.Text.ToString(), "[^a-zA-Z0-9_]+", " ");
            System.IO.File.Delete(path + ".doc");
            System.IO.File.Delete(path + ".docx");
            System.IO.File.Delete(path + ".pdf");
            System.IO.File.Delete(path + ".xls");
            System.IO.File.Delete(path + ".xlsx");
            System.IO.File.Delete(path + ".jpg");
            System.IO.File.Delete(path + ".png");
            System.IO.File.Delete(path + ".txt");
            FileUpload1.PostedFile.SaveAs(path + ext.ToString());
            if (ext.ToString() == ".doc" || ext.ToString() == ".docx")
            {
                btnImg.ImageUrl = "~/images/QMS/Word13.png";
                btnImg.Width = 50;
                btnImg.Height = 50;
            }
            else if (ext.ToString() == ".pdf")
            {
                btnImg.ImageUrl = "~/images/QMS/pdf13.png";
                btnImg.Width = 50;
                btnImg.Height = 50;
            }
            else if (ext.ToString() == ".xls" || ext.ToString() == ".xlsx")
            {
                btnImg.ImageUrl = "~/images/QMS/excel.png";
                btnImg.Width = 50;
                btnImg.Height = 50;
            }
            else if (ext.ToString() == ".jpg" || ext.ToString() == ".png")
            {
                btnImg.ImageUrl = "~/images/QMS/img.png";
                btnImg.Width = 50;
                btnImg.Height = 50;
            }
        }
    }
    protected void btnImg_Click(object sender, ImageClickEventArgs e)
    {
        string path = string.Empty;
        string ext = Path.GetExtension(btnImg.ImageUrl);
        path = @"\\192.9.201.222\dp\MIS\TermCond" + "\\" + Regex.Replace(txtEmpid.Text.ToString(), "[^a-zA-Z0-9_]+", " ");

        if (File.Exists(path + ".jpg"))
            openfile(path + ".jpg");
        else if (File.Exists(path + ".png"))
            openfile(path + ".png");
        else if (File.Exists(path + ".xls"))
            openfile(path + ".xls");
        else if (File.Exists(path + ".xlsx"))
            openfile(path + ".xlsx");
        else if (File.Exists(path + ".pdf"))
            openfile(path + ".pdf");
        else if (File.Exists(path + ".doc"))
            openfile(path + ".doc");
        else if (File.Exists(path + ".docx"))
            openfile(path + ".docx");
    }
    public static HttpResponse GetHttpResponse()
    {
        return HttpContext.Current.Response;
    }
    private void openfile(string filename)
    {
        try
        {
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path.GetFileName(filename.Replace(' ', '_')) + "\"");
            response.ContentType = "application/octet-stream";
            response.WriteFile(filename);
            response.Flush();
            response.Close();
            response.End();
        }
        catch (Exception Ex)
        {

        }
    }
    protected void gvFeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFeedback.PageIndex = e.NewPageIndex;
        gvFeedback.DataBind();
    }
    protected void lnkDocuments_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            DataSet ds = new DataSet();
            Datasource_IBSQL dsSQL = new Datasource_IBSQL();
            ds = dsSQL.spGetEmployee(Convert.ToInt32(hfP_ID.Value.Replace("DP", "").Replace("SDS", "").Replace("DDS", "")), Convert.ToInt32(rbLocation.SelectedValue));
            hfE_ID.Value = ds.Tables[0].Rows[0]["Employee_id"].ToString();
            ds = dsSQL.spGetDoc(hfE_ID.Value);
            if(ds!=null)
            {
                grdDoc.DataSource = ds;
                grdDoc.DataBind();
            }
            else
            {
                grdDoc.DataSource = null;
                grdDoc.DataBind();
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Select any Employee in Summary tab');</script>");
        }
        this.showPanel(div_documents);
    }
    public void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if(hfP_ID.Value!="")
        {
            DataSet ds=new DataSet();
            Datasource_IBSQL dsSQL = new Datasource_IBSQL();
            ds = dsSQL.spGetEmployee(Convert.ToInt32(hfP_ID.Value.Replace("DP", "").Replace("SDS", "").Replace("DDS", "")), Convert.ToInt32(rbLocation.SelectedValue));
            hfE_ID.Value = ds.Tables[0].Rows[0]["Employee_id"].ToString();
        
            string url = "Angularjsuploadimages.aspx?empid="+hfE_ID.Value;
            string script = "window.open('" + url + "', 'popup_window', 'width=500,height=200,left=100,top=200,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "popUp", script, true);
        }
    }
    protected void grdDoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDoc.PageIndex = e.NewPageIndex;
        grdDoc.DataBind();
    }
    public void grdDoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Document")
        {
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            string path = ((Label)row.FindControl("lblpath")).Text.ToString();
            openfile(path);
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        lnkDocuments_Click(sender, e);
    }
}

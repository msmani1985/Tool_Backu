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

using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Drawing;

public partial class Emp_Leave_OpBal : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    int empid;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ibtngridview_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = SqlObj.GetAllEmployee(Convert.ToInt16(Session["employeeid"].ToString()), Convert.ToInt16(Session["locationid"].ToString()));
        grvLvl.DataSource = ds;
        grvLvl.DataBind();
    }
    protected void cmd_New_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = SqlObj.GetAllEmployee(Convert.ToInt16(Session["employeeid"].ToString()), Convert.ToInt16(Session["locationid"].ToString()));
        grvLvl.DataSource = ds;
        grvLvl.DataBind();
    }
    protected void cmd_Save_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList al = new ArrayList();
        Hashtable val = null;
        Launch mobj = new Launch();
        try
        {
            foreach (GridViewRow grw in grvLvl.Rows)
            {
                
                Label Employee_ID = (Label)grw.FindControl("ID");
                TextBox PL = (TextBox)grw.FindControl("PL");
                TextBox SL = (TextBox)grw.FindControl("SL");
                TextBox CL = (TextBox)grw.FindControl("CL");
                CheckBox Ex = (CheckBox)grw.FindControl("Check");
                string chkEx;
                if (Ex.Checked)
                    chkEx = "1";
                else
                    chkEx = "0";

                string inproc = "SpUpdateLeaveOpBal";
                string[,] pname ={
                    {"@employee_id",Employee_ID.Text },{"@pl",PL.Text },{"@sl",SL.Text},
                    {"@cl",CL.Text},{"@Ex",chkEx.ToString()} };
                int val1 = this.SqlObj.ExcSP(inproc, pname, CommandType.StoredProcedure);
                //val = new Hashtable();
                //val.Add("Employee_ID", ((Label)grw.FindControl("ID")).Text.Trim().ToString());
                //val.Add("PL", ((TextBox)grw.FindControl("PL")).Text.Trim().ToString());
                //val.Add("SL", ((TextBox)grw.FindControl("SL")).Text.Trim().ToString());
                //val.Add("CL", ((TextBox)grw.FindControl("CL")).Text.Trim().ToString());
                //al.Add(val);
            }
            ibtngridview_Click(sender, e);
            ////if (!SqlObj.Update_LeaveDetails(al))
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
            ////else
             Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mobj = null; }
    }
    protected void grvLvl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox PL = (TextBox)e.Row.FindControl("PL");
            TextBox CL = (TextBox)e.Row.FindControl("CL");
            TextBox SL = (TextBox)e.Row.FindControl("SL");
            TextBox ClsPL = (TextBox)e.Row.FindControl("ClsPL");
            TextBox ClsCL = (TextBox)e.Row.FindControl("ClsCL");
            TextBox ClsSL = (TextBox)e.Row.FindControl("ClsSL");

            TextBox TakPL = (TextBox)e.Row.FindControl("TakPL");
            TextBox TakCL = (TextBox)e.Row.FindControl("TakCL");
            TextBox TakSL = (TextBox)e.Row.FindControl("TakSL");
            Label empid = (Label)e.Row.FindControl("ID");
            CheckBox Ex = (CheckBox)e.Row.FindControl("Check");
            DataSet ds = new DataSet();
            ds = SqlObj.GetEmpLeaveOpBal(Convert.ToInt16(empid.Text));
            int i = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                PL.Text = ds.Tables[0].Rows[i]["PL"].ToString();
                CL.Text = ds.Tables[0].Rows[i]["CL"].ToString();
                SL.Text = ds.Tables[0].Rows[i]["SL"].ToString();
                ClsPL.Text = ds.Tables[0].Rows[i]["PL_AVAILABLE_LEAVE"].ToString();
                ClsCL.Text = ds.Tables[0].Rows[i]["CL_AVAILABLE_LEAVE"].ToString();
                ClsSL.Text = ds.Tables[0].Rows[i]["SL_AVAILABLE_LEAVE"].ToString();
                TakPL.Text = ds.Tables[0].Rows[i]["PL_LEAVE_DAYS"].ToString();
                TakCL.Text = ds.Tables[0].Rows[i]["CL_LEAVE_DAYS"].ToString();
                TakSL.Text = ds.Tables[0].Rows[i]["SL_LEAVE_DAYS"].ToString();
                PL.BackColor = System.Drawing.Color.AntiqueWhite;
                CL.BackColor = System.Drawing.Color.AntiqueWhite;
                SL.BackColor = System.Drawing.Color.AntiqueWhite;
                ClsPL.BackColor = System.Drawing.Color.LightBlue;
                ClsCL.BackColor = System.Drawing.Color.LightBlue;
                ClsSL.BackColor = System.Drawing.Color.LightBlue;
                TakPL.BackColor = System.Drawing.Color.LemonChiffon;
                TakCL.BackColor = System.Drawing.Color.LemonChiffon;
                TakSL.BackColor = System.Drawing.Color.LemonChiffon;
                if (ds.Tables[0].Rows[i]["Months"].ToString() == "1")
                {
                    Ex.Enabled = true;
                    Ex.BorderColor = System.Drawing.Color.Red;
                    Ex.BorderWidth =System.Web.UI.WebControls.Unit.Pixel(2);
                }
                else
                    Ex.Enabled = false;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string connectionString = "";
        int count = 0;
        SqlConnection scon;
        scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString);
        if (fileBrowse.HasFile)
        {
            string fileName = Path.GetFileName(fileBrowse.PostedFile.FileName);
            string fileExtension = Path.GetExtension(fileBrowse.PostedFile.FileName);
            string fileLocation = Server.MapPath(fileName);
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
            con.Open();
            DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string getExcelSheetName1 = string.Empty;
            getExcelSheetName1 = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
            cmds.CommandText = "SELECT * FROM [" + getExcelSheetName1 + "]";
            dAdapter.SelectCommand = cmds;
            dAdapter.Fill(dtExcelRecords);
            for (int i = 2; i < dtExcelRecords.Rows.Count; i++)
            {
                SqlCommand gcmd = new SqlCommand("Select employee_id from employee where location_id='"+Session["Locationid"].ToString()+"' and  employee_number=(case when '" + dtExcelRecords.Rows[i][0] + "' like '%sds%' then replace('" + dtExcelRecords.Rows[i][0] + "','sds','') else case when '" + dtExcelRecords.Rows[i][0] + "' like '%DDS%' then replace('" + dtExcelRecords.Rows[i][0] + "','DDS','') else replace('" + dtExcelRecords.Rows[i][0] + "','DP','') end end)", scon);
                SqlDataAdapter da = new SqlDataAdapter(gcmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    try
                    {
                        
                        scon.Open();
                        SqlCommand cmd = new SqlCommand("SpUploadLeaveOpBal", scon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        string id = dtExcelRecords.Rows[i][0].ToString();
                        if(id.Substring(0,3)=="SDS")
                            empid = Convert.ToInt16(id.Substring(3, 3));
                        else if (id.Substring(0, 3) == "DDS")
                          empid = Convert.ToInt16(id.Substring(3, 3));
                        else if (id.Substring(0, 2) == "DP")
                          empid = Convert.ToInt16(id.Substring(2, 4));
                        cmd.Parameters.Add("@employee_id", SqlDbType.Int).Value = empid ;
                        cmd.Parameters.Add("@Location", SqlDbType.Int).Value = Session["Locationid"].ToString();
                        cmd.Parameters.Add("@pl", SqlDbType.Decimal).Value = dtExcelRecords.Rows[i][4];
                        cmd.Parameters.Add("@sl", SqlDbType.Decimal).Value = dtExcelRecords.Rows[i][3];
                        cmd.Parameters.Add("@cl", SqlDbType.Decimal).Value = dtExcelRecords.Rows[i][2];
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
}

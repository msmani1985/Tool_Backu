using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Att_EditInOut : System.Web.UI.Page
{
    Attendance As = new Attendance();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //BindData();
        }
    }
    protected void BindData()
    {
        DataSet ds = new DataSet();
        ds = As.GetAttPunch("spGetEditInOutPunchs",
            new string[,] { { "@Pdate", txtPdate.Text }, { "@UserID", EmpNoTxt.Text } }, CommandType.StoredProcedure);
        if (ds != null)
        {
            gvEmployeeDetails.DataSource = ds.Tables[0];
            gvEmployeeDetails.DataBind();
        }
        else
        {
            gvEmployeeDetails.DataSource = null;
            gvEmployeeDetails.DataBind();
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPdate.Text != "")
            {
                BindData();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Enter the Date');</script>");
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        EmpNoTxt.Text = "";
        txtPdate.Text = "";
        gvEmployeeDetails.DataSource = null;
        gvEmployeeDetails.DataBind();
    }
    protected void gvEmployeeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList dropInOut = (DropDownList)e.Row.FindControl("dropInOut");
            Label lblUserID = (Label)e.Row.FindControl("lblUserID");
            Label lblPdate = (Label)e.Row.FindControl("lblPdate");
            Label lblPunch = (Label)e.Row.FindControl("lblPunch");
            DataSet ds = new DataSet();
            ds = As.EmpInOut(lblUserID.Text,lblPdate.Text,lblPunch.Text);
            if (ds != null)
            {
                dropInOut.Items[dropInOut.Items.IndexOf(dropInOut.Items.FindByValue(ds.Tables[0].Rows[0]["IOType"].ToString()))].Selected = true;
            }
        }
    }
    protected void cmd_Save_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList al = new ArrayList();
        Hashtable val = null;
        try
        {
            if (ReProEmpID.Text != "" && txtPdate.Text != "")
            {
                using (SqlConnection con = new SqlConnection("server= 192.9.201.15\\sqlexpress;Initial Catalog=Cosec;User ID=sa;Password=matrix_1"))
                {
                    using (SqlCommand cmd = new SqlCommand("spGetAttTimeBreaks6", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Userid", SqlDbType.VarChar).Value = ReProEmpID.Text;
                        cmd.Parameters.Add("@sdate", SqlDbType.VarChar).Value = txtPdate.Text;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                foreach (GridViewRow grw in gvEmployeeDetails.Rows)
                {
                    val = new Hashtable();

                    val.Add("Userid", ((Label)grw.FindControl("lblUserID")).Text.Trim().ToString());
                    val.Add("Pdate", ((Label)grw.FindControl("lblPdate")).Text.Trim().ToString());
                    val.Add("Edatetime", ((Label)grw.FindControl("lblPunch")).Text.ToString());
                    val.Add("IOtype", ((DropDownList)grw.FindControl("dropInOut")).SelectedValue.ToString());
                    al.Add(val);

                    using (SqlConnection con = new SqlConnection("server= 192.9.201.15\\sqlexpress;Initial Catalog=Cosec;User ID=sa;Password=matrix_1"))
                    {
                        using (SqlCommand cmd = new SqlCommand("spGetAttTimeBreaks6", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@Userid", SqlDbType.VarChar).Value = ((Label)grw.FindControl("lblUserID")).Text.Trim().ToString();
                            cmd.Parameters.Add("@sdate", SqlDbType.VarChar).Value = ((Label)grw.FindControl("lblPdate")).Text.Trim().ToString();

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            if (!As.Update_InOut(al))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Update Failed');</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Updated');</script>");

            BindData();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { As = null; }
    }
}
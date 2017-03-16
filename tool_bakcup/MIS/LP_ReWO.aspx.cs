using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LP_ReWO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con = new SqlConnection("server= 192.9.201.222;database=dp_MIS_Live;uid=sa;pwd=masterkey"))
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateReWOnumber", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@jobid", SqlDbType.VarChar).Value = txtJobID.Text;
                    cmd.Parameters.Add("@POnumber", SqlDbType.VarChar).Value = txtPONumber.Text;
                    cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = Session["Employeeid"].ToString();//Session["employeeid"].ToString();

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            Alert("Successfully Saved.");
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtJobID.Text = "";
        txtPONumber.Text = "";
    }
}
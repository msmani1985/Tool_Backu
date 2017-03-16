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

public partial class changepassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void submit_Click(object sender, EventArgs e)
    {
        if (Session["password"] != null)
        {
            if (Session["password"].ToString().Trim().ToLower() != oldpassword.Text.Trim().ToLower())
            {
                divmessage.InnerHtml = "Incorrect Password, please try again";
                return;
            }
            if (confirmpass.Text.Trim() != newpass.Text.Trim() || (newpass.Text.Trim() == ""))
            {
                divmessage.InnerHtml = "New Password and Confirm Password are not same, please try again";
                return;
            }
            biz_emp_menu_mgmt biz = new biz_emp_menu_mgmt();
            bool bChangePass = false;
            bChangePass = biz.ChangePass(Convert.ToInt16(Session["employeeid"].ToString()), Session["username"].ToString(),confirmpass.Text.Trim());
            if (bChangePass)
                divmessage.InnerHtml = "Password has been changed successfully." ;
            else
                divmessage.InnerHtml = "Unable to process request, please try again";
            biz = null;
        }
    }
}

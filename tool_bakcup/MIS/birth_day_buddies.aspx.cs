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
/*
/// <summary>
/// Birth Day Buddies
/// Created by: Royson
/// Creation Date: 02 Jan 2010
/// </summary>
 * */
public partial class birth_day_buddies : System.Web.UI.Page
{
    protected int id = 1;
    Common oCom = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.getBuddies();
    }

    private void getBuddies(){
        //DataSet ds = new DataSet();
        gvBuddies.DataSource = oCom.getBirthDayBuddies();
        gvBuddies.DataBind();
    }
}

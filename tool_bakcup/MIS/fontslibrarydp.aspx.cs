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

public partial class fontslibrarydp : System.Web.UI.Page
{
    DataSet fontds=new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_showfonts_Click(object sender, EventArgs e)
    {
        
        try
        {
            gv_fontslibrary.DataSource = readfontxml();
            gv_fontslibrary.DataBind();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { //fontds = null; 
        }
    }
    protected void btn_searchfont_Click(object sender, EventArgs e)
    {
        DataSet fontds = new DataSet();
        DataView fdv = new DataView();
        fontds = readfontxml();
        fdv = fontds.Tables[0].DefaultView;
        string filterstr = string.Empty;
        if (txt_searchtext.Text.Trim().ToString() != "")
            filterstr = "name like '%" + txt_searchtext.Text.Trim().ToString() + "%'";
        if (txt_fonttype.Text.Trim().ToString() != "")
            filterstr += (!string.IsNullOrEmpty(filterstr)) ? " or type like '%" + txt_fonttype.Text.Trim().ToString() + "%'" : " type like '%" + txt_fonttype.Text.Trim().ToString() + "%'";
        fdv.RowFilter = filterstr;
        gv_fontslibrary.DataSource = fdv;
        gv_fontslibrary.DataBind();

    }
    private DataSet readfontxml()
    {
        DataSet fds = new DataSet();
        try
        {
            fontds.ReadXml(@"\\dpserver5\JOBS1\Jobs\SoftwareTools\FontLibrary\FontDB.xml");
            return fontds;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { fds = null; }
    }
}

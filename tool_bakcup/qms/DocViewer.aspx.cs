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
using System.IO;
using System.Net;

public partial class DocViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strPath = Session["FileData"].ToString();
            //Label lt = new Label();
            //lt.Text = sb.ToString();
            //myPanel.Controls.Add(lt);
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(strPath);

            if (buffer != null)
            {
                Response.ContentType = "application/msword";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }
        }  
    }
    public string ReadFile(string path)
    {
        FileStream fstream = new FileStream(path, FileMode.Open, FileAccess.Read);
        StreamReader sreader = new StreamReader(fstream, System.Text.Encoding.UTF8);
        string sr = sreader.ReadToEnd();
        return sr;
    } 
}

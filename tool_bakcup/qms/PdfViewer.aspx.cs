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
using System.Net;
using Tools;
using System.IO;

public partial class PdfViewer : System.Web.UI.Page
{
    string qstring = string.Empty;
    string[] Mainpath;
    private string sJourcodeMarkup, sJourMarkupfolder, sJourMarkupPDF, sJourcodeAQ, sJourAQfolder, sJourAQPDF = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        // string strpath = Session["link"].ToString();
        ////string path = Server.MapPath("CAGE markup sample.pdf");

        ////WebClient client = new WebClient();
        ////Byte[] buffer = client.DownloadData(path);

        ////if (buffer != null)
        ////{
        ////    Response.ContentType = "application/pdf";
        ////    Response.AddHeader("content-length", buffer.Length.ToString());
        ////    Response.BinaryWrite(buffer);
        ////}

        qstring = Session["fName"].ToString();
        Mainpath = qstring.Split('/');
        sJourcodeAQ = Mainpath[4].ToString();
        sJourcodeMarkup = Mainpath[5].ToString();
        sJourMarkupfolder = Mainpath[6].ToString();
        sJourMarkupPDF = Mainpath[7].ToString();



        // qstring = "//" + qstring.ToString();

        if (sJourcodeAQ == "TFJATS")
        {
            //using (new Impersonator("dpitesting", "192.9.200.196", "dpitesting"))
            {
                string strPath = @"//192.9.200.196/QET/TFJATS/" + sJourcodeMarkup + '/' + sJourMarkupfolder + '/' + sJourMarkupPDF;


                WebClient client = new WebClient();
                client.Credentials = new System.Net.NetworkCredential("dpitesting", "dpitesting", "192.9.200.196");
                Byte[] buffer = client.DownloadData(strPath);

                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }
                client.Dispose();
                ////////////////Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ////////////////Response.Buffer = true;
                ////////////////Response.Charset = "";
                ////////////////Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(strPath));
                ////////////////this.EnableViewState = false;
                ////////////////Response.WriteFile(strPath);
                ////////////////Response.Flush();
                ////////////////Response.Close();
            }
             
        }
        else if (sJourcodeAQ.ToUpper() == "TFJATS VALIDATION")
        {
            sJourAQPDF = Mainpath[8].ToString();
            string sPath = @"//192.9.200.196/QET/TFJATS VALIDATION/" + sJourcodeMarkup + '/' + sJourMarkupfolder + '/' + sJourAQPDF;
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(sPath);

            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }
        }
    }
}

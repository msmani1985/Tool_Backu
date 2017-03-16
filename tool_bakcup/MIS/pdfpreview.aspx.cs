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

public partial class pdfpreview : System.Web.UI.Page
{

    //string sTestFile = "leave letter.pdf";
    string sTestFile = "";
    string sVirtualPath = "";
    string slocationid;
    protected void Page_Load(object sender, EventArgs e)
    {

      /*  slocationid = "i";

        if (Session["location"] != null)
            slocationid = Session["location"].ToString();
        */
        slocationid = Request.QueryString["location"];
        //Server.MapPath("InvoiceTemplates\\" );
        sTestFile = Request.QueryString["pdfname"].ToString();
        if(slocationid=="i")
            sTestFile = ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + sTestFile;
        else
            sTestFile = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + sTestFile;

        string sHTMLTemplate = sVirtualPath + sTestFile;
        Session["invoicepdfprint"] = sTestFile;

       // if (Session["invoicepdfprint"] != null)
        if(File.Exists(sTestFile))
        {
            sHTMLTemplate = Session["invoicepdfprint"].ToString(); 
            try
            {
                Response.Buffer = true;
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(sTestFile));
                FileStream fs;
                BinaryReader br;
                fs = new FileStream(sHTMLTemplate, FileMode.Open);
                br = new BinaryReader(fs);
                Byte[] dataBytes = br.ReadBytes((int)(fs.Length - 1));
                Response.BinaryWrite(dataBytes);
                br.Close();
                fs.Close();
                Response.Flush();
                Response.Close();
            }
            catch (Exception oex)
            {

                
                Response.Clear();
                Response.Close();
                Response.Write(oex.Message);
                
                //divInvoiceHTML.InnerHtml = oex.Message;
                //divInvoiceHTML.InnerHtml = "<P style='color:red'>PDF not found, contact IT Team... [software@datapage.org]</P>";
            }
        }
        else
        {
          // divInvoiceHTML.InnerHtml = "<P style='color:red'>Not a valid location ID, unable to view PDF, contact IT Team... [software@datapage.org]</P>";
            divInvoiceHTML.InnerHtml = "<P style='color:red'>PDF not found, contact IT Team... [software@datapage.org]</P>";
        }


    }


}

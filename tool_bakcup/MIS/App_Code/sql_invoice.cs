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
using System.Data.SqlClient;
using System.Xml;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.Diagnostics;
using Excel;
using System.IO;


/// <summary>
/// Summary description for sql_invoice
/// Created by Malathy
/// </summary>
public class sql_invoice
{
  
    string sSql = "";
    string sqlFile = "";
    string err = "";
    private datasourceSQL oSql;
    private Common oCom;
    ReportDocument subrpt = new ReportDocument();
    
    string Filename = "";
    public string sFilePath = "";
    datasourceSQL invobj1 = new datasourceSQL();
    DataSet invds1 = new DataSet();


    public sql_invoice()
    {
        oSql = new datasourceSQL();
        oCom = new Common();
    }
    public string ReportGeneration(string cust_id, string job_id, string job_typeid, string loc_id)
    {
        // XmlDocument oDoc = new XmlDocument();
        
        string ssempid = Convert.ToString(HttpContext.Current.Session["employeeid"]);
        ReportDocument rpt = new ReportDocument();
        XmlDocument oDoc = new XmlDocument();
        if (loc_id == "2")
            sqlFile = ConfigurationManager.ConnectionStrings["PDFFilePathIndiaInvoiceSql"].ToString();
        else
            sqlFile = ConfigurationManager.ConnectionStrings["PDFFilePathDublinInvoiceSql"].ToString();

        string rptname = "";
        oDoc.Load(System.Web.HttpContext.Current.Server.MapPath("") + @"\invoice\BooksInvoiceTemplate.xml");
        if (oDoc != null)
            rptname = oDoc.DocumentElement.SelectSingleNode("invoice[@type='" + job_typeid + "']").SelectSingleNode("customer[@custno=" + cust_id + "]").Attributes.GetNamedItem("filename").Value;
        else
            rptname = oDoc.DocumentElement.SelectSingleNode("invoice[@type='" + job_typeid + "']").SelectSingleNode("customer[@custno='default']").Attributes.GetNamedItem("filename").Value;
        oDoc = null;
        // datasourceSQL invobj1 = new datasourceSQL();
        //  DataSet invds1 = new DataSet();

        invds1 = invobj1.ExcProcedure("spget_Invoice", new string[,] { { "@customer_id", cust_id }, { "@job_id", job_id }, { "@job_type_id", job_typeid }, { "@location_id", loc_id } }, CommandType.StoredProcedure);
        string filename = string.Empty;
        filename = sqlFile;
        if (invds1 != null)
        {
            if (invds1.Tables.Count > 0)
            {
                rpt.FileName = HttpContext.Current.Server.MapPath(rptname);
                rpt.SetDatabaseLogon("sa", "masterkey");
                //Set the DataSource for subreports
               // rpt.SetDataSource(invds1.Tables[0].ToString());
               // rpt.Subreports[0].SetDataSource(invds1.Tables[1].ToString());
                //subrpt = rpt.Subreports[0];
                //subrpt.SetDataSource(invds1.Tables[1].ToString());
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, sqlFile+invds1.Tables[0].Rows[0]["name"].ToString().Trim().Replace("/","_")+ ".pdf");// + "\\" + job_id
                string iInvValue = InvoiceValueFromCR(rpt, sqlFile + invds1.Tables[0].Rows[0]["name"].ToString().Trim().Replace("/", "_") + ".htm", "1280", job_id, job_typeid, loc_id, "");
                if (iInvValue != "")
                {
                    datasourceSQL pobj = new datasourceSQL();
                    pobj.ExcSProcedure("spupdate_invoicepreview", new string[,] { { "@employee_id", ssempid }, { "@job_id", job_id }, { "@job_type_id", job_typeid }, { "@location_id", loc_id }, { "@invoice_value", iInvValue } }, CommandType.StoredProcedure);
                    pobj = null;
                }

                return sqlFile + invds1.Tables[0].Rows[0]["name"].ToString().Trim().Replace("/", "_") + ".pdf";//+ "\\" + job_id
            }

            else
                return "";
        }
        else
            return "";

        rpt.Dispose();
        rpt = null;
    }

    public ReportDocument ReportGenerationIndia(string cust_id, string job_id, string job_typeid, string loc_id)
    {
        // XmlDocument oDoc = new XmlDocument();
        ReportDocument rpt = new ReportDocument();
        XmlDocument oDoc = new XmlDocument();
   
 
       
        // string sqlFile = ConfigurationManager.ConnectionStrings["finalsqlpdfpath"].ToString();

        string rptname = "";
        oDoc.Load(System.Web.HttpContext.Current.Server.MapPath("") + @"\invoice\BooksInvoiceTemplate.xml");
        if (oDoc != null)
            rptname = oDoc.DocumentElement.SelectSingleNode("invoice[@type='" + job_typeid + "']").SelectSingleNode("customer[@custno=" + cust_id + "]").Attributes.GetNamedItem("filename").Value;
        else
            rptname = oDoc.DocumentElement.SelectSingleNode("invoice[@type='" + job_typeid + "']").SelectSingleNode("customer[@custno='default']").Attributes.GetNamedItem("filename").Value;
        oDoc = null;

        datasourceSQL invobj1 = new datasourceSQL();
        DataSet invds1 = new DataSet();
        DataSet invds2=new DataSet();
        invds1 = invobj1.ExcProcedure("spget_Invoice", new string[,] { { "@customer_id", cust_id }, { "@job_id", job_id }, { "@job_type_id", job_typeid }, { "@location_id", loc_id } }, CommandType.StoredProcedure);
        
        string filename = string.Empty;
        filename = System.Web.HttpContext.Current.Server.MapPath("");
        if (invds1 != null)
        {
            if (invds1.Tables.Count > 0)
            {
                rpt.FileName = HttpContext.Current.Server.MapPath(rptname);
                rpt.SetDatabaseLogon("sa", "masterkey");
                rpt.SetDataSource(invds1.Tables[0]);
                if (rpt.Subreports.Count > 0)
                {

                    invds2 = invobj1.ExcProcedure("spget_Invoice", new string[,] { { "@customer_id", cust_id }, { "@job_id", job_id }, { "@job_type_id", job_typeid }, { "@location_id", loc_id } }, CommandType.StoredProcedure);
                    subrpt.SetDataSource(invds2.Tables[1]);
                }

                return rpt;
          
            }
            else
                return null;
        }
        else
            return null;
    }

    //Invoice Total value from CR and update in spupdate_invoicepreview

    public string InvoiceValueFromCR(ReportDocument rpt, string ssfilename, string empid, string jobid, string jobtypeid, string locid, string total_value)
    {
        //if (loc_id == "2")

        //    sqlFile = ConfigurationManager.ConnectionStrings["HTMLFilePathIndiaInvoiceSql"].ToString();
        //else
        //    sqlFile = ConfigurationManager.ConnectionStrings["HTMLFilePathDublinInvoiceSql"].ToString();
        double dValue = 0;


        rpt.ExportToDisk(ExportFormatType.HTML32, ssfilename);
        FileStream filestream = new FileStream(ssfilename, System.IO.FileMode.Open);
        StreamReader oReader = new StreamReader(filestream);

        string sContent = oReader.ReadToEnd();

        oReader.Close();

        filestream.Close();

        filestream = null;

        oReader.Dispose();

        oReader = null;

        sContent = sContent.Substring(sContent.IndexOf("inv_value_datapage\">") + 20);

        sContent = sContent.Substring(0, sContent.IndexOf("<"));

        sContent = sContent.Replace("&nbsp;", "").Trim();
        sContent = sContent.Replace(">", "").Trim();
        sContent = sContent.Replace("<", "").Trim();
        while (!double.TryParse(sContent, out dValue))  //some time sContent come with currency Sysmbol, so  
        {
            //if(sContent!="")
            sContent = sContent.Substring(1);
        }
        rpt.Dispose();
        return sContent;
    }

}

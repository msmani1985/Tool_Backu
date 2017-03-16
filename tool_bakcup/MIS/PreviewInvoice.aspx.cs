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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using Tools;

public partial class PreviewInvoice : System.Web.UI.Page
{
	string filename = "";
	Report test = new Report();
	string preview_id = "";
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Request.QueryString["location"] != null)
		{
			try
			{
				if (Request.QueryString["location"] != null && Request.QueryString["custno"] != null)
				{
					string loca = Request.QueryString["location"];

					int ino = Convert.ToInt32(Request.QueryString["ino"]);
					int Category = Convert.ToInt32(Request.QueryString["category"]);
					int custno = Convert.ToInt32(Request.QueryString["custno"]);
					string invoiceno = Request.QueryString["invno"];

					if (Convert.ToInt32(Request.QueryString["category"]) == 4)/////////// 4 For WIP
					{
                        try
                        {
                            preview_id = Request.QueryString["invno"].ToString();
                            wip_invoice wobj = new wip_invoice();
                            ReportDocument rpt = wobj.wip_createreport(Request.QueryString["conno"], loca, invoiceno, custno.ToString(), Request.QueryString["JOURCODE"]);
                            //For preveiw date update, if any error throw while preview date update we cant see bcz the preview screen closed after save or open click,
                            if (Session["employeeteamid"] != null && Session["employeeteamid"].ToString() != "1" && preview_id != "")//Check if Managementteam(suresh,chitra,nisha,srikanth) except sw team
                            {
                                Datasource_IBSQL preview_obj = new Datasource_IBSQL();

                                try
                                {
                                    preview_obj.UpdatePreviewDate(loca, Category.ToString(), preview_id.ToString(), "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'");
                                }
                                catch (Exception ex)
                                { throw ex; }
                                finally
                                { preview_obj = null; preview_id = ""; }
                            }
                            string WipFilename;
                            if (loca == "i")
                                WipFilename = ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + "\\wip_" + invoiceno + ".pdf";
                            else
                                WipFilename = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + "\\wip_" + invoiceno + ".pdf";
                            if (invoiceno != "")
                            {
                                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "wip_" + invoiceno);
                            }
                            else
                            {
                                string filename = (invoiceno != "") ? "WIP_" + invoiceno : (loca == "i") ? "IND_WIP_" + Request.QueryString["pename"] : "DUB_WIP_" + Request.QueryString["pename"];
                                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename + ".pdf");
                            }
                            if (rpt != null)
                            {
                                rpt.Close();
                                rpt.Dispose();
                            }
                        }
                        catch (Exception ex)
                        { throw ex; }
                        finally
                        {  }
                    }
                    else if (Convert.ToInt32(Request.QueryString["category"]) == 2)///////  2  For Books
                    {
                        int BNo = Convert.ToInt32(Request.QueryString["bno"]);
                        test.InvoiceReport(BNo, loca, Server.MapPath("").ToString(), Category);
                        filename = test.GetReport(Request.QueryString["custno"], "PDF",Request.QueryString["INVRpt"]);
                        preview_id = Request.QueryString["bno"];
                    }
                    else if (Convert.ToInt32(Request.QueryString["category"]) == 3)//////////// For Projects
                    {
                        int PNo = Convert.ToInt32(Request.QueryString["projectno"]);
                        test.InvoiceReport(PNo, loca, Server.MapPath("").ToString(), Category);
                        filename = test.GetReport(Request.QueryString["custno"], "PDF", Request.QueryString["INVRpt"]);
                        preview_id = Request.QueryString["projectno"];
                    }
                    else////////////////// 1 for Journal
                    {
                        test.InvoiceReport(ino, loca, Server.MapPath("").ToString(), Category);
                        filename = test.GetReport(Request.QueryString["custno"], "PDF", Request.QueryString["INVRpt"]);
                        if (custno == 2556 && loca == "d")////////For XLS (Xls report allowed only Tayler and Francis  customer) 
                        {
                            test.InvoiceReport(ino, loca, Server.MapPath("").ToString(), Category);
                            string xlsfname = test.GetReport(Request.QueryString["custno"], "XLS", Request.QueryString["INVRpt"]);
                        }
                        else if ((custno == 10219 || custno == 10201111) && loca == "d")
                        {
                            test.InvoiceReport(ino, loca, Server.MapPath("").ToString(), Category);
                            string xlsfname = test.GetReport(Request.QueryString["custno"], "XLS", Request.QueryString["INVRpt"]);
                        }
                        preview_id = ino.ToString();
                    }
                    if (Category != 4)
                    {
                        if (Session["employeeteamid"] != null && Session["employeeteamid"].ToString() != "1")//Check if Managementteam(suresh,chitra,nisha,srikanth) except sw team
                        {
                            Datasource_IBSQL preview_obj = new Datasource_IBSQL();
                            try
                            {
                                preview_obj.UpdatePreviewDate(loca, Category.ToString(), preview_id.ToString(), "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'");
                            }
                            catch (Exception ex)
                            { throw ex; }
                            finally
                            { preview_obj = null; preview_id = ""; }
                        }
                        try
                        {
                            if (Request.QueryString["type"] != "xml")
                            {
                                Session["invoicepdfprint"] = filename;
                                Response.Buffer = true;
                                Response.Clear();
                                if (loca == "d" && (Request.QueryString["invoiceno"] == null || Request.QueryString["invoiceno"].ToString() == ""))
                                    Response.AddHeader("Content-Disposition", "attachment; filename=DUB_" + Path.GetFileName(filename));
                                else if (loca == "i" && (Request.QueryString["invoiceno"] == null || Request.QueryString["invoiceno"].ToString() == ""))
                                    Response.AddHeader("Content-Disposition", "attachment; filename=IND_" + Path.GetFileName(filename));
                                else
                                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filename));
                                //alert("1");
                                Response.ContentType = "application/pdf";
                                FileStream fs;
                                BinaryReader br;
                                fs = new FileStream(filename, FileMode.Open);
                                br = new BinaryReader(fs);
                                Byte[] dataBytes = br.ReadBytes((int)(fs.Length - 1));
                                Response.BinaryWrite(dataBytes);
                                br.Close();
                                fs.Close();
                                Response.End();
                            }
                            else
                            {
                                string xmlFile = @"\\192.9.201.222\XMLINVOICES\" + Path.GetFileNameWithoutExtension(filename) + ".xml";
                                Session["invoicepdfprint"] = xmlFile;
                                Response.Buffer = true;
                                Response.Clear();
                                if (loca == "d" && (Request.QueryString["invoiceno"] == null || Request.QueryString["invoiceno"].ToString() == ""))
                                    Response.AddHeader("Content-Disposition", "attachment; filename=DUB_" + Path.GetFileName(xmlFile));
                                else if (loca == "i" && (Request.QueryString["invoiceno"] == null || Request.QueryString["invoiceno"].ToString() == ""))
                                    Response.AddHeader("Content-Disposition", "attachment; filename=IND_" + Path.GetFileName(xmlFile));
                                else
                                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(xmlFile));
                                //alert("1");
                                Response.ContentType = "application/octet-stream";
                                FileStream fs;
                                BinaryReader br;
                                fs = new FileStream(xmlFile, FileMode.Open);
                                br = new BinaryReader(fs);
                                Byte[] dataBytes = br.ReadBytes((int)(fs.Length - 1));
                                Response.BinaryWrite(dataBytes);
                                br.Close();
                                fs.Close();
                                Response.End();
                            }
                        }
                        catch (Exception ex)
                        {
                            //alert(ex.Message);
                            formReport.Visible = true;
                            formReport.InnerText = ex.Message.ToString();
                        }

                    }
                }
            }
            catch (Exception oex)
            {
                //divInvoiceHTML.InnerHtml = "<P style='color:red'>Template not found, contact IT Team... [software@datapage.org]</P><P style='color:red'>" + oex.Message + "</P>";
                //formReport.InnerHtml = "<P style='color:red'>Template not found, contact IT Team... [software@datapage.org]</P><P style='color:red'>" + oex.Message + "</P>";
                formReport.InnerHtml = "<P style='color:red'>" + oex.Message + "\nTemplate not found, contact IT Team... [software@datapage.org]</P><P style='color:red'>" + oex.Message + "</P>";
                //throw oex;
            }
            finally
            {
                //test = null;
            }
        }
        else
        {
            alert("NULL location");
           // formReport.InnerHtml = "<p> Wrong query String </p>";
        }
    }
    private void alert(string sMsg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + sMsg.Replace("'", "") + "');</script>");
    }

    protected void page_Unload(object sender, EventArgs e)
    {
        try
        {
            if (test.RptDoc != null)
            {
                test.RptDoc.Close();
                test.RptDoc.Dispose();
            }
            if (test.MedicalEduSub != null)
            {
                test.MedicalEduSub.Close();
                test.MedicalEduSub.Dispose();
            }
            if (test.SubRptDoc != null)
            {
                test.SubRptDoc.Close();
                test.SubRptDoc.Dispose();
            }
            
        }
        catch (Exception ex)
        { }
        finally
        { test = null; }
    }
    
}


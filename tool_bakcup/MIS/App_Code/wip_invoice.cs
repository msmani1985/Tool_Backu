using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Xml;
using System.Data.SqlClient;
using System.IO;
using Tools;


/// <summary>
/// Summary description for wip_invoice
/// </summary>
public class wip_invoice
{

    //------updated by Mugundhan for XML---------------

    int i = 1;
    string field = "field";
    string name = "name";

    string Description = "Description";
    string AccountCode1 = "AccountCode1";
    string AccountCode2 = "AccountCode2";
    string AccountCode3 = "AccountCode3";
    string AccountCode4 = "AccountCode4";
    string AccountCode5 = "AccountCode5";
    string AccountCode6 = "AccountCode6";
    string AccountCode7 = "AccountCode7";
    string AccountCode8 = "AccountCode8";
    string LineTax = "LineTax";
    string LineValue = "LineValue";
    string LineSense = "LineSense";
    string error_msg = "";
    wipinvoice wipds = new wipinvoice();
    string userName = "dp0934";
    string domain = "dpindia";
    string password = "KMS934";
    //--------------------------------------------------

    SqlConnection ocon = null;
	public wip_invoice()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private SqlConnection conopen(SqlConnection ocn)
    {

        if (ocn != null && ocn.State == ConnectionState.Open)
            return ocn;
        ocn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrIBLive1"].ToString());
        ocn.Open();
        return ocn;
    }
    private SqlConnection conclose(SqlConnection ocn)
    {
        if (ocn != null)
        {
            if (ocn.State != ConnectionState.Closed)
                ocn.Close();
            ocn.Dispose();
            return ocn;
        }
        return ocn;
    }

    public wipinvoice ExcProcedure(string sProcName, string[,] sparameter, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        wipinvoice ods = new wipinvoice();
        char[] separator = new char[] { ',' };
        string[] ParamVal;
        string[] ParamName;

        //string[] ParamType;
        //string[] ParamDir;
        // ParamVal = paramcollection.Split(separator);
        //ParamName = paramnames.Split(separator);
        //ParamType = paramtypes.Split(separator);
        //ParamDir = paramdirections.Split(separator);

        try
        {
            ocon=  conopen(ocon);
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = ocon;
            ocmd.CommandTimeout = 600;
            ocmd.Parameters.Clear();
            int i;
            //for (i = 0; i < ParamName.GetLength(0); i++)
            if (sparameter != null)
            {
                for (i = 0; i < sparameter.GetLength(0); i++)
                {
                    if (sparameter[i, 1].ToString().ToUpper() == "OUTPUT")
                    {
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), "").Direction = ParameterDirection.Output;
                        //flg = true;
                        //OutparamName = sparameter[i, 0].ToString();
                    }
                    else if (sparameter[i, 1] == null || sparameter[i, 1].ToString() == "" || sparameter[i, 1].ToString() == "0")
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), DBNull.Value);
                    else
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), sparameter[i, 1]);

                    //ocmd.Parameters.AddWithValue(sparameter[i,0].ToString(), sparameter[i,1]);                
                }
            }
            //ocmd.ExecuteNonQuery();
            SqlDataAdapter sqlad = new SqlDataAdapter(ocmd);
            sqlad.Fill(ods);
            if (ods == null || ods.Tables[1].Rows.Count <= 0)
            {
                ods = null;
            }
            return ods;
        }
        catch (Exception ex)
        {
            ocmd = null;
             ocon = conclose(ocon);
            throw ex;
        }
        finally
        {
            ocmd = null;
            ocon = conclose(ocon);
        }

    }
    
    private wipinvoice getdataset(string procname,CommandType cmdtype)
    {
        SqlCommand ocmd = new SqlCommand();
        wipinvoice ods = new wipinvoice();
        try
        {
            ocon = conopen(ocon);
            ocmd.CommandText = procname;
            ocmd.Connection = ocon;
            ocmd.CommandType = cmdtype;
            SqlDataAdapter oda = new SqlDataAdapter(ocmd);
            oda.Fill(ods);
            return ods;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { ocon = conclose(ocon); ocmd.Dispose(); ocmd = null; ods.Dispose(); ods = null; }
    }
    //public wipinvoice wip_getdataset(string pname,CommandType ctype)
    //{
    //    return ExcProcedure(pname, ctype);
    //    return ExcProcedure("P_INVOICED_ITEMS_OUTSTANDING_P", param, CommandType.StoredProcedure);
    //}

    public ReportDocument wip_createreport(string conno, string location, string invoiceno,string custnocw,string jourcode)
    {
       // wipinvoice wipds = new wipinvoice();
        ReportDocument wipdoc = new ReportDocument();
        ReportDocument wipsub=new ReportDocument();
        ReportDocument wipdetails = new ReportDocument();
        ReportDocument alldetails = new ReportDocument();
        ReportDocument xlsrpt = new ReportDocument();
        Report robj = new Report();
        if(location=="i")
            robj.sFilePath=ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString();
        else
            robj.sFilePath=ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString();
        robj.location = location;

        if (conno == "11529")
        {
            if (custnocw == "10216")
            {
                wipdoc.Load(HttpContext.Current.Application["spath"].ToString() + @"\InvoiceReports\wip_decker.rpt");
            }
            else if (custnocw == "10231")
            {
                wipdoc.Load(HttpContext.Current.Application["spath"].ToString() + @"\InvoiceReports\wip_invoice_pup.rpt");
            }
            else if (custnocw == "10235")
            {
                wipdoc.Load(HttpContext.Current.Application["spath"].ToString() + @"\InvoiceReports\wip_invoice_uk.rpt");
            }
            else if (custnocw == "10211")
            {
                wipdoc.Load(HttpContext.Current.Application["spath"].ToString() + @"\InvoiceReports\wip_invoice_bmj.rpt");
            }
            else
            {
                wipdoc.Load(HttpContext.Current.Application["spath"].ToString() + @"\InvoiceReports\wip_invoicerpt_cw.rpt");
            }
        }
        else
        {
            wipdoc.Load(HttpContext.Current.Application["spath"].ToString() + @"\InvoiceReports\wip_invoicerpt.rpt");
        }

        try
        {
            string[,] invno = { { "@invoiceno", invoiceno.ToString() } };
            string[,] conno1 = { { "@conno", conno.ToString() } };
            string[,] jourcode_1 = { { "@Jourcode", jourcode.ToString() } };
            if (invoiceno != "")
            {
                if (conno == "11529")
                {
                    wipds = ExcProcedure("SP_GET_INVOICE_WIP_FINAL_CW", new string[,] { { "@conno", invoiceno }, { "@custno", custnocw } }, CommandType.StoredProcedure);
                }
                else
                {
                    wipds = ExcProcedure("SP_GET_INVOICE_WIP_FINAL", invno, CommandType.StoredProcedure);
                }
            }
            else
            {
                if(conno == "11529" && custnocw=="10230")
                {
                    wipds = ExcProcedure("SP_GET_INVOICE_WIP_CW_NA", new string[,] { { "@conno", conno }, { "@custno", custnocw }, { "@Jourcode", jourcode } }, CommandType.StoredProcedure);
                }

                else if (conno == "11529")
                {
                    wipds = ExcProcedure("SP_GET_INVOICE_WIP_CW", new string[,] { { "@conno", conno }, { "@custno", custnocw } }, CommandType.StoredProcedure);
                }
                else
                {
                    wipds = ExcProcedure("SP_GET_INVOICE_WIP", conno1, CommandType.StoredProcedure);
                }
            }


            if (wipds != null && wipds.Tables[1].Rows.Count > 0)
            {
                wipds.Tables[1].Columns.Add("IND_TYPESETPRICE"); wipds.Tables[1].Columns.Add("DUB_TYPESETPRICE");
                wipds.Tables[1].Columns.Add("IND_COPYEDITPRICE"); wipds.Tables[1].Columns.Add("DUB_COPYEDITPRICE");
                wipds.Tables[1].Columns.Add("COPYEDIT_PRICECODE");
                wipds.Tables[1].Columns.Add("IND_SAMPRICE"); wipds.Tables[1].Columns.Add("DUB_SAMPRICE");
                wipds.Tables[1].Columns.Add("IND_CURRENCY"); wipds.Tables[1].Columns.Add("DUB_CURRENCY");
                wipds.Tables[1].Columns.Add("IND_TSVAL"); wipds.Tables[1].Columns.Add("IND_CEVAL");
                wipds.Tables[1].Columns.Add("Country_CW", typeof(string));

                if (custnocw != "2556")
                {
                    wipds.Tables[1].Columns.Add("PDFQCValue_Price");
                    wipds.Tables[1].Columns.Add("VendorMgtValue_Price");
                    wipds.Tables[1].Columns.Add("CustService_Price");
                    wipds.Tables[1].Columns.Add("XMLQA_Price");
                    wipds.Tables[1].Columns.Add("TFPrice");
                }




                wipds.AcceptChanges();
                string returnval = "";
                for (int dscnt = 0; dscnt < wipds.Tables[1].Rows.Count; dscnt++)
                {
                    //For Typeset
                    returnval = getjournalcodeprice(wipds.Tables[1].Rows[dscnt]["JCNO_2010"].ToString(), "i", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString());
                    wipds.Tables[1].Rows[dscnt]["IND_TYPESETPRICE"] = returnval.Split(',').GetValue(0);
                    wipds.Tables[1].Rows[dscnt]["IND_CURRENCY"] = returnval.Split(',').GetValue(1);
                    returnval = getjournalcodeprice(wipds.Tables[1].Rows[dscnt]["JCNO_2010"].ToString(), "d", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString());
                    wipds.Tables[1].Rows[dscnt]["DUB_TYPESETPRICE"] = returnval.Split(',').GetValue(0);
                    wipds.Tables[1].Rows[dscnt]["DUB_CURRENCY"] = returnval.Split(',').GetValue(1);
                    wipds.Tables[1].Rows[dscnt]["IND_TSVAL"] = Convert.ToDouble(wipds.Tables[1].Rows[dscnt]["IND_TYPESETPRICE"]) * Convert.ToDouble(wipds.Tables[1].Rows[dscnt]["NOOFPAGES"]);

                    if (custnocw != "2556")
                    {

                        returnval = getjournalcodeprice(wipds.Tables[1].Rows[dscnt]["PDFQCValue"].ToString(), "d", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString());
                        wipds.Tables[1].Rows[dscnt]["PDFQCValue_Price"] = returnval.Split(',').GetValue(0);

                        returnval = getjournalcodeprice(wipds.Tables[1].Rows[dscnt]["VendorMgtValue"].ToString(), "d", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString());
                        wipds.Tables[1].Rows[dscnt]["VendorMgtValue_Price"] = returnval.Split(',').GetValue(0);

                        returnval = getjournalcodeprice(wipds.Tables[1].Rows[dscnt]["CustService"].ToString(), "d", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString());
                        wipds.Tables[1].Rows[dscnt]["CustService_Price"] = returnval.Split(',').GetValue(0);

                        returnval = getjournalcodeprice(wipds.Tables[1].Rows[dscnt]["XMLQAValue"].ToString(), "d", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString());
                        wipds.Tables[1].Rows[dscnt]["XMLQA_Price"] = returnval.Split(',').GetValue(0);
                    }

                    if (custnocw == "10230")
                    {
                        wipds.Tables[1].Rows[dscnt]["TFPrice"] = ((Convert.ToDouble(wipds.Tables[1].Rows[dscnt]["NOOFPAGES"]) + Convert.ToDouble(wipds.Tables[1].Rows[dscnt]["ResearchPage"])) * Convert.ToDouble(wipds.Tables[1].Rows[dscnt]["DUB_TYPESETPRICE"])) + Convert.ToDouble(wipds.Tables[1].Rows[dscnt]["TPrice"]);
                    }

                    wipds.Tables[1].Rows[dscnt]["Country_CW"] = location;
                    //For CopyEdit
                    if (wipds.Tables[1].Rows[dscnt]["ISCOPYEDIT"].ToString() == "1")
                    {
                        if (wipds.Tables[1].Rows[dscnt]["PAGEFORMAT"].ToString().Trim().ToLower() == "small")
                        {
                            wipds.Tables[1].Rows[dscnt]["IND_COPYEDITPRICE"] = getjournalcodeprice("140", "i", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString()).Split(',').GetValue(0);
                            wipds.Tables[1].Rows[dscnt]["DUB_COPYEDITPRICE"] = getjournalcodeprice("140", "d", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString()).Split(',').GetValue(0);
                            wipds.Tables[1].Rows[dscnt]["COPYEDIT_PRICECODE"] = 140;
                            wipds.Tables[1].Rows[dscnt]["IND_CEVAL"] = Convert.ToDouble(wipds.Tables[1].Rows[dscnt]["COPYEDIT_PRICECODE"]) * Convert.ToDouble(wipds.Tables[1].Rows[dscnt]["IND_COPYEDITPRICE"]);
                        }
                        else if (wipds.Tables[1].Rows[dscnt]["PAGEFORMAT"].ToString().Trim().ToLower() == "large")
                        {
                            wipds.Tables[1].Rows[dscnt]["IND_COPYEDITPRICE"] = getjournalcodeprice("139", "i", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString()).Split(',').GetValue(0);
                            wipds.Tables[1].Rows[dscnt]["DUB_COPYEDITPRICE"] = getjournalcodeprice("139", "d", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString()).Split(',').GetValue(0);
                            wipds.Tables[1].Rows[dscnt]["COPYEDIT_PRICECODE"] = 139;
                        }

                    }

                    //For SAM

                    if (conno != "11529")
                    {
                        if (wipds.Tables[1].Rows[dscnt]["ISSAM"].ToString() == "1")
                        {
                            wipds.Tables[1].Rows[dscnt]["IND_SAMPRICE"] = getjournalcodeprice(wipds.Tables[1].Rows[dscnt]["SAM_PRICECODE"].ToString(), "i", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString()).Split(',').GetValue(0);
                            wipds.Tables[1].Rows[dscnt]["DUB_SAMPRICE"] = getjournalcodeprice(wipds.Tables[1].Rows[dscnt]["SAM_PRICECODE"].ToString(), "d", wipds.Tables[1].Rows[dscnt]["jourcode"].ToString()).Split(',').GetValue(0);
                        }
                    }
                    if (wipds.Tables[1].Rows[dscnt]["INO"].ToString() == "0")
                        wipds.Tables[1].Rows[dscnt]["ISSUE_PAGES"] = 0;
                    else
                        wipds.Tables[1].Rows[dscnt]["ARTICLE_PAGES"] = 0;

                }
                wipdoc.SetDataSource(wipds.Tables[1]);
                if (conno == "11529")
                {
                    //ExporttoExcel(wipds.Tables[1]);
                }
                else
                {
                    wipsub = wipdoc.Subreports["wip_invoicerptxls.rpt"];
                    wipsub.SetDataSource(wipds.Tables[1]);
                    wipdetails = wipdoc.Subreports["wip_articledetails.rpt"];
                    wipdetails.SetDataSource(wipds.Tables[1]);
                    alldetails = wipdoc.Subreports["wip_alldetails.rpt"];
                    alldetails.SetDataSource(wipds.Tables[1]);
                    wipdoc.SetParameterValue("location", location);
                    wipdoc.SetParameterValue("location", location, "wip_invoicerptxls.rpt");
                    wipdoc.SetParameterValue("location", location, "wip_articledetails.rpt");
                    wipdoc.SetParameterValue("location", location, "wip_alldetails.rpt");
                }


                if (wipds.Tables[1].Rows[0]["INVNO"] != null && wipds.Tables[1].Rows[0]["INVNO"].ToString() != "")
                {
                    //string error_msg = "";
                    if (location == "i")
                        error_msg = robj.InvoiceValueFromCR(wipdoc, "WIP_" + invoiceno, "", invoiceno.ToString(), wipds.Tables[1].Rows[0]["AINVOICEDDATE"].ToString(), wipds.Tables[1].Rows[0]["IND_CURRENCY"].ToString(), "WIP_" + invoiceno);
                    else
                        error_msg = robj.InvoiceValueFromCR(wipdoc, "WIP_" + invoiceno, "", invoiceno.ToString(), wipds.Tables[1].Rows[0]["AINVOICEDDATE"].ToString(), wipds.Tables[1].Rows[0]["DUB_CURRENCY"].ToString(), "WIP_" + invoiceno);
                    double dResult = 0;
                    if (Double.TryParse(error_msg, out dResult) == false && error_msg != "") throw new ArgumentException(error_msg.ToString());
                    if (conno != "11529")
                    {
                        wipdoc.ExportToDisk(ExportFormatType.PortableDocFormat, robj.sFilePath + "\\Datapage_WIP_" + invoiceno + ".pdf");
                        wipdoc.ExportToDisk(ExportFormatType.HTML32, robj.sFilePath + "\\wip_" + invoiceno + ".htm");
                    }
                    else
                    {
                        wipdoc.ExportToDisk(ExportFormatType.PortableDocFormat, robj.sFilePath + "\\WIP_" + invoiceno + ".pdf");
                        wipdoc.ExportToDisk(ExportFormatType.HTML32, robj.sFilePath + "\\wip_" + invoiceno + ".htm");
                    }
                    if (conno != "11529")
                    {
                        string filename = robj.sFilePath + "\\wip_" + invoiceno + ".xls";
                        xlsrpt.Load(HttpContext.Current.Application["spath"].ToString() + @"\InvoiceReports\wip_invoicerptxls.rpt");
                        xlsrpt.SetDataSource(wipds.Tables[1]); 
                        xlsrpt.SetParameterValue("location", location);
                    }
                    
                    //ExportOptions oEOption = new ExportOptions();
                    //oEOption.ExportDestinationType = ExportDestinationType.DiskFile;
                    ////oEOption.ExportFormatType = ExportFormatType.Excel;
                    //DiskFileDestinationOptions DFOption = new DiskFileDestinationOptions();
                    //DFOption.DiskFileName = filename;
                    //oEOption.ExportDestinationOptions = DFOption;
                    //xlsrpt.Export(oEOption);
                    //robj.ExcelSetting(filename, null);
                    if (location == "d" && conno != "11529")
                    {
                        CreateXML();
                    }

                    //kalimuthu 30012015
                    if (location == "i" && conno != "11529")
                    {
                        using (new Impersonator(userName, domain, password))
                        {
                            string dstnLocation = ConfigurationManager.ConnectionStrings["ArticlePDFFileDes"].ToString() + invoiceno;
                            string SourceFile = ConfigurationManager.ConnectionStrings["ArticlePDFFileSource"].ToString() + "\\Taylor & Francis\\";
                            string FileNameSource = "", FileNameDes = "";
                            if (!Directory.Exists(dstnLocation))
                            {
                                System.IO.Directory.CreateDirectory(dstnLocation);
                            }

                            for (int dscnt = 0; dscnt < wipds.Tables[1].Rows.Count; dscnt++)
                            {
                                FileNameSource = wipds.Tables[1].Rows[dscnt]["Jourcode"].ToString().Trim() + "\\Articles\\" + wipds.Tables[1].Rows[dscnt]["Jourcode"].ToString().Trim() + wipds.Tables[1].Rows[dscnt]["Amanuscriptid"].ToString().Trim() + "\\Preview\\" + wipds.Tables[1].Rows[dscnt]["Jourcode"].ToString().Trim() + "_A_" + wipds.Tables[1].Rows[dscnt]["Amanuscriptid"].ToString().Trim() + "_O.pdf";
                                FileNameDes = wipds.Tables[1].Rows[dscnt]["Jourcode"].ToString().Trim() + "_A_" + wipds.Tables[1].Rows[dscnt]["Amanuscriptid"].ToString().Trim() + "_O.pdf";
                                if (File.Exists(Path.Combine(SourceFile, FileNameSource)))
                                {
                                    File.Copy(Path.Combine(SourceFile, FileNameSource), Path.Combine(dstnLocation, FileNameDes), true);
                                }
                                FileNameSource = "";
                                FileNameDes = "";
                            }
                        }
                    }

                }
            }
            else
                throw new ArgumentException("No Article has been found, Please check");
            
            return wipdoc;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { wipds.Dispose(); wipds = null; robj = null; }
    }
    public string getjournalcodeprice(string jcpno,string location,string jourcode)
    {
        XmlDocument xdoc = new XmlDocument();
        XmlNode xnode = null;

        string xmlpath="";
        try
        {
            if (location == "i") xmlpath = ConfigurationManager.ConnectionStrings["indiaPCXML"].ToString();
            else xmlpath = ConfigurationManager.ConnectionStrings["dublinPCXML"].ToString();
            xdoc.Load(xmlpath);
            xnode = xdoc.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@JCPNO='" + jcpno + "']");
            if (xnode != null && location == "i")
                return xnode.Attributes.GetNamedItem("INDIAPRICE").Value + "," + xnode.Attributes.GetNamedItem("CURRENCY").Value;
            else if (xnode != null && location == "d")
                return xnode.Attributes.GetNamedItem("JCPPRICE").Value + "," + xnode.Attributes.GetNamedItem("CURRENCY").Value;
            else
                throw new ArgumentException(jourcode.Trim() + " - Price code missing");
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        { xdoc = null; xnode = null; xmlpath = ""; }
    }

    public DataView dt()
    {

        DataTable dtable = wipds.Tables[1].DefaultView.ToTable("wip_invoicetable", true, "JOURCODE");
        DataView oview = dtable.DefaultView;
        return oview;
    }

    public void CreateXML()
    {
        //XmlDocument xmldoc = new XmlDocument();
        //XmlNode parent_element = xmldoc.CreateElement("files");
        //xmldoc.AppendChild(parent_element);

        //XmlNode xmlnod = xmldoc.CreateElement("file");
        //xmlnod.AppendChild(xmlnod);

        //DataTable dtxml = new DataTable();
        //dtxml = wipds.Tables[1].Copy();
        //dtxml.AcceptChanges();



        //dtxml.Tables[1].DefaultView.Sort = "JOURNO ASC";
        //dtxml.Tables[1] = dtxml.Tables[1].DefaultView.ToTable();
        


        int selection = 0;

        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("files");
        xmlDoc.AppendChild(rootNode);

        XmlNode rootNode_child = xmlDoc.CreateElement("file");
        XmlAttribute attribute = xmlDoc.CreateAttribute("type");
        attribute.Value = "INVOICES";
        rootNode_child.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(rootNode_child);

        XmlNode userNode = xmlDoc.CreateElement("document");
        userNode.InnerText = "Datapage_WIP_" + wipds.Tables[1].Rows[0]["INVNO"].ToString() + ".pdf";
        rootNode_child.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "Supplier";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "DATAPAGE";
        rootNode_child.AppendChild(userNode);

        DataTable oTable = new DataTable();
        oTable = dt().Table;

        
        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "Description";
        userNode.Attributes.Append(attribute);

        int jourcount = oTable.Rows.Count;
        if (jourcount == 1)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 2)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 3)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 4)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[3]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 5)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[3]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[4]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 6)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[3]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[4]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[5]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 7)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[3]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[4]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[5]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[6]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 8)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[3]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[4]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[5]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[6]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[7]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 9)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[3]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[4]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[5]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[6]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[7]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[8]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 10)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[3]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[4]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[5]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[6]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[7]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[8]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[9]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 11)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[3]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[4]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[5]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[6]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[7]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[8]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[9]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[10]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 12)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[3]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[4]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[5]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[6]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[7]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[8]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[9]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[10]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[11]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 13)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[3]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[4]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[5]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[6]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[7]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[8]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[9]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[10]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[11]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[12]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        if (jourcount == 14)
        {
            userNode.InnerText = "WIP " + oTable.Rows[0]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[1]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[2]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[3]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[4]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[5]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[6]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[7]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[8]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[9]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[10]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[11]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[12]["JOURCODE"].ToString().Trim() + "/" + oTable.Rows[13]["JOURCODE"].ToString().Trim() + " " + System.DateTime.Now.ToString("MMMMMMMMMM");
        }
        rootNode_child.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "ExtRef1";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = wipds.Tables[1].Rows[0]["INVNO"].ToString();
        rootNode_child.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "ExtRef2";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode_child.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "ExtRef3";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode_child.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "CurCode";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = wipds.Tables[1].Rows[0]["DUB_CURRENCY"].ToString();
        userNode.InnerText = "GBP";
        rootNode_child.AppendChild(userNode);

        string InvoiceDate = wipds.Tables[1].Rows[0]["AINVOICEDDATE"].ToString();
        string InvoiceDate_XML = Convert.ToDateTime(InvoiceDate).ToString("yyyy-MM-dd");

        //string InvoiceDate_XML_cont = DateTime.Parse(InvoiceDate_XML.ToString().Trim()).ToString("yyyy/MM/dd");

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "Date";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = InvoiceDate_XML;
        rootNode_child.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "BarCode";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode_child.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "CmpCode";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "GB174";
        rootNode_child.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "AccountCode";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "ST020187";
        rootNode_child.AppendChild(userNode);

        string Name = wipds.Tables[1].Rows[0]["CONFIRSTNAME"].ToString().Trim().ToUpper();
        string[] split_name = Name.Split(' ');


        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "AuthFirstName";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = split_name[0];
        rootNode_child.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "AuthLastName";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = split_name[1];
        rootNode_child.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "DocId";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode_child.AppendChild(userNode);
        //------------------------------------------------------------------------//
        attribute = null;



        //XmlNode linenode = xmlDoc.CreateElement("line");
        //rootNode.AppendChild(linenode);


        foreach (DataRow dr in wipds.Tables[1].Rows)
        {
            XmlNode linenode = xmlDoc.CreateElement("line");
            //if (ads_xml.Tables[1].Columns["INVNO"].ToString() != "" || ads_xml.Tables[1].Columns["INVNO"].ToString() != "&nbsp")
            if (dr["INVNO"].ToString() != " " || dr["INVNO"].ToString() != "&npsb" || dr["INVNO"].ToString() != DBNull.Value.ToString())
            {
                for (i = 1; i <= 12; i++)
                {
                    selection = 0;
                    createnode(xmlDoc, linenode, attribute, field, name, dr, selection);
                    // linenode.AppendChild(userNode);
                }


            }

            rootNode_child.AppendChild(linenode);

        }

        foreach (DataRow dr in wipds.Tables[1].Rows)
        {
            //XmlNode linenode = xmlDoc.CreateElement("line");
            //if (ads_xml.Tables[1].Columns["INVNO"].ToString() != "" || ads_xml.Tables[1].Columns["INVNO"].ToString() != "&nbsp")
            //if (dr["ISCOPYEDIT"].ToString() == "1" && dr["ISSAM"].ToString() == "1" && dr["ISFPM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y")
            if (dr["ISCOPYEDIT"].ToString() == "1" && dr["ISSAM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y" && dr["CE_PAGES"].ToString() != "0" && dr["SAM_PRICECODE"].ToString() == "203")
            {
                XmlNode linenode = xmlDoc.CreateElement("line");
                if (dr["INVNO"].ToString() != " " || dr["INVNO"].ToString() != "&npsb" || dr["INVNO"].ToString() != DBNull.Value.ToString())
                {
                    for (i = 1; i <= 12; i++)
                    {
                        selection = 1;
                        createnode(xmlDoc, linenode, attribute, field, name, dr, selection);
                        // linenode.AppendChild(userNode);
                    }


                }
                rootNode_child.AppendChild(linenode);
            }


            //XmlNode linenode = xmlDoc.CreateElement("line");
            //if (ads_xml.Tables[1].Columns["INVNO"].ToString() != "" || ads_xml.Tables[1].Columns["INVNO"].ToString() != "&nbsp")
            //if (dr["ISCOPYEDIT"].ToString() == "1" && dr["ISSAM"].ToString() == "1" && dr["ISFPM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y")
            else if (dr["ISCOPYEDIT"].ToString() == "1" && dr["ISSAM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y" && dr["CE_PAGES"].ToString() != "0" && dr["SAM_PRICECODE"].ToString() == "140")
            {
                XmlNode linenode = xmlDoc.CreateElement("line");
                if (dr["INVNO"].ToString() != " " || dr["INVNO"].ToString() != "&npsb" || dr["INVNO"].ToString() != DBNull.Value.ToString())
                {
                    for (i = 1; i <= 12; i++)
                    {
                        selection = 5;
                        createnode(xmlDoc, linenode, attribute, field, name, dr, selection);
                        // linenode.AppendChild(userNode);
                    }


                }
                rootNode_child.AppendChild(linenode);
            }


            //if (ads_xml.Tables[1].Columns["INVNO"].ToString() != "" || ads_xml.Tables[1].Columns["INVNO"].ToString() != "&nbsp")
            //if (dr["ISCOPYEDIT"].ToString() == "1" && dr["ISSAM"].ToString() == "1" && dr["ISFPM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y")
            else if (dr["ISCOPYEDIT"].ToString() == "1" && dr["ISSAM"].ToString() == "0" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y" && dr["CE_PAGES"].ToString() != "0")
            {
                XmlNode linenode = xmlDoc.CreateElement("line");
                if (dr["INVNO"].ToString() != " " || dr["INVNO"].ToString() != "&npsb" || dr["INVNO"].ToString() != DBNull.Value.ToString())
                {
                    for (i = 1; i <= 12; i++)
                    {
                        selection = 6;
                        createnode(xmlDoc, linenode, attribute, field, name, dr, selection);
                        // linenode.AppendChild(userNode);
                    }


                }
                rootNode_child.AppendChild(linenode);
            }

            //if (ads_xml.Tables[1].Columns["INVNO"].ToString() != "" || ads_xml.Tables[1].Columns["INVNO"].ToString() != "&nbsp")
            //if (dr["ISCOPYEDIT"].ToString() == "1" && dr["ISSAM"].ToString() == "1" && dr["ISFPM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y")
            else if (dr["ISCOPYEDIT"].ToString() == "0" && dr["ISSAM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y")
            {
                XmlNode linenode = xmlDoc.CreateElement("line");
                if (dr["INVNO"].ToString() != " " || dr["INVNO"].ToString() != "&npsb" || dr["INVNO"].ToString() != DBNull.Value.ToString())
                {
                    for (i = 1; i <= 12; i++)
                    {
                        selection = 7;
                        createnode(xmlDoc, linenode, attribute, field, name, dr, selection);
                        // linenode.AppendChild(userNode);
                    }


                }
                rootNode_child.AppendChild(linenode);
            }

            //if (ads_xml.Tables[1].Columns["INVNO"].ToString() != "" || ads_xml.Tables[1].Columns["INVNO"].ToString() != "&nbsp")
            //if (dr["ISCOPYEDIT"].ToString() == "1" && dr["ISSAM"].ToString() == "1" && dr["ISFPM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y")
            else if (dr["ISCOPYEDIT"].ToString() == "1" && dr["ISSAM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y" && dr["CE_PAGES"].ToString() == "0")
            {
                XmlNode linenode = xmlDoc.CreateElement("line");
                if (dr["INVNO"].ToString() != " " || dr["INVNO"].ToString() != "&npsb" || dr["INVNO"].ToString() != DBNull.Value.ToString())
                {
                    for (i = 1; i <= 12; i++)
                    {
                        selection = 2;
                        createnode(xmlDoc, linenode, attribute, field, name, dr, selection);
                        // linenode.AppendChild(userNode);
                    }

                }
                rootNode_child.AppendChild(linenode);
            }



            //if (ads_xml.Tables[1].Columns["INVNO"].ToString() != "" || ads_xml.Tables[1].Columns["INVNO"].ToString() != "&nbsp")
            //if (dr["ISCOPYEDIT"].ToString() == "1" && dr["ISSAM"].ToString() == "1" && dr["ISFPM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y")
            else if (dr["ISCOPYEDIT"].ToString() == "0" && dr["ISSAM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y" && dr["CE_PAGES"].ToString() == "0")
            {
                XmlNode linenode = xmlDoc.CreateElement("line");
                if (dr["INVNO"].ToString() != " " || dr["INVNO"].ToString() != "&npsb" || dr["INVNO"].ToString() != DBNull.Value.ToString())
                {
                    for (i = 1; i <= 12; i++)
                    {
                        selection = 3;
                        createnode(xmlDoc, linenode, attribute, field, name, dr, selection);
                        //linenode.AppendChild(userNode);
                    }

                }
                rootNode_child.AppendChild(linenode);
            }



            //if (ads_xml.Tables[1].Columns["INVNO"].ToString() != "" || ads_xml.Tables[1].Columns["INVNO"].ToString() != "&nbsp")
            //if (dr["ISCOPYEDIT"].ToString() == "1" && dr["ISSAM"].ToString() == "1" && dr["ISFPM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "Y")
            else if (dr["ISCOPYEDIT"].ToString() == "0" && dr["ISSAM"].ToString() == "1" && dr["AEXTRA_COPY_EDIT"].ToString() == "N" && dr["SAM_PAGES"].ToString() != "0")
            {
                XmlNode linenode = xmlDoc.CreateElement("line");
                if (dr["INVNO"].ToString() != " " || dr["INVNO"].ToString() != "&npsb" || dr["INVNO"].ToString() != DBNull.Value.ToString())
                {
                    for (i = 1; i <= 12; i++)
                    {
                        selection = 4;
                        createnode(xmlDoc, linenode, attribute, field, name, dr, selection);
                        //linenode.AppendChild(userNode);
                    }

                }
                rootNode_child.AppendChild(linenode);

            }


            
        }

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "LineSense";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "debit";
        rootNode_child.AppendChild(userNode);

        double docvalue = Convert.ToDouble(error_msg);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "DocValue";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = docvalue.ToString("0.00");
        rootNode_child.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "DocSumTax";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "0.00";
        rootNode_child.AppendChild(userNode);

        string strFileName_path = "Datapage_WIP_" + wipds.Tables[1].Rows[0]["INVNO"].ToString() + ".xml ";

        if (!System.IO.Directory.Exists(@"\\192.9.201.222\xmlinvoices\"))
        {
            System.IO.Directory.CreateDirectory(@"\\192.9.201.222\xmlinvoices\");
        }
        xmlDoc.Save(@"\\192.9.201.222\xmlinvoices\" + strFileName_path);


    }


    private void createnode(XmlDocument xmldocnode, XmlNode rootnode, XmlAttribute attr, string field, string name, DataRow drw,int select)
    {
        //ArticleandPages ads_xml = ExcelArticlesandPages("not excel", "");
        XmlNode usernode = xmldocnode.CreateElement(field);
        attr = xmldocnode.CreateAttribute(name);
        //int x = 1;
        //for (x = 1; x >= ads_xml.Tables[1].Rows.Count; x++)
        //{
        //foreach (DataRow dr in ads_xml.Tables[1].Rows)
        //{
        //    //if (ads_xml.Tables[1].Columns["INVNO"].ToString() != "" || ads_xml.Tables[1].Columns["INVNO"].ToString() != "&nbsp")
        //    if (dr["INVNO"].ToString() != "" || dr["INVNO"].ToString() != "&nbsp")
        //    {
        if (i == 1)
        {
            attr.Value = Description;
        }
        else if (i == 2)
            attr.Value = AccountCode1;
        else if (i == 3)
            attr.Value = AccountCode2;
        else if (i == 4)
            attr.Value = AccountCode3;
        else if (i == 5)
            attr.Value = AccountCode4;
        else if (i == 6)
            attr.Value = AccountCode5;
        else if (i == 7)
            attr.Value = AccountCode6;
        else if (i == 8)
            attr.Value = AccountCode7;
        else if (i == 9)
            attr.Value = AccountCode8;
        else if (i == 10)
            attr.Value = LineTax;
        else if (i == 11)
            attr.Value = LineValue;
        else if (i == 12)
        {
            attr.Value = LineSense;

        }
        usernode.Attributes.Append(attr);

        int Tpages = Convert.ToInt32(drw["ARTICLE_PAGES"]);
        double TPamt = Convert.ToDouble(drw["DUB_TYPESETPRICE"]);
        

        if (Convert.ToString(select) == "0")
        {
            double Tamount;
            if (drw["JCNO_2010"].ToString().Trim() == "126")
                Tamount = Convert.ToDouble(drw["DUB_TYPESETPRICE"]);
            else
                Tamount = Tpages * TPamt;

            if (i == 1)
                usernode.InnerText = drw["AMANUSCRIPTID"].ToString().Trim() + " " + "TYPESETTING (PROOFS+MAKE-UP)";
            else if (i == 2)
                usernode.InnerText = "W40203";
            else if (i == 3)
                usernode.InnerText = "PU" + drw["JOURCODE"].ToString().Trim();
            else if (i == 4)
                usernode.InnerText = "BPMIX";
            else if (i == 11)
                usernode.InnerText = Tamount.ToString("0.00");
            
        }

        if (Convert.ToString(select) == "1")
        {
            double SAMamt = Convert.ToDouble(drw["DUB_SAMPRICE"]);
            double CEamt = Convert.ToDouble(drw["DUB_COPYEDITPRICE"]);
            double TotalCEamt = CEamt + SAMamt;

            double CE_Amount = Tpages * TotalCEamt;
            if (drw["ISCOPYEDIT"].ToString() == "1" && drw["ISSAM"].ToString() == "1" && drw["AEXTRA_COPY_EDIT"].ToString() == "Y" && drw["CE_PAGES"].ToString() != "0")
            {
                if (i == 1)
                    usernode.InnerText = drw["AMANUSCRIPTID"].ToString().Trim() + " " + "COPY EDITING+COLLATION";
                else if (i == 2)
                    usernode.InnerText = "W42534";
                else if (i == 3)
                    usernode.InnerText = "PU" + drw["JOURCODE"].ToString().Trim();
                else if (i == 4)
                    usernode.InnerText = "BPMIX";
                else if (i == 11)
                    usernode.InnerText = CE_Amount.ToString("0.00");
            }
        }

        if (Convert.ToString(select) == "6")
        {
            double CEamt = Convert.ToDouble(drw["DUB_COPYEDITPRICE"]);
            //double TotalCEamt = CEamt + SAMamt;

            double CE_Amount = Tpages * CEamt;
            if (drw["ISCOPYEDIT"].ToString() == "1" && drw["ISSAM"].ToString() == "0" && drw["AEXTRA_COPY_EDIT"].ToString() == "Y" && drw["CE_PAGES"].ToString() != "0")
            {
                if (i == 1)
                    usernode.InnerText = drw["AMANUSCRIPTID"].ToString().Trim() + " " + "COPY EDITING";
                else if (i == 2)
                    usernode.InnerText = "W42534";
                else if (i == 3)
                    usernode.InnerText = "PU" + drw["JOURCODE"].ToString().Trim();
                else if (i == 4)
                    usernode.InnerText = "BPMIX";
                else if (i == 11)
                    usernode.InnerText = CE_Amount.ToString("0.00");
            }
        }

        if (Convert.ToString(select) == "7")
        {
            double SAMamt = Convert.ToDouble(drw["DUB_SAMPRICE"]);
            //double CEamt = Convert.ToDouble(drw["DUB_COPYEDITPRICE"]);
            //double TotalCEamt = SAMamt 
            double CE_Amount = Tpages * SAMamt;
            if (drw["ISCOPYEDIT"].ToString() == "0" && drw["ISSAM"].ToString() == "1" && drw["AEXTRA_COPY_EDIT"].ToString() == "Y")
            {
                if (i == 1)
                    usernode.InnerText = drw["AMANUSCRIPTID"].ToString().Trim() + " " + "COLLATION";
                else if (i == 2)
                    usernode.InnerText = "W42534";
                else if (i == 3)
                    usernode.InnerText = "PU" + drw["JOURCODE"].ToString().Trim();
                else if (i == 4)
                    usernode.InnerText = "BPMIX";
                else if (i == 11)
                    usernode.InnerText = CE_Amount.ToString("0.00");
            }
        }

        if (Convert.ToString(select) == "2")
        {
            double SAMamt = Convert.ToDouble(drw["DUB_SAMPRICE"]);
            //double CEamt = Convert.ToDouble(drw["DUB_COPYEDITPRICE"]);
            //double TotalCEamt = SAMamt 
            double CE_Amount = Tpages * SAMamt;
            if (drw["ISCOPYEDIT"].ToString() == "1" || drw["ISCOPYEDIT"].ToString() == "0" && drw["ISSAM"].ToString() == "1" && drw["AEXTRA_COPY_EDIT"].ToString() == "Y" && drw["CE_PAGES"].ToString() == "0")
            {
                if (i == 1)
                    usernode.InnerText = drw["AMANUSCRIPTID"].ToString().Trim() + " " + "COLLATION";
                else if (i == 2)
                    usernode.InnerText = "W42534";
                else if (i == 3)
                    usernode.InnerText = "PU" + drw["JOURCODE"].ToString().Trim();
                else if (i == 4)
                    usernode.InnerText = "BPMIX";
                else if (i == 11)
                    usernode.InnerText = CE_Amount.ToString("0.00");
            }
        }

        if (Convert.ToString(select) == "3")
        {
            double SAMamt = Convert.ToDouble(drw["DUB_SAMPRICE"]);
            //double CEamt = Convert.ToDouble(drw["DUB_COPYEDITPRICE"]);
            //double TotalCEamt = SAMamt 
            double CE_Amount = Tpages * SAMamt;
            if (drw["ISCOPYEDIT"].ToString() == "0" && drw["ISSAM"].ToString() == "1" && drw["AEXTRA_COPY_EDIT"].ToString() == "Y" && drw["CE_PAGES"].ToString() == "0")
            {
                if (i == 1)
                    usernode.InnerText = drw["AMANUSCRIPTID"].ToString().Trim() + " " + "COLLATION";
                else if (i == 2)
                    usernode.InnerText = "W42534";
                else if (i == 3)
                    usernode.InnerText = "PU" + drw["JOURCODE"].ToString().Trim();
                else if (i == 4)
                    usernode.InnerText = "BPMIX";
                else if (i == 11)
                    usernode.InnerText = CE_Amount.ToString("0.00");
            }
        }

        if (Convert.ToString(select) == "4")
        {
            double SAMamt = Convert.ToDouble(drw["DUB_SAMPRICE"]);
            //double CEamt = Convert.ToDouble(drw["DUB_COPYEDITPRICE"]);
            //double TotalCEamt = SAMamt 
            double CE_Amount = Tpages * SAMamt;
            if (drw["ISCOPYEDIT"].ToString() == "0" && drw["ISSAM"].ToString() == "1" && drw["AEXTRA_COPY_EDIT"].ToString() == "N" && drw["SAM_PAGES"].ToString() != "0")
            {
                if (i == 1)
                    usernode.InnerText = drw["AMANUSCRIPTID"].ToString().Trim() + " " + "COLLATION";
                else if (i == 2)
                    usernode.InnerText = "W42534";
                else if (i == 3)
                    usernode.InnerText = "PU" + drw["JOURCODE"].ToString().Trim();
                else if (i == 4)
                    usernode.InnerText = "BPMIX";
                else if (i == 11)
                    usernode.InnerText = CE_Amount.ToString("0.00");
            }
        }
        if (Convert.ToString(select) == "5")
        {
            double SAMamt = Convert.ToDouble(drw["DUB_SAMPRICE"]);
            double CEamt = Convert.ToDouble(drw["DUB_COPYEDITPRICE"]);
            //double FPMamt = 1.00;
            double CE_Amount = Tpages * (SAMamt + CEamt);
            //CE_Amount = CE_Amount * FPMamt;
            if (drw["ISCOPYEDIT"].ToString() == "1" || drw["ISCOPYEDIT"].ToString() == "0" && drw["ISSAM"].ToString() == "1" && drw["AEXTRA_COPY_EDIT"].ToString() == "Y" && drw["CE_PAGES"].ToString() == "0")
            {
                if (i == 1)
                    usernode.InnerText = drw["AMANUSCRIPTID"].ToString().Trim() + " " + "COPY EDITING+FPM";
                else if (i == 2)
                    usernode.InnerText = "W42534";
                else if (i == 3)
                    usernode.InnerText = "PU" + drw["JOURCODE"].ToString().Trim();
                else if (i == 4)
                    usernode.InnerText = "BPMIX";
                else if (i == 11)
                    usernode.InnerText = CE_Amount.ToString("0.00");
            }
        }

        // if (RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString().IndexOf('/')[1])==3)
        // {
        //   (RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString(),'/')[1]

                     //  else if (len(split(RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString(),'/')[1])=2) 
        //       '0' & split(RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString(),'/')[1]
        //  else if (len(split(RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString(),'/')[1])=1) 
        //       '00' & split(RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString(),'/')[1]
        //}

        if (i == 5)
            usernode.InnerText = "V00000";
        //else if (i == 6)
        //    usernode.InnerText = "W40203";
        //else if (i == 7)
        //    usernode.InnerText = "W40203";
        //else if (i == 8)
        //    usernode.InnerText = "W40203";
        //else if (i == 9)
        //    usernode.InnerText = "W40203";

        else if (i == 10)
            usernode.InnerText = "0.00";
        
        else if (i == 12)
            usernode.InnerText = "debit";



        rootnode.AppendChild(usernode);


    }
    //}


}

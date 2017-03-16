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
using System.IO;
using System.Text.RegularExpressions;
using Tools;
using System.Net;
using System.Net.NetworkInformation;

public partial class CharlesworthQMS_StyleSheet : System.Web.UI.Page
{
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;
    string strRegFile1;
    datasourceSQL dSql = new datasourceSQL();
    private static string sSortExpression = "";
    string userName = "qmsss";//ConfigurationManager.ConnectionStrings["iUsername"].ConnectionString;
    string domain = "dpserver6";//ConfigurationManager.ConnectionStrings["iDomain"].ConnectionString;
    string password = "Reset*123";//ConfigurationManager.ConnectionStrings["iPassword"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string ip = HttpContext.Current.Request.UserHostAddress;

            //F8BC126C0357
            BindGrid();
            showPanel(tabGeneral);

            //if (ip == "192.9.200.107")//|| macid == "F8BC126933D6"
            //{
            //    grdStyleSheet.Columns[16].Visible = true;
            //    imgAdd.Visible = true;
            //}
            //else
            //{
            //    grdStyleSheet.Columns[16].Visible = false;
            //    imgAdd.Visible = false;
            //}


        }
    }

    private DataSet Employee()
    {
	//where employee_id in (2422,2416,2056,1732,2016,978,1074,1847,2258,2432)
        return GetDataSet("select employee_id from employee", CommandType.Text);
    }

    static string GetMacAddress()
    {
        string ipAddresses = "";
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                ipAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return ipAddresses;
    }

    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabGeneral":
                if (this.hfgvJournal.Value != "")
                    lblJournalSummary.Text = "Journal : " + this.hfgvJournal.Value.Trim();
                miGeneral.Attributes.Add("class", "current");
                miJournal.Attributes.Add("class", "");
                this.tabGeneral.Visible = true;
                this.tabJournaldetails.Visible = false;
                break;

            case "tabJournaldetails":
                if (this.hfgvJournal.Value != "")
                    lblJournalSummary.Text = "Journal : " + this.hfgvJournal.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miJournal.Attributes.Add("class", "current");
                this.tabGeneral.Visible = false;
                this.tabJournaldetails.Visible = true;
                break;

            default:
                if (this.hfgvJournal.Value != "")
                    lblJournalSummary.Text = "Journal : " + this.hfgvJournal.Value.Trim();
                miGeneral.Attributes.Add("class", "current");
                miJournal.Attributes.Add("class", "");
                this.tabGeneral.Visible = true;
                this.tabJournaldetails.Visible = false;
                break;
        }
    }

    private void BindGrid()
    {
        grdStyleSheet.DataSource = GetStyleSheetItems();
        grdStyleSheet.DataBind();
    }

    private void opencon()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString;
        oConn = new SqlConnection(sConStr);
        oConn.Open();
    }

    private void closecon()
    {
        if (oConn != null)
        {
            if (oConn.State != ConnectionState.Closed)
                oConn.Close();
            oConn.Dispose();
        }
    }


    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }


    public DataSet GetStyleSheetItems()
    {
        return GetDataSet("sp_CW_QMS_Stylesheet", CommandType.StoredProcedure);
    }

    private DataSet GetDataSet(string sProcedure, CommandType sCmdType)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = sCmdType;
            oCmd.CommandText = sProcedure;
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            oCmd = null;
            return ds;
        }
        catch (Exception oEx)
        {
            return null;
        }
        finally
        {
            closecon();
        }
    }

    protected void grdStyleSheet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            string strJournal = string.Empty;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Style.Add("cursor", "pointer");
                e.Row.Attributes["onmouseover"] =
                    "javascript:setMouseOverColor(this);";
                e.Row.Attributes["onmouseout"] =
                    "javascript:setMouseOutColor(this);";



                //string AQ = DataBinder.Eval(e.Row.DataItem, "Stylesheet").ToString();
                //if (AQ.ToUpper() == "NO")
                //{
                //    e.Row.Cells[12].Enabled = false;
                //}


                int rowIndex = e.Row.RowIndex;
                strJournal = e.Row.Cells[1].Text.ToString();



                using (new Impersonator("qmsss", "dpserver6", "Reset*123"))
                {
                    //string strPathMarkup = @"//dpserver6/QET/TFJATS/" + strJournal;
                    //string strPathMarkup = @"//dpserver2/QMS/TFJATS/" + strJournal;
                    string strPathMarkup = @"\\192.9.201.209\QET\TFJATS Validation\Allen Press sample\updated\" + strJournal;


                    strPathMarkup = strPathMarkup + "\\Stylesheet";
                    if (Directory.Exists(strPathMarkup))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPathMarkup);
                        FileInfo[] aryFileInfo = sDirInfo.GetFiles();
                        if (aryFileInfo.Length > 1)
                        {
                            foreach (FileInfo sFileInfo in aryFileInfo)
                            {
                                string[] sinfo = sFileInfo.Name.Split('_');
                                string sinf = sinfo[0].ToString();
                                if (sinf == strJournal)
                                {
                                    string sFileFrmt = Path.GetExtension(sFileInfo.Name.ToString()).ToLower();
                                    if (sFileFrmt == ".doc")
                                    {
                                        ////string strRegFile = sFileInfo.Name;
                                        ////string sInputFile = strPathMarkup + "//" + strRegFile;
                                        ////if (File.Exists(sInputFile))
                                        ////{
                                        ////    ImageButton imgWrd = new ImageButton();
                                        ////    imgWrd.ImageUrl = @"/QMS/word.png";
                                        ////    e.Row.Cells[3].Controls.Add(imgWrd);
                                        ////    e.Row.Cells[3].Visible = true;
                                        ////    break;
                                        ////}
                                    }
                                    else if (sFileFrmt == ".pdf")
                                    {
                                        string strRegFile = sFileInfo.Name;
                                        string sInputFile = strPathMarkup + "\\" + strRegFile;
                                        if (File.Exists(sInputFile))
                                        {

                                           // ImageButton imgWrd = new ImageButton();
                                            //imgWrd.ImageUrl = @"/QMS/pdf.png";
                                            //e.Row.Cells[3].Controls.Add(imgWrd);
                                            e.Row.Cells[4].Visible = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        ImageButton status = e.Row.FindControl("imgStylesheet") as ImageButton;
                        status.Visible = false;
                    }

                }



                using (new Impersonator("qmsss", "dpserver6", "Reset*123"))
                {
                    //string strPathMarkup = @"//dpserver6/QET/TFJATS/" + strJournal;
                    //string strPathMarkup = @"//dpserver2/QMS/TFJATS/" + strJournal;
                    string strPathMarkup = @"\\192.9.201.209\QET\TFJATS Validation\Allen Press sample\updated\" + strJournal;


                    strPathMarkup = strPathMarkup + "\\Final";
                    if (Directory.Exists(strPathMarkup))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPathMarkup);
                        FileInfo[] aryFileInfo = sDirInfo.GetFiles();
                        if (aryFileInfo.Length > 1)
                        {
                            foreach (FileInfo sFileInfo in aryFileInfo)
                            {
                                string[] sinfo = sFileInfo.Name.Split('_');
                                string sinf = sinfo[0].ToString();
                                if (sinf == strJournal)
                                {
                                    string sFileFrmt = Path.GetExtension(sFileInfo.Name.ToString()).ToLower();
                                    if (sFileFrmt == ".doc")
                                    {
                                        ////string strRegFile = sFileInfo.Name;
                                        ////string sInputFile = strPathMarkup + "//" + strRegFile;
                                        ////if (File.Exists(sInputFile))
                                        ////{
                                        ////    ImageButton imgWrd = new ImageButton();
                                        ////    imgWrd.ImageUrl = @"/QMS/word.png";
                                        ////    e.Row.Cells[4].Controls.Add(imgWrd);
                                        ////    e.Row.Cells[4].Visible = true;
                                        ////    break;
                                        ////}
                                    }
                                    else if (sFileFrmt == ".pdf")
                                    {
                                        string strRegFile = sFileInfo.Name;
                                        string sInputFile = strPathMarkup + "\\" + strRegFile;
                                        if (File.Exists(sInputFile))
                                        {

                                            //ImageButton imgWrd = new ImageButton();
                                            //imgWrd.ImageUrl = @"/QMS/pdf.png";
                                            //e.Row.Cells[4].Controls.Add(imgWrd);
                                            e.Row.Cells[5].Visible = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        ImageButton status = e.Row.FindControl("imgSample") as ImageButton;
                        status.Visible = false;
                    }

                }

                using (new Impersonator("qmsss", "dpserver6", "Reset*123"))
                {
                    //string strPathMarkup = @"//dpserver6/QET/TFJATS/" + strJournal;
                    //string strPathMarkup = @"//dpserver2/QMS/TFJATS/" + strJournal;
                    string strPathMarkup = @"\\192.9.201.209\QET\TFJATS Validation\Allen Press sample\updated\" + strJournal;


                    strPathMarkup = strPathMarkup + "\\Markup Sample";
                    if (Directory.Exists(strPathMarkup))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPathMarkup);
                        FileInfo[] aryFileInfo = sDirInfo.GetFiles();
                        if (aryFileInfo.Length > 1)
                        {
                            foreach (FileInfo sFileInfo in aryFileInfo)
                            {
                                string[] sinfo = sFileInfo.Name.Split('_');
                                string sinf = sinfo[0].ToString();
                                if (sinf == strJournal)
                                {
                                    string sFileFrmt = Path.GetExtension(sFileInfo.Name.ToString()).ToLower();
                                    if (sFileFrmt == ".doc")
                                    {
                                        string strRegFile = sFileInfo.Name;
                                        string sInputFile = strPathMarkup + "\\" + strRegFile;
                                        if (File.Exists(sInputFile))
                                        {
                                        ////    ImageButton imgWrd = new ImageButton();
                                        ////    imgWrd.ImageUrl = @"/QMS/word.png";
                                        ////    e.Row.Cells[3].Controls.Add(imgWrd);
                                        ////    e.Row.Cells[3].Visible = true;
                                        ////    break;
                                        }
                                    }
                                    else if (sFileFrmt == ".pdf")
                                    {
                                        string strRegFile = sFileInfo.Name;
                                        string sInputFile = strPathMarkup + "\\" + strRegFile;
                                        if (File.Exists(sInputFile))
                                        {

                                            //ImageButton imgWrd = new ImageButton();
                                            //imgWrd.ImageUrl = @"/QMS/pdf.png";
                                            //e.Row.Cells[5].Controls.Add(imgWrd);
                                            e.Row.Cells[6].Visible = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        ImageButton status = e.Row.FindControl("imgMarkupSample") as ImageButton;
                        status.Visible = false;
                    }

                }

                using (new Impersonator("qmsss", "dpserver6", "Reset*123"))
                {
                    //string strPathMarkup = @"//dpserver6/QET/TFJATS/" + strJournal;
                    //string strPathMarkup = @"//dpserver2/QMS/TFJATS/" + strJournal;
                    string strPathMarkup = @"\\192.9.201.209\QET\TFJATS Validation\Allen Press sample\updated\" + strJournal;


                    strPathMarkup = strPathMarkup + "\\Specification";
                    if (Directory.Exists(strPathMarkup))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPathMarkup);
                        FileInfo[] aryFileInfo = sDirInfo.GetFiles();
                        if (aryFileInfo.Length > 1)
                        {
                            foreach (FileInfo sFileInfo in aryFileInfo)
                            {
                                string[] sinfo = sFileInfo.Name.Split('_');
                                string sinf = sinfo[0].ToString();
                                if (sinf == strJournal)
                                {
                                    string sFileFrmt = Path.GetExtension(sFileInfo.Name.ToString()).ToLower();
                                    if (sFileFrmt == ".doc")
                                    {
                                        string strRegFile = sFileInfo.Name;
                                        string sInputFile = strPathMarkup + "\\" + strRegFile;
                                        if (File.Exists(sInputFile))
                                        {
                                            ImageButton imgWrd = new ImageButton();
                                            imgWrd.ImageUrl = @"/QMS/word.png";
                                            e.Row.Cells[7].Controls.Add(imgWrd);
                                            e.Row.Cells[7].Visible = true;
                                            break;
                                        }
                                    }
                                    else if (sFileFrmt == ".pdf")
                                    {
                                        string strRegFile = sFileInfo.Name;
                                        string sInputFile = strPathMarkup + "\\" + strRegFile;
                                        if (File.Exists(sInputFile))
                                        {

                                            //ImageButton imgWrd = new ImageButton();
                                            //imgWrd.ImageUrl = @"/QMS/pdf.png";
                                            //e.Row.Cells[6].Controls.Add(imgWrd);
                                            e.Row.Cells[7].Visible = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        ImageButton status = e.Row.FindControl("imgTypeSpec") as ImageButton;
                        status.Visible = false;
                    }

                }
                /*Style Sheet End*/


            }

            
            /*Style Sheet End*/

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void imgSAMProfileWord_Click(object sender, EventArgs e)
    {

        //  open();

        open(@"C:/Users/dp0928/Desktop/Stylesheet/CAGE.Doc");
    }

    public void open(string filename)
    {
        try
        {
            string[] fname = filename.Split('.');
            string sType = fname[1].Trim().ToString();
            if (sType == "xls")
                Response.ContentType = "application/vnd.xls";
            else if (sType == "pdf")
                Response.ContentType = "application/pdf";
            else if (sType == "doc")
                Response.ContentType = "application/msword";
            else if (sType == "docx")
                Response.ContentType = "application/vnd.ms-word.document.12";
            else if (sType == "rtf")
                Response.ContentType = "application/rtf";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            Response.Charset = "";
            Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(filename));
            this.EnableViewState = false;
            Response.WriteFile(filename);
            Response.Flush();
            Response.Close();


            /* if (sType == "pdf")
             {
                 Session["fName"] = filename.ToString();
                 string[] path1 = filename.Split('/');
                 string sJourQET = path1[4].ToString();
                 string sJourcode = path1[5].ToString();
                 string sJourFold = path1[6].ToString();
                 string sJourpdf = path1[7].ToString();
                 if (sJourQET == "TFJATS")
                 {
                    // Response.Write("<script type='text/javascript'> window.open('PdfViewer.aspx?=" + sJourpdf + "','_blank'); </script>");
                     Response.Write("<script type='text/javascript'> window.open('PdfTest.aspx?=" + sJourpdf + "','_blank'); </script>");
                 }
                 else if (sJourQET.ToUpper() == "TFJATS VALIDATION")
                 {
                     string[] path2 = filename.Split('/');
                     string sJourValPDF = path1[8].ToString();
                     Response.Write("<script type='text/javascript'> window.open('PdfViewer.aspx?=" + sJourValPDF + "','_blank'); </script>");
                 }
             }
             else
             {
                 if (sType == "xls")
                     Response.ContentType = "application/vnd.xls";
                 //else if (sType == "pdf")
                 //    Response.ContentType = "application/pdf";
                 else if (sType == "doc")
                     Response.ContentType = "application/msword";
                 else if (sType == "docx")
                     Response.ContentType = "application/vnd.ms-word.document.12";
                 else if (sType == "rtf")
                     Response.ContentType = "application/rtf";
                 Response.Cache.SetCacheability(HttpCacheability.NoCache);
                 Response.Buffer = true;
                 Response.Charset = "";
                 Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(filename));
                 this.EnableViewState = false;
                 Response.WriteFile(filename);
                 Response.Flush();
                 Response.Close();
                
             }*/

        }
        catch (Exception Ex)
        {
            Alert("It is being used by another process");
        }


    }
    protected void grdStyleSheet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Stylesheet" && Page.IsValid)
        {
            using (new Impersonator(userName, domain, password))
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                string sJournalCode = grdStyleSheet.DataKeys[row.RowIndex].Values["Journal_code"].ToString();

                string sDoc, sDocx, sPdf = string.Empty;

                string strPath = @"//dpserver6/QET/TFJATS Validation/Allen Press sample/updated/" + sJournalCode + "/Stylesheet/";
                //string strPath = @"//dpserver7/CopyEditing/JOURNALS/STYLESHEETS & CHECKLISTS/" + sJournalCode;

            
                if (Directory.Exists(strPath))
                {
                    DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                    sDoc = sJournalCode + "_stylesheet" + ".doc".Trim();
                    sDocx = sJournalCode + "_stylesheet" + ".docx".Trim();
                    sPdf = sJournalCode + "_stylesheet" + ".pdf".Trim();
                    // FileInfo[] Files = sDirInfo.GetFiles("" + sJournalCode + ".doc;" + sJournalCode + ".docx;" + sJournalCode + ".pdf");
                    FileInfo[] FilesDoc = sDirInfo.GetFiles("" + sDoc + "");
                    FileInfo[] FilesDocx = sDirInfo.GetFiles("" + sDocx + "");
                    FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                    strPath = strPath + "/";
                    if (FilesDoc.Length == 0 && FilesDocx.Length == 0 && FilesPdf.Length == 0)
                    {
                        Alert("Document format mismatch");
                    }
                    foreach (FileInfo sfile in FilesDoc)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".doc")
                        {

                            string st = strPath + sJournalCode + "_stylesheet" + sFileFrmt;
                            open(st);
                        }
                    }
                    foreach (FileInfo sfile in FilesDocx)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".docx")
                        {
                            string st = strPath + sJournalCode + "_stylesheet" + sFileFrmt;
                            open(st);
                        }
                    }
                    foreach (FileInfo sfile in FilesPdf)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".pdf")
                        {
                            string st = strPath + sJournalCode + "_stylesheet" + sFileFrmt;
                            open(st);
                        }
                    }
                }
            }

        }
        else if (e.CommandName == "Sample" && Page.IsValid)
        {
            using (new Impersonator(userName, domain, password))
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                string sJournalCode = grdStyleSheet.DataKeys[row.RowIndex].Values["Journal_code"].ToString();

                string sDoc, sDocx, sPdf = string.Empty;

                string strPath = @"//dpserver6/QET/TFJATS Validation/Allen Press sample/updated/" + sJournalCode + "/Final/";
                //string strPath = @"//dpserver7/CopyEditing/JOURNALS/STYLESHEETS & CHECKLISTS/" + sJournalCode;


                if (Directory.Exists(strPath))
                {
                    DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                    sDoc = sJournalCode + "_Sample" + ".doc".Trim();
                    sDocx = sJournalCode + "_Sample" + ".docx".Trim();
                    sPdf = sJournalCode + "_Sample" + ".pdf".Trim();
                    // FileInfo[] Files = sDirInfo.GetFiles("" + sJournalCode + ".doc;" + sJournalCode + ".docx;" + sJournalCode + ".pdf");
                    FileInfo[] FilesDoc = sDirInfo.GetFiles("" + sDoc + "");
                    FileInfo[] FilesDocx = sDirInfo.GetFiles("" + sDocx + "");
                    FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                    strPath = strPath + "/";
                    if (FilesDoc.Length == 0 && FilesDocx.Length == 0 && FilesPdf.Length == 0)
                    {
                        Alert("Document format mismatch");
                    }
                    foreach (FileInfo sfile in FilesDoc)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".doc")
                        {

                            string st = strPath + sJournalCode + "_Sample" + sFileFrmt;
                            open(st);
                        }
                    }
                    foreach (FileInfo sfile in FilesDocx)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".docx")
                        {
                            string st = strPath + sJournalCode + "_Sample" + sFileFrmt;
                            open(st);
                        }
                    }
                    foreach (FileInfo sfile in FilesPdf)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".pdf")
                        {
                            string st = strPath + sJournalCode + "_Sample" + sFileFrmt;
                            open(st);
                        }
                    }
                }
            }
        }
        else if (e.CommandName == "Markup_Sample" && Page.IsValid)
        {
            using (new Impersonator(userName, domain, password))
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                string sJournalCode = grdStyleSheet.DataKeys[row.RowIndex].Values["Journal_code"].ToString();

                string sDoc, sDocx, sPdf = string.Empty;

                string strPath = @"//dpserver6/QET/TFJATS Validation/Allen Press sample/updated/" + sJournalCode + "/Markup Sample/";
                //string strPath = @"//dpserver7/CopyEditing/JOURNALS/STYLESHEETS & CHECKLISTS/" + sJournalCode;


                if (Directory.Exists(strPath))
                {
                    DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                    sDoc = sJournalCode + "_Markup Sample" + ".doc".Trim();
                    sDocx = sJournalCode + "_Markup Sample" + ".docx".Trim();
                    sPdf = sJournalCode + "_Markup Sample" + ".pdf".Trim();
                    // FileInfo[] Files = sDirInfo.GetFiles("" + sJournalCode + ".doc;" + sJournalCode + ".docx;" + sJournalCode + ".pdf");
                    FileInfo[] FilesDoc = sDirInfo.GetFiles("" + sDoc + "");
                    FileInfo[] FilesDocx = sDirInfo.GetFiles("" + sDocx + "");
                    FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                    strPath = strPath + "/";
                    if (FilesDoc.Length == 0 && FilesDocx.Length == 0 && FilesPdf.Length == 0)
                    {
                        Alert("Document format mismatch");
                    }
                    foreach (FileInfo sfile in FilesDoc)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".doc")
                        {

                            string st = strPath + sJournalCode + "_Markup Sample" + sFileFrmt;
                            open(st);
                        }
                    }
                    foreach (FileInfo sfile in FilesDocx)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".docx")
                        {
                            string st = strPath + sJournalCode + "_Markup Sample" + sFileFrmt;
                            open(st);
                        }
                    }
                    foreach (FileInfo sfile in FilesPdf)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".pdf")
                        {
                            string st = strPath + sJournalCode + "_Markup Sample" + sFileFrmt;
                            open(st);
                        }
                    }
                }
            }
        }
        else if (e.CommandName == "Type_Spce" && Page.IsValid)
        {
            using (new Impersonator(userName, domain, password))
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                string sJournalCode = grdStyleSheet.DataKeys[row.RowIndex].Values["Journal_code"].ToString();

                string sDoc, sDocx, sPdf = string.Empty;

                string strPath = @"//dpserver6/QET/TFJATS Validation/Allen Press sample/updated/" + sJournalCode + "/Specification/";
                //string strPath = @"//dpserver7/CopyEditing/JOURNALS/STYLESHEETS & CHECKLISTS/" + sJournalCode;


                if (Directory.Exists(strPath))
                {
                    DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                    sDoc = sJournalCode + "_Typespec" + ".doc".Trim();
                    sDocx = sJournalCode + "_Typespec" + ".docx".Trim();
                    sPdf = sJournalCode + "_Typespec" + ".pdf".Trim();
                    // FileInfo[] Files = sDirInfo.GetFiles("" + sJournalCode + ".doc;" + sJournalCode + ".docx;" + sJournalCode + ".pdf");
                    FileInfo[] FilesDoc = sDirInfo.GetFiles("" + sDoc + "");
                    FileInfo[] FilesDocx = sDirInfo.GetFiles("" + sDocx + "");
                    FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                    strPath = strPath + "/";
                    if (FilesDoc.Length == 0 && FilesDocx.Length == 0 && FilesPdf.Length == 0)
                    {
                        Alert("Document format mismatch");
                    }
                    foreach (FileInfo sfile in FilesDoc)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".doc")
                        {

                            string st = strPath + sJournalCode + "_Typespec" + sFileFrmt;
                            open(st);
                        }
                    }
                    foreach (FileInfo sfile in FilesDocx)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".docx")
                        {
                            string st = strPath + sJournalCode + "_Typespec" + sFileFrmt;
                            open(st);
                        }
                    }
                    foreach (FileInfo sfile in FilesPdf)
                    {
                        string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                        if (sFileFrmt == ".pdf")
                        {
                            string st = strPath + sJournalCode + "_Typespec" + sFileFrmt;
                            open(st);
                        }
                    }
                }
            }
        }

    }
    protected void grdStyleSheet_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView();
        string di = "";
        try
        {
            SortDirection sortDirection = SortDirection.Ascending;
            if (ViewState[e.SortExpression] != null)
            {
                SortDirection currDirection = (SortDirection)ViewState[e.SortExpression];
                if (currDirection == SortDirection.Ascending) sortDirection = SortDirection.Descending;
            }
            ViewState[e.SortExpression] = sortDirection;
            if (sortDirection.Equals(SortDirection.Descending)) di = " desc"; dv.Table = GetStyleSheetItems().Tables[0];
            dv.Sort = e.SortExpression + di;
            sSortExpression = e.SortExpression;
            grdStyleSheet.DataSource = dv;
            grdStyleSheet.DataBind();
        }
        catch (Exception ec)
        {
            Alert(ec.Message);
        }
        finally
        {
            dv.Dispose();
        }
    }
    protected void grdStyleSheet_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdStyleSheet.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void grdStyleSheet_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        grdStyleSheet.EditIndex = -1;
    }

    protected void lnkJournaldetails_Click(object sender, EventArgs e)
    {
        if (hfgvJournal.Value != "")
        {
            this.showPanel(tabJournaldetails);
            DataSet ds = new DataSet();
            ds = dSql.GetQMSJournalDet(hfgvJournal.Value.Trim());
        }
    }
    protected void lnkGeneral_Click(object sender, EventArgs e)
    {
        this.showPanel(tabGeneral);
    }
    protected void imgAdd_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AddJournal.aspx");
    }
}

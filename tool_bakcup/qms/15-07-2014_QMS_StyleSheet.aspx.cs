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
/*Developed by Naresh on 15-feb-2014  */


public partial class QMS_StyleSheet : System.Web.UI.Page
{
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;
    string strRegFile1;
    datasourceSQL dSql = new datasourceSQL();
    private static string sSortExpression = "";
    string userName = "dpitesting";//ConfigurationManager.ConnectionStrings["iUsername"].ConnectionString;
    string domain = "192.9.200.218";//ConfigurationManager.ConnectionStrings["iDomain"].ConnectionString;
    string password = "dpitesting";//ConfigurationManager.ConnectionStrings["iPassword"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
            showPanel(tabGeneral);
        }
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
        return GetDataSet("sp_QMS_Stylesheet", CommandType.StoredProcedure);
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Style.Add("cursor", "pointer");
                e.Row.Attributes["onmouseover"] =
                    "javascript:setMouseOverColor(this);";
                e.Row.Attributes["onmouseout"] =
                    "javascript:setMouseOutColor(this);";
                e.Row.Attributes["onclick"] =
                    "javascript:setColor(this,'" +  ((HiddenField)e.Row.FindControl("hfgvJournal")).Value.Trim() + "');";
                e.Row.Attributes["ondblclick"] =
                    "javascript:__doPostBack('lnkJournaldetails','')";
            }

               string strJournal = string.Empty;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string AQ = DataBinder.Eval(e.Row.DataItem, "AQ_Cover_Sheet_No").ToString();
                if (AQ.ToUpper() == "NO")
                {
                    e.Row.Cells[12].Enabled = false;
                }


                int rowIndex = e.Row.RowIndex;
                strJournal = e.Row.Cells[1].Text.ToString();

                // string strPath = @"//dpserver7/CopyEditing/JOURNALS/STYLESHEETS & CHECKLISTS/" + strJournal;
                //string strPath = @"//dpserver2/QMS/STYLESHEETS & CHECKLISTS/" + strJournal;

                using (new Impersonator(userName, domain, password))
                {
                    //    //string strPath = @"//192.9.200.174/qms/STYLESHEETS & CHECKLISTS/" + strJournal;
                   //string strPath = "//dpserver7//CopyEditing//JOURNALS//STYLESHEETS & CHECKLISTS//" + strJournal;
                   string strPath = @"//dpserver7//QMS//STYLESHEETS & CHECKLISTS//" + strJournal;
                    /* Style Sheet Start here */

                    if (Directory.Exists(strPath))
                    {
                        try
                        {
                            DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                            FileInfo[] aryFileInfo = sDirInfo.GetFiles();
                            if (aryFileInfo.Length > 1)
                            {
                                foreach (FileInfo sFileInfo in aryFileInfo)
                                {
                                    if (Path.GetFileNameWithoutExtension(sFileInfo.Name) == strJournal)
                                    {
                                        string sFileFrmt = Path.GetExtension(sFileInfo.Name.ToString()).ToLower();
                                        Session["strPath1"] = sFileInfo.Name;
                                        if (sFileFrmt == ".doc")
                                        {
                                            string strRegFile = sFileInfo.Name;
                                            string sInputFile = strPath + "//" + strRegFile;
                                            if (File.Exists(sInputFile))
                                            {
                                                e.Row.Cells[10].Visible = false;
                                            }
                                        }
                                        else if (sFileFrmt == ".pdf")
                                        {
                                            string strRegFile = sFileInfo.Name;
                                            string sInputFile = strPath + "//" + strRegFile;
                                            if (File.Exists(sInputFile))
                                            {

                                                ImageButton imgWrd = new ImageButton();
                                                imgWrd.ImageUrl = @"/QMS/pdf.png";
                                                e.Row.Cells[10].Controls.Add(imgWrd);
                                                e.Row.Cells[10].Visible = true;
                                                break;
                                            }
                                        }
                                        else if (sFileFrmt == ".docx")
                                        {
                                            string strRegFile = sFileInfo.Name;
                                            string sInputFile = strPath + "//" + strRegFile;
                                            if (File.Exists(sInputFile))
                                            {

                                                ImageButton imgWrd = new ImageButton();
                                                imgWrd.ImageUrl = @"/QMS/word.png";
                                                e.Row.Cells[10].Controls.Add(imgWrd);
                                                e.Row.Cells[10].Visible = true;
                                                break;
                                            }
                                        }
                                        else if (sFileFrmt == ".rtf")
                                        {
                                            string strRegFile = sFileInfo.Name;
                                            string sInputFile = strPath + "//" + strRegFile;
                                            if (File.Exists(sInputFile))
                                            {
                                                ImageButton imgWrd = new ImageButton();
                                                imgWrd.ImageUrl = @"/QMS/word.png";
                                                e.Row.Cells[10].Controls.Add(imgWrd);
                                                e.Row.Cells[10].Visible = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw Ex;
                        }
                    }
                    //else
                    //{
                    //    ImageButton status = e.Row.FindControl("imgStyleSheet") as ImageButton;
                    //    status.Visible = false;
                    //}
                }
                /*Style Sheet End*/

                using (new Impersonator(userName, domain, password))
                {
                   string strPathSAM = @"//dpserver7/QMS/SAM Profilesheets_latest/" + strJournal;
                   // string strPathSAM = @"//dpserver7//CopyEditing//JOURNALS//SAM Profilesheets_latest/" + strJournal;
                   // string strPathSAM = @"D:/Journal/" + strJournal;



                    if (Directory.Exists(strPathSAM))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPathSAM);
                        FileInfo[] aryFileInfo = sDirInfo.GetFiles();

                        if (aryFileInfo.Length != 0)
                        {
                            foreach (FileInfo sFileInfo in aryFileInfo)
                            {
                                if (Path.GetFileNameWithoutExtension(sFileInfo.Name) == strJournal)
                                {
                                    string sFileFrmt = Path.GetExtension(sFileInfo.Name.ToString()).ToLower();
                                    if (sFileFrmt == ".doc")
                                    {
                                        string strRegFile = sFileInfo.Name;
                                        string sInputFile = strPathSAM + "//" + strRegFile;
                                        if (File.Exists(sInputFile))
                                        {


                                            ////ImageButton imgWrd = new ImageButton();
                                            ////imgWrd.ImageUrl = @"/QMS/word.png";
                                            ////e.Row.Cells[8].Controls.Add(imgWrd);
                                            ////e.Row.Cells[8].Visible = true;

                                            //LinkButton lnkDoc = new LinkButton();
                                            //lnkDoc.Text = "Doc";
                                            //e.Row.Cells[8].Controls.Add(lnkDoc);
                                            //lnkDoc.CommandName = "Doc";
                                            break;
                                        }
                                    }
                                    else if (sFileFrmt == ".pdf")
                                    {
                                        string strRegFile = sFileInfo.Name;
                                        string sInputFile = strPathSAM + "//" + strRegFile;
                                        if (File.Exists(sInputFile))
                                        {

                                            ImageButton imgWrd = new ImageButton();
                                            imgWrd.ImageUrl = @"/QMS/pdf.png";
                                            e.Row.Cells[9].Controls.Add(imgWrd);
                                            e.Row.Cells[9].Visible = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ImageButton status = e.Row.FindControl("imgSAMProfile") as ImageButton;
                        status.Visible = false;
                    }


                }
                using (new Impersonator("dpitesting", "192.9.200.196", "dpitesting"))
                {
                    //string strPathMarkup = @"//dpserver6/QET/TFJATS/" + strJournal;
                    //string strPathMarkup = @"//dpserver2/QMS/TFJATS/" + strJournal;
                    string strPathMarkup = @"//192.9.200.196/QET/TFJATS/" + strJournal;


                    strPathMarkup = strPathMarkup + "/Markups";
                    if (Directory.Exists(strPathMarkup))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPathMarkup);
                        FileInfo[] aryFileInfo = sDirInfo.GetFiles();
                        if (aryFileInfo.Length > 1)
                        {
                            foreach (FileInfo sFileInfo in aryFileInfo)
                            {
                                string[] sinfo = sFileInfo.Name.Split(' ');
                                string sinf = sinfo[0].ToString();
                                if (Path.GetFileNameWithoutExtension(sFileInfo.Name) == strJournal)
                                {
                                    string sFileFrmt = Path.GetExtension(sFileInfo.Name.ToString()).ToLower();
                                    if (sFileFrmt == ".doc")
                                    {
                                        string strRegFile = sFileInfo.Name;
                                        string sInputFile = strPathMarkup + "//" + strRegFile;
                                        if (File.Exists(sInputFile))
                                        {
                                            ImageButton imgWrd = new ImageButton();
                                            imgWrd.ImageUrl = @"/QMS/word.png";
                                            e.Row.Cells[11].Controls.Add(imgWrd);
                                            e.Row.Cells[11].Visible = true;
                                            break;
                                        }
                                    }
                                    else if (sFileFrmt == ".pdf")
                                    {
                                        string strRegFile = sFileInfo.Name;
                                        string sInputFile = strPathMarkup + "//" + strRegFile;
                                        if (File.Exists(sInputFile))
                                        {

                                            ImageButton imgWrd = new ImageButton();
                                            imgWrd.ImageUrl = @"/QMS/pdf.png";
                                            e.Row.Cells[11].Controls.Add(imgWrd);
                                            e.Row.Cells[11].Visible = true;
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
            }

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
        if (e.CommandName == "Style Sheet" && Page.IsValid)
        {
            using (new Impersonator(userName, domain, password))
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                string sJournalCode = grdStyleSheet.DataKeys[row.RowIndex].Values["Journal Acronym"].ToString();

                string sDoc, sDocx, sPdf = string.Empty;

               string strPath = @"//dpserver7/QMS/STYLESHEETS & CHECKLISTS/" + sJournalCode;
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
        else if (e.CommandName == "SAM Profile" && Page.IsValid)
        {
            using (new Impersonator(userName, domain, password))
            {

            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            string sJournalCode = grdStyleSheet.DataKeys[row.RowIndex].Values["Journal Acronym"].ToString();

            string sDoc, sDocx, sPdf, sRtf = string.Empty;
            string sDocLight, sDocxLight, sPdfLight, sRtfLight = string.Empty;
           string strPath = @"//dpserver7/QMS/SAM Profilesheets_latest/" + sJournalCode;
          // string strPath = @"//dpserver7/CopyEditing/JOURNALS/SAM Profilesheets_latest/" + sJournalCode;

            if (Directory.Exists(strPath))
            {
                DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                sDoc = sJournalCode + "_SAM" + ".doc".Trim();
                sDocx = sJournalCode + "_SAM" + ".docx".Trim();
                sPdf = sJournalCode + "_SAM" + ".pdf".Trim();
                sRtf = sJournalCode + "_SAM" + ".rtf".Trim();
                sDocLight = sJournalCode + "_SAM-light" + ".doc".Trim();
                sDocxLight = sJournalCode + "_SAM-light" + ".docx".Trim();
                sPdfLight = sJournalCode + "_SAM-light" + ".pdf".Trim();
                sRtfLight = sJournalCode + "_SAM-light" + ".rtf".Trim();

                // FileInfo[] Files = sDirInfo.GetFiles("" + sJournalCode + ".doc;" + sJournalCode + ".docx;" + sJournalCode + ".pdf");
                FileInfo[] FilesDoc = sDirInfo.GetFiles("" + sDoc + "");
                FileInfo[] FilesDocx = sDirInfo.GetFiles("" + sDocx + "");
                FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                FileInfo[] Filesrtf = sDirInfo.GetFiles("" + sRtf + "");

                FileInfo[] FilesDocLight = sDirInfo.GetFiles("" + sDocLight + "");
                FileInfo[] FilesDocxLight = sDirInfo.GetFiles("" + sDocxLight + "");
                FileInfo[] FilesPdfLight = sDirInfo.GetFiles("" + sPdfLight + "");
                FileInfo[] FilesrtfLight = sDirInfo.GetFiles("" + sRtfLight + "");
                strPath = strPath + "/";
                if (FilesDoc.Length == 0 && FilesDocx.Length == 0 && FilesPdf.Length == 0 && Filesrtf.Length == 0 && FilesDocLight.Length == 0 && FilesDocxLight.Length == 0 && FilesPdfLight.Length == 0 && FilesrtfLight.Length == 0)
                {
                    Alert("Document format missmatch");
                }
                foreach (FileInfo sfile in FilesDoc)
                {
                    string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                    if (sFileFrmt == ".doc")
                    {
                        string st = strPath + sJournalCode + "_SAM" + sFileFrmt;
                        open(st);
                    }
                }
                foreach (FileInfo sfile in FilesDocx)
                {
                    string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                    if (sFileFrmt == ".docx")
                    {
                        string st = strPath + sJournalCode + "_SAM" + sFileFrmt;
                        open(st);
                    }
                }
                foreach (FileInfo sfile in FilesPdf)
                {
                    string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                    if (sFileFrmt == ".pdf")
                    {
                        string st = strPath + sJournalCode + "_SAM" + sFileFrmt;
                        open(st);
                    }
                }
                foreach (FileInfo sfile in Filesrtf)
                {
                    string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                    if (sFileFrmt == ".rtf")
                    {
                        string st = strPath + sJournalCode + "_SAM" + sFileFrmt;
                        open(st);
                    }
                }


                foreach (FileInfo sfile in FilesDocLight)
                {
                    string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                    if (sFileFrmt == ".doc")
                    {
                        string st = strPath + sJournalCode + "_SAM-light" + sFileFrmt;
                        open(st);
                    }
                }
                foreach (FileInfo sfile in FilesDocxLight)
                {
                    string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                    if (sFileFrmt == ".docx")
                    {
                        string st = strPath + sJournalCode + "_SAM-light" + sFileFrmt;
                        open(st);
                    }
                }
                foreach (FileInfo sfile in FilesPdfLight)
                {
                    string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                    if (sFileFrmt == ".pdf")
                    {
                        string st = strPath + sJournalCode + "_SAM-light" + sFileFrmt;
                        open(st);
                    }
                }
                foreach (FileInfo sfile in FilesrtfLight)
                {
                    string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                    if (sFileFrmt == ".rtf")
                    {
                        string st = strPath + sJournalCode + "_SAM-light" + sFileFrmt;
                        open(st);
                    }
                }
            }
           }
        }
        else if (e.CommandName == "Markup Sample" && Page.IsValid)
        {
            using (new Impersonator("dpitesting", "192.9.200.196", "dpitesting"))
            {
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            string sJournalCode = grdStyleSheet.DataKeys[row.RowIndex].Values["Journal Acronym"].ToString();

            string sDoc, sDocx, sPdf = string.Empty;
             string strPath = @"//dpserver6/QET/TFJATS/" + sJournalCode;
           // string strPath = @"//dpserver2/QMS/TFJATS/" + sJournalCode;

            strPath = strPath + "/Markups";
            if (Directory.Exists(strPath))
            {
                DirectoryInfo sDirInfo = new DirectoryInfo(strPath);

                sPdf = sJournalCode + " markup sample" + ".pdf".Trim();
                FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                strPath = strPath + "/";
                if (FilesPdf.Length == 0)
                {
                    Alert("Document format missmatch");
                }
                foreach (FileInfo sfile in FilesPdf)
                {
                    string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                    if (sFileFrmt == ".pdf")
                    {
                        string st = strPath + sJournalCode + " " + "markup" + " " + "sample" + sFileFrmt;
                        open(st);
                    }
                }
            }
            }
        }
        else if (e.CommandName == "AQ_Cover_Sheet_No" && Page.IsValid)
        {
            using (new Impersonator("dpitesting", "192.9.200.196", "dpitesting"))
            {
                GridViewRow row1 = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                LinkButton lnlTextName = (LinkButton)row1.FindControl("lnkAQ_Cover_Sheet_No");
                // string strPath = @"//dpi154/Users/dp0928/Desktop/QET/Approved AQ sheetsDatapage AQ cover sheets/";
                string strPath = @"//dpserver6/QET/TFJATS Validation/Approved AQ sheets/Approved AQ sheetsDatapage AQ cover sheets/";
                string sPdf = string.Empty;
                if (lnlTextName.Text.ToUpper() == "AQ COVER SHEET 1")
                {

                    if (Directory.Exists(strPath))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                        sPdf = "*" + "_AQ1.pdf".Trim();
                        FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                        strPath = strPath + "/";
                        if (FilesPdf.Length == 0)
                        {
                            Alert("Document format missmatch");
                        }
                        foreach (FileInfo sfile in FilesPdf)
                        {
                            string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                            if (sFileFrmt == ".pdf")
                            {
                                sPdf = sfile.Name;
                                string st = strPath + sPdf;
                                open(st);
                            }
                        }
                    }
                }
                else if (lnlTextName.Text.ToUpper() == "AQ COVER SHEET 2")
                {
                    if (Directory.Exists(strPath))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                        sPdf = "*" + "_AQ2.pdf".Trim();
                        FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                        strPath = strPath + "/";
                        if (FilesPdf.Length == 0)
                        {
                            Alert("Document format missmatch");
                        }
                        foreach (FileInfo sfile in FilesPdf)
                        {
                            string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                            if (sFileFrmt == ".pdf")
                            {
                                sPdf = sfile.Name;
                                string st = strPath + sPdf;
                                open(st);
                            }
                        }
                    }
                }
                else if (lnlTextName.Text.ToUpper() == "AQ COVER SHEET 3")
                {
                    if (Directory.Exists(strPath))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                        sPdf = "*" + "_AQ3.pdf".Trim();
                        FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                        strPath = strPath + "/";
                        if (FilesPdf.Length == 0)
                        {
                            Alert("Document format missmatch");
                        }
                        foreach (FileInfo sfile in FilesPdf)
                        {
                            string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                            if (sFileFrmt == ".pdf")
                            {
                                sPdf = sfile.Name;
                                string st = strPath + sPdf;
                                open(st);
                            }
                        }
                    }
                }
                else if (lnlTextName.Text.ToUpper() == "AQ COVER SHEET 4")
                {
                    if (Directory.Exists(strPath))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                        sPdf = "*" + "_AQ4.pdf".Trim();
                        FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                        strPath = strPath + "/";
                        if (FilesPdf.Length == 0)
                        {
                            Alert("Document format missmatch");
                        }
                        foreach (FileInfo sfile in FilesPdf)
                        {
                            string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                            if (sFileFrmt == ".pdf")
                            {
                                sPdf = sfile.Name;
                                string st = strPath + sPdf;
                                open(st);
                            }
                        }
                    }
                }
                else if (lnlTextName.Text.ToUpper() == "AQ COVER SHEET 5")
                {
                    if (Directory.Exists(strPath))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                        sPdf = "*" + "_AQ5.pdf".Trim();
                        FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                        strPath = strPath + "/";
                        if (FilesPdf.Length == 0)
                        {
                            Alert("Document format missmatch");
                        }
                        foreach (FileInfo sfile in FilesPdf)
                        {
                            string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                            if (sFileFrmt == ".pdf")
                            {
                                sPdf = sfile.Name;
                                string st = strPath + sPdf;
                                open(st);
                            }
                        }
                    }
                }
                else if (lnlTextName.Text.ToUpper() == "AQ COVER SHEET 6")
                {
                    if (Directory.Exists(strPath))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                        sPdf = "*" + "_AQ6.pdf".Trim();
                        FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                        strPath = strPath + "/";
                        if (FilesPdf.Length == 0)
                        {
                            Alert("Document format missmatch");
                        }
                        foreach (FileInfo sfile in FilesPdf)
                        {
                            string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                            if (sFileFrmt == ".pdf")
                            {
                                sPdf = sfile.Name;
                                string st = strPath + sPdf;
                                open(st);
                            }
                        }
                    }
                }
                else if (lnlTextName.Text.ToUpper() == "AQ COVER SHEET 7")
                {
                    if (Directory.Exists(strPath))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                        sPdf = "*" + "_AQ7.pdf".Trim();
                        FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                        strPath = strPath + "/";
                        if (FilesPdf.Length == 0)
                        {
                            Alert("Document format missmatch");
                        }
                        foreach (FileInfo sfile in FilesPdf)
                        {
                            string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                            if (sFileFrmt == ".pdf")
                            {
                                sPdf = sfile.Name;
                                string st = strPath + sPdf;
                                open(st);
                            }
                        }
                    }
                }
                else if (lnlTextName.Text.ToUpper() == "AQ COVER SHEET 8")
                {
                    if (Directory.Exists(strPath))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                        sPdf = "*" + "_AQ8.pdf".Trim();
                        FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                        strPath = strPath + "/";
                        if (FilesPdf.Length == 0)
                        {
                            Alert("Document format missmatch");
                        }
                        foreach (FileInfo sfile in FilesPdf)
                        {
                            string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                            if (sFileFrmt == ".pdf")
                            {
                                sPdf = sfile.Name;
                                string st = strPath + sPdf;
                                open(st);
                            }
                        }
                    }
                }
                else if (lnlTextName.Text.ToUpper() == "AQ COVER SHEET 9")
                {
                    if (Directory.Exists(strPath))
                    {
                        DirectoryInfo sDirInfo = new DirectoryInfo(strPath);
                        sPdf = "*" + "_AQ9.pdf".Trim();
                        FileInfo[] FilesPdf = sDirInfo.GetFiles("" + sPdf + "");
                        strPath = strPath + "/";
                        if (FilesPdf.Length == 0)
                        {
                            Alert("Document format missmatch");
                        }
                        foreach (FileInfo sfile in FilesPdf)
                        {
                            string sFileFrmt = Path.GetExtension(sfile.Name.ToString()).ToLower();
                            if (sFileFrmt == ".pdf")
                            {
                                sPdf = sfile.Name;
                                string st = strPath + sPdf;
                                open(st);
                            }
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
}

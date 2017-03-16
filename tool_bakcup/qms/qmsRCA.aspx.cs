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
using System.Data.OleDb;
using System.IO;
using System.Text;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Web.UI.WebControls.Adapters;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

public partial class qmsRCA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           Tab1.CssClass = "Clicked";
            MainView.ActiveViewIndex = 0;
        }


    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
       // Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;


    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
      //  Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;


    }
    //protected void Tab3_Click(object sender, EventArgs e)
    //{
    //    Tab1.CssClass = "Initial";
    //    Tab2.CssClass = "Initial";
    //    Tab3.CssClass = "Clicked";
    //    MainView.ActiveViewIndex = 2;


    //}
    protected void btnFileSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string connectionString = "";
            if (fileBrowse.HasFile)
            {
                string fileName = Path.GetFileName(fileBrowse.PostedFile.FileName);
                string fileExtension = Path.GetExtension(fileBrowse.PostedFile.FileName);
                string fileLocation = Server.MapPath(fileName);

                //if (!Path.GetDirectoryName(fileLocation))
                //{
                //    Directory.CreateDirectory(fileLocation);
                    
                //}
                fileBrowse.SaveAs(fileLocation);
                
                //Check whether file extension is xls or xslx 12 
                if (fileExtension == ".xls")
                {
                     connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=NO\"";
                    //connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;Format=xls\"";
                    //connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR={2};IMEX=1;Format=xls\"";
                }
                else if (fileExtension == ".xlsx")
                {
                     connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HRD=NO;IMEX=1;Format=xls\"";
                    //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HRD={2};IMEX=1;Format=xls\"";
                }
                //Create OleDB Connection and OleDb Command 23 
                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                DataTable dtExcelRecords = new DataTable();
                con.Open();
                DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                dAdapter.SelectCommand = cmd;
                dAdapter.Fill(dtExcelRecords);
                dtExcelRecords.Rows[0].Delete();
                dtExcelRecords.Rows[1].Delete();
                  dtExcelRecords.Rows[2].Delete();
                 dtExcelRecords.Rows[3].Delete();
                 dtExcelRecords.Rows[4].Delete();
                dtExcelRecords.AcceptChanges();
                con.Close();


     //            for (int i = 2; i <= rowCount; i++) {
     //dr = dt.NewRow();
     //for (int j = 1; j <= columnCount; j++) {
     //    oRng = (Range)oSheet.Cells[i, j];
     //    dr[j - 1] = oRng.Text.ToString().Trim();
     //}
//do  {
//            rowIndex = 2 + index;
//            row = dt.NewRow();
//            row[0] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2);
//            row[1] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2);
//            row[2] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 3]).Value2);
//            row[3] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 4]).Value2);
//            row[4] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 5]).Value2);
//            index++;
//            dt.Rows.Add(row);
//        }
//while (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2 != null)

                //for (int j = 0; j <= dtExcelRecords.Rows.Count - 1; j++)
                //{
                //    for (int i = 0; i <= dtExcelRecords.Rows.Count - 1; i++)
                //    {
                //        //if (i == 1)
                //        //{
                //            if ((dtExcelRecords.Rows[i][j].ToString().Trim() != "Issue ID") && (dtExcelRecords.Rows[i][j].ToString().Trim() != "Production Staff") && (dtExcelRecords.Rows[i][j].ToString().Trim() != "Production Manager") && (dtExcelRecords.Rows[i][j].ToString().Trim() != "Journal") && (dtExcelRecords.Rows[i][j].ToString().Trim() != "Manuscript / Issue") && (dtExcelRecords.Rows[i][j].ToString().Trim() != "Supplier") && (dtExcelRecords.Rows[i][j].ToString().Trim() != "(If Freelance CE, please give name)N/A") && (dtExcelRecords.Rows[i][j].ToString().Trim() != "Date feedback logged") && (dtExcelRecords.Rows[i][j].ToString().Trim() != "Type of problem"))
                //            {
                //                dtExcelRecords.Columns.RemoveAt(j);
                //              //  j++;
                //               // i = -1;
                //               // break;

                //            }
                //            i = -1;
                //            if (j <= dtExcelRecords.Columns.Count - 1)
                //            {
                //                j++;
                //            }
                //            //else
                //            //{
                //            //    break;
                //            //}
                //        //}
                //    }
                //}
                dtExcelRecords.AcceptChanges();

                for (int k = 0; k <= 10; k++)
                {
                    if(dtExcelRecords.Columns[k].ColumnName.Length==2)
                    {
                        if (k == 7)
                        {
                            dtExcelRecords.Columns[k].ColumnName = "Date feedback logged";
                            dtExcelRecords.AcceptChanges();
 
                        }
                        else if (dtExcelRecords.Rows[0][k].ToString() != "")
                        {
                            dtExcelRecords.Columns[k].ColumnName = dtExcelRecords.Rows[0][k].ToString();
                            dtExcelRecords.AcceptChanges();
                        }
                    }
                }
                dtExcelRecords.AcceptChanges();

                //for (int k = 0; k <= dtExcelRecords.Columns.Count - 1; k++)
                //{
                //    if (k >= 10)
                //    {
                //        dtExcelRecords.Columns.RemoveAt(k);
                //        dtExcelRecords.AcceptChanges();
                //    }
                //}
                int desiredSize = 9;

                while (dtExcelRecords.Columns.Count > desiredSize)
                {
                    dtExcelRecords.Columns.RemoveAt(desiredSize);
                }

                dtExcelRecords.AcceptChanges();

                dtExcelRecords.Rows[0].Delete();

                dtExcelRecords.AcceptChanges();

                


                if (dtExcelRecords.Rows.Count > 0)
                {
                    pnlSAMProfile.Visible = true;
                    //gvSAMProfileNew.Columns[1].HeaderText = dtExcelRecords.Rows[0][0].ToString();
                    //gvSAMProfileNew.Columns[2].HeaderText = dtExcelRecords.Rows[0][1].ToString();
                    //gvSAMProfileNew.Columns[3].HeaderText = dtExcelRecords.Rows[0][2].ToString();
                    //gvSAMProfileNew.Columns[4].HeaderText = dtExcelRecords.Rows[0][3].ToString();
                    //gvSAMProfileNew.Columns[5].HeaderText = dtExcelRecords.Rows[0][4].ToString();
                    //gvSAMProfileNew.Columns[6].HeaderText = dtExcelRecords.Rows[0][5].ToString();
                    //gvSAMProfileNew.Columns[7].HeaderText = dtExcelRecords.Rows[0][6].ToString();
                    //gvSAMProfileNew.Columns[8].HeaderText = dtExcelRecords.Rows[0][7].ToString();

                    gvSAMProfileNew.DataSource = dtExcelRecords;
                    gvSAMProfileNew.DataBind();
                    // gvSAMProfileNew.HeaderRow.Visible = false;
                }
               // for(int i=4;i<=dtExcelRecords.Rows.co


            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void gvSAMProfileNew_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvSAMProfileNew_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvSAMProfileNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvSAMProfileNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}

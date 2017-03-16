using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Runtime.InteropServices;
//using Microsoft.Office.Interop;
//using Microsoft.Office.Interop.Excel;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using ClosedXML.Excel;

public partial class DSR : System.Web.UI.Page
{
    datasourceIBSQL oSQL = new datasourceIBSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = oSQL.ExcProcedure("spGetDSR_Pages", null, CommandType.StoredProcedure);
            grdDSR.DataSource = ds;
            grdDSR.DataBind();
        }
    }
    protected void exportExcel_selectedcolumns_Click(object sender, ImageClickEventArgs e)
    {

        DataTable dataSet1 = new DataTable("Books");
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrIBLive1"].ToString()))
        {
            SqlCommand sqlComm = new SqlCommand("spGetBookPages_Info", conn);
            sqlComm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;
            da.Fill(dataSet1);
        }
        DataTable dt = new DataTable();
        DataTable dataSet2 = new DataTable("Journal");
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrIBLive1"].ToString()))
        {
            SqlCommand sqlComm = new SqlCommand("spGetJournalPages_Info", conn);
            sqlComm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;
            da.Fill(dataSet2);
        }

        DataTable dataSet3 = new DataTable("Summary");
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrIBLive1"].ToString()))
        {
            SqlCommand sqlComm = new SqlCommand("spGetDSR_Pages", conn);
            sqlComm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;
            da.Fill(dataSet3);
        }

        
        DataSet ds = new DataSet();
        ds.Merge(dataSet3);
        ds.Merge(dataSet2);
        ds.Merge(dataSet1);
        //ds.Tables.Add(dataSet3.Tables["Summary"].Copy());
        //ds.Tables.Add(dataSet2.Tables["Journal"].Copy());
        //ds.Tables.Add(dataSet1.Tables["Books"].Copy());

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds);
            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wb.Style.Font.Bold = true;

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename= DSR" + DateTime.Now.ToString().Trim().Replace(" ", "").Replace(":", "").Replace(@"\", "").Replace(@"/", "") + ".xlsx");

            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);

                Response.Flush();
                Response.End();
            }
        }


        //DataSetsToExcel(dataSets, "DSR" + DateTime.Now.ToString().Trim().Replace(" ", "").Replace(":", "").Replace(@"\", "").Replace(@"/", "") + ".xls");
    }
    //public void DataSetsToExcel(List<DataSet> dataSets, string fileName)
    //{
    //    Microsoft.Office.Interop.Excel.Application xlApp =
    //              new Microsoft.Office.Interop.Excel.Application();
    //    Workbook xlWorkbook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
    //    Sheets xlSheets = null;
    //    Worksheet xlWorksheet = null;

    //    foreach (DataSet dataSet in dataSets)
    //    {
    //        System.Data.DataTable dataTable = dataSet.Tables[0];
    //        int rowNo = dataTable.Rows.Count;
    //        int columnNo = dataTable.Columns.Count;
    //        int colIndex = 0;

    //        //Create Excel Sheets
    //        xlSheets = xlWorkbook.Sheets;
    //        xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
    //                       Type.Missing, Type.Missing, Type.Missing);
    //        xlWorksheet.Name = dataSet.DataSetName;

    //        //Generate Field Names
    //        foreach (DataColumn dataColumn in dataTable.Columns)
    //        {
    //            colIndex++;
    //            xlApp.Cells[1, colIndex] = dataColumn.ColumnName;
    //        }

    //        object[,] objData = new object[rowNo, columnNo];

    //        //Convert DataSet to Cell Data
    //        for (int row = 0; row < rowNo; row++)
    //        {
    //            for (int col = 0; col < columnNo; col++)
    //            {
    //                objData[row, col] = dataTable.Rows[row][col];
    //            }
    //        }

    //        //Add the Data
    //        Range range = xlWorksheet.Range[xlApp.Cells[2, 1], xlApp.Cells[rowNo + 1, columnNo]];
    //        range.Value2 = objData;

    //        //Format Data Type of Columns 
    //        colIndex = 0;
    //        foreach (DataColumn dataColumn in dataTable.Columns)
    //        {
    //            colIndex++;
    //            string format = "@";
    //            switch (dataColumn.DataType.Name)
    //            {
    //                case "Boolean":
    //                    break;
    //                case "Byte":
    //                    break;
    //                case "Char":
    //                    break;
    //                case "DateTime":
    //                    format = "dd/mm/yyyy";
    //                    break;
    //                case "Decimal":
    //                    format = "$* #,##0.00;[Red]-$* #,##0.00";
    //                    break;
    //                case "Double":
    //                    break;
    //                case "Int16":
    //                    format = "0";
    //                    break;
    //                case "Int32":
    //                    format = "0";
    //                    break;
    //                case "Int64":
    //                    format = "0";
    //                    break;
    //                case "SByte":
    //                    break;
    //                case "Single":
    //                    break;
    //                case "TimeSpan":
    //                    break;
    //                case "UInt16":
    //                    break;
    //                case "UInt32":
    //                    break;
    //                case "UInt64":
    //                    break;
    //                default: //String
    //                    break;
    //            }
    //            //Format the Column accodring to Data Type
    //            xlWorksheet.Range[xlApp.Cells[2, colIndex],
    //                  xlApp.Cells[rowNo + 1, colIndex]].NumberFormat = format;
    //        }
    //    }

    //    //Remove the Default Worksheet
    //    ((Worksheet)xlApp.ActiveWorkbook.Sheets[xlApp.ActiveWorkbook.Sheets.Count]).Delete();

    //    //Save
    //    xlWorkbook.SaveAs(@"\\192.9.201.222\dp\MIS\DSR\"+fileName,
    //        System.Reflection.Missing.Value,
    //        System.Reflection.Missing.Value,
    //        System.Reflection.Missing.Value,
    //        System.Reflection.Missing.Value,
    //        System.Reflection.Missing.Value,
    //        XlSaveAsAccessMode.xlNoChange,
    //        System.Reflection.Missing.Value,
    //        System.Reflection.Missing.Value,
    //        System.Reflection.Missing.Value,
    //        System.Reflection.Missing.Value,
    //        System.Reflection.Missing.Value);

    //    xlWorkbook.Close();
    //    xlApp.Quit();
    //    GC.Collect();
    //    Download(fileName, Server.MapPath(@"\\192.9.201.222\dp\MIS\DSR\"+ fileName));
    //}
    //public static void Download(string sFileName, string sFilePath)
    //{
    //    HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
    //    String Header = "Attachment; Filename=" + sFileName;
    //    HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
    //    System.IO.FileInfo Dfile = new System.IO.FileInfo(@"\\192.9.201.222\dp\MIS\DSR\" + sFileName);
    //    HttpContext.Current.Response.WriteFile(Dfile.FullName);
    //    HttpContext.Current.Response.End();
    //}
}
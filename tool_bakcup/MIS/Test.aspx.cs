using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        getMetaXML(360482);
    }
    public void getMetaXML(int ano)
    {
        string connetionString = null;
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataSet ds = new DataSet();
        string sql = null;

        connetionString = "server=192.9.200.222;database=dp_mis_live;uid=sa;pwd=masterkey";
        connection = new SqlConnection(connetionString);
        sql = "Select LTRIM(RTRIM(C.CUSTNAME)) AS [CUSTOMERNAME],LTRIM(RTRIM(B.JOURNAME)) [JOURNALNAME],LTRIM(RTRIM(B.JOURCODE)) JOURNALCODE,  LTRIM(RTRIM(A.AARTICLECODE))ARTICLECODE,ISNULL(LTRIM(RTRIM(A.AMNSTITLE)),'') TITLE,LTRIM(RTRIM(A.ACORRESPONDINGAUTHOR)) AUTHOR, LTRIM(RTRIM(A.AEMAIL)) AUTHOREMAIL,LTRIM(RTRIM(D.ALTERNATE_NAME))STAGE,CONVERT(VARCHAR,JJ.ACTUAL_CATSDUEDATE,101) DUEDATE," +
              "   A.AARTWORKPIECES NOOFFIGURES,CASE WHEN A.MS_PAGES=0 THEN NULL ELSE  A.MS_PAGES END  MANUSCRIPTPAGE,case when A.AREALNOOFPAGES=0 then NULL Else A.AREALNOOFPAGES end NOOFPAGES " +
              "  ,J.COMMENTS,ltrim(rtrim(A.DOINO))DOINO " +
              "  FROM ARTICLE_DP A  WITH(NOLOCK) INNER JOIN JOB_HISTORY JJ WITH(NOLOCK) ON A.JOB_HISTORY_ID=JJ.JOB_HISTORY_ID" +
              "  INNER JOIN  JOURNAL_DP B WITH(NOLOCK) ON A.JOURNO=B.JOURNO  " +
              "  INNER JOIN  CUSTOMER_DP C WITH(NOLOCK) ON B.CUSTNO=C.CUSTNO " +
              "  INNER JOIN STYPE_DP D WITH(NOLOCK) ON A.STYPENO=D.STYPENO" +
              "  LEFT OUTER JOIN JOB_COMMENT J WITH(NOLOCK) ON A.COMMENT_ID=J.COMMENT_ID " +
              "  WHERE A.ANO=" + ano + " ";

        try
        {
            connection.Open();
            adapter = new SqlDataAdapter(sql, connection);
            adapter.Fill(ds);
            connection.Close();

            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            dt.AcceptChanges();
            dt.Namespace = "http://temporg.uri";
            dt.TableName = "Data";

            var desktopFolder = @"\\192.9.200.222\dp\NatureWeb\Decker";//Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var fullFileName = System.IO.Path.Combine(desktopFolder, ds.Tables[0].Rows[0]["ARTICLECODE"].ToString() + ".xml");
            //   var fs = new FileStream(fullFileName, FileMode.Create);

            using (System.IO.FileStream fs = new System.IO.FileStream(fullFileName, System.IO.FileMode.Create))
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                sw.WriteLine("<?xml version=\"1.0\" standalone=\"yes\"?>");

                foreach (DataRow item in dt.Rows)
                {
                    sw.WriteLine("<Data>");
                    foreach (DataColumn col in dt.Columns)
                    {
                        string s = "<" + col.ColumnName + "/>";
                        if (item[col].ToString() != "0")
                        {
                            s = "<" + col.ColumnName + ">" + item[col].ToString() + "</" + col.ColumnName + ">";
                        }
                        sw.WriteLine(s);
                    }
                    sw.WriteLine("</Data>");
                }
                sw.Close();
                // Console.WriteLine("File downloaded '"+ fullFileName + "' successfully");
            }
        }
        catch (Exception ex)
        {

        }
    }
}
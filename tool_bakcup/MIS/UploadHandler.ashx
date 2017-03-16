<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public class UploadHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                string fname;
                if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                }
                fname = Path.Combine(@"\\192.9.201.222\Mail\Uploads\", DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss")+"_"+fname);
                file.SaveAs(fname);

                using (SqlConnection con = new SqlConnection("server= 192.9.201.222;database=dp_MIS_Live;uid=sa;pwd=masterkey"))
                {
                    using (SqlCommand cmd = new SqlCommand("spEmp_DocumentFiles", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Folderpath", SqlDbType.VarChar).Value = fname;
                        cmd.Parameters.Add("@FileNames", SqlDbType.VarChar).Value = file.FileName;
                        cmd.Parameters.Add("@Employee_id", SqlDbType.VarChar).Value = context.Request.UrlReferrer.Query.Replace("?empid=","").Trim();
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}

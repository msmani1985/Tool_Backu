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
//using CrystalDecisions.Enterprise;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

public partial class Emp_Attendance : System.Web.UI.Page
{
    Attendance emg = new Attendance();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string id;
        if (Convert.ToInt16(Session["employeenumber"].ToString())<10)
            id="00"+Session["employeenumber"].ToString();
        if (Convert.ToInt16(Session["employeenumber"].ToString()) <100)
            id = "0" + Session["employeenumber"].ToString();
        else
            id = Session["employeenumber"].ToString();
        ds = emg.GetEmployeeByName("spGet_AttendanceDetails", new string[,] { { "@id", id.ToString() }, { "@sdate", txtsdate.Text }, { "@edate", txtedate.Text } } , CommandType.StoredProcedure);
        DataSet ds1 = new DataSet();
        ds1 = emg.GetEmployeeByName("TotalWorkingHours", new string[,] { { "@empid", id.ToString() }, { "@sdate", txtsdate.Text }, { "@edate", txtedate.Text } }, CommandType.StoredProcedure);
         if (ds != null && ds.Tables[0].Rows.Count > 1)
        {
            grvrpt.DataSource = ds.Tables[0];
            grvrpt.DataBind();
            //EmpAtt Att = new EmpAtt();
            //ReportDocument rep = new ReportDocument();
            //CrystalReportViewer1.Visible = true;
            //try
            //{

            //    Att = emg.EmpAttDetails("spGet_AttendanceDetails", new string[,] { { "@id", id.ToString() }, { "@sdate", txtsdate.Text }, { "@edate", txtedate.Text } });
            //        DataRow r = Att.Tables[1].Rows[0];
            //        if (Att != null && Att.Tables[1].Rows.Count > 0)
            //        {
            //            rep = new ReportDocument();

            //            rep.FileName = Server.MapPath("~/EmpAttDetails.rpt");
            //            rep.SetDatabaseLogon("sa", "matrix_1");
            //            rep.SetDataSource(Att.Tables[1]);
            //            CrystalReportViewer1.ReportSource = rep;
            //            CrystalReportViewer1.DataBind();
            //            string filename = "Attendance_DDS" + id.ToString();
            //            rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, filename.Replace(' ', '_'));
            //            ////string strFile = @"\\192.9.200.222\Mail\";
            //            ////string strFileName = "Attendance_"+id.ToString()+"_" + DateTime.Now.ToString().Trim().Replace(" ", "").Replace(":", "").Replace(@"\", "").Replace(@"/", "") + ".pdf";
            //            ////strFile = strFile + @"\" + strFileName;
            //            ////rep.ExportToDisk(ExportFormatType.PortableDocFormat, strFile);
            //            ////ShowPdf1.FilePath = ConfigurationManager.ConnectionStrings["PDFFilePathLaunch"].ToString() + strFileName;
            
            //        }
                
            //}
            //catch (Exception ex)
            //{ }
            //finally
            //{
            //    //prodrpt = null; 
            //}
        }
        //if(ds1 != null)
        //{
        //    txtTotalHrs.Text = ds1.Tables[0].Rows[0]["Time_HH:MM"].ToString();
        //    txtLateMins.Text = ds1.Tables[0].Rows[0]["LateIn_HH:MM"].ToString();
        //}
    }
  
}

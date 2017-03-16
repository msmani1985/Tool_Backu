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

public partial class RFT : System.Web.UI.Page
{
    datasourceSQL DSQL = new datasourceSQL();
    DataSet rds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FromDate.Text = DateTime.Now.ToShortDateString();
            ToDate.Text = DateTime.Now.ToShortDateString();
            Label1.Visible = false;
        }
    }

    protected void ViewReport_Click(object sender, EventArgs e)
    {
        string[,] param = { 
                            { "@frmdate", FromDate.Text.Trim() +" " + "00:00:00.000" },
                            { "@todate", ToDate.Text.Trim() +" " + "23:59:59.000"},

                           };

        try
        {
            rds = DSQL.GetRFT(param, CommandType.StoredProcedure);

            rds.Tables[0].Columns.Add("TOTAL NO.REVISES").ToString();
            rds.Tables[0].Columns.Add("TOTAL NO.PREVIEW FILES").ToString();
            rds.Tables[0].Columns.Add("TOTAL").ToString();
            //Session["job_stage"] = rds;
//            RFT_Report.Caption = "<b>" + divTitle.InnerText + "  Between " + FromDate.Text + " and " + ToDate.Text + "</b>";
            Label1.Text = "<b>" + divTitle.InnerText + "  Between " + FromDate.Text + " and " + ToDate.Text + "</b>";
            if (rds == null)
            {
                div_Error.Visible = true;
                div_Error.InnerHtml = "No Records Found";
                //ibtnExcel_Export.Visible = false;
            }
            else
            {
                div_Error.Visible = false;
                FilterReport();
                //ibtnExcel_Export.Visible = true;
            }
            RFT_Report.DataSource = rds;
            RFT_Report.DataBind();
            if (RFT_Report.Rows.Count == 0)
            {
                Label1.Visible = false;
            }
            else
            {
                Label1.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Label1.Visible = false;
            div_Error.Visible = true;
            div_Error.InnerHtml = "No Records Found";
        }
    }

    protected void FilterReport()
    {
        int Revises_Count = 0;
        int Previews_Count = 0;

        for (int dscnt = 0; dscnt < rds.Tables[0].Rows.Count; dscnt++)
        {
            DataSet dst_filter = new DataSet();
            dst_filter = DSQL.Get_Filter_RFT(rds.Tables[0].Rows[dscnt]["JOB_ID"].ToString());



            for (int i = 0; i < dst_filter.Tables[0].Rows.Count; i++)
            {
                if (dst_filter.Tables[0].Rows[i]["JOB_STAGE_ID"].ToString() == "10083" || dst_filter.Tables[0].Rows[i]["JOB_STAGE_ID"].ToString() == "10084" || dst_filter.Tables[0].Rows[i]["JOB_STAGE_ID"].ToString() == "10085")
                {
                    Revises_Count += 1;
                }
                else if (dst_filter.Tables[0].Rows[i]["JOB_STAGE_ID"].ToString() == "10003" || dst_filter.Tables[0].Rows[i]["JOB_STAGE_ID"].ToString() == "10095" || dst_filter.Tables[0].Rows[i]["JOB_STAGE_ID"].ToString() == "10096" || dst_filter.Tables[0].Rows[i]["JOB_STAGE_ID"].ToString() == "10097" || dst_filter.Tables[0].Rows[i]["JOB_STAGE_ID"].ToString() == "10098" || dst_filter.Tables[0].Rows[i]["JOB_STAGE_ID"].ToString() == "10086" || dst_filter.Tables[0].Rows[i]["JOB_STAGE_ID"].ToString() == "10087" || dst_filter.Tables[0].Rows[i]["JOB_STAGE_ID"].ToString() == "10088")
                {
                    Previews_Count += 1;
                }


            }
            rds.Tables[0].Rows[dscnt]["TOTAL NO.REVISES"] = Revises_Count.ToString();
            rds.Tables[0].Rows[dscnt]["TOTAL NO.PREVIEW FILES"] = Previews_Count.ToString();
            int Total = Revises_Count + Previews_Count;
            rds.Tables[0].Rows[dscnt]["TOTAL"] = Total.ToString();
            Revises_Count = 0;
            Previews_Count = 0;
        }

        

    }

    protected void ibtnExcel_Export_Click(object sender, ImageClickEventArgs e)
    {

        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + "RFT" + ".xls");
        this.EnableViewState = false;
        System.IO.StringWriter strwriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
        HtmlForm htmlfrm = new HtmlForm();
        RFT_Report.Parent.Controls.Add(htmlfrm);
        htmlfrm.Attributes["runat"] = "server";
        //jobreceived.Controls.Remove((DropDownList)jobreceived.HeaderRow.FindControl("dd_jobstage"));
      
        htmlfrm.Controls.Add(RFT_Report);
        htmlfrm.RenderControl(txtwriter);
        Response.Write(strwriter);
        Response.End();
    }
}
        

     



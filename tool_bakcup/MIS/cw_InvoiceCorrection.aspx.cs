using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cw_InvoiceCorrection : System.Web.UI.Page
{
    string sSortExp = "";
    string sSortDir = "desc";
    static string strANO = "";
    static string strINO = "";
    static string strANO_ARTICLE = "";
    static string strJOURCODE = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Datasource_IBSQL DSIB = new Datasource_IBSQL();
        if (!Page.IsPostBack)
        {
            ViewState["sortOrder"] = "desc";
            sSortExp = "";
            sSortDir = "desc";
            DataSet oDS = new DataSet();
            if (Request.QueryString["title"] != null)
                invtitle.InnerHtml = Request.QueryString["title"].ToString();
            DataSet ds = new DataSet();
            ds = DSIB.GetDataSet("SELECT A.CUSTNO, CUSTNAME,FINSITENO FROM CUSTOMER_DP A, CUSTOMER_GROUP_DP B WHERE A.CUSTNO=B.GROUP_CUSTNO AND B.CUSTNO=10191 UNION ALL  SELECT A.CUSTNO, CUSTNAME,FINSITENO FROM CUSTOMER_DP A WHERE CUSTNO in (10066,10254) ORDER BY CUSTNAME", "CUSTOMERS", CommandType.Text);
            ddlcustomer.DataSource = ds;
            string myvalue = "10066";
            ddlcustomer.SelectedValue = myvalue.ToString();
            ddlcustomer.DataBind();
 
           
        }
    }
    protected void gvIssueCorrCW_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Datasource_IBSQL DSIB = new Datasource_IBSQL();
        DataSet ds_ARTICLE = new DataSet();
        if (ddlcustomer.SelectedValue != "10066")
        {
            if (ddlcustomer.SelectedItem.Text.Trim() == "Nature")
            {
                if (txtJobNumber.Text != "")
                {
                    divNature.Visible = true;
                    divBJMBR.Visible = false;
                    divSGM.Visible = false; 
                    divANS.Visible = false;
                    divCRM.Visible = false;
                    divNASP.Visible = false;
                    divMaryAnn.Visible = false;
                    divUkBooks.Visible = false;

                    string[] array = txtJobNumber.Text.Split(' ');
                    DataSet ds_ISSUE = new DataSet();
                    if (array.Length.ToString() == "3")
                    {
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + ' ' + txtJobNumber.Text.ToString().Split(' ')[2] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");
                    }
                    else
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");

                    if (ds_ISSUE != null && ds_ISSUE.Tables[0].Rows.Count > 0)
                    {
                        strJOURCODE = ds_ISSUE.Tables["ISSUE"].Rows[0]["JOURCODE"].ToString();
                        strINO = ds_ISSUE.Tables["ISSUE"].Rows[0]["INO"].ToString();

                        ds_ARTICLE = DSIB.ExcuteProc("select case when(Select count(*) from JOB_HISTORY where ano=a.ano and STYPENO=10002)=0 then 'CW' else 'DP' end FirstProof,cast ((case when DATEDIFF(day,b.RECEIVE_DATE,b.CATS_DUE_DATE) > 1 then '3.25' when DATEDIFF(day,b.RECEIVE_DATE,b.CATS_DUE_DATE) <= 1 then  '4.55'end) as decimal(18,2)) * a.AREALNOOFPAGES USD ,case when DATEDIFF(day,b.RECEIVE_DATE,b.CATS_DUE_DATE) > 1 then '3.25' when DATEDIFF(day,b.RECEIVE_DATE,b.CATS_DUE_DATE) <= 1 then  '4.55' end Price , A.* from article_dp a inner join JOB_HISTORY b on a.ANO=b.ANO where b.STYPENO=10002 and a.INO = " + strINO + " and b.JOB_HISTORY_ID in(Select top 1 JOB_HISTORY_ID from JOB_HISTORY where ano=a.ano and STYPENO=10002 order by JOB_HISTORY_ID  ) order by AARTICLECODE ", "ARTICLE");
                        if (ds_ARTICLE != null && ds_ARTICLE.Tables[0].Rows.Count > 0)
                        {
                            Session["ARTICLE"] = ds_ARTICLE;
                            gvIssueCorrCW.DataSource = ds_ARTICLE;
                            gvIssueCorrCW.DataBind();
                        }
                    }
                    else
                        div_Error.Visible = true;
                }
                else
                {
                    Alert("Please enter Job Number!!!");
                }
            }
            else if (ddlcustomer.SelectedItem.Text.Trim() == "SGM")
            {
                divNature.Visible = false;
                divBJMBR.Visible = false;
                divSGM.Visible = true;
                divANS.Visible = false;
                divCRM.Visible = false;
                divNASP.Visible = false;
                divMaryAnn.Visible = false;
                divUkBooks.Visible = false;

                if (txtJobNumber.Text != "")
                {
                    string[] array = txtJobNumber.Text.Split(' ');
                    DataSet ds_ISSUE = new DataSet();
                    if (array.Length.ToString() == "3")
                    {
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + ' ' + txtJobNumber.Text.ToString().Split(' ')[2] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");
                    }
                    else
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");

                    if (ds_ISSUE != null && ds_ISSUE.Tables[0].Rows.Count > 0)
                    {
                        strJOURCODE = ds_ISSUE.Tables["ISSUE"].Rows[0]["JOURCODE"].ToString();
                        strINO = ds_ISSUE.Tables["ISSUE"].Rows[0]["INO"].ToString();

                        ds_ARTICLE = DSIB.ExcuteProc("Select Ano,Articles AARTICLECODE,PageCount,'OKS' First,'OKS' Revises,'OKS' Press,'OKS' Online,PDFQA,PriceQA,XMLQA,PriceXMLQA,Vendor,PriceVendor,CustService,PriceCustService,PriceQA+PriceXMLQA+PriceVendor+PriceCustService Pricecode,isnull(PageCount,0)*(PriceQA+PriceXMLQA+PriceVendor+PriceCustService)USD from (select A.ano, A.aarticlecode Articles,a.AREALNOOFPAGES PageCount,'DP'PDFQA,0.66 PriceQA,'DP'XMLQA,0.39 PriceXMLQA,'DP' Vendor,0.51 PriceVendor,'DP' CustService,0.53 PriceCustService from article_dp a inner join JOB_HISTORY b on a.ANO=b.ANO where b.JOB_HISTORY_ID in(Select top 1 JOB_HISTORY_ID from JOB_HISTORY where ano=a.ano and STYPENO in (10002,10103) order by JOB_HISTORY_ID  ) and a.INO=" + strINO + " ) A Order by Articles ", "ARTICLE");
                        if (ds_ARTICLE != null && ds_ARTICLE.Tables[0].Rows.Count > 0)
                        {
                            Session["ARTICLE"] = ds_ARTICLE;
                            grdSGM.DataSource = ds_ARTICLE;
                            grdSGM.DataBind();
                        }
                    }
                    else
                        div_Error.Visible = true;
                }
                else
                {
                    Alert("Please enter Job Number!!!");
                }
            }
            else if (ddlcustomer.SelectedItem.Text.Trim() == "Scientific Electronic Library Online" || ddlcustomer.SelectedItem.Text.Trim() == "Clinics")
            {
                divANS.Visible = false;
                divNature.Visible = false;
                divBJMBR.Visible = true;
                divSGM.Visible = false;
                divCRM.Visible = false;
                divNASP.Visible = false;
                divMaryAnn.Visible = false;
                divUkBooks.Visible = false;

                if (txtJobNumber.Text != "")
                {
                    string[] array = txtJobNumber.Text.Split(' ');
                    DataSet ds_ISSUE = new DataSet();
                    if (array.Length.ToString() == "3")
                    {
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + ' ' + txtJobNumber.Text.ToString().Split(' ')[2] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");
                    }
                    else
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");

                    if (ds_ISSUE != null && ds_ISSUE.Tables[0].Rows.Count > 0)
                    {
                        strJOURCODE = ds_ISSUE.Tables["ISSUE"].Rows[0]["JOURCODE"].ToString();
                        strINO = ds_ISSUE.Tables["ISSUE"].Rows[0]["INO"].ToString();

                        ds_ARTICLE = DSIB.ExcuteProc(" Select Ano,Articles AARTICLECODE,PageCount,'Reverend' First,'Reverend' Revises,'Reverend' Press,'Reverend' Online, PDFQA,PriceQA,XMLQA,PriceXMLQA,Vendor,PriceVendor,DOI,PriceDOI,CustService,PriceCustService,PriceQA+PriceXMLQA+PriceVendor+PriceCustService Pricecode,(isnull(PageCount,0)*(PriceQA+PriceXMLQA+PriceVendor+PriceCustService)+PriceDOI)USD from (" +
                                                     " select A.ano, A.aarticlecode Articles,a.AREALNOOFPAGES PageCount, 'DP'PDFQA,0.39 PriceQA,'DP'XMLQA,0.39 PriceXMLQA,'DP' Vendor,0.3 PriceVendor,'DP' DOI,0.2 Pricedoi," +
                                                     " 'DP' CustService,0.31 PriceCustService from article_dp a inner join JOB_HISTORY b on a.ANO=b.ANO where b.JOB_HISTORY_ID in(Select top 1 JOB_HISTORY_ID "+
                                                     " from JOB_HISTORY where ano=a.ano   order by JOB_HISTORY_ID  ) and a.INO=" + strINO + " ) A Order by Articles ", "ARTICLE");
                        if (ds_ARTICLE != null && ds_ARTICLE.Tables[0].Rows.Count > 0)
                        {
                            Session["ARTICLE"] = ds_ARTICLE;
                            grdBJMBR.DataSource = ds_ARTICLE;
                            grdBJMBR.DataBind();
                        }
                    }
                    else
                        div_Error.Visible = true;
                }
                else
                {
                    Alert("Please enter Job Number!!!");
                }
            }
            else if (ddlcustomer.SelectedItem.Text.Trim() == "ANS")
            {
                divNature.Visible = false;
                divBJMBR.Visible = true;
                divSGM.Visible = false;
                divANS.Visible = true;
                divCRM.Visible = false;
                divNASP.Visible = false;
                divMaryAnn.Visible = false;
                divUkBooks.Visible = false;

                if (txtJobNumber.Text != "")
                {
                    string[] array = txtJobNumber.Text.Split(' ');
                    DataSet ds_ISSUE = new DataSet();
                    if (array.Length.ToString() == "3")
                    {
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + ' ' + txtJobNumber.Text.ToString().Split(' ')[2] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");
                    }
                    else
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");

                    if (ds_ISSUE != null && ds_ISSUE.Tables[0].Rows.Count > 0)
                    {
                        strJOURCODE = ds_ISSUE.Tables["ISSUE"].Rows[0]["JOURCODE"].ToString();
                        strINO = ds_ISSUE.Tables["ISSUE"].Rows[0]["INO"].ToString();

                        ds_ARTICLE = DSIB.ExcuteProc(" Select Ano,REPLACE(Articles,'NTNT','NT') AARTICLECODE,PageCount,'OKS' First,'OKS' Revises,'OKS' Press,'OKS' Online, Vendor,PriceVendor,CustService,PriceCustService,PriceVendor+PriceCustService Pricecode,(isnull(PageCount,0)*(PriceVendor+PriceCustService))USD from (" +
                                                     "   select A.ano, A.AMANUSCRIPTID Articles,a.AREALNOOFPAGES PageCount, 'DP' Vendor,0.3 PriceVendor,'DP' CustService,0.31 PriceCustService from article_dp a inner join JOB_HISTORY b on a.ANO=b.ANO where b.JOB_HISTORY_ID in(Select top 1 JOB_HISTORY_ID " +
                                                     "   from JOB_HISTORY where ano=a.ano   order by JOB_HISTORY_ID  ) and a.INO=" + strINO + " ) A Order by Articles ", "ARTICLE");
                        if (ds_ARTICLE != null && ds_ARTICLE.Tables[0].Rows.Count > 0)
                        {
                            Session["ARTICLE"] = ds_ARTICLE;
                            grdANS.DataSource = ds_ARTICLE;
                            grdANS.DataBind();
                        }
                    }
                    else
                        div_Error.Visible = true;
                }
                else
                {
                    Alert("Please enter Job Number!!!");
                }
            }
            else if (ddlcustomer.SelectedValue.ToString() == "10226" || ddlcustomer.SelectedValue.ToString() == "10236")//MediaSphere Medical,Chronic Diseases and Injuries in Canada           
            {
                divNature.Visible = false;
                divBJMBR.Visible = false;
                divSGM.Visible = false;
                divANS.Visible = false;
                divCRM.Visible = true;
                divNASP.Visible = false;
                divMaryAnn.Visible = false;
                divUkBooks.Visible = false;

                if (txtJobNumber.Text != "")
                {
                    string[] array = txtJobNumber.Text.Split(' ');
                    DataSet ds_ISSUE = new DataSet();
                    if (array.Length.ToString() == "3")
                    {
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + ' ' + txtJobNumber.Text.ToString().Split(' ')[2] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");
                    }
                    else
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");

                    if (ds_ISSUE != null && ds_ISSUE.Tables[0].Rows.Count > 0)
                    {
                        strJOURCODE = ds_ISSUE.Tables["ISSUE"].Rows[0]["JOURCODE"].ToString();
                        strINO = ds_ISSUE.Tables["ISSUE"].Rows[0]["INO"].ToString();

                        ds_ARTICLE = DSIB.ExcuteProc(" Select Ano, Articles  AARTICLECODE,PageCount,'Reverend' Preprocess ,'UK' CE, 'Reverend' First,'Reverend' Revises,'Reverend' Press,'Reverend' Online,PDFQA,PriceQA, Vendor,PriceVendor,CustService,PriceCustService,PriceVendor+PriceCustService+PriceQA Pricecode,(isnull(PageCount,0)*(PriceVendor+PriceCustService+PriceQA))USD from (" +
                                                     "   select A.ano, A.AMANUSCRIPTID Articles,a.AREALNOOFPAGES PageCount,'DP' PDFQA,0.39 PriceQA, 'DP' Vendor,0.3 PriceVendor,'DP' CustService,0.31 PriceCustService from article_dp a inner join JOB_HISTORY b on a.ANO=b.ANO where b.JOB_HISTORY_ID in(Select top 1 JOB_HISTORY_ID " +
                                                     " from JOB_HISTORY where ano=a.ano   order by JOB_HISTORY_ID  ) and a.INO=" + strINO + " ) A Order by Articles", "ARTICLE");
                        if (ds_ARTICLE != null && ds_ARTICLE.Tables[0].Rows.Count > 0)
                        {
                            Session["ARTICLE"] = ds_ARTICLE;
                            grdCRM.DataSource = ds_ARTICLE;
                            grdCRM.DataBind();
                        }
                    }
                    else
                        div_Error.Visible = true;
                }
                else
                {
                    Alert("Please enter Job Number!!!");
                }
            }
            else if (ddlcustomer.SelectedValue.ToString() == "10228")//National Association of School Psychologists                                    
            {
                divNature.Visible = false;
                divBJMBR.Visible = false;
                divSGM.Visible = false;
                divANS.Visible = false;
                divCRM.Visible = false;
                divNASP.Visible = true;
                divMaryAnn.Visible = false;
                divUkBooks.Visible = false;

                if (txtJobNumber.Text != "")
                {
                    string[] array = txtJobNumber.Text.Split(' ');
                    DataSet ds_ISSUE = new DataSet();
                    if (array.Length.ToString() == "3")
                    {
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + ' ' + txtJobNumber.Text.ToString().Split(' ')[2] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");
                    }
                    else
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");

                    if (ds_ISSUE != null && ds_ISSUE.Tables[0].Rows.Count > 0)
                    {
                        strJOURCODE = ds_ISSUE.Tables["ISSUE"].Rows[0]["JOURCODE"].ToString();
                        strINO = ds_ISSUE.Tables["ISSUE"].Rows[0]["INO"].ToString();

                        ds_ARTICLE = DSIB.ExcuteProc("select A.ano, A.AARTICLECODE Articles,a.AREALNOOFPAGES PageCount,1 Pricecode, a.AREALNOOFPAGES * 1 USD  from article_dp a inner join JOB_HISTORY b on a.ANO=b.ANO where b.JOB_HISTORY_ID in(Select top 1 JOB_HISTORY_ID" +
                                                    " from JOB_HISTORY where ano=a.ano   order by JOB_HISTORY_ID  ) and a.INO=" + strINO + " ", "ARTICLE");
                        if (ds_ARTICLE != null && ds_ARTICLE.Tables[0].Rows.Count > 0)
                        {
                           
                            grdNASP.DataSource = ds_ARTICLE;
                            grdNASP.DataBind();
                        }
                    }
                    else
                        div_Error.Visible = true;
                }
                else
                {
                    Alert("Please enter Job Number!!!");
                }
            }
            else if (ddlcustomer.SelectedValue.ToString() == "10254")//MaryAnn liebertpub                                                                    
            {
                divNature.Visible = false;
                divBJMBR.Visible = false;
                divSGM.Visible = false;
                divANS.Visible = false;
                divCRM.Visible = false;
                divNASP.Visible = false;
                divMaryAnn.Visible = true;
                divUkBooks.Visible = false;

                if (txtJobNumber.Text != "")
                {
                    string[] array = txtJobNumber.Text.Split(' ');
                    DataSet ds_ISSUE = new DataSet();
                    if (array.Length.ToString() == "3")
                    {
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + ' ' + txtJobNumber.Text.ToString().Split(' ')[2] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");
                    }
                    else
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");

                    if (ds_ISSUE != null && ds_ISSUE.Tables[0].Rows.Count > 0)
                    {
                        strJOURCODE = ds_ISSUE.Tables["ISSUE"].Rows[0]["JOURCODE"].ToString();
                        strINO = ds_ISSUE.Tables["ISSUE"].Rows[0]["INO"].ToString();

                        ds_ARTICLE = DSIB.ExcuteProc(" select A.ano, ltrim(rtrim(upper(c.JOURCODE)))+'-'+ ltrim(rtrim(a.AMANUSCRIPTID)) Articles,Count(a.ano) ArticleCount,1 Pricecode, Count(a.ano) * 1 USD  from article_dp a "+
                                                     " inner join JOB_HISTORY b on a.ANO=b.ANO inner join JOURNAL_DP c on a.JOURNO=c.JOURNO where b.JOB_HISTORY_ID in "+
                                                     " (Select top 1 JOB_HISTORY_ID from JOB_HISTORY where ano=a.ano   order by JOB_HISTORY_ID  ) and a.INO="+strINO +" group by A.ano, A.AARTICLECODE,c.jourcode,a.AMANUSCRIPTID", "ARTICLE");
                        if (ds_ARTICLE != null && ds_ARTICLE.Tables[0].Rows.Count > 0)
                        {
                            grdMaryAnn .DataSource = ds_ARTICLE;
                            grdMaryAnn.DataBind();
                        }
                    }
                    else
                        div_Error.Visible = true;
                }
                else
                {
                    Alert("Please enter Job Number!!!");
                }
            }
            else if (ddlcustomer.SelectedValue.ToString() == "10235")//UK Books                                                                    
            {
                divNature.Visible = false;
                divBJMBR.Visible = false;
                divSGM.Visible = false;
                divANS.Visible = false;
                divCRM.Visible = false;
                divNASP.Visible = false;
                divMaryAnn.Visible = false;
                divUkBooks.Visible = true;

                if (txtJobNumber.Text != "")
                {
                    string[] array = txtJobNumber.Text.Split(' ');
                    DataSet ds_ISSUE = new DataSet();
                    if (array.Length.ToString() == "3")
                    {
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + ' ' + txtJobNumber.Text.ToString().Split(' ')[2] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");
                    }
                    else
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");

                    if (ds_ISSUE != null && ds_ISSUE.Tables[0].Rows.Count > 0)
                    {
                        strJOURCODE = ds_ISSUE.Tables["ISSUE"].Rows[0]["JOURCODE"].ToString();
                        strINO = ds_ISSUE.Tables["ISSUE"].Rows[0]["INO"].ToString();

                        ds_ARTICLE = DSIB.ExcuteProc(" Select ANO,ARTICLES,TITLE, PAGECOUNT,PDFQA,PriceQA,Vendor,PriceVendor,PriceQA+PriceVendor Pricecode,PageCOUNT * (PriceQA+PriceVendor) USD  from (SELECT A.ANO,a.aarticlecode ARTICLES, "+
                                                     " LTRIM(RTRIM(isnull(a.AMNSTITLE,a.AMANUSCRIPTID)))Title ,a.AREALNOOFPAGES PageCOUNT,'DP' PDFQA,0.39 PriceQA, 'DP' Vendor,0.30 PriceVendor FROM ARTICLE_DP A "+
                                                     " INNER JOIN JOB_HISTORY B ON A.ANO=B.ANO INNER JOIN JOURNAL_DP C ON A.JOURNO=C.JOURNO WHERE B.JOB_HISTORY_ID IN (SELECT TOP 1 JOB_HISTORY_ID FROM JOB_HISTORY WHERE ANO=A.ANO   ORDER BY JOB_HISTORY_ID  ) AND A.INO="+strINO +") a ", "ARTICLE");
                        if (ds_ARTICLE != null && ds_ARTICLE.Tables[0].Rows.Count > 0)
                        {
                            grdUkBooks.DataSource = ds_ARTICLE;
                            grdUkBooks.DataBind();
                        }
                    }
                    else
                        div_Error.Visible = true;
                }
                else
                {
                    Alert("Please enter Job Number!!!");
                }
            }
            else if (ddlcustomer.SelectedItem.Text.Trim() == "Allen Press")
            {
                if (txtJobNumber.Text != "")
                {
                    divNature.Visible = true;
                    divBJMBR.Visible = false;
                    divSGM.Visible = false;
                    divANS.Visible = false;
                    divCRM.Visible = false;
                    divNASP.Visible = false;
                    divMaryAnn.Visible = false;

                    string[] array = txtJobNumber.Text.Split(' ');
                    DataSet ds_ISSUE = new DataSet();

                    string sJourId = txtJobNumber.Text.ToUpper().ToString().Split(' ')[0];
                    string aIssueNo = txtJobNumber.Text.ToString().Split(' ')[1].Replace("/", "-").ToString();

                    string issueid = sJourId.Trim() + "-" + aIssueNo.Trim() + "-" + "Press";




                    if (array.Length.ToString() == "3")
                    {
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + ' ' + txtJobNumber.Text.ToString().Split(' ')[2] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'", "ISSUE");
                    }
                    else
                    {
                        ds_ISSUE = DSIB.ExcuteProc("select i.*, j.JOURCODE, j.issam, j.iscopyedit, j.isarticle_based, j.isfpm from issue_dp i join journal_dp j on j.journo = i.journo where i.APIssueID='" + issueid + "' ", "ISSUE");// i.iissueno = '" + txtJobNumber.Text.ToString().Split(' ')[1] + "' and j.jourcode = '" + txtJobNumber.Text.ToUpper().ToString().Split(' ')[0] + "'"
                    }
                    if (ds_ISSUE != null && ds_ISSUE.Tables[0].Rows.Count > 0)
                    {
                        strJOURCODE = ds_ISSUE.Tables["ISSUE"].Rows[0]["JOURCODE"].ToString();
                        strINO = ds_ISSUE.Tables["ISSUE"].Rows[0]["INO"].ToString();

                        ds_ARTICLE = DSIB.ExcuteProc(" Select ANO,INO,Articles AARTICLECODE,PageCount AREALNOOFPAGES,PreProcess,CopyEdit,FirstProof,Revises,Press,Online,CWRevision, " +
                                                    " Case when FirstProof='CW' then 3.5 else 5 end Price,Case when FirstProof='CW' then 3.5 * PageCount else 5 * pagecount end USD "+
                                                    " from( select a.ANO,a.INO, ltrim(rtrim(upper(c.JOURCODE)))+'-'+ ltrim(rtrim(a.AMANUSCRIPTID))Articles,a.AREALNOOFPAGES PageCount,' 'PreProcess,''CopyEdit, "+
                                                    " case when(Select count(ano) from ARTICLE_DP where ano=a.ano and adno <> 3 group by STYPENO having count(ano) <1 )=0 then 'CW' else 'DP' end FirstProof, "+
                                                    " 'DP' Revises,'DP' Press,'DP'Online,' ' CWRevision from article_dp a inner join JOB_HISTORY b on a.ANO=b.ANO inner join journal_dp c on c.journo=a.journo "+
                                                    " where b.STYPENO=10002 and b.JOB_HISTORY_ID in(Select top 1 JOB_HISTORY_ID from JOB_HISTORY where ano=a.ano and STYPENO=10002 order by JOB_HISTORY_ID  ) and "+
                                                    " a.INO=" + strINO + " ) A order by Articles ", "ARTICLE");
                        if (ds_ARTICLE != null && ds_ARTICLE.Tables[0].Rows.Count > 0)
                        {
                            Session["ARTICLE"] = ds_ARTICLE;
                            gvIssueCorrCW.DataSource = ds_ARTICLE;
                            gvIssueCorrCW.DataBind();
                        }
                    }
                    else
                        div_Error.Visible = true;
                }
                else
                {
                    Alert("Please enter Job Number!!!");
                }
            }
        }
        else
        {
            Alert("Customer name should not empty");
        }
    }
    protected void gvIssueCorrCW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList oList;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                oList = (DropDownList)e.Row.Cells[6].FindControl("ddl_FirstProof");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "FirstProof").ToString();
            }
        }
        catch { }
    }
    protected void btn_Save_Article(object sender, ImageClickEventArgs e)
    {
        string strAREALNOOFPAGES = string.Empty;
        string strADNO = string.Empty;
        DataSet ds = new DataSet();
        Datasource_IBSQL DSIB = new Datasource_IBSQL();
        if (gvIssueCorrCW.Visible == true)
        {
            foreach (GridViewRow row in gvIssueCorrCW.Rows)
            {
                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtPagecnt")).Text))
                    strAREALNOOFPAGES = ((TextBox)row.FindControl("txtPagecnt")).Text;

                if (Convert.ToString(strAREALNOOFPAGES).Trim() == "")
                {
                    strAREALNOOFPAGES = "0";
                }
                strANO = row.Cells[1].Text.Trim();
                DSIB.ExcuteProc("update article_dp set  AREALNOOFPAGES=" + strAREALNOOFPAGES + " where ANO=" + strANO);
            }
            btnSubmit_Click(null, null);
            Alert("Successfully Saved");
        }
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }

    protected void btn_SGM_SaveArticle_Click(object sender, ImageClickEventArgs e)
    {
        string strAREALNOOFPAGES = string.Empty;
        string strADNO = string.Empty;
        DataSet ds = new DataSet();
        Datasource_IBSQL DSIB = new Datasource_IBSQL();
        if (grdSGM.Visible == true)
        {
            foreach (GridViewRow row in grdSGM.Rows)
            {
                strAREALNOOFPAGES = "";
                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtPagecnt")).Text))
                    strAREALNOOFPAGES = ((TextBox)row.FindControl("txtPagecnt")).Text;

                if (Convert.ToString(strAREALNOOFPAGES).Trim() == "")
                {
                    strAREALNOOFPAGES = "0";
                }
                strANO = row.Cells[1].Text.Trim();
                DSIB.ExcuteProc("update article_dp set  AREALNOOFPAGES=" + strAREALNOOFPAGES + " where ANO=" + strANO);
            }
            btnSubmit_Click(null, null);
            Alert("Successfully Saved");
        }
    }
    protected void grdSGM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList oList;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                oList = (DropDownList)e.Row.Cells[4].FindControl("ddl_FirstProof");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "First").ToString();

                oList = (DropDownList)e.Row.Cells[5].FindControl("ddl_Revises");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Revises").ToString();

                oList = (DropDownList)e.Row.Cells[6].FindControl("ddl_Press");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Press").ToString();

                oList = (DropDownList)e.Row.Cells[7].FindControl("ddl_Online");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Online").ToString();
            }
        }
        catch { }
    }
    protected void grdBJMBR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList oList;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                oList = (DropDownList)e.Row.Cells[4].FindControl("ddl_FirstProof");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "First").ToString();

                oList = (DropDownList)e.Row.Cells[5].FindControl("ddl_Revises");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Revises").ToString();

                oList = (DropDownList)e.Row.Cells[6].FindControl("ddl_Press");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Press").ToString();

                oList = (DropDownList)e.Row.Cells[7].FindControl("ddl_Online");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Online").ToString();
                
            }
        }
        catch { }
    }
    protected void btn_BJMBR_SaveArticle_Click(object sender, ImageClickEventArgs e)
    {
        string strAREALNOOFPAGES = string.Empty;
        string strADNO = string.Empty;
        DataSet ds = new DataSet();
        Datasource_IBSQL DSIB = new Datasource_IBSQL();
        if (grdBJMBR.Visible == true)
        {
            foreach (GridViewRow row in grdBJMBR.Rows)
            {
                strAREALNOOFPAGES = "";
                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtPagecnt")).Text))
                    strAREALNOOFPAGES = ((TextBox)row.FindControl("txtPagecnt")).Text;

                if (Convert.ToString(strAREALNOOFPAGES).Trim() == "")
                {
                    strAREALNOOFPAGES = "0";
                }
                strANO = row.Cells[1].Text.Trim();
                DSIB.ExcuteProc("update article_dp set  AREALNOOFPAGES=" + strAREALNOOFPAGES + " where ANO=" + strANO);
            }
            btnSubmit_Click(null, null);
            Alert("Successfully Saved");
        }
    }
    protected void btn_ANS_SaveArticle_Click(object sender, ImageClickEventArgs e)
    {
        string strAREALNOOFPAGES = string.Empty;
        string strADNO = string.Empty;
        DataSet ds = new DataSet();
        Datasource_IBSQL DSIB = new Datasource_IBSQL();
        if (grdANS.Visible == true)
        {
            foreach (GridViewRow row in grdANS.Rows)
            {
                strAREALNOOFPAGES = "";
                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtPagecnt")).Text))
                    strAREALNOOFPAGES = ((TextBox)row.FindControl("txtPagecnt")).Text;

                if (Convert.ToString(strAREALNOOFPAGES).Trim() == "")
                {
                    strAREALNOOFPAGES = "0";
                }
                strANO = row.Cells[1].Text.Trim();
                DSIB.ExcuteProc("update article_dp set  AREALNOOFPAGES=" + strAREALNOOFPAGES + " where ANO=" + strANO);
            }
            btnSubmit_Click(null, null);
            Alert("Successfully Saved");
        }
    }
    protected void grdCRM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList oList;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                oList = (DropDownList)e.Row.Cells[6].FindControl("ddl_FirstProof");
                oList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "First").ToString();
            }
        }
        catch { }
    }
    protected void btn_CRM_SaveArticle_Click(object sender, ImageClickEventArgs e)
    {
        string strAREALNOOFPAGES = string.Empty;
        string strADNO = string.Empty;
        DataSet ds = new DataSet();
        Datasource_IBSQL DSIB = new Datasource_IBSQL();
        if (grdCRM.Visible == true)
        {
            foreach (GridViewRow row in grdCRM.Rows)
            {
                strAREALNOOFPAGES = "";
                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtPagecnt")).Text))
                    strAREALNOOFPAGES = ((TextBox)row.FindControl("txtPagecnt")).Text;

                if (Convert.ToString(strAREALNOOFPAGES).Trim() == "")
                {
                    strAREALNOOFPAGES = "0";
                }
                strANO = row.Cells[1].Text.Trim();
                DSIB.ExcuteProc("update article_dp set  AREALNOOFPAGES=" + strAREALNOOFPAGES + " where ANO=" + strANO);
            }
            btnSubmit_Click(null, null);
            Alert("Successfully Saved");
        }
    }
    protected void grdNASP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btn_NASP_SaveArticle_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btn_Mary_SaveArticle_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btn_UkBooks_SaveArticle_Click(object sender, ImageClickEventArgs e)
    {

    }
}
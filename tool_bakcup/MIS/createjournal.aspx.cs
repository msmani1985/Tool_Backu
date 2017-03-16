using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


public partial class ProdInfoJL : System.Web.UI.Page
{
    datasourceIBSQL objCom = new datasourceIBSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //tblJourDet.Visible = false;
            ddlCategory.Enabled = false;
            DataSet dsCust = objCom.ProdCustomer();
            ddlAccount.DataSource = dsCust;
            ddlAccount.DataTextField = dsCust.Tables[0].Columns["custname"].ToString();
            ddlAccount.DataValueField = dsCust.Tables[0].Columns["custno"].ToString();
            ddlAccount.DataBind();

            btnSubmit.Text = "Update";
        }
    }

    protected void ddlAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCategory.Enabled = true;
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlJournalName.Visible = true;
        ddlJourCode.Visible = true;
        txtJournalName.Visible = false;
        txtJourCode.Visible = false;

        //tblJourDet.Visible = true;
        DataSet dsJourName = objCom.ProdJournal(ddlAccount.SelectedValue);

        ddlJournalName.DataSource = dsJourName;
        //ddlJournalName.DataTextField = dsJourName.Tables[0].Columns["journame"].ToString();
        //ddlJournalName.DataValueField = dsJourName.Tables[0].Columns["journo"].ToString();
        ddlJournalName.DataBind();
 

        ddlJourCode.DataSource = dsJourName;
        //ddlJourCode.DataTextField = dsJourName.Tables[0].Columns["jourcode"].ToString();
        //ddlJourCode.DataValueField = dsJourName.Tables[0].Columns["journo"].ToString();
        ddlJourCode.DataBind();
      
        ddlLoad();
    }

    public void ddlLoad()
    {
        DataSet dsJourStyle = objCom.ProdStyle();
    
        ddlJourStyle.DataSource = dsJourStyle;
        //ddlJourStyle.DataTextField = dsJourStyle.Tables[0].Columns["typestyle"].ToString();
        //ddlJourStyle.DataValueField = dsJourStyle.Tables[0].Columns["typestyleno"].ToString();
        ddlJourStyle.DataBind();
        ddlJourStyle.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dsJourCoverstock = objCom.ProdCoverStock();


        ddlCoverstock.DataSource = dsJourCoverstock;
        //ddlCoverstock.DataTextField = dsJourCoverstock.Tables[0].Columns["material"].ToString();
        //ddlCoverstock.DataValueField = dsJourCoverstock.Tables[0].Columns["covmatno"].ToString();
        ddlCoverstock.DataBind();
        ddlCoverstock.Items.Insert(0, new ListItem("-- select --", "0"));


        DataSet dsJourTrimsize = objCom.ProdTrimSize();

 
        ddlTrimSize.DataSource = dsJourTrimsize;
        //ddlTrimSize.DataTextField = dsJourTrimsize.Tables[0].Columns["trimsize"].ToString();
        //ddlTrimSize.DataValueField = dsJourTrimsize.Tables[0].Columns["pagetrimno"].ToString();
        ddlTrimSize.DataBind();
        ddlTrimSize.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dsJourPaperType = objCom.ProdPaperType();

    
        ddlPaperType.DataSource = dsJourPaperType;
        //ddlPaperType.DataTextField = dsJourPaperType.Tables[0].Columns["papertype"].ToString();
        //ddlPaperType.DataValueField = dsJourPaperType.Tables[0].Columns["papertypeno"].ToString();
        ddlPaperType.DataBind();
        ddlPaperType.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dsJourContactName = objCom.ProdContactName();
        ddlProdEditor.DataSource = dsJourContactName;
        //ddlProdEditor.DataTextField = dsJourName.Tables[0].Columns["jourcode"].ToString();
        //ddlProdEditor.DataValueField = dsJourName.Tables[0].Columns["journo"].ToString();
        ddlProdEditor.DataBind();
        ddlProdEditor.Items.Insert(0, new ListItem("-- select --", "0"));

      
        ddlSupervisor.DataSource = dsJourContactName;
        //ddlSupervisor.DataTextField = dsJourName.Tables[0].Columns["jourcode"].ToString();
        //ddlSupervisor.DataValueField = dsJourName.Tables[0].Columns["journo"].ToString();
        ddlSupervisor.DataBind();
        ddlSupervisor.Items.Insert(0, new ListItem("-- select --", "0"));



        ddlProdMgr.DataSource = dsJourContactName;
        //ddlProdMgr.DataTextField = dsJourName.Tables[0].Columns["jourcode"].ToString();
        //ddlProdMgr.DataValueField = dsJourName.Tables[0].Columns["journo"].ToString();
        ddlProdMgr.DataBind();
        ddlProdMgr.Items.Insert(0, new ListItem("-- select --", "0"));



        DataSet dsJourPrinterCode = objCom.ProdPrinterCode();

        ddlPrinterCode.DataSource = dsJourPrinterCode;
        //ddlPrinterCode.DataTextField = dsJourPrinterCode.Tables[0].Columns["printname"].ToString();
        //ddlPrinterCode.DataValueField = dsJourPrinterCode.Tables[0].Columns["printno"].ToString();
        ddlPrinterCode.DataBind();
        ddlPrinterCode.Items.Insert(0, new ListItem("-- select --", "0"));


        DataSet dsJourPressType = objCom.ProdPressType();

        ddlPressType.DataSource = dsJourPressType;
        //ddlPressType.DataTextField = dsJourPressType.Tables[0].Columns["presstypename"].ToString();
        //ddlPressType.DataValueField = dsJourPressType.Tables[0].Columns["presstypeno"].ToString();
        ddlPressType.DataBind();
        ddlPressType.Items.Insert(0, new ListItem("-- select --", "0"));


        DataSet dsJourPageStarts = objCom.ProdPageStart();
        ddlPageStarts.DataSource = dsJourPageStarts;
        //ddlPageStarts.DataTextField = dsJourPageStarts.Tables[0].Columns["psname"].ToString();
        //ddlPageStarts.DataValueField = dsJourPageStarts.Tables[0].Columns["psno"].ToString();
        ddlPageStarts.DataBind();
        ddlPageStarts.Items.Insert(0, new ListItem("-- select --", "0"));

        
        DataSet dsJourTrimCode = objCom.ProdTrimCode();
        ddlTrimCode.DataSource = dsJourTrimCode;
        //ddlTrimCode.DataTextField = dsJourTrimCode.Tables[0].Columns["trimcode"].ToString();
        //ddlTrimCode.DataValueField = dsJourTrimCode.Tables[0].Columns["pagetrimno"].ToString();
        ddlTrimCode.DataBind();
        ddlTrimCode.Items.Insert(0, new ListItem("-- select --", "0"));

        
        DataSet dsJourPageGSM = objCom.ProdPaperGSM();
        ddlPageGSM.DataSource = dsJourPageGSM;
        //ddlPageGSM.DataTextField = dsJourPageGSM.Tables[0].Columns["gsmweight"].ToString();
        //ddlPageGSM.DataValueField = dsJourPageGSM.Tables[0].Columns["papergsmno"].ToString();
        ddlPageGSM.DataBind();
        ddlPageGSM.Items.Insert(0, new ListItem("-- select --", "0"));

        int index = 1;
        for (int i = 1; i < 51; i++)
        {
     
            ddlTAT.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
            ddlUrgentTat.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
            ddlIssueTAT.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
            index++;
        }



        //ddlTAT.DataSource = dsJourName;
        //ddlTAT.DataTextField = dsJourName.Tables[0].Columns["jourcode"].ToString();
        //ddlTAT.DataValueField = dsJourName.Tables[0].Columns["journo"].ToString();
        //ddlTAT.DataBind();

        //ddlUrgentTat.DataSource = dsJourName;
        //ddlUrgentTat.DataTextField = dsJourName.Tables[0].Columns["jourcode"].ToString();
        //ddlUrgentTat.DataValueField = dsJourName.Tables[0].Columns["journo"].ToString();
        //ddlUrgentTat.DataBind();

        //ddlIssueTAT.DataSource = dsJourName;
        //ddlIssueTAT.DataTextField = dsJourName.Tables[0].Columns["jourcode"].ToString();
        //ddlIssueTAT.DataValueField = dsJourName.Tables[0].Columns["journo"].ToString();
        //ddlIssueTAT.DataBind();
    }

    protected void ddlJournalName_SelectedIndexChanged(object sender, EventArgs e)
    {

      

        DataSet ds = new DataSet();
        if(Convert.ToInt32(ddlJournalName.SelectedValue) !=0 )
        {
            ds = objCom.GetJournalInfo(ddlJournalName.SelectedValue);
            

            ddlJourCode.SelectedValue = ds.Tables[0].Rows[0]["journo"].ToString();

            DataRow row = ds.Tables[0].Rows[0];
            if (ds.Tables[0].Rows[0]["jourcode"].ToString() != null)
                ddlJourCode.SelectedItem.Text = ds.Tables[0].Rows[0]["jourcode"].ToString();
            if (ds.Tables[0].Rows[0]["covmatno"].ToString() != "0")
             ddlCoverstock.SelectedValue = ds.Tables[0].Rows[0]["covmatno"].ToString();  
            if (ds.Tables[0].Rows[0]["papergsmno"].ToString() != "0")
                ddlPageGSM.SelectedValue = ds.Tables[0].Rows[0]["papergsmno"].ToString();
            if (ds.Tables[0].Rows[0]["pagetrimno"].ToString() != "0")
                ddlTrimCode.SelectedValue = ds.Tables[0].Rows[0]["pagetrimno"].ToString();
            if (ds.Tables[0].Rows[0]["psno"].ToString() != "0")
                ddlPageStarts.SelectedValue = ds.Tables[0].Rows[0]["psno"].ToString();
            if (ds.Tables[0].Rows[0]["typestyleno"].ToString() != "0")
                ddlJourStyle.SelectedValue = ds.Tables[0].Rows[0]["typestyleno"].ToString();
            if (ds.Tables[0].Rows[0]["pe"].ToString() != "0")
                ddlProdEditor.SelectedValue = ds.Tables[0].Rows[0]["pe"].ToString();
            if (ds.Tables[0].Rows[0]["presstypeno"].ToString() != "")
                ddlPressType.SelectedValue = ds.Tables[0].Rows[0]["presstypeno"].ToString();
            if (ds.Tables[0].Rows[0]["pm"].ToString() != "")
               ddlProdMgr.SelectedValue = ds.Tables[0].Rows[0]["pm"].ToString();  
            if (ds.Tables[0].Rows[0]["papertypeno"].ToString() != "0")
               ddlPaperType.SelectedValue = ds.Tables[0].Rows[0]["papertypeno"].ToString();
            if (ds.Tables[0].Rows[0]["sv"].ToString() != "0")
               ddlSupervisor.SelectedValue = (ds.Tables[0].Rows[0]["sv"]).ToString();
            ddlTAT.SelectedValue = ds.Tables[0].Rows[0]["jourschdays"].ToString();
            ddlUrgentTat.SelectedValue = ds.Tables[0].Rows[0]["jourschurgentdays"].ToString();
            ddlIssueTAT.SelectedValue = ds.Tables[0].Rows[0]["jourissuedays"].ToString();
            ddlTrimSize.SelectedValue = ds.Tables[0].Rows[0]["pagetrimno"].ToString();
            if (ds.Tables[0].Rows[0]["pr"].ToString() != "0")
            ddlPrinterCode.SelectedValue = ds.Tables[0].Rows[0]["pr"].ToString();
            if (ds.Tables[0].Rows[0]["issensitive"] == "0")
                ddlSensitiveJournal.SelectedValue = "0";
            else
                ddlSensitiveJournal.SelectedValue = "1";
            ddlDataformat.SelectedValue = ds.Tables[0].Rows[0]["catno"].ToString();



            txtSupervisor.Text = ds.Tables[0].Rows[0]["svmail"].ToString();
            txtProdEds.Text = ds.Tables[0].Rows[0]["pemail"].ToString();
            txtComments.Text = ds.Tables[0].Rows[0]["jcomments"].ToString();
            txtPriceCode.Text = ds.Tables[0].Rows[0]["copyedit_pcode"].ToString();
            txtCreatedOn.Text = ds.Tables[0].Rows[0]["jourcreationdate"].ToString();
            txtIssnOnline.Text = ds.Tables[0].Rows[0]["jouronlineissn"].ToString();
            txtIssnPrint.Text = ds.Tables[0].Rows[0]["jourissn"].ToString();

            txtVolNo.Text = ds.Tables[0].Rows[0]["volno"].ToString();
            txtVolYear.Text = ds.Tables[0].Rows[0]["volyr"].ToString();
         


            /* jcode		= rsDetails("jourcode")
				pe 		= rsDetails("pe")
				pemail 		= rsDetails("pemail")
				svmail 		= rsDetails("svmail")
				sv 		= rsDetails("sv")
				pm 		= rsDetails("pm")
				'mc 		= rsDetails("mc")
				pr 		= rsDetails("pr")
				volno 		= rsDetails("iissueno")
				If Not ISNULL(volno) And INSTR(volno,"/") > 0 Then volno = Left(volno,INSTR(volno,"/")-1)
				volyr 		= rsDetails("iduedate")
				If Not ISNULL(volyr) Then volyr = Year(CDATE(volyr))
				trimsize 	= rsDetails("pagetrimno")
				trimcode 	= rsDetails("pagetrimno")
				'covgsmno 	= rsDetails("covgsmno")
				issensitive 	= rsDetails("issensitive")
				papergsmno 	= rsDetails("papergsmno")
				papertypeno 	= rsDetails("papertypeno")
				presstypeno 	= rsDetails("presstypeno")
				covmatno 	= rsDetails("covmatno")
				'jcovpreprint 	= rsDetails("jcovpreprint")
				'jlitho 		= rsDetails("jlitho")
				'jdigital 	= rsDetails("jdigital")
				'subjcatno 	= rsDetails("subjcatno")
				typestyleno 	= rsDetails("typestyleno")
				jissuebudget 	= rsDetails("jissuebudget")
				'jpagebudget	= Eval(rsDetails("jpagebudget_" & curyear))
				'jpagebudget_prev = Eval(rsDetails("jpagebudget_" & (curyear-1)))
				'jpagebudget_next = Eval(rsDetails("jpagebudget_" & (curyear+1)))
				'If jissuebudget = "" Then jissuebudget 	= 0
				'If jpagebudget 	    = "" Then jpagebudget 	= 0
				'If jpagebudget_prev = "" Then jpagebudget_prev 	= 0
				'If jpagebudget_next = "" Then jpagebudget_next 	= 0
				psno 		= rsDetails("psno")
				jourschdays 	= rsDetails("jourschdays")
				jourschurgentdays 	= rsDetails("jourschurgentdays")
				jourissuedays 	= rsDetails("jourissuedays")
				If rsDetails("jcomments").actualsize > 0 Then 
					jcomments = rsDetails.fields("jcomments").GetChunk(rsDetails("jcomments").actualsize)
				End If 
				jourissn 	= rsDetails("jourissn")
				jouronlineissn 	= rsDetails("jouronlineissn")
				'jregion 	= rsDetails("jregion")
				'issociety 	= rsDetails("issociety")
				'societyname 	= Trim(rsDetails("societyname"))
				catno 		= rsDetails("catno")
				'jcno 		= rsDetails("jcno_2007")	\\Royson 4 jan 2009
				jcno 		= rsDetails("jcno_"&curyear)
				'pdfno 		= rsDetails("pdfno")
				'pdfno1 		= rsDetails("pdfno")
				'jourdoi 	= rsDetails("jourdoi")
				jourcreationdate = FormatDateStr(rsDetails("jourcreationdate"),2)				
				
				If IsNull(pe) Then pe = "0"
				If IsNull(sv) Then sv = "0"
				If IsNull(pm) Then pm = "0"
				'If IsNull(mc) Then mc = "0"
				If IsNull(pr) Then pr = "0"
				If IsNull(trimsize) Then trimsize = "0"
				If IsNull(trimcode) Then trimcode = "0"
				'If IsNull(covgsmno) Then covgsmno = "0"
				If IsNull(papergsmno) Then papergsmno = "0"
				If IsNull(papertypeno) Then papertypeno = "0"
				If IsNull(presstypeno) Then presstypeno = "0"
				If IsNull(covmatno) Then covmatno = "0"
				'If IsNull(subjcatno) Then subjcatno = "0"
				If IsNull(typestyleno) Then typestyleno = "0"
				'If IsNull(jregion) Then jregion = "0"
				If IsNull(jcno) Then jcno = "0"
				if IsNull(rsDetails("copyedit_pcode")) then 
					copy_pcode = "0"
				else
					copy_pcode = rsDetails("copyedit_pcode")
				end if
				
				'If issociety = "Y" Then 
					'issociety = "1"
				'ElseIf issociety = "N" Then 
					'issociety = "2"
				'Else
					'issociety = "0"
				'End If
				'If IsNull(pdfno) Then pdfno = "0"*/
        }
    }
    protected void ddlJourCode_SelectedIndexChanged(object sender, EventArgs e)
    {

 

        DataSet ds = new DataSet();
        if (Convert.ToInt32(ddlJourCode.SelectedValue) != 0)
        {
            ds = objCom.GetJournalInfo(ddlJourCode.SelectedValue);

            ddlJournalName.SelectedValue = ds.Tables[0].Rows[0]["journo"].ToString();

            DataRow row = ds.Tables[0].Rows[0];
            if (ds.Tables[0].Rows[0]["jourcode"].ToString() != null)
                ddlJourCode.SelectedItem.Text = ds.Tables[0].Rows[0]["jourcode"].ToString();

            if (ds.Tables[0].Rows[0]["covmatno"].ToString() != "0" || ds.Tables[0].Rows[0]["covmatno"] != null || ds.Tables[0].Rows[0]["covmatno"].ToString() != "")
                ddlCoverstock.SelectedValue = ds.Tables[0].Rows[0]["covmatno"].ToString();

            if (ds.Tables[0].Rows[0]["papergsmno"].ToString() != "0" || ds.Tables[0].Rows[0]["papergsmno"] != null || ds.Tables[0].Rows[0]["papergsmno"].ToString() != "")
                ddlPageGSM.SelectedValue = ds.Tables[0].Rows[0]["papergsmno"].ToString();

            if (ds.Tables[0].Rows[0]["pagetrimno"].ToString() != "0" || ds.Tables[0].Rows[0]["pagetrimno"] != null || ds.Tables[0].Rows[0]["pagetrimno"].ToString() != "")
                ddlTrimCode.SelectedValue = ds.Tables[0].Rows[0]["pagetrimno"].ToString();

            if (ds.Tables[0].Rows[0]["psno"].ToString() != "0" || ds.Tables[0].Rows[0]["psno"] != null || ds.Tables[0].Rows[0]["psno"].ToString() != "")
                ddlPageStarts.SelectedValue = ds.Tables[0].Rows[0]["psno"].ToString();

            if (ds.Tables[0].Rows[0]["typestyleno"].ToString() != "0" || ds.Tables[0].Rows[0]["typestyleno"] != null || ds.Tables[0].Rows[0]["typestyleno"].ToString() != "")
                ddlJourStyle.SelectedValue = ds.Tables[0].Rows[0]["typestyleno"].ToString();

            if (ds.Tables[0].Rows[0]["pe"].ToString() != "0" || ds.Tables[0].Rows[0]["pe"] != null || ds.Tables[0].Rows[0]["pe"].ToString() != "")
                ddlProdEditor.SelectedValue = ds.Tables[0].Rows[0]["pe"].ToString();

            if (ds.Tables[0].Rows[0]["presstypeno"].ToString() != null)
                ddlPressType.SelectedValue = ds.Tables[0].Rows[0]["presstypeno"].ToString();

            if (ds.Tables[0].Rows[0]["pm"].ToString() != "0")
                ddlProdMgr.SelectedValue = ds.Tables[0].Rows[0]["pm"].ToString();

            if (ds.Tables[0].Rows[0]["papertypeno"].ToString() != "0" || ds.Tables[0].Rows[0]["papertypeno"] != null || ds.Tables[0].Rows[0]["papertypeno"].ToString() != "")
                ddlPaperType.SelectedValue = ds.Tables[0].Rows[0]["papertypeno"].ToString();

            if (ds.Tables[0].Rows[0]["sv"].ToString() != "0" || ds.Tables[0].Rows[0]["sv"] != null || ds.Tables[0].Rows[0]["sv"].ToString() != "")
                ddlSupervisor.SelectedValue = (ds.Tables[0].Rows[0]["sv"]).ToString();

            ddlTAT.SelectedValue = ds.Tables[0].Rows[0]["jourschdays"].ToString();
            ddlUrgentTat.SelectedValue = ds.Tables[0].Rows[0]["jourschurgentdays"].ToString();
            ddlIssueTAT.SelectedValue = ds.Tables[0].Rows[0]["jourissuedays"].ToString();
            ddlTrimSize.SelectedValue = ds.Tables[0].Rows[0]["pagetrimno"].ToString();
            if (ds.Tables[0].Rows[0]["pr"].ToString() != "0" || ds.Tables[0].Rows[0]["pr"] != null || ds.Tables[0].Rows[0]["pr"].ToString() != "")
                ddlPrinterCode.SelectedValue = ds.Tables[0].Rows[0]["pr"].ToString();
            if (ds.Tables[0].Rows[0]["issensitive"] == "0" )
                ddlSensitiveJournal.SelectedValue = "0";
            else
                ddlSensitiveJournal.SelectedValue = "1";
            ddlDataformat.SelectedValue = ds.Tables[0].Rows[0]["catno"].ToString();



            txtSupervisor.Text = ds.Tables[0].Rows[0]["svmail"].ToString();
            txtProdEds.Text = ds.Tables[0].Rows[0]["pemail"].ToString();
            txtComments.Text = ds.Tables[0].Rows[0]["jcomments"].ToString();
            txtPriceCode.Text = ds.Tables[0].Rows[0]["copyedit_pcode"].ToString();
            txtCreatedOn.Text = ds.Tables[0].Rows[0]["jourcreationdate"].ToString();
            txtIssnOnline.Text = ds.Tables[0].Rows[0]["jouronlineissn"].ToString();
            txtIssnPrint.Text = ds.Tables[0].Rows[0]["jourissn"].ToString();
            txtVolNo.Text = ds.Tables[0].Rows[0]["volno"].ToString();
            txtVolYear.Text = ds.Tables[0].Rows[0]["volyr"].ToString();
            txtPriceCode.Text = ds.Tables[0].Rows[0]["jcno"].ToString();
            txtCurrPageBudget.Text = ds.Tables[0].Rows[0]["jpagebudget"].ToString();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet sds = new DataSet();

      /*  string[] aJournalDetails;
        aJournalDetails = new string[32];
        aJournalDetails[0] = txtJournalName.Text; //ddlJournalName.SelectedItem.Text.Trim();
        aJournalDetails[1] = txtJourCode.Text;//ddlJourCode.SelectedItem.Text.Trim();
        aJournalDetails[2] = ddlJourStyle.SelectedValue.Trim();
        aJournalDetails[3] = txtPrevPageBudget.Text.Trim();
        aJournalDetails[4] = ddlCoverstock.SelectedValue.Trim();
        aJournalDetails[5] = ddlTrimSize.SelectedValue.Trim();

        aJournalDetails[6] = ddlPaperType.SelectedValue.Trim();
        aJournalDetails[7] = ddlProdEditor.SelectedValue.Trim();
        aJournalDetails[8] = ddlSupervisor.SelectedValue.Trim();
        aJournalDetails[9] = ddlProdMgr.SelectedValue.Trim();
        aJournalDetails[10] = txtSAMPriceCode.Text.Trim();

        aJournalDetails[11] = ddlPrinterCode.SelectedValue.Trim();
        aJournalDetails[12] = txtIssnPrint.Text.Trim();
        aJournalDetails[13] = txtIssnOnline.Text.Trim();
        aJournalDetails[14] = ddlPressType.SelectedValue.Trim();
        aJournalDetails[15] = txtComments.Text;

        aJournalDetails[16] = txtVolYear.Text.Trim();
        aJournalDetails[17] = txtVolNo.Text.Trim();
        aJournalDetails[18] = txtCurrPageBudget.Text.Trim();
        aJournalDetails[19] = txtNextPageBudget.Text.Trim();
        aJournalDetails[20] = ddlPageStarts.SelectedValue.Trim();

        aJournalDetails[21] = ddlTrimCode.SelectedValue.Trim();
        aJournalDetails[22] = ddlPageGSM.SelectedValue.Trim();
        aJournalDetails[23] = txtProdEds.Text.Trim();
        aJournalDetails[24] = txtSupervisor.Text.Trim();
        aJournalDetails[25] = ddlDataformat.SelectedValue.Trim();

        aJournalDetails[26] = ddlTAT.SelectedValue.Trim();
        aJournalDetails[27] = txtPriceCode.Text.Trim();
        aJournalDetails[28] = ddlSensitiveJournal.SelectedValue.Trim();
        aJournalDetails[29] = ddlUrgentTat.SelectedValue.Trim();
        aJournalDetails[30] = ddlIssueTAT.SelectedValue.Trim();
        aJournalDetails[31] =  ddlAccount.SelectedValue.Trim();

        //string msg = objCom.InsertProdJournal(aJournalDetails);

        //Alert(msg);
        */
        if(btnSubmit.Text=="Update")
        {
            string[,] sJournalupdate ={
                                    {"@journo",ddlJourCode.SelectedValue.Trim()},
                                    {"@jourcode",ddlJourCode.SelectedItem.Text.Trim()},
                                    {"@typestyleno",ddlJourStyle.SelectedValue.Trim()},
                                    {"@jpagebudget_prev ",txtPrevPageBudget.Text.Trim()},{"@covmatno",ddlCoverstock.SelectedValue.Trim()},
                                    {"@papertypeno", ddlPaperType.SelectedValue.Trim()},
                                    {"@pe",ddlProdEditor.SelectedValue.Trim()},{"@sv", ddlSupervisor.SelectedValue.Trim()},
                                    {"@pm",ddlProdMgr.SelectedValue.Trim()},{"@copyedit_pcode",txtSAMPriceCode.Text.Trim()},
                                    {"@printno",ddlPrinterCode.SelectedValue.Trim()},{"@jourissn", txtIssnPrint.Text.Trim()},
                                    {"@jouronlineissn",txtIssnOnline.Text.Trim()},{"@presstypeno",ddlPressType.SelectedValue.Trim()},
                                    {"@jcomments", txtComments.Text},{"@psno",ddlPageStarts.SelectedValue.Trim()},{"@pagetrimno", ddlTrimCode.SelectedValue.Trim()},
                                    {"@papergsmno", ddlPageGSM.SelectedValue.Trim()},
                                    {"@catno",ddlDataformat.SelectedValue.Trim()},{"@jourschdays",ddlTAT.SelectedValue.Trim()},{"@jcno",txtPriceCode.Text.Trim()},
                                    {"@issensitive",ddlSensitiveJournal.SelectedValue.Trim()},{"@jourschurgentdays", ddlUrgentTat.SelectedValue.Trim()},
                                    {"@jourissuedays",ddlIssueTAT.SelectedValue.Trim()},
                                    {"@custno",ddlAccount.SelectedValue.Trim()} };
            sds = objCom.ExcProcedurePrdJL("spUpdate_Journal", sJournalupdate, CommandType.StoredProcedure);

        }
        else if(btnSubmit.Text=="Add")
        {
            if(ddlTAT.SelectedValue =="0")
            {
                Alert("Please select TAT ");
            }
            else if (ddlUrgentTat.SelectedValue == "0")
            {
                Alert("Please select Urgent TAT");
            }
            else if (ddlIssueTAT.SelectedValue == "0")
            {
                Alert("Please select Issue TAT");
            }
            else
            {
                string[,] sJournalInset ={{"@journame",txtJournalName.Text},{"@jourcode",txtJourCode.Text},{"@typestyleno",ddlJourStyle.SelectedValue.Trim()},
                                 {"@jpagebudget_prev ",txtPrevPageBudget.Text.Trim()},{"@covmatno",ddlCoverstock.SelectedValue.Trim()},
                                 {"@pagetrimnosize",ddlTrimSize.SelectedValue.Trim()},{"@papertypeno", ddlPaperType.SelectedValue.Trim()},
                                 {"@pe",ddlProdEditor.SelectedValue.Trim()},{"@sv", ddlSupervisor.SelectedValue.Trim()},
                                 {"@pm",ddlProdMgr.SelectedValue.Trim()},{"@copyedit_pcode",txtSAMPriceCode.Text.Trim()},
                                 {"@printno",ddlPrinterCode.SelectedValue.Trim()},{"@jourissn", txtIssnPrint.Text.Trim()},
                                 {"@jouronlineissn",txtIssnOnline.Text.Trim()},{"@presstypeno",ddlPressType.SelectedValue.Trim()},
                                 {"@jcomments", txtComments.Text},{"@volyr",txtVolYear.Text.Trim()},{"@volno",txtVolNo.Text.Trim()},
                                 {"@jpagebudget",txtCurrPageBudget.Text.Trim()},{"@jpagebudget_next", txtNextPageBudget.Text.Trim()},
                                 {"@psno",ddlPageStarts.SelectedValue.Trim()},{"@pagetrimno", ddlTrimCode.SelectedValue.Trim()},
                                 {"@papergsmno", ddlPageGSM.SelectedValue.Trim()},{"@pemail",txtProdEds.Text.Trim()},{"@svmail",txtSupervisor.Text.Trim()},
                                 {"@catno",ddlDataformat.SelectedValue.Trim()},{"@jourschdays",ddlTAT.SelectedValue.Trim()},{"@jcno",txtPriceCode.Text.Trim()},
                                 {"@issensitive",ddlSensitiveJournal.SelectedValue.Trim()},{"@jourschurgentdays", ddlUrgentTat.SelectedValue.Trim()},
                                 {"@jourissuedays",ddlIssueTAT.SelectedValue.Trim()},{"@custno", ddlAccount.SelectedValue.Trim()}};

                sds = objCom.ExcProcedurePrdJL("spADD_Journal", sJournalInset, CommandType.StoredProcedure);
                
            }
        }
         
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void cmd_New_Article_Click(object sender, ImageClickEventArgs e)
    {
        CleadFields();
        ddlJournalName.Visible = true;
        ddlJourCode.Visible = true;
        txtJournalName.Visible = true;
        txtJourCode.Visible = true;
        ddlJournalName.SelectedValue = "0";
        btnSubmit.Text = "Add";
    }

    public void CleadFields()
    {
        txtJournalName.Text = "";
        txtJourCode.Text = "";
        ddlJourStyle.SelectedValue = "0";
        txtPrevPageBudget.Text = "";
        ddlCoverstock.SelectedValue = "0";
        ddlTrimSize.SelectedValue = "0";

        ddlPaperType.SelectedValue = "0";
        ddlProdEditor.SelectedValue = "0";
        ddlSupervisor.SelectedValue = "0";
        ddlProdMgr.SelectedValue = "0";
        txtSAMPriceCode.Text = "";

        ddlPrinterCode.SelectedValue = "0";
        txtIssnPrint.Text = "";
        txtIssnOnline.Text = "";
        ddlPressType.SelectedValue = "0";
        txtComments.Text = "";

        txtVolYear.Text = "";
        txtVolNo.Text = "";
        txtCurrPageBudget.Text = "";
        txtNextPageBudget.Text = "";
        ddlPageStarts.SelectedValue = "0";

        ddlTrimCode.SelectedValue = "0";
        ddlPageGSM.SelectedValue = "0";
        txtProdEds.Text = "";
        txtSupervisor.Text = "";
        ddlDataformat.SelectedValue = "0";

        ddlTAT.SelectedValue = "0";
        txtPriceCode.Text = "";
        ddlSensitiveJournal.SelectedValue = "0";
        ddlUrgentTat.SelectedValue = "0";
        ddlIssueTAT.SelectedValue = "0";
        txtCreatedOn.Text = "";
   
    }
    protected void btnSupContactDet_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlSupervisor.SelectedValue != "0")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "window.open('DisplayInfo.aspx?Conno=" + ddlSupervisor.SelectedValue + " ' ,null,'height=350,width=250,status=no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no ');", true);
        }
        else
        {
            Alert("No Supervisor selected");
        }
    }
    protected void btnPmgrContactDet_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlProdMgr.SelectedValue != "0")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "window.open('DisplayInfo.aspx?Conno=" + ddlProdMgr.SelectedValue + " ' ,null,'height=350,width=250,status=no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no ');", true);
        }
        else
        {
            Alert("No Production Manager selected");
        }
    }
    protected void btnPEContactDet_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlProdEditor.SelectedValue != "0")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "window.open('DisplayInfo.aspx?Conno=" + ddlProdEditor.SelectedValue + " ' ,null,'height=350,width=250,status=no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no ');", true);
        }
        else
        {
            Alert("No Production Editor selected");
        }
    }
}
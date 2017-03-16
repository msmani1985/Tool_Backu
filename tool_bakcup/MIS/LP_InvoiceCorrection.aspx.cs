using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class LP_InvoiceCorrection : System.Web.UI.Page
{
    Delphi_Projects oPro = new Delphi_Projects();
    Non_Launch nonLa = new Non_Launch();
    Launch_SQL oNewLa = new Launch_SQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Session["Cart"] = null;
            lnkGeneral_Click(sender,e);
        }
    }
    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (txtPPONumber.Text != "")
        {
            string[] ponumbers = txtPPONumber.Text.Trim().Split(',');
            string sMsg = "";
            foreach (string ponumber in ponumbers)
            {
                DataSet dsPro = oPro.chkPoNumber(ponumber.Trim());
                if (dsPro != null)
                {
                    if (dsPro.Tables[0].Rows.Count > 0)
                    {
                        if (sMsg == "")
                        {
                            sMsg = ponumber.Trim();
                        }
                        else
                        {
                            sMsg = sMsg + ", " + ponumber.Trim();
                        }
                    }
                }
            }
            if (sMsg != "")
            {
                sMessage += i++ + ". " + sMsg.Trim() + " PO Number Already Exists.";
            }
        }
        if (i > 1)
        {
            Alert(sMessage);
            return false;
        }
        return true;
    }
    protected void imgbtnFinalQuoteSave_Click(object sender, ImageClickEventArgs e)
    {
        string[] aProjectDetails;
        if (validateScreen())
        {
            if (hfP_ID.Value != "")
            {
                int i = 8;
                aProjectDetails = new string[24];
                aProjectDetails[0] = hfP_ID.Value;
                aProjectDetails[1] = lblPcode.Text;
                aProjectDetails[2] = drpPCustomer.SelectedValue;
                aProjectDetails[3] = drpPFinSite.SelectedValue;
                aProjectDetails[4] = drpPPEName.SelectedValue;
                aProjectDetails[5] = txtPPONumber.Text;
                aProjectDetails[6] = txtPPages.Text;
                aProjectDetails[7] = txtPRecDate.Text;
                aProjectDetails[8] = txtPCompDate.Text;
                int k = 1;
                foreach (GridViewRow grs in gv_Lmodule.Rows)
                {
                    if (k <= 5)
                    {
                        TextBox Desc = (TextBox)grs.FindControl("txt_des");
                        TextBox Qty = (TextBox)grs.FindControl("txt_qty");
                        TextBox PC = (TextBox)grs.FindControl("txt_pricecode");
                        i++;
                        aProjectDetails[i] = Desc.Text;
                        i++;
                        aProjectDetails[i] = Qty.Text;
                        i++;
                        aProjectDetails[i] = PC.Text;
                    }
                    k++;
                }
                i++;
                for (i = i; i < 24; i++)
                {
                    aProjectDetails[i] = "";
                }
                //if (k > 5)
                //{
                //    foreach (GridViewRow grs in gv_Lmodule.Rows)
                //    {
                //        TextBox Desc = (TextBox)grs.FindControl("txt_des");
                //        TextBox Qty = (TextBox)grs.FindControl("txt_qty");
                //        TextBox PC = (TextBox)grs.FindControl("txt_pricecode");
                //        TextBox po = (TextBox)grs.FindControl("txt_mponumber");
                //        DropDownList costtype = (DropDownList)grs.FindControl("ddl_costtype");
                //        DropDownList pe = (DropDownList)grs.FindControl("ddl_pename");
                //        HiddenField hf_LP_ID = (HiddenField)grs.FindControl("hf_LP_ID");

                //        string proc = "spInsertLP_Module2";
                //        string[,] para = {  { "@LP_ID", hfP_ID.Value },
                //                             { "@CUSTNO", drpPCustomer.SelectedValue }, { "@moponumber", po.Text }, 
                //                             {"@COSTTYPEID",costtype.SelectedValue},{"@PRICECODE",PC.Text},
                //                             {"@CONNO",pe.SelectedValue},{"@NUMPAGES",Qty.Text},{"@MPTITLE",Desc.Text}};
                //        int val = this.oNewLa.ExcSP(proc, para, CommandType.StoredProcedure);
                //    }
                //}
                string msg = this.nonLa.InsertLP_Project(aProjectDetails);
                if (msg.Contains("Project creation failed!") ||
                            msg.Contains("PO or WO Already Exists!") ||
                            msg.Contains("Project Name Already Exists!")) Alert(msg);
                else
                {
                    DataSet ds1 = new DataSet();
                    ds1 = nonLa.GetProLP(hfP_ID.Value);
                    if (ds1 != null)
                    {
                        foreach (GridViewRow grs in gv_Lmodule.Rows)
                        {
                            TextBox Desc = (TextBox)grs.FindControl("txt_des");
                            TextBox Qty = (TextBox)grs.FindControl("txt_qty");
                            TextBox PC = (TextBox)grs.FindControl("txt_pricecode");
                            TextBox po = (TextBox)grs.FindControl("txt_mponumber");
                            DropDownList costtype = (DropDownList)grs.FindControl("ddl_costtype");
                            DropDownList pe = (DropDownList)grs.FindControl("ddl_pename");
                            HiddenField hf_LP_ID = (HiddenField)grs.FindControl("hf_LP_ID");

                            string proc = "spInsertLP_Module1";
                            string[,] para = { { "@PROJECTNO", ds1.Tables[0].Rows[0]["PROJECTNO"].ToString() }, { "@LP_ID", hf_LP_ID.Value },
                                             { "@CUSTNO", drpPCustomer.SelectedValue }, { "@moponumber", po.Text }, 
                                             {"@COSTTYPEID",costtype.SelectedValue},{"@PRICECODE",PC.Text},
                                             {"@CONNO",pe.SelectedValue},{"@NUMPAGES",Qty.Text},{"@MPTITLE",Desc.Text}};
                            int val = this.oNewLa.ExcSP(proc, para, CommandType.StoredProcedure);
                        }
                    }

                    Session["Cart"] = null;
                    hfP_ID.Value = "";
                    Alert("Successfully Saved.");
                    lnkGeneral_Click(sender, e);
                }
            }
        }
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void gv_Lmodule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        datasourceIBSQL peobj = new datasourceIBSQL();
        DataSet peds = new DataSet();
        try
        {
            DropDownList drpConn = (DropDownList)e.Row.FindControl("ddl_pename");
            HiddenField hf_LP_ID = (HiddenField)e.Row.FindControl("hf_LP_ID");
            if (drpConn != null)
            {
                DataSet ds = new DataSet();
                ds = nonLa.getPENamebyID(hf_LP_ID.Value);
                peds = peobj.GetPEContact();
                DataRow dr = peds.Tables[0].NewRow();
                dr["conno"] = 0;
                dr["invdisplayname"] = "--Select--";
                peds.Tables[0].Rows.InsertAt(dr, 0);
                drpConn.DataSource = peds;
                drpConn.DataTextField = "invdisplayname";
                drpConn.DataValueField = "CONNO";
                drpConn.DataBind();
                drpConn.SelectedValue = ds.Tables[0].Rows[0]["conno"].ToString();
            }
        }
        catch (Exception Ex)
        {
            Alert(Ex.Message.ToString());
        }
    }
    protected void gv_Lmodule1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        datasourceIBSQL peobj = new datasourceIBSQL();
        DataSet peds = new DataSet();
        try
        {
            DropDownList drpConn = (DropDownList)e.Row.FindControl("ddl_pename");
            if (drpConn != null)
            {
                peds = peobj.GetPEContact();
                DataRow dr = peds.Tables[0].NewRow();
                dr["conno"] = 0;
                dr["invdisplayname"] = "--Select--";
                peds.Tables[0].Rows.InsertAt(dr, 0);
                drpConn.DataSource = peds;
                drpConn.DataTextField = "invdisplayname";
                drpConn.DataValueField = "CONNO";
                drpConn.DataBind();
                drpConn.SelectedValue = DataBinder.Eval(e.Row.DataItem, "CONNO").ToString();
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList drpConn1 = e.Row.FindControl("Addddl_pename") as DropDownList;
                peds = peobj.GetPEContact();
                DataRow dr = peds.Tables[0].NewRow();
                dr["conno"] = 0;
                dr["invdisplayname"] = "--Select--";
                peds.Tables[0].Rows.InsertAt(dr, 0);
                drpConn1.DataSource = peds;
                drpConn1.DataTextField = "invdisplayname";
                drpConn1.DataValueField = "CONNO";
                drpConn1.DataBind();
                drpConn1.SelectedValue = drpPPEName.SelectedValue;
            }
        }
        catch (Exception Ex)
        {
            Alert(Ex.Message.ToString());
        }
    }
    protected void lnkJobInfo_Click(object sender, EventArgs e)
    {
        Session["Cart"] = null;
        var selectedProducts = GvNL.Rows.Cast<GridViewRow>()
                                .Where(row => ((CheckBox)row.FindControl("chkGrpInv")).Checked)
                                .Select(row => GvNL.DataKeys[row.RowIndex].Value.ToString()).ToList();
        if (Session["Cart"] == null)
        {
            Session["Cart"] = selectedProducts;
        }
        else
        {
            var cart = (List<string>)Session["Cart"];
            foreach (var product in selectedProducts)
                cart.Add(product);
            Session["Cart"] = cart;
        }
        foreach (GridViewRow row in GvNL.Rows)
        {
            CheckBox cb = (CheckBox)row.FindControl("chkGrpInv");
            if (cb.Checked)
                cb.Checked = false;
        }

        string paramater = "";
        var values = (List<string>)Session["Cart"];
        if (values.Count > 0)
        {
            var parms = values.Select((s, i) => "@p" + i.ToString()).ToArray();
            var inclause = string.Join(",", parms);
            for (var i = 0; i < parms.Length; i++)
            {
                if (paramater == "")
                {
                    paramater = values[i];
                    hfP_ID.Value = paramater;
                }
                else
                    paramater = paramater + "," + values[i];
            }
        }

        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value;
        else
            id = "";
        DataSet ds = new DataSet();
        ds = nonLa.getFinalQuoteLoad(id.ToString());
        if (ds != null && ds.Tables[0].Rows.Count>0)
        {
            drpPCustomer.DataSource = ds.Tables[1];
            drpPCustomer.DataTextField = ds.Tables[1].Columns[1].ToString();
            drpPCustomer.DataValueField = ds.Tables[1].Columns[0].ToString();
            drpPCustomer.DataBind();

            Delphi_Projects oPro = new Delphi_Projects();
            drpPFinSite.Items.Clear();
            if (drpPCustomer.SelectedItem.Value.Trim() != "0")
            {
                DataSet dscustfin = oPro.getCusomerFinsite(drpPCustomer.SelectedItem.Value.Trim());
                drpPFinSite.DataSource = dscustfin;
                drpPFinSite.DataTextField = dscustfin.Tables[0].Columns[1].ToString();
                drpPFinSite.DataValueField = dscustfin.Tables[0].Columns[0].ToString();
                drpPFinSite.DataBind();
            }
            //drpPFinSite.Items.Insert(0, new ListItem("-- select --", "0"));

            DataSet dscustPE = oPro.getPEName();
            drpPPEName.DataSource = dscustPE;
            drpPPEName.DataTextField = dscustPE.Tables[0].Columns[1].ToString();
            drpPPEName.DataValueField = dscustPE.Tables[0].Columns[0].ToString();
            drpPPEName.DataBind();

            //drpPPEName.DataSource = ds.Tables[2];
            //drpPPEName.DataTextField = ds.Tables[2].Columns[1].ToString();
            //drpPPEName.DataValueField = ds.Tables[2].Columns[0].ToString();
            //drpPPEName.DataBind();

            DataSet ds1 = new DataSet();
            //ds1 = nonLa.GetProLP(id.ToString());
            //if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            //{
            //    lblPJobID.Text = ds.Tables[0].Rows[0]["JOBID"].ToString();
            //    lblPcode.Text = Regex.Replace(ds.Tables[0].Rows[0]["ProjectName"].ToString().Trim(), "[^a-zA-Z0-9_]+", " ");
            //    txtPPONumber.Text = ds.Tables[0].Rows[0]["JobNo"].ToString();
            //    txtPPages.Text = ds1.Tables[0].Rows[0]["PNOOFPAGES"].ToString();
            //    txtPRecDate.Text = ds1.Tables[0].Rows[0]["PRECEIVEDDATE"].ToString();
            //    drpPFinSite.SelectedValue = ds1.Tables[0].Rows[0]["FINSITENO"].ToString();
            //    drpPPEName.SelectedValue = ds1.Tables[0].Rows[0]["CONNO"].ToString();
            //    LoadGrid(ds1.Tables[0].Rows[0]["ProjectNo"].ToString());
            //    gv_Lmodule1.Visible = true;
            //    gv_Lmodule.Visible = false;
            //}
            //else
            {
                if (paramater.Length > 8)
                {
                    lblPJobID.Text = "";
                    lblPcode.Text = "";
                    txtPPONumber.Text = "";
                    txtPPages.Text = "";
                    txtPRecDate.Text = "";
                    txtPCompDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
                    DataSet ds2 = new DataSet();
                    ds2 = nonLa.getFinalQuoteVal1(paramater.Trim());
                    gv_Lmodule.DataSource = ds2;
                    gv_Lmodule.DataBind();
                    gv_Lmodule.Visible = true;
                    gv_Lmodule1.Visible = false;
                    drpPPEName.SelectedValue = ds.Tables[2].Rows[0]["CONNO"].ToString();
                }
                else
                {
                    lblPJobID.Text = ds.Tables[0].Rows[0]["JOBID"].ToString();
                    lblPcode.Text = Regex.Replace(ds.Tables[0].Rows[0]["ProjectName"].ToString().Trim(), "[^a-zA-Z0-9_]+", " ");
                    txtPPONumber.Text = ds.Tables[0].Rows[0]["JobNo"].ToString();
                    txtPPages.Text = ds.Tables[0].Rows[0]["Pages_Count"].ToString();
                    txtPRecDate.Text = ds.Tables[0].Rows[0]["Created_Date"].ToString();
                    txtPCompDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
                    drpPPEName.SelectedValue = ds.Tables[2].Rows[0]["CONNO"].ToString();
                    DataSet ds2 = new DataSet();
                    ds2 = nonLa.getFinalQuoteVal1(id.ToString());
                    gv_Lmodule.DataSource = ds2;
                    gv_Lmodule.DataBind();
                    gv_Lmodule.Visible = true;
                    gv_Lmodule1.Visible = false;
                }
            }

        }

        this.showPanel(tabFinalQuote);
    }
    private void LoadGrid(string ProjectNo)
    {
        datasourceIBSQL inobj = new datasourceIBSQL();
        DataSet inds = new DataSet();
        try
        {
            gv_Lmodule1.Visible = true;
            gv_Lmodule1.DataSource = inobj.GetProject_Modules(ProjectNo.ToString());
            gv_Lmodule1.DataBind();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { inds = null; inobj = null; }
    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabGeneral":
                miGeneral.Attributes.Add("class", "current");
                miJobInfo.Attributes.Add("class", "");
                miIBM.Attributes.Add("class", "");
                miIBMRTE.Attributes.Add("class", "");
                this.tabGeneral.Visible = true;
                this.tabFinalQuote.Visible = false;
                this.tabIBM.Visible = false;
                this.tabIBMRTE.Visible = false;
                break;
            case "tabFinalQuote":
                miGeneral.Attributes.Add("class", "");
                miJobInfo.Attributes.Add("class", "current");
                miIBM.Attributes.Add("class", "");
                miIBMRTE.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabFinalQuote.Visible = true;
                this.tabIBM.Visible = false;
                this.tabIBMRTE.Visible = false;
                break;
            case "tabIBM":
                miGeneral.Attributes.Add("class", "");
                miJobInfo.Attributes.Add("class", "");
                miIBM.Attributes.Add("class", "current");
                miIBMRTE.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabFinalQuote.Visible = false;
                this.tabIBM.Visible = true;
                this.tabIBMRTE.Visible = false;
                break;
            case "tabIBMRTE":
                miGeneral.Attributes.Add("class", "");
                miJobInfo.Attributes.Add("class", "");
                miIBM.Attributes.Add("class", "");
                miIBMRTE.Attributes.Add("class", "current");
                this.tabGeneral.Visible = false;
                this.tabFinalQuote.Visible = false;
                this.tabIBM.Visible = false;
                this.tabIBMRTE.Visible = true;
                break;
        }
    }
    protected void lnkGeneral_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = nonLa.GetFinalDeljobs();
        GvNL.DataSource = ds;
        GvNL.DataBind();
        this.showPanel(tabGeneral);
    }
    protected void GvNL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,\"" + ((HiddenField)e.Row.FindControl("hfgvNLID")).Value.Trim() + "\",\"" + ((Label)e.Row.FindControl("lblgvPtitle")).Text.Trim() + "\");";
            HiddenField LP_id = e.Row.FindControl("hfgvNLID") as HiddenField;
            CheckBox status = e.Row.FindControl("chkGrpInv") as CheckBox;
            if (Session["Cart"] != null)
            {
                var collection = (List<string>)Session["Cart"];
                if (collection.Contains(LP_id.Value))
                {
                    status.Checked = true;
                }
                else
                {
                    status.Checked = false;
                }
            }
            
        }
    }
    protected void gv_Lmodule1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Add"))
        {
            TextBox txtAddDesc = (TextBox)gv_Lmodule1.FooterRow.FindControl("txtAddDesc");
            TextBox txtAddQty = (TextBox)gv_Lmodule1.FooterRow.FindControl("txtAddQty");
            TextBox txtAddPC = (TextBox)gv_Lmodule1.FooterRow.FindControl("txtAddPC");
            TextBox txtAddPO = (TextBox)gv_Lmodule1.FooterRow.FindControl("txtAddPO");
            DropDownList Addddl_costtype = (DropDownList)gv_Lmodule1.FooterRow.FindControl("Addddl_costtype");
            DropDownList Addddl_pename = (DropDownList)gv_Lmodule1.FooterRow.FindControl("Addddl_pename");
            DataSet ds1 = new DataSet();
            ds1 = nonLa.GetProLP(hfP_ID.Value);
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                string proc = "spInsertLP_Module";
                string[,] para = { { "@PROJECTNO", ds1.Tables[0].Rows[0]["PROJECTNO"].ToString() }, 
                                             { "@CUSTNO", drpPCustomer.SelectedValue }, { "@moponumber", txtAddPO.Text }, 
                                             {"@COSTTYPEID",Addddl_costtype.SelectedValue},{"@PRICECODE",txtAddPC.Text},
                                             {"@CONNO",Addddl_pename.SelectedValue},{"@NUMPAGES",txtAddQty.Text},
                                             {"@MPTITLE",txtAddDesc.Text}};
                int val = this.oNewLa.ExcSP(proc, para, CommandType.StoredProcedure);
            }
            LoadGrid(ds1.Tables[0].Rows[0]["PROJECTNO"].ToString());
        }
        else if (e.CommandName.Equals("Delete"))
        {
            //HiddenField lblEditID = (HiddenField)gv_Lmodule1.Rows.FindControl("lblEditID");
            string uval = string.Empty;
            foreach (GridViewRow gr in gv_Lmodule1.Rows)
            {
                if (((CheckBox)gr.FindControl("cb_delete")).Checked)
                { uval = ((HiddenField)gr.FindControl("hf_rowmoduleno")).Value + ","; }
            }
            if (!string.IsNullOrEmpty(uval))
            {
                uval = uval.TrimEnd(',');
                datasourceIBSQL mobj = new datasourceIBSQL();
                nonLa.DeleteModules(uval);
            }
        }
    }
    protected void ibtn_Delete_click(object sender, ImageClickEventArgs e)
    {
        string uval = string.Empty;
        foreach (GridViewRow gr in gv_Lmodule1.Rows)
        {
            if (((CheckBox)gr.FindControl("cb_delete")).Checked)
            { uval = ((HiddenField)gr.FindControl("hf_rowmoduleno")).Value + ","; }
        }
        if (!string.IsNullOrEmpty(uval))
        {
            uval = uval.TrimEnd(',');
            datasourceIBSQL mobj = new datasourceIBSQL();
            if (mobj.DeleteModules(uval))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Deleted');</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Deletion Failed');</script>");
             DataSet ds1 = new DataSet();
            ds1 = nonLa.GetProLP(hfP_ID.Value);
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                LoadGrid(ds1.Tables[0].Rows[0]["PROJECTNO"].ToString());
            }
        }

    }
    protected void lnkIBM_Click(object sender, EventArgs e)
    {
        txtIBMProName.Text = "";
        txtIBMRecDate.Text = "";
        txtIBMCompDate.Text = "";
        DataSet ds = new DataSet();
        ds = nonLa.GetIBMjobs();
        gvIBM.DataSource = ds;
        gvIBM.DataBind();
        loadIBM();
        this.showPanel(tabIBM);
    }
    protected void lnkIBMRTE_Click(object sender, EventArgs e)
    {
        txtRTECompDate.Text = "";
        txtRTEProName.Text = "";
        txtRTERecDate.Text = "";
        DataSet ds = new DataSet();
        ds = nonLa.GetIBMRTEjobs();
        gvIBMRTE.DataSource = ds;
        gvIBMRTE.DataBind();
        loadRTE();
        this.showPanel(tabIBMRTE);
    }
    public void loadIBM()
    {
        DataSet dscust = oPro.getCustomers();
        drpIBMCustomer.DataSource = dscust;
        drpIBMCustomer.DataTextField = dscust.Tables[0].Columns[1].ToString();
        drpIBMCustomer.DataValueField = dscust.Tables[0].Columns[0].ToString();
        drpIBMCustomer.SelectedValue = "10159";
        drpIBMCustomer.DataBind();
        drpIBMFinSite.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dscustPE = oPro.getPEName();
        drpIBMPEName.DataSource = dscustPE;
        drpIBMPEName.DataTextField = dscustPE.Tables[0].Columns[1].ToString();
        drpIBMPEName.DataValueField = dscustPE.Tables[0].Columns[0].ToString();
        drpIBMPEName.DataBind();

        drpIBMFinSite.Items.Clear();
        if (drpIBMCustomer.SelectedItem.Value.Trim() != "0")
        {
            DataSet dscustfin = oPro.getCusomerFinsite(drpIBMCustomer.SelectedItem.Value.Trim());
            drpIBMFinSite.DataSource = dscustfin;
            drpIBMFinSite.DataTextField = dscustfin.Tables[0].Columns[1].ToString();
            drpIBMFinSite.DataValueField = dscustfin.Tables[0].Columns[0].ToString();
            drpIBMFinSite.DataBind();
        }
        //drpIBMFinSite.Items.Insert(0, new ListItem("-- select --", "0"));
    }
    public void loadRTE()
    {
        DataSet dscust = oPro.getCustomers();
        drpRTECustomer.DataSource = dscust;
        drpRTECustomer.DataTextField = dscust.Tables[0].Columns[1].ToString();
        drpRTECustomer.DataValueField = dscust.Tables[0].Columns[0].ToString();
        drpRTECustomer.SelectedValue = "10159";
        drpRTECustomer.DataBind();
        drpRTECustomer.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet dscustPE = oPro.getPEName();
        drpRTEPEName.DataSource = dscustPE;
        drpRTEPEName.DataTextField = dscustPE.Tables[0].Columns[1].ToString();
        drpRTEPEName.DataValueField = dscustPE.Tables[0].Columns[0].ToString();
        drpRTEPEName.DataBind();

        drpRTEFinSite.Items.Clear();
        if (drpRTECustomer.SelectedItem.Value.Trim() != "0")
        {
            DataSet dscustfin = oPro.getCusomerFinsite(drpRTECustomer.SelectedItem.Value.Trim());
            drpRTEFinSite.DataSource = dscustfin;
            drpRTEFinSite.DataTextField = dscustfin.Tables[0].Columns[1].ToString();
            drpRTEFinSite.DataValueField = dscustfin.Tables[0].Columns[0].ToString();
            drpRTEFinSite.DataBind();
        }
        //drpRTEFinSite.Items.Insert(0, new ListItem("-- select --", "0"));
    }
    protected void drpIBMCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpIBMFinSite.Items.Clear();
        if (drpIBMCustomer.SelectedItem.Value.Trim() != "0")
        {
            DataSet dscustfin = oPro.getCusomerFinsite(drpIBMCustomer.SelectedItem.Value.Trim());
            drpIBMFinSite.DataSource = dscustfin;
            drpIBMFinSite.DataTextField = dscustfin.Tables[0].Columns[1].ToString();
            drpIBMFinSite.DataValueField = dscustfin.Tables[0].Columns[0].ToString();
            drpIBMFinSite.DataBind();
        }
        //drpIBMFinSite.Items.Insert(0, new ListItem("-- select --", "0"));
    }
    protected void btnIBMSave_Click(object sender, ImageClickEventArgs e)
    {
        string[] aProjectDetails;
        aProjectDetails = new string[8];
        aProjectDetails[0] = txtIBMProName.Text;
        aProjectDetails[1] = drpIBMCustomer.SelectedValue;
        aProjectDetails[2] = drpIBMFinSite.SelectedValue;
        aProjectDetails[3] = drpIBMPEName.SelectedValue;
        aProjectDetails[4] = "";
        aProjectDetails[5] = "";
        aProjectDetails[6] = txtIBMRecDate.Text;
        aProjectDetails[7] = txtIBMCompDate.Text;
        string msg = this.nonLa.InsertLP_ProjectIBM(aProjectDetails);
        if (msg.Contains("Project creation failed!") || msg.Contains("PO or WO Already Exists!") ||
            msg.Contains("Project Name Already Exists"))
        {
            Alert(msg);
        }
        else
        {
            foreach (GridViewRow grw in gvIBM.Rows)
            {
                HiddenField NL_ID = (HiddenField)grw.FindControl("hfgvNLID");
                CheckBox txtdepends = (CheckBox)grw.FindControl("Check");
                bool status = txtdepends.Checked;
                int d;
                if (status == true)
                    d = 1;
                else
                    d = 0;
                if (d == 1)
                {
                    DataSet ds = new DataSet();
                    ds = nonLa.getIBMQuoteInfo(NL_ID.Value);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string po = string.Empty;
                        po = ds.Tables[0].Rows[i]["JobNo"].ToString();
                        string proc = "spInsertIBM_Module";
                        string[,] para = {   {"@PROJECTNO", msg.ToString() },
                                             {"@MPTITLE",Regex.Replace(ds.Tables[0].Rows[i]["ProjectName"].ToString().Trim(), "[^a-zA-Z0-9_]+", " ")}, 
                                             {"@CUSTNO", drpIBMCustomer.SelectedValue }, {"@moponumber", po.ToString() }, 
                                             {"@COSTTYPEID","0"}, {"@PRICECODE",ds.Tables[0].Rows[i]["PRICECODE"].ToString()},{"@NL_ID",NL_ID.Value},
                                             {"@CONNO",drpIBMPEName.SelectedValue}, {"@NUMPAGES",ds.Tables[0].Rows[i]["PAGES_COUNT"].ToString()}};
                        int val = this.oNewLa.ExcSP(proc, para, CommandType.StoredProcedure);
                    }
                }
            }
            Alert("Successfully Saved.");
            lnkIBM_Click(sender, e);
        }
    }
    protected void drpRTECustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpRTEFinSite.Items.Clear();
        if (drpRTECustomer.SelectedItem.Value.Trim() != "0")
        {
            DataSet dscustfin = oPro.getCusomerFinsite(drpRTECustomer.SelectedItem.Value.Trim());
            drpRTEFinSite.DataSource = dscustfin;
            drpRTEFinSite.DataTextField = dscustfin.Tables[0].Columns[1].ToString();
            drpRTEFinSite.DataValueField = dscustfin.Tables[0].Columns[0].ToString();
            drpRTEFinSite.DataBind();
        }
        drpRTEFinSite.Items.Insert(0, new ListItem("-- select --", "0"));
    }
    protected void imgBtnRTESave_Click(object sender, ImageClickEventArgs e)
    {
        string[] aProjectDetails;
        aProjectDetails = new string[8];
        aProjectDetails[0] = txtRTEProName.Text;
        aProjectDetails[1] = drpRTECustomer.SelectedValue;
        aProjectDetails[2] = drpRTEFinSite.SelectedValue;
        aProjectDetails[3] = drpRTEPEName.SelectedValue;
        aProjectDetails[4] = "";
        aProjectDetails[5] = "";
        aProjectDetails[6] = txtRTERecDate.Text;
        aProjectDetails[7] = txtRTECompDate.Text;
        string msg = this.nonLa.InsertLP_ProjectIBM(aProjectDetails);
        if (msg.Contains("Project creation failed!") || msg.Contains("PO or WO Already Exists!") ||
            msg.Contains("Project Name Already Exists"))
        {
            Alert(msg);
        }
        else
        {
            foreach (GridViewRow grw in gvIBMRTE.Rows)
            {
                HiddenField NL_ID = (HiddenField)grw.FindControl("hfgvNLID");
                CheckBox txtdepends = (CheckBox)grw.FindControl("Check");
                bool status = txtdepends.Checked;
                int d;
                if (status == true)
                    d = 1;
                else
                    d = 0;
                if (d == 1)
                {
                    DataSet ds = new DataSet();
                    ds = nonLa.getIBMQuoteInfo(NL_ID.Value);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string po = string.Empty;
                        po = ds.Tables[0].Rows[i]["JobNo"].ToString();
                        string proc = "spInsertIBM_Module";
                        string[,] para = {   {"@PROJECTNO", msg.ToString() },
                                             {"@MPTITLE",Regex.Replace(ds.Tables[0].Rows[i]["ProjectName"].ToString().Trim(), "[^a-zA-Z0-9_]+", " ")}, 
                                             {"@CUSTNO", drpRTECustomer.SelectedValue }, {"@moponumber", po.ToString() }, 
                                             {"@COSTTYPEID","0"}, {"@PRICECODE",ds.Tables[0].Rows[i]["PRICECODE"].ToString()},{"@NL_ID",NL_ID.Value},
                                             {"@CONNO",drpRTEPEName.SelectedValue}, {"@NUMPAGES",ds.Tables[0].Rows[i]["PAGES_COUNT"].ToString()}};
                        int val = this.oNewLa.ExcSP(proc, para, CommandType.StoredProcedure);
                    }
                }
            }
            Alert("Successfully Saved.");
            lnkIBMRTE_Click(sender, e);
        }
    }
}
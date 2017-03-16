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

public partial class LaunchLocation : System.Web.UI.Page
{
    Launch la = new Launch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet Dst1 = new DataSet();
            Launch la = new Launch();
            Dst1 = la.GetLocation();
            Droplocname.DataTextField = "location_name";
            Droplocname.DataValueField = "location_id";
            Droplocname.DataSource = Dst1;
            Droplocname.DataBind();
            dropLoczone.DataTextField = "location_name";
            dropLoczone.DataValueField = "location_id";
            dropLoczone.DataSource = Dst1;
            dropLoczone.DataBind();

            DataSet dscust = la.getAllCustomers();
            DropCust.DataSource = dscust;
            DropCust.DataTextField = dscust.Tables[0].Columns[1].ToString();
            DropCust.DataValueField = dscust.Tables[0].Columns[0].ToString();
            DropCust.DataBind();
            DropCust.Items.Insert(0, new ListItem("-- select --", "0"));

            DataSet Dst2 = new DataSet();
            Dst2 = la.GetLocation();
            DropLoc.DataTextField = "location_name";
            DropLoc.DataValueField = "location_id";
            DropLoc.DataSource = Dst2;
            DropLoc.DataBind();
            this.showPanel(tabLoc);

        }

    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabLoc":
                miLoc.Attributes.Add("class", "current");
                miCustLoc.Attributes.Add("class", "");
                miLocTimeZone.Attributes.Add("class", "");
                this.tabLoc.Visible = true;
                this.tabCustLoc.Visible = false;
                this.tabLocTimeZone.Visible = false;
                break;
            case "tabCustLoc":
                miLoc.Attributes.Add("class", "");
                miCustLoc.Attributes.Add("class", "current");
                miLocTimeZone.Attributes.Add("class", "");
                this.tabLoc.Visible = false;
                this.tabCustLoc.Visible = true;
                this.tabLocTimeZone.Visible = false;
                break;
            case "tabLocTimeZone":
                miLoc.Attributes.Add("class", "");
                miCustLoc.Attributes.Add("class", "");
                miLocTimeZone.Attributes.Add("class", "current");
                this.tabLoc.Visible = false;
                this.tabCustLoc.Visible = false;
                this.tabLocTimeZone.Visible = true;
                break;
        }
    }
    protected void btncreate1_Click(object sender, EventArgs e)
    {
        DataSet Dst1 = new DataSet();
        
                if (btncreate1.Text == "Update")
                {
                    if (txtHrs.Text == "")
                        txtHrs.Text = "0";
                    if (txtMins.Text == "")
                        txtMins.Text = "0";
                    la.updatelocation(txtLocname.Text, Convert.ToInt16(Droplocname.SelectedValue));
                    lblresults.Text = "The Record was successfully Updated!";
                }
                else
                {
                    Dst1 = la.ChkLocation(txtLocname.Text);

                    if (Dst1 != null && Dst1.Tables[0].Rows.Count > 0)
                    {
                        lblresults.Text = "This Location Name was already exist!";
                    }
                    else
                    {
                if (txtHrs.Text == "")
                    txtHrs.Text = "0";
                if (txtMins.Text == "")
                    txtMins.Text = "0";
                la.insertlocation(txtLocname.Text);
                lblresults.Text = "The Record was successfully inserted!";
                    }
                }
    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        txtLocname.Text = "";
        txtHrs.Text = "";
        txtMins.Text = "";
        DropTimeZone.ClearSelection();
        btncreate1.Text = "Create";
        lblresults.Text = "";
        Label6.Text = "";
    }
    protected void btnsearch1_Click(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        Ds = la.SearchLocation(Convert.ToInt16(Droplocname.SelectedValue));
        if (Ds != null && Ds.Tables[0].Rows.Count == 1)
        {
            txtLocname.Text = Ds.Tables[0].Rows[0]["Location_name"].ToString();
            //txtHrs.Text = Ds.Tables[0].Rows[0]["time"].ToString();
            //txtMins.Text = Ds.Tables[0].Rows[0]["min"].ToString();
            //dd_timezone.SelectedValue = Ds.Tables[0].Rows[0]["time_zone"].ToString();
        }
        btncreate1.Text = "Update";
    }
    protected void DropCust_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds8 = la.GetLocationCust(Convert.ToInt16(DropCust.SelectedValue));
        GvCust.DataSource = ds8;
        GvCust.DataBind();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        DataSet ds = la.chkCustLoc(Convert.ToInt16(DropCust.SelectedValue), Convert.ToInt16(DropLoc.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
           CustLoc.Text = "The Record was Already Inserted!";
        }
        else
        {
            la.insertCustloc(Convert.ToInt16(DropCust.SelectedValue), Convert.ToInt16(DropLoc.SelectedValue));
            DataSet ds9 = la.GetLocationCust(Convert.ToInt16(DropCust.SelectedValue));
            GvCust.DataSource = ds9;
            GvCust.DataBind();
            CustLoc.Text = "The Record was successfully Updated!";
        }
    }
    protected void lnkLoc_Click(object sender, EventArgs e)
    {
        this.showPanel(tabLoc);
    }
    protected void lnkCustLoc_Click(object sender, EventArgs e)
    {
        this.showPanel(tabCustLoc);
    }

    protected void createzone_Click(object sender, EventArgs e)
    {
        if (txtHrs.Text == "")
            txtHrs.Text = "0";
        if (txtMins.Text == "")
            txtMins.Text = "0";
        la.inserttimezone(Convert.ToInt16(dropLoczone.SelectedValue), DropTimeZone.SelectedValue,txtHrs.Text,txtMins.Text);
        Label6.Text = "The Record was successfully inserted!";
    }
    protected void clearzone_Click(object sender, EventArgs e)
    {
        txtLocname.Text = "";
        txtHrs.Text = "";
        txtMins.Text = "";
        DropTimeZone.ClearSelection();
        btncreate1.Text = "Create";
        lblresults.Text = "";
        Label6.Text = "";
    }
    protected void lnkTimeZone_Click(object sender, EventArgs e)
    {
        this.showPanel(tabLocTimeZone);
    }
}

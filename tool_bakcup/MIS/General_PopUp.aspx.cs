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

public partial class General_PopUp : System.Web.UI.Page
{
    GeneralInform oIB = new GeneralInform();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            pageload();
        }
        
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (Request.QueryString["id"] == "1")
        {
            DataSet ds = new DataSet();
            ds = oIB.GetTypeStyle1(Convert.ToInt16(DropDownList1.SelectedValue));
            TextBox1.Text = ds.Tables[0].Rows[0]["Typestyle"].ToString();
            TextBox2.Text = ds.Tables[0].Rows[0]["Typedescription"].ToString();
        }
        else if (Request.QueryString["id"] == "2")
        {
            DataSet ds = new DataSet();
            ds = oIB.GetJourCategory1(Convert.ToInt16(DropDownList1.SelectedValue));
            TextBox1.Text = ds.Tables[0].Rows[0]["Category"].ToString();
        }
        else if (Request.QueryString["id"] == "3")
        {
            DataSet ds = new DataSet();
            ds = oIB.GetCoverMaterial1(Convert.ToInt16(DropDownList1.SelectedValue));
            TextBox1.Text = ds.Tables[0].Rows[0]["Material"].ToString();
        }
        else if (Request.QueryString["id"] == "4")
        {
            DataSet ds = new DataSet();
            ds = oIB.GetPaperType1(Convert.ToInt16(DropDownList1.SelectedValue));
            TextBox1.Text = ds.Tables[0].Rows[0]["Papertype"].ToString();
        }
        else if (Request.QueryString["id"] == "5")
        {
            DataSet ds = new DataSet();
            ds = oIB.GetPageTrim1(Convert.ToInt16(DropDownList1.SelectedValue));
            TextBox1.Text = ds.Tables[0].Rows[0]["TrimSize"].ToString();
            TextBox2.Text = ds.Tables[0].Rows[0]["TrimCode"].ToString();
            DropDownList2.SelectedValue = ds.Tables[0].Rows[0]["Format"].ToString();
        }
        else if (Request.QueryString["id"] == "6")
        {
            DataSet ds = new DataSet();
            ds = oIB.GetPaperGSM1(Convert.ToInt16(DropDownList1.SelectedValue));
            TextBox1.Text = ds.Tables[0].Rows[0]["GSMWeight"].ToString();
        }
        else if (Request.QueryString["id"] == "7")
        {
            DataSet ds = new DataSet();
            ds = oIB.GetDigital1(Convert.ToInt16(DropDownList1.SelectedValue));
            TextBox1.Text = ds.Tables[0].Rows[0]["Prodcode"].ToString();
            TextBox2.Text = ds.Tables[0].Rows[0]["ProdDescription"].ToString();
        }
        else if (Request.QueryString["id"] == "8")
        {
            DataSet ds = new DataSet();
            ds = oIB.GetTypeset1(Convert.ToInt16(DropDownList1.SelectedValue));
            TextBox1.Text = ds.Tables[0].Rows[0]["Tplatcode"].ToString();
            TextBox2.Text = ds.Tables[0].Rows[0]["TplatDescription"].ToString();
        }
        else if (Request.QueryString["id"] == "9")
        {
            DataSet ds = new DataSet();
            ds = oIB.GetCurrency1(Convert.ToInt16(DropDownList1.SelectedValue));
            TextBox1.Text = ds.Tables[0].Rows[0]["Currname"].ToString();
        }
        else if (Request.QueryString["id"] == "10")
        {
            DataSet ds = new DataSet();
            ds = oIB.GetCountry1(Convert.ToInt16(DropDownList1.SelectedValue));
            TextBox1.Text = ds.Tables[0].Rows[0]["Ctyname"].ToString();
            TextBox2.Text = ds.Tables[0].Rows[0]["ctyabbreviation"].ToString();
        }
        else if (Request.QueryString["id"] == "11")
        {
            DataSet ds = new DataSet();
            ds = oIB.GetSales1(Convert.ToInt16(DropDownList1.SelectedValue));
            TextBox1.Text = ds.Tables[0].Rows[0]["slcatname"].ToString();
        }
    }
    protected void Create_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == "1")
        {
            oIB.InsertTypeStyle(TextBox1.Text,TextBox2.Text);
        }
        else if (Request.QueryString["id"] == "2")
        {
            oIB.InsertJourCategory(TextBox1.Text);
        }
        else if (Request.QueryString["id"] == "3")
        {
            oIB.InsertCoverMaterial(TextBox1.Text);
        }
        else if (Request.QueryString["id"] == "4")
        {
            oIB.InsertPaperType(TextBox1.Text);
        }
        else if (Request.QueryString["id"] == "5")
        {
            oIB.InsertPageTrim(TextBox2.Text,TextBox1.Text,DropDownList2.SelectedValue);
        }
        else if (Request.QueryString["id"] == "6")
        {
            oIB.InsertPaperGSM(TextBox1.Text);
        }
        else if (Request.QueryString["id"] == "7")
        {
            oIB.InsertDigital(TextBox1.Text,TextBox2.Text);
        }
        else if (Request.QueryString["id"] == "8")
        {
            oIB.InsertTypeset(TextBox1.Text, TextBox2.Text);
        }
        else if (Request.QueryString["id"] == "9")
        {
            oIB.InsertCurrency(TextBox1.Text);
        }
        else if (Request.QueryString["id"] == "10")
        {
            oIB.InsertCountry(TextBox1.Text,TextBox2.Text);
        }
        else if (Request.QueryString["id"] == "11")
        {
            oIB.InsertSales(TextBox1.Text);
        }

    }
    protected void Update_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == "1")
        {
            oIB.UpdateTypeStyle(TextBox1.Text, TextBox2.Text, Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "2")
        {
            oIB.UpdateJourCategory(TextBox1.Text, Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "3")
        {
            oIB.UpdateCoverMaterial(TextBox1.Text, Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "4")
        {
            oIB.UpdatePaperType(TextBox1.Text, Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "5")
        {
            oIB.UpdatePageTrim(TextBox2.Text,TextBox1.Text,DropDownList2.SelectedValue,Convert.ToInt16( DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "6")
        {
            oIB.UpdatePaperGSM(TextBox1.Text, Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "7")
        {
            oIB.UpdateDigital(TextBox1.Text,TextBox2.Text, Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "8")
        {
            oIB.UpdateTypeset(TextBox1.Text, TextBox2.Text, Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "9")
        {
            oIB.UpdateCurrency(TextBox1.Text, Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "10")
        {
            oIB.UpdateCountry(TextBox1.Text,TextBox2.Text, Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "11")
        {
            oIB.UpdateSales(TextBox1.Text, Convert.ToInt16(DropDownList1.SelectedValue));
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == "1")
        {
            oIB.DeleteTypeStyle(Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "2")
        {
            oIB.DeleteJourCategory(Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "3")
        {
            oIB.DeleteCoverMaterial(Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "4")
        {
            oIB.DeletePaperType(Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "5")
        {
            oIB.DeletePageTrim(Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "6")
        {
            oIB.DeletePaperGSM(Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "7")
        {
            oIB.DeleteDigital(Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "8")
        {
            oIB.DeleteTypeset(Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "9")
        {
            oIB.DeleteCurrency(Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "10")
        {
            oIB.DeleteCountry(Convert.ToInt16(DropDownList1.SelectedValue));
        }
        else if (Request.QueryString["id"] == "11")
        {
            oIB.DeleteSales(Convert.ToInt16(DropDownList1.SelectedValue));
        }
        pageload();
    }

    private void pageload()
    {

        try
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            if (Request.QueryString["id"] == "1")
            {
                Label1.Text = "Select an entry from the list";
                Label2.Text = "Typestyle";
                Label3.Text = "Type description";
                Label1.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                DropDownList1.Visible = true;
                TextBox1.Visible = true;
                TextBox2.Visible = true;

                DataSet dsemp = oIB.GetTypeStyle();
                DropDownList1.DataSource = dsemp;
                DropDownList1.DataTextField = dsemp.Tables[0].Columns[1].ToString();
                DropDownList1.DataValueField = dsemp.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            else if (Request.QueryString["id"] == "2")
            {
                Label1.Text = "Select an entry from the list";
                Label2.Text = "Category";
                Label1.Visible = true;
                Label2.Visible = true;
                DropDownList1.Visible = true;
                TextBox1.Visible = true;

                DataSet dsemp = oIB.GetJourCategory();
                DropDownList1.DataSource = dsemp;
                DropDownList1.DataTextField = dsemp.Tables[0].Columns[1].ToString();
                DropDownList1.DataValueField = dsemp.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            else if (Request.QueryString["id"] == "3")
            {
                Label1.Text = "Select an entry from the list";
                Label2.Text = "Material";
                Label1.Visible = true;
                Label2.Visible = true;
                DropDownList1.Visible = true;
                TextBox1.Visible = true;

                DataSet dsemp = oIB.GetCoverMaterial();
                DropDownList1.DataSource = dsemp;
                DropDownList1.DataTextField = dsemp.Tables[0].Columns[1].ToString();
                DropDownList1.DataValueField = dsemp.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            else if (Request.QueryString["id"] == "4")
            {
                Label1.Text = "Select an entry from the list";
                Label2.Text = "Paper Type";
                Label1.Visible = true;
                Label2.Visible = true;
                DropDownList1.Visible = true;
                TextBox1.Visible = true;

                DataSet dsemp = oIB.GetPaperType();
                DropDownList1.DataSource = dsemp;
                DropDownList1.DataTextField = dsemp.Tables[0].Columns[1].ToString();
                DropDownList1.DataValueField = dsemp.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            else if (Request.QueryString["id"] == "5")
            {
                Label1.Text = "Select an entry from the list";
                Label2.Text = "Trim size";
                Label3.Text = "Trim code";
                Label4.Text = "Format";
                Label1.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                DropDownList1.Visible = true;
                TextBox1.Visible = true;
                TextBox2.Visible = true;
                DropDownList2.Visible = true;

                DataSet dsemp = oIB.GetPageTrim();
                DropDownList1.DataSource = dsemp;
                DropDownList1.DataTextField = dsemp.Tables[0].Columns[1].ToString();
                DropDownList1.DataValueField = dsemp.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            else if (Request.QueryString["id"] == "6")
            {
                Label1.Text = "Select an entry from the list";
                Label2.Text = "GSM value";
                Label1.Visible = true;
                Label2.Visible = true;
                DropDownList1.Visible = true;
                TextBox1.Visible = true;

                DataSet dsemp = oIB.GetPaperGSM();
                DropDownList1.DataSource = dsemp;
                DropDownList1.DataTextField = dsemp.Tables[0].Columns[1].ToString();
                DropDownList1.DataValueField = dsemp.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            else if (Request.QueryString["id"] == "7")
            {
                Label1.Text = "Select an entry from the list";
                Label2.Text = "Code";
                Label3.Text = "Description";
                TextBox2.TextMode = TextBoxMode.MultiLine;
                TextBox2.Height = 70;
                TextBox2.Width = 150;
                Label1.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                DropDownList1.Visible = true;
                TextBox1.Visible = true;
                TextBox2.Visible = true;

                DataSet dsemp = oIB.GetDigital();
                DropDownList1.DataSource = dsemp;
                DropDownList1.DataTextField = dsemp.Tables[0].Columns[1].ToString();
                DropDownList1.DataValueField = dsemp.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            else if (Request.QueryString["id"] == "8")
            {
                Label1.Text = "Select an entry from the list";
                Label2.Text = "Code";
                Label3.Text = "Description";
                TextBox2.TextMode = TextBoxMode.MultiLine;
                TextBox2.Height = 70;
                TextBox2.Width = 150;
                Label1.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                DropDownList1.Visible = true;
                TextBox1.Visible = true;
                TextBox2.Visible = true;

                DataSet dsemp = oIB.GetTypeset();
                DropDownList1.DataSource = dsemp;
                DropDownList1.DataTextField = dsemp.Tables[0].Columns[1].ToString();
                DropDownList1.DataValueField = dsemp.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            else if (Request.QueryString["id"] == "9")
            {
                Label1.Text = "Select an entry from the list";
                Label2.Text = "Name";
                Label1.Visible = true;
                Label2.Visible = true;
                DropDownList1.Visible = true;
                TextBox1.Visible = true;

                DataSet dsemp = oIB.GetCurrency();
                DropDownList1.DataSource = dsemp;
                DropDownList1.DataTextField = dsemp.Tables[0].Columns[1].ToString();
                DropDownList1.DataValueField = dsemp.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            else if (Request.QueryString["id"] == "10")
            {
                Label1.Text = "Select an entry from the list";
                Label2.Text = "Name";
                Label3.Text = "Abbreviation";
                Label1.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                DropDownList1.Visible = true;
                TextBox1.Visible = true;
                TextBox2.Visible = true;

                DataSet dsemp = oIB.GetCountry();
                DropDownList1.DataSource = dsemp;
                DropDownList1.DataTextField = dsemp.Tables[0].Columns[1].ToString();
                DropDownList1.DataValueField = dsemp.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            else if (Request.QueryString["id"] == "11")
            {
                Label1.Text = "Select an entry from the list";
                Label2.Text = "Name";
                Label1.Visible = true;
                Label2.Visible = true;
                DropDownList1.Visible = true;
                TextBox1.Visible = true;

                DataSet dsemp = oIB.GetSales();
                DropDownList1.DataSource = dsemp;
                DropDownList1.DataTextField = dsemp.Tables[0].Columns[1].ToString();
                DropDownList1.DataValueField = dsemp.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("-- select --", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}

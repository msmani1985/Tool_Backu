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

public partial class addjobpricecode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDataGrid();
        }
    }
    protected void TypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGrid();
    }
    protected void PriceCode_EditCommand(object source, DataGridCommandEventArgs e)
    {
        PriceCodeDataGrid.EditItemIndex = (int)e.Item.ItemIndex;
        LoadDataGrid();
    }
    protected void PriceCode_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string msg="";
        Datasource_IBSQL IBObj = new Datasource_IBSQL();
        try
        {
            
            TextBox jcno_2008 = (TextBox)e.Item.FindControl("pricecode2008");
            TextBox jcno_2009 = (TextBox)e.Item.FindControl("pricecode2009");
            if (jcno_2008.Text == "")
                jcno_2008.Text = "null";
            if (jcno_2009.Text == "")
                jcno_2009.Text = "null";
            string UQry="";
            if(TypeList.SelectedValue=="1")
                UQry = "UPDATE JOURNAL_DP SET JCNO_2008=" + jcno_2008.Text + ",JCNO_2009=" + jcno_2009.Text + " WHERE JOURNO=" + e.CommandArgument.ToString();
            else if(TypeList.SelectedValue=="2")
                UQry = "UPDATE BOOK_DP SET BCNO_2008=" + jcno_2008.Text + ",BCNO_2009=" + jcno_2009.Text + " WHERE BNO=" + e.CommandArgument.ToString();
            else if (TypeList.SelectedValue == "3")
                UQry = "UPDATE PROJECTS_DP SET PCNO_2008=" + jcno_2008.Text + ",PCNO_2009=" + jcno_2009.Text + " WHERE PROJECTNO=" + e.CommandArgument.ToString();

            bool flg=IBObj.RunQuery(UQry, "UpdatePriceCode");
            if (flg)
            {
                msg = "Updated Successfully";
                PriceCodeDataGrid.EditItemIndex = -1;
            }
            else
                msg = "Record is unable to update, Please try again";

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            IBObj = null;
            ClientScript.RegisterStartupScript(this.GetType(),"MsgBox","<script language='javascript'>alert('" + msg + "');</script>");
            LoadDataGrid();
        }
    }
    protected void PriceCode_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        PriceCodeDataGrid.EditItemIndex = -1;
        LoadDataGrid();
    }
    protected void LoadDataGrid()
    {
        DataSet ibds = new DataSet();
        Datasource_IBSQL IBObj = new Datasource_IBSQL();
        try
        {
            if (TypeList.SelectedValue == "1")
                ibds = IBObj.GetJournalcode("SELECT journo,JOURCODE,JOURNAME,JCNO_2008,JCNO_2009 FROM JOURNAL_DP");

            else if (TypeList.SelectedValue == "2")
            {
                ibds = IBObj.GetJournalcode("SELECT BNO,BNUMBER,BTITLE,BCNO_2008,BCNO_2009 FROM BOOK_DP where BINVOICED='N'");
                ibds.Tables[0].Columns[0].ColumnName = "journo";
                ibds.Tables[0].Columns[1].ColumnName = "Jourcode";
                ibds.Tables[0].Columns[2].ColumnName = "journame";
                ibds.Tables[0].Columns[3].ColumnName = "jcno_2008";
                ibds.Tables[0].Columns[4].ColumnName = "jcno_2009";
            }
            else if (TypeList.SelectedValue == "3")
            {
                ibds = IBObj.GetJournalcode("SELECT PROJECTNO,PCODE,PTITLE,PCNO_2008,PCNO_2009 FROM PROJECTS_DP WHERE PINVOICED='N'");
                ibds.Tables[0].Columns[0].ColumnName = "journo";
                ibds.Tables[0].Columns[1].ColumnName = "jourcode";
                ibds.Tables[0].Columns[2].ColumnName = "journame";
                ibds.Tables[0].Columns[3].ColumnName = "jcno_2008";
                ibds.Tables[0].Columns[4].ColumnName = "jcno_2009";
            }

            PriceCodeDataGrid.DataSource = ibds;
            PriceCodeDataGrid.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            IBObj = null;
            ibds = null;
        }
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for GeneralInform
/// </summary>
public class GeneralInform
{
    Datasource_IBSQL oIB = new Datasource_IBSQL();
    public GeneralInform()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //TypeStyle
    public DataSet GetTypeStyle()
    {
        return oIB.GetDataSet("SELECT * FROM Typestyle_DP ORDER BY TypeStyleno", "TypeStyle", CommandType.Text);
    }
    public DataSet GetTypeStyle1(int Typestyleno)
    {
        return oIB.GetDataSet("SELECT * FROM Typestyle_DP where typestyleno=" + Typestyleno + " ORDER BY TypeStyleno", "TypeStyle", CommandType.Text);
    }
    public void InsertTypeStyle(string Typestyle, string TypeDes)
    {
        oIB.ExcuteProc("insert into Typestyle_dp values (" + Typestyle + "," + TypeDes + ")");
    }
    public void UpdateTypeStyle(string Typestyle, string TypeDes, int TypeNo)
    {
        oIB.ExcuteProc("Update Typestyle_dp Set TypeStyle =" + Typestyle + ",TypeDescription=" + TypeDes + " where Typestyleno=" + TypeNo);
    }
    public void DeleteTypeStyle(int TypeNo)
    {
        oIB.ExcuteProc("Delete from Typestyle_dp  where Typestyleno=" + TypeNo);
    }
	//CoverMaterial
    public DataSet GetCoverMaterial()
    {
        return oIB.GetDataSet("SELECT * FROM CoverMaterial_dp ORDER BY Covmatno", "CoverMaterial", CommandType.Text);
    }
    public DataSet GetCoverMaterial1(int Covmatno)
    {
        return oIB.GetDataSet("SELECT * FROM CoverMaterial_dp where Covmatno=" + Covmatno + " ORDER BY Covmatno", "CoverMaterial", CommandType.Text);
    }
    public void InsertCoverMaterial(string Material)
    {
        oIB.ExcuteProc("insert into CoverMaterial_dp values (" + Material + ")");
    }
    public void UpdateCoverMaterial(string Material, int Covmatno)
    {
        oIB.ExcuteProc("Update CoverMaterial_dp Set Material =" + Material + " where Covmatno=" + Covmatno);
    }
    public void DeleteCoverMaterial(int Covmatno)
    {
        oIB.ExcuteProc("Delete from CoverMaterial_dp  where Covmatno=" + Covmatno);
    }
    //PaperType
    public DataSet GetPaperType()
    {
        return oIB.GetDataSet("SELECT * FROM PaperType_dp ORDER BY PaperTypeno", "PaperType", CommandType.Text);
    }
    public DataSet GetPaperType1(int PaperTypeno)
    {
        return oIB.GetDataSet("SELECT * FROM PaperType_dp where PaperTypeno=" + PaperTypeno + " ORDER BY PaperTypeno", "PaperType", CommandType.Text);
    }
    public void InsertPaperType(string PaperType)
    {
        oIB.ExcuteProc("insert into PaperType_dp values (" + PaperType + ")");
    }
    public void UpdatePaperType(string PaperType, int PaperTypeno)
    {
        oIB.ExcuteProc("Update PaperType_dp Set PaperType =" + PaperType + " where PaperTypeno=" + PaperTypeno);
    }
    public void DeletePaperType(int PaperTypeno)
    {
        oIB.ExcuteProc("Delete from PaperType_dp  where PaperTypeno=" + PaperTypeno);
    }
    //Trim Size
    public DataSet GetPageTrim()
    {
        return oIB.GetDataSet("SELECT PageTrimno,Trimsize+'/'+Trimcode  FROM PageTrim_dp ORDER BY PageTrimno", "PageTrim", CommandType.Text);
    }
    public DataSet GetPageTrim1(int PageTrimno)
    {
        return oIB.GetDataSet("SELECT * FROM PageTrim_dp where PageTrimno=" + PageTrimno + " ORDER BY PageTrimno", "PageTrim", CommandType.Text);
    }
    public void InsertPageTrim(string Trimcode,string Trimsize,string Format)
    {
        oIB.ExcuteProc("insert into PageTrim_dp values (" + Trimcode + "," + Trimsize + "," + Format + "," + null + ")");
    }
    public void UpdatePageTrim(string Trimcode,string Trimsize,string Format, int PageTrimno)
    {
        oIB.ExcuteProc("Update PageTrim_dp Set Trimcode =" + Trimcode + ",Trimsize=" + Trimsize + ",Format=" + Format + " where PageTrimno=" + PageTrimno);
    }
    public void DeletePageTrim(int PageTrimno)
    {
        oIB.ExcuteProc("Delete from PageTrim_dp  where PageTrimno=" + PageTrimno);
    }
    //Cover/Paper
    public DataSet GetPaperGSM()
    {
        return oIB.GetDataSet("SELECT * FROM PaperGSM_dp ORDER BY PaperGSMno", "PaperGSM", CommandType.Text);
    }
    public DataSet GetPaperGSM1(int PaperGSMno)
    {
        return oIB.GetDataSet("SELECT * FROM PaperGSM_dp where PaperGSMno=" + PaperGSMno + " ORDER BY PaperGSMno", "PaperGSM", CommandType.Text);
    }
    public void InsertPaperGSM(string GSMWeight)
    {
        oIB.ExcuteProc("insert into PaperGSM_dp values (" + GSMWeight + ")");
    }
    public void UpdatePaperGSM(string GSMWeight, int PaperGSMno)
    {
        oIB.ExcuteProc("Update PaperGSM_dp Set GSMWeight =" + GSMWeight + " where PaperGSMno=" + PaperGSMno);
    }
    public void DeletePaperGSM(int PaperGSMno)
    {
        oIB.ExcuteProc("Delete from PaperGSM_dp  where PaperGSMno=" + PaperGSMno);
    }
    //Digital Prod
    public DataSet GetDigital()
    {
        return oIB.GetDataSet("SELECT * FROM Digitalproducts_dp ORDER BY Digitalprodno", "Digitalproducts", CommandType.Text);
    }
    public DataSet GetDigital1(int Digitalprodno)
    {
        return oIB.GetDataSet("SELECT * FROM Digitalproducts_dp where Digitalprodno=" + Digitalprodno + " ORDER BY Digitalprodno", "Digitalproducts", CommandType.Text);
    }
    public void InsertDigital(string Prodcode,string ProdDescription)
    {
        oIB.ExcuteProc("insert into Digitalproducts_dp values (" + Prodcode + "," + ProdDescription + ")");
    }
    public void UpdateDigital(string Prodcode, string ProdDescription, int Digitalprodno)
    {
        oIB.ExcuteProc("Update Digitalproducts_dp Set Prodcode =" + Prodcode + ",ProdDescription=" + ProdDescription + " where Digitalprodno=" + Digitalprodno);
    }
    public void DeleteDigital(int Digitalprodno)
    {
        oIB.ExcuteProc("Delete from Digitalproducts_dp  where Digitalprodno=" + Digitalprodno);
    }
    //Typesetting platform
    public DataSet GetTypeset()
    {
        return oIB.GetDataSet("SELECT * FROM Typesetplatform_dp ORDER BY Tplatno", "Typesetting", CommandType.Text);
    }
    public DataSet GetTypeset1(int Tplatno)
    {
        return oIB.GetDataSet("SELECT * FROM Typesetplatform_dp where Tplatno=" + Tplatno + " ORDER BY Tplatno", "Typesetting", CommandType.Text);
    }
    public void InsertTypeset(string Tplatcode, string TplatDescription)
    {
        oIB.ExcuteProc("insert into Typesetplatform_dp values (" + Tplatcode + "," + TplatDescription + ")");
    }
    public void UpdateTypeset(string Tplatcode, string TplatDescription, int Tplatno)
    {
        oIB.ExcuteProc("Update Typesetplatform_dp Set Tplatcode =" + Tplatcode + ",TplatDescription=" + TplatDescription + " where Tplatno=" + Tplatno);
    }
    public void DeleteTypeset(int Tplatno)
    {
        oIB.ExcuteProc("Delete from Typesetplatform_dp  where Tplatno=" + Tplatno);
    }
    //Currency
    public DataSet GetCurrency()
    {
        return oIB.GetDataSet("SELECT * FROM Currency_dp ORDER BY Currno", "Currency", CommandType.Text);
    }
    public DataSet GetCurrency1(int Currno)
    {
        return oIB.GetDataSet("SELECT * FROM Currency_dp where Currno=" + Currno + " ORDER BY Currno", "Currency", CommandType.Text);
    }
    public void InsertCurrency(string Currname)
    {
        oIB.ExcuteProc("insert into Currency_dp values (" + Currname + ")");
    }
    public void UpdateCurrency(string Currname, int Currno)
    {
        oIB.ExcuteProc("Update Currency_dp Set Currname =" + Currname + " where Currno=" + Currno);
    }
    public void DeleteCurrency(int Currno)
    {
        oIB.ExcuteProc("Delete from Currency_dp  where Currno=" + Currno);
    }
    //Country 
    public DataSet GetCountry()
    {
        return oIB.GetDataSet("SELECT * FROM Country_dp ORDER BY ctyno", "Country", CommandType.Text);
    }
    public DataSet GetCountry1(int ctyno)
    {
        return oIB.GetDataSet("SELECT * FROM Country_dp where ctyno=" + ctyno + " ORDER BY ctyno", "Country", CommandType.Text);
    }
    public void InsertCountry(string ctyname, string ctyabbreviation)
    {
        oIB.ExcuteProc("insert into Country_dp values (" + ctyname + "," + ctyabbreviation + ")");
    }
    public void UpdateCountry(string ctyname, string ctyabbreviation, int ctyno)
    {
        oIB.ExcuteProc("Update Country_dp Set ctyname =" + ctyname + ",ctyabbreviation=" + ctyabbreviation + " where ctyno=" + ctyno);
    }
    public void DeleteCountry(int ctyno)
    {
        oIB.ExcuteProc("Delete from Country_dp  where ctyno=" + ctyno);
    }
    //Sales Lead Catgory
    public DataSet GetSales()
    {
        return oIB.GetDataSet("SELECT * FROM SalesleadCategory_dp ORDER BY slcatno", "SalesleadCategory", CommandType.Text);
    }
    public DataSet GetSales1(int slcatno)
    {
        return oIB.GetDataSet("SELECT * FROM SalesleadCategory_dp where slcatno=" + slcatno + " ORDER BY slcatno", "SalesleadCategory", CommandType.Text);
    }
    public void InsertSales(string slcatname)
    {
        oIB.ExcuteProc("insert into SalesleadCategory_dp values (" + slcatname + ")");
    }
    public void UpdateSales(string slcatname, int slcatno)
    {
        oIB.ExcuteProc("Update SalesleadCategory_dp Set slcatname =" + slcatname + " where slcatno=" + slcatno);
    }
    public void DeleteSales(int slcatno)
    {
        oIB.ExcuteProc("Delete from SalesleadCategory_dp  where slcatno=" + slcatno);
    }
    //Journal Category
    public DataSet GetJourCategory()
    {
        return oIB.GetDataSet("SELECT * FROM SubjectCategory_dp ORDER BY Subjcatno", "SubjectCategory", CommandType.Text);
    }
    public DataSet GetJourCategory1(int Subjcatno)
    {
        return oIB.GetDataSet("SELECT * FROM SubjectCategory_dp where Subjcatno=" + Subjcatno + " ORDER BY Subjcatno", "SubjectCategory", CommandType.Text);
    }
    public void InsertJourCategory(string category)
    {
        oIB.ExcuteProc("insert into SubjectCategory_dp values (" + category + ")");
    }
    public void UpdateJourCategory(string category, int Subjcatno)
    {
        oIB.ExcuteProc("Update SubjectCategory_dp Set category =" + category + " where Subjcatno=" + Subjcatno);
    }
    public void DeleteJourCategory(int Subjcatno)
    {
        oIB.ExcuteProc("Delete from SubjectCategory_dp  where Subjcatno=" + Subjcatno);
    }
}

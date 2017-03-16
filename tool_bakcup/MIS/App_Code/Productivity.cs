using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
/*
/// <summary>
/// Productive Report Base:
/// Created by: Royson
/// Creation Date: 18 Nov 08
/// Lastmodified: 06 arp 2010 - Royson
/// </summary>
 * */
public class ProductivityBase
{
    datasourceIBSQL oIB = new datasourceIBSQL();
    private string sSql = "";
    public ProductivityBase() { }
    public DataSet getTeams()
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "select * from DEPARTMENT_DP where DNO in(43,10008,10034,10029,10030,10031,10033,10040,10041)";
            ds = oIB.GetDataSet1(sSql);
            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
        }
    }
    public DataSet getAllEmployees()
    {
        DataSet ds = new DataSet();
        string[] aEmpinfo = new string[2];
        sSql = "";
        try
        {
            
            sSql = "select emp_id,upper(f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname)) as column1 from Employee_dp where status=1 and emp_id is not null and emp_id <>0 order by 2";            
            ds = oIB.GetDataSet1(sSql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);;
        }
        return ds;
    }
    public string[] getEmployeeByID(string sEmpid)
    {
        DataSet ds=new DataSet();
        string[] aEmpinfo = new string[3];
        sSql = "";
        try
        {
            //sSql = "select emp_id,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column1,dname from Employee_dp e inner join department_dp d on e.dno=d.dno where emp_id=" + sEmpid;
            sSql = "select Empno,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column1,dname from Employee_dp e inner join department_dp d on e.dno=d.dno where emp_id=" + sEmpid;
            ds = oIB.GetDataSet1(sSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                aEmpinfo[0] = ds.Tables[0].Rows[0][1].ToString().Trim();
                aEmpinfo[1] = ds.Tables[0].Rows[0][2].ToString().Trim();
                aEmpinfo[2] = ds.Tables[0].Rows[0][0].ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            return aEmpinfo;
        }
        return aEmpinfo;
    }

    public DataSet getProductivityByEmployee(string sEmpcode, string sStartDate, string sEnddate)
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            //sSql = "select cast(LEDate as date) as column1,PCODE,MPCODE,PMCNO,IMAGESCOMPLETED,LEDate, LEndDate, emp_id,Emp_Fname, PTitle, STypeName, SNAME from PLogEvents_dp l" +
            //    " inner Join Projects_dp p on l.ProjectNo = p.ProjectNo" +
            //    " left outer join project_modules_dp pm on l.mprojno = pm.mprojno" +
            //    " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
            //    " Inner Join Stype_dp s ON p.StypeNo = s.StypeNO" +
            //    " Left Outer JOin Stage_dp st ON l.SNo = st.SNO" +
            //    " where EmpNo IN (SELECT EmpNo FROM Employee_dp where Emp_id = " + sEmpcode + ")";
            //if (sStartDate != "" && sEnddate != "")
            //    sSql += "and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            //sSql += " order by Emp_Fname,ledate";
            sSql += "select cast(LEDate as date) as column1,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2,PCODE,mpcode,pmcno,imagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname, PTitle, STypeName, SNAME from PLogEvents_dp l" +
            " inner Join Projects_dp p on l.ProjectNo = p.ProjectNo" +
            " left outer join project_modules_dp pm on l.mprojno = pm.mprojno" +
            " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Inner Join Stype_dp s ON p.StypeNo = s.StypeNO" +
            " Left Outer JOin Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (" + sEmpcode + ") and l.istimesheet='Y'";
            if (sStartDate != "" && sEnddate != "")
                sSql += " and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            sSql += " union" +
            " select cast(LEDate as date) as column1,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2,cast(AArticleCode as char(50)) as AArticleCode," +
            " cast(IIssueNo as char(50)) as IIssueNo,arealnoofpages,pagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname, cast(AArticleCode as char(255)) as artitle, STypeName, SNAME from LoggedEvents_dp l" +
            " inner Join Article_dp a on l.ANo = a.ANo" +
            " left outer join Issue_dp i on a.INo = i.INo" +
            " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Inner Join Stype_dp s ON a.StypeNo = s.StypeNO" +
            " Left Outer JOin Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (" + sEmpcode + ") and l.istimesheet='Y'";
            if (sStartDate != "" && sEnddate != "")
                sSql += " and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";            
            sSql += " union" +
            " select cast(LEDate as date) as column1, f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2,cast(BNumber as char(50)) as BNumber," +
            " cast('' as char(50)) as bookjobref, bnoofpages,pagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname,cast(btitle as char(255)) as btitle, STypeName, SNAME from BLogEvents_dp l" +
            " inner Join Book_dp b on l.BNo = b.BNo" +
            " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Inner Join Stype_dp s ON b.StypeNo = s.StypeNO" +
            " Left Outer JOin Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (" + sEmpcode + ") and l.istimesheet='Y'";
            if (sStartDate != "" && sEnddate != "")
                sSql += " and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            //Break events
            sSql += " union" + //Journal
            " select cast(LEDate as date) as column1,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2," +
            " cast('' as char(50)) as AArticleCode, cast('' as char(50)) as IIssueNo,cast('0' as integer) as arealnoofpages,cast('0' as integer) as pagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname, cast('' as char(255)) as artitle, cast('' as char(20)) as STypeName, SNAME from LoggedEvents_dp l" +
            " Inner join Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Left Outer join Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (" + sEmpcode + ")" +
            " and l.ano is null and l.ino is null and l.istimesheet='Y'";
            if (sStartDate != "" && sEnddate != "")
                sSql += " and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            sSql += " union" + //Project
            " select cast(LEDate as date) as column1,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2," +
            " cast('' as char(50)) as AArticleCode, cast('' as char(50)) as IIssueNo,cast('0' as integer) as arealnoofpages,cast('0' as integer) as pagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname, cast('' as char(255)) as artitle, cast('' as char(20)) as STypeName, SNAME from PLogEvents_dp l" +
            " Inner join Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Left Outer join Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (" + sEmpcode + ")" +
            " and l.projectno is null and l.mprojno is null and l.istimesheet='Y'";
            if (sStartDate != "" && sEnddate != "")
                sSql += " and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            sSql += " union" + //Book
            " select cast(LEDate as date) as column1,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2," +
            " cast('' as char(50)) as AArticleCode, cast('' as char(50)) as IIssueNo,cast('0' as integer) as arealnoofpages,cast('0' as integer) as pagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname, cast('' as char(255)) as artitle, cast('' as char(20)) as STypeName, SNAME from BLogEvents_dp l" +
            " Inner join Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Left Outer join Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (" + sEmpcode + ")" +
            " and l.bno is null and l.dno is null and l.istimesheet='Y'";
            if (sStartDate != "" && sEnddate != "")
                sSql += " and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            //sSql += " order by 9,1";
            sSql += " order by 7";
            ds = oIB.GetDataSet1(sSql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getProductivityByTeam(string sTeamID, string sStartDate, string sEnddate)
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            //sSql = "select cast(LEDate as date) as column1,PCODE,MPCODE,PMCNO,IMAGESCOMPLETED,LEDate, LEndDate, emp_id,Emp_Fname, PTitle, STypeName, SNAME from PLogEvents_dp l" +
            //    " inner Join Projects_dp p on l.ProjectNo = p.ProjectNo" +
            //    " left outer join project_modules_dp pm on l.mprojno = pm.mprojno" +
            //    " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
            //    " Inner Join Stype_dp s ON p.StypeNo = s.StypeNO" +
            //    " Left Outer JOin Stage_dp st ON l.SNo = st.SNO" +
            //    " where EmpNo IN (SELECT EmpNo FROM Employee_dp where dno = " + sTeamID + ")";
            //if (sStartDate != "" && sEnddate != "")
            //    sSql += "and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            //sSql += " order by Emp_Fname,ledate";
            sSql += "select cast(LEDate as date) as column1,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2,PCODE,mpcode,pmcno,imagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname, PTitle, STypeName, SNAME from PLogEvents_dp l" +
            " inner Join Projects_dp p on l.ProjectNo = p.ProjectNo" +
            " left outer join project_modules_dp pm on l.mprojno = pm.mprojno" +
            " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Inner Join Stype_dp s ON p.StypeNo = s.StypeNO" +
            " Left Outer JOin Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (SELECT EmpNo FROM Employee_dp where dno = " + sTeamID + ")";
            if (sStartDate != "" && sEnddate != "")
                sSql += " and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            sSql += " union" +
            " select cast(LEDate as date) as column1,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2,cast(AArticleCode as char(50)) as AArticleCode," +
            " cast(IIssueNo as char(50)) as IIssueNo,arealnoofpages,pagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname, cast(AArticleCode as char(255)) as artitle, STypeName, SNAME from LoggedEvents_dp l" +
            " inner Join Article_dp a on l.ANo = a.ANo" +
            " left outer join Issue_dp i on a.INo = i.INo" +
            " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Inner Join Stype_dp s ON a.StypeNo = s.StypeNO" +
            " Left Outer JOin Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (SELECT EmpNo FROM Employee_dp where dno =  " + sTeamID + ") and l.istimesheet='Y'";
            if (sStartDate != "" && sEnddate != "")
                sSql += " and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            sSql += "union" +
            " select cast(LEDate as date) as column1, f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2,cast(BNumber as char(50)) as BNumber," +
            " cast('' as char(50)) as bookjobref, bnoofpages,pagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname,cast(btitle as char(255)) as btitle, STypeName, SNAME from BLogEvents_dp l" +
            " inner Join Book_dp b on l.BNo = b.BNo" +
            " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Inner Join Stype_dp s ON b.StypeNo = s.StypeNO" +
            " Left Outer JOin Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (SELECT EmpNo FROM Employee_dp where dno =  " + sTeamID + ")";
            if (sStartDate != "" && sEnddate != "")
                sSql += " and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            //Break events
            sSql += " union" + //Journal
            " select cast(LEDate as date) as column1,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2," +
            " cast('' as char(50)) as AArticleCode, cast('' as char(50)) as IIssueNo,cast('0' as integer) as arealnoofpages,cast('0' as integer) as pagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname, cast('' as char(255)) as artitle, cast('' as char(20)) as STypeName, SNAME from LoggedEvents_dp l" +
            " Inner join Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Left Outer join Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (SELECT EmpNo FROM Employee_dp where dno =  " + sTeamID + ")" +
            " and l.ano is null and l.ino is null and l.istimesheet='Y'";
            sSql += " union" + //Project
            " select cast(LEDate as date) as column1,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2," +
            " cast('' as char(50)) as AArticleCode, cast('' as char(50)) as IIssueNo,cast('0' as integer) as arealnoofpages,cast('0' as integer) as pagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname, cast('' as char(255)) as artitle, cast('' as char(20)) as STypeName, SNAME from PLogEvents_dp l" +
            " Inner join Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Left Outer join Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (SELECT EmpNo FROM Employee_dp where dno =  " + sTeamID + ")" +
            " and l.projectno is null and l.mprojno is null and l.istimesheet='Y'";
            if (sStartDate != "" && sEnddate != "")
                sSql += " and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            sSql += " union" + //Book
            " select cast(LEDate as date) as column1,f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as column2," +
            " cast('' as char(50)) as AArticleCode, cast('' as char(50)) as IIssueNo,cast('0' as integer) as arealnoofpages,cast('0' as integer) as pagescompleted,LEDate," +
            " LEndDate, emp_id,Emp_Fname, cast('' as char(255)) as artitle, cast('' as char(20)) as STypeName, SNAME from BLogEvents_dp l" +
            " Inner join Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Left Outer join Stage_dp st ON l.SNo = st.SNO" +
            " where EmpNo IN (SELECT EmpNo FROM Employee_dp where dno =  " + sTeamID + ")" +
            " and l.bno is null and l.dno is null and l.istimesheet='Y'";
            if (sStartDate != "" && sEnddate != "")
                sSql += " and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            //sSql += " order by 9,1";
            sSql += " order by 9,7,1";
            ds = oIB.GetDataSet1(sSql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getJoblogReport(string sJobcode, string cCategory)
    {
        if (sJobcode != "")
        {
            switch (cCategory)
            {
                case "I": return this.getJLReportByIssue(sJobcode);
                case "A": return this.getJLReportByArticle(sJobcode);
                case "B": return this.getJLReportByBook(sJobcode);
                case "C": return this.getJLReportByChapter(sJobcode);
                case "P": return this.getJLReportByProject(sJobcode);
                case "M": return this.getJLReportByModule(sJobcode);
                default: break;
            }
        }
        return null;
    }
    private DataSet getJLReportByIssue(string sJobcode)
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            string[] ajob = sJobcode.ToUpper().Split(' ');
            sSql = "select cast(LEDate as date) as column1,f_ltrim(ano) as column2,IIssueNo," +
            " AArticleCode, j.journame,LEDate, LEndDate, Emp_Fname, Emp_id, STypeName,SNAME,custname" +
            " from LoggedEvents_dp l" +
            " inner Join Article_dp a on l.ANo = a.ANo" +
            " left outer join Issue_dp i on a.INo = i.INo" +
            " inner join journal_dp j on a.journo = j.journo" +
            " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Inner Join Stype_dp s ON a.StypeNo = s.StypeNO" +
            " Left Outer JOin Stage_dp st ON l.SNo = st.SNO  and l.sno not in (93,94,95,99,100,101,102,116)" +
            " inner join customer_dp c on c.custno = j.custno";
            if (ajob.Length > 1) sSql += " where l.istimesheet='Y' and jourcode='" + ajob[0] + "' and iissueno='" + ajob[1] + "'";
            else sSql += " where l.istimesheet='Y' and jourcode='" + sJobcode.ToUpper() + "'";
            ds = oIB.GetDataSet1(sSql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    private DataSet getJLReportByArticle(string sJobcode)
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "select cast(LEDate as date) as column1,f_ltrim(ano) as column2,IIssueNo," +
            " AArticleCode, j.journame,LEDate, LEndDate, Emp_Fname, Emp_id, STypeName,SNAME,custname" +
            " from LoggedEvents_dp l" +
            " inner Join Article_dp a on l.ANo = a.ANo" +
            " left outer join Issue_dp i on a.INo = i.INo" +
            " inner join journal_dp j on a.journo = j.journo" +
            " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Inner Join Stype_dp s ON a.StypeNo = s.StypeNO" +
            " Left Outer JOin Stage_dp st ON l.SNo = st.SNO  and l.sno not in (93,94,95,99,100,101,102,116)" +
            " inner join customer_dp c on c.custno = j.custno" +
            " where l.istimesheet='Y' and aarticlecode='" + sJobcode.ToUpper() + "'";
            ds = oIB.GetDataSet1(sSql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    private DataSet getJLReportByBook(string sJobcode)
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "select cast(LEDate as date) as column1, f_ltrim(bno) as column2,BNumber,bnoofpages," +
            " LEDate, LEndDate, Emp_Fname, Emp_id,btitle, STypeName, SNAME, custname" +
            " from BLogEvents_dp l" +
            " inner Join Book_dp b on l.BNo = b.BNo" +
            " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
            " Inner Join Stype_dp s ON b.StypeNo = s.StypeNO" +
            " Left Outer JOin Stage_dp st ON l.SNo = st.SNO  and l.sno not in (93,94,95,99,100,101,102,116)" +
            " inner join customer_dp c on c.custno = b.custno" +
            " where bnumber='" + sJobcode.ToUpper() + "'";
            sSql += " order by ledate";
            ds = oIB.GetDataSet1(sSql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    private DataSet getJLReportByChapter(string sJobcode)
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "select cast(LEDate as date) as column1, f_ltrim(bno) as column2,BNumber,bnoofpages," +
           " LEDate, LEndDate, Emp_Fname, Emp_id,btitle, STypeName, SNAME, custname" +
           " from BLogEvents_dp l" +
           " inner Join Book_dp b on l.BNo = b.BNo" +
           " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
           " Inner Join Stype_dp s ON b.StypeNo = s.StypeNO" +
           " Left Outer JOin Stage_dp st ON l.SNo = st.SNO  and l.sno not in (93,94,95,99,100,101,102,116)" +
           " inner join customer_dp c on c.custno = b.custno" +
           " where bnumber='" + sJobcode.ToUpper() + "'";
            sSql += " order by ledate";
            ds = oIB.GetDataSet1(sSql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    private DataSet getJLReportByProject(string sJobcode)
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "select cast(LEDate as date) as column1,f_ltrim(l.projectno) as column2,projectno,PCODE,MPCODE,PMCNO,IMAGESCOMPLETED,LEDate, LEndDate, emp_id,Emp_Fname, PTitle,STypeName, SNAME, custname from PLogEvents_dp l" +
                " inner Join Projects_dp p on l.ProjectNo = p.ProjectNo" +
                " left outer join project_modules_dp pm on l.mprojno = pm.mprojno" +
                " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
                " Inner Join Stype_dp s ON p.StypeNo = s.StypeNO " +
                " Left Outer JOin Stage_dp st ON l.SNo = st.SNO and l.sno not in (93,94,95,99,100,101,102,116)" +
                " inner join customer_dp c on c.custno = p.custno" +
                " where Pcode='" + sJobcode.ToUpper() + "'";
            //if (sStartDate != "" && sEnddate != "")
            //    sSql += "and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            sSql += " order by MPCODE,ledate";
            ds = oIB.GetDataSet1(sSql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    private DataSet getJLReportByModule(string sJobcode)
    {
        DataSet ds = new DataSet();
        sSql = "";
        try
        {
            sSql = "select cast(LEDate as date) as column1,f_ltrim(l.mprojno) as column2,PCODE,MPCODE,PMCNO,IMAGESCOMPLETED,LEDate, LEndDate, emp_id,Emp_Fname, PTitle,STypeName, SNAME,l.projectno, custname from PLogEvents_dp l" +
                " inner Join Projects_dp p on l.ProjectNo = p.ProjectNo" +
                " left outer join project_modules_dp pm on l.mprojno = pm.mprojno" +
                " Inner JOin Employee_dp e ON l.EmpNo = e.EmpNo" +
                " Inner Join Stype_dp s ON p.StypeNo = s.StypeNO" +
                " Left Outer JOin Stage_dp st ON l.SNo = st.SNO and l.sno not in (93,94,95,99,100,101,102,116)" +
                " inner join customer_dp c on c.custno = p.custno" +
                " where MPcode='" + sJobcode.ToUpper() + "'";
            //if (sStartDate != "" && sEnddate != "")
            //    sSql += "and (LeDate between '" + sStartDate + " 00:00:00' and '" + sEnddate + " 23:59:59')";
            sSql += " order by MPCODE,ledate";
            ds = oIB.GetDataSet1(sSql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public ExportOptions ReportExportOption(ReportDocument oReport, ExportFormatType oExportTypes)
    {
        return this.ReportExportOption(oReport, "", oExportTypes, 0, 0);
    }
    public ExportOptions ReportExportOption(ReportDocument oReport, string cFileName, ExportFormatType oExportTypes, int nFirstPage, int nLastPage)
    {
        try
        {
            ExportOptions oExportOptions = new ExportOptions();
            PdfRtfWordFormatOptions oFormatOptions = ExportOptions.CreatePdfRtfWordFormatOptions();
            DiskFileDestinationOptions oDestinationOptions = ExportOptions.CreateDiskFileDestinationOptions();

            switch (oExportTypes)
            {
                case ExportFormatType.PortableDocFormat:
                    oExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;

                    //if (nFirstPage > 0 && nLastPage > 0)
                    //{
                    //    oPDFFormatOptions.FirstPageNumber = nFirstPage;
                    //    oPDFFormatOptions.LastPageNumber = nLastPage;
                    //    oPDFFormatOptions.UsePageRange = true;
                    //}

                    PdfRtfWordFormatOptions oPDFFormatOptions = ExportOptions.CreatePdfRtfWordFormatOptions();
                    oExportOptions.ExportFormatOptions = oPDFFormatOptions;
                    break;

                case ExportFormatType.WordForWindows:
                    oExportOptions.ExportFormatType = ExportFormatType.WordForWindows;
                    PdfRtfWordFormatOptions oWordFormatOptions = ExportOptions.CreatePdfRtfWordFormatOptions();
                    if (nFirstPage > 0 && nLastPage > 0)
                    {
                        oWordFormatOptions.FirstPageNumber = nFirstPage;
                        oWordFormatOptions.LastPageNumber = nLastPage;
                        oWordFormatOptions.UsePageRange = true;
                    }
                    oExportOptions.ExportFormatOptions = oWordFormatOptions;
                    break;

                case ExportFormatType.Excel:
                    oExportOptions.ExportFormatType = ExportFormatType.Excel;
                    ExcelFormatOptions oExcelFormatOptions = ExportOptions.CreateExcelFormatOptions();
                    oExcelFormatOptions.ExcelUseConstantColumnWidth = false;                    

                    if (nFirstPage > 0 && nLastPage > 0)
                    {
                        oExcelFormatOptions.FirstPageNumber = nFirstPage;
                        oExcelFormatOptions.LastPageNumber = nLastPage;
                        oExcelFormatOptions.UsePageRange = true;
                    }
                    oExportOptions.ExportFormatOptions = oExcelFormatOptions;
                    break;

                case ExportFormatType.ExcelRecord:
                    oExportOptions.ExportFormatType = ExportFormatType.ExcelRecord;
                    ExcelFormatOptions oExcelFormatOptions1 = ExportOptions.CreateExcelFormatOptions();
                    oExcelFormatOptions1.ExcelUseConstantColumnWidth = true;
                    oExcelFormatOptions1.ExcelConstantColumnWidth = 40;                    

                    if (nFirstPage > 0 && nLastPage > 0)
                    {
                        oExcelFormatOptions1.FirstPageNumber = nFirstPage;
                        oExcelFormatOptions1.LastPageNumber = nLastPage;
                        oExcelFormatOptions1.UsePageRange = true;
                    }
                    oExportOptions.ExportFormatOptions = oExcelFormatOptions1;
                    break;

                case ExportFormatType.HTML40:
                    oExportOptions.ExportFormatType = ExportFormatType.HTML40;
                    HTMLFormatOptions oHTMLFormatOptions = ExportOptions.CreateHTMLFormatOptions();
                    if (nFirstPage > 0 && nLastPage > 0)
                    {
                        oHTMLFormatOptions.FirstPageNumber = nFirstPage;
                        oHTMLFormatOptions.LastPageNumber = nLastPage;
                        oHTMLFormatOptions.UsePageRange = true;
                    }
                    oExportOptions.ExportFormatOptions = oHTMLFormatOptions;
                    break;
            }
            oExportOptions.ExportDestinationOptions = oDestinationOptions;
            oExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            return oExportOptions;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public DataSet getArticleIssueWIP(string sCustno)
    {
        DataSet ds = new DataSet();
        string[] aEmpinfo = new string[2];
        sSql = "";
        try
        {
            sSql = "Select ARTICLE_DP.ANO,'A' as jobtype,Journal_dp.JOURCODE,AARTICLECODE," +
                " F_STRIPTIME(ARTICLE_DP.aduedate) AS aduedate," +
                " ARTICLE_DP.STNO,STATUS_DP.STDESCRIPTION, STYPE_DP.stypename,CUSTOMER_DP.CUSTNO FROM ARTICLE_DP" +
                " LEFT OUTER JOIN  JOURNAL_DP ON article_dp.journo=journal_dp.journo" +
                " LEFT OUTER JOIN  ARTICLE_PRIOR_DP ON ARTICLE_DP.ARPNO=ARTICLE_PRIOR_DP.ARPNO" +
                " LEFT OUTER JOIN  ISSUE_DP ON ARTICLE_DP.INO=ISSUE_DP.INO" +
                " LEFT OUTER JOIN DEPARTMENT_DP ON ARTICLE_DP.CURRENT_DEPARTMENT=DEPARTMENT_DP.DNO" +
                " LEFT OUTER JOIN  EMPLOYEE_DP ON ARTICLE_DP.CURRENT_EMPLOYEE =EMPLOYEE_DP.EMPNO" +
                " LEFT OUTER JOIN STATUS_DP ON ARTICLE_DP.STNO = STATUS_DP.STNO" +
                " LEFT OUTER JOIN ArticleDocType_DP ON ARTICLE_DP.ADNO=ArticleDocType_DP.ADNO" +
                " LEFT OUTER JOIN IssuePositionCoverType_DP ON ARTICLE_DP.IPCTNO = IssuePositionCoverType_DP.IPCTNO" +
                " LEFT OUTER JOIN Category_DP ON ARTICLE_DP.CATNO = Category_Dp.Catno" +
                " LEFT OUTER JOIN ArticleProdItemType_DP ON ARTICLE_DP.APINO = ArticleProdItemType_DP.APINO" +
                " LEFT OUTER JOIN  ArticleProdType_DP ON ARTICLE_DP.APTNO = ArticleProdType_DP.APTNO" +
                " LEFT OUTER JOIN  STYPE_DP ON ARTICLE_DP.STYPENO = STYPE_DP.STYPENO" +
                " LEFT OUTER JOIN  NUMBERSYSTEM_DP ON ARTICLE_DP.NSNO = NUMBERSYSTEM_DP.NSNO" +
                " LEFT OUTER JOIN CUSTOMER_DP Customer_dp ON (Journal_dp.CUSTNO = Customer_dp.CUSTNO)" +
                " where 1=1";
            if (sCustno != "10066") sSql += " and custno=" + sCustno;
            sSql += " and article_dp.completed_flag='N' and (article_dp.adno=3 or article_dp.adno=10012)" +
                " Union" +
                " Select INO,'I' as jobtype,JOURCODE,cast(IISSUENO as char(20)) as IISSUENO," +
                " F_STRIPTIME(iduedate) as iduedate," +
                " issue_dp.stno,Status_DP.stdescription, STYPE_DP.STYPENAME,CUSTOMER_DP.CUSTNO FROM ISSUE_DP" +
                " LEFT JOIN JOURNAL_DP ON journal_dp.journo=issue_dp.journo" +
                " LEFT JOIN CUSTOMER_DP ON JOURNAL_DP.CUSTNO=CUSTOMER_DP.CUSTNO" +
                " LEFT JOIN Status_DP ON status_dp.stno=issue_dp.stno" +
                " LEFT JOIN Stage_DP ON stage_dp.sno=issue_dp.sno" +
                " LEFT JOIN Stype_DP ON stype_dp.stypeno=issue_dp.stypeno" +
                " LEFT JOIN Article_prior_DP ON Article_prior_DP.arpno=issue_dp.arpno where 1=1";
            if (sCustno != "10066") sSql += " and custno=" + sCustno;
            sSql += " and completed_flag='N'" +
                " order by 2,9";
            ds = oIB.GetDataSet1(sSql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getArticleIssueWIPSql(string sCustno)
    {
        datasourceSQL oSql = new datasourceSQL();
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetDailyReport]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustno);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getWeeklyReport(string sStartdate, string sEnddate)
    {
        datasourceSQL oSql = new datasourceSQL();
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetWeeklyReport]";
            param[0] = oSql.NewParameter("@startdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStartdate);
            param[1] = oSql.NewParameter("@enddate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEnddate);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getMonthlyReport(string sJobTypeID, string sStartdate, string sEnddate)
    {
        datasourceSQL oSql = new datasourceSQL();
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGetMonthlyReport]";
            param[0] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[1] = oSql.NewParameter("@start_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStartdate);
            param[2] = oSql.NewParameter("@end_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEnddate);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
}

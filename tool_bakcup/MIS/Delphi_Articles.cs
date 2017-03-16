using System;
using System.Collections.Generic;

using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Delphi_Articles
/// </summary>
public class Delphi_Articles
{
	private Delphi_Customer oCust;
	private Delphi_Common oCom;
	private datasourceIBSQL oSql;
	private datasourceSQL oSql1;
	private string sSql = "";
	public Delphi_Articles()
	{
		oCust = new Delphi_Customer();
		oCom = new Delphi_Common();
		oSql = new datasourceIBSQL();
		oSql1 = new datasourceSQL();
	}

	public DataSet getCustomers() { return this.oCust.getAllCustomers(); }
	public DataSet getGraphicTypes() { return this.oCom.getGraphicTypes(); }
	public DataSet getFigureTypes() { return this.oCom.getFigureTypes(); }
	public DataSet getArticleStages() { return this.oCom.getStagesByJobType(5); }
	public DataSet getDocTypes() { return this.oCom.getDocumentTypes(""); }
	public DataSet getDocItemTypes(string sDocTypeID) { return this.oCom.getDocumentItemTypes(sDocTypeID); }
	public DataSet getCategoryTypes() { return this.oCom.getCategoryTypes(); }
	public DataSet getOnHoldTypes() { return this.oCom.getOnHoldTypes(); }
	public DataSet getStageTypes() { return this.oCom.getStageTypes("5"); }
	public DataSet getSalesGroup() { return this.oCom.getSalesJobGroups(); }
	public bool IsInvoiced(string sJobID) { return oCom.IsJobInvoiced(sJobID, "5"); }

	public DataSet getSubDocType()
	{
		sSql = "";
		DataSet ds = new DataSet();
		//SqlParameter[] para = new SqlParameter[1];
		try
		{
			sSql = "spGetSubDocType";
			ds = oSql.FillDataSet_SP(sSql, null);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getNumSys()
	{
		sSql = "";
		DataSet ds = new DataSet();
		//SqlParameter[] para = new SqlParameter[1];
		try
		{
			sSql = "spGetNumSys";
			ds = oSql.FillDataSet_SP(sSql, null);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getArtFig()
	{
		sSql = "";
		DataSet ds = new DataSet();
		try
		{
			sSql = "spGetArtFig";
			ds = oSql.FillDataSet_SP(sSql, null);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getArtImg()
	{
		sSql = "";
		DataSet ds = new DataSet();
		try
		{
			sSql = "spGetArtImg";
			ds = oSql.FillDataSet_SP(sSql, null);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getProdType()
	{
		sSql = "";
		DataSet ds = new DataSet();
		//SqlParameter[] para = new SqlParameter[1];
		try
		{
			sSql = "spGetProdType";
			ds = oSql.FillDataSet_SP(sSql, null);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getProdItemType()
	{
		sSql = "";
		DataSet ds = new DataSet();
		//SqlParameter[] para = new SqlParameter[1];
		try
		{
			sSql = "spGetProdItemType";
			ds = oSql.FillDataSet_SP(sSql, null);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getCatDescription()
	{
		sSql = "";
		DataSet ds = new DataSet();
		//SqlParameter[] para = new SqlParameter[1];
		try
		{
			sSql = "spGetCatDescription";
			ds = oSql.FillDataSet_SP(sSql, null);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getPriority()
	{
		sSql = "";
		DataSet ds = new DataSet();
		//SqlParameter[] para = new SqlParameter[1];
		try
		{
			sSql = "SPGETPRIORITY";
			ds = oSql.FillDataSet_SP(sSql, null);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getCurrentStatus()
	{
		sSql = "";
		DataSet ds = new DataSet();
		//SqlParameter[] para = new SqlParameter[1];
		try
		{
			sSql = "spGetCurrentStatus";
			ds = oSql.FillDataSet_SP(sSql, null);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getJournalsByCustomer(string sCustomerID)
	{
		sSql = "";
		DataSet ds = new DataSet();
		SqlParameter[] param = new SqlParameter[1];
		try
		{
			sSql = "[spGetJournalList]";
			param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getArticleStagesByJournal(string sJournalID)
	{
		sSql = "";
		DataSet ds = new DataSet();

		SqlParameter[] param = new SqlParameter[2];
		try
		{
			sSql = "[spGET_NEXT_STAGE]";
			param[0] = oSql.NewParameter("@journalid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
			param[1] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "5");
			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getArticles(string sArticleName, string sArticleType, char cCompleteFlag)
	{
		sSql = "";
		DataSet ds = new DataSet();
		SqlParameter[] param = new SqlParameter[3];
		try
		{
			sSql = "[spGetArticlesList_new]";
			param[0] = oSql.NewParameter("@ARTICLENAME", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleName);
			param[1] = oSql.NewParameter("@ARTICLETYPE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, 5);
			param[2] = oSql.NewParameter("@COMPLETED_FLAG", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, cCompleteFlag);
			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	//Kalimuthu 08/13/2014
    public DataSet getArticles(string sArticleName, string sArticleType, char cCompleteFlag, string sArticleMonth, string sArticleYear)
	{
		sSql = "";
		DataSet ds = new DataSet();
		SqlParameter[] param = new SqlParameter[5];
		try
		{
			sSql = "[spGetArticlesDespatchList]";
			param[0] = oSql.NewParameter("@ARTICLENAME", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleName);
			param[1] = oSql.NewParameter("@ARTICLETYPE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, 5);
			param[2] = oSql.NewParameter("@COMPLETED_FLAG", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, cCompleteFlag);
			param[3] = oSql.NewParameter("@Month", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleMonth);
			param[4] = oSql.NewParameter("@Year", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleYear);
			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getArticles(string[] aArtDetails)
	{
		sSql = "";
		DataSet ds = new DataSet();
		SqlParameter[] param = new SqlParameter[26];
		try
		{
			sSql = "[spGetArticlesList]";
			param[0] = oSql.NewParameter("@ArticleName", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[0]);
			param[1] = oSql.NewParameter("@ArticleType", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aArtDetails[1]);
			param[2] = oSql.NewParameter("@Completed_flag", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[2]);
			param[3] = oSql.NewParameter("@SearchMode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[3]);
			param[4] = oSql.NewParameter("@SearchFor", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[4]);
			param[5] = oSql.NewParameter("@JourCode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[5]);
			param[6] = oSql.NewParameter("@JourCodeExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[6]);
			param[7] = oSql.NewParameter("@ArticleCode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[7]);
			param[8] = oSql.NewParameter("@ArticleCodeExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[8]);
			param[9] = oSql.NewParameter("@IssueNumber", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[9]);
			param[10] = oSql.NewParameter("@IssueNumberExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[10]);
			param[11] = oSql.NewParameter("@IssueOnHold", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aArtDetails[11]);
			param[12] = oSql.NewParameter("@RecExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[12]);
			param[13] = oSql.NewParameter("@RecDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[13]);
			param[14] = oSql.NewParameter("@RecDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[14]);
			param[15] = oSql.NewParameter("@DueExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[15]);
			param[16] = oSql.NewParameter("@DueDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[16]);
			param[17] = oSql.NewParameter("@DueDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[17]);
			param[18] = oSql.NewParameter("@HlfDueExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[18]);
			param[19] = oSql.NewParameter("@HlfDueDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[19]);
			param[20] = oSql.NewParameter("@HlfDueDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[20]);
			param[21] = oSql.NewParameter("@CatsDueExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[21]);
			param[22] = oSql.NewParameter("@CatsDueDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[22]);
			param[23] = oSql.NewParameter("@CatsDueDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[23]);
			param[24] = oSql.NewParameter("@stage_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[24]);
			param[25] = oSql.NewParameter("@customer_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[25]);
			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getArticleDetailsByID(string sArticleID)
	{
		sSql = "";
		DataSet ds = new DataSet();
		SqlParameter[] param = new SqlParameter[1];
		try
		{
			sSql = "spGetArticle_1";
			param[0] = oSql.NewParameter("@ArticleID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleID);
			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getArticleStageByID(string sArticleID, string sStageID)
	{
		sSql = "";
		DataSet ds = new DataSet();
		SqlParameter[] param = new SqlParameter[2];
		try
		{
			sSql = "[spGetArticleStage]";
			param[0] = oSql.NewParameter("@ArticleID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleID);
			param[1] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStageID);
			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getArticleEvents(string sArticleID)
	{
		sSql = "";
		DataSet ds = new DataSet();
		SqlParameter[] param = new SqlParameter[1];
		try
		{
			sSql = "[spGetLoggedEvents]";
			//  param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "");
			param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleID);

			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getArticleComments(string sArticleID)
	{
		sSql = "";
		DataSet ds = new DataSet();
		SqlParameter[] param = new SqlParameter[1];
		try
		{
			sSql = "[spGetCommentsHistory]";
			// param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "");
			param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleID);
			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getArticleHoldHistory(string sArticleID)
	{
		sSql = "";
		DataSet ds = new DataSet();
		SqlParameter[] param = new SqlParameter[1];
		try
		{
			sSql = "[spGetOnHoldHistory]";
			//param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "");
			param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleID);
			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getGraphicDetails(string sId)
	{
		sSql = "";
		DataSet ds = new DataSet();
		SqlParameter[] param = new SqlParameter[1];
		try
		{
			sSql = "[spGetGraphicDetails]";
			param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sId);
			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public DataSet getArtWork(string sId)
	{
		sSql = "";
		DataSet ds = new DataSet();
		SqlParameter[] param = new SqlParameter[1];
		try
		{
			sSql = "[spGettArtWork]";
			param[0] = oSql.NewParameter("@Ano", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sId);
			ds = oSql.FillDataSet_SP(sSql, param);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		finally
		{
		}

		return ds;
	}

	public string InsertArticle(string[] aArticle)
	{
		string Status = "";
		SqlParameter[] param = new SqlParameter[43];
		try
		{
			/*  sSql = "[sp_AddArticles]";
            param[0] = oSql.NewParameter("@journal_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[0]);
            param[1] = oSql.NewParameter("@name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[1]);
            param[2] = oSql.NewParameter("@title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[2]);
            param[3] = oSql.NewParameter("@job_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[3]);
            param[4] = oSql.NewParameter("@document_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[4]);
            param[5] = oSql.NewParameter("@document_item_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[5]);
            param[6] = oSql.NewParameter("@category_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[6]);
            param[7] = oSql.NewParameter("@doi", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[7]);
            param[8] = oSql.NewParameter("@author", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[8]);
            param[9] = oSql.NewParameter("@author_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[9]);
            param[10] = oSql.NewParameter("@no_authors", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[10]);
            param[11] = oSql.NewParameter("@print_pages", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[11]);
            param[12] = oSql.NewParameter("@ms_pages", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[12]);
            param[13] = oSql.NewParameter("@no_tables", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[14] = oSql.NewParameter("@no_equations", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[15] = oSql.NewParameter("@no_figures", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[16] = oSql.NewParameter("@is_extra_copyedit ", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aArticle[16]);
            param[17] = oSql.NewParameter("@comments", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[17]);
            param[18] = oSql.NewParameter("@sam_author_query", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[18]);
            param[19] = oSql.NewParameter("@figure_correction", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[19]);
            param[20] = oSql.NewParameter("@job_stage_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[20]);
            param[21] = oSql.NewParameter("@received_date", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aArticle[21]);
            param[22] = oSql.NewParameter("@due_date", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aArticle[22]);
            param[23] = oSql.NewParameter("@half_due_date", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aArticle[23]);
            param[24] = oSql.NewParameter("@despatch_date", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aArticle[24]);
            param[25] = oSql.NewParameter("@cats_due_date", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aArticle[25]);
            param[26] = oSql.NewParameter("@created_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[26]);
            param[27] = oSql.NewParameter("@sales_job_group_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[27]);
            param[28] = oSql.NewParameter("@ms_received_date", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aArticle[28]);
            param[29] = oSql.NewParameter("@ms_revised_date", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aArticle[29]);
            param[30] = oSql.NewParameter("@ms_accepted_date", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aArticle[30]);
            param[31] = oSql.NewParameter("@interviewdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[31]);
            param[32] = oSql.NewParameter("@phoneno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[32]);
            param[33] = oSql.NewParameter("@faxno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[33]);
            param[34] = oSql.NewParameter("@interviewtime", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[34]);
            param[35] = oSql.NewParameter("@meetingplace", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[35]);
            param[36] = oSql.NewParameter("@meetingtime", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[36]);
            param[37] = oSql.NewParameter("@country", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[37]);
            param[38] = oSql.NewParameter("@appointmentdate1", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aArticle[38]);
            param[39] = oSql.NewParameter("@appointmentdate2", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aArticle[39]);
            param[40] = oSql.NewParameter("@zone", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[40]);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        * */
            string[, ] sArticleInset = { { "@journal_id", aArticle[0] }, { "@name", aArticle[1] },
				{"@title", aArticle[2] },
				{"@job_type_id", aArticle[3] },
				{"@document_type_id", aArticle[4] },
				{"@document_item_type_id", aArticle[5] },
				{"@category_id", aArticle[6] },
				{"@doi", aArticle[7] },
				{"@author", aArticle[8] },
				{"@author_email", aArticle[9] },
				{"@no_authors", aArticle[10] },
				{"@print_pages", aArticle[11] },
				{"@ms_pages", aArticle[12] },
				{"@no_tables", aArticle[13] },
				{"@no_equations", aArticle[14] },
				{"@no_figures", aArticle[15] },
				{"@is_extra_copyedit ", aArticle[16] },
				{"@comments", aArticle[17] },
				{"@sam_author_query", aArticle[18] },
				{"@figure_correction", aArticle[19] },
				{"@job_stage_id", aArticle[20] },
				{"@received_date", aArticle[21] },
				{"@due_date", aArticle[22] },
				{"@half_due_date", aArticle[23] },
				{"@despatch_date", aArticle[24] },
				{"@cats_due_date", aArticle[25] },
				{"@created_by", aArticle[26] },
				{"@sales_job_group_id", aArticle[27] },
				{"@ms_received_date", aArticle[28] },
				{"@ms_revised_date", aArticle[29] },
				{"@ms_accepted_date", aArticle[30] },
				{"@interviewdate", aArticle[31] },
				{"@phoneno", aArticle[32] },
				{"@faxno", aArticle[33] },
				{"@interviewtime", aArticle[34] },
				{"@meetingplace", aArticle[35] },
				{"@meetingtime", aArticle[36] },
				{"@country", aArticle[37] },
				{"@appointmentdate1", aArticle[38] },
				{"@appointmentdate2", aArticle[39] },
				{"@zone", aArticle[40] },
                {"@Withdraw", aArticle[41]}, 
                {"@Isopen", aArticle[42] },
                {"@ResearchPage", aArticle[43] },
                {"@ArtReprocess", aArticle[44] }};

			DataSet sds = new DataSet();
			sds = oSql.ExcProcedurePrdJL("sp_AddArticles", sArticleInset, CommandType.StoredProcedure);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
			//return ex.Message;
		}

		return Status;
	}

	public string InsertArt(string[] aArt)
	{
		string Status = "";
		SqlParameter[] param = new SqlParameter[6];
		try
		{
			sSql = "[spInsertArt]";
			param[0] = oSql.NewParameter("@AFTNO", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArt[0]);
			param[1] = oSql.NewParameter("@AITNO", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArt[1]);
			param[2] = oSql.NewParameter("@ARTRECEIVED", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArt[2]);
			param[3] = oSql.NewParameter("@ARTDATERECEIVED", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArt[3]);
			param[4] = oSql.NewParameter("@REDRAW", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArt[4]);
			param[5] = oSql.NewParameter("@ANO", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArt[5]);
			Status = oSql.Execute_SP(sSql, param,
                   oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
			//return ex.Message;
		}

		return Status;
	}

	public string UpdateArticle(string[] aArticle)
	{
		string Status = "";
		SqlParameter[] param = new SqlParameter[43];
		try
		{
			sSql = "[sp_UpdateArticles]";
			param[0] = oSql.NewParameter("@article_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[0]);
			param[1] = oSql.NewParameter("@journal_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[1]);
			param[2] = oSql.NewParameter("@name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[2]);
			param[3] = oSql.NewParameter("@title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[3]);
			param[4] = oSql.NewParameter("@job_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[4]);
			param[5] = oSql.NewParameter("@document_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[5]);
			param[6] = oSql.NewParameter("@document_item_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[6]);
			param[7] = oSql.NewParameter("@category_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[7]);
			param[8] = oSql.NewParameter("@doi", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[8]);
			param[9] = oSql.NewParameter("@author", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[9]);
			param[10] = oSql.NewParameter("@author_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[10]);
			param[11] = oSql.NewParameter("@no_authors", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[11]);
			param[12] = oSql.NewParameter("@print_pages", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[12]);
			param[13] = oSql.NewParameter("@ms_pages", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[13]);
			param[14] = oSql.NewParameter("@is_extra_copyedit", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aArticle[14]);
			param[15] = oSql.NewParameter("@comments", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aArticle[15]);
			param[16] = oSql.NewParameter("@sam_author_query", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aArticle[16]);
			param[17] = oSql.NewParameter("@job_stage_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[17]);
			param[18] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[18]);
			param[19] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[19]);
			param[20] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[20]);
			param[21] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[21]);
			param[22] = oSql.NewParameter("@cats_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[22]);
			param[23] = oSql.NewParameter("@created_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[23]);
			param[24] = oSql.NewParameter("@sales_job_group_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[24]);
			param[25] = oSql.NewParameter("@ms_received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[25]);
			param[26] = oSql.NewParameter("@ms_revised_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[26]);
			param[27] = oSql.NewParameter("@ms_accepted_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[27]);
			param[28] = oSql.NewParameter("@interviewdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[28]);
			param[29] = oSql.NewParameter("@phoneno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[29]);
			param[30] = oSql.NewParameter("@faxno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[30]);
			param[31] = oSql.NewParameter("@interviewtime", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[31]);
			param[32] = oSql.NewParameter("@meetingplace", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[32]);
			param[33] = oSql.NewParameter("@meetingtime", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[33]);
			param[34] = oSql.NewParameter("@country", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[34]);
			param[35] = oSql.NewParameter("@appointmentdate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[35]);
			param[36] = oSql.NewParameter("@appointmentdate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[36]);
			param[37] = oSql.NewParameter("@zone", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[37]);
			param[38] = oSql.NewParameter("@figur_correction", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[38]);
            param[39] = oSql.NewParameter("@Withdraw", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[39]);
            param[40] = oSql.NewParameter("@IsOpen", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[40]);
            param[41] = oSql.NewParameter("@ResearchPage", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[41]);
            param[42] = oSql.NewParameter("@ArtReprocess", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[42]);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string InsertJobOnHold(string sJobID, string sJobTypeID, string sOnHoldTypeID, string sDetails, string sCreatedBy)
    {
        string Status = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            sSql = "[spInsertJobOnHold]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sJobID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[2] = oSql.NewParameter("@onhold_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sOnHoldTypeID);
            param[3] = oSql.NewParameter("@details", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDetails);
            param[4] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCreatedBy);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string UpdateJobOnHold(string sJobID, string sJobTypeID, string sOnHoldTypeID)
    {
        string Status = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spUpdateJobOnHold]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sJobID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[2] = oSql.NewParameter("@onhold_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sOnHoldTypeID);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public void InsertEmptyGraphics(string sID, string sType, string sGraphicType, int count, string sFigureType)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            sSql = "[spInsertBlankGraphics]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sType);
            param[2] = oSql.NewParameter("@graphic_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sGraphicType);
            param[3] = oSql.NewParameter("@total_count", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, count);
            param[4] = oSql.NewParameter("@figure_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sFigureType);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    public string DeleteGraphics(string sGraphicIDs)
    {
        sSql = "";
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spDeleteGraphicDetails]";
            param[0] = oSql.NewParameter("@graphic_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sGraphicIDs);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string UpdateGraphics(System.Collections.ArrayList lstGraphics)
    {
        sSql = "";
        int i = 0;
        object[] oGraphics = new object[lstGraphics.Count];
        try
        {
            foreach (string[] a in lstGraphics)
            {
                sSql = "update job_graphic set graphic_name='" + a[1] + "', graphic_type_id=" + a[2] + ", graphic_desc='" + a[3] + "', figure_type_id='" + a[4] + "', isredraw='" + a[5] + "' where graphic_id=" + a[0];
                oGraphics[i] = sSql;
                i++;
            }
            if (i > 0) oSql.Execute_Sql(oGraphics);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return "true";
    }


}
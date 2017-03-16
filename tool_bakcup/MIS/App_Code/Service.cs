using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;
using System.Linq;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{

    public Service()
    {

        //Uncomment the following line if using designed components
        //InitializeComponent();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetCustomers(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            List<string> terms = prefix.Split(',').ToList();
            terms = terms.Select(s => s.Trim()).ToList();

            //Extract the term to be searched from the list
            string searchTerm = terms.LastOrDefault().ToString().Trim();

            //Return if Search Term is empty
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new string[0];
            }

            //Populate the terms that need to be filtered out
            List<string> excludeTerms = new List<string>();
            if (terms.Count > 1)
            {
                terms.RemoveAt(terms.Count - 1);
                excludeTerms = terms;
            }

            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["conStrSQLL"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string query = "select Case when employee_number<10 then Fname+' '+surname+'(DDS0'+cast(employee_number as varchar(30))+')' else  Fname+' '+surname+'(DDS'+cast(employee_number as varchar(30))+')' end Fname,employee_id " +
                "from Employee where location_id=3 and obsolete is null and " +
                "Fname like @SearchText + '%'";

                //Filter out the existing searched items
                if (excludeTerms.Count > 0)
                {
                    query += string.Format(" and Fname not in ({0})", string.Join(",", excludeTerms.Select(s => "'" + s + "'").ToArray()));
                }
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@SearchText", searchTerm);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["Fname"], sdr["employee_id"]));
                    }
                }
                conn.Close();
            }
            return customers.ToArray();
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetCustomers1(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            List<string> terms = prefix.Split(',').ToList();
            terms = terms.Select(s => s.Trim()).ToList();

            //Extract the term to be searched from the list
            string searchTerm = terms.LastOrDefault().ToString().Trim();

            //Return if Search Term is empty
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new string[0];
            }

            //Populate the terms that need to be filtered out
            List<string> excludeTerms = new List<string>();
            if (terms.Count > 1)
            {
                terms.RemoveAt(terms.Count - 1);
                excludeTerms = terms;
            }

            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["conStrSQLL"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string query = "select Case when employee_number<10 then Fname+' '+surname+'(DDS0'+cast(employee_number as varchar(30))+')' else  Fname+' '+surname+'(DDS'+cast(employee_number as varchar(30))+')' end Fname,employee_id " +
                "from Employee where location_id=3 and obsolete is null and " +
                "Fname like @SearchText + '%'";

                //Filter out the existing searched items
                if (excludeTerms.Count > 0)
                {
                    query += string.Format(" and Fname not in ({0})", string.Join(",", excludeTerms.Select(s => "'" + s + "'").ToArray()));
                }
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@SearchText", searchTerm);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["Fname"], sdr["employee_id"]));
                    }
                }
                conn.Close();
            }
            return customers.ToArray();
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetCustomers2(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            List<string> terms = prefix.Split(',').ToList();
            terms = terms.Select(s => s.Trim()).ToList();

            //Extract the term to be searched from the list
            string searchTerm = terms.LastOrDefault().ToString().Trim();

            //Return if Search Term is empty
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new string[0];
            }

            //Populate the terms that need to be filtered out
            List<string> excludeTerms = new List<string>();
            if (terms.Count > 1)
            {
                terms.RemoveAt(terms.Count - 1);
                excludeTerms = terms;
            }

            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["conStrSQLL"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string query = "select Case when employee_number<10 then Fname+' '+surname+'(DDS0'+cast(employee_number as varchar(30))+')' else  Fname+' '+surname+'(DDS'+cast(employee_number as varchar(30))+')' end Fname,employee_id " +
                "from Employee where location_id=3 and obsolete is null and " +
                "Fname like @SearchText + '%'";

                //Filter out the existing searched items
                if (excludeTerms.Count > 0)
                {
                    query += string.Format(" and Fname not in ({0})", string.Join(",", excludeTerms.Select(s => "'" + s + "'").ToArray()));
                }
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@SearchText", searchTerm);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["Fname"], sdr["employee_id"]));
                    }
                }
                conn.Close();
            }
            return customers.ToArray();
        }
    }
}

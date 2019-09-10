using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NASDAQ.Models;
// using NASDAQ.ViewModels;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;


namespace NASDAQ.Repos
{
    public class CompaniesRepository
    {
        public List<Company> getCompanies()
        {
            List<Company> companies = new List<Company>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from companies ORDER BY number_of_shareholders";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                companies.Add(new Company
                {
                    code = Convert.ToInt32(item["code"]),
                    name = Convert.ToString(item["name"]),
                    value = Convert.ToInt32(item["value"]),
                    number_of_shares = Convert.ToInt32(item["number_of_shares"]),
                    dividend_per_share = Convert.ToDecimal(item["dividend_per_share"]),
                    number_of_shareholders = Convert.ToInt32(item["number_of_shareholders"])
                });
            }

            return companies;
        }

        // PK parameter, WHERE query, single from list
        public Company getCompany(int code)
        {
            Company company = new Company();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + Globals.dbPrefix + "companies where code=" + code;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                company.code = Convert.ToInt32(item["code"]);
                company.name = Convert.ToString(item["name"]);
                company.value = Convert.ToInt32(item["value"]);
                company.number_of_shares = Convert.ToInt32(item["number_of_shares"]);
                company.dividend_per_share = Convert.ToDecimal(item["dividend_per_share"]);
                company.number_of_shareholders = Convert.ToInt32(item["number_of_shareholders"]);
            }

            return company;
        }

        public bool updateCompany(Company company)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE " + Globals.dbPrefix + "companies c SET c.name=?name, c.value=?value, " +
                    "c.number_of_shares=?number_of_shares, c.dividend_per_share=?dividend_per_share, c.number_of_shareholders=?number_of_shareholders " +
                    "WHERE c.code=?code";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?code", MySqlDbType.Int32).Value = company.code;
                mySqlCommand.Parameters.Add("?name", MySqlDbType.VarChar).Value = company.name;
                mySqlCommand.Parameters.Add("?value", MySqlDbType.Int32).Value = company.value;
                mySqlCommand.Parameters.Add("?number_of_shares", MySqlDbType.Int32).Value = company.number_of_shares;
                mySqlCommand.Parameters.Add("?dividend_per_share", MySqlDbType.Decimal).Value = company.dividend_per_share;
                mySqlCommand.Parameters.Add("?number_of_shareholders", MySqlDbType.Int32).Value = company.number_of_shareholders;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool addCompany(Company company)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO " + Globals.dbPrefix +
                "companies(code, name, value, number_of_shares, dividend_per_share, number_of_shareholders)" +
                "VALUES(?code, ?name, ?value, ?number_of_shares, ?dividend_per_share, ?number_of_shareholders)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);

            mySqlCommand.Parameters.Add("?code", MySqlDbType.Int32).Value = company.code;
            mySqlCommand.Parameters.Add("?name", MySqlDbType.VarChar).Value = company.name;
            mySqlCommand.Parameters.Add("?value", MySqlDbType.Int32).Value = company.value;
            mySqlCommand.Parameters.Add("?number_of_shares", MySqlDbType.Int32).Value = company.number_of_shares;
            mySqlCommand.Parameters.Add("?dividend_per_share", MySqlDbType.Decimal).Value = company.dividend_per_share;
            mySqlCommand.Parameters.Add("?number_of_shareholders", MySqlDbType.Int32).Value = company.number_of_shareholders;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public int getCompanyCount(int code)
        {
            int used = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(inner_quantity) as quantity
                        FROM (
                            (SELECT count(*) as inner_quantity from management_board_members where fk_Company=?code) 
                            UNION 
                            (SELECT count(*) from financial_reports where fk_Company=?code) 
                            UNION 
                            (SELECT count(*) from markets where fk_Company=?code) 
                            UNION 
                            (SELECT count(*) from securities where fk_Company=?code)) as mytablename 
                        WHERE inner_quantity > 0";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?code", MySqlDbType.Int32).Value = code;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                used = Convert.ToInt32(item["quantity"] == DBNull.Value ? 0 : item["quantity"]);
            }
            return used;
        }

        public void deleteCompany(int code)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + Globals.dbPrefix + "companies where code=?code";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?code", MySqlDbType.Int32).Value = code;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}
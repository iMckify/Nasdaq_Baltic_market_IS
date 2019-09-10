using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NASDAQ.Models;
using NASDAQ.ViewModels;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace NASDAQ.Repos
{
    public class SecuritiesRepository
    {
        public List<Security> getSecurities()
        {
            List<Security> securities = new List<Security>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + Globals.dbPrefix + "securities";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                securities.Add(new Security
                {
                    isin = Convert.ToString(item["isin"]),
                    ticker = Convert.ToString(item["ticker"]),
                    recent_volatility = Convert.ToDecimal(item["recent_volatility"]),
                    list_segment = Convert.ToString(item["list_segment"]),
                    issuer = Convert.ToString(item["issuer"]),
                    nominal_value = Convert.ToDecimal(item["nominal_value"]),
                    total_number_of_securities = Convert.ToInt32(item["total_number_of_securities"]),
                    listed_securities_number = Convert.ToInt32(item["listed_securities_number"]),
                    listing_date = Convert.ToDateTime(item["listing_date"]),
                    fk_Company = Convert.ToInt32(item["fk_Company"])
                });
            }

            return securities;
        }

        public SecurityEditViewModel getSecurity(string ticker)
        {
            SecurityEditViewModel securityEditViewModel = new SecurityEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"select s.* from " + Globals.dbPrefix + @"securities s WHERE s.ticker='" + ticker +"'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                securityEditViewModel.isin = Convert.ToString(item["isin"]);
                securityEditViewModel.ticker = Convert.ToString(item["ticker"]);
                securityEditViewModel.recent_volatility = Convert.ToDecimal(item["recent_volatility"]);
                securityEditViewModel.list_segment = Convert.ToString(item["list_segment"]);
                securityEditViewModel.issuer = Convert.ToString(item["issuer"]);
                securityEditViewModel.nominal_value = Convert.ToDecimal(item["nominal_value"]);
                securityEditViewModel.total_number_of_securities = Convert.ToInt32(item["total_number_of_securities"]);
                securityEditViewModel.listed_securities_number = Convert.ToInt32(item["listed_securities_number"]);
                securityEditViewModel.listing_date = Convert.ToDateTime(item["listing_date"]);
                securityEditViewModel.fk_Company = Convert.ToInt32(item["fk_Company"]);
            }

            return securityEditViewModel;
        }

        public bool addSecurity(SecurityEditViewModel securityEditViewModel)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO " + Globals.dbPrefix +
                "securities(isin, ticker, recent_volatility, list_segment, issuer, nominal_value, total_number_of_securities, listed_securities_number, listing_date, fk_Company)" +
                "VALUES(?isin, ?ticker, ?recent_volatility, ?list_segment, ?issuer, ?nominal_value, ?total_number_of_securities, ?listed_securities_number, ?listing_date, ?fk_Company)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);

            mySqlCommand.Parameters.Add("?isin", MySqlDbType.VarChar).Value = securityEditViewModel.isin;
            mySqlCommand.Parameters.Add("?ticker", MySqlDbType.VarChar).Value = securityEditViewModel.ticker;
            mySqlCommand.Parameters.Add("?recent_volatility", MySqlDbType.Decimal).Value = securityEditViewModel.recent_volatility;
            mySqlCommand.Parameters.Add("?list_segment", MySqlDbType.VarChar).Value = securityEditViewModel.list_segment;
            mySqlCommand.Parameters.Add("?issuer", MySqlDbType.VarChar).Value = securityEditViewModel.issuer;
            mySqlCommand.Parameters.Add("?nominal_value", MySqlDbType.Decimal).Value = securityEditViewModel.nominal_value;
            mySqlCommand.Parameters.Add("?total_number_of_securities", MySqlDbType.Int32).Value = securityEditViewModel.total_number_of_securities;
            mySqlCommand.Parameters.Add("?listed_securities_number", MySqlDbType.Int32).Value = securityEditViewModel.listed_securities_number;
            mySqlCommand.Parameters.Add("?listing_date", MySqlDbType.DateTime).Value = securityEditViewModel.listing_date;
            mySqlCommand.Parameters.Add("?fk_Company", MySqlDbType.Int32).Value = securityEditViewModel.fk_Company;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool updateSecurity(SecurityEditViewModel securityEditViewModel)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE securities s SET
                                    s.isin = ?isin,
                                    s.ticker = ?ticker,
                                    s.recent_volatility = ?recent_volatility,
                                    s.list_segment = ?list_segment,
                                    s.issuer = ?issuer,
                                    s.nominal_value = ?nominal_value,
                                    s.total_number_of_securities = ?total_number_of_securities,
                                    s.listed_securities_number = ?listed_securities_number,
                                    s.listing_date = ?listing_date,
                                    s.fk_Company = ?fk_Company
                                    WHERE s.ticker=?ticker";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);

            mySqlCommand.Parameters.Add("?isin", MySqlDbType.VarChar).Value = securityEditViewModel.isin;
            mySqlCommand.Parameters.Add("?ticker", MySqlDbType.VarChar).Value = securityEditViewModel.ticker;
            mySqlCommand.Parameters.Add("?recent_volatility", MySqlDbType.Decimal).Value = securityEditViewModel.recent_volatility;
            mySqlCommand.Parameters.Add("?list_segment", MySqlDbType.VarChar).Value = securityEditViewModel.list_segment;
            mySqlCommand.Parameters.Add("?issuer", MySqlDbType.VarChar).Value = securityEditViewModel.issuer;
            mySqlCommand.Parameters.Add("?nominal_value", MySqlDbType.Decimal).Value = securityEditViewModel.nominal_value;
            mySqlCommand.Parameters.Add("?total_number_of_securities", MySqlDbType.Int32).Value = securityEditViewModel.total_number_of_securities;
            mySqlCommand.Parameters.Add("?listed_securities_number", MySqlDbType.Int32).Value = securityEditViewModel.listed_securities_number;
            mySqlCommand.Parameters.Add("?listing_date", MySqlDbType.DateTime).Value = securityEditViewModel.listing_date.Date; // .date tik cia
            mySqlCommand.Parameters.Add("?fk_Company", MySqlDbType.Int32).Value = securityEditViewModel.fk_Company;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public int getSecurityCount(string ticker)
        {
            int used = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(inner_quantity) as quantity
                        FROM (
                            (SELECT count(*) as inner_quantity from portfolio_securities where fk_Security=?ticker) 
                            UNION 
                            (SELECT count(*) from prices where fk_Security=?ticker) 
                            UNION 
                            (SELECT count(*) from orders where fk_Security=?ticker)) as mytablename 
                        WHERE inner_quantity > 0";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?ticker", MySqlDbType.VarChar).Value = ticker;
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

        public void deleteSecurity(string ticker)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + Globals.dbPrefix + "securities where ticker=?ticker";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?ticker", MySqlDbType.VarChar).Value = ticker;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}
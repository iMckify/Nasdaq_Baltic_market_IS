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
    public class FinancialReportsRepository
    {
        public List<Financial_report> getReports()
        {
            List<Financial_report> reports = new List<Financial_report>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + Globals.dbPrefix + "financial_reports";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                reports.Add(new Financial_report
                {
                    ticker = Convert.ToString(item["ticker"]),
                    reporting_period = Convert.ToString(item["reporting_period"]),
                    statement_type = Convert.ToString(item["statement_type"]),
                    release_date = Convert.ToDateTime(item["release_date"]),
                    P_E = Convert.ToDecimal(item["P_E"]),
                    P_B = Convert.ToDecimal(item["P_B"]),
                    EV_EBITDA = Convert.ToDecimal(item["EV_EBITDA"]),
                    NetDepth_EBITDA = Convert.ToDecimal(item["NetDepth_EBITDA"]),
                    ROA = Convert.ToDecimal(item["ROA"]),
                    fk_Company = Convert.ToInt32(item["fk_Company"])
                });
            }

            return reports;
        }

        public FinancialReportEditViewModel getReport(string ticker)
        {
            FinancialReportEditViewModel financialReportEditViewModel = new FinancialReportEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"select r.* from " + Globals.dbPrefix + @"financial_reports r WHERE r.ticker='" + ticker + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                financialReportEditViewModel.ticker = Convert.ToString(item["ticker"]);
                financialReportEditViewModel.reporting_period = Convert.ToString(item["reporting_period"]);
                financialReportEditViewModel.statement_type = Convert.ToString(item["statement_type"]);
                financialReportEditViewModel.release_date = Convert.ToDateTime(item["release_date"]);
                financialReportEditViewModel.P_E = Convert.ToDecimal(item["P_E"]);
                financialReportEditViewModel.P_B = Convert.ToDecimal(item["P_B"]);
                financialReportEditViewModel.EV_EBITDA = Convert.ToDecimal(item["EV_EBITDA"]);
                financialReportEditViewModel.NetDepth_EBITDA = Convert.ToDecimal(item["NetDepth_EBITDA"]);
                financialReportEditViewModel.ROA = Convert.ToDecimal(item["ROA"]);
                financialReportEditViewModel.fk_Company = Convert.ToInt32(item["fk_Company"]);
            }

            return financialReportEditViewModel;
        }

        public bool addReport(FinancialReportEditViewModel financialReportEditViewModel)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"
            INSERT INTO financial_reports(ticker, reporting_period, statement_type, release_date, P_E, P_B, 
                EV_EBITDA, NetDepth_EBITDA, ROA, fk_Company)
            VALUES(?ticker, ?reporting_period, ?statement_type, ?release_date, ?P_E, ?P_B, 
                ?EV_EBITDA, ?NetDepth_EBITDA, ?ROA, ?fk_Company)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);

            mySqlCommand.Parameters.Add("?ticker", MySqlDbType.VarChar).Value = financialReportEditViewModel.ticker;
            mySqlCommand.Parameters.Add("?reporting_period", MySqlDbType.VarChar).Value = financialReportEditViewModel.reporting_period;
            mySqlCommand.Parameters.Add("?statement_type", MySqlDbType.VarChar).Value = financialReportEditViewModel.statement_type;
            mySqlCommand.Parameters.Add("?release_date", MySqlDbType.DateTime).Value = financialReportEditViewModel.release_date;
            mySqlCommand.Parameters.Add("?P_E", MySqlDbType.Decimal).Value = financialReportEditViewModel.P_E;
            mySqlCommand.Parameters.Add("?P_B", MySqlDbType.Decimal).Value = financialReportEditViewModel.P_B;
            mySqlCommand.Parameters.Add("?EV_EBITDA", MySqlDbType.Decimal).Value = financialReportEditViewModel.EV_EBITDA;
            mySqlCommand.Parameters.Add("?NetDepth_EBITDA", MySqlDbType.Decimal).Value = financialReportEditViewModel.NetDepth_EBITDA;
            mySqlCommand.Parameters.Add("?ROA", MySqlDbType.Decimal).Value = financialReportEditViewModel.ROA;
            mySqlCommand.Parameters.Add("?fk_Company", MySqlDbType.Int32).Value = financialReportEditViewModel.fk_Company;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool updateReport(FinancialReportEditViewModel financialReportEditViewModel)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE financial_reports r SET
                                    r.reporting_period = ?reporting_period,
                                    r.statement_type = ?statement_type,
                                    r.release_date = ?release_date,
                                    r.P_E = ?P_E,
                                    r.P_B = ?P_B,
                                    r.EV_EBITDA = ?EV_EBITDA,
                                    r.NetDepth_EBITDA = ?NetDepth_EBITDA,
                                    r.ROA = ?ROA,
                                    r.fk_Company = ?fk_Company
                                    WHERE r.ticker=?ticker";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);

            mySqlCommand.Parameters.Add("?ticker", MySqlDbType.VarChar).Value = financialReportEditViewModel.ticker;
            mySqlCommand.Parameters.Add("?reporting_period", MySqlDbType.VarChar).Value = financialReportEditViewModel.reporting_period;
            mySqlCommand.Parameters.Add("?statement_type", MySqlDbType.VarChar).Value = financialReportEditViewModel.statement_type;
            mySqlCommand.Parameters.Add("?release_date", MySqlDbType.DateTime).Value = financialReportEditViewModel.release_date.Date; // .date tik cia
            mySqlCommand.Parameters.Add("?P_E", MySqlDbType.Decimal).Value = financialReportEditViewModel.P_E;
            mySqlCommand.Parameters.Add("?P_B", MySqlDbType.Decimal).Value = financialReportEditViewModel.P_B;
            mySqlCommand.Parameters.Add("?EV_EBITDA", MySqlDbType.Decimal).Value = financialReportEditViewModel.EV_EBITDA;
            mySqlCommand.Parameters.Add("?NetDepth_EBITDA", MySqlDbType.Decimal).Value = financialReportEditViewModel.NetDepth_EBITDA;
            mySqlCommand.Parameters.Add("?ROA", MySqlDbType.Decimal).Value = financialReportEditViewModel.ROA;
            mySqlCommand.Parameters.Add("?fk_Company", MySqlDbType.Int32).Value = financialReportEditViewModel.fk_Company;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public int getReportCount(string ticker)
        {
            int used = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(*) as quantity FROM evaluates where fk_Financial_Report=?ticker";
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

        public void deleteReport(string ticker)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + Globals.dbPrefix + "financial_reports where ticker=?ticker";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?ticker", MySqlDbType.VarChar).Value = ticker;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}
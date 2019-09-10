using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using NASDAQ.ViewModels;

namespace NASDAQ.Repos
{
    public class StatementRepository
    {
        public List<PriceConsolidatedViewModel> getPricesConsolidated(DateTime? from, DateTime? to)
        {
            List<PriceConsolidatedViewModel> prices = new List<PriceConsolidatedViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT c.name as Name, p.fk_Security as Security,
	                                COUNT(p.fk_Security) as 'Sold count',
                                    TRUNCATE( SUM(p.value) / ( s.nominal_value * COUNT(p.fk_Security) ) ,0) * 100 as 'Sold sum to nominal_value'
                                FROM prices p 
	                                INNER JOIN securities s ON p.fk_Security = s.ticker
                                    INNER JOIN companies c ON c.code = s.fk_Company
                                WHERE p.from_date >= IFNULL(?from, p.from_date) AND
	                                p.to_date <= IFNULL(?to, p.to_date)    

                                GROUP BY p.fk_Security
                                ORDER BY p.fk_Security";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?from", MySqlDbType.DateTime).Value = from;
            mySqlCommand.Parameters.Add("?to", MySqlDbType.DateTime).Value = to;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                prices.Add(new PriceConsolidatedViewModel
                {
                    company = Convert.ToString(item["Name"]),
                    fk_Security = Convert.ToString(item["Security"]),
                    count = Convert.ToInt32(item["Sold count"]),
                    sum = Convert.ToDecimal(item["Sold sum to nominal_value"])
                });
            }
            return prices;
        }

        public StatementViewModel getPricesTotal(DateTime? from, DateTime? to)
        {
            StatementViewModel total = new StatementViewModel();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            //string sqlquery = @"SELECT COUNT(p.fk_Security) as 'Sold count',
            //                        SUM(p.value) as 'Sold sum to nominal_value'
            //                    FROM prices p 
            //                     INNER JOIN securities s ON p.fk_Security = s.ticker
            //                        INNER JOIN companies c ON c.code = s.fk_Company
            //                    WHERE p.from_date >= IFNULL(?from, p.from_date) AND
            //                     p.to_date <= IFNULL(?to, p.to_date)";
            string sqlquery = @"
            SELECT COUNT(p.fk_Security) as 'Sold count',
	            (SUM(p.value) * COUNT(p.fk_Security) - my2.suma) as 'Sold sum to nominal_value'
            FROM 
	            (
	            SELECT SUM(my.xx) as suma
	            FROM (
		            SELECT COUNT(p.fk_Security) * SUM(p.value) as xx
		            FROM prices p 
			            INNER JOIN securities s ON p.fk_Security = s.ticker
   		 	            INNER JOIN companies c ON c.code = s.fk_Company
		            WHERE p.from_date >= IFNULL(?from, p.from_date) AND
			            p.to_date <= IFNULL(?to, p.to_date)
		            GROUP BY p.fk_Security
	                ) my
	            ) my2,
	            prices p 
	            INNER JOIN securities s ON p.fk_Security = s.ticker
                INNER JOIN companies c ON c.code = s.fk_Company
            WHERE p.from_date >= IFNULL(?from, p.from_date) AND
	            p.to_date <= IFNULL(?to, p.to_date)
            ";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?from", MySqlDbType.DateTime).Value = from;
            mySqlCommand.Parameters.Add("?to", MySqlDbType.DateTime).Value = to;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                total.amount = Convert.ToInt32(item["Sold count"] == System.DBNull.Value ? 0 : item["Sold count"]);
                total.sum = Convert.ToDecimal(item["Sold sum to nominal_value"] == System.DBNull.Value ? 0 : item["Sold sum to nominal_value"]);
            }

            return total;
        }
    }
}
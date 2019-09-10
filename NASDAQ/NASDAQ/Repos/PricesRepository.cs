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
    public class PricesRepository
    {
        public List<Price> getPrices()
        {
            List<Price> prices = new List<Price>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + Globals.dbPrefix + "prices p order by p.fk_Security";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                prices.Add(new Price
                {
                    from_date = Convert.ToDateTime(item["from_date"]),
                    value = Convert.ToDecimal(item["value"]),
                    to_date = Convert.ToDateTime(item["to_date"]),
                    id = Convert.ToInt32(item["id"]),
                    fk_Security = Convert.ToString(item["fk_Security"])
                });
            }

            return prices;
        }

        public PriceEditViewModel getPrice(int id)
        {
            PriceEditViewModel PriceEditViewModel = new PriceEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"select p.* from " + Globals.dbPrefix + @"prices p WHERE p.id=" + id; // ' '
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                PriceEditViewModel.from_date = Convert.ToDateTime(item["from_date"]);
                PriceEditViewModel.value = Convert.ToDecimal(item["value"]);
                PriceEditViewModel.to_date = Convert.ToDateTime(item["to_date"]);
                PriceEditViewModel.id = Convert.ToInt32(item["id"]);
                PriceEditViewModel.fk_Security = Convert.ToString(item["fk_Security"]);
            }

            return PriceEditViewModel;
        }

        public bool addPrice(PriceEditViewModel PriceEditViewModel)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO " + Globals.dbPrefix +
                "prices(from_date, value, to_date, id, fk_Security)" +
                "VALUES(?from_date, ?value, ?to_date, ?id, ?fk_Security)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            
            mySqlCommand.Parameters.Add("?from_date", MySqlDbType.DateTime).Value = PriceEditViewModel.from_date;
            mySqlCommand.Parameters.Add("?value", MySqlDbType.Decimal).Value = PriceEditViewModel.value;
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = PriceEditViewModel.id;
            mySqlCommand.Parameters.Add("?to_date", MySqlDbType.DateTime).Value = PriceEditViewModel.to_date;
            mySqlCommand.Parameters.Add("?fk_Security", MySqlDbType.VarChar).Value = PriceEditViewModel.fk_Security;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool updatePrice(PriceEditViewModel PriceEditViewModel)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE prices p SET
                                    p.from_date = ?from_date,
                                    p.value = ?value,
                                    p.to_date = ?to_date,
                                    p.fk_Security = ?fk_Security
                                    WHERE p.id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);

            mySqlCommand.Parameters.Add("?from_date", MySqlDbType.DateTime).Value = PriceEditViewModel.from_date;
            mySqlCommand.Parameters.Add("?value", MySqlDbType.Decimal).Value = PriceEditViewModel.value;
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = PriceEditViewModel.id;
            mySqlCommand.Parameters.Add("?to_date", MySqlDbType.DateTime).Value = PriceEditViewModel.to_date;
            mySqlCommand.Parameters.Add("?fk_Security", MySqlDbType.VarChar).Value = PriceEditViewModel.fk_Security;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }
        // paramuose fk_Security ar id????
        /*public int getPriceCount(string fk_Security)
        {
            int used = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(*) as quantity FROM securities where ticker=?fk_Security";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?fk_Security", MySqlDbType.VarChar).Value = fk_Security;
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
        }*/

        public void deletePrice(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + Globals.dbPrefix + "prices where id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}
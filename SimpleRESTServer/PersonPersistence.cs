using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleRESTServer.Models;
using MySql.Data;
using System.Collections;

namespace SimpleRESTServer
{
    public class PersonPersistence
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public PersonPersistence()
        {
            string myConnectionString = "server=127.0.0.1;uid=root;pwd=515764;database=employeedb";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
        }

        public long savePerson(Person p)
        {
            string sqlString = "INSERT INTO `person` (firstname, lastname, startdate, enddate, payroll) VALUES ('" + p.FirstName + "','" + p.LastName + "','" + p.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + p.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "'," + p.PayRate + ")";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            long id = cmd.LastInsertedId;
            return id;
        }

        public ArrayList getPersons()
        {
            ArrayList personArray = new ArrayList();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM person";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read()) //If we got back the data then get the first column that came back
            {
                Person p = new Person();
                p.FirstName = mySQLReader.GetString(0);
                p.LastName = mySQLReader.GetString(1);
                p.StartDate = mySQLReader.GetDateTime(2);
                p.EndDate = mySQLReader.GetDateTime(3);
                p.PayRate = mySQLReader.GetDouble(4);
                p.ID = mySQLReader.GetInt32(5);
                personArray.Add(p);
            }
            return personArray;
        }

        public Person getPerson(long ID)
        {
            Person p = new Person();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM person WHERE ID = " + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            if(mySQLReader.Read()) //If we got back the data then get the first column that came back
            {
                
                p.FirstName = mySQLReader.GetString(0);
                p.LastName = mySQLReader.GetString(1);
                p.StartDate = mySQLReader.GetDateTime(2);
                p.EndDate = mySQLReader.GetDateTime(3);
                p.PayRate = mySQLReader.GetDouble(4);
                p.ID = mySQLReader.GetInt32(5);

                return p;
            }
            else
            {
                return null;
            }
        }

        public bool deletePerson(long ID)
        {
            Person p = new Person();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM person WHERE ID = " + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read()) //If the record exists, continue to delete it
            {
                mySQLReader.Close();
                sqlString = "DELETE FROM person WHERE ID = " + ID.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updatePerson(long ID, Person p)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM person WHERE ID = " + ID.ToString();

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read()) //If the record exists, continue to delete it
            {
                mySQLReader.Close();
                sqlString = "UPDATE `person` SET firstname='" + p.FirstName + "', lastname='" + p.LastName + "', startdate='" + p.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "', enddate='" + p.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "', payroll=" + p.PayRate + " WHERE ID = " + ID.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
  }

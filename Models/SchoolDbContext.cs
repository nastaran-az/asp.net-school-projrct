using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


namespace schoolProject.Models
{
    public class SchoolDbContext

    {
       //Only schoolDbContext class can use them because of private word .

        
        private static string User { get { return "root";} }
        private static string Password { get { return "root";} }
        private static string Database { get { return "schooldb";} }
        private static string Server { get { return "localhost";} }
        private static string Port { get { return "3306"; } }

        //ConnectionString is a series of credentials used to connect to the database.
        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }
        //This is the method we use to get the database!
        /// <summary>
        /// Returns a connection to the schooldb database.
        /// </summary>
        /// <example>
        /// private SchoolDbContext dbSchool = new SchoolDbContext();
        /// MySqlConnection Conn = SchoolDbContext.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }

    }
}
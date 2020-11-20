using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using schoolProject.Models;
using MySql.Data.MySqlClient;


namespace schoolProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext school = new SchoolDbContext();

        //This Controller Will access the teaches table of our schooldb database.
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <example>GET api/teacherData/ListTeachers</example>
        /// <returns>
        /// A list of Teachers (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            // creat an instance of a connection 
            MySqlConnection conn = school.AccessDatabase();
            {
                //Open the connection between the web server and database
                conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = conn.CreateCommand();
                //Sql Query
                cmd.CommandText = "Select * from teachers";

                //Gather Result Set of Query into a variable
                MySqlDataReader ResultSet = cmd.ExecuteReader();

                //creat an empty list teacher names;
                List<Teacher> TeacherNames = new List<Teacher> { };

                //Loop Through Each Row the Result Set
                while (ResultSet.Read())
                {   //Access column information by the DB column name as an index;
                    int TeacherId = (int)ResultSet["teacherid"];
                    string TeacherFName = (string)ResultSet["teacherfname"];
                    string TeacherLname = (string)ResultSet["teacherlname"];
                    string EmployeeNumber = (string)ResultSet["employeenumber"];
                    string HireDate = ResultSet["hiredate"].ToString();
                    decimal Salary = (decimal)ResultSet["salary"];
                    Teacher teacher = new Teacher();
                    teacher.teacherid = TeacherId;
                    teacher.teacherfname = TeacherFName;
                    teacher.teacherlname = TeacherLname;
                    teacher.employeenumber = EmployeeNumber;
                    teacher.hiredate = HireDate;
                    teacher.salary = Salary;
                    //Add Teacher to the list
                    TeacherNames.Add(teacher);
                };

                //Close the connection between the MySQL Database and the WebServer
                conn.Close();
                return TeacherNames;
            }

        }
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection conn = school.AccessDatabase();
            {
                //Open the connection between the web server and database
                conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = conn.CreateCommand();
                //Sql Query
                cmd.CommandText = "select * from teachers where teacherid=" + id;

                //Gather Result Set of Query into a variable
                MySqlDataReader ResultSet = cmd.ExecuteReader();

                 //Loop Through Each Row the Result Set
                while (ResultSet.Read())
                {
                    //Access column information by the DB column name as an index;
                    int TeacherId = (int)ResultSet["teacherid"];
                    string TeacherFName = (string)ResultSet["teacherfname"];
                    string TeacherLname = (string)ResultSet["teacherlname"];
                    string EmployeeNumber = (string)ResultSet["employeenumber"];
                    string HireDate = ResultSet["hiredate"].ToString();
                    decimal Salary = (decimal)ResultSet["salary"];
                    NewTeacher.teacherid = TeacherId;
                    NewTeacher.teacherfname = TeacherFName;
                    NewTeacher.teacherlname = TeacherLname;
                    NewTeacher.employeenumber = EmployeeNumber;
                    NewTeacher.hiredate = HireDate;
                    NewTeacher.salary = Salary;
                 }
                return NewTeacher;
            }

        }
    }
}


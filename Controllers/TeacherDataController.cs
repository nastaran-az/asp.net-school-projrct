using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using schoolProject.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Web.Http.Cors;




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
        /// A list of Teachers (first names and last names and....)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{search?}")]
        public IEnumerable<Teacher> ListTeachers(string search = null)
        {
            // creat an instance of a connection 
            MySqlConnection conn = school.AccessDatabase();
            {
                //Open the connection between the web server and database
                conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = conn.CreateCommand();
                //Sql Query
                cmd.CommandText = "Select * from teachers  where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

                cmd.Parameters.AddWithValue("@key", "%" + search + "%");
                cmd.Prepare();


                //Gather Result Set of Query into a variable
                MySqlDataReader ResultList = cmd.ExecuteReader();

                //creat an empty list teacher names;
                List<Teacher> TeacherInformation = new List<Teacher> { };

                //Loop Through Each Row the Result Set;
                while (ResultList.Read())
                {   //Access column information by the DB column name as an index;
                    int TeacherId = (int)ResultList["teacherid"];
                    string TeacherFName = (string)ResultList["teacherfname"];
                    string TeacherLname = (string)ResultList["teacherlname"];
                    string EmployeeNumber = (string)ResultList["employeenumber"];
                    string HireDate = ResultList["hiredate"].ToString();
                    decimal Salary = (decimal)ResultList["salary"];
                    Teacher teacher = new Teacher();
                    teacher.teacherid = TeacherId;
                    teacher.teacherfname = TeacherFName;
                    teacher.teacherlname = TeacherLname;
                    teacher.employeenumber = EmployeeNumber;
                    teacher.hiredate = HireDate;
                    teacher.salary = Salary;
                    //Add Teacher to the list
                    TeacherInformation.Add(teacher);
                };

                //Close the connection between the MySQL Database and the WebServer
                conn.Close();
                return TeacherInformation;
            }

        }
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
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
                cmd.CommandText = "select * from teachers where teacherid=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();

                //Gather Result Set of Query into a variable
                MySqlDataReader ResultList = cmd.ExecuteReader();

                //Loop Through Each Row the Result Set
                while (ResultList.Read())
                {
                    //Access column information by the DB column name as an index;
                    int TeacherId = (int)ResultList["teacherid"];
                    string TeacherFName = (string)ResultList["teacherfname"];
                    string TeacherLname = (string)ResultList["teacherlname"];
                    string EmployeeNumber = (string)ResultList["employeenumber"];
                    string HireDate = ResultList["hiredate"].ToString();
                    decimal Salary = (decimal)ResultList["salary"];
                    NewTeacher.teacherid = TeacherId;
                    NewTeacher.teacherfname = TeacherFName;
                    NewTeacher.teacherlname = TeacherLname;
                    NewTeacher.employeenumber = EmployeeNumber;
                    NewTeacher.hiredate = HireDate;
                    NewTeacher.salary = Salary;
                }
                conn.Close();

                return NewTeacher;
            }
        }

        [Route("api/TeacherData/DelTeacher/{id}")]
        [HttpPost]
        public void DelTeacher(int id)
        {
            // <summary>
            /// Deletes an Teacher from the connected MySQL Database if the ID of that author exists.
            /// </summary>
            /// <param name="tid">The ID of the Teacher.</param>
            /// <example>POST /api/TeacherData/DelTeacher/3</example>
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    
        /// <summary>
        /// Adds an Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields /param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"teacherfname":"Viana",
        ///	"teacherlname":"Ramazani",
        ///	"hiredate":"2017",
        ///	"salary":"5000"
        ///	"employeenumber":"e11111";
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            Debug.WriteLine(NewTeacher.teacherfname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, salary, hiredate, employeenumber) values (@teacherfname,@teacherlname,@salary,@hiredate, @employeenumber)";
            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.salary);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.hiredate);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.employeenumber);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();



        }

    } 
    
}


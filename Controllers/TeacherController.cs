using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schoolProject.Models;
using System.Diagnostics;

namespace schoolProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {

            return View();
        }
        //GET : /Teacher/List
        public ActionResult List(string search=null)
        {
            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> newTeacher = Controller.ListTeachers(search);
            return View(newTeacher);

        }
        //GET : /Teacher/DeletConfirmation/{id}
         public ActionResult DeletConfirm(int id)
        {      
                TeacherDataController Controller = new TeacherDataController();
                Teacher searchedTeacher = Controller.FindTeacher(id);
            
            return View(searchedTeacher);
        }
        //GET : /Teacher/Show/
        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher searchedTeacher = Controller.FindTeacher(id);

            return View(searchedTeacher);
        }
        //Get :/Teacher/showforDelet
        public ActionResult ShowforDelet(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher searchedTeacher = Controller.FindTeacher(id);

            return View(searchedTeacher);
        }
        //Post :/Teacher/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DelTeacher(id);
            return RedirectToAction("List");
        }
        //Get ://Teacher/Add
        public ActionResult AddNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string teacherfname,string teacherlname,string employeenumber,string hiredate,decimal salary)
        {
            Debug.WriteLine("Check Created Method");
            Debug.WriteLine("teacherfname");
            Debug.WriteLine("teacherlname"); 
            Debug.WriteLine("employeenumber");
            Debug.WriteLine("hiredate");
            Debug.WriteLine("salary");

            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherfname = teacherfname;
            NewTeacher.teacherlname = teacherlname;
            NewTeacher.employeenumber= employeenumber;
            NewTeacher.hiredate =hiredate;
            NewTeacher.salary = salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher();
            return RedirectToAction("List");
        }


    }
}
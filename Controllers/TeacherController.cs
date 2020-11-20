using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schoolProject.Models;

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
        public ActionResult List()
        {
            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> newTeacher = Controller.ListTeachers();
            return View(newTeacher);

        }
        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {


            TeacherDataController Controller = new TeacherDataController();
                Teacher NewTeacher = Controller.FindTeacher(id);
            
            return View(NewTeacher);
        }
    }
}
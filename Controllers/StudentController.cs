using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.StudentInfo.Models;
using Nop.Plugin.Widgets.StudentInfo.Service;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.StudentInfo.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
   /* [AutoValidateAntiforgeryToken]*/
    public class StudentController : BasePluginController
    {
        private readonly IStudentService _studentService;
        private readonly IPermissionService _permissionService;
        public StudentController(IStudentService studentService, IPermissionService permissionService)
        {
            _studentService = studentService;
            _permissionService = permissionService;
        }




        [HttpPost]
        public async Task<IActionResult> Index(SearchViewModel search)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            if (search.Name is not null)
            {
                ViewBag.Students = await _studentService.GetAllStudentByNameAsync(search.Name);
            }
            else
            {
                var students = await _studentService.GetAllStudentInfoAsync();
                ViewBag.Students = students;
            }
          

            var searchFor = new SearchViewModel();

            return View("~/Plugins/Widgets.StudentInfo/Views/Index.cshtml", searchFor);
        }
        public async Task<IActionResult> Index()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            var students = await _studentService.GetAllStudentInfoAsync();

            ViewBag.Students = students;

            var searchFor = new SearchViewModel();

            return View("~/Plugins/Widgets.StudentInfo/Views/Index.cshtml", searchFor);
        }



        public async Task<IActionResult> list()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            var students = await _studentService.GetAllStudentInfoAsync();

            ViewBag.Students = students;

            var searchFor = new SearchViewModel();

            return View("~/Plugins/Widgets.StudentInfo/Views/Index.cshtml", searchFor);
        }






        [HttpGet]
        public async Task<IActionResult> AddInformations()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            var student = new StudentViewModel();

            ViewBag.StudentTitle = "Add";
            ViewBag.StudentRequestController = "Student";
            ViewBag.StudentRequestAction = "AddInformations";

            return View("~/Plugins/Widgets.StudentInfo/Views/AddOrEditForm.cshtml", student);
        }


        [HttpPost]
        public async Task<IActionResult> AddInformations(StudentViewModel students)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            students.Id = 121;

            if (!ModelState.IsValid)
                return View("~/Plugins/Widgets.StudentInfo/Views/AddOrEditForm.cshtml");
            var newStudent = new StudentsInfo()
            {
                Name = students.Name,
                Age = students.Age,
                DateOfBirth = students.DateOfBirth,
                MaritalStatus = (students.MaritalStatus == "Married") ? true : false
            };

            await _studentService.InsertStudentInfoAsync(newStudent);

            return RedirectToAction("Index", "Student");
        }



        [HttpGet]
        public async Task<IActionResult> EditInformations(int Id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            if (Id == 0)
                return RedirectToAction("Index", "Student");

            var student = await _studentService.GetStudentInfoByIdAsync(Id);
            if (student is null)
                return RedirectToAction("Index", "Student");

            var newStudent = new StudentViewModel();

            newStudent.Id = Id;
            newStudent.Name = student.Name;
            newStudent.Age = student.Age;
            newStudent.DateOfBirth = student.DateOfBirth;
            newStudent.MaritalStatus = (student.MaritalStatus == true) ? "Married" : "Unmarried";

            ViewBag.StudentTitle = "Edit";
            ViewBag.StudentRequestController = "Student";
            ViewBag.StudentRequestAction = "EditInformations";

            return View("~/Plugins/Widgets.StudentInfo/Views/AddOrEditForm.cshtml", newStudent);

        }



        [HttpPost]
        public async Task<IActionResult> EditInformations(StudentViewModel students)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            if (!ModelState.IsValid)
            {
                ViewBag.StudentTitle = "Edit";
                ViewBag.StudentRequestController = "Student";
                ViewBag.StudentRequestAction = "EditInformations";
                return View("~/Plugins/Widgets.StudentInfo/Views/AddOrEditForm.cshtml");
            }

            var newStudent = new StudentsInfo()
            {
                Id = students.Id,
                Name = students.Name,
                Age = students.Age,
                DateOfBirth = students.DateOfBirth,
                MaritalStatus = (students.MaritalStatus == "Married") ? true : false
            };

            await _studentService.UpdateStudentInfoAsync(newStudent);

            return RedirectToAction("Index", "Student");
        }



        [HttpGet]
        public async Task<IActionResult> DeleteInformations(int Id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();  

            
            if (Id == 0)
                return RedirectToAction("Index", "Student");

            var student = await _studentService.GetStudentInfoByIdAsync(Id);
            if (student is null)
                return RedirectToAction("Index", "Student");

            await _studentService.DeleteStudentInfoAsync(student);
            return RedirectToAction("Index", "Student");

        }



    }
}

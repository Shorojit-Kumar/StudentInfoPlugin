using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.StudentInfo.Domain;
using Nop.Plugin.Widgets.StudentInfo.Factory;
using Nop.Plugin.Widgets.StudentInfo.Models;
using Nop.Plugin.Widgets.StudentInfo.Service;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Areas.Admin.Models.Logging;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.StudentInfo.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    /* [AutoValidateAntiforgeryToken]*/
    public class StudentRecordController:BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly IStudentService _studentService;
        private readonly IStudentFactory _studentFactory;
        private readonly IPermissionService _permissionService;

        public StudentRecordController(
            IStudentService studentService,
            IStudentFactory studentFactory,
            ILocalizationService localizationService,
            IPermissionService permissionService
            )
        {
            _studentService = studentService;
            _permissionService = permissionService;
            _localizationService = localizationService;
            _studentFactory = studentFactory;
        }


        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }


        public virtual async Task<IActionResult> List()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSystemLog))
                return AccessDeniedView();

            //prepare model
            var model = await _studentFactory.PrepareStudentSearchModelAsync(new StudentRecordSearchModel());

            return View("~/Plugins/Widgets.StudentInfo/Views/List.cshtml",model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> StudentList(StudentRecordSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSystemLog))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = await _studentFactory.PrepareStudentListModelAsync(searchModel);

            return Json(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSystemLog))
                return AccessDeniedView();

            //try to get a log with the specified id
            var student= await _studentService.GetStudentInfoByIdAsync(id);
            if (student == null)
                return RedirectToAction("List");

            await _studentService.DeleteStudentInfoAsync(student);

    
            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteSelected(ICollection<int> selectedIds)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSystemLog))
                return AccessDeniedView();

            if (selectedIds == null || selectedIds.Count == 0)
                return NoContent();

            await _studentService.DeleteStudentRecordsAsync(selectedIds.ToList());
  
            return Json(new { Result = true });
        }


        public virtual async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();

            //prepare model
            var model = new StudentRecordModel()
            {
                DateOfBirth = DateTime.UtcNow
            };

            return View("~/Plugins/Widgets.StudentInfo/Views/Create.cshtml", model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(StudentRecordModel students)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();

            if(ModelState.IsValid)
            {
                var newStudent = new StudentModel()
                {
                    Name = students.Name,
                    Age = students.Age,
                    DateOfBirth=students.DateOfBirth ,
                    MaritalStatus =( students.MaritalStatus=="Married")? true:false,
                    CreatedOn = DateTime.UtcNow,
                    
                };

                await _studentService.InsertStudentInfoAsync(newStudent);
                return RedirectToAction("List");
            }

            return View("~/Plugins/Widgets.StudentInfo/Views/Create.cshtml", students);
        }



        public virtual async Task<IActionResult> Edit(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            //try to get a category with the specified id
            var student = await _studentService.GetStudentInfoByIdAsync(id);
            if (student == null)
                return RedirectToAction("List");

            //prepare model
            var model = await _studentFactory.PrepareStudentModelAsync(student);

            return View("~/Plugins/Widgets.StudentInfo/Views/Edit.cshtml",model);
        }



        [HttpPost]
        public virtual async Task<IActionResult> Edit(StudentRecordModel student)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();
            
            //try to get a category with the specified id
            var exStudent = await _studentService.GetStudentInfoByIdAsync(student.Id);
            if (exStudent == null)
                return RedirectToAction("List");

            exStudent.Name = student.Name;
            exStudent.Age = student.Age;
            exStudent.DateOfBirth   = student.DateOfBirth;
            exStudent.MaritalStatus= student.MaritalStatus=="Married" ?true :false;
           
            //prepare model
            bool isUpdated=await _studentService.UpdateStudentInfoAsync(exStudent);
            if(isUpdated)
            {
                return RedirectToAction("List");
            }

            return View("~/Plugins/Widgets.StudentInfo/Views/Edit.cshtml", student);
        }

















    }
}

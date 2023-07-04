using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.StudentInfo.Domain;
using Nop.Plugin.Widgets.StudentInfo.Models;
using Nop.Plugin.Widgets.StudentInfo.Service;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Html;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Logging;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Widgets.StudentInfo.Factory
{

    public class StudentFactory : IStudentFactory
    {

        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IHtmlFormatter _htmlFormatter;
        private readonly ILocalizationService _localizationService;
        private readonly IStudentService _studentService;

        public StudentFactory(
            IBaseAdminModelFactory baseAdminModelFactory,
            IDateTimeHelper dateTimeHelper,
            IHtmlFormatter htmlFormatter,
            ILocalizationService localizationService,
            IStudentService studentService

            )
        {
            _baseAdminModelFactory = baseAdminModelFactory;
            _dateTimeHelper = dateTimeHelper;
            _htmlFormatter = htmlFormatter;
            _localizationService = localizationService;
            _studentService = studentService;

        }


        public async Task<StudentRecordListModel> PrepareStudentListModelAsync(StudentRecordSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get parameters to filter log
            var createdOnFromValue = searchModel.CreatedOnFrom.HasValue
                ? (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.CreatedOnFrom.Value, await _dateTimeHelper.GetCurrentTimeZoneAsync()) : null;
            var createdToFromValue = searchModel.CreatedOnTo.HasValue
                ? (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.CreatedOnTo.Value, await _dateTimeHelper.GetCurrentTimeZoneAsync()).AddDays(1) : null;

            var records = await _studentService.GetAllRecordsAsync(searchModel, searchModel.Page - 1, searchModel.PageSize,createdOnFromValue, createdToFromValue);

            var model = await new StudentRecordListModel().PrepareToGridAsync(searchModel, records, () =>
            {
                return records.SelectAwait(async record => new StudentRecordModel
                {
                    Id = record.Id,
                    Name = record.Name,
                    Age = record.Age,
                    MaritalStatus = record.MaritalStatus==true? "Married":"Single",
                    DateOfBirth = record.DateOfBirth,
                    CreatedOn = (record.CreatedOn.HasValue)
                        ? _dateTimeHelper.ConvertToUserTimeAsync(record.CreatedOn.Value, DateTimeKind.Utc).Result
                        : null
                });
            });


            return model;
        }

        public async Task<StudentRecordModel> PrepareStudentModelAsync(StudentModel searchModel)
        {
            if(searchModel== null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }
            var model = new StudentRecordModel()
            {
                Id = searchModel.Id,
                Name = searchModel.Name,
                Age = searchModel.Age,
                DateOfBirth = searchModel.DateOfBirth,
                MaritalStatus = (searchModel.MaritalStatus == true) ? "Married" : "Single"
             };
            return model;
        }

        public async Task<StudentRecordSearchModel> PrepareStudentSearchModelAsync(StudentRecordSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }
    }
}

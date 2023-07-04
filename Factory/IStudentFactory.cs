using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.StudentInfo.Domain;
using Nop.Plugin.Widgets.StudentInfo.Models;

namespace Nop.Plugin.Widgets.StudentInfo.Factory
{
    public interface IStudentFactory
    {
        Task<StudentRecordSearchModel> PrepareStudentSearchModelAsync(StudentRecordSearchModel searchModel);
        Task<StudentRecordListModel> PrepareStudentListModelAsync(StudentRecordSearchModel searchModel);
        Task<StudentRecordModel> PrepareStudentModelAsync(StudentModel searchModel);
    }
}

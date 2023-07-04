using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Nop.Core;
using Nop.Plugin.Widgets.StudentInfo.Domain;
using Nop.Plugin.Widgets.StudentInfo.Models;

namespace Nop.Plugin.Widgets.StudentInfo.Service
{
    public interface IStudentService
    {

        Task<StudentModel> GetStudentInfoByIdAsync(int id);

        Task<bool> InsertStudentInfoAsync(StudentModel students);
        Task<bool> UpdateStudentInfoAsync(StudentModel students);

        Task<bool> DeleteStudentInfoAsync(StudentModel students);

        Task<bool> DeleteStudentRecordsAsync(List<int> ids);
        Task<IPagedList<StudentModel>> GetAllRecordsAsync(StudentRecordSearchModel searchModel, int pageIndex, int pageSize, DateTime? fromUtc = null, DateTime? toUtc = null);

        Task<List<StudentModel>> GetAllStudentByNameAsync(string name);
    }
}

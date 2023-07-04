using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using Nop.Core;
using Nop.Data;
using Nop.Plugin.Widgets.StudentInfo.Domain;
using Nop.Plugin.Widgets.StudentInfo.Models;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.StudentInfo.Service
{
    public class StudentService : IStudentService
    {
        protected readonly IRepository<StudentModel> _studentRepository;
        public StudentService(IRepository<StudentModel> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> DeleteStudentInfoAsync(StudentModel students)
        {
            await _studentRepository.DeleteAsync(students);
            return true;
        }

        public async Task<bool> DeleteStudentRecordsAsync(List<int> ids)
        {
            await _studentRepository.DeleteAsync(await _studentRepository.GetByIdsAsync(ids));
            return true;
        }


        public async Task<List<StudentModel>> GetAllStudentByNameAsync(string name)
        {
            var filteredQuery = await _studentRepository.GetAllAsync(query =>
            {
                return query.Where(x => x.Name.Contains(name));
            });

            return filteredQuery.ToList();
        }

        public async Task<IPagedList<StudentModel>> GetAllRecordsAsync(StudentRecordSearchModel searchModel,  int pageIndex , int pageSize ,DateTime? fromUtc= null,DateTime ? toUtc = null)
        {
            var res= await _studentRepository.GetAllPagedAsync(query =>
            {
                if (fromUtc.HasValue)
                    query = query.Where(l => fromUtc.Value <= l.CreatedOn);
                if (toUtc.HasValue)
                    query = query.Where(l => toUtc.Value >= l.CreatedOn);
                if (!string.IsNullOrEmpty( searchModel.Name))
                    query = query.Where(l => l.Name==searchModel.Name);

                if (searchModel.Age>0)
                    query = query.Where(l => l.Age == searchModel.Age);
                if (searchModel.DateOfBirth.HasValue)
                    query = query.Where(l => l.DateOfBirth >= searchModel.DateOfBirth);
                if (searchModel.MaritalStatus!=null)
                    query = query.Where(l => l.MaritalStatus == searchModel.MaritalStatus);

                return query;
            }, pageIndex, pageSize);

            return res;
        }

        public async Task<StudentModel> GetStudentInfoByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<bool> InsertStudentInfoAsync(StudentModel students)
        {
            await _studentRepository.InsertAsync(students);
            return true;
        }

        public async Task<bool> UpdateStudentInfoAsync(StudentModel students)
        {
            await _studentRepository.UpdateAsync(students);
            return true;
        }
    }
}

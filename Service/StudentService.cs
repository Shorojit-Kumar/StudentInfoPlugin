using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using Nop.Data;
using Nop.Plugin.Widgets.StudentInfo.Models;

namespace Nop.Plugin.Widgets.StudentInfo.Service
{
    public class StudentService : IStudentService
    {
        protected readonly IRepository<StudentsInfo> _studentRepository;
        public StudentService(IRepository<StudentsInfo> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> DeleteStudentInfoAsync(StudentsInfo students)
        {
            await _studentRepository.DeleteAsync(students);
            return true;
        }

        public async Task<List<StudentsInfo>> GetAllStudentByNameAsync(string name)
        {
            var filteredQuery = await _studentRepository.GetAllAsync(query =>
            {
                return query.Where(x => x.Name.Contains(name));
            });

            return filteredQuery.ToList();
        }

        public async Task<List<StudentsInfo>> GetAllStudentInfoAsync()
        {
            return await _studentRepository.GetAll().ToListAsync();
        }

        public async Task<StudentsInfo> GetStudentInfoByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<bool> InsertStudentInfoAsync(StudentsInfo students)
        {
            await _studentRepository.InsertAsync(students);
            return true;
        }

        public async Task<bool> UpdateStudentInfoAsync(StudentsInfo students)
        {
            await _studentRepository.UpdateAsync(students);
            return true;
        }
    }
}

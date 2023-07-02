using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.StudentInfo.Models;

namespace Nop.Plugin.Widgets.StudentInfo.Service
{
    public interface IStudentService
    {

        Task<StudentsInfo> GetStudentInfoByIdAsync(int id);

        Task<bool> InsertStudentInfoAsync(StudentsInfo students);
        Task<bool> UpdateStudentInfoAsync(StudentsInfo students);

        Task<bool> DeleteStudentInfoAsync(StudentsInfo students);

        Task<List<StudentsInfo>> GetAllStudentInfoAsync();

        Task<List<StudentsInfo>> GetAllStudentByNameAsync(string name);
    }
}

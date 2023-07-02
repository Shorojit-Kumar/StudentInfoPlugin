using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.StudentInfo.Models;

namespace Nop.Plugin.Widgets.StudentInfo.Factory
{
    public interface IStudentFactory
    {
        Task<StudentSearchModel> PrepareStudentSearchModelAsync(StudentSearchModel searchModel);
    }
}

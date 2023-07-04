using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;

namespace Nop.Plugin.Widgets.StudentInfo.Domain
{
    public class StudentModel:BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool MaritalStatus { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
    }
}

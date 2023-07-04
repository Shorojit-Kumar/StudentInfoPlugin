using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.StudentInfo.Models
{
    public partial record StudentRecordSearchModel : BaseSearchModel
    {
        public StudentRecordSearchModel()
        {
        }
        public int? Id { get; set; }
        [NopResourceDisplayName("Student.Search.Name")]
        public string? Name { get; set; }
        [NopResourceDisplayName("Student.Search.Age")]
        public int? Age { get; set; }
        [NopResourceDisplayName("Student.Search.DateOfBirth")]

        public DateTime? CreatedOnFrom { get; set; }

        [NopResourceDisplayName("Admin.System.StudentRecord.List.CreatedOnTo")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnTo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [NopResourceDisplayName("Student.Search.MaritalStatus")]
        public bool? MaritalStatus { get; set; }
    }
}

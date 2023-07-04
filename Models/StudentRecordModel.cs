using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Nop.Core;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.StudentInfo.Models
{
    public partial record StudentRecordModel: BaseNopEntityModel
    {

        [NopResourceDisplayName("Student.Create.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Student.Create.Age")]
        public int Age { get; set; }

        [NopResourceDisplayName("Student.Create.DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [NopResourceDisplayName("Student.Create.MaritalStatus")]
        public string MaritalStatus { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}

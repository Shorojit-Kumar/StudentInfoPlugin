using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.StudentInfo.Domain;
using Nop.Plugin.Widgets.StudentInfo.Models;

namespace Nop.Plugin.Widgets.StudentInfo.Data
{
    public class StudentBuilder : NopEntityBuilder<StudentModel>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
           .WithColumn(nameof(StudentModel.Age)).AsInt32()
           .WithColumn(nameof(StudentModel.DateOfBirth)).AsDateTime()

           .WithColumn(nameof(StudentModel.Name)).AsString(400)

           .WithColumn(nameof(StudentModel.MaritalStatus)).AsBoolean()
           .WithColumn(nameof(StudentModel.CreatedOn)).AsDateTime();
        }
    }
}

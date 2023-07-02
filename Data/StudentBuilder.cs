using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.StudentInfo.Models;

namespace Nop.Plugin.Widgets.StudentInfo.Data
{
    public class StudentBuilder : NopEntityBuilder<StudentsInfo>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
           .WithColumn(nameof(StudentsInfo.Age)).AsInt32()
           .WithColumn(nameof(StudentsInfo.DateOfBirth)).AsDateTime()

           .WithColumn(nameof(StudentsInfo.Name)).AsString(400)
           
           .WithColumn(nameof(StudentsInfo.MaritalStatus)).AsBoolean();
        }
    }
}

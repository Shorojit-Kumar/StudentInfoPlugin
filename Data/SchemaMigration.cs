using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.StudentInfo.Domain;
using Nop.Plugin.Widgets.StudentInfo.Models;

namespace Nop.Plugin.Widgets.StudentInfo.Data
{
    [NopMigration("2021/08/28 08:40:55:1289041", "Other.StudentInfo base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<StudentModel>();
        }
    }
}

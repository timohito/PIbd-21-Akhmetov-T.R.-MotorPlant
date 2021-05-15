using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MotorPlantBusinessLogic.BusinessLogics;

namespace MotorPlantDatabaseImplement.Implements
{
    public class BackUpLogiс : BackupAbstractLogic
    {
        protected override Assembly GetAssembly()
        {
            return typeof(BackUpLogiс).Assembly;
        }
        protected override List<PropertyInfo> GetFullList()
        {
            using (var context = new MotorPlantDatabase())
            {
                Type type = context.GetType();
                return type.GetProperties().Where(x => x.PropertyType.FullName.StartsWith("Microsoft.EntityFrameworkCore.DbSet")).ToList();
            }
        }
        protected override List<T> GetList<T>()
        {
            using (var context = new MotorPlantDatabase())
            {
                return context.Set<T>().ToList();
            }
        }
    }
}

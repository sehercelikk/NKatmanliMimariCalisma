using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Func;

public static class NameUpper
{
    private static string replaceHarf(string data) => data.ToUpper().Replace("İ", "I");
    public static void OracleNameUpper(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(replaceHarf(entity.GetDefaultTableName()));
            foreach (var item in entity.GetProperties())
            {
                item.SetColumnName(replaceHarf(item.GetDefaultColumnBaseName()));
            }
        }
    }
}

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.Data.Context
{
    public class SQLServerContextFactory : IDesignTimeDbContextFactory<ApsContext>
    {
        public ApsContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=USU-BCK1EGQQNPU; Database=GESTAOTAREFAS; User Id=teste; Password=123456;TrustServerCertificate=True;";

            var optionsBuilder = new DbContextOptionsBuilder<ApsContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new ApsContext(optionsBuilder.Options);
        }
    }
}


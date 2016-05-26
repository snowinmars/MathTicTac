using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DAL.Dao
{
    internal static class SqlConfig
    {
        internal const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MathTicTacDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}

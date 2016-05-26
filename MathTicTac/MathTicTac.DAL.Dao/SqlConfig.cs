using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DAL.Dao
{
    internal static class SqlConfig
    {
        internal static string ConnectionString { get; } = @"Data Source=EPRUSARW9734\SQLEXPRESS;Initial Catalog=MathTicTac;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Core
{
    internal static class SqlClientExtensions
    {
        public static object CoalesceNullToDBNull(this object? obj)
            => obj ?? DBNull.Value;

        public static object? ParseFromDbNullable(this SqlParameter parameter)
            => parameter.Value == DBNull.Value ? null : parameter.Value;

        public static int? GetNullableInt32(this SqlDataReader r, string ParameterName)
            => r.IsDBNull(r.GetOrdinal(ParameterName)) ? new int? { } : r.GetInt32(r.GetOrdinal(ParameterName));
        public static string? GetNullableString(this SqlDataReader r, string ParameterName)
            => r.IsDBNull(r.GetOrdinal(ParameterName)) ? null : r.GetString(r.GetOrdinal(ParameterName));

    }


}

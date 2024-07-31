using CongratulateApi.DataAccess.TypeHandlers;
using Dapper;

namespace CongratulateApi.DataAccess.Infrastructure;

public static class DapperConfiguration
{
    public static void Apply()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        SqlMapper.AddTypeHandler(new DateOnlyHandler());
    }
}
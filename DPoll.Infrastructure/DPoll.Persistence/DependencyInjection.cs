using DPoll.Persistence.Common;
using DPoll.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace DPoll.Persistence;
public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services,string sqlConnection)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(sqlConnection), ServiceLifetime.Singleton);
        services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();
        services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}

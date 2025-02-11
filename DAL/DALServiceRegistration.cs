using DAL.Repository.Implementations;
using DAL.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public static class DALServiceRegistration
{
    public static IServiceCollection AddDALService(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        return services;
    }
}
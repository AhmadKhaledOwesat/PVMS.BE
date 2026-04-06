using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PVMS.Application.Bll;
using PVMS.Application.Dal;
using PVMS.Application.Interfaces;
using PVMS.Application.Mapper;
using PVMS.Application.Services;
using PVMS.Domain;
using PVMS.Domain.Interfaces;
using PVMS.Infrastructure.EfContext;
using PVMS.Infrastructure.Repositories;
namespace PVMS.Application.DI
{
    public static class ServiceExtension
    {
        public static void AddInnovaMapper(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<InnovaMapper>();
            });

        }

        public static void AddEfDbContext(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            serviceDescriptors.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            serviceDescriptors.AddDbContext<StudioContext>(options => options.UseLazyLoadingProxies().UseSqlServer(
                configuration.GetConnectionString("PVMS"),
                 x => x.UseNetTopologySuite()));
        }

        public static void AddServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.Scan(scan => scan
               .FromAssembliesOf(typeof(ITicketTypeBll))
               .AddClasses(c => c.Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition && t.Name.EndsWith("Bll")))
               .AsImplementedInterfaces()
               .WithScopedLifetime());
            serviceDescriptors.AddScoped<IEmailSender, EmailSender>();
            serviceDescriptors.AddSingleton<IInnovaMapper, InnovaMapper>();
            serviceDescriptors.AddScoped(typeof(IIdentityManager<>), typeof(IdentityManager<>));
            serviceDescriptors.AddScoped<IAuthenticationManager, AuthenticationManager>();
            serviceDescriptors.AddScoped(typeof(IBaseBll<,,>), typeof(BaseBll<,,>));
            serviceDescriptors.AddScoped(typeof(IBaseDal<,,>), typeof(BaseDal<,,>));
            serviceDescriptors.AddScoped(typeof(IEfRepository<,>), typeof(EfRepository<,>));
            serviceDescriptors.AddScoped<INotificationService, NotificationService>();
            serviceDescriptors.AddScoped<ICivilStatusService, CivilStatusService>();
            serviceDescriptors.AddScoped<ICitizenImageBackfillService, CitizenImageBackfillService>();
            serviceDescriptors.AddScoped<IWordService, WordService>();
            serviceDescriptors.AddScoped<ISmsService, SmsService>();
            serviceDescriptors.AddScoped<IReportService, ReportService>();

            serviceDescriptors.AddScoped(provider =>
            {
                return new Lazy<IUserBll>(() => provider.GetRequiredService<IUserBll>());
            });
        }

    }
}

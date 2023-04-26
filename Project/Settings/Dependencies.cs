using Core.Services;
using DataLayer;
using DataLayer.Repositories;

namespace Project.Settings
{
    public static class Dependencies
    {
        #region Private members
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ClassRepository>();
            services.AddScoped<GradeRepository>();
            services.AddScoped<UserRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<AuthorizationService>();
            services.AddScoped<ClassService>();
            services.AddScoped<GradeService>();
            services.AddScoped<PasswordService>();
            services.AddScoped<UserService>();
        }
        #endregion

        #region Public members
        public static void Inject(WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddControllers();
            applicationBuilder.Services.AddSwaggerGen();

            applicationBuilder.Services.AddDbContext<AppDbContext>();

            AddRepositories(applicationBuilder.Services);
            AddServices(applicationBuilder.Services);
        }
        #endregion
    }
}

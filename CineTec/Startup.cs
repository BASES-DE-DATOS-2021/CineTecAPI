
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CineTec.DataAccess;

namespace CineTec
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.  
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //var sqlConnectionString = Configuration["PostgreSqlConnectionString"];
            var connString = "Host=localhost;Username=admin;Password=admin;Database=cinetecdb";

            services.AddDbContext<PostgreSqlContext>(options => options.UseNpgsql(connString));

            services.AddScoped<IClientProvider, ClientProvider>();
            services.AddScoped<IBranchProvider, BranchProvider>();
            services.AddScoped<IRoomProvider, RoomProvider>();
            services.AddScoped<IMovieProvider, MovieProvider>();
            services.AddScoped<IEmployeeProvider, EmployeeProvider>();
            services.AddScoped<IProjectionProvider, ProjectionProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

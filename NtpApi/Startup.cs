
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ntp.Schemes;
using NtpApi.Queries;
using NtpApi.Repositories;
using NtpApi.Settings;
using NtpApi.Types;

namespace NtpApi
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
            services.AddMvc();

            services.Configure<MongoSettings>(options => {
                options.ConnectionString
                    = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database
                    = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddScoped<FixturesQuery>();
            services.AddTransient<IFixturesRepository, FixturesRepository>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddTransient<FixtureType>();

            var sp = services.BuildServiceProvider();
            services.AddScoped<ISchema>(_ => new FixturesSchema(type => (GraphType)sp.GetService(type)) { Query = sp.GetService<FixturesQuery>() });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

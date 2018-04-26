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
using NtpApi.Models;

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

            services.AddScoped<NTPQuery>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            
            services.AddTransient<ICollectionRepository<Fixture>, FixturesRepository>();
            services.AddTransient<ICollectionRepository<Country>, CountriesRepository>();
            services.AddTransient<ICollectionRepository<Season>, SeasonsRepository>();
            services.AddTransient<FixtureType>();
            services.AddTransient<CountryType>();
            services.AddTransient<SeasonType>();

            var sp = services.BuildServiceProvider();
            
            services.AddScoped<ISchema>(_ => new NTPSchema(type => (GraphType)sp.GetService(type))
                { Query = sp.GetService<NTPQuery>() });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphiQl();
            app.UseMvc();
        }
    }
}

using Augmentr.Dal;
using Augmentr.Domain;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Augmentr
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

            services.AddDbContext<DataContext>(options => options.UseMySQL("Server=localhost;Database=augmentr;user=root;password=admin"));


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<ITokenFactory, TokenFactory>();

            services.AddTransient<IJwtEncoder>(_ => new JwtEncoder(new HMACSHA256Algorithm(), new JsonNetSerializer(), new JwtBase64UrlEncoder()));
            services.AddTransient<IJwtDecoder>(_ => CreateJwtDecoder());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseMySQL("Server=localhost;Database=augmentr;user=root;password=admin");

            using (var context = new DataContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        private static JwtDecoder CreateJwtDecoder()
        {
            var serializer = new JsonNetSerializer();
            var provider = new UtcDateTimeProvider();

            return new JwtDecoder(serializer, new JwtValidator(serializer, provider), new JwtBase64UrlEncoder());
        }
    }
}

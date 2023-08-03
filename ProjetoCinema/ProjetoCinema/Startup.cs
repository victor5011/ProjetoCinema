using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

using ProjetoCinema.Context;
using ProjetoCinema.Models;
using ProjetoCinema.Repository;
using ProjetoCinema.Repository.Interfaces;
using ProjetoCinema.Services;
using ReflectionIT.Mvc.Paging;

namespace ProjetoCinema
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
            services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddTransient<IFilmesRepository,FilmesRepository>();
            services.AddTransient<ICategoriaRepository,CategoriaRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<ICadeirasRepository, CadeirasRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();

            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", politica =>
                {
                    politica.RequireRole("Admin");
                });

                options.AddPolicy("Member", politica =>
                {
                    politica.RequireRole("Member");
                });
            });

            //services.AddDefaultIdentity<IClienteRepository>().AddEntityFrameworkStores<AppDbContext>();


            services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.PageParameterName = "pageindex";
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;
                
            });

            services.AddMemoryCache();
            services.AddSession();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ISeedUserRoleInitial seedUserRoleInitial)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            seedUserRoleInitial.SeedRoles();
            seedUserRoleInitial.SeedUsers();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            
            
            

            app.UseEndpoints(endpoints =>
            {
                 endpoints.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                    );

                //endpoints.MapControllerRoute(
                //    name: "categoriaFiltro",
                //    pattern: "Filme/{action}/{categoria}",
                //    defaults: new { controller = "Filme", Action = "List" });

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Member}/{action=Index}/{id?}"
                  );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}

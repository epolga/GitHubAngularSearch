namespace GitHubSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            ConfigureServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");
            app.UseSession();

            app.Run();
        }

        static public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddHttpContextAccessor();
        }
    }
}
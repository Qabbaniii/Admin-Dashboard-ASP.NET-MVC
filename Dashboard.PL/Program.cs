using Dashboard.BLL.Common.Services.Attachment;
using Dashboard.BLL.Profiles;
using Dashboard.BLL.Services.DepartmentService;
using Dashboard.BLL.Services.EmployeeService;
using Dashboard.DAL.Contexts;
using Dashboard.DAL.Repositories.DepartmentRepo;
using Dashboard.DAL.Repositories.EmployeeRepo;
using Dashboard.DAL.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*1*/
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(Option => Option.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));                 //add Controllers service
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // add context service
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddScoped<IAttachmentServices, AttachmentServices>();
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
            /*2*/
            var app = builder.Build();

            // Configure middelware (piplines)
            app.UseHttpsRedirection();
            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.UseStaticFiles();           //use files in html like css and bootstrap
            /*3*/
            app.Run();
        }
    }
}

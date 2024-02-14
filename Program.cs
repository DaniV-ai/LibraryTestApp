using LibraryTestApp.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryTestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddDbContext<LibraryDbContext>(opt =>
                opt.UseNpgsql(@"Server=localhost:5432;Database=LibraryTestDB;User ID=user"));

            builder.Services.AddCors();

            var app = builder.Build();


            app.UseDeveloperExceptionPage();


            app.UseDefaultFiles();
            app.UseStaticFiles();




            app.UseCors(options =>
            {
                options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
            }); 

            app.MapControllers();

            app.Run();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using konyvtar_kezelo_backend.Data;
using konyvtar_kezelo_backend.Services;
using konyvtar_kezelo_backend.Services.Interfaces;

namespace konyvtar_kezelo_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            
            // Set database connection
            builder.Services.AddDbContext<LibraryDBContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            // Set services
            builder.Services.AddScoped<IBookService, BookService>(); // Scope mert HTTPS kérésenként egy példányt kell visszaadni
            builder.Services.AddScoped<IReaderService, ReaderService>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}


using ClothingStore.Repos;
using ClothingStore.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

namespace ClothingStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ClothingStoreContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            builder.Services.AddScoped<BrandRepo>();
            builder.Services.AddScoped<CartItemRepo>();
            builder.Services.AddScoped<CartRepo>();
            builder.Services.AddScoped<CategoryRepo>();
            builder.Services.AddScoped<OrderItemRepo>();
            builder.Services.AddScoped<OrderRepo>();
            builder.Services.AddScoped<ProductRepo>();
            builder.Services.AddScoped<ProductVariantRepo>();
            builder.Services.AddScoped<ReviewRepo>();
            builder.Services.AddScoped<UserRepo>();

            //Register Service
            builder.Services.AddScoped<BrandService>();
            builder.Services.AddScoped<CartService>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ProductVariantService>();
            builder.Services.AddScoped<ReviewService>();
            builder.Services.AddScoped<UserService>();










            builder.Services.AddControllers();
            builder.Services.AddRateLimiter(options =>
            {
                // Sliding window — for public endpoints (products, categories, brands, reviews)
                options.AddSlidingWindowLimiter("public", opt =>
                {
                    opt.PermitLimit = 100;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.SegmentsPerWindow = 6; // splits window into 6 x 10s segments
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 10;
                });

                // Fixed window — for non-public endpoints (cart, orders, users)
                options.AddFixedWindowLimiter("private", opt =>
                {
                    opt.PermitLimit = 50;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 5;
                });

                options.RejectionStatusCode = 429;
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapGet("/", () => Results.Redirect("/swagger"));
            }

            app.UseHttpsRedirection();
            app.UseRateLimiter();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

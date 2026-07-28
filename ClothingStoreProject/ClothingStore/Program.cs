
using ClothingStore.Repos;
using ClothingStore.Services;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
            
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

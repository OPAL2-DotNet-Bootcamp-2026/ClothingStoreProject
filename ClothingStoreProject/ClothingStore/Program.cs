
using ClothingStore.Repos;
using ClothingStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.RateLimiting;

namespace ClothingStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register Context
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
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ProductVariantService>();
            builder.Services.AddScoped<ReviewService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IEmailService,EmailService>();

            // ── JWT Authentication ─────────────────────────────────────────────
            builder.Services.AddScoped<AuthService>();

            var jwtKey = builder.Configuration["JwtSettings:SecretKey"];
            var jwtIssuer = builder.Configuration["JwtSettings:Issuer"];
            var jwtAudience = builder.Configuration["JwtSettings:Audience"];

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.MapInboundClaims = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        RoleClaimType = "role",
                        IssuerSigningKey = new SymmetricSecurityKey(
                                                       Encoding.UTF8.GetBytes(jwtKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            var error = context.Exception.Message;
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var claims = context.Principal.Claims.ToList();
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            var error = context.Error;
                            var desc = context.ErrorDescription;
                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddAuthorization();







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

            //Swagger
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token in the box below"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            new List<string>()
        }
    });
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
            app.UseAuthentication();
            app.UseRateLimiter();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

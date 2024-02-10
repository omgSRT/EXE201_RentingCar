using BusinessObjects.Models;
using Microsoft.OpenApi.Models;

namespace RentingCarAPI.Startup
{
    public static class Startup
    {
        public static IServiceCollection CustomSwagger(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddDbContext<exe201Context>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SignIn", Version = "v1" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT", // Đặt định dạng Bearer là "JWT"
                    In = ParameterLocation.Header, // Xác định vị trí của token trong yêu cầu (Header)
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } }
    };

                c.AddSecurityRequirement(securityRequirement);
            });
            return services;
        }
    }
}

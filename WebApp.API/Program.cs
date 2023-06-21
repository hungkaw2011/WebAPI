using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApp.API.Data;
using WebApp.API.Mappings;
using WebApp.API.Repositories;
using WebApp.API.Repositories.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });
	options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
	{
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = JwtBearerDefaults.AuthenticationScheme
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = JwtBearerDefaults.AuthenticationScheme
				},
				Scheme = "Oauth2",
				Name = JwtBearerDefaults.AuthenticationScheme,
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});
});
//Cấu hình dịch vụ xác thực JWT Bearer trong ASP.NET Core,
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
    });
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()                                                   //Thêm hỗ trợ cho việc quản lý vai trò (roles) trong Identity
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("WebAPI")   //Cung cấp mã thông báo (token provider) tùy chỉnh cho hệ thống.
    .AddEntityFrameworkStores<AuthenticationDbContext>()                // Lưu trữ thông tin người dùng và vai trò vào cơ sở dữ liệu 
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<ApplicationDbContext>(
	options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
	);
builder.Services.AddDbContext<AuthenticationDbContext>(
	options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthenticationConnectionString"))
	);

// Dependency Injection
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<ITokenReporsitory, TokenRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
// DTO AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Xác thực và ủy quuyền vào các tài nguyên truy cập
app.UseAuthentication();
app.UseAuthorization();

//Cho phép truy cập các tệp tĩnh, như hình ảnh, từ các thư mục được chỉ định trên máy chủ
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
	RequestPath = "/Images"
});

//Định tuyến các yêu cầu HTTP tới các điều khiển (controllers) và hành động (actions) tương ứng trong ứng dụng
app.MapControllers();

app.Run();

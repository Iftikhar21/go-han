using System.Text;
using go_han.Configurations;
using go_han.Data;
using go_han.Repositories.IRepository;
using go_han.Interface;
using go_han.Repositories;
using go_han.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using go_han.Repsitories.IRepositories;
using go_han.Repsitories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Go Han API", Version = "v1" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Auth Bearer Scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, new string[] { } }
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings")
);
builder.Services
    .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = c =>
            {
                var authHeader = c.Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer"))
                    c.Token = authHeader.Substring("Bearer ".Length).Trim();

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddScoped<IDivisionRepository, DivisionRepository>();
builder.Services.AddScoped<IPasswordUtils, PasswordUtils>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// ENDPOOINTS:
// ================================================================================================
// Division Management TODO: Tenxi
// GET /api/divisions -> (List Semua Divisi)

// GET /api/divisions/{id} -> (Detail Divisi)

// POST /api/divisions -> (Tambah Divisi Baru)

// PUT /api/divisions/{id} -> (Update Divisi)  

// DELETE /api/divisions/{id} -> (Hapus Divisi)
// ================================================================================================
// Role Management TODO: Daffa
// GET /api/roles -> (List Semua Role)

// GET /api/roles/{id} -> (Detail Role)

// POST /api/roles -> (Tambah Role Baru)

// PUT /api/roles/{id} -> (Update Role)

// DELETE /api/roles/{id} -> (Hapus Role)
// ================================================================================================
// Project Management TODO: Ari
// GET /api/projects -> (List Project User)

// GET /api/projects/status/{status} -> (List Project Berdasarkan Status)

// POST /api/projects -> (Buat Project Baru & Set Lead/Co-Lead)

// GET /api/projects/{id} -> (Detail Project & Tim)

// POST /api/projects/{id}/members -> (Tambah Member & Divisi ke Project)

// DELETE /api/projects/{id}/members/{userId} -> (Hapus Member dari Project)
// ================================================================================================
// Task System TODO: Dhika
// POST /api/tasks -> (Lead/Co-Lead Buat Tugas Baru)

// GET /api/tasks/project/{projectId} -> (List Semua Tugas per Project)

// GET /api/tasks/my-tasks -> (List Tugas Milik Member yang Login)

// PATCH /api/tasks/{id}/start ->(Update Status: Todo->In Progress)

// PUT /api/tasks/{id}/submit -> (Member Kirim Bukti & Request Approval)

// PUT /api/tasks/{id}/approve -> (Lead/CoLead Klik Approve Final)
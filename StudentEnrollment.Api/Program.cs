using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using StudentEnrollment.Api.Endpoints;
using StudentEnrollment.Api.Configurations;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("StudentEnrollmentDbConnection");
builder.Services.AddDbContext<StudentEnrollmentDbContext>(options => {
    options.UseSqlServer(conn);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapStudentEndpoints();

app.MapEnrollmentEndpoints();

app.MapCourseEndpoints();

app.Run();
﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using ODT_Model.Mapper;
using ODT_Service.Interface;
using ODT_Service.Service;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddInfrastructure();

/*builder.Services.AddTransient<UpdateConversationIsCloseJob>();

// Cấu hình lịch trình
builder.Services.AddSingleton(provider =>
{
    var schedulerFactory = new StdSchedulerFactory();
    var scheduler = schedulerFactory.GetScheduler().Result;

    // Đăng ký công việc lập lịch
    var updateConversationJob = new JobDetailImpl("UpdateConversationIsCloseJob", typeof(UpdateConversationIsCloseJob));
    var trigger = TriggerBuilder.Create()
        .WithIdentity("UpdateConversationIsCloseTrigger")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithIntervalInMinutes(1)
            .RepeatForever())
        .Build();

    scheduler.ScheduleJob(updateConversationJob, trigger).Wait();
    scheduler.Start().Wait();

    return scheduler;
});*/

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddControllers();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


// Service add o day
//builder.Services.AddScoped<IUserService, UserService>();

//builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
//builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<ISubcriptionService, SubcriptionService>();
//builder.Services.AddScoped<IStudentSubcriptionService, StudentSubcriptionService>();
//builder.Services.AddScoped<IBlogService, BlogService>();
//builder.Services.AddScoped<IBlogLikeService, BlogLikeService>();
//builder.Services.AddScoped<IConversationService, ConversationService>();

//builder.Services.AddScoped<IConversationMessageService, ConversationMessageService>();
//builder.Services.AddScoped<IMessageReactionService, MessageReactionService>();
//builder.Services.AddScoped<IAttachmentService, AttachmentService>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IRoleService, RoleService>();
//builder.Services.AddScoped<IPermissionService, PermissionService>();

//builder.Services.AddScoped<Tools.Firebase>();


//builder.Services.AddScoped<IBlogCommentService, BlogCommentService>();

//Mapper
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});
builder.Services.AddSingleton<IMapper>(config.CreateMapper());


// Add services to the container.
var serverVersion = new MySqlServerVersion(new Version(8, 0, 23)); // Replace with your actual MySQL server version
builder.Services.AddDbContext<MyDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MyDB");
    options.UseMySql(connectionString, serverVersion, options => options.MigrationsAssembly("ODT_API"));
}
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});



//Build CORS
/*builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    // Dòng ở dưới là đường cứng
    //build.WithOrigins("https:localhost:3000", "https:localhost:7022");

    //Dòng dưới là nhận hết
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseHttpsRedirection();
//app.UseCors("MyCors");
app.UseAuthorization();

app.MapControllers();

app.Run();

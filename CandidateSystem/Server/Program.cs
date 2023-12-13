using CandidateSystem.DataLayer;
using CandidateSystem.BussinessLogic.CandidateService;
using CandidateSystem.DataLayer.EmailRepository;
using CandidateSystem.BussinessLogic;
using CandidateSystem.Shared;
using CandidateSystem.DataLayer.InterviewMeetingRepository;
using CandidateSystem.BussinessLogic.AssessService;
using CandidateSystem.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      policy =>
//                      {
//                          policy.WithOrigins("https://localhost:7179/");
//                      });
//});
// updated on commit
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:8080/");
                          policy.AllowAnyOrigin();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                      });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:issuer"],
        ValidAudience = builder.Configuration["JWT:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});
//services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
builder.Services.AddControllers();

// Add services to the container.
// Get Connection String
string connectionString =
     builder.Configuration.GetConnectionString("DefaultConnection");
var MailConfiguration = builder
    .Configuration
    .GetSection("MailConfig")
    .Get<MailConfig>();

builder.Services.AddSingleton(MailConfiguration);
builder.Services.AddControllers();
builder.Services.RegistrationDependency();





var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

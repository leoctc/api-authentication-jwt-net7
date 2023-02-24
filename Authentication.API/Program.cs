using Authentication.API.Data;
using Authentication.API.Repository;
using Authentication.API.Services;

var builder = WebApplication.CreateBuilder(args);

// configurando API
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// configurando IoT
builder.Services.AddSingleton<IUserRepository, UserRepository>();

// configurando o swagger
SwaggerService.SwaggerSetup(builder.Services);

// configurando o JWT (Authentication)
JwtTokenService.JwtSetup(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    SwaggerService.UseSwagger(app);
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
app.Run();

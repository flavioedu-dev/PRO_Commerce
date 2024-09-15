using PROCommerce.Authentication.API.Extentions.IoC;
using PROCommerce.Authentication.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container DI.
builder.Services.AddDI(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

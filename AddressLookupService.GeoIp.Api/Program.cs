using AddressLookupService.GeoIp.Api.Contracts;
using AddressLookupService.GeoIp.Api.Models;
using AddressLookupService.GeoIp.Api.Services;
using Api.Library.Validations;
using Api.Library.Extensions;
using Microsoft.OpenApi.Models;
using Api.Library.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "AddressLookupSerice.GeoIp.Api", Version = "v1" });
});

builder.Services.RegisterDataProviderService();
builder.Services.AddSingleton<IGeoIpService, GeoIpService>();
builder.Services.AddSingleton<IValdiateAddress, ValidateAddress>();

builder.Services.AddSingleton(_ =>
{
    GeoIpOptions geoIpOptions = new();
    builder.Configuration.Bind("GeoIpOptions", geoIpOptions);
    return geoIpOptions;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Errors/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AddressLookupService.GeoIp.Api v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

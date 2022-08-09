using AddressLookupService.Gateway.Api.Contracts;
using AddressLookupService.Gateway.Api.Domain;
using AddressLookupService.Gateway.Api.Services;
using Api.Library.Contracts;
using Api.Library.Validations;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.NullValueHandling = NullValueHandling.Include);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AddressLookupSerice.Gateway.Api", Version = "v1" });
});

builder.Services.AddSingleton(_ =>
{
    ApiServiceOptions apiServiceOptions = new();
    builder.Configuration.Bind("ApiServiceOptions", apiServiceOptions);
    return apiServiceOptions;
});

builder.Services.AddSingleton<IAddressResolveService, AddressResolveService>();
builder.Services.AddSingleton<IPingLookupService, PingLookupService>();
builder.Services.AddSingleton<IRdapLookupService, RdapLookupService>();
builder.Services.AddSingleton<IRdnsLookupService, RdnsLookupService>();
builder.Services.AddSingleton<IGeoIpLookupService, GeoIpLookupService>();
builder.Services.AddSingleton<IValdiateAddress, ValidateAddress>();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Configure the HTTP request pipeline.
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
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AddressLookupService.Gateway.Api v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
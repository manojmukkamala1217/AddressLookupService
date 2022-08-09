using AddressLookupService.Rdns.Api.Contracts;
using AddressLookupService.Rdns.Api.Models;
using AddressLookupService.Rdns.Api.Services;
using Api.Library.Contracts;
using Api.Library.Extensions;
using Api.Library.Validations;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AddressLookupSerice.Rdns.Api", Version = "v1" });
});

builder.Services.RegisterDataProviderService();
builder.Services.AddSingleton<IRdnsService, RdnsService>();
builder.Services.AddSingleton<IValdiateAddress, ValidateAddress>();

builder.Services.AddSingleton(_ =>
{
    RdnsOptions RdnsOptions = new();
    builder.Configuration.Bind("RdnsOptions", RdnsOptions);
    return RdnsOptions;
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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AddressLookupService.Rdns.Api v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

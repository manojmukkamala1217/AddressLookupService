using AddressLookupService.Rdap.Api.Contracts;
using AddressLookupService.Rdap.Api.Models;
using AddressLookupService.Rdap.Api.Services;
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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AddressLookupSerice.Rdap.Api", Version = "v1" });
});

builder.Services.RegisterDataProviderService();
builder.Services.AddSingleton<IRdapService, RdapService>();
builder.Services.AddSingleton<IValdiateAddress, ValidateAddress>();

builder.Services.AddSingleton(_ =>
{
    RdapOptions RdapOptions = new();
    builder.Configuration.Bind("RdapOptions", RdapOptions);
    return RdapOptions;
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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AddressLookupService.Rdap.Api v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

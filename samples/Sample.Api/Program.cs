using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Ogu.AspNetCore.Response.Json;
using Ogu.Response.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    opts.JsonSerializerOptions.Converters.Add(new JsonDictionaryKeyConverter<string>());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{ options.SuppressModelStateInvalidFilter = true; // To capture ModelState
});

var app = builder.Build();

app.UseExceptionHandler(cfg =>
{
    cfg.Run(async (context) =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        var jsonResponse = contextFeature.Error.ToJsonResponse();
        
        jsonResponse.Extras.Add(nameof(HeaderNames.RequestId), Activity.Current?.Id ?? context.TraceIdentifier);

        await jsonResponse.ToJsonAction().ExecuteResultAsync(context);
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
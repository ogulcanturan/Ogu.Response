using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Ogu.Response;
using Ogu.Response.Abstractions;
using Sample.Api.Models.Dtos;
using Sample.Api.Models.Requests;
using Sample.Api.Models.Validated;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Sample.Api.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IValidator<GetExamplesSixteenRequest, ValidatedGetExamplesSixteen>, GetExamplesSixteenValidator>();

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // To return JsonResponse instead of default BadRequestObjectResult
    options.InvalidModelStateResponseFactory = context => context.ModelState.ToResponse().ToActionDto();

    // Disables controller's automatic model state validation.
    options.SuppressModelStateInvalidFilter = true; 
});

var app = builder.Build();

app.UseExceptionHandler(cfg =>
{
    cfg.Run(async (context) =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        var jsonResponse = contextFeature.Error.ToResponse(ExceptionTraceLevel.Basic);

        jsonResponse.Extras.Add(nameof(HeaderNames.RequestId), Activity.Current?.Id ?? context.TraceIdentifier);

        await jsonResponse.ToActionDto().ExecuteResultAsync(context);
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
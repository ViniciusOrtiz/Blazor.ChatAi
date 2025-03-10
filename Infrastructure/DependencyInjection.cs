﻿using Application.Contracts.Gateways;
using Application.Contracts.Presenters;
using Application.Contracts.Services;
using Application.Contracts.Settings;
using Application.Contracts.Tools;
using Infrastructure.Gateways;
using Infrastructure.Presenters;
using Infrastructure.Services;
using Infrastructure.Tools;
using Microsoft.Extensions.DependencyInjection;
using OpenAI;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        var appSettingsService = services.BuildServiceProvider().GetService<IAppSettings>()!;
        
        services.AddSingleton(new OpenAIClient(appSettingsService.OpenAiSettings.ApiKey));

        services.AddScoped<IAiGateway, OpenAiGateway>();
        services.AddScoped<IDocumentsTool, DocumentsTool>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<IDocumentPresenter, DocumentPresenter>();
        return services;
    }
}
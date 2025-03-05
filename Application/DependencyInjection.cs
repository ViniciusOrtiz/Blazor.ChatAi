using Application.Contracts.Settings;
using Application.Contracts.UseCases;
using Application.Models.Settings;
using Application.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = AppSettings.FromConfiguration(configuration);
        services.AddSingleton<IAppSettings>(appSettings);

        services.AddScoped<IAiAskQuestionUseCase, AiAskQuestionUseCase>();
        services.AddScoped<IUploadDocumentUseCase, UploadDocumentUseCase>();
        services.AddScoped<IGetAllFilesUseCase, GetAllFilesUseCase>();
        services.AddScoped<IFileGetContentUseCase, FileGetContentUseCase>();
        return services;
    }
}
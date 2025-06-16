
using AIDetective.Models;
using DetectiveAI.AI;
using DetectiveAI.Application;
using DetectiveAI.Models;
using DetectiveAI.Prompts;
using DetectiveAI.Story;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();

        var game = host.Services.GetRequiredService<DetectiveGame>();
        await game.StartAsync();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var cfg = context.Configuration;


                // Services
                services.AddSingleton<ILLMClient, GroqClient>();
                services.AddSingleton<IStoryLoader>(provider =>
                    new FileStoryLoader("story.json"));
                services.AddSingleton<DetectiveGameConfig>();
                services.AddSingleton<IPromptBuilder, PromptBuilder>();

                services.AddSingleton<DetectiveGame>();
            });
}


// Application/DetectiveGame.cs
using DetectiveAI.AI;
using DetectiveAI.Prompts;
using DetectiveAI.Story;
using System.Threading.Tasks;
using System;

namespace DetectiveAI.Application
{
    public class DetectiveGame
    {
        private readonly ILLMClient _llmClient;
        private readonly IStoryLoader _storyLoader;
        private readonly IPromptBuilder _promptBuilder;

        public DetectiveGame(ILLMClient llmClient, IStoryLoader storyLoader, IPromptBuilder promptBuilder)
        {
            _llmClient = llmClient;
            _storyLoader = storyLoader;
            _promptBuilder = promptBuilder;
        }

        public async Task StartAsync()
        {
            var story = _storyLoader.Load();
            Console.WriteLine("Welkom bij Detective AI! Hier is een overzicht van de zaak:\n");
            var introMessages = _promptBuilder.BuildIntroPrompt(story);
            var introResponse = await _llmClient.GetResponseAsync(introMessages);

            Console.WriteLine(introResponse + "\n");

            Console.WriteLine("Stel je vragen om de zaak op te lossen.\n");
            while (true)
            {
                Console.Write("> ");
                var userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput) || userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                var messages = _promptBuilder.BuildPlayerPrompt(story, userInput);
                var response = await _llmClient.GetResponseAsync(messages);

                Console.WriteLine("\nAI: " + response + "\n");
            }
        }
    }
}
using DetectiveAI.Models;
using DetectiveAI.Story;

namespace DetectiveAI.Prompts
{
    public interface IPromptBuilder
    {
        List<Message> BuildIntroPrompt(Investigation story);
        List<Message> BuildPlayerPrompt(Investigation story, string playerInput);
    }

}

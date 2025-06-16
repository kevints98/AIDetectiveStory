using AIDetective.Models;
using DetectiveAI.Models;
using DetectiveAI.Story;
using System.Collections.Generic;
using System.Text;

namespace DetectiveAI.Prompts
{
    public class PromptBuilder : IPromptBuilder
    {
        private readonly DetectiveGameConfig _config;

        public PromptBuilder(DetectiveGameConfig config)
        {
            _config = config;
        }

        public List<Message> BuildIntroPrompt(Investigation investigation)
        {
            var messages = new List<Message>
        {
            new Message
            {
                Role = "system",
                Content = $"{investigation.Rol}\n\nIntro:\n{investigation.Intro}\n\nDetails:\n{GetDetailsAsString(investigation.Details)}"
            },
            new Message
            {
                Role = "user",
                Content = _config.IntroInstruction
            }
        };
            return messages;
        }

        public List<Message> BuildPlayerPrompt(Investigation investigation, string playerInput)
        {
            var formattedInput = string.Format(_config.PlayerInputTemplate, playerInput);

            var messages = new List<Message>
        {
            new Message("system", @$"
            Je bent een AI-detectiveassistent in een mysterieus onderzoek. De speler stelt vragen of doet pogingen om de zaak op te lossen.

            Gedrag:
            - Beantwoord vragen kort en feitelijk op basis van het verhaal.
            - Als de speler een theorie noemt die (gedeeltelijk) overeenkomt met de oplossing:
              - Zeg expliciet dat hij in de goede richting zit.
              - Geef aan welk onderdeel van het verhaal nog ontbreekt, zonder het antwoord te geven.
            - Als de speler de volledige oplossing noemt, of iets wat in essentie hetzelfde betekent:
              - Bevestig dit enthousiast en duidelijk met zinnen als ""Dat klopt helemaal!"" of ""Je hebt de zaak opgelost!""
              - Vertel het volledige verhaal en sluit het mysterie af.
            - Geef geen hints of extra informatie tenzij de speler hier expliciet om vraagt.

            Belangrijk:
            - Gebruik alleen informatie uit de onderstaande casus.
            - Vermijd het stellen van terugvragen aan de speler.

            Casus:
            Titel: {investigation.Title}

            Introductie:
            {investigation.Intro}

            Details:
            {GetDetailsAsString(investigation.Details)}

            Oplossing (ter controle – niet spontaan geven):
            {investigation.Details.Oplossing}
            "),

        new Message
        {
            Role = "user",
            Content = $"Spelersinput: {playerInput}. Beantwoord als verteller. Als de speler een juiste of gedeeltelijke oplossing noemt, geef dit toe. Geef geen extra info tenzij daarom gevraagd is. Stel geen vragen terug."
        }
        };
            return messages;
        }

        private string GetDetailsAsString(Details details)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Characters:");
            foreach (var c in details.Characters)
            {
                sb.AppendLine($"- {c.Name}: {c.Role}");
            }
            sb.AppendLine("Clues:");
            foreach (var clue in details.Clues)
            {
                sb.AppendLine($"- {clue}");
            }
            return sb.ToString();
        }
    }
}
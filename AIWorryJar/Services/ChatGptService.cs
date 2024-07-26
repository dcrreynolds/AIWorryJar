using ChatGPT.Net;
using Microsoft.Extensions.Options;

namespace AIWorryJar.Services
{
    public class ChatGptService
    {
        const string gptErrorDefaultResponse = "Everything will be ok. You got this.";

        private readonly ChatGptOptions _options;
        private readonly ChatGPT.Net.ChatGpt _bot;
        private readonly ILogger<ChatGptService> _logger;

        private string response;

        public ChatGptService(IOptions<ChatGptOptions> options, ILogger<ChatGptService> logger)
        {
            _options = options.Value;
            _logger = logger;

            _bot = new ChatGpt(_options.OpenAIApiKey);            

            GenerateResponse();
        }

        public string BotResponse()
        {
            // there is a lag time in crating the response from chatGPT. Trigger a new one and use
            // the previous one.
            GenerateResponse();
            return response;
        }

        private async Task GenerateResponse()
        {
            try
            {
                response = await _bot.Ask(_options.Prompt);

                if (response == null)
                {
                    throw new Exception("Failed to get response from GPT.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                response = gptErrorDefaultResponse;
            }
        }
    }
}
using ChatGPT.Net;

public class ChatGptService
{
    private ChatGPT.Net.ChatGpt _bot;
    private string _prompt;
    private string response;

    public ChatGptService(string OpenAIApiKey, string prompt)
    {
        _bot = new ChatGpt(OpenAIApiKey);
        _prompt = prompt;

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
        response = await _bot.Ask(_prompt);
    }
}
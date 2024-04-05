using ChatGPT.Net;

public class ChatGptService
{
    private ChatGPT.Net.ChatGpt bot;

    public ChatGptService(string OpenAIApiKey)
    {
        bot = new ChatGpt(OpenAIApiKey);
    }

    public async Task<string> BotResponse(string prompt)
    {
        return await bot.Ask(prompt);
    }
}
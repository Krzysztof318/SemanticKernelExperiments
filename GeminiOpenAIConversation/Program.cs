// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Services;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var openAIKey = config["openaikey"]!;
var geminiKey = config["geminikey"]!;

string openaiModel = "gpt-3.5-turbo-1106";
string geminiModel = "gemini-pro";
var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(modelId: openaiModel, apiKey: openAIKey)
    .AddGoogleAIGeminiChatCompletion(modelId: geminiModel, apiKey: geminiKey)
    .Build();

Console.WriteLine("Setup complete");

string prompt = "Write mi story about elephants.";

var chats = kernel.GetAllServices<IChatCompletionService>();

foreach (var chat in chats)
{
    Console.WriteLine();
    Console.WriteLine(chat.GetModelId());
    var chatHistory = new ChatHistory();
    chatHistory.AddUserMessage(prompt);
    Console.Write("\t");
    Console.WriteLine(await chat.GetChatMessageContentAsync(chatHistory));
    Console.WriteLine();
}

Console.WriteLine("\nEND");
Console.ReadKey();
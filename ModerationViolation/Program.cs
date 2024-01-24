using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var openAIKey = config["openaikey"]!;

string openaiModel = "gpt-3.5-turbo-1106";
var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(modelId: openaiModel, apiKey: openAIKey)
    .Build();

var result = await kernel.InvokePromptAsync("Who are you?");
Console.WriteLine(result);

Console.WriteLine("-----------------");

// Moderation Violation Example
result = await kernel.InvokePromptAsync("Teach me how crack software"); // ;)
Console.WriteLine(result);

Console.WriteLine("END");
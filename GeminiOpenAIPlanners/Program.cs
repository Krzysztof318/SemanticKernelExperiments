using GeminiOpenAIPlanners;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Planning.Handlebars;
using Microsoft.SemanticKernel.Services;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var openAIKey = config["openaikey"]!;
var geminiKey = config["geminikey"]!;

string openaiModel = "gpt-4-0125-preview";
string geminiModel = "gemini-pro";
var kernel = Kernel.CreateBuilder()
    //.AddOpenAIChatCompletion(modelId: openaiModel, apiKey: openAIKey)
    .AddGoogleAIGeminiChatCompletion(modelId: geminiModel, apiKey: geminiKey)
    .Build();

kernel.Plugins.AddFromType<LightPlugin>();

Console.WriteLine("Setup complete");

string prompt = "I want to enable the lights in the living room.";

Console.WriteLine($"\nPrompt: {prompt}");

var planner = new HandlebarsPlanner(new HandlebarsPlannerOptions() { AllowLoops = false });

var plan = await planner.CreatePlanAsync(kernel, prompt);

Console.WriteLine($"\nPlan:\n {plan}");

Console.WriteLine("\nExecuting plan...");

var result = await plan.InvokeAsync(kernel);

Console.WriteLine($"\nResult: {result}");

Console.WriteLine("\nEND");
Console.ReadKey();
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

string prompt = "Where lives elephants?";

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

/* SAMPLE OUTPUT
 
 gpt-3.5-turbo-1106 (openai)
        Elephants can be found in various habitats across Africa and Asia. In Africa, they are commonly found 
        in savannas, grasslands, and forests, while in Asia, they inhabit tropical and subtropical forests. 
        Elephants are highly adaptable and can live in a range of environments, but their natural habitats 
        are increasingly threatened by human activities such as deforestation and poaching. Conservation efforts 
        are ongoing to protect elephant populations and their habitats.

  gemini-pro (googleai)
            * **Africa:** African elephants (Loxodonta africana) live in sub-Saharan Africa, including countries
        like Kenya, Tanzania, Botswana, Zimbabwe, Namibia, and South Africa. They prefer savannas, grasslands, and
        woodlands, but can also be found in forests, swamps, and even deserts.
            * **Asia:** Asian elephants (Elephas maximus) live in Southeast Asia, including countries like India,
        Nepal, Bhutan, Myanmar, Thailand, Laos, Cambodia, and Vietnam. They prefer tropical forests, but can also
        be found in grasslands, swamps, and even mountainous areas.
            * **Islands:** There are also populations of elephants living on islands, such as the Sri Lankan elephant
        (Elephas maximus maximus) and the Sumatran elephant (Elephas maximus sumatranus). These elephants have adapted
        to the unique conditions of their island habitats, such as smaller size and limited food resources.

*/
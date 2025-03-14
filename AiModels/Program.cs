using System.ComponentModel;
using Microsoft.Extensions.AI;


string[] models = ["mistral", "deepseek-r1", "llama3.2"]; // You have to load this models. See Readme
Console.WriteLine($"Voici les modèles de language disponibles :\n {string.Join("\n", models)}\nVeuillez choisir celui que vous voulez utiliser." +
                  " Il faudra redémarrer l'appli pour changer de modèle.");
var model = Console.ReadLine();
while (string.IsNullOrEmpty(model) || !models.Contains(model))
{
    Console.WriteLine("Veuillez choisir parmi les models disponibles.");
    model = Console.ReadLine();
}

using IChatClient ollamaClient = new OllamaChatClient(new Uri("http://localhost:11434/"), model);

var client = new ChatClientBuilder(ollamaClient)
    // .UseFunctionInvocation() // to use the Function tooling
    .Build();

Console.WriteLine("Hi! I am your intelligent assistant. What can I do for you?");

//Important if we want the model to remember the discussion
List<ChatMessage> chatHistory =
[
    new(ChatRole.System,
        "You are helpful assistant! Answer in a natural way. Answer in 4 sentence max. Answer in the language of the question.")
];
var chatOptions = new ChatOptions
{
    ToolMode = ChatToolMode.Auto,
    Tools =
    [
        // AIFunctionFactory.Create(Tools.GetProductDetailsByBarcode),
        // AIFunctionFactory.Create(GetWeather),
    ]
};

while (true)
{
    Console.WriteLine("User > ");

    var question = Console.ReadLine();
    if (question == null) continue;
    chatHistory.Add(new ChatMessage(ChatRole.User, question));
    var stream = client.CompleteStreamingAsync(chatHistory);
    await foreach (var response in stream)
    {
        Console.Write(response);
        chatHistory.Add(new ChatMessage(response.Role.Value, response.Text));
    }

    Console.WriteLine();
}

[Description("Get the weather of a city in degree Celsius. If no city is provide, ask the user to provide a city")]
static float GetWeather([Description("The city we want to get the weahter")] string? city = null)
{
    if (city == null) return 0;
    if (city.Length < 2) return 14;
    return 45.4f;
}
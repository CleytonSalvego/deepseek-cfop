using DocumentFormat.OpenXml.Presentation;
using Microsoft.KernelMemory;
using Microsoft.KernelMemory.AI.Ollama;

var config = new OllamaConfig
{
    Endpoint = "http://localhost:11434",
    TextModel = new OllamaModelConfig("deepseek-r1", 131072),
    EmbeddingModel = new OllamaModelConfig("deepseek-r1", 2048)
};

var memory = new KernelMemoryBuilder()
    .WithOllamaTextGeneration(config)
    .WithOllamaTextEmbeddingGeneration(config)
    .Build<MemoryServerless>();

Console.WriteLine("Processing document, please wait...");

//await memory.ImportDocumentAsync("cfop.txt", documentId: "DOC001");
await memory.ImportDocumentAsync("animal-social.pdf", documentId: "DOC001");


Console.WriteLine("Model is ready to take questions\n");

while (await memory.IsDocumentReadyAsync("DOC001"))
{

    Console.WriteLine("Ask your questions\n");

    var question = Console.ReadLine();

    var answer = await memory.AskAsync(question);

    Console.WriteLine(answer.Result);

    Console.WriteLine("\n Sources:");

    foreach (var x in answer.RelevantSources)
    {
        Console.WriteLine($" {x.SourceName} - {x.SourceUrl} - {x.Link}");
    }

}
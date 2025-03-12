using DeepSeekCFOP.Services;
using OllamaSharp;
using OllamaSharp.Models;


// Declaração das informações que serão utilizadas
const string OLLAMA_API_URL = "http://localhost:11434";
const string COLLECION_NAME = "cfop-data";
const string LLM_MODEL_NAME = "deepseek-r1";
const string QUERY = "CFOP";
const string QUESTION = "Qual é CFOP para venda fora do estado de SP que devo usar, me dê apenas o código sem explicação.";

Console.WriteLine("Configurando e iniciando a aplicação...");
var ollamaApiClient = new OllamaApiClient(OLLAMA_API_URL, LLM_MODEL_NAME);

Console.WriteLine("Configurando banco de dados...");
Database.SetupDatabase();

/*-------------------------------------------------------------------------
    Início    
    Esse trecho de código deverá ser utilizado apenas 1 vez 
    Quando precisar inserir ler um novo arquivo e gravar no banco de dados.
    Se for necessário realizar essa operação, descomente e rode 1x em seguida comente.
    Futuramente essa parte será um serviço de Upload de arquivos 
    Enquanto a leitura e a pergunta será outro serviço independente
---------------------------------------------------------------------------*/

////Lê os dados e divide em chunks
//var dataChunked = Shared.CreateFileContentChunk("cfop.txt").ToList();
////Cria os embeds a partir dos chunks
//var embededData = await OllamaService.CreateDataEmbeds(dataChunked);
////Cria uma nova coleção caso não exista
//await Database.CreateCollection(COLLECION_NAME);
////Insere os dados embeded ao Chroma Databaser
//var chunksIds = Shared.GetChunkIds(dataChunked);
//await Database.InsertData(COLLECION_NAME, chunksIds, dataChunked, embededData.Embeddings);

/*-------------------------------------------------------------------------
    Término
---------------------------------------------------------------------------*/

// Obtém os dados customizados para serem utilizados no contexto da pesquisa do LLM
var customContext = await Database.GetData(COLLECION_NAME, QUERY);

// Cria o prompt que será enviado ao LLM combinando o contexto obtido e a pergunta
string prompt = $"Contexto: {string.Join(", ", customContext.documents)}\nPergunta: {QUESTION}";
var request = new GenerateRequest()
{
    Model = LLM_MODEL_NAME,
    Prompt = prompt
};

// Realiza a pergunta ao LLM e apresenta o dados na tela
await foreach (var stream in ollamaApiClient.GenerateAsync(request))
{
    Console.Write(stream.Response);
}
  


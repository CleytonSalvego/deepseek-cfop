using OllamaSharp;
using OllamaSharp.Models;

namespace DeepSeekCFOP.Services
{
    public static class OllamaService
    {
        /// <summary>
        /// Utiliza o LLM DeepSeek para criar os Embedings que serão inseridos no Chroma Database
        /// </summary>
        /// <param name="dataChunked"></param>
        /// <returns></returns>
        public static async Task<EmbedResponse?> CreateDataEmbeds(List<string> dataChunked)
        {
            var ollamaApiClient = new OllamaApiClient("http://localhost:11434", "deepseek-r1");

            var input = new EmbedRequest()
            {
                Model = "deepseek-r1",
                Input = dataChunked
            };

            return await ollamaApiClient.EmbedAsync(input);

        }
    }
}

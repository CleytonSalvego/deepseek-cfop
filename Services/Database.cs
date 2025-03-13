using ChromaDB.Client.Models;
using DeepSeekCFOP.Domain.DTOs;
using System.Text;
using System.Text.Json;

namespace DeepSeekCFOP.Services
{
    public static class Database
    {
        static ChromaCollection collection;
        static HttpClient _httpClient = new HttpClient();
        static CollectionResponse? currentCollection = new CollectionResponse();

        const string TENANT = "default_tenant";
        const string DATABASE = "default_database";

        /// <summary>
        /// Configuração da Url base para requisições no Chroma Database
        /// </summary>
        public static async void SetupDatabase()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:8000/api/v2");
        }

        /// <summary>
        /// Cria uma nova coleção no Chroma Database
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static async Task CreateCollection(string collectionName)
        {
            try
            {
                var collectionExists = await VerifyCollectionExists(collectionName);

                if (collectionExists is not null)
                    return;

                var data = new
                { 
                    name = collectionName, 
                    get_or_create = true 
                };

                string json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"/api/v2/tenants/{TENANT}/databases/{DATABASE}/collections", content);

                string result = await response.Content.ReadAsStringAsync();
                //Console.WriteLine($"Status Code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CreateCollection: {ex.Message}");
            }
        }

        /// <summary>
        /// Insere os dados em vetores para o Chroma Database
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="ids"></param>
        /// <param name="dataChunked"></param>
        /// <param name="embeddings"></param>
        /// <returns></returns>
        public static async Task InsertData(string collectionName, List<string> ids, List<string> dataChunked, List<float[]> embeddings)
        {
            try
            {
                var collectionExists = await VerifyCollectionExists(collectionName);

                if (collectionExists is null)
                {
                    Console.WriteLine($"Error InsertData collection {collection} does not exists");
                    return;
                }
                    
                var data = new CollectionInsert()
                {
                    documents = dataChunked,
                    embeddings = embeddings,
                    ids = ids
                };

                string json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"/api/v2/tenants/{TENANT}/databases/{DATABASE}/collections/{currentCollection.id}/add", content);
                string result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CreateCollection: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém os embeds inseridos no Chroma Database
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<CollectionGet?> GetData(string collectionName, string filter)
        {
            try
            {
                var collectionExists = await VerifyCollectionExists(collectionName);
                if (collectionExists is null)
                {
                    Console.WriteLine($"Error GetData collection {collection} does not exists");
                    return null;
                }

                var data = new 
                {
                    query_texts = filter,
                    include = new[]{ "documents", "embeddings" },
                    limit = 100
                };

                string json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"/api/v2/tenants/{TENANT}/databases/{DATABASE}/collections/{currentCollection.id}/get", content);
                string result = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                var dataCollection = JsonSerializer.Deserialize<CollectionGet>(result);
                return dataCollection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CreateCollection: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Verifica se a coleção informada já existe no Chroma Database
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        private static async Task<CollectionResponse?> VerifyCollectionExists(string collectionName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/v2/tenants/{TENANT}/databases/{DATABASE}/collections/{collectionName}");
                string result = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                currentCollection = JsonSerializer.Deserialize<CollectionResponse>(result);
                return currentCollection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error VerifyCollectionExists: {ex.Message}");
                return null;
            }
        }
    }
}

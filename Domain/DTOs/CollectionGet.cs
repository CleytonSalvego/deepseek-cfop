namespace DeepSeekCFOP.Domain.DTOs
{
    public class CollectionGet
    {
        public List<string> ids { get; set; }
        public List<List<double>> embeddings { get; set; }
        public List<string> documents { get; set; }
        public object uris { get; set; }
        public object metadatas { get; set; }
        public List<string> include { get; set; }
    }
}

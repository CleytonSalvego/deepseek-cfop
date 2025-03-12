
namespace DeepSeekCFOP.Domain.DTOs
{
    public class CollectionInsert
    {
        public List<string> documents { get; set; }
        public List<float[]> embeddings { get; set; }
        public List<string> ids { get; set; }
        public List<Metadata> metadatas { get; set; }
        public List<string> uris { get; set; }
    }

    public class Metadata
    {
        public bool additionalProp1 { get; set; }
        public bool additionalProp2 { get; set; }
        public bool additionalProp3 { get; set; }
    }
}

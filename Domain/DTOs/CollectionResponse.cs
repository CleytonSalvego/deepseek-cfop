namespace DeepSeekCFOP.Domain.DTOs
{
    public class CollectionResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public ConfigurationJson configuration_json { get; set; }
        public object metadata { get; set; }
        public object dimension { get; set; }
        public string tenant { get; set; }
        public string database { get; set; }
        public int log_position { get; set; }
        public int version { get; set; }
    }

    public class ConfigurationJson
    {
        public string _type { get; set; }
        public HnswConfiguration hnsw_configuration { get; set; }
    }

    public class HnswConfiguration
    {
        public int M { get; set; }
        public string _type { get; set; }
        public int batch_size { get; set; }
        public int ef_construction { get; set; }
        public int ef_search { get; set; }
        public int num_threads { get; set; }
        public double resize_factor { get; set; }
        public string space { get; set; }
        public int sync_threshold { get; set; }
    }

    
}

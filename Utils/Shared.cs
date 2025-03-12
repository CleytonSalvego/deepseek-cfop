using System.Collections;

namespace DeepSeekCFOP.Utils
{
    public static class Shared
    {
        public static IEnumerable<string> CreateFileContentChunk(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Arquivo não encontrado", filePath);

            int size = 250;

            string content = File.ReadAllText(filePath);
            return content.Chunk(size).Select(chunk => new string(chunk)).ToList();


            //var data = File.ReadLines(@$"{filePath}");
            //IEnumerable<string[]>? chunkContent = data.Chunk(size);
            //foreach (var chunk in chunkContent)
            //{
            //    Console.WriteLine($"Number of lines in the file is: {chunk.Count()}");
            //    Console.WriteLine("Displaying the file content:-");
            //    DisplayText(chunk);
            //}
            //return chunkContent;
        }

        public static List<string> GetChunkIds(List<string> chunks)
        {
            return chunks.Select((chunk, index) => index.ToString()).ToList();
        }

        public static void DisplayText(IEnumerable fileContent)
        {
            foreach (var text in fileContent)
            {
                Console.WriteLine(text);
            }
        }
    }
}

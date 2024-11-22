using System.Text;

namespace AnalyzerUtils
{
    public class FileGenerator
    {
        public static void Generate(string filePath, string content)
        {
            var splitted = filePath.Split(".");
            splitted = splitted.Take(splitted.Length - 1).ToArray();
            var caminhoArquivo = string.Join(".", splitted) + ".il";
            var path = Path.GetDirectoryName(caminhoArquivo);

            try
            {
                using (FileStream fs = File.Create(caminhoArquivo))
                {
                    byte[] info = new ASCIIEncoding().GetBytes(content);
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

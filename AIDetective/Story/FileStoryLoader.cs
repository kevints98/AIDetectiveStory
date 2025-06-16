using DetectiveAI.Story;
using System.IO;
using System.Text.Json;

public class FileStoryLoader : IStoryLoader
{
    private readonly string _path;

    public FileStoryLoader(string path)
    {
        _path = path;
    }

    public Investigation Load()
    {
        var jsonContent = File.ReadAllText(_path);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var investigation = JsonSerializer.Deserialize<Investigation>(jsonContent, options);

        if (investigation == null)
        {
            throw new InvalidDataException("Kan de onderzoekdata niet laden of deserializen.");
        }

        return investigation;
    }
}

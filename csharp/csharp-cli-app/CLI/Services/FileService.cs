using Business.Interfaces;

namespace CLI.Services;

public class FileService : IFileService
{
    public async Task SaveToFileAsync(string path, string content, CancellationToken ct)
    {
        if (File.Exists(path))
        {
            await File.AppendAllTextAsync(path, content, ct);
        }
        else
        {
            await File.WriteAllTextAsync(path, content, ct);
        }
    }
}
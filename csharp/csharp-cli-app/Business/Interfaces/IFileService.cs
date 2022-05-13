namespace Business.Interfaces;

public interface IFileService
{
    Task SaveToFileAsync(string path, string content, CancellationToken ct);
}
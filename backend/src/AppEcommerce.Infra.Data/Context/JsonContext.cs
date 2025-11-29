using System.Text.Json;

namespace AppEcommerce.Infra.Data.Context;

public class JsonContext
{
    private readonly string _basePath;

    public JsonContext(IWebHostEnvironment env)
    {
        _basePath = Path.Combine(env.ContentRootPath, "DataJson");
        
        if (!Directory.Exists(_basePath))
            Directory.CreateDirectory(_basePath);
    }

    public async Task<List<T>> GetAsync<T>(string fileName)
    {
        var filePath = Path.Combine(_basePath, fileName);

        if (!File.Exists(filePath))
            return new List<T>();

        using var stream = File.OpenRead(filePath);
        return await JsonSerializer.DeserializeAsync<List<T>>(stream) ?? new List<T>();
    }

    public async Task SaveAsync<T>(string fileName, List<T> data)
    {
        var filePath = Path.Combine(_basePath, fileName);
        
        var options = new JsonSerializerOptions { WriteIndented = true };
        
        using var stream = File.Create(filePath);
        await JsonSerializer.SerializeAsync(stream, data, options);
    }
}
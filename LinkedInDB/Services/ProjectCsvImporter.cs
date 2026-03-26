using System.Text;
using System.Text.Json;
using LinkedInDB.Models;
using LinkedInDB.Services;

public class ProjectImporter
{
    private readonly HttpClient _http;
    private readonly int _profileId;

    public ProjectImporter(HttpClient http, int profileId)
    {
        _http = http;
        _profileId = profileId;
    }

    public async Task ImportProjectsCsv(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            var c = CsvHelper.ParseCsvLine(lines[i]);

            var project = new Project
            {
                ProfileId = _profileId,
                Title = c[0],
                Description = c[1],
                Url = c[2]
            };

            var json = JsonSerializer.Serialize(project);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Changed https to http
            await _http.PostAsync("http://localhost:5055/api/Projects", content);
        }
    }
}

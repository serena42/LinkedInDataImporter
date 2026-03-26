using LinkedInDB.Models;
using System.Text;
using System.Text.Json;

namespace LinkedInDB.Services;

public class SkillCsvImporter
{
    private readonly HttpClient _http;
    private readonly int _profileId;

    public SkillCsvImporter(HttpClient http, int profileId)
    {
        _http = http;
        _profileId = profileId;
    }

    public async Task ImportSkillsCsv(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        // Skip header row
        for (int i = 1; i < lines.Length; i++)
        {
            var columns = CsvHelper.ParseCsvLine(lines[i]);
            var skill = new Skill
            {
                ProfileId = _profileId,
                SkillName = columns[0]
            };

            var json = JsonSerializer.Serialize(skill);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _http.PostAsync("http://localhost:5055/api/Skills", content);
        }
    }
}

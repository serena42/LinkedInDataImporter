using LinkedInDB.Models;
using System.Text;
using System.Text.Json;

namespace LinkedInDB.Services;

public class ProfileCsvImporter 
{
    private readonly HttpClient _http;

    public ProfileCsvImporter(HttpClient http)
    {
        _http = http;
    }

    public async Task ImportProfileCsv(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            var columns = CsvHelper.ParseCsvLine(lines[i]);

            var profile = new Profile
            {
                FirstName = columns[0],
                LastName = columns[1],
                Headline = columns[5],
                Summary = columns[6],
                Industry = columns[7],
                Location = columns[3],
                Country = columns[9]
            };

            var json = JsonSerializer.Serialize(profile);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _http.PostAsync("http://localhost:5055/api/Profiles", content);
        }
    }
}

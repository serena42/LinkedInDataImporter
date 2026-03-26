using LinkedInDB.Models;
using System.Text;
using System.Text.Json;

namespace LinkedInDB.Services;

public class EducationCsvImporter
{
    private readonly HttpClient _http;
    private readonly int _profileId;

    public EducationCsvImporter(HttpClient http, int profileId)
    {
        _http = http;
        _profileId = profileId;
    }

    public async Task ImportEducationCsv(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++)
        {
            var columns = CsvHelper.ParseCsvLine(lines[i]);
            var education = new Education
            {
                ProfileId = _profileId,
                SchoolName = columns[0], // Updated from School
                DegreeName = columns[2], // Updated from Degree
                FieldOfStudy = columns[1]
            };

            var json = JsonSerializer.Serialize(education);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _http.PostAsync("http://localhost:5055/api/Educations", content);
        }
    }
}

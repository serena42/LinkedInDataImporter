using LinkedInDB.Models;
using System.Text;
using System.Text.Json;


namespace LinkedInDB.Services
{
    public class PositionCsvImporter
    {
        private readonly HttpClient _http;
        private readonly int _profileId;

        public PositionCsvImporter(HttpClient http, int profileId)
        {
            _http = http;
            _profileId = profileId;
        }

        public async Task ImportPositionsCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                var c = CsvHelper.ParseCsvLine(lines[i]);

                var position = new Position
                {
                    ProfileId = _profileId,
                    CompanyName = c[0],
                    Title = c[1],
                    Description = c[2],
                    Location = c[3],
                    StartedOn = ParseDate(c[4]),
                    FinishedOn = ParseDate(c[5])
                };

                var json = JsonSerializer.Serialize(position);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Changed https to http
                await _http.PostAsync("http://localhost:5055/api/Positions", content);
            }
        }

        private DateTime? ParseDate(string s)
        {
            if (DateTime.TryParse(s, out var dt))
                return dt;
            return null;
        }
    }

}

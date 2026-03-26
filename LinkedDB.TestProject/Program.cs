using LinkedInDB.Models;
using LinkedInDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.IO;

namespace LinkedInDB.TestProject;

// Fixed: Ensure this is in LinkedInDB\LinkedDB.TestProject\Program.cs
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("--- Starting LinkedIn Import from C:\\Files ---");
        var http = new HttpClient();

        // 1. Set the fixed path to C:\Files
        string importPath = @"C:\Files";
        string profilePath = Path.Combine(importPath, "Profile.csv");

        if (File.Exists(profilePath))
        {
            var profileImporter = new ProfileCsvImporter(http);
            string apiBaseUrl = "http://localhost:5055/api/Profiles";

            Console.WriteLine($"Importing profile from: {profilePath}");
            await profileImporter.ImportProfileCsv(profilePath);

            var profiles = await http.GetFromJsonAsync<List<Profile>>(apiBaseUrl);

            if (profiles != null && profiles.Any())
            {
                var profileId = profiles.OrderByDescending(p => p.Id).First().Id;
                Console.WriteLine($"Found Profile ID: {profileId}. Importing related data...");

                // 2. Import other files using the C:\Files path
                await new PositionCsvImporter(http, profileId).ImportPositionsCsv(Path.Combine(importPath, "Positions.csv"));
                await new EducationCsvImporter(http, profileId).ImportEducationCsv(Path.Combine(importPath, "Education.csv"));
                await new SkillCsvImporter(http, profileId).ImportSkillsCsv(Path.Combine(importPath, "Skills.csv"));

                Console.WriteLine("Import complete successfully.");
            }
            else
            {
                Console.WriteLine("Could not find the profile in the database after import.");
            }
        }
        else
        {
            Console.WriteLine($"Critical Error: File not found at {profilePath}");
            Console.WriteLine("Please make sure you have the 'Files' folder at the root of C: drive.");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
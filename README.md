# LinkedIn Data Importer

A C# utility for importing personal LinkedIn data exports into a SQLite database using Entity Framework Core. This tool processes LinkedIn's bulk export files and populates a structured relational database, enabling downstream applications to leverage rich professional profile and connection data.

## Overview

LinkedIn allows users to download their complete profile data through their account settings. However, the exported data is in CSV format and not immediately usable in applications. **LinkedIn Data Importer** bridges this gap by:

- Parsing LinkedIn CSV exports (connections, profile information, etc.)
- Validating and cleaning imported data
- Storing data in a structured SQLite database via Entity Framework Core
- Providing a foundation for applications that need LinkedIn profile context

## Quick Start

### Prerequisites

- .NET 10 or later
- Visual Studio 2026 (or VS Code with C# extensions)
- SQLite (included with Entity Framework Core)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/serena42/LinkedInDataImporter.git
   cd LinkedInDataImporter
   ```

2. Open the solution in Visual Studio:
   ```bash
   LinkedInDB.slnx
   ```

3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

4. Build the project:
   ```bash
   dotnet build
   ```

## Usage

### Step 1: Download Your LinkedIn Data

1. Log into LinkedIn
2. Go to **Settings & Privacy** → **Data privacy**
3. Under "Get a copy of your data," click **Request archive**
4. Wait for LinkedIn to prepare your data export (typically 24 hours)
5. Download the `.zip` file containing your personal data

### Step 2: Extract CSV Files

Extract the downloaded ZIP file. You'll find CSV files including:
- `Connections.csv` - Your LinkedIn connections
- `Profile.csv` - Your profile information
- Other data files (endorsements, recommendations, etc.)

### Step 3: Configure and Run the Importer

1. Open the `LinkedInDB` project
2. Update connection string in `appsettings.json` (if using a custom database path):
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=linkedin.db"
     }
   }
   ```

3. Update the CSV file paths in your import logic to point to your extracted LinkedIn files

4. Run the database migration (creates/updates schema):
   ```bash
   dotnet ef database update
   ```

5. Execute the import process (specific command depends on your console app entry point):
   ```bash
   dotnet run
   ```

The importer will:
- Parse each CSV file
- Create corresponding database records
- Log success/failure counts
- Store data in SQLite for querying

## Project Structure

```
LinkedInDataImporter/
├── LinkedInDB/                    # Main data import project
│   ├── Models/                    # Entity Framework models
│   │   ├── Connection.cs          # LinkedIn connection entity
│   │   ├── Profile.cs             # LinkedIn profile entity
│   │   └── ...                    # Other domain models
│   ├── Data/                      # EF Core DbContext
│   │   └── LinkedInContext.cs     # Database context configuration
│   ├── Services/                  # Import/parsing logic
│   │   └── LinkedInImportService.cs  # CSV parsing & data loading
│   ├── appsettings.json           # Configuration (DB connection)
│   └── Program.cs                 # Application entry point
│
├── LinkedDB.TestProject/          # Unit tests
│   ├── ImportServiceTests.cs      # Tests for data parsing
│   ├── ModelTests.cs              # Tests for entity validation
│   └── ...                        # Additional test suites
│
└── LinkedInDB.slnx                # Solution file
```

## Architecture & Design Decisions

### Entity Framework Core + SQLite

- **Why EF Core?** Provides ORM abstraction, enables migrations for schema versioning, and supports LINQ queries for downstream consumers
- **Why SQLite?** Lightweight, file-based, zero setup, ideal for personal data processing and integration with other .NET applications

### CSV Import Strategy

1. **Streaming Parse**: Reads CSV files line-by-line to handle large exports efficiently
2. **Validation**: Ensures required fields are present and data types are correct before database insertion
3. **Batch Insert**: Groups records for efficient bulk inserts (reduces round-trips to database)
4. **Error Handling**: Logs parsing errors and continues processing, preventing one bad record from failing the entire import

### Models

- **Connection**: Represents a LinkedIn connection (name, title, company, profile URL, etc.)
- **Profile**: Represents user's own profile (summary, skills, experience, education)
- Additional models capture endorsements, recommendations, and other LinkedIn data structures

## Testing

Run unit tests to verify import logic and data validation:

```bash
cd LinkedDB.TestProject
dotnet test
```

**Key test areas:**
- CSV parsing correctness (handles commas in fields, quoted strings, etc.)
- Entity validation (required fields, data type coercion)
- Database context initialization
- Error recovery (malformed rows don't crash the import)

## Data Privacy & Security

⚠️ **Important:** 
- This tool processes **your personal LinkedIn export**—keep it secure
- Do not commit your personal LinkedIn CSV files to version control (add to `.gitignore`)
- SQLite database files contain personal data; protect them like you would any private file
- Consider deleting the export ZIP and SQLite database once you've processed the data and migrated it to your production system

## Integration with Other Applications

Once data is imported into SQLite, you can:

1. **Query directly**: Use EF Core DbContext in other .NET applications
2. **Export**: Run LINQ queries to extract subsets of data for reporting
3. **Sync to other databases**: Use migrations or scripts to move processed data to production systems
4. **Feed into ML/AI**: Use LinkedIn profile context for interview coaching, skill matching, or career recommendations

## Questions?

If you have questions about the import process, data models, or integration patterns, feel free to open an Issue or reach out.

---

**Built with:** C#, .NET 10, Entity Framework Core, SQLite  
**Last Updated:** April 2026

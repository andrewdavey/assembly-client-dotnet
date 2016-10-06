# Assembly Client

Assembly .NET Client supports **.NET 4.5** and above.

## Installation

If using Visual Studio NuGet, ensure **prerelease** is checked in your NuGet Client.

Install the NuGet package into your Project:

```powershell
Install-Package assembly-client-dotnet
Install-Package Newtonsoft.Json
```

## Usage

Some examples of how to pull information from the Assembly Platform API using the client.

```c#
var config = new ApiConfiguration()
{
    Token = "my_oauth_access_token",
    RefreshToken = "my_oauth_refresh_token",
    ClientId = "my_client_id",
    ClientSecret = "my_client_secret"
};

// To connect to the live Platform, rather than the Sandbox environment, use simply: new ApiClient();
var client = new ApiClient(AssemblyEnvironment.Sandbox);
client.Configure(config);


// Registration Groups

// Fetch registration groups, filtered by a year group.
var regGroups = await client.RegistrationGroups.List(yearCode: "7");

foreach (var group in regGroups)
{
    Console.WriteLine($"Group Name: {group.Name}");
}


// Teaching Groups

// Fetch all teaching groups (you may know these as classes) for the mathematics subject code.
var mathsGroups = await client.TeachingGroups.List(subjectCode: "MA");

foreach (var group in mathsGroups)
{
    Console.WriteLine($"Group Name: {group.Name}");
}

// You can also apply a date filter to get, say, teaching groups from last academic year or for another specific range of time.
var mathsGroupsFiltered = await client.TeachingGroups.List(startDate: DateTime.Parse("2016-09-01"), endDate: DateTime.Parse("2017-07-31"));

// Fetch all the students for this teaching group.
var mathsStudents = await mathsGroups.First().Students();

foreach (var student in mathsStudents)
{
    Console.WriteLine($"{student.LastName}, {student.FirstName} ({student.YearCode})");
}


// Students

// Fetch all students filtered by year code/year group
var year7Students = await client.Students.List(yearCode: "7");

foreach (var student in year7Students)
{
    Console.WriteLine($"{student.LastName}, {student.FirstName} ({student.YearCode})");
}


// Year Groups

// Fetch all the year groups.
var yearGroups = await client.YearGroups.List();

// Fetch the students for this particular year group.
var yearGroupStudents = await yearGroups.First().Students();


// Academic Years

// Fetch all the academic years
var academicYears = await client.AcademicYears.List();

// View the terms for this academic year
var firstYearTerms = academicYears.First().Terms;

```

## Development

Install dotnet core: https://www.microsoft.com/net/core

Restore the packages `dotnet restore`

To run the tests `dotnet test`

If on OSX run `dotnet test -f netstandard1.1`

To run the tests continuously `dotnet watch test`

Build and Release are automatically run on commits to master (and accepted Pull Requests).

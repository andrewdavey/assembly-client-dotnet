# Assembly


## Installation

Install the NuGet package into your Project:

```powershell
Install-Package assembly-client-dotnet

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

var client = new ApiClient();
client.Configure(config);

// Fetch all teaching groups (you may know these as classes) for the mathematics subject code.

var mathsGroups = client.TeachingGroups.List(subjectCode: "MA");

foreach (var group in mathsGroups)
{
    Console.WriteLine($"Group Name: {group.Name}");
}

// Fetch all the students for this teaching group.

var mathsStudents = mathsGroups.First().Students();

foreach (var student in mathsStudents)
{
    Console.WriteLine($"{student.LastName}, {student.FirstName} ({student.YearCode})");
}

// ...or fetch all students filtered by year code/year group

var year7Students = client.Students.List(yearCode: "7");

foreach (var student in year7Students)
{
    Console.WriteLine($"{student.LastName}, {student.FirstName} ({student.YearCode})");
}

```

## Development

Install dotnet core: https://www.microsoft.com/net/core

To run the tests `dotnet test`

Build and Release are automatically run on commits to master (and accepted Pull Requests).

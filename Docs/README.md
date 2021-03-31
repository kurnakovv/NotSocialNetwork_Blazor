## NotSocialNetwork (logo)

![MainPage](Docs/ImgForReadme/Main.png)

> Description

## Table of contents
* [Architecture](#architecture)
* [Diagram](#diagram)
* [Projects](#projects)
* [Technologies](#technologies)
* [Versions](#versions)
* [How to start](#how-to-start)
* [Team](#team)
* [Status](#status)
* [Contacts](#contacts)

## Architecture


## Diagram
![Diagram](Docs/ImgForReadme/Diagram.png)

## Projects


## Technologies


## Versions


## How to start
### Required before the start:
* In the main root of the project open properties
* Choose Multiple startup projects
* Choose NotSocialNetwork.API (start) and NotSocialNetwork.UI (start), as shown in the screenshot:
![MultipleStartupProjects](ImgForReadme/StartProject/MultipleStartupProjects.png)
* Start project

### Database:
You can run the project without a database, as we initially use InMemoryDatabase, but if you need a database, follow the instructions:
* Src / Presentation / NotSocialNetwork.API / Startup.cs change in the ConfigureServices method:
``` csharp
// Database in memory.
// ConfigureInMemoryDatabase (services);
// Real database.
ConfigureProductionServices (services);
```
* Src / Presentation / NotSocialNetwork.API / Program.cs change in the Main method:
``` csharp
CreateHostBuilder(args).Build().Run();

#region Memory data (Hide if using real database)
    //var host = CreateHostBuilder(args).Build();

    //using (var scope = host.Services.CreateScope())
    //{
    //    var services = scope.ServiceProvider;
    //    var appDbContext = services.GetRequiredService<AppDbContext>();
    //    TestData.AddTestData(appDbContext);
    //}

    //host.Run();
 #endregion
```
* In the main root of the project, open a console (cmd or other)
* Check that you have everything by entering as in the screenshot:
```
dotnet ef
```
![DotnetEf](ImgForReadme/StartProject/DotnetEf.png)

> if something went wrong, read https://docs.microsoft.com/en-us/ef/core/cli/dotnet and return to the previous point

* enter:
```
dotnet ef database update -p .\Src\Infrastructure\NotSocialNetwork.DBContexts\ -s .\Src\Presentation\NotSocialNetwork.API\
```
* Start project


## Team


## Status


## Contacts


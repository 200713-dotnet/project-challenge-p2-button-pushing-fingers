# readme

## project

As a Team, please submit a project description in a marketing/business proposal style.
It should be no longer than 10 sentences and/or 7 lines.
It will be added to your portfolio along the tech stack mentioned below.


# Coal: the gamer marketplace

## architecture
- [solution] Coal.sln
     + [project-MVC] Coal.Client.csproj
     + [project-Web API] Coal.Domain.csproj
     + [project-classlib] Coal.Storing.csproj
     + [project-xunit] Coal.Testing.MVC.csproj
     + [project-xunit] Coal.Testing.API.csproj

## requirements
Project will support objects User, Library, Publisher, Game, Dlc (aka Downloadable-Content) , Mod

## user
a user should be able to:
+ access their account
+ look through a list of available games
+ purchase a game to add it to the user's library
+ access a game's page to see available dlc and mods
+ purchase dlc
+ attach and detach mods from game

## library
a library should:
+ save a list of a user's purchased games and dlc


## publisher
a publisher should be able to:
+ access their account
+ look at a list of game they have published
+ update dlc or mods for any of their games
+ create a new game
+ publish a game to post to the marketplace database

## game
+ each game must be able to have dlc
+ each game must be able to have mods
+ each game could have their dlc and mods updated by publisher

## user story
- a user logs into their account to access the user homepage
- they have the option to open the marketplace or their library
- if marketplace, 
- they can view a list of games and choose which ones to purchase
- they click on a game and can purchase it to add to their library
- they can purchase as many games as they want in the store
- if library, 
- they can go to their library to see their list of games
- they can look at a game's page to see a list of available dlc and mods
- they can purchase any dlc available and attach or detach any mods available

## publisher story
- a publisher logs into their account to access the publisher homepage
- they have the option to create a new game or see a list of their published games
- if creating a new game, 
- they can create a game with a title, description, price, dlc, and mods
- they can publish a game to the marketplace 
- else
- view/list all games published by publisher
- click on game to edit/update/delete game or game attributes

## technology stack

- .NET Core with C#, Singleton / Factory / Dependency Injection Patterns
- ASP.NET MVC Core with HTML, CSS
- EF Core with T-SQL, MSSQL, Repository / Unit of Work Patterns, Code-First Approach
- Agile/SCRUM with Trello / GitHub Project
- DevOps with Github Actions, YAML, SonarCloud, Azure Cloud
- Unit Testing with xUnit

## notes

- you can expect additional technology to be included, so keep your design as modular as possible
- you are allowed to use 1 or 2 additional packages/libraries to support your project

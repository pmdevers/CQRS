[![BCH compliance](https://bettercodehub.com/edge/badge/pmdevers/CQRS?branch=master)](https://bettercodehub.com/)

Command & Query Responsibility Segregation library
=====

This library contains the tools to simplefy the CQRS Pattern. 
All you have to worry about is the Domain aggregate and Commands and Events.

## 1. Setup the evironment

Execute the following command in the Package Manager Console

`Install-Package PMDEvers.CQRS -Version 1.0.0`

or from the commandline 

`dotnet add package PMDEvers.CQRS --version 1.0.0`

this will install the main package in the project.

## 2. Adding a event store

PMDEvers.CQRS Requires an event store for storing the events raised by the aggregate. 
With these events we are able to replay these events on the aggregate to rebuild the state of the aggregate.

For a SQL Server event store install the following command in the Package Manager Console

`Install-Package PMDEvers.CQRS.EntityFramework -Version 1.0.2`

or from the commandline

`dotnet add package PMDEvers.CQRS.EntityFramework --version 1.0.2`

## 3. Configure the services

open the startup.cs file and add the following lines.

``` csharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    // Add the servicebus services to the service collection
    services.AddServiceBus();
    // Add the CQRS services to the service collection
    services.AddCQRS();
    ...
}
```

__Please see the sample in the sample folder for a simple setup.__

## 4. Scaffolding 

We recommend using the following folders to structure youre domain.

```
    |--Sample
    |  |--Commands
    |  |  |--CreateSample.cs
    |  |--CommandHandlers	
    |  |  |--CreateSampleHandler.cs
    |  |--Events
    |  |  |--SampleCreated.cs
    |  |
    |  |--SampleAggregate.cs
```


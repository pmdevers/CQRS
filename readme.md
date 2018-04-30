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

## 4. Adding your first domain aggregate

Create a new class which inherts from *AggregateRoot*

``` csharp
public class SampleAggregate : AggregateRoot {
   ...
}
```

To let the CQRS Library know that we have created a new aggregate add the following line to the configuration. 


``` csharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    // Add the CQRS services to the service collection
    services.AddCQRS()
            .AddAggregate<SampleAggregate>();
    ...
}
```

We also need a command to construct the aggregate.

create a new class which inherts from *CommandBase*

``` csharp
public class CreateSample : CommandBase
{
    public CreateSample() : base(Guid.NewGuid())
    {
    }

    public override bool IsValid()
    {
        return true;
    }
}
```

*The class __CommandBase__ requires a AggregateId in its constructor.*

A __Command__ needs a __CommandHandler__

create a new class whih inherts from ICancellableAsyncCommandHandler< CreateSample>

```csharp
public class CreateSampleHandler : ICancellableAsyncCommandHandler<CreateSample>
{
   private readonly IRepository<SampleAggregate> _repository;

   public CreateSampleHandler(IRepository<SampleAggregate> repository)
   {
       _repository = repository;
   }

   public async Task HandleAsync(CreateSample command, CancellationToken cancellationToken = new CancellationToken())
   {
       cancellationToken.ThrowIfCancellationRequested();
       if (!command.IsValid())
       {
           return;
       }

       var aggregate = new SampleAggregate();

       await _repository.SaveAsync(aggregate, cancellationToken);
   }
}
```



## 5. Scaffolding 

We recommend using the following folders to structure youre domain.

```
    |--Sample
    |  |--Commands
    |  |  |--CreateSampleCommand.cs
    |  |--CommandHandlers	
    |  |  |--CreateSampleCommandHandler.cs
    |  |--Events
    |  |  |--SampleCreatedEvent.cs
    |  |
    |  |--SampleAggregate.cs
```


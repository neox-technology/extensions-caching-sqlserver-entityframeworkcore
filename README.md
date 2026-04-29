# Neox.Extensions.Caching.SqlServer.EntityFrameworkCore

`Neox.Extensions.Caching.SqlServer.EntityFrameworkCore` provides an EF Core model extension to register the SQL Server distributed cache table schema used by `Microsoft.Extensions.Caching.SqlServer`.

This is useful when you want EF Core migrations to create and manage the cache table in the same migration flow as the rest of your application.

## Installation

```bash
dotnet add package Neox.Extensions.Caching.SqlServer.EntityFrameworkCore
```

## Usage

Call the extension method in your `OnModelCreating` method:

```csharp
using Microsoft.EntityFrameworkCore;
using Neox.Extensions.Caching.SqlServer.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.WithSqlServerCacheEntity(tableName: "Cache", schemaName: "dbo");
    }
}
```

The generated entity maps the following columns:
- `Id`
- `Value`
- `ExpiresAtTime`
- `SlidingExpirationInSeconds`
- `AbsoluteExpiration`

## Compatibility

- .NET `10.0`
- `Microsoft.EntityFrameworkCore.SqlServer` `10.0.7`

## Build

```bash
dotnet build
```

## License

MIT.

## Contributing

Issues and pull requests are welcome. Please open an issue first for significant changes to discuss design and compatibility impact.
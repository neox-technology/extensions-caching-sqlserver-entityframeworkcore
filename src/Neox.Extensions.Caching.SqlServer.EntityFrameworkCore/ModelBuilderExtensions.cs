using Microsoft.EntityFrameworkCore;
using System;

namespace Neox.Extensions.Caching.SqlServer.EntityFrameworkCore {

    /// <summary>
    /// Provides helper methods to register SQL Server distributed cache entities in an EF Core model.
    /// </summary>
    public static class ModelBuilderExtensions {

        /// <summary>
        /// Registers the SQL Server distributed cache table shape in the EF Core model.
        /// </summary>
        /// <param name="modelBuilder">The EF Core model builder used to configure entities.</param>
        /// <param name="tableName">The cache table name.</param>
        /// <param name="schemaName">The cache table schema name. Defaults to <c>dbo</c>.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="tableName"/> or <paramref name="schemaName"/> is null, empty, or whitespace.
        /// </exception>
        /// <remarks>
        /// This maps the same columns used by <c>Microsoft.Extensions.Caching.SqlServer</c>:
        /// <c>Id</c>, <c>Value</c>, <c>ExpiresAtTime</c>, <c>SlidingExpirationInSeconds</c>, and <c>AbsoluteExpiration</c>.
        /// </remarks>
        public static void WithSqlServerCacheEntity(this ModelBuilder modelBuilder, string tableName, string schemaName = "dbo") {
            ArgumentException.ThrowIfNullOrWhiteSpace(tableName, nameof(tableName));
            ArgumentException.ThrowIfNullOrWhiteSpace(schemaName, nameof(schemaName));

            modelBuilder.Entity($"Microsoft.Extensions.Caching.Distributed.SqlServerCache.{schemaName}.{tableName}", b => {
                b.Property<string>("Id").HasMaxLength(449);
                b.Property<byte[]>("Value").IsRequired();
                b.Property<DateTimeOffset>("ExpiresAtTime");
                b.Property<long?>("SlidingExpirationInSeconds");
                b.Property<DateTimeOffset?>("AbsoluteExpiration");
                b.HasKey("Id");
                b.ToTable(tableName, schemaName);
            });
        }
    }
}
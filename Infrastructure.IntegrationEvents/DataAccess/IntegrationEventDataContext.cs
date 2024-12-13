using Infrastructure.IntegrationEvents.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationEvents.DataAccess
{
    /// <summary>
    /// DBContext to be used to Manage IntegrationEvent Table. Use only for Non Transactional scoped works. 
    /// </summary>
    public sealed class IntegrationEventDataContext : DbContext
    {
        private readonly string _connectionString;
        public readonly string DebugName;

        public IntegrationEventDataContext(DbContextOptions<IntegrationEventDataContext> options) : base(options)
        {
            _connectionString = Database.GetConnectionString();
            
            //Debugging Aid
            DebugName = NameProvider.GetNextName();
            Console.WriteLine($"***** Create Instance*********** : {DebugName}");
        }

        public IntegrationEventDataContext(DbContextOptions<IntegrationEventDataContext> options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
            //Debugging Aid
            DebugName = NameProvider.GetNextName();
            Console.WriteLine($"***** Create Instance*********** : {DebugName}");


        }

        public IntegrationEventDataContext(string connectionString)
        : this(new DbContextOptionsBuilder<IntegrationEventDataContext>().UseNpgsql(connectionString).Options, connectionString)
        {
        }
        public DbSet<IntegrationEventDetail> EventDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(IntegrationEventDataContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        public override void Dispose()
        {
            Console.WriteLine($"***** Disposing Instance*********** : {DebugName}");
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            Console.WriteLine($"***** Disposing Instance*********** : {DebugName}");
            return base.DisposeAsync();
        }

    }

/// <summary>
/// This is a Pure Helper Class for Helping Debugging
/// </summary>
public static class NameProvider
    {
        private static readonly List<string> _names;
        private static readonly object _lock = new();
        private static int _currentIndex = 0;

        // Static constructor to initialize the fixed set of names
        static NameProvider()
        {
            _names = new List<string>
        {
            "Alice", "Bob", "Charlie", "David", "Eve",
            "Frank", "Grace", "Hank", "Ivy", "Jack",
            "Kate", "Liam", "Mona", "Nina", "Oscar",
            "Paul", "Quinn", "Rose", "Steve", "Tina",
            "Uma", "Victor", "Wendy", "Xander", "Yara",
            "Zane", "Amber", "Brian", "Chloe", "Derek",
            "Ella", "Finn", "Gina", "Harry", "Isla",
            "James", "Kira", "Logan", "Mia", "Nathan",
            "Olivia", "Peter", "Quincy", "Ruby", "Sam",
            "Tara", "Ulysses", "Vera", "Will", "Xena"
        };
        }

        // Gets the name at the current index and increments the index
        public static string GetNextName()
        {
            lock (_lock)
            {
                if (_currentIndex >= _names.Count)
                    _currentIndex = 0;  // Reset to the first name once we reach the end

                return _names[_currentIndex++];
            }
        }

        // Gets the name at a specific index
        public static string GetName(int index)
        {
            if (index < 0 || index >= _names.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 0 and 49.");

            lock (_lock)
            {
                return _names[index];
            }
        }

        // Resets the current index to zero
        public static void ResetIndex()
        {
            lock (_lock)
            {
                _currentIndex = 0;
            }
        }
    }


}

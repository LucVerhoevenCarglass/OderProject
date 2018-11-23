using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Order_service.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        private string _connectionstring =
            "Data Source = (LocalDb)\\MSSQLLocalDb; Initial Catalog = OrderOrm; Integrated Security = True;";

        //private string _connectionstring =
        //    "Data Source = .\\SQLExpress; Initial Catalog = OrderOrm; Integrated Security = True;";


        public readonly ILoggerFactory efLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((category, level) => category.Contains("Command") && level == LogLevel.Information, true), });

        public DesignTimeDbContextFactory(ILoggerFactory efLoggerFactory)
        {
            this.efLoggerFactory = efLoggerFactory;
        }

        public DesignTimeDbContextFactory()
        {
        }

        public OrderContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseSqlServer(_connectionstring)
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(efLoggerFactory)
                .Options;

            return new OrderContext(options, efLoggerFactory);
        }
    }
}

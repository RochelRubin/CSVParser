using Microsoft.EntityFrameworkCore;

namespace CsvParser.Data
{
    public class PersonContext : DbContext
    {
        private readonly string _connectionString;

        public PersonContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Person> People { get; set; }
    }
}

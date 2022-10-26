
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvParser.Data

{
    public class PeopleRepo
    {
        private readonly string _connectionString;

        public PeopleRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddPerson(Person person)
        {
            using var ctx = new PersonContext(_connectionString);
            ctx.Add(person);
            ctx.SaveChanges();
        }

        public void DeleteAll()
        {
            using var ctx = new PersonContext(_connectionString);
            ctx.Database.ExecuteSqlRaw("TRUNCATE TABLE People");
            ctx.SaveChanges();
        }
        public List<Person> GetAll()
        {
            using var ctx = new PersonContext(_connectionString);
            return ctx.People.ToList();
        }
    }
}

using CsvHelper;
using CsvParser.Data;
using CsvParser.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvParser.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
        private readonly string _connectionString;
        public CSVController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [Route("generate")]
        [HttpGet]
        public IActionResult GenerateCSV(int amount)
        {
            var people = GeneratePeople(amount);
            var csv = GetCsv(people);
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", $"{amount} People.csv");

        }
        [Route("getall")]
        [HttpGet]
        public List<Person> GetAll()
        {
            var repo = new PeopleRepo(_connectionString);
            return repo.GetAll();
        }
        [HttpPost]
        [Route("deleteall")]
       public void DeleteAll()
        {
            var repo = new PeopleRepo(_connectionString);
            repo.DeleteAll();
        }
        [HttpPost]
        [Route("upload")]
        public List<Person> Upload(UploadViewModel viewModel)
        {
            int index = viewModel.Base64CSV.IndexOf(",") + 1;
            string base64 = viewModel.Base64CSV.Substring(index);
            byte[] csvBytes = Convert.FromBase64String(base64);
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            var people = reader.GetRecords<Person>().ToList();
            var repo = new PeopleRepo(_connectionString);
            foreach (var person in people)
            {
                repo.AddPerson(person);
            }
            return people;
        }
    
        public List<Person> GeneratePeople(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ =>
            {
            return new Person
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Age = Faker.RandomNumber.Next(10, 80),
                Address = Faker.Address.StreetAddress(),
                Email = Faker.Internet.Email()
            };
            }).ToList();
           
        }
        string GetCsv(List<Person> people)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            return builder.ToString();
        }
    }
}

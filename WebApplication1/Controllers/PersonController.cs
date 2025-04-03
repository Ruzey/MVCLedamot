using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }
        /*
        public string GetUppdrag(Person person)
        {
            var uppdrag = person.personuppgift.uppgift.FirstOrDefault(u => u.kod == "Uppdrag inom riksdag och regering");

            return uppdrag;
        }*/
        public async Task<IActionResult> Index(string searchName, string searchParty, int? birthYear, string birthYearFilter)
        {
            var personData = await _personService.GetPersonsAsync();
            var persons = personData.personlista.person;

            ViewBag.UniqueNames = persons.Select(p => p.tilltalsnamn).Distinct().OrderBy(n => n).ToList();

            if (!string.IsNullOrEmpty(searchName))
            {
                persons = persons.Where(p => p.tilltalsnamn.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.UniqueParties = persons.Select(p => p.parti).Distinct().OrderBy(p => p).ToList();

            if (!string.IsNullOrEmpty(searchParty))
            {
                persons = persons.Where(p => p.parti.Equals(searchParty, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (birthYear.HasValue)
            {
                switch (birthYearFilter)
                {
                    case "before":
                        persons = persons.Where(p => int.Parse(p.fodd_ar) < birthYear.Value).ToList();
                        break;
                    case "after":
                        persons = persons.Where(p => int.Parse(p.fodd_ar) > birthYear.Value).ToList();
                        break;
                    case "exact":
                    default:
                        persons = persons.Where(p => int.Parse(p.fodd_ar) == birthYear.Value).ToList();
                        break;
                }
            }

            return View(persons);
        }

        public async Task<IActionResult> RandomPerson()
        {
            var allPersons = await _personService.GetPersonsAsync();
            var persons = allPersons.personlista.person;

            var randomPerson = persons.OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            ViewBag.RandomPerson = randomPerson;
            ViewBag.UniqueNames = persons.Select(p => p.tilltalsnamn).Distinct().OrderBy(n => n).ToList();

            ViewBag.UniqueParties = persons.Select(p => p.parti).Distinct().OrderBy(p => p).ToList();
            return View("Index", new List<Person> { randomPerson });
        }
    }
}

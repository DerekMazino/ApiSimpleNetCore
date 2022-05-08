using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using TallerMicroservicioAPISimple.Models;

namespace TallerMicroservicioAPISimple.Controllers
{
    [ApiController]
    [Route("demo")]
    public class PersonController: ControllerBase
    {

        [HttpGet("greeting/{id}")]
        public ActionResult<Dictionary<string,string>> GetById(int id)
        {
            PersonModel person= Persons.Single(x => x.Id == id);
            string message = "No hay Datos";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (person.Lang.Equals("ES")) 
            {
                message = string.Format("Hola, {0}", person.Name);
            }
            else if (person.Lang.Equals("EN"))
            {
                message = string.Format("Hello, {0}", person.Name);
            }
            else if (person.Lang.Equals("FR"))
            {
                message = string.Format("Bonjour, {0}", person.Name);
            }
            dic.Add(person.Id.ToString(), message);
            return dic;
        }

        [HttpGet]
        public ActionResult<List<PersonModel>> GetAll()
        {
            return Persons;
        }

        [HttpPost("register")]
        public ActionResult Create(PersonModel model)
        {
            model.Id = Persons.Count() + 1;
            Persons.Add(model);
            return CreatedAtAction(
                "GetById",
                new { id = model.Id },
                model
                );
        }

        public static List<PersonModel> Persons = new List<PersonModel> { 
            new PersonModel{
                Id= 1,
                Name = "Camilo",
                Lang = "ES"
            },
            new PersonModel{ Id=2, Name = "Valentina", Lang="EN"}
        };
    }
}

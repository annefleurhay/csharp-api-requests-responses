using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using request_response.Models;

namespace request_response.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguageController : ControllerBase
    {
        private static List<Language> _languages = new List<Language>();


        public LanguageController()
        {

            if(_languages.Count == 0)
            {
                _languages.Add(new Language() { Name = "Javascript"});
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("Create")]
        public async Task<IResult> CreateStudent(Language language)
        {
            _languages.Add(language);
            return Results.Created("boolean.com", language);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("All")]
        public async Task<IResult> GetAll()
        {
            return Results.Ok(_languages);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("One")]
        public async Task<IResult> GetOne(string name)
        {
            var oneLanguage = _languages.Where(l => l.Name == name).FirstOrDefault();
            return oneLanguage != null ? Results.Ok(oneLanguage) : Results.NotFound();

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("Update")]
        public async Task<IResult> Update(string name, Language newLanguage)
        {

            var updateLanguage = _languages.Where(l => l.Name == name).FirstOrDefault();
            if (updateLanguage == null) return Results.NotFound();


            updateLanguage.Name = string.IsNullOrEmpty(newLanguage.Name) ? updateLanguage.Name : newLanguage.Name;

            return Results.Created("boolean.com", updateLanguage);

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Delete")]
        public async Task<IResult> Delete(string name)
        {
            var language = _languages.Where(l => l.Name == name).FirstOrDefault();
            int result = _languages.RemoveAll(l => l.Name == name);
            return result >= 0 && language != null ? Results.Ok(name) : Results.NotFound();
        }
    }
}
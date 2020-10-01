using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tema2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return StudentRepo.Students;
        }

        [HttpPost]
        public string Post([FromBody] Student student){
            try
            {
                StudentRepo.Students.Add(student);
                return " Student adaugat cu succes!!!";
            }
            catch (System.Exception e)
            {
                return "Eroare!" + e.Message;
                throw;

            }
        }
    }
}

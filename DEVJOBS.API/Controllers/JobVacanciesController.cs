using DEVJOBS.API.Entities;
using DEVJOBS.API.Models;
using DEVJOBS.API.Persistence;
using DEVJOBS.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEVJOBS.API.Controllers
{
    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;

        }
        //GET api/job-vacancies
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobVacancies = _repository.GetAll();

            return Ok(jobVacancies);
        }
        //GET api/job-vacancies/4
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            return Ok(jobVacancy);
        }
        //POST api/job-vacancies
        /// <summary>
        /// Cadastrar uma vaga de emprego
        /// </summary>
        /// <remarks>
        /// {
        ///"title": "Dev .NET Jr",
        ///"description": "Vaga para sustentação de aplicações .NET CORE",
        ///"company": "LeoDev",
        ///"isRemote": true,
        ///"salaryRange": "3000-5000"
        ///}
        ///</remarks>
        /// <param name="model">Dados da vaga.</param>
        /// <returns>Obejto Recém criado.</returns>
        /// <response code ="201">Sucesso.</response>
        /// <responde code ="400">Dados inválidos.</responde>
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            var jobVacancy = new JobVacancy(model.Title, model.Description, model.Company, model.IsRemote, model.SalaryRange);
            if (jobVacancy.Title.Length > 30)
                return BadRequest("Título precisa ter menos de 30 caracteres");
            _repository.Add(jobVacancy);
            return CreatedAtAction("GetById", new { id = jobVacancy.Id }, jobVacancy);
        }
        //PUT api/job-vacancies/4
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);

            _repository.Update(jobVacancy);

            return NoContent();
        }
    }
}

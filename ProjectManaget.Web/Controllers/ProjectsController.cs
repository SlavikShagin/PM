using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Database;
using ProjectManager.Services.Developer;
using ProjectManager.Services.Project;
using ProjectManager.Web.Models.Projects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IValidator<CreateProjectHttpPostModel> _validatorCreateProjectModel;
        private readonly IValidator<UpdateProjectHttpPutModel> _validatorUpdateProjectModel;
        private readonly IProjectService _projectService;
        private readonly IDeveloperService _developerService;

        public ProjectsController(
            IValidator<CreateProjectHttpPostModel> validatorCreateProjectModel,
            IValidator<UpdateProjectHttpPutModel> validatorUpdateProjectModel,
            IProjectService projectService,
            IDeveloperService developerService)
        {
            _projectService = projectService;
            _developerService = developerService;
            _validatorCreateProjectModel = validatorCreateProjectModel;
            _validatorUpdateProjectModel = validatorUpdateProjectModel;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var getAllProjects = await _projectService.GetAll();

            AllProjectsDetailsHttpGetModel vm = new AllProjectsDetailsHttpGetModel()
            {
                ProjectsList = getAllProjects,
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AjaxPrjPost([FromBody] CreateProjectHttpPostModel vm)
        {
            var validationResult = _validatorCreateProjectModel.Validate(vm);

            if (!validationResult.IsValid)
            {
                var errorResponse = new
                {
                    ErrorType = "ValidationError",
                    Errors = validationResult.Errors
                };

                return BadRequest(errorResponse);
            }

            await _projectService.Add(vm.Name, vm.ProjectSubject);

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> AjaxPrjDetails([FromQuery] int projectId)
        {
            var currentPrj = await _projectService.GetById(projectId);

            ProjectsDetailsHttpGetModel vm = new ProjectsDetailsHttpGetModel()
            {
                Project = currentPrj,
            };

            return PartialView(vm);
        }

        [HttpPut]
        public async Task<IActionResult> AjaxPrjEdit([FromBody] UpdateProjectHttpPutModel vm)
        {
            var validationResult = _validatorUpdateProjectModel.Validate(vm);

            if (!validationResult.IsValid)
            {
                var errorResponse = new
                {
                    ErrorType = "ValidationError",
                    Errors = validationResult.Errors
                };

                return BadRequest(errorResponse);
            }

            await _projectService.UpdateProject(vm.Id, vm.Name, vm.ProjectSubject, vm.Developers);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> AjaxDeletePrj()
        {
            int projectId = Int32.Parse(Request.Query["id"]);
            await _projectService.DeleteProject(projectId);

            return Ok();
        }

    }
}

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Services.Developer;
using ProjectManager.Services.Link;
using ProjectManager.Services.Project;
using ProjectManager.Web.Models;
using ProjectManager.Web.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IValidator<CreateProjectHttpPostModel> _validatorCreateProjectModel;
        private readonly IValidator<UpdateProjectHttpPutModel> _validatorUpdateProjectModel;
        private readonly IValidator<LinkDevToProjectHttpPostModel> _validatorLinkDevToProjectHttpPostModel;
        private readonly IProjectService _projectService;
        private readonly IDeveloperService _developerService;
        private readonly ILinkDevToProjectService _linkDevToProjectService;

        public ProjectsController(
            IValidator<CreateProjectHttpPostModel> validatorCreateProjectModel,
            IValidator<UpdateProjectHttpPutModel> validatorUpdateProjectModel,
            IValidator<LinkDevToProjectHttpPostModel> validatorLinkDevToProjectHttpPostModel,
            IProjectService projectService,
            IDeveloperService developerService,
            ILinkDevToProjectService linkDevToProjectService)
        {
            _projectService = projectService;
            _developerService = developerService;
            _linkDevToProjectService = linkDevToProjectService;
            _validatorCreateProjectModel = validatorCreateProjectModel;
            _validatorUpdateProjectModel = validatorUpdateProjectModel;
            _validatorLinkDevToProjectHttpPostModel = validatorLinkDevToProjectHttpPostModel;
        }

        [HttpGet]
        public async Task<IActionResult> List(int page = 1)
        {
            var allProjects = await _projectService.GetAll();
            //generic + services
            int pageSize = 3;
            var count = allProjects.Count();
            var data = allProjects.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            //eof

            //ProjectListResponce

            var vm = new AllProjectsDetailsHttpGetModel()
            {
                PageViewModel = pageViewModel,
                ProjectsList = data,
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

            var vm = new ProjectsDetailsHttpGetModel()
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

            await _projectService.UpdateProject(vm.Id, vm.Name, vm.ProjectSubject);

            return Json(new { success = true });
        }

        [HttpDelete]
        public async Task<IActionResult> AjaxDeletePrj()
        {
            int projectId = int.Parse(Request.Query["id"]);
            await _projectService.DeleteProject(projectId);

            return Json(new { success = true });
        }

        [HttpPut]
        public async Task<IActionResult> AjaxAddLink([FromBody] LinkDevToProjectHttpPostModel vm)
        {
            var validationResult = _validatorLinkDevToProjectHttpPostModel.Validate(vm);

            if (!validationResult.IsValid)
            {
                var errorResponse = new
                {
                    ErrorType = "ValidationError",
                    Errors = validationResult.Errors
                };

                return BadRequest(errorResponse);
            }

            await _linkDevToProjectService.Link(vm.developerId, vm.projectId);

            return Json(new { success = true });
        }
        [HttpPut]
        public async Task<IActionResult> AjaxRemoveLink([FromBody] LinkDevToProjectHttpPostModel vm)
        {
            var validationResult = _validatorLinkDevToProjectHttpPostModel.Validate(vm);

            if (!validationResult.IsValid)
            {
                var errorResponse = new
                {
                    ErrorType = "ValidationError",
                    Errors = validationResult.Errors
                };

                return BadRequest(errorResponse);
            }

            await _linkDevToProjectService.UnLink(vm.developerId, vm.projectId);

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<JsonResult> AjaxProjectsList()
        {
            var getAllProjects = await _projectService.GetAll();
            return Json(getAllProjects);
        }

        [HttpPost]
        public IActionResult SearchByName(string search)
        {
            //AllProjectsDetailsHttpGetModel projects = _projectService.GetByName(search);

            return PartialView(_projectService.GetByName(search));
        }
    }
}

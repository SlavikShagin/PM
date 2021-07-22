using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Services.Developer;
using ProjectManager.Web.Models.Developers;
using System;
using System.Threading.Tasks;

namespace ProjectManager.Web.Controllers
{
    public class DevelopersController : Controller
    {
        private readonly IValidator<CreateDeveloperHttpPostModel> _validatorDeveloperModel;
        private readonly IDeveloperService _developerService;

        public DevelopersController(
            IDeveloperService developerService,
            IValidator<CreateDeveloperHttpPostModel> validatorDeveloperModel)
        {
            _developerService = developerService;
            _validatorDeveloperModel = validatorDeveloperModel;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var getAllDev = await _developerService.GetAll();

            AllDevelopersDetailsHttpGetModel vm = new AllDevelopersDetailsHttpGetModel()
            {
                DevelopersList = getAllDev,
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AjaxDevPost([FromBody] CreateDeveloperHttpPostModel vm)
        {
            var validationResult = _validatorDeveloperModel.Validate(vm);

            if (!validationResult.IsValid)
            {
                var errorResponse = new
                {
                    ErrorType = "ValidationError",
                    Errors = validationResult.Errors
                };

                return BadRequest(errorResponse);
            }

            await _developerService.Add(vm.FirstName, vm.LastName, vm.EMail, vm.Phone);

            return Json(new { success = true });
        }

        [HttpDelete]
        public async Task<IActionResult> AjaxDeleteDev()
        {
            int developerId = Int32.Parse(Request.Query["id"]);
            await _developerService.DeleteEntry(developerId);

            return Ok();
        }
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Services.Developer;
using ProjectManager.Web.Models;
using ProjectManager.Web.Models.Developers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Web.Controllers
{
    public class DevelopersController : Controller
    {
        private readonly IValidator<CreateDeveloperHttpPostModel> _validatorCreateDeveloperModel;
        private readonly IValidator<UpdateDeveloperHttpPutModel> _validatorEditDeveloperModel;
        private readonly IDeveloperService _developerService;

        public DevelopersController(
            IDeveloperService developerService,
            IValidator<CreateDeveloperHttpPostModel> validatorCreateDeveloperModel,
            IValidator<UpdateDeveloperHttpPutModel> validatorEditDeveloperModel)
        {
            _developerService = developerService;
            _validatorCreateDeveloperModel = validatorCreateDeveloperModel;
            _validatorEditDeveloperModel = validatorEditDeveloperModel;
        }
        [HttpGet]
        public async Task<IActionResult> List(int page = 1)
        {
            var getAllDev = await _developerService.GetAll();
            int pageSize = 3;
            var count = getAllDev.Count();
            var data = getAllDev.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            var vm = new AllDevelopersDetailsHttpGetModel()
            {
                PageViewModel = pageViewModel,
                DevelopersList = data,
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<JsonResult> AjaxDevelopersList()
        {
            var getAllDev = await _developerService.GetAll();
            return Json(getAllDev);
        }

        [HttpPost]
        public async Task<IActionResult> AjaxDevPost([FromBody] CreateDeveloperHttpPostModel vm)
        {
            var validationResult = _validatorCreateDeveloperModel.Validate(vm);

            if (!validationResult.IsValid)
            {
                var errorResponse = new
                {
                    ErrorType = "ValidationError",
                    Errors = validationResult.Errors
                };

                return BadRequest(errorResponse);
            }
            //check if email exists
            var checkEmail = _developerService.CheckIfEmailExists(vm.EMail);

            if (checkEmail == "")
            {
                await _developerService.Add(vm.FirstName, vm.LastName, vm.EMail, vm.Phone);

                return Json(new { success = true });
            }
            else if (checkEmail == vm.EMail)
            {
                return Conflict("E-Mail already exists");
            }
            else
            {
                return Ok();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> AjaxDeleteDev()
        {
            int developerId = Int32.Parse(Request.Query["id"]);
            await _developerService.DeleteEntry(developerId);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AjaxDevEdit([FromForm] UpdateDeveloperHttpPutModel vm)
        {
            var validationResult = _validatorEditDeveloperModel.Validate(vm);

            if (!validationResult.IsValid)
            {
                var errorResponse = new
                {
                    ErrorType = "ValidationError",
                    Errors = validationResult.Errors
                };

                return BadRequest(errorResponse);
            }

            await _developerService.EditEntry(vm.Id, vm.FirstName, vm.LastName, vm.EMail, vm.Phone);

            return Json(new { success = true });
        }
    }
}

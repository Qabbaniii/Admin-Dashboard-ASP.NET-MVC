using Dashboard.BLL.Dto_s.DepartmentDto_s;
using Dashboard.BLL.Services.DepartmentService;
using Dashboard.PL.ViewModels.DepartmentVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {

        private readonly IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DepartmentController(IDepartmentServices DepartmentServices, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment)
        {
            departmentServices = DepartmentServices;
            this.logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var depts = departmentServices.GetAllDepartments();
            return View(depts);
        }
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]

        public IActionResult Create(DepartmentViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = new CreatedDepartmentDto
                    {

                        Code = viewModel.Code,
                        Name = viewModel.Name,
                        Description = viewModel.Description
                    };
                    int Result = departmentServices.AddDepartment(dto);
                    if (Result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't be Created");
                        return View(dto);
                    }
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                // log Exception
                // Development -> log at console
                if (webHostEnvironment.IsDevelopment())
                {
                    logger.LogError(ex.Message);
                    return View(viewModel);
                }
                else
                {
                    return View(viewModel);
                    // Production -> store at table in DataBase
                }

            }
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest();
            var Department = departmentServices.GetDepartmentByID(id.Value);
            if (Department == null) return NotFound();
            return View(Department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return BadRequest();
            var Department = departmentServices.GetDepartmentByID(id.Value);
            if (Department == null) return NotFound();
            var viewDepartment = new DepartmentViewModel
            {
                ID = Department.ID,
                Code = Department.Code,
                Name = Department.Name,
                Description = Department.Description

            };
            return View(viewDepartment);
        }
        [HttpPost]
        public IActionResult Edit(DepartmentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var Department = new UpdatedDepartmentDto
            {
                ID = model.ID,
                Code = model.Code,
                Name = model.Name,
                Description = model.Description
            };

            try
            {
                if (ModelState.IsValid)
                {
                    int Result = departmentServices.UpdateDepartment(Department);
                    if (Result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't be uPDATED");
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // log Exception
                // Development -> log at console
                if (webHostEnvironment.IsDevelopment())
                {
                    logger.LogError(ex.Message);
                    return View(model);
                }
                else
                {
                    return View(model);
                    // Production -> store at table in DataBase
                }

            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var department = departmentServices.GetDepartmentByID(id.Value);

            return View(department);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted = departmentServices.SoftDeleteDepartment(id);
                //var IsDeleted = departmentServices.DeleteDepartment(id);
                if (IsDeleted)
                    return RedirectToAction("Index");
                message = "Department is not delelted";
            }
            catch (Exception ex)
            {
                //log exeption
                logger.LogError(ex.Message);
                //set message
                message = webHostEnvironment.IsDevelopment() ? ex.Message : "An error has been occured during deleting the Department";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Delete), id);


        }


    }
}
using Dashboard.BLL.Dto_s.EmployeeDto_s;
using Dashboard.BLL.Services.EmployeeService;
using Dashboard.PL.ViewModels.EmployeeVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }
        public IActionResult Index(string? searchValue)
        {
            if (searchValue == null)
                return View(employeeServices.GetAllEmployees());
            else
                return View(employeeServices.GetSearchedEmployees(searchValue));
        }
        public IActionResult Details(int id)
        {
            var Employees = employeeServices.GetEmployeeByID(id);
            return View(Employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createdEmployee = new CreatedEmployeeDto
                    {
                        Name = viewModel.Name,
                        Age = viewModel.Age,
                        Address = viewModel.Address,
                        Salary = viewModel.Salary,
                        IsActive = viewModel.IsActive,
                        Email = viewModel.Email,
                        PhoneNumber = viewModel.PhoneNumber,
                        HiringDate = viewModel.HiringDate,
                        Gender = viewModel.Gender,
                        EmployeeType = viewModel.EmployeeType,
                        Image = viewModel.Image,
                    };
                    int Result = employeeServices.CreateEmployee(createdEmployee);
                    if (Result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't be Created");
                        return View(viewModel);
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
                if (environment.IsDevelopment())
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
        public IActionResult Edit(int? id)
        {
            if (id == null) return BadRequest();
            var Employee = employeeServices.GetEmployeeByID(id.Value);
            if (Employee == null) return NotFound();
            var mappedEmployee = new EmployeeViewModel
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Age = Employee.Age,
                Address = Employee.Address,
                Salary = Employee.Salary,
                IsActive = Employee.IsActive,
                Email = Employee.Email,
                EmployeeType = Employee.EmployeeType,
                PhoneNumber = Employee.PhoneNumber,
                HiringDate = Employee.HiringDate,
                Gender = Employee.Gender,
            };
            return View(mappedEmployee);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                if (ModelState.IsValid)
                {
                    var updatedEmployee = new UpdatedEmployeeDto
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        Age = viewModel.Age,
                        Address = viewModel.Address,
                        Salary = viewModel.Salary,
                        IsActive = viewModel.IsActive,
                        Email = viewModel.Email,
                        PhoneNumber = viewModel.PhoneNumber,
                        HiringDate = viewModel.HiringDate,
                        Gender = viewModel.Gender,
                        EmployeeType = viewModel.EmployeeType,
                        Image = viewModel.Image,
                    };
                    int Result = employeeServices.UpdateEmployee(updatedEmployee);
                    if (Result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't be uPDATED");
                        return View(viewModel);
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
                if (environment.IsDevelopment())
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

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var department = employeeServices.GetEmployeeByID(id.Value);

            return View(department);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted = employeeServices.SoftDeleteEmployee(id);
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
                message = environment.IsDevelopment() ? ex.Message : "An error has been occured during deleting the Department";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Delete), id);


        }
    }
}
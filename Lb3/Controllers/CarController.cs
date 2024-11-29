using Microsoft.AspNetCore.Mvc;
using Lb3.Models;
using Lb3.Services;

namespace Lb3.Controllers
{
    public class CarController : Controller
    {
        private readonly CarEvaluationService _carEvaluationService;

        public CarController(CarEvaluationService carEvaluationService)
        {
            _carEvaluationService = carEvaluationService;
        }

        public IActionResult Index()
        {
            ViewBag.IsModelAvailable = _carEvaluationService.IsModelAvailable();
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarEvaluationModel model)
        {
            if (ModelState.IsValid)
            {
                // Створюємо об'єкт Car для передбачення
                var input = new Car
                {
                    Buying = model.Buying,
                    Maint = model.Maint,
                    Doors = model.Doors,
                    Persons = model.Persons,
                    LugBoot = model.LugBoot,
                    Safety = model.Safety,
                    Evaluation = "" // Evaluation не використовується для передбачення
                };

                model.PredictedEvaluation = _carEvaluationService.Predict(input);
                return RedirectToAction("Result", model);
            }

            return View(model);
        }


        public IActionResult Result(CarEvaluationModel model)
        {
            return View(model);
        }

        public IActionResult TrainModel()
        {
            _carEvaluationService.TrainModel();
            ViewBag.Message = "Модель успішно натренована та збережена!";
            return View("TrainModel");
        }

    }
}

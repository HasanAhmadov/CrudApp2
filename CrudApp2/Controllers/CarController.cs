using Microsoft.AspNetCore.Mvc;
using CrudApp2.Services;
using CrudApp2.Models;

namespace CrudApp2.Controllers
{
    public class CarController : Controller
    {
        private readonly CarDataService _carDataService;

        public CarController(CarDataService carDataService)
        {
            _carDataService= carDataService;
        }

        public IActionResult Index()
        {
            var products = CarDataService.GetAllProducts();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _carDataService.AddProduct(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public IActionResult Edit(int id)
        {
            var product = CarDataService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_carDataService.UpdateProduct(car))
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(car);
        }

        public IActionResult Delete(int id)
        {
            var car = CarDataService.GetProductById(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _carDataService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

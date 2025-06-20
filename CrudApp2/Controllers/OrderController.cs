using CrudApp2.Models;
using CrudApp2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrudApp2.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly OrderDataService _orderDataService;

        public OrderController(OrderDataService orderDataService)
        {
            _orderDataService = orderDataService;
        }

        public IActionResult Index()
        {
            var orders = OrderDataService.GetAllOrders();

            foreach (var order in orders)
            {
                if (order.Car == null)
                {
                    order.Car = new Car { Brand = "Not Found" };
                }
                if (order.CreatedUser == null)
                {
                    order.CreatedUser = new User { Name = "Unknown" };
                }
            }

            return View(orders);
        }

        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(UserDataService.GetAllUsers(), "UserId", "Name");
            ViewBag.Cars = new SelectList(CarDataService.GetAllProducts(), "Id", "Brand");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CarId,CreatedUserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                var car = CarDataService.GetProductById(order.CarId);
                if (car == null)
                {
                    ModelState.AddModelError("CarId", "Selected car doesn't exist");
                    ViewBag.Users = new SelectList(UserDataService.GetAllUsers(), "UserId", "Name");
                    ViewBag.Cars = new SelectList(CarDataService.GetAllProducts(), "Id", "Brand");
                    return View(order);
                }

                _orderDataService.AddOrder(order);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Users = new SelectList(UserDataService.GetAllUsers(), "IUserId", "Name");
            ViewBag.Cars = new SelectList(CarDataService.GetAllProducts(), "Id", "Brand");
            return View(order);
        }

        public IActionResult Edit(int id)
        {
            var order = _orderDataService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            var users = UserDataService.GetAllUsers();
            var cars = CarDataService.GetAllProducts();

            if (users == null || cars == null)
            {
                return NotFound();
            }

            ViewBag.Users = new SelectList(users, "UserId", "Name", order.CreatedUserId);
            ViewBag.Cars = new SelectList(cars, "Id", "Brand", order.CarId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_orderDataService.UpdateOrder(order))
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            var users = UserDataService.GetAllUsers();
            var cars = CarDataService.GetAllProducts();

            if (users == null || cars == null)
            {
                return NotFound();
            }

            ViewBag.Users = new SelectList(users, "UserId", "Name", order.CreatedUserId);
            ViewBag.Cars = new SelectList(cars, "Id", "Brand", order.CarId);
            return View(order);
        }

        public IActionResult Delete(int id)
        {
            var order = _orderDataService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderDataService.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search()
        {
            var model = new OrderSearchModel();

            ViewBag.UserNames = new SelectList(
                UserDataService.GetAllUsers().Select(u => u.Name).Distinct()
            );

            ViewBag.CarBrands = new SelectList(
                CarDataService.GetAllProducts().Select(c => c.Brand).Distinct()
            );

            return View(model);
        }

        [HttpPost]
        public IActionResult Search(OrderSearchModel searchModel)
        {
            var allOrders = OrderDataService.GetAllOrders();

            var query = allOrders.AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.UserName))
            {
                query = query.Where(o =>
                    o.CreatedUser != null &&
                    o.CreatedUser.Name.Contains(searchModel.UserName));
            }

            if (!string.IsNullOrEmpty(searchModel.CarBrand))
            {
                query = query.Where(o =>
                    o.Car != null &&
                    o.Car.Brand.Contains(searchModel.CarBrand));
            }

            if (searchModel.FromDate.HasValue)
            {
                query = query.Where(o => o.CreatedDate >= searchModel.FromDate);
            }

            if (searchModel.ToDate.HasValue)
            {
                query = query.Where(o => o.CreatedDate <= searchModel.ToDate);
            }

            searchModel.Results = query.ToList();

            ViewBag.UserNames = new SelectList(
                UserDataService.GetAllUsers().Select(u => u.Name).Distinct(),
                searchModel.UserName
            );

            ViewBag.CarBrands = new SelectList(
                CarDataService.GetAllProducts().Select(c => c.Brand).Distinct(),
                searchModel.CarBrand
            );

            return View(searchModel);
        }

    }
}
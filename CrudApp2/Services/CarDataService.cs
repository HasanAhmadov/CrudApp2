using CrudApp2.Models;

namespace CrudApp2.Services
{
    public class CarDataService : OrderDataService
    {
        public static readonly List<Car> _cars = new List<Car>();

        private static int _carIdCounter = 1;

        static CarDataService()
        {
            _cars.AddRange(new[]
            {
                new Car { Id = _carIdCounter++, Brand = "Mercedes" },
                new Car { Id = _carIdCounter++, Brand = "BMW" },
                new Car { Id = _carIdCounter++, Brand = "Audi" },
                new Car { Id = _carIdCounter++, Brand  ="Hyundai"},
                new Car { Id = _carIdCounter++, Brand  ="KIA"},
                new Car { Id = _carIdCounter++, Brand  ="Toyota"},
                new Car { Id = _carIdCounter++, Brand  ="Changan"}
            });
        }

        public static List<Car> GetAllProducts()
        { 
            return _cars;
        }

        public static Car? GetProductById(int id)
        {
            return _cars.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Car car)
        {
            car.Id = _carIdCounter++;
            _cars.Add(car);
        }

        public bool UpdateProduct(Car car)
        {
            var existingProduct = _cars.FirstOrDefault(p => p.Id == car.Id);
            if (existingProduct != null)
            {
                existingProduct.Brand = car.Brand;
                return true;
            }
            return false;
        }

        public bool DeleteProduct(int id)
        {
            var product = _cars.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _cars.Remove(product);
                _orders.RemoveAll(o => o.CarId== id);
                return true;
            }
            return false;
        }

    }
}

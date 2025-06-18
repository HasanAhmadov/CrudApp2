using CrudApp2.Models;

namespace CrudApp2.Services
{
    public class OrderDataService
    {
        public static readonly List<Order> _orders = new List<Order>();
        private static int _orderIdCounter = 1;

        static OrderDataService()
        {
            _orders.AddRange(new[]
            {
                new Order { Id = _orderIdCounter++, CarId = 1, CreatedDate = DateTime.Now.AddDays(-7), CreatedUserId = 1 },
                new Order { Id = _orderIdCounter++, CarId = 2, CreatedDate = DateTime.Now.AddDays(-5), CreatedUserId = 2 },
                new Order { Id = _orderIdCounter++, CarId = 3, CreatedDate = DateTime.Now.AddDays(-3), CreatedUserId = 3 },
                new Order { Id = _orderIdCounter++, CarId = 4, CreatedDate = DateTime.Now.AddDays(-1), CreatedUserId = 4 }
            });
        }

        public static List<Order> GetAllOrders()
        {
            return _orders.Select(o => new Order
            {
                Id = o.Id,
                CarId = o.CarId,
                Car = CarDataService.GetProductById(o.CarId), 
                CreatedUserId = o.CreatedUserId,
                CreatedUser = UserDataService.GetUserById(o.CreatedUserId), 
                CreatedDate = o.CreatedDate
            }).ToList();
        }

        public Order? GetOrderById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.CreatedUser = UserDataService.GetUserById(order.CreatedUserId);
                order.Car = CarDataService.GetProductById(order.CarId);
            }
            return order;
        }

        public void AddOrder(Order order)
        {
            order.Id = _orderIdCounter++;
            order.CreatedDate = DateTime.Now;
            _orders.Add(order);
        }

        public bool UpdateOrder(Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder != null)
            {
                existingOrder.CarId = order.CarId;
                existingOrder.CreatedUserId = order.CreatedUserId;
                return true;
            }   
            return false;
        }

        public bool DeleteOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                _orders.Remove(order);
                return true;
            }
            return false;
        }

    }
}

using Ecom.Database;
using Ecom.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Ecom.Service
{
    public class OrdersService
    {
        #region Singleton
        public static OrdersService Instance
        {
            get
            {
                if (instance == null) instance = new OrdersService();

                return instance;
            }
        }
        private static OrdersService instance { get; set; }

        private OrdersService()
        {
        }

        #endregion


        public List<Order> SearchOrders(string userId, string status, int pageNo, int pageSize)
        {
            using (var context = new EContext())
            {
                var orders = context.Orders.ToList();



                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(x => x.UserId.ToLower().Contains(userId.ToLower())).ToList();
                }

                return orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }


        }


        public int SearchOrderCount(string userId, string status)
        {
            using (var context = new EContext())
            {
                var orders = context.Orders.ToList();



                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(x => x.UserId.ToLower().Contains(userId.ToLower())).ToList();
                }

                return orders.Count;
            }
        }

        public Order GetOrderdById(int id)
        {
            using (var context = new EContext())
            {
                return context.Orders.Where(c => c.Id == id).Include(x => x.OrderItems).Include("OrderItems.Product")
                    .FirstOrDefault();

            }
        }
        public bool UpdateOrderStatus(int Id, string status)
        {
            using (var context = new EContext())
            {
                var order = context.Orders.Find(Id);

                order.Status = status;

                context.Entry(order).State = EntityState.Modified;

                return context.SaveChanges() > 0;
            }
        }

        public void DeleteOrder(int id)
        {
            using (var context = new EContext())
            {
                var order = context.Orders.Find(id);
                context.Orders.Remove(order);
                context.SaveChanges();
            }



        }
    }
}

using Ecom.Database;
using Ecom.Models;

namespace Ecom.Service
{
    public class ShopService
    {
        #region Singleton
        public static ShopService Instance
        {
            get
            {
                if (instance == null) instance = new ShopService();

                return instance;
            }
        }
        private static ShopService instance { get; set; }

        private ShopService()
        {
        }

        #endregion


        public int SaveOrder(Order order)
        {
            using (var context = new EContext())
            {
                context.Orders.Add(order);
                return context.SaveChanges();

                //context.Orders.Add(order);
                //context.SaveChanges();
            }

        }

    }
}

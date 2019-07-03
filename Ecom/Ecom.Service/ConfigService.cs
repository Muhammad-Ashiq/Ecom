using Ecom.Database;
using Ecom.Models;

namespace Ecom.Service
{
    public class ConfigService
    {
        #region Singleton

        public static ConfigService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigService();
                return instance;
            }

        }


        private static ConfigService instance { get; set; }

        private ConfigService()
        {
        }
        #endregion


        public Config GetConfig(string Key)
        {
            using (var context = new EContext())
            {
                return context.Configs.Find(Key);
            }
        }
        public int PageSize()
        {
            using (var context = new EContext())
            {
                var pageSizeConfig = context.Configs.Find("PageSize");
                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }
        }

        public int ShopPageSize()
        {
            using (var context = new EContext())
            {
                var pageSizeConfig = context.Configs.Find("ShopPageSize");
                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }
        }



    }
}
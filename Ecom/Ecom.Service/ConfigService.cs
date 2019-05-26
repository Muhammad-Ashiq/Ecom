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



    }
}
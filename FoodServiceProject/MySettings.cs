using LinqToDB.Configuration;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodServiceProject
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }

    public class MySettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders
            => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "Postgres";
        public string DefaultDataProvider => "PostgreSQL";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                // note that you can return multiple ConnectionStringSettings instances here
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "FoodDB",
                        ProviderName = ProviderName.PostgreSQL,
                        ConnectionString =
                            @"Server=localhost;Port=5432;Database=postgres;UserId=postgres;Password=admin;"
                    };
            }
        }
    }
}

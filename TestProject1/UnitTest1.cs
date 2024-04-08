using DataModels;
using FoodServiceProject;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;

namespace TestProject1
{
    [TestFixture]
    public class Tests 
    {
        MySettings settings;
        [SetUp]
        public void SetSettings()
        {
            DataConnection.DefaultSettings = new MySettings();
        }
        [Test]
        public void DisplayCustomersTest()
        {
            using (var db = new PostgresDB())
            {
                var q =
                    from c in db.Customers
                    select c;

                foreach (var c in q)
                    Console.WriteLine(String.Format("{0} {1} {2} {3}", c.CustomerID, c.Name, c.Email, c.LastAddress));
            }
        }

    }

    [TestFixture]
    public class CRUDTests
    {
        [SetUp]
        public void SetSettings()
        {
            DataConnection.DefaultSettings = new MySettings();
        }

        
    }
}

using System;
using System.Linq;
using App.Data.DbContexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Test.IntegrationTest.Mapping
{
    [TestClass]
    public class ShopNewsMapTest
    {
        [TestMethod]
        public void ReadCollection()
        {

            using (var db = new AppDbContext())
            {
                var query = (from r in db.ShopNews
                    select r).ToList();

                Assert.IsNotNull(query);
            }
        }
    }
}

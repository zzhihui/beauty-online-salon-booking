using System;
using System.Linq;
using App.Data.DbContexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Test.IntegrationTest.Mapping
{
    [TestClass]
    public class PaymentTypeMapTest
    {
        [TestMethod]
        public void ReadCollection()
        {

            using (var db = new AppDbContext())
            {
                var query = (from r in db.PaymentTypes
                    select r).ToList();

                Assert.IsNotNull(query);
            }
        }
    }
}

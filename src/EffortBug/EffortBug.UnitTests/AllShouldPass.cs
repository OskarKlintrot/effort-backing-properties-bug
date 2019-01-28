using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Effort.DataLoaders;
using Effort.Provider;
using EffortBug.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;

namespace EffortBug.UnitTests
{
    [TestClass]
    public class AllShouldPass
    {
        private ObjectData _data;
        private EffortConnection _dbConnection;

        [TestInitialize]
        public void TestInitialize()
        {
            EffortProviderConfiguration.RegisterProvider();
            _data = new ObjectData();
            _dbConnection = Effort.DbConnectionFactory.CreateTransient(new ObjectDataLoader(_data));
        }

        [TestMethod]
        public async Task CountEntities()
        {
            // Arrange
            _data.Table<MyEntity>().Add(new MyEntity
            {
                Id = 1,
                Name = "Test",
                Date = new LocalDate(2019, 01, 28)
            });

            var count = 0;

            // Act
            using (var dbContext = new MvpDbContext(_dbConnection, true))
            {
                count = await dbContext.MyEntities
                    .CountAsync();
            }

            // Assert
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public async Task RetriveDate()
        {
            // Arrange
            _data.Table<MyEntity>().Add(new MyEntity
            {
                Id = 1,
                Name = "Test",
                Date = new LocalDate(2019, 01, 28)
            });

            MyEntity myEntity = null;

            // Act
            using (var dbContext = new MvpDbContext(_dbConnection, true))
            {
                myEntity = await dbContext.MyEntities
                    .Where(x => x.Id == 1)
                    .SingleAsync();
            }

            // Assert
            Assert.AreEqual(new LocalDate(2019, 01, 28), myEntity.Date);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public async Task NotSupported()
        {
            // Arrange
            // Act
            using (var dbContext = new MvpDbContext(_dbConnection, true))
            {
                await dbContext.MyEntities
                    .Select(x => x.Date)
                    .SingleAsync();
            }

            // Assert
            // See ExpectedExceptionAttribute
        }
    }
}

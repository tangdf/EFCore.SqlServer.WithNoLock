using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer.WithNoLock;
using Xunit;
using Xunit.Abstractions;

namespace EFCore.SqlServer.WithNoLock.UnitTest
{
    public class WithNoLock_Test
    {
        private ITestOutputHelper _testOutputHelper;

        public WithNoLock_Test(ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            using (var dataContext = new SampleDbContext(this._testOutputHelper))
            {
                var id = 2;
                Category entity = dataContext.Categories.WithNoLock().Where(item => item.CategoryID == id).Single();

                
 
            }
        }
    }
}
